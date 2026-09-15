using FrmComun.Utils;
using SQLSIVEV.Domain.Models;


namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucTarjetaCirculacion : UserControl {
        public event EventHandler? Editar;
        public event EventHandler? Guardar;
        public event EventHandler? FiltroSubmarcaChanged;


        public string Propietario => txtPropietario.Text.Trim();
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

            txtPropietario.CharacterCasing = CharacterCasing.Upper;


            btnEditar.Click += btnEditar_Click;
            btnGuardar.Click += btnGuardar_Click;

            txtClaveVehicular.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtClaveVehicular, @"[^0-9]");
            txtPropietario.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtPropietario, @"[^A-ZÁÉÍÓÚÜÑ ]");
            txtFolioTC.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtFolioTC, @"[^0-9]");


            txtPropietario.MaxLength = 50;

            txtFolioTC.MaxLength = 12;
            txtClaveVehicular.MaxLength = 7; 


            nudTubosEscape.ValueChanged += (s, ev) => {
                if (nudTubosEscape.Value < 0) nudTubosEscape.Value = 0;
            };
            nudTubosEscape.Minimum = 1;
            nudTubosEscape.Value = 1;

            cbMarcas.SelectionChangeCommitted += (s, e) => FiltroSubmarcaChanged?.Invoke(this, EventArgs.Empty);


            nudModelo.ValueChanged += (s, e) => FiltroSubmarcaChanged?.Invoke(this, EventArgs.Empty);


            cbSubmarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSubmarcas.SelectionChangeCommitted += (s, e) => ConfirmarSubmarca();
            cbSubmarcas.KeyDown += cbSubmarcas_KeyDown;
        }
        public void SeleccionarMarca(int marcaId) {
            if (cbMarcas.DataSource is null)
                return;
            cbMarcas.SelectedValue = marcaId;
        }
        public void CargarMarcas(IEnumerable<MarcasDto> marcas) {
            cbMarcas.DataSource = null;
            cbMarcas.DisplayMember = nameof(MarcasDto.Marca);
            cbMarcas.ValueMember = nameof(MarcasDto.MarcaId);
            cbMarcas.DataSource = marcas.ToList();
            //cbMarcas.SelectedIndex = -1;
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
        public int? CombustibleId => cbCombustibles.SelectedValue is null  ? null : Convert.ToInt32(cbCombustibles.SelectedValue);

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








        public int? SubmarcaId =>  cbSubmarcas.SelectedValue is null ? null : Convert.ToInt32(cbSubmarcas.SelectedValue);
        public string Submarca =>  cbSubmarcas.SelectedItem is SubmarcaDto submarca ? submarca.Submarca : string.Empty;

        private void btnEditar_Click(object sender, EventArgs e) =>
           Editar?.Invoke(this, e);
        private void btnGuardar_Click(object sender, EventArgs e) =>
           Guardar?.Invoke(this, e);
    }
}
