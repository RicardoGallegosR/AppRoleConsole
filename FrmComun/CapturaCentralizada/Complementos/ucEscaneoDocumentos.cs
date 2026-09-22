using FrmComun.Utils;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Utils;
using WIA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System.Drawing.Imaging;



namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucEscaneoDocumentos : UserControl {
        public event EventHandler? EscanearClick;
        public event EventHandler? AceptarClick;
        public event EventHandler? CancelarClick;
        public event EventHandler<DocumentoEscaneadoEventArgs>?  DocumentoAceptado;
        public string? ScannerId => cbScanners.SelectedValue?.ToString();
        private byte[]? _paginaEscaneada;

        private bool _seleccionando;
        private Point _inicioSeleccion;
        private Rectangle _rectSeleccion;
        private bool _modoRecorte;
        private Bitmap? _imagenEscaneadaOriginal;
        private Bitmap? _imagenTrabajo;


        public ucEscaneoDocumentos() {
            InitializeComponent();

            ibEscanear.Click += (s, e) => EscanearClick?.Invoke(this, e);
            ibAceptar.Click += (s, e) => AceptarClick?.Invoke(this, e);
            ibCancelar.Click += (s, e) => CancelarClick?.Invoke(this, e);

            cbScanners.DropDownStyle = ComboBoxStyle.DropDownList;
            cbScanners.MaxDropDownItems = 8;

            cbScanners.IntegralHeight = false;
            cbScanners.DropDownHeight = 150;
            cbScanners.DropDownWidth = 400;


            pbDocumento.MouseDown += pbDocumento_MouseDown;
            pbDocumento.MouseMove += pbDocumento_MouseMove;
            pbDocumento.MouseUp += pbDocumento_MouseUp;
            pbDocumento.Paint += pbDocumento_Paint;

            ibRecortar.Click += ibRecortar_Click;
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
        private void pbDocumento_MouseDown(
    object? sender,
    MouseEventArgs e) {
            if (!_modoRecorte ||
                e.Button != MouseButtons.Left ||
                pbDocumento.Image is null)
                return;

            RectangleF visible =
        ObtenerRectanguloImagenVisible();

            if (!visible.Contains(e.Location))
                return;

            _seleccionando = true;
            _inicioSeleccion = e.Location;
            _rectSeleccion = Rectangle.Empty;

            pbDocumento.Capture = true;
        }
        private void pbDocumento_MouseMove(
    object? sender,
    MouseEventArgs e) {
            if (!_modoRecorte ||
                !_seleccionando)
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
        private void pbDocumento_MouseUp(object? sender, MouseEventArgs e) {
            if (!_modoRecorte || !_seleccionando)
                return;

            // MUY IMPORTANTE:
            // tomar la posición exacta donde soltó el mouse.
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
            pbDocumento.Image?.Dispose();
            pbDocumento.Image = new Bitmap(_imagenTrabajo!);
            _modoRecorte = false;
            _rectSeleccion = Rectangle.Empty;
            pbDocumento.Cursor = Cursors.Default;
            pbDocumento.Invalidate();
            pbDocumento.Refresh();
            lblEstadoScanner.Text = "Documento recortado";
            ibAceptar.Enabled = true;
        }

        private bool RecortarSeleccion() {
            if (_imagenTrabajo is null)
                return false;

            Rectangle areaReal = _rectSeleccion;

            areaReal.Intersect(
                new Rectangle(
                    0,
                    0,
                    _imagenTrabajo.Width,
                    _imagenTrabajo.Height));

            if (areaReal.Width <= 0 ||
                areaReal.Height <= 0)
                return false;

            // Margen de seguridad
            const int margen = 20;

            areaReal = Rectangle.FromLTRB(
                Math.Max(0, areaReal.Left - margen),
                Math.Max(0, areaReal.Top - margen),

                Math.Min(
                    _imagenTrabajo.Width,
                    areaReal.Right + margen),

                Math.Min(
                    _imagenTrabajo.Height,
                    areaReal.Bottom + margen)
            );

            Bitmap recortada =
        _imagenTrabajo.Clone(
            areaReal,
            _imagenTrabajo.PixelFormat);

            _imagenTrabajo.Dispose();
            _imagenTrabajo = recortada;

            return true;
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
        private Rectangle ObtenerAreaImagenEnPictureBox() {
            if (pbDocumento.Image is null)
                return Rectangle.Empty;

            float proporcionImagen = (float)pbDocumento.Image.Width / pbDocumento.Image.Height;
            float proporcionControl = (float)pbDocumento.ClientSize.Width / pbDocumento.ClientSize.Height;

            int width;
            int height;
            int x;
            int y;

            if (proporcionImagen > proporcionControl) {
                width = pbDocumento.ClientSize.Width;
                height = (int)(width / proporcionImagen);
                x = 0;
                y = (pbDocumento.ClientSize.Height - height) / 2;
            } else {
                height = pbDocumento.ClientSize.Height;
                width = (int)(height * proporcionImagen);
                y = 0;
                x = (pbDocumento.ClientSize.Width - width) / 2;
            }
            return new Rectangle(x, y, width, height);
        }
        private PointF ConvertirPictureBoxAImagen(Point punto) {
            if (_imagenTrabajo is null)
                return PointF.Empty;

            RectangleF visible =
        ObtenerRectanguloImagenVisible();

            if (visible.IsEmpty)
                return PointF.Empty;

            float x =
        (punto.X - visible.Left)
        / visible.Width
        * _imagenTrabajo.Width;

            float y =
        (punto.Y - visible.Top)
        / visible.Height
        * _imagenTrabajo.Height;

            x = Math.Clamp(
                x,
                0,
                _imagenTrabajo.Width);

            y = Math.Clamp(
                y,
                0,
                _imagenTrabajo.Height);

            return new PointF(x, y);
        }
        private RectangleF ObtenerRectanguloImagenVisible() {
            if (pbDocumento.Image is null)
                return RectangleF.Empty;

            float escala = Math.Min(
        (float)pbDocumento.ClientSize.Width /
            pbDocumento.Image.Width,

        (float)pbDocumento.ClientSize.Height /
            pbDocumento.Image.Height
    );

            float ancho =
        pbDocumento.Image.Width * escala;

            float alto =
        pbDocumento.Image.Height * escala;

            float x =
        (pbDocumento.ClientSize.Width - ancho) / 2f;

            float y =
        (pbDocumento.ClientSize.Height - alto) / 2f;

            return new RectangleF(
                x,
                y,
                ancho,
                alto);
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
                MostrarPreview(_paginaEscaneada);
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

            cbScanners.Enabled = flag;
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

            // JPEG en WIA.
            const string WiaFormatJpeg = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            WIA.ImageFile image = (WIA.ImageFile)item.Transfer(WiaFormatJpeg);
            object binaryData = image.FileData.get_BinaryData();
            if (binaryData is not byte[] bytes ||
                bytes.Length == 0) {
                throw new InvalidOperationException("El escáner no devolvió ninguna imagen.");
            }

            return bytes;
        }

        private void MostrarPreview(byte[] imagen) {
            using var ms = new MemoryStream(imagen);
            using var original = Image.FromStream(ms);

            pbDocumento.Image?.Dispose();
            _imagenEscaneadaOriginal?.Dispose();
            _imagenTrabajo?.Dispose();

            _imagenEscaneadaOriginal = new Bitmap(original);
            _imagenTrabajo = new Bitmap(original);
            pbDocumento.Image = new Bitmap(_imagenTrabajo);

            _rectSeleccion = Rectangle.Empty;
            _modoRecorte = false;
            _seleccionando = false;

            pbDocumento.Cursor = Cursors.Default;
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
        private void ibAceptar_Click(object sender, EventArgs e) {
            try {
                string ruta = GuardarPdfPrueba();

                Mostrar.Mensaje("PDF generado", $"Se guardó correctamente:\n{ruta}");
            } catch (Exception ex) {
                Mostrar.Mensaje(
                    "Error",
                    $"No se pudo generar el PDF.\n{ex.Message}");
            }
        }

        private void ibCancelar_Click(object sender, EventArgs e) {
            RestaurarImagenOriginal();
        }

        private void ActualizarRectSeleccion(Point actual) {
            RectangleF visibleF =
        ObtenerRectanguloImagenVisible();

            if (visibleF.IsEmpty)
                return;

            Rectangle visible =
        Rectangle.Round(visibleF);

            int inicioX = Math.Clamp(
        _inicioSeleccion.X,
        visible.Left,
        visible.Right);

            int inicioY = Math.Clamp(
        _inicioSeleccion.Y,
        visible.Top,
        visible.Bottom);

            int actualX = Math.Clamp(
        actual.X,
        visible.Left,
        visible.Right);

            int actualY = Math.Clamp(
        actual.Y,
        visible.Top,
        visible.Bottom);

            _rectSeleccion =
                Rectangle.FromLTRB(
                    Math.Min(inicioX, actualX),
                    Math.Min(inicioY, actualY),
                    Math.Max(inicioX, actualX),
                    Math.Max(inicioY, actualY));
        }


        #region PDF
        private string GuardarPdfPrueba() {
            if (_imagenTrabajo is null)
                throw new InvalidOperationException(
                    "No existe una imagen para guardar.");

            string carpeta = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.Desktop),
        "EscaneosPrueba");

            Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(
        carpeta,
        $"{Guid.NewGuid()}.pdf");

            using var documento = new PdfDocument();

            var pagina = documento.AddPage();
            pagina.Size = PageSize.A4;

            using var ms = new MemoryStream();

            _imagenTrabajo.Save(
                ms,
                ImageFormat.Jpeg);

            ms.Position = 0;

            using var imagenPdf =
        XImage.FromStream(ms);

            using var gfx =
        XGraphics.FromPdfPage(pagina);

            // Área útil del PDF
            double margen = 20;

            double anchoDisponible =
        pagina.Width.Point - (margen * 2);

            double altoDisponible =
        pagina.Height.Point - (margen * 2);

            // Mantener proporción de la imagen
            double escala = Math.Min(
        anchoDisponible / imagenPdf.PixelWidth,
        altoDisponible / imagenPdf.PixelHeight);

            double ancho =
        imagenPdf.PixelWidth * escala;

            double alto =
        imagenPdf.PixelHeight * escala;

            double x =
        (pagina.Width.Point - ancho) / 2;

            double y =
        (pagina.Height.Point - alto) / 2;

            gfx.DrawImage(
                imagenPdf,
                x,
                y,
                ancho,
                alto);

            documento.Save(ruta);

            return ruta;
        }
       
        #endregion




    }



}
