using FrmComun.Utils;
using SQLSIVEV.Comun;
using System.ComponentModel;

namespace Apps_Regedit.Views.Configuracion {
    public partial class ucEstacion : UserControl {

        #region Propiedades de configuración
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IP {
            get => txtIp.Text;
            set => txtIp.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OpcionMenu {
            get => txtOpcionMenu.Text;
            set => txtOpcionMenu.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CentroId {
            get => txtCentroId.Text;
            set => txtCentroId.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Centro {
            get => txtCentro.Text;
            set => txtCentro.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Estacion {
            get => txtEstacionId.Text;
            set => txtEstacionId.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Usuario {
            get => txtUsuarioDeLinea.Text;
            set => txtUsuarioDeLinea.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PasswordUsuario {
            get => txtPasswordAutoLogin.Text;
            set => txtPasswordAutoLogin.Text = value;
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Log {
            get => cbActivarLog.Checked;
            set => cbActivarLog.Checked = value;
        }
        public bool SoloLectura {
            get => txtEstacionId.ReadOnly;
            set {
                txtEstacionId.ReadOnly = value;
                txtOpcionMenu.ReadOnly = value;
                txtCentroId.ReadOnly = value;
                txtCentro.ReadOnly = value;
                txtIp.ReadOnly = value;
                txtUsuarioDeLinea.ReadOnly = value;
                //txtPasswordAutoLogin.ReadOnly = value;
                //cbActivarLog.Enabled = !value;
            }
        }
        #endregion

        #region Constructores
        public ucEstacion() {
            InitializeComponent();
            txtIp.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtIp, @"[^0-9.]");
            txtOpcionMenu.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtOpcionMenu, @"[^0-9]");
            txtCentroId.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtCentroId, @"[^0-9]");
            txtCentro.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtCentro, @"[^A-Za-z0-9-]");
            txtEstacionId.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtEstacionId, @"[^A-Fa-f0-9-]");
            txtUsuarioDeLinea.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtUsuarioDeLinea, @"[^A-Za-z0-9]");
            txtPasswordAutoLogin.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtPasswordAutoLogin, @"[^\x21-\x7E]");

            txtPasswordAutoLogin.UseSystemPasswordChar = true;

            txtIp.MaxLength = 15;
            txtOpcionMenu.MaxLength = 3;
            txtCentroId.MaxLength = 4;
            txtCentro.MaxLength = 6;
            txtEstacionId.MaxLength = 36;
            txtUsuarioDeLinea.MaxLength = 20;
            txtPasswordAutoLogin.MaxLength = 36;

        }

        private void ucEstacion_Load(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtIp.Text)) {
                txtIp.Text = Red.ObtenerIP192PC();
            }
        }
        #endregion
    }
}
