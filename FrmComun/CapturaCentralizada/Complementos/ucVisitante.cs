using FontAwesome.Sharp;
using FrmComun.Utils;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucVisitante : UserControl {
        public event EventHandler? Acceso;
        public string Nombre => txtNombre.Text.Trim();
        public string ApellidoPaterno => txtApellidoP.Text.Trim();
        public string ApellidoMaterno => txtApellidoM.Text.Trim();


        public ucVisitante() {
            InitializeComponent();
            txtNombre.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtNombre, @"[^A-ZÁÉÍÓÚÜÑ ]"); 
            txtApellidoP.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtApellidoP, @"[^A-ZÁÉÍÓÚÜÑ ]"); 
            txtApellidoM.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtApellidoM , @"[^A-ZÁÉÍÓÚÜÑ ]");

            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtApellidoP.CharacterCasing = CharacterCasing.Upper;
            txtApellidoM.CharacterCasing = CharacterCasing.Upper;

            txtNombre.MaxLength = 50;
            txtApellidoP.MaxLength = 50;
            txtApellidoM.MaxLength = 50;

            btnAcceso.Click += (s, e) => SolicitarAcceso();
            txtApellidoM.KeyDown += txtApellidoM_KeyDown;

        }
        public void EnfocarNombre() {
            BeginInvoke(() => {
                txtNombre.Focus();
                txtNombre.SelectAll();
            });
        }

        private void txtApellidoM_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;
            SolicitarAcceso();
        }

        private void SolicitarAcceso() {
            Acceso?.Invoke(this, EventArgs.Empty);
        }

    }
}
