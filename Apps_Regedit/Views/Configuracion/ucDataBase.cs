using FrmComun.Utils;
using SQLSIVEV.Comun;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Apps_Regedit.Views.Configuracion {
    public partial class ucDataBase : UserControl {

        #region Propiedades de configuración
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Server {
            get => txtServer.Text;
            set => txtServer.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Database {
            get => txtDatabase.Text;
            set => txtDatabase.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string User {
            get => txtUser.Text;
            set => txtUser.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AppName {
            get => txtAppName.Text;
            set => txtAppName.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AppRole {
            get => txtAppRole.Text;
            set => txtAppRole.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AppRolePassword {
            get => txtAppRolePassword.Text;
            set => txtAppRolePassword.Text = value;
        }
        public bool SoloLectura {
            get => txtServer.ReadOnly;
            set {
                txtServer.ReadOnly = value;
                txtDatabase.ReadOnly = value;
                txtUser.ReadOnly = value;
                txtPassword.ReadOnly = value;
                txtAppName.ReadOnly = value;
                txtAppRole.ReadOnly = value;
                txtAppRolePassword.ReadOnly = value;
            }
        }
        #endregion

        #region Constructor
        public ucDataBase() {
            InitializeComponent();
            txtServer.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtServer, @"[^A-Za-z0-9.\-\\,]");
            txtDatabase.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtDatabase, @"[^A-Za-z0-9_\-]");
            txtUser.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtUser, @"[^A-Za-z0-9]");
            txtPassword.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtPassword, @"[^A-Za-z0-9-]");
            txtAppName.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtAppName, @"[^A-Za-z]");
            txtAppRole.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtAppRole, @"[^A-Za-z]");
            txtAppRolePassword.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtAppRolePassword, @"[^A-Fa-f0-9-]");

            txtPassword.MaxLength = 36;
            txtAppRolePassword.MaxLength = 36;

            txtServer.MaxLength = 64;
            txtDatabase.MaxLength = 20;
            txtUser.MaxLength = 20;
            txtAppName.MaxLength = 25;
            txtAppRole.MaxLength = 30;

            txtPassword.UseSystemPasswordChar = true;
            txtAppRolePassword.UseSystemPasswordChar = true;
            
        }

        private void ucDataBase_Load(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtServer.Text)) {
                txtServer.Text = Red.ObtenerIP192ServerPrincipal();
            }
        }
        #endregion
    }
}
