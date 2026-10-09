using FrmComun.Utils;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucVinModelo : UserControl {
        public event EventHandler? DatosCompletos;
        public event EventHandler? SeleccionVehiculo;
        public event EventHandler? ConsultarVin;


        public string Vin => txtVin.Text.Trim();
        public int Modelo => (int)nudModelo.Value;
        public bool PET => cbPET.Checked;


        private const int LongitudVin = 17;
        private const int LongitudMinVin = 7;
        private const int MaxErroresVin = 3;

       
        private int _erroresConfirmacionVin = 0;




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
            txtVinConfirmar.KeyDown += txtVinConfirmar_KeyDown;

            Load += ucVinModelo_Load;
        }
        private void btnConsultarVin_Click(object? sender, EventArgs e) {
            SolicitarConsultaVin();
        }

        private void txtVinConfirmar_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            SolicitarConsultaVin();
        }

        private void SolicitarConsultaVin() {
            int longitudVin = txtVin.TextLength;
            if (!EsLongitudVinValida(longitudVin)) {
                Mostrar.Mensaje("VIN inválido","Revisa la longitud del vin");
                txtVin.Focus();
                return;
            }

            if (txtVinConfirmar.TextLength != longitudVin ||
                !string.Equals(
                    txtVin.Text,
                    txtVinConfirmar.Text,
                    StringComparison.OrdinalIgnoreCase)) {
                Mostrar.Mensaje("VIN inválido", "La confirmación del VIN no coincide.");

                txtVinConfirmar.Focus();
                return;
            }

            ConsultarVin?.Invoke(this, EventArgs.Empty);
        }
        public void EstablecerModelo(int modelo) {
            if (modelo < nudModelo.Minimum || modelo > nudModelo.Maximum) {
                return;
            }

            nudModelo.Value = modelo;
        }
        private void ValidarDatos() {
            int modelo = (int)nudModelo.Value;
            if (txtVin.Text.Length == 17 && modelo >= 1900 &&  modelo <= DateTime.Now.Year + 1) {
                DatosCompletos?.Invoke(this, EventArgs.Empty);
            }
        }
        public void HabilitarModulo(bool flag = false) {
            pnlPrincipal.Enabled = flag;
        }

        private void btnSeleccionVehiculo_Click(object sender, EventArgs e) =>
            SeleccionVehiculo?.Invoke(this, e);


        private void ucVinModelo_Load(object sender, EventArgs e) {
            lblConfirmarVin.Visible = false;
            txtVinConfirmar.Visible = false;

            btnSeleccionVehiculo.Enabled = false;

            txtVin.MaxLength = LongitudVin;
            txtVinConfirmar.MaxLength = LongitudVin;



            /*
            // Bloquea Ctrl+C, Ctrl+V, Ctrl+X, Shift+Insert, etc.
            txtVin.ShortcutsEnabled = false;
            txtVinConfirmar.ShortcutsEnabled = false;

            // Evita copiar/pegar desde el menú del botón derecho.
            txtVin.ContextMenuStrip = new ContextMenuStrip();
            txtVinConfirmar.ContextMenuStrip = new ContextMenuStrip();
            //*/



            txtVin.PasswordChar = '\0';

            _erroresConfirmacionVin = 0;
        }

        private void txtVin_TextChanged(object? sender, EventArgs e) {
            btnSeleccionVehiculo.Enabled = false;
            int longitud = txtVin.TextLength;

            if (longitud >= LongitudMinVin) {
                txtVin.PasswordChar = '●';
                lblConfirmarVin.Visible = true;
                txtVinConfirmar.Visible = true;

                // Si cambia el VIN original, cualquier confirmación previa deja de servir.
                txtVinConfirmar.Clear();
                _erroresConfirmacionVin = 0;

                // IMPORTANTE:
                // No hacemos Focus() aquí porque puede ser un VIN de 11 o 17.
                return;
            }

            txtVin.PasswordChar = '\0';
            lblConfirmarVin.Visible = false;
            txtVinConfirmar.Visible = false;
            txtVinConfirmar.Clear();
            _erroresConfirmacionVin = 0;
        }

        private void txtVinConfirmar_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsControl(e.KeyChar))
                return;

            int longitudVin = txtVin.TextLength;
            if (!EsLongitudVinValida(longitudVin)) {
                e.Handled = true;
                return;
            }

            int posicion = txtVinConfirmar.TextLength;

            if (posicion >= longitudVin) {
                e.Handled = true;
                return;
            }

            char caracterIngresado = char.ToUpperInvariant(e.KeyChar);
            char caracterEsperado =  char.ToUpperInvariant(txtVin.Text[posicion]);

            if (caracterIngresado != caracterEsperado) {
                e.Handled = true;
                _erroresConfirmacionVin++;
                Mostrar.Mensaje("Advertencia", "Anotaste mal un dígito :(");

                if (_erroresConfirmacionVin >= MaxErroresVin) {
                    ReiniciarConfirmacionVin();
                    Mostrar.Mensaje("VIN", "El VIN fue confirmado incorrectamente tres veces.\nDebe capturarlo nuevamente.");
                }
                return;
            }

            e.KeyChar = caracterIngresado;
        }
        private static bool EsLongitudVinValida(int longitud) {
            return longitud == 7 || longitud == 11 || longitud == 17;
        }
        private void txtVinConfirmar_TextChanged(object sender, EventArgs e) {
            bool vinCorrecto =  EsLongitudVinValida(
                txtVin.TextLength) &&
                txtVinConfirmar.TextLength == txtVin.TextLength &&
                string.Equals(txtVin.Text, txtVinConfirmar.Text,  StringComparison.OrdinalIgnoreCase
            );
            btnSeleccionVehiculo.Enabled = vinCorrecto;
        }
        private void ReiniciarConfirmacionVin() {
            _erroresConfirmacionVin = 0;
            txtVinConfirmar.Clear();
            txtVin.Clear();
            txtVin.PasswordChar = '\0';
            lblConfirmarVin.Visible = false;
            txtVinConfirmar.Visible = false;
            btnSeleccionVehiculo.Enabled = false;

            txtVin.Focus();
        }

        private void txtVin_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            if (!EsLongitudVinValida(txtVin.TextLength)) {
                Mostrar.Mensaje("Error", $"Longitud invalida {txtVin.Text.Length}");
                txtVin.Focus();
                return;
            }

            IniciarConfirmacionVin();
        }
        private void IniciarConfirmacionVin() {
            _erroresConfirmacionVin = 0;

            txtVin.PasswordChar = '●';

            lblConfirmarVin.Visible = true;
            txtVinConfirmar.Visible = true;

            txtVinConfirmar.Clear();
            txtVinConfirmar.Focus();

            btnSeleccionVehiculo.Enabled = false;
        }
    }
}
