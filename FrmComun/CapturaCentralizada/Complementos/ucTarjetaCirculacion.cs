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
        public bool EsPersonaFisica =>   cbTipoPersona.Checked;

        public DateTime FechaTC => dtpFTC.Value;
        public int Modelo => (int)nudModelo.Value;
        public string FolioTarjetaCirculacion => txtFolioTC.Text.Trim();
        public int TubosEscape => (int)nudTubosEscape.Value;
        public string ClaveVehicular => txtClaveVehicular.Text.Trim();
        public event EventHandler? SubmarcaSeleccionada;


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
            txtClaveVehicular.MaxLength = 7; 
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

        public void CargarSubmarcas( IEnumerable<SubmarcaDto> submarcas) {
            cbSubmarcas.DataSource = null;
            cbSubmarcas.DisplayMember = nameof(SubmarcaDto.Submarca);
            cbSubmarcas.ValueMember = nameof(SubmarcaDto.SubmarcaId);
            cbSubmarcas.DataSource =   submarcas.ToList();
            //cbSubmarcas.SelectedIndex = -1;
        }

        public void CargarCombustibles(IEnumerable<CombustibleDto> combustibleDtos) {
            cbCombustibles.DataSource = null;
            cbCombustibles.DisplayMember = nameof(CombustibleDto.Combustible);
            cbCombustibles.ValueMember = nameof(CombustibleDto.CombustibleId);
            cbCombustibles.DataSource = combustibleDtos.ToList();
            //cbCombustibles.SelectedIndex = -1;
        }
        public byte? CombustibleId => cbCombustibles.SelectedValue is null  ? null : Convert.ToByte(cbCombustibles.SelectedValue);

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



        private void txtNombre_TextChanged(object? sender,EventArgs e) {
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

        public int? SubmarcaId =>  cbSubmarcas.SelectedValue is null ? null : Convert.ToInt32(cbSubmarcas.SelectedValue);
        public string Submarca =>  cbSubmarcas.SelectedItem is SubmarcaDto submarca ? submarca.Submarca : string.Empty;

        private void btnEditar_Click(object sender, EventArgs e) =>
           Editar?.Invoke(this, e);
        private void btnGuardar_Click(object sender, EventArgs e) =>
           Guardar?.Invoke(this, e);
    }
}
