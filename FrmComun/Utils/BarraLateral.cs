using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FrmComun.Utils {

    public class BarraLateral {
        private readonly FlowLayoutPanel _menu;
        private readonly Control _contenedor;

        private readonly Dictionary<string, UserControl> _vistasAbiertas = new();
        private readonly Dictionary<string, Button> _botonesVistas = new();

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

        public void CrearCabecera(Action? alHacerClick = null) {
            Panel pnlCabecera = new() {
                Width = ObtenerAnchoMenu(),
                Height = 150,
                BackColor = Color.White,
                AutoSize = false,
                Margin = new Padding(3, 3, 3, 12),
                Padding = new Padding(10)
            };

            Label lblInfo = new() {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(45, 55, 65),
                Cursor = Cursors.Hand
            };

            if (alHacerClick != null) {
                lblInfo.Click += (_, _) => alHacerClick();
            }

            try {
                string exe = Application.ExecutablePath;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(exe);
                _versionTexto = $"v{info.FileVersion ?? Application.ProductVersion}";
            } catch {
                _versionTexto = "vDESCONOCIDA";
            }

            lblInfo.Text =
                $"Equipo: {Environment.MachineName}\r\n" +
                $"Usuario: {Environment.UserName}\r\n" +
                $"Dominio: {Environment.UserDomainName}\r\n" +
                $"Versión: {_versionTexto}";
            pnlCabecera.Controls.Add(lblInfo);
            _menu.Controls.Add(pnlCabecera);
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
    }
}