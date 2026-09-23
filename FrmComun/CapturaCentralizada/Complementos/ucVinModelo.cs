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
            txtVin.KeyDown += txtVin_KeyDown;

            Load += ucVinModelo_Load;
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
                Mostrar.Mensaje("VIN inválido", "El VIN debe contener 17 caracteres.");
                txtVin.Focus();
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

            if (txtVin.Text.Length == 17 &&
                modelo >= 1900 &&
                modelo <= DateTime.Now.Year + 1) {

                DatosCompletos?.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnSeleccionVehiculo_Click(object sender, EventArgs e) =>
            SeleccionVehiculo?.Invoke(this, e);


        private void ucVinModelo_Load(object sender, EventArgs e) {
            lblConfirmarVin.Visible = false;
            txtVinConfirmar.Visible = false;

            btnSeleccionVehiculo.Enabled = false;

            txtVin.MaxLength = LongitudVin;
            txtVinConfirmar.MaxLength = LongitudVin;

            // Bloquea Ctrl+C, Ctrl+V, Ctrl+X, Shift+Insert, etc.
            txtVin.ShortcutsEnabled = false;
            txtVinConfirmar.ShortcutsEnabled = false;

            // Evita copiar/pegar desde el menú del botón derecho.
            txtVin.ContextMenuStrip = new ContextMenuStrip();
            txtVinConfirmar.ContextMenuStrip = new ContextMenuStrip();

            txtVin.PasswordChar = '\0';

            _erroresConfirmacionVin = 0;
        }

        private void txtVin_TextChanged(object? sender, EventArgs e) {
            btnSeleccionVehiculo.Enabled = false;

            if (txtVin.Text.Length == LongitudVin) {

                txtVin.PasswordChar = '●';

                lblConfirmarVin.Visible = true;
                txtVinConfirmar.Visible = true;
                txtVinConfirmar.Clear();

                _erroresConfirmacionVin = 0;
                txtVinConfirmar.Focus();

                return;
            }

            txtVin.PasswordChar = '\0';
            lblConfirmarVin.Visible = false;
            txtVinConfirmar.Visible = false;
            txtVinConfirmar.Clear();
            _erroresConfirmacionVin = 0;
        }

        private void txtVinConfirmar_KeyPress(object sender, KeyPressEventArgs e) {
            // Permitir Backspace, Delete, etc.
            if (char.IsControl(e.KeyChar))
                return;

            if (txtVin.Text.Length != LongitudVin) {
                e.Handled = true;
                return;
            }

            int posicion = txtVinConfirmar.TextLength;

            if (posicion >= LongitudVin) {
                e.Handled = true;
                return;
            }
            char caracterIngresado = char.ToUpperInvariant(e.KeyChar);
            char caracterEsperado = char.ToUpperInvariant(txtVin.Text[posicion]);
            if (caracterIngresado != caracterEsperado) {
                // No dejamos que el carácter incorrecto entre.
                e.Handled = true;

                _erroresConfirmacionVin++;

                if (_erroresConfirmacionVin >= MaxErroresVin) {
                    ReiniciarConfirmacionVin();
                    Mostrar.Mensaje("VIN", "El VIN fue confirmado incorrectamente tres veces.\nDebe capturarlo nuevamente.");
                    return;
                }
                return;
            }
            e.KeyChar = caracterIngresado;
        }

        private void txtVinConfirmar_TextChanged(object sender, EventArgs e) {
            bool vinCorrecto =  txtVin.Text.Length == LongitudVin &&  txtVinConfirmar.Text.Length == LongitudVin && string.Equals(txtVin.Text, txtVinConfirmar.Text, StringComparison.OrdinalIgnoreCase);
            btnSeleccionVehiculo.Enabled =  vinCorrecto;
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
    }
}
