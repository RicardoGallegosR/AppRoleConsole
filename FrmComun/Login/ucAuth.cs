using FrmComun.Utils;

namespace FrmComun.Login {
    public partial class ucAuth : UserControl {
        public event Action<Guid> AccesoObtenido;
        //public VisualRegistroWindows _Visual;

        private Size _formSizeInicial;
        private float _fontSizeInicial;

        public int credencial = 0, panelX = 0, panelY = 0;
        public short opcionMenu = 0;

        public bool ExisteHuella;
        public byte[] Huella;
        public event Action<string> _credencial;


        public ucAuth() {
            InitializeComponent();
            ResetForm();
        }
        private void ResetForm() {
            txbCredencial.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txbCredencial, @"[^0-9]");
            txbPassword.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txbPassword, @"[^a-zA-Z0-9]");
            txbCredencial.MaxLength = 6;
            txbPassword.MaxLength = 32;
            txbCredencial.Focus();

            //txbCredencial.PreviewKeyDown += txbCredencial_PreviewKeyDown;
            //txbCredencial.TextChanged += txbCredencial_TextChanged;
            _fontSizeInicial = this.Font.Size;

            lblCredencial.Enabled = true;
            txbCredencial.Enabled = true;
            btnAcceder.Enabled = false;
            btnAcceder.Visible = false;
            txbPassword.Enabled = false;
            txbPassword.Visible = false;
            lblPassword.Visible = false;

            if (panelX == 0 && panelY == 0) {
                pnlPrincipal.Size = new Size(Width, Height);
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * Width), 0);
            } else {
                pnlPrincipal.Size = new Size((int)Math.Ceiling(.98 * panelX), (int)Math.Ceiling(.95 * panelY));
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * panelX), 0);
            }
            txbCredencial.Focus();
        }


        #region Buscar
        private void btnAcceder_Click(object sender, EventArgs e) {
            ActivacionBotonAcceder();
        }
        private void txbPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ActivacionBotonAcceder();
            }
        }

        private async void ActivacionBotonAcceder() {
            /*
            btnAcceder.Enabled = false;
            txbPassword.Enabled = false;
            lblPassword.Enabled = false;
            txbCredencial.Focus();
            _Visual.dvar18 = txbCredencial.Text.ToString();
            Mostrar.Mensajes($"Verificando credencial {_Visual.dvar18} y contraseña, por favor espere...");
            var r = await GetAccesoSQL(V:_Visual, credencial:credencial);
            Guid accesoNormalizado = Guid.Empty;
            if (r != null && r.MensajeId == 0 && r.AccesoId != Guid.Empty) {
                accesoNormalizado = r.AccesoId;
                await Task.Delay(200);
                AccesoObtenido?.Invoke(accesoNormalizado);
            }
            if (accesoNormalizado == Guid.Empty) {
                btnAcceder.Enabled = true;
                txbPassword.Text = "";
                txbPassword.Enabled = true;
                lblPassword.Enabled = true;
                txbPassword.Focus();
            }
            */
        }
            
        #endregion

        
    }
}
