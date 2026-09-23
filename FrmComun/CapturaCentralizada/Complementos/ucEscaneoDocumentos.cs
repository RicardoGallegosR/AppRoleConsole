using FrmComun.Utils;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Utils;
using WIA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System.Drawing.Imaging;


/*
 MEJORAR LA VELOCIDAD DE SCANNER CON TWAIN 
 
 */
namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucEscaneoDocumentos : UserControl {
        public event EventHandler? EscanearClick;
        public event EventHandler? CancelarClick;
        public event EventHandler? GuardarClick;
        public event EventHandler<DocumentoEscaneadoEventArgs>?  DocumentoAceptado;
        public string? ScannerId => cbScanners.SelectedValue?.ToString();
        public Guid? VerificacionId { get; set; }
        public string RutaBase { get; set; } = string.Empty;
        private byte[]? _paginaEscaneada;
        private bool _seleccionando;
        private Point _inicioSeleccion;
        private Rectangle _rectSeleccion;
        private bool _modoRecorte;
        private Bitmap? _imagenEscaneadaOriginal;
        private Bitmap? _imagenTrabajo;
        private sealed class PaginaEscaneada {
            public byte[] Original { get; set; } = Array.Empty<byte>();
            public byte[] Actual { get; set; } = Array.Empty<byte>();
        }
        private readonly List<PaginaEscaneada> _paginas = new();
        private int _paginaActual = -1;
        public int NumeroPaginas => _paginas.Count;

        private float _zoomVista = 0.35f;
        private float _zoom = 0.50f;

        private const float ZoomMinimo = 0.25f;
        private const float ZoomMaximo = 2.00f;
        private const float PasoZoom = 0.25f;



        public ucEscaneoDocumentos() {
            InitializeComponent();

            ibEscanear.Click += (s, e) => EscanearClick?.Invoke(this, e);
            ibCancelar.Click += (s, e) => CancelarClick?.Invoke(this, e);
            ibGuardar.Click += (s, e) => GuardarClick?.Invoke(this, e);

            cbScanners.DropDownStyle = ComboBoxStyle.DropDownList;
            cbScanners.MaxDropDownItems = 8;

            cbScanners.IntegralHeight = false;
            cbScanners.DropDownHeight = 150;
            cbScanners.DropDownWidth = 400;


            pbDocumento.MouseDown += pbDocumento_MouseDown;
            pbDocumento.MouseMove += pbDocumento_MouseMove;
            pbDocumento.MouseUp += pbDocumento_MouseUp;
            pbDocumento.Paint += pbDocumento_Paint;
            ibAgregar.Click += ibAgregar_Click;
            ibRecortar.Click += ibRecortar_Click;
            ibEliminar.Click += ibEliminar_Click;

            ibZoomMas.Click += ibZoomMas_Click;
            ibZoomMenos.Click += ibZoomMenos_Click;

        }


        private void ibZoomMas_Click(
    object sender,
    EventArgs e) {
            _zoom = Math.Min(
                ZoomMaximo,
                _zoom + PasoZoom);

            AplicarZoom();

            lblEstadoScanner.Text =
                $"Zoom {(int)(_zoom * 100)}%";
        }
        #region Buscar escaneres 
        private void CargarScanners() {
            try {
                var scanners = new List<ScannerDeviceDto>();
                var deviceManager = new DeviceManager();

                foreach (DeviceInfo device in deviceManager.DeviceInfos) {
                    if (device.Type != WiaDeviceType.ScannerDeviceType)
                        continue;

                    string nombre = device.DeviceID;

                    try {
                        nombre = device.Properties["Name"].get_Value()?.ToString() ?? device.DeviceID;
                    } catch (Exception ex) {
                        SivevLogger.Error($"{ex.Message}", SivevOrigen.Captura);
                    }

                    scanners.Add(new ScannerDeviceDto {
                        Id = device.DeviceID,
                        Nombre = nombre
                    });
                }

                cbScanners.DataSource = null;
                cbScanners.DisplayMember = nameof(ScannerDeviceDto.Nombre);
                cbScanners.ValueMember = nameof(ScannerDeviceDto.Id);
                cbScanners.DataSource = scanners;

                if (scanners.Count == 1)
                    cbScanners.SelectedIndex = 0;

                ibActualizar.Enabled = scanners.Count > 0;

            } catch (Exception ex) {
                cbScanners.DataSource = null;
                ibActualizar.Enabled = false;
                SivevLogger.Warning($"No fue posible detectar los escáneres.\n\n{ex.Message}", SivevOrigen.Captura);
                Mostrar.Mensaje("Escáner", $"No fue posible detectar los escáneres.\n\n{ex.Message}");
            }
        }
        #endregion

        #region Configuracion de ComboBox


        #endregion
        #region Eventos
        #region Load
        private void ucEscaneoDocumentos_Load(object sender, EventArgs e) {
            CargarScanners();
        }
        #endregion



        #endregion
        #region Recortar
        /*
         Inicia la selección del área a recortar: valida que exista imagen, 
        que esté activo el modo recorte y registra el punto inicial del mouse.
         */
        private void pbDocumento_MouseDown(object? sender, MouseEventArgs e) {
            if (!_modoRecorte || e.Button != MouseButtons.Left || pbDocumento.Image is null)
                return;

            RectangleF visible = ObtenerRectanguloImagenVisible();

            if (!visible.Contains(e.Location))
                return;

            _seleccionando = true;
            _inicioSeleccion = e.Location;
            _rectSeleccion = Rectangle.Empty;
            pbDocumento.Capture = true;
        }
        private void pbDocumento_MouseMove(object? sender, MouseEventArgs e) {
            if (!_modoRecorte || !_seleccionando)
                return;

            ActualizarRectSeleccion(e.Location);
            pbDocumento.Invalidate();
        }
        private void pbDocumento_Paint(object? sender, PaintEventArgs e) {
            if (_rectSeleccion.Width <= 0 || _rectSeleccion.Height <= 0)
                return;
            using var pen = new Pen(Color.Red, 2);
            e.Graphics.DrawRectangle(pen, _rectSeleccion);
        }

        /*
         Finaliza la selección del recorte: actualiza el área, valida su tamaño, 
        aplica el recorte y refresca la imagen mostrando el resultado al usuario.
         */
        private void pbDocumento_MouseUp(object? sender, MouseEventArgs e) {
            if (!_modoRecorte || !_seleccionando)
                return;

            ActualizarRectSeleccion(e.Location);

            _seleccionando = false;
            pbDocumento.Capture = false;

            if (_rectSeleccion.Width < 10 || _rectSeleccion.Height < 10) {
                _rectSeleccion = Rectangle.Empty;
                pbDocumento.Invalidate();
                lblEstadoScanner.Text = "Área de recorte inválida";
                return;
            }

            bool recortado = RecortarSeleccion();

            if (!recortado) {
                _rectSeleccion = Rectangle.Empty;
                _modoRecorte = false;
                pbDocumento.Cursor =  Cursors.Default;
                pbDocumento.Invalidate();
                lblEstadoScanner.Text = "No fue posible realizar el recorte";
                return;
            }

            MostrarImagenTrabajo();
            _modoRecorte = false;
            _rectSeleccion = Rectangle.Empty;
            pbDocumento.Cursor = Cursors.Default;

            lblEstadoScanner.Text = "Documento recortado";
            ibAceptar.Enabled = true;
        }
        private void MostrarImagenTrabajo() {
            if (_imagenTrabajo is null)
                return;
            //_zoomVista = CalcularZoomVista();
            int ancho = Math.Max(
        1,
        (int)(_imagenTrabajo.Width * _zoomVista));

            int alto = Math.Max(
        1,
        (int)(_imagenTrabajo.Height * _zoomVista));

            Bitmap vistaReducida =
        new Bitmap(
            _imagenTrabajo,
            ancho,
            alto);

            Image? anterior =
        pbDocumento.Image;

            pbDocumento.Image =
                vistaReducida;

            anterior?.Dispose();

            pbDocumento.SizeMode =
                PictureBoxSizeMode.AutoSize;

            pbDocumento.Invalidate();
        }
        private bool RecortarSeleccion() {
            if (_imagenTrabajo is null ||
                pbDocumento.Image is null)
                return false;

            float escalaX =
        (float)_imagenTrabajo.Width /
        pbDocumento.Image.Width;

            float escalaY =
        (float)_imagenTrabajo.Height /
        pbDocumento.Image.Height;

            Rectangle areaReal = new Rectangle(
        (int)(_rectSeleccion.X * escalaX),
        (int)(_rectSeleccion.Y * escalaY),
        (int)(_rectSeleccion.Width * escalaX),
        (int)(_rectSeleccion.Height * escalaY)
    );

            areaReal.Intersect(
                new Rectangle(
                    0,
                    0,
                    _imagenTrabajo.Width,
                    _imagenTrabajo.Height));

            if (areaReal.Width <= 0 ||
                areaReal.Height <= 0)
                return false;

            const int margen = 20;

            areaReal = Rectangle.FromLTRB(
                Math.Max(
                    0,
                    areaReal.Left - margen),

                Math.Max(
                    0,
                    areaReal.Top - margen),

                Math.Min(
                    _imagenTrabajo.Width,
                    areaReal.Right + margen),

                Math.Min(
                    _imagenTrabajo.Height,
                    areaReal.Bottom + margen)
            );

            Bitmap anterior =
        _imagenTrabajo;

            Bitmap recortada;

            try {
                recortada = anterior.Clone(
                    areaReal,
                    PixelFormat.Format24bppRgb);
            } catch {
                return false;
            }

            _imagenTrabajo =
                recortada;

            anterior.Dispose();

            return true;
        }

        private float CalcularZoomVista() {
            if (_imagenTrabajo is null ||
                pbDocumento.Parent is null)
                return 1f;

            Size disponible =
        pbDocumento.Parent.ClientSize;

            float zoomX =
        (float)(disponible.Width - 20) /
        _imagenTrabajo.Width;

            float zoomY =
        (float)(disponible.Height - 20) /
        _imagenTrabajo.Height;

            return Math.Min(
                1f,
                Math.Min(zoomX, zoomY));
        }
        private void ibRecortar_Click(object? sender, EventArgs e) {
            if (_imagenTrabajo is null || pbDocumento.Image is null) {
                Mostrar.Mensaje("Recortar","Primero debe escanear un documento.");
                return;
            }
            _modoRecorte = true;
            _seleccionando = false;
            _rectSeleccion = Rectangle.Empty;

            pbDocumento.Cursor = Cursors.Cross;
            pbDocumento.Invalidate();

            lblEstadoScanner.Text = "Seleccione el área que desea recortar";
        }
                
        private RectangleF ObtenerRectanguloImagenVisible() {
            if (pbDocumento.Image is null)
                return RectangleF.Empty;

            float escala = Math.Min((float)pbDocumento.ClientSize.Width / pbDocumento.Image.Width, (float)pbDocumento.ClientSize.Height / pbDocumento.Image.Height);
            float ancho =  pbDocumento.Image.Width * escala;
            float alto =   pbDocumento.Image.Height * escala;
            float x =      (pbDocumento.ClientSize.Width - ancho) / 2f;
            float y =      (pbDocumento.ClientSize.Height - alto) / 2f;

            return new RectangleF(x, y, ancho, alto);
        }
        #endregion

        #region Escanear
        private async void ibEscanear_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(ScannerId)) {
                Mostrar.Mensaje("Escáner", "Debe seleccionar un escáner.");
                SivevLogger.Error("El usuario no selecciono ningun escaner", SivevOrigen.Captura);
                return;
            }
            try {
                //Mostrar.Mensaje("Informacion", "Se procedera a realizar un escaner en cuanto cierre esta ventana,\npuede tomar algunos segundos sea pasiente.");
                lblEstadoScanner.Text = "Escaneando...";
                Cursor = Cursors.WaitCursor;
                await Task.Yield();
                BotonesHabilitados();
                _paginaEscaneada = EscanearPagina(ScannerId);

                AgregarPaginaEscaneada(_paginaEscaneada);

                lblEstadoScanner.Text = $"Página {_paginaActual + 1} de {_paginas.Count}";

                //MostrarPreview(_paginaEscaneada);
                lblEstadoScanner.Text = "Documento escaneado";
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al escanear", ex.Message);
                SivevLogger.Warning($"Error al escanear\n{ex.Message}", SivevOrigen.Captura);
            } finally {
                BotonesHabilitados(true);
                Cursor = Cursors.Default;
            }
        }

        private void BotonesHabilitados(bool flag = false) {
            ibEscanear.Enabled = flag;
            ibAceptar.Enabled = flag;
            ibCancelar.Enabled = flag;
            ibActualizar.Enabled = flag;
            ibRecortar.Enabled = flag;
            ibAgregar.Enabled = flag;

            ibEliminar.Enabled = flag;
            ibGuardar.Enabled = flag;


            cbScanners.Enabled = flag;
        }
        private static void EstablecerPropiedadWia(WIA.Properties propiedades, int id, object valor) {
            foreach (WIA.Property propiedad in propiedades) {
                if (propiedad.PropertyID == id) {
                    propiedad.set_Value(ref valor);
                    return;
                }
            }
        }
        private byte[] EscanearPagina(string scannerId) {
            var deviceManager = new WIA.DeviceManager();
            WIA.DeviceInfo? scannerInfo = null;

            foreach (WIA.DeviceInfo deviceInfo in deviceManager.DeviceInfos) {
                if (deviceInfo.Type != WIA.WiaDeviceType.ScannerDeviceType)
                    continue;

                if (deviceInfo.DeviceID == scannerId) {
                    scannerInfo = deviceInfo;
                    break;
                }
            }

            if (scannerInfo is null)
                throw new InvalidOperationException("El escáner seleccionado ya no está disponible.");

            WIA.Device device = scannerInfo.Connect();

            if (device.Items.Count == 0)
                throw new InvalidOperationException("El escáner no tiene elementos disponibles.");
            
            WIA.Item item = device.Items[1];
            const int WIA_IPS_XRES = 6147;
            const int WIA_IPS_YRES = 6148;

            EstablecerPropiedadWia(
                item.Properties,
                WIA_IPS_XRES,
                300);
            EstablecerPropiedadWia(
                item.Properties,
                WIA_IPS_YRES,
                300);
            // JPEG en WIA.
            const string WiaFormatJpeg = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            WIA.ImageFile image = (WIA.ImageFile)item.Transfer(WiaFormatJpeg);
            object binaryData = image.FileData.get_BinaryData();
            
            if (binaryData is not byte[] bytes || bytes.Length == 0) {
                throw new InvalidOperationException("El escáner no devolvió ninguna imagen.");
            }

            return bytes;
        }

        private void MostrarPreview(byte[] imagen) {
            using var ms =
        new MemoryStream(imagen);

            using var original =
        Image.FromStream(ms);

            pbDocumento.Image?.Dispose();
            _imagenEscaneadaOriginal?.Dispose();
            _imagenTrabajo?.Dispose();

            _imagenEscaneadaOriginal =
                new Bitmap(original);

            _imagenTrabajo =
                new Bitmap(original);

            pbDocumento.Image =
                new Bitmap(_imagenTrabajo);

            _rectSeleccion =
                Rectangle.Empty;

            _modoRecorte = false;
            _seleccionando = false;

            pbDocumento.Cursor =
                Cursors.Default;

            pbDocumento.Invalidate();
        }
        public sealed class DocumentoEscaneadoEventArgs : EventArgs {
            public byte[] Imagen { get; }
            public DocumentoEscaneadoEventArgs(byte[] imagen) {
                Imagen = imagen;
            }
        }
        #endregion
        private void RestaurarImagenOriginal() {
            if (_imagenEscaneadaOriginal is null)
                return;

            pbDocumento.Image?.Dispose();
            _imagenTrabajo?.Dispose();
            _imagenTrabajo = new Bitmap(_imagenEscaneadaOriginal);
            pbDocumento.Image = new Bitmap(_imagenTrabajo);
            _rectSeleccion = Rectangle.Empty;
            pbDocumento.Invalidate();
            lblEstadoScanner.Text = "Documento original";
        }
        private void ibAceptar_Click(
    object sender,
    EventArgs e) {
            if (_imagenTrabajo is null ||
                _paginaActual < 0) {
                Mostrar.Mensaje(
                    "Documento",
                    "No existe una página para aceptar.");

                return;
            }

            GuardarCambiosPaginaActual();
            ActualizarMiniaturas();

            lblEstadoScanner.Text =
                $"Página {_paginaActual + 1} aceptada";
        }

        private void ibCancelar_Click(object sender, EventArgs e) {
            RestaurarImagenOriginal();
        }

        private void ActualizarRectSeleccion(Point actual) {
            RectangleF visibleF = ObtenerRectanguloImagenVisible();

            if (visibleF.IsEmpty)
                return;

            Rectangle visible = Rectangle.Round(visibleF);

            int inicioX = Math.Clamp(_inicioSeleccion.X, visible.Left, visible.Right);
            int inicioY = Math.Clamp(_inicioSeleccion.Y, visible.Top,  visible.Bottom);
            int actualX = Math.Clamp(actual.X,  visible.Left, visible.Right);
            int actualY = Math.Clamp(actual.Y,  visible.Top,  visible.Bottom);

            _rectSeleccion = Rectangle.FromLTRB(
                Math.Min(inicioX, actualX),
                Math.Min(inicioY, actualY),
                Math.Max(inicioX, actualX),
                Math.Max(inicioY, actualY)
            );
        }


        #region PDF
        public string GuardarPdf() {
            if (_paginas.Count == 0)
                throw new InvalidOperationException(
                    "No existen páginas para guardar.");

            if (VerificacionId is null ||
                VerificacionId == Guid.Empty)
                throw new InvalidOperationException(
                    "No existe un identificador de verificación.");

            if (string.IsNullOrWhiteSpace(RutaBase))
                throw new InvalidOperationException(
                    "No se ha configurado la ruta para guardar documentos.");

            GuardarCambiosPaginaActual();

            string carpeta =
        ObtenerCarpetaDelDia();

            string rutaCompleta =
        Path.Combine(
            carpeta,
            $"1_{VerificacionId.Value}.pdf");

            using var documento =
        new PdfDocument();

            for (int i = 0; i < _paginas.Count; i++) {
                PaginaEscaneada paginaDocumento =
            _paginas[i];

                try {
                    var pagina =
                documento.AddPage();

                    pagina.Size =
                        PageSize.A4;

                    // Siempre convertimos a JPEG estándar
                    using var ms =
                PrepararImagenParaPdf(
                    paginaDocumento.Actual);

                    using var imagenPdf =
                XImage.FromStream(ms);

                    using var gfx =
                XGraphics.FromPdfPage(pagina);

                    double margen = 20;

                    double anchoDisponible =
                pagina.Width.Point -
                margen * 2;

                    double altoDisponible =
                pagina.Height.Point -
                margen * 2;

                    double escala =
                Math.Min(
                    anchoDisponible /
                        imagenPdf.PixelWidth,

                    altoDisponible /
                        imagenPdf.PixelHeight);

                    double ancho =
                imagenPdf.PixelWidth *
                escala;

                    double alto =
                imagenPdf.PixelHeight *
                escala;

                    double x =
                (pagina.Width.Point - ancho)
                / 2;

                    double y =
                (pagina.Height.Point - alto)
                / 2;

                    gfx.DrawImage(
                        imagenPdf,
                        x,
                        y,
                        ancho,
                        alto);
                } catch (Exception ex) {
                    throw new InvalidOperationException(
                        $"No fue posible procesar la página {i + 1}. " +
                        $"{ex.Message}",
                        ex);
                }
            }

            documento.Save(rutaCompleta);

            return rutaCompleta;
        }

        private static byte[] NormalizarImagen(byte[] datos) {
            using var entrada = new MemoryStream(datos);
            using var original = Image.FromStream(entrada);

            using var bitmap = new Bitmap(
        original.Width,
        original.Height,
        PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.Clear(Color.White);

                g.DrawImage(
                    original,
                    0,
                    0,
                    bitmap.Width,
                    bitmap.Height);
            }

            using var salida =
        new MemoryStream();

            bitmap.Save(
                salida,
                ImageFormat.Jpeg);

            return salida.ToArray();
        }
        #endregion

        private string ObtenerCarpetaDelDia() {
            if (string.IsNullOrWhiteSpace(RutaBase))
                throw new InvalidOperationException("No existe una ruta base para el escaneo.");

            DateTime fecha = DateTime.Today;
            string nombreMes = new System.Globalization.CultureInfo("es-MX").DateTimeFormat.GetMonthName(fecha.Month).ToUpperInvariant();
            string carpeta = Path.Combine(RutaBase, fecha.Year.ToString(), nombreMes, fecha.ToString("yyyy-MM-dd"));
            Directory.CreateDirectory(carpeta);

            return carpeta;
        }

        private static byte[] BitmapABytes(Bitmap imagen) {
            using var ms = new MemoryStream();
            imagen.Save(ms,ImageFormat.Jpeg);

            return ms.ToArray();
        }

        private static Bitmap BytesABitmap(byte[] datos) {
            using var ms = new MemoryStream(datos);
            using var imagen = Image.FromStream(ms);

            return new Bitmap(imagen);
        }

        private void AgregarPaginaEscaneada(byte[] imagen) {
            byte[] jpeg =
        NormalizarImagen(imagen);

            var pagina =
        new PaginaEscaneada
        {
            Original = jpeg.ToArray(),
            Actual = jpeg.ToArray()
        };

            _paginas.Add(pagina);

            _paginaActual =
                _paginas.Count - 1;

            CargarPagina(_paginaActual);

            ActualizarMiniaturas();
        }

        private void CargarPagina(int indice) {
            if (indice < 0 || indice >= _paginas.Count)
                return;

            _paginaActual = indice;

            PaginaEscaneada pagina = _paginas[indice];

            pbDocumento.Image?.Dispose();
            _imagenEscaneadaOriginal?.Dispose();
            _imagenTrabajo?.Dispose();

            _imagenEscaneadaOriginal = BytesABitmap(pagina.Original);

            _imagenTrabajo = BytesABitmap(pagina.Actual);

            pbDocumento.Image = new Bitmap(_imagenTrabajo);

            _rectSeleccion = Rectangle.Empty;
            _modoRecorte = false;
            _seleccionando = false;

            pbDocumento.Cursor = Cursors.Default;

            pbDocumento.Invalidate();

            lblEstadoScanner.Text = $"Página {indice + 1} de {_paginas.Count}";
        }

        private void GuardarCambiosPaginaActual() {
            if (_imagenTrabajo is null)
                return;
            if (_paginaActual < 0 || _paginaActual >= _paginas.Count)
                return;
            _paginas[_paginaActual].Actual = BitmapABytes(_imagenTrabajo);
        }



        private void ActualizarMiniaturas() {
            foreach (Control control in flpPaginas.Controls) {
                if (control is Panel panel) {
                    foreach (Control hijo in panel.Controls) {
                        if (hijo is PictureBox picture)
                            picture.Image?.Dispose();
                    }
                }

                control.Dispose();
            }

            flpPaginas.Controls.Clear();

            for (int i = 0; i < _paginas.Count; i++) {
                int indice = i;

                var contenedor = new Panel {
                    Width = 115,
                    Height = 100,
                    Margin = new Padding(5)
                };

                var preview = new PictureBox {
                    Width = 105,
                    Height = 72,
                    Left = 5,
                    Top = 2,

                    SizeMode = PictureBoxSizeMode.Zoom,

                    BorderStyle = indice == _paginaActual ? BorderStyle.Fixed3D : BorderStyle.FixedSingle,

                    Cursor = Cursors.Hand
                };

                preview.Image = BytesABitmap( _paginas[indice].Actual);

                string titulo = indice switch  {
                    0 => "Frente",
                    1 => "Reverso",
                    _ => $"Página {indice + 1}"
                };

                var label = new Label {
                    Text = titulo,

                    Width = 105,
                    Height = 20,

                    Left = 5,
                    Top = 76,

                    TextAlign = ContentAlignment.MiddleCenter,

                    Cursor = Cursors.Hand
                };

                preview.Click += (_, _) => {
                    CargarPagina(indice);
                    ActualizarMiniaturas();
                };

                label.Click += (_, _) => {
                    CargarPagina(indice);
                    ActualizarMiniaturas();
                };

                contenedor.Controls.Add(preview);
                contenedor.Controls.Add(label);

                flpPaginas.Controls.Add(contenedor);
            }
        }


        private void ibAgregar_Click( object? sender, EventArgs e) {
            Mostrar.Mensaje("Advertencia", "Rote o anexe el nuevo documento");
            ibEscanear.PerformClick();
        }

        private static MemoryStream PrepararImagenParaPdf(byte[] datos) {
            using var entrada = new MemoryStream(datos);
            using var original = Image.FromStream(entrada);

            using var bitmap = new Bitmap(
        original.Width,
        original.Height,
        PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.Clear(Color.White);

                g.DrawImage(
                    original,
                    0,
                    0,
                    bitmap.Width,
                    bitmap.Height);
            }

            var salida = new MemoryStream();

            bitmap.Save(
                salida,
                ImageFormat.Jpeg);

            salida.Position = 0;

            return salida;
        }

        private void ibEliminar_Click(object? sender, EventArgs e) {
            if (_paginas.Count == 0 ||
                _paginaActual < 0 ||
                _paginaActual >= _paginas.Count) {
                Mostrar.Mensaje(
                    "Eliminar",
                    "No existe una página seleccionada.");

                return;
            }

            int paginaEliminada = _paginaActual;

            _paginas.RemoveAt(_paginaActual);

            if (_paginas.Count == 0) {
                _paginaActual = -1;

                pbDocumento.Image?.Dispose();
                pbDocumento.Image = null;

                _imagenTrabajo?.Dispose();
                _imagenTrabajo = null;

                _imagenEscaneadaOriginal?.Dispose();
                _imagenEscaneadaOriginal = null;

                lblEstadoScanner.Text =
                    "No existen páginas escaneadas";

                ActualizarMiniaturas();

                return;
            }

            // Si eliminamos la última página,
            // nos movemos a la anterior.
            if (paginaEliminada >= _paginas.Count)
                _paginaActual = _paginas.Count - 1;
            else
                _paginaActual = paginaEliminada;

            CargarPagina(_paginaActual);

            ActualizarMiniaturas();

            lblEstadoScanner.Text =
                $"Página {_paginaActual + 1} de {_paginas.Count}";
        }

        private void ibGuardar_Click(
    object sender,
    EventArgs e) {
            if (_paginas.Count == 0) {
                Mostrar.Mensaje(
                    "Guardar",
                    "No existen páginas para guardar.");

                return;
            }

            // Asegurar que la página actualmente
            // visible tenga sus últimos cambios.
            GuardarCambiosPaginaActual();

            GuardarClick?.Invoke(
                this,
                EventArgs.Empty);
        }


        private void AplicarZoom() {
            if (_imagenTrabajo is null)
                return;

            int ancho = Math.Max(
        1,
        (int)(_imagenTrabajo.Width * _zoom));

            int alto = Math.Max(
        1,
        (int)(_imagenTrabajo.Height * _zoom));

            var preview = new Bitmap(
        _imagenTrabajo,
        ancho,
        alto);

            Image? anterior = pbDocumento.Image;

            pbDocumento.Image = preview;

            anterior?.Dispose();

            pbDocumento.SizeMode =
                PictureBoxSizeMode.AutoSize;

            pbDocumento.Invalidate();
        }

        


        private void ibZoomMenos_Click(
    object sender,
    EventArgs e) {
            _zoom = Math.Max(
                ZoomMinimo,
                _zoom - PasoZoom);

            AplicarZoom();

            lblEstadoScanner.Text =
                $"Zoom {(int)(_zoom * 100)}%";
        }



















    }



}
