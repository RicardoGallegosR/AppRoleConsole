namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmMensajes : Form {
        private string _mensaje = string.Empty;
        private bool _Cerrar = true;


        public frmMensajes() {
            InitializeComponent();
        }

        
        public frmMensajes(string mensaje) : this() {
            Mensaje = mensaje;
        }

        public string Mensaje {
            get => _mensaje;
            set {
                _mensaje = value ?? string.Empty;
                if (txbMensaje != null) txbMensaje.Text = _mensaje;
            }
        }
        private void txbMensaje_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;
        }

        public bool Cerrar {
            get => _Cerrar;
            set {
                _Cerrar = value;
                btnCerrar.Enabled = _Cerrar;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();
    }
}