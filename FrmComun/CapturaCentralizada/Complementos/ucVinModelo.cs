using FrmComun.Utils;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucVinModelo : UserControl {
        public event EventHandler? DatosCompletos;
        public event EventHandler? SeleccionVehiculo;
        public event EventHandler? ConsultarVin;


        public string Vin => txtVin.Text.Trim();
        public int Modelo => (int)nudModelo.Value;

        public ucVinModelo() {
            InitializeComponent();

            nudModelo.Minimum = 1900;
            nudModelo.Maximum = DateTime.Now.Year + 1;
            nudModelo.Value = DateTime.Now.Year;

            txtVin.CharacterCasing = CharacterCasing.Upper;
            txtVin.MaxLength = 17;

            txtVin.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtVin, @"[^A-HJ-NPR-Z0-9]");

            txtVin.TextChanged += (_, _) => ValidarDatos();
            nudModelo.ValueChanged += (_, _) => ValidarDatos();

            btnSeleccionVehiculo.Click += btnSeleccionVehiculo_Click;

            // Nuevo botón
            btnSeleccionVehiculo.Click += btnConsultarVin_Click;

            // Enter en VIN
            txtVin.KeyDown += txtVin_KeyDown;
        }
        private void btnConsultarVin_Click(object? sender, EventArgs e) {
            SolicitarConsultaVin();
        }

        private void txtVin_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            SolicitarConsultaVin();
        }

        private void SolicitarConsultaVin() {
            if (txtVin.Text.Trim().Length != 17) {
                Mostrar.Mensaje("VIN inválido","El VIN debe contener 17 caracteres.");
                txtVin.Focus();
                return;
            }
            ConsultarVin?.Invoke(this, EventArgs.Empty);
        }
        public void EstablecerModelo(int modelo) {
            if (modelo < nudModelo.Minimum ||  modelo > nudModelo.Maximum) {
                return;
            }

            nudModelo.Value = modelo;
        }
        private void ValidarDatos() {
            int modelo = (int)nudModelo.Value;

            if (txtVin.Text.Length == 17 &&
                modelo >= 1900 &&
                modelo <= DateTime.Now.Year + 1) {

                DatosCompletos?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnSeleccionVehiculo_Click(object sender, EventArgs e) =>
            SeleccionVehiculo?.Invoke(this, e);
    }
}
