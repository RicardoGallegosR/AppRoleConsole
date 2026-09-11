using FrmComun.Utils;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucTarjetaCirculacion : UserControl {
        public event EventHandler? Editar;
        public event EventHandler? Guardar;

        public string Propietario => txtPropietario.Text.Trim();
        public string Marca => txtMarca.Text.Trim();
        public string Submarca => txtSubMarca.Text.Trim();
        public string Combustible => txtCombustible.Text.Trim();
        public DateTime FechaTC => dtpFTC.Value;
        public int Modelo => (int)nudModelo.Value;
        public string FolioTarjetaCirculacion => txtFolioTC.Text.Trim();
        public int TubosEscape => (int)nudTubosEscape.Value;
        public string ClaveVehicular => txtClaveVehicular.Text.Trim();



        public ucTarjetaCirculacion() {
            InitializeComponent();
            nudModelo.Minimum = 1900;
            nudModelo.Maximum = DateTime.Now.Year + 1;
            nudModelo.Value = DateTime.Now.Year;

            dtpFTC.Format = DateTimePickerFormat.Custom;
            dtpFTC.CustomFormat = "yyyy/MM/dd";

            txtPropietario.CharacterCasing = CharacterCasing.Upper;
            txtMarca.CharacterCasing = CharacterCasing.Upper;
            txtSubMarca.CharacterCasing = CharacterCasing.Upper;
            txtCombustible.CharacterCasing = CharacterCasing.Upper;


            btnEditar.Click += btnEditar_Click;
            btnGuardar.Click += btnGuardar_Click;

            txtClaveVehicular.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtClaveVehicular, @"[^0-9]");
            txtCombustible.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtCombustible, @"[^A-Z]");
            txtMarca.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtMarca, @"[^A-Z0-9ÁÉÍÓÚÜÑ .\-_]");
            txtSubMarca.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtSubMarca, @"[^A-Z0-9ÁÉÍÓÚÜÑ .\-_]");
            txtPropietario.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtPropietario, @"[^A-ZÁÉÍÓÚÜÑ ]");
            txtFolioTC.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtFolioTC, @"[^0-9]");


            txtPropietario.MaxLength = 50;
            txtMarca.MaxLength = 30;
            txtSubMarca.MaxLength = 40;
            txtCombustible.MaxLength = 25;
            txtFolioTC.MaxLength = 12;
            txtClaveVehicular.MaxLength = 7; 


            nudTubosEscape.ValueChanged += (s, ev) => {
                if (nudTubosEscape.Value < 0) nudTubosEscape.Value = 0;
            };


        }

        private void btnEditar_Click(object sender, EventArgs e) =>
           Editar?.Invoke(this, e);
        private void btnGuardar_Click(object sender, EventArgs e) =>
           Guardar?.Invoke(this, e);
    }
}
