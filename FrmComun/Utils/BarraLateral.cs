using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FrmComun.Utils {

    public class BarraLateral {
        private readonly FlowLayoutPanel _menu;
        private readonly Control _contenedor;

        private readonly Dictionary<string, UserControl> _vistasAbiertas = new();
        private readonly Dictionary<string, Button> _botonesVistas = new();
        private readonly System.Windows.Forms.Timer _timerHora = new();
        private Label? _lblHora;


        private string? _vistaActiva;
        private string _versionTexto = "";
        private Label? _lblInfo;
        public string? VistaActiva => _vistaActiva;

        public BarraLateral(FlowLayoutPanel menu, Control contenedor) {
            _menu = menu;
            _contenedor = contenedor;
            ConfigurarMenu();
            _menu.SizeChanged += (_, _) => AjustarAnchoVistas();
        }

        private void ConfigurarMenu() {
            _menu.FlowDirection = FlowDirection.TopDown;
            _menu.WrapContents = false;
            _menu.AutoScroll = true;
        }
        /*
        public void CrearCabecera(string centro, Action? alHacerClick = null) {
            Panel pnlCabecera = new() {
                Width = ObtenerAnchoMenu(),
                Height = 160,
                BackColor = Color.White,
                AutoSize = false,
                Margin = new Padding(3, 3, 3, 12),
                Padding = new Padding(10)
            };
            Label lblCentro = new() {
                Dock = DockStyle.Top,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 65),
                Text = $"{centro}",
                Cursor = Cursors.Hand
            };

            _lblHora = new Label() {
                Dock = DockStyle.Top,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(45, 55, 65)
            };

            Label lblInfo = new() {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(45, 55, 65),
                Cursor = Cursors.Hand
            };



            if (alHacerClick != null) {
                lblCentro.Click += (_, _) => alHacerClick();
                _lblHora.Click += (_, _) => alHacerClick();
                lblInfo.Click += (_, _) => alHacerClick();
            }

            try {
                string exe = Application.ExecutablePath;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(exe);
                _versionTexto = $"v{info.FileVersion ?? Application.ProductVersion}";
            } catch {
                _versionTexto = "vDESCONOCIDA";
            }
            ActualizarHora();

            _timerHora.Interval = 1000;
            _timerHora.Tick += (_, _) => ActualizarHora();
            _timerHora.Start();

            lblInfo.Text =
                $"{Environment.UserName}\r\n" +
                $"{Environment.MachineName}\r\n" +
                $"{Environment.UserDomainName}\r\n" +
                //$"{centro}\r\n" +
                $"{_versionTexto}";


            pnlCabecera.Controls.Add(lblInfo);
            pnlCabecera.Controls.Add(_lblHora);
            pnlCabecera.Controls.Add(lblCentro);
            _menu.Controls.Add(pnlCabecera);
        }

        */

        public void CrearCabecera(string centro,string _lblTitulo, Action? alHacerClick = null) {

            FlowLayoutPanel pnlCabecera = new() {
                Width = ObtenerAnchoMenu(),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = _menu.BackColor,
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(16, 14, 16, 12)
            };

            pnlCabecera.Paint += (_, e) => {
                using Pen pen = new(Color.White, 1);

                e.Graphics.DrawRectangle(
                    pen,
                    0,
                    0,
                    pnlCabecera.ClientSize.Width - 1,
                    pnlCabecera.ClientSize.Height - 1
                );
            };
            Label lblTitulo = new() {
                AutoSize = true,
                Text = _lblTitulo,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 6)
            };

            Label lblCentro = new() {
                AutoSize = true,
                Text = $"Centro: {centro}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 4)
            };

            _lblHora = new Label() {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            Label lblUsuario = new() {
                AutoSize = true,
                Text = Environment.UserName,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 1)
            };

            Label lblEquipo = new() {
                AutoSize = true,
                Text = $"{Environment.MachineName} · {Environment.UserDomainName}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor =Color.White,
                Margin = new Padding(0, 0, 0, 1)
            };
          
            try {
                string exe = Application.ExecutablePath;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(exe);

                _versionTexto =
                    $"v{info.FileVersion ?? Application.ProductVersion}";
            } catch {
                _versionTexto = "vDESCONOCIDA";
            }

            Label lblVersion = new() {
                AutoSize = true,
                Text = _versionTexto,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 0)
            };

            pnlCabecera.Controls.Add(lblTitulo);
            pnlCabecera.Controls.Add(lblCentro);
            pnlCabecera.Controls.Add(_lblHora);
            pnlCabecera.Controls.Add(lblUsuario);
            pnlCabecera.Controls.Add(lblEquipo);
            pnlCabecera.Controls.Add(lblVersion);

            if (alHacerClick != null) {

                Control[] controles = {
                    pnlCabecera,
                    lblTitulo,
                    lblCentro,
                    _lblHora,
                    lblUsuario,
                    lblEquipo,
                    lblVersion
                };

                foreach (Control control in controles) {
                    control.Cursor = Cursors.Hand;
                    control.Click += (_, _) => alHacerClick();
                }
            }

            _menu.Controls.Add(pnlCabecera);

            ActualizarHora();

            _timerHora.Interval = 1000;
            _timerHora.Tick += (_, _) => ActualizarHora();
            _timerHora.Start();
        }



        private void ActualizarHora() {
            if (_lblHora != null)
                _lblHora.Text = $"{DateTime.Now:HH:mm:ss}";
        }
        public void MostrarVista(string clave, string titulo, Func<UserControl> crearControl, bool mostrarEnMenu = true) {
            
            if (!_vistasAbiertas.TryGetValue(clave,out UserControl? control)) {
                control = crearControl();
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                _contenedor.Controls.Add(control);
                _vistasAbiertas.Add(clave, control);
                if (mostrarEnMenu) {
                    CrearBotonVista(clave, titulo);
                }
            }
            ActivarVista(clave);
        }

        private void CrearBotonVista(string clave, string titulo) {
            Button boton = new() {
                Name = $"btnVista_{clave}",
                Text = titulo,
                Width = ObtenerAnchoMenu(),
                Height = 42,
                FlatStyle = FlatStyle.Flat,


                BackColor = Color.White,
                ForeColor = Color.FromArgb(45, 55, 65),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12F),
                Cursor = Cursors.Hand,
                Margin = new Padding(3)
            };
            boton.FlatAppearance.BorderSize = 0;
            boton.Click += (_, _) => ActivarVista(clave);
            _menu.Controls.Add(boton);
            _botonesVistas.Add(clave, boton);
        }

        public void ActivarVista(string clave) {
            if (!_vistasAbiertas.TryGetValue(clave, out UserControl? control)) {
                return;
            }
            _vistaActiva = clave;

            // Ocultar todas las vistas
            foreach (UserControl vista in _vistasAbiertas.Values) {
                vista.Visible = false;
            }

            // Mostrar la seleccionada
            control.Visible = true;
            control.BringToFront();

            // Actualizar aspecto de botones
            foreach (var item in _botonesVistas) {

                bool seleccionado = item.Key == clave;

                if (seleccionado) {
                    item.Value.BackColor = Color.White;
                    item.Value.ForeColor = Color.Crimson;
                    item.Value.Font = new Font("Segoe UI", 14F,  FontStyle.Bold);
                    item.Value.FlatAppearance.MouseOverBackColor = Color.White;

                } else {

                    item.Value.BackColor = Color.Crimson;
                    item.Value.ForeColor = Color.White;
                    item.Value.Font = new Font("Segoe UI", 14F, FontStyle.Regular );
                    item.Value.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 35, 70);
                }
            }
        }

        private void AjustarAnchoVistas() {
            int ancho = ObtenerAnchoMenu();
            foreach (Control control in _menu.Controls) {
                control.Width = ancho;
            }
        }
        public void ActualizarVersionYHora() {
            if (_lblInfo == null)
                return;

            _lblInfo.Text =
                $"Equipo: {Environment.MachineName}\r\n" +
                $"Usuario: {Environment.UserName}\r\n" +
                $"Dominio: {Environment.UserDomainName}\r\n" +
                $"Versión: {_versionTexto}\r\n" +
                $"Fecha: {DateTime.Now:dd/MM/yyyy}\r\n" +
                $"Hora: {DateTime.Now:HH:mm:ss}";
        }
        private int ObtenerAnchoMenu() {
            return Math.Max(0, _menu.ClientSize.Width - _menu.Padding.Horizontal- 8);
        }

        public void EliminarVista(string clave) {

            // Eliminar UserControl
            if (_vistasAbiertas.TryGetValue(clave, out UserControl? vista)) {

                _contenedor.Controls.Remove(vista);
                _vistasAbiertas.Remove(clave);

                vista.Dispose();
            }

            // Eliminar botón de la barra lateral
            if (_botonesVistas.TryGetValue(clave, out Button? boton)) {

                _menu.Controls.Remove(boton);
                _botonesVistas.Remove(clave);

                boton.Dispose();
            }

            // Si era la vista activa
            if (_vistaActiva == clave) {
                _vistaActiva = null;
            }

            AjustarAnchoVistas();
        }
    }
}