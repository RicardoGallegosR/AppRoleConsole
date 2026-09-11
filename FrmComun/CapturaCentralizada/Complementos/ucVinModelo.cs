using FrmComun.Utils;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucVinModelo : UserControl {
        public event EventHandler? DatosCompletos;
        public event EventHandler? SeleccionVehiculo;

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
