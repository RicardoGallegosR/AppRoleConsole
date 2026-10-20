using FrmComun.Utils;
using SQLSIVEV.Domain.Models;


namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucTarjetaCirculacion : UserControl {
        public event EventHandler? Editar;
        public event EventHandler? Guardar;
        public event EventHandler? FiltroSubmarcaChanged;
        private bool _editando;


        public string Nombre => txtNombre.Text.Trim();
        public string ApellidoPaterno => txtApellidoPaterno.Text.Trim();
        public string ApellidoMaterno => txtApellidoMaterno.Text.Trim();
        public bool EsPersonaFisica => cbTipoPersona.Checked;

        public DateTime FechaTC => dtpFTC.Value;
        public int Modelo => (int)nudModelo.Value;
        public string FolioTarjetaCirculacion => txtFolioTC.Text.Trim();
        public int TubosEscape => (int)nudTubosEscape.Value;
        public int? ClaveVehicular => int.TryParse(txtClaveVehicular.Text.Trim(), out int clave) ? clave: 0; 
        public event EventHandler? SubmarcaSeleccionada;

        private const int LongitudMinClaveVehicular = 3;
        private const int LongitudClaveVehicular = 7;
        private const int MaxErroresClaveVehicular = 3;
        private int _erroresConfirmacionClaveVehicular = 0;





        public ucTarjetaCirculacion() {
            InitializeComponent();
            nudModelo.Minimum = 1900;
            nudModelo.Maximum = DateTime.Now.Year + 1;
            nudModelo.Value = DateTime.Now.Year;

            dtpFTC.Format = DateTimePickerFormat.Custom;
            dtpFTC.CustomFormat = "yyyy/MM/dd";
            dtpFTC.MinDate = new DateTime(1900, 1, 1);
            dtpFTC.MaxDate = DateTime.Today;
            dtpFTC.Value = DateTime.Today;


            txtNombre.CharacterCasing = CharacterCasing.Upper;


            btnEditar.Click += btnEditar_Click;
            btnGuardar.Click += btnGuardar_Click;

            txtClaveVehicular.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtClaveVehicular, @"[^0-9]");
            txtConfirmarClaveVehicular.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtConfirmarClaveVehicular, @"[^0-9]");

            txtNombre.TextChanged += txtNombre_TextChanged;
            txtFolioTC.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtFolioTC, @"[^0-9]");

            txtApellidoPaterno.CharacterCasing = CharacterCasing.Upper;
            txtApellidoMaterno.CharacterCasing = CharacterCasing.Upper;

            txtApellidoPaterno.MaxLength = 50;
            txtApellidoMaterno.MaxLength = 50;

            txtApellidoPaterno.TextChanged += (s, e) => Expresiones.SanitizeByRegex(txtApellidoPaterno, @"[^A-ZÁÉÍÓÚÜÑ ]");
            txtApellidoMaterno.TextChanged += (s, e) => Expresiones.SanitizeByRegex(txtApellidoMaterno, @"[^A-ZÁÉÍÓÚÜÑ ]");

            txtNombre.MaxLength = 50;
            txtFolioTC.MaxLength = 12;

            nudTubosEscape.Minimum = 1;
            nudTubosEscape.Value = 1;

            cbMarcas.SelectionChangeCommitted += (s, e) => FiltroSubmarcaChanged?.Invoke(this, EventArgs.Empty);

            nudModelo.ValueChanged += (s, e) => FiltroSubmarcaChanged?.Invoke(this, EventArgs.Empty);


            cbSubmarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSubmarcas.SelectionChangeCommitted += (s, e) => ConfirmarSubmarca();
            cbSubmarcas.KeyDown += cbSubmarcas_KeyDown;

            cbTipoPersona.CheckedChanged += chkPersonaFisica_CheckedChanged;
            cbTipoPersona.Checked = true;
            ConfigurarTipoPersona();
            EstablecerModoEdicion(false);


            Load += uucTarjetaCirculacion_Load;
        }

        private void uucTarjetaCirculacion_Load(object sender, EventArgs e) {
            lblConfirmarCV.Visible = false;
            txtConfirmarClaveVehicular.Visible = false;

            btnGuardar.Enabled = false;

            txtClaveVehicular.MaxLength = LongitudClaveVehicular;
            txtConfirmarClaveVehicular.MaxLength = LongitudClaveVehicular;

            //*
            // Bloquea Ctrl+C, Ctrl+V, Ctrl+X, Shift+Insert, etc.
            txtClaveVehicular.ShortcutsEnabled = false;
            txtConfirmarClaveVehicular.ShortcutsEnabled = false;

            // Evita copiar/pegar desde el menú del botón derecho.
            txtClaveVehicular.ContextMenuStrip = new ContextMenuStrip();
            txtConfirmarClaveVehicular.ContextMenuStrip = new ContextMenuStrip();
            //*/

            txtClaveVehicular.PasswordChar = '\0';
            _erroresConfirmacionClaveVehicular = 0;
        }




        public void SeleccionarMarca(int marcaId) {
            if (cbMarcas.DataSource is null)
                return;
            cbMarcas.SelectedValue = marcaId;
        }
        public void SeleccionarSubMarca(int submarcaId) {
            if (cbSubmarcas.DataSource is null)
                return;
            cbSubmarcas.SelectedValue = submarcaId;
        }
        public void SeleccionarCombustible(byte combustibleId) {
            if (cbCombustibles.DataSource is null)
                return;
            cbCombustibles.SelectedValue = combustibleId;
        }

        public void CargarTarjetaForlio(string TCFolio) {
            txtFolioTC.Text = TCFolio;
        }

        public void CargarFechaTC(DateTime fecha) {
            if (fecha < dtpFTC.MinDate || fecha > dtpFTC.MaxDate)
                return;
            dtpFTC.Value = fecha.Date;
        }

        private void chkPersonaFisica_CheckedChanged(object? sender, EventArgs e) {
            ConfigurarTipoPersona();
        }

        private void ConfigurarTipoPersona() {
            bool personaFisica = EsPersonaFisica;

            lblNombre.Text = personaFisica ? "NOMBRE" : "RAZÓN SOCIAL";
            cbTipoPersona.Text = personaFisica ? "PERSONA FÍSICA" : "PERSONA MORAL";

            lblApellidoPaterno.Visible = personaFisica;
            txtApellidoPaterno.Visible = personaFisica;

            lblApellidoMaterno.Visible = personaFisica;
            txtApellidoMaterno.Visible = personaFisica;
        }
        public void CargarMarcas(IEnumerable<MarcasDto> marcas) {
            cbMarcas.DataSource = null;
            cbMarcas.DisplayMember = nameof(MarcasDto.Marca);
            cbMarcas.ValueMember = nameof(MarcasDto.MarcaId);
            cbMarcas.DataSource = marcas.ToList();
            //cbMarcas.SelectedIndex = -1;
        }




        public void EstablecerPersonaFisica(string nombre, string apellidoPaterno, string apellidoMaterno) {
            cbTipoPersona.Checked = true;
            txtNombre.Text = nombre;
            txtApellidoPaterno.Text = apellidoPaterno;
            txtApellidoMaterno.Text = apellidoMaterno;
        }
        public void EstablecerPersonaMoral(string razonSocial) {
            cbTipoPersona.Checked = false;
            txtNombre.Text = razonSocial;
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
        }
        public int? MarcaId => cbMarcas.SelectedValue is int id ? id : null;
        public string Marca => cbMarcas.SelectedItem is MarcasDto marca ? marca.Marca : string.Empty;

        public void CargarSubmarcas(IEnumerable<SubmarcaDto> submarcas) {
            cbSubmarcas.DataSource = null;
            cbSubmarcas.DisplayMember = nameof(SubmarcaDto.Submarca);
            cbSubmarcas.ValueMember = nameof(SubmarcaDto.SubmarcaId);
            cbSubmarcas.DataSource = submarcas.ToList();
            //cbSubmarcas.SelectedIndex = -1;
        }

        public void CargarCombustibles(IEnumerable<CombustibleDto> combustibleDtos) {
            cbCombustibles.DataSource = null;
            cbCombustibles.DisplayMember = nameof(CombustibleDto.Combustible);
            cbCombustibles.ValueMember = nameof(CombustibleDto.CombustibleId);
            cbCombustibles.DataSource = combustibleDtos.ToList();
            //cbCombustibles.SelectedIndex = -1;
        }
        public byte? CombustibleId => cbCombustibles.SelectedValue is null ? null : Convert.ToByte(cbCombustibles.SelectedValue);

        private void cbSubmarcas_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;
            ConfirmarSubmarca();
        }
        private void ConfirmarSubmarca() {
            if (cbSubmarcas.SelectedIndex < 0)
                return;
            SubmarcaSeleccionada?.Invoke(this, EventArgs.Empty);
        }

        public void EstablecerModelo(int modelo) {
            if (modelo < nudModelo.Minimum || modelo > nudModelo.Maximum) {
                return;
            }
            nudModelo.Value = modelo;
        }



        private void txtNombre_TextChanged(object? sender, EventArgs e) {
            if (EsPersonaFisica) {
                Expresiones.SanitizeByRegex(txtNombre, @"[^A-ZÁÉÍÓÚÜÑ ]");
            } else {
                Expresiones.SanitizeByRegex(txtNombre, @"[^A-ZÁÉÍÓÚÜÑ0-9 .,&'()/-]");
            }
        }
        public void EnfocarNombre() {
            BeginInvoke(() => {
                txtNombre.Focus();
                txtNombre.SelectAll();
            });
        }
        public void EstablecerModoEdicion(bool editar) {
            _editando = editar;

            cbTipoPersona.Enabled = editar;

            txtNombre.Enabled = editar;
            txtApellidoPaterno.Enabled = editar;
            txtApellidoMaterno.Enabled = editar;

            cbMarcas.Enabled = editar;
            cbSubmarcas.Enabled = editar;
            cbCombustibles.Enabled = editar;

            nudModelo.Enabled = editar;
            nudTubosEscape.Enabled = editar;

            dtpFTC.Enabled = editar;
            txtFolioTC.Enabled = editar;
            btnEditar.Enabled = !editar;
            //btnGuardar.Enabled = editar;
        }

        public void EsperarStore(bool flag = false) {
            pnlPrincipal.Enabled = flag;
        }

        public int? SubmarcaId => cbSubmarcas.SelectedValue is null ? null : Convert.ToInt32(cbSubmarcas.SelectedValue);
        public string Submarca => cbSubmarcas.SelectedItem is SubmarcaDto submarca ? submarca.Submarca : string.Empty;

        private void btnEditar_Click(object sender, EventArgs e) => Editar?.Invoke(this, e);
        private void btnGuardar_Click(object sender, EventArgs e) => Guardar?.Invoke(this, e);

        private void txtClaveVehicular_TextChanged(object sender, EventArgs e) {
            btnGuardar.Enabled = false;

            if (txtClaveVehicular.Text.Length <= LongitudClaveVehicular && txtClaveVehicular.Text.Length >= LongitudMinClaveVehicular) {
                txtClaveVehicular.PasswordChar = '●';
                lblConfirmarCV.Visible = true;
                txtConfirmarClaveVehicular.Visible = true;
                txtConfirmarClaveVehicular.Clear();

                _erroresConfirmacionClaveVehicular = 0;
                //txtConfirmarClaveVehicular.Focus();

                return;
            }

            txtClaveVehicular.PasswordChar = '\0';
            lblConfirmarCV.Visible = false;
            txtConfirmarClaveVehicular.Visible = false;
            txtConfirmarClaveVehicular.Clear();
            _erroresConfirmacionClaveVehicular = 0;
        }

        private void txtConfirmarClaveVehicular_KeyPress(object sender, KeyPressEventArgs e) {

            // Permitir Backspace y otras teclas de control.
            if (char.IsControl(e.KeyChar))
                return;

            string claveOriginal = txtClaveVehicular.Text;

            // La clave original debe estar dentro del rango permitido.
            if (claveOriginal.Length < LongitudMinClaveVehicular || claveOriginal.Length > LongitudClaveVehicular) {
                e.Handled = true;
                return;
            }

            int posicion = txtConfirmarClaveVehicular.TextLength;

            // No permitir escribir más caracteres que la clave original.
            if (posicion >= claveOriginal.Length) {
                e.Handled = true;
                return;
            }

            char caracterIngresado = char.ToUpperInvariant(e.KeyChar);
            char caracterEsperado = char.ToUpperInvariant(claveOriginal[posicion]);

            if (caracterIngresado != caracterEsperado) {
                e.Handled = true;
                _erroresConfirmacionClaveVehicular++;
                Mostrar.Mensaje("Advertencia", "Anotaste mal un digito :(");
                if (_erroresConfirmacionClaveVehicular >= MaxErroresClaveVehicular) {
                    ReiniciarConfirmacionClaveVehicular();
                    Mostrar.Mensaje("Clave vehicular", "La clave vehicular fue confirmada incorrectamente tres veces.\nDebe capturarla nuevamente.");
                    return;
                }
                return;
            }
            e.KeyChar = caracterIngresado;
        }

        private void ReiniciarConfirmacionClaveVehicular() {
            _erroresConfirmacionClaveVehicular = 0;
            txtConfirmarClaveVehicular.Clear();
            txtClaveVehicular.Clear();
            txtClaveVehicular.PasswordChar = '\0';
            lblConfirmarCV.Visible = false;
            txtConfirmarClaveVehicular.Visible = false;
            btnGuardar.Enabled = false;

            txtClaveVehicular.Focus();
        }

        private void txtConfirmarClaveVehicular_TextChanged(object sender, EventArgs e) {
            btnGuardar.Enabled = txtClaveVehicular.Text.Length >= LongitudMinClaveVehicular && txtClaveVehicular.Text.Length <= LongitudClaveVehicular && txtClaveVehicular.Text.Equals(txtConfirmarClaveVehicular.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void txtConfirmarClaveVehicular_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            if (!btnGuardar.Enabled)
                return;

            btnGuardar.PerformClick();
        }

    }
}
