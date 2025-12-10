using Apps_Visual.ObdAppGUI.Views;
using Apps_Visual.UI.Theme;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Devices;
using Serilog;
using Serilog.Exceptions;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        
        private VisualRegistroWindows Visual;
        private readonly RegistroCrypto _reg = new();


        private string _placa = string.Empty;
        private Guid _verificacionId = Guid.Empty;

        private Guid _AccesoIdObtenido = Guid.Empty;
        private bool _RealizarPruebaOBD = false;
        
        private TaskCompletionSource<bool>? _tcsAcceso;

        private HomeView home;
        private frmAuth frmcredenciales;
        private frmCapturaVisual CapturaVisual;
        private frmOBD PruebaOBD;
        #endregion

        #region inicio
        public frmBASE() {

            InitializeComponent();
            btnInspecionVisual.Enabled = false;
            btnApagar.Enabled = false;

            this.Load += async (_, __) => {
                await Task.Delay(200);
                if (LecturaRegedit()) {
                    BloquearEstacionRegEdit();
                }else {
                    if (!await SpAppRollClaveGet()) {
                        BloquearEstacionSqlTest();
                    } else {
                        SivevLogger.Information("Se ha revisado el registro de windows y las configuraciones son correctas, problemas por aqui no son :)");
                        pnlHome();
                    }
                }
            };
        }
        #endregion

        #region SpAppRollClaveGet
        private async Task< bool> SpAppRollClaveGet() {
            try {
                await using var conn = SqlConnectionFactory.Create( 
                    server:     Visual.Server,
                    db:         Visual.Database,
                    user:       Visual.User, 
                    pass:       Visual.Password,
                    appName:    Visual.AppName
                );
                await conn.OpenAsync();

                using (var scope = new AppRoleScope(conn, Visual.AppRole, Visual.AppRolePassword.ToString().ToUpper())) {
                    var repo = new SivevRepository();
                    var r = repo.SpAppRollClaveGet(conn);
                    var MensajesSQL = await  repo.PrintIfMsgAsync(conn, "Fallo en SpAppRollClaveGet", r.MensajeId);
                    if (r.MensajeId != 0) {
                        SivevLogger.Error($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}");
                        using (var dlg = new frmMensajes($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}")) {
                            dlg.StartPosition = FormStartPosition.CenterParent;
                            dlg.TopMost = true;
                            dlg.ShowDialog(this);
                        }
                        return false;
                    }
                    Visual.RollVisualAcceso = Guid.Parse((r.ClaveAcceso ?? "").Reverse().ToArray());
                    Visual.RollVisual = r.FuncionAplicacion;

                    SivevLogger.Information($"Valores regresados {Visual.RollVisual}");
                }



                return true;
            } catch (SqlException ex) {
                SivevLogger.Error($"Fallo de conexión a SQL {ex.Message}");
                using (var dlg = new frmMensajes($"Fallo de conexión a SQL {ex.Message}")) {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.TopMost = true;
                    dlg.ShowDialog(this);
                }
                return false;
            }
        }

        #region BloquearEstacionSqlTest
        private void BloquearEstacionSqlTest() {
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorSQLPanel;
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = true;
            btnApagar.Visible = true;
            btnInspecionVisual.ForeColor = AppColors.BloqueoPorSQLTexto;
            pnlLateralIzquierdoAbajo.BackColor = AppColors.BloqueoPorSQLPanel;
            btnApagar.ForeColor = AppColors.BloqueoPorSQLTexto;


            pnlSeparadorFooterGrimsonI.BackColor = AppColors.BloqueoPorSQLPanel;
            pnlSeparadorFooterGrimsonII.BackColor = AppColors.BloqueoPorSQLPanel;

            pnlSeparadorFooterAmarillo.BackColor = AppColors.BloqueoPorSqlPanelSecundario;

            lblCDMX.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblDGCA.ForeColor = AppColors.BloqueoPorSQLTexto;


            lblSEDEMAFooter.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblVerificaciónVehicularFoother.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblGobiernoDeLa.ForeColor = AppColors.BloqueoPorSQLTexto;
        }
        #endregion

        #endregion

        #region Regedit
        private bool LecturaRegedit() {
            bool vacio(string s) => string.IsNullOrWhiteSpace(s);
            var lector = new GuardarWinRarConf();
            lector.CargarEnCryptoHelper();
            var conf = lector.GetConfig();
            CryptoHelper.Configurar(conf);
           
            Visual = new VisualRegistroWindows{
                Server                    = Leer(nameof(VisualRegistroWindows.Server)),
                Database                  = Leer(nameof(VisualRegistroWindows.Database)),
                User                      = Leer(nameof(VisualRegistroWindows.User)),
                Password                  = Leer(nameof(VisualRegistroWindows.Password)),
                AppName                   = Leer(nameof(VisualRegistroWindows.AppName)),
                AppRole                   = Leer(nameof(VisualRegistroWindows.AppRole)),
                AppRolePassword           = LeerGuid(nameof(VisualRegistroWindows.AppRolePassword)),
                OpcionMenuId              = LeerShort(nameof(VisualRegistroWindows.OpcionMenuId), 0),
                Relleno                   = LeerBool(nameof(VisualRegistroWindows.Relleno), false),
                UsuarioLinea              = Leer(nameof(VisualRegistroWindows.UsuarioLinea)),
                Ip                        = Leer(nameof(VisualRegistroWindows.Ip)),
                Centro                    = LeerShort(nameof(VisualRegistroWindows.Centro), 0),
                ServidorVersionesControlador = Leer(nameof(VisualRegistroWindows.ServidorVersionesControlador)),
                Url                       = Leer((nameof(VisualRegistroWindows.Url))),
                EstacionId                = LeerGuid(nameof(VisualRegistroWindows.EstacionId))
            };


            SivevLogger.Information(
                $"|| Lectura de REGEDIT " +
                $"|| SERVER: {Visual.Server}, " +
                $"|| DB: {Visual.Database}, " +
                $"|| SQL_USER: {Visual.User}, " +
                $"|| APPNAME: {Visual.Password}, " +
                $"|| APPROLE: {Visual.AppName}, " +
                $"|| RollAcceso: {Visual.AppRole}, " +
                $"|| opcionMenu: {Visual.OpcionMenuId}, " +
                $"|| estacionId: {Visual.EstacionId}, " 
            );

            return vacio(Visual.Server)
                 || vacio(Visual.Database)
                 || vacio(Visual.User)
                 || vacio(Visual.Password)
                 || vacio(Visual.AppName)
                 || vacio(Visual.AppRole)
                 || vacio(Visual.EstacionId.ToString())
                 || Visual.OpcionMenuId <= 0;
        }

        private void BloquearEstacionRegEdit() {
            SivevLogger.Error("Estación no configurada");
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorRegEditPanel;
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = true;
            btnApagar.Visible = true;
            pnlLateralIzquierdoAbajo.BackColor = AppColors.BloqueoPorRegEditPanel;
            btnApagar.ForeColor = AppColors.BloqueoPorRegEditTexto;


            pnlSeparadorFooterGrimsonI.BackColor = AppColors.BloqueoPorRegEditTexto;
            pnlSeparadorFooterGrimsonII.BackColor = AppColors.BloqueoPorRegEditTexto;

            pnlSeparadorFooterAmarillo.BackColor = AppColors.BloqueoPorRegEditPanelSecundario;

            lblCDMX.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblDGCA.ForeColor = AppColors.BloqueoPorRegEditTexto;
            
            lblSEDEMAFooter.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblVerificaciónVehicularFoother.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblGobiernoDeLa.ForeColor = AppColors.BloqueoPorRegEditTexto;

            using (var dlg = new frmMensajes("Estación no configurada\n Contactar a Calidad del Aire")) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }



        }
        #endregion

        #region metodo home
        private void pnlHome() {
            SivevLogger.Information("pnlHome se accede para el inicio del panel de home");
            btnInspecionVisual.Enabled = true;
            btnInspecionVisual.Visible = true;
            btnApagar.Enabled = true;
            btnApagar.Visible = true;
            SivevLogger.Information("Se habilita btnInspecionVisual");

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();
            if (home == null || home.IsDisposed) {
                home = new HomeView();
            }
            home.panelX = pnlPanelCambios.Width;
            home.panelY = pnlPanelCambios.Height;
            home.InicializarTamanoYFuente();
            pnlPanelCambios.Controls.Add(home.GetPanel());
            pnlPanelCambios.Dock = DockStyle.Fill;
        }
        #endregion

        #region Inicio de inspeccion visual
        private async void LoginForm_Load(object sender, EventArgs e) {
            //Application.Exit();
            //Close();
            //await InicioAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == (Keys.Alt | Keys.F4)) {
                // Bloqueas esa combinación
                return true; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private async void btnInspecionVisual_Click(object sender, EventArgs e){
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = false;
            btnApagar.Visible = false;

            // Validamos Verificacion 
            bool accesoValido = await ValidaCredencial();

            if (!accesoValido) {
                SivevLogger.Information($"Acceso no valido: {accesoValido}");
                return;
            }
            SivevLogger.Information($"Se guarda con el accesoId: {_AccesoIdObtenido}");
                
            bool pruebaVisual = await ListadoVisual();
            if (!pruebaVisual) {
                SivevLogger.Information($"No pasa a prueba OBD: {pruebaVisual}");
                return;
            }
            Visual.PlacaId = _placa;
            Visual.VerificacionId = _verificacionId;

            await Task.Delay(200);

            bool PruebaOBD = await PruebaOBDPanel();
            if (!PruebaOBD) {
                SivevLogger.Information($"No pasa la prueba OBD: {PruebaOBD}");
                return;
            }
        }
        

        #region Credenciales :D
        private async Task<bool> ValidaCredencial() {
            _tcsAcceso = new TaskCompletionSource<bool>();

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
                
            pnlPanelCambios.Controls.Clear();

            if (frmcredenciales == null || frmcredenciales.IsDisposed) {
                frmcredenciales = new frmAuth();
                frmcredenciales.AccesoObtenido += Frmcredenciales_AccesoObtenido;
            }
            frmcredenciales.panelX = pnlPanelCambios.Width;
            frmcredenciales.panelY = pnlPanelCambios.Height;
            frmcredenciales.InicializarTamanoYFuente();

            frmcredenciales._Visual = Visual;
            pnlPanelCambios.Controls.Add(frmcredenciales.GetPanel());

            bool ok = await _tcsAcceso.Task;
            return ok;
        }
        #endregion

        #region Prueba Visual
        private async Task<bool> ListadoVisual() {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();

            
            if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                CapturaVisual = new frmCapturaVisual();

                CapturaVisual.SetCallbacks(
                    placa => { _placa = placa; },
                    verId => { _verificacionId = verId; },
                    pasaObd => { _RealizarPruebaOBD = pasaObd; }
                );
            }
            CapturaVisual.panelX = pnlPanelCambios.Width;
            CapturaVisual.panelY = pnlPanelCambios.Height;
            CapturaVisual.InicializarTamanoYFuente();
            CapturaVisual._Visual = Visual;

            pnlPanelCambios.Controls.Add(CapturaVisual.GetPanel());
            bool VerificacionesDisponibles = await CapturaVisual.InicializarAsync();
            
            if (!VerificacionesDisponibles) {
                pnlPanelCambios.Controls.Clear();
                CapturaVisual.Dispose();
                CapturaVisual = null;
                if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                    home = new HomeView();
                    home.panelX = pnlPanelCambios.Width;
                    home.panelY = pnlPanelCambios.Height;
                    home.InicializarTamanoYFuente();
                    pnlPanelCambios.Controls.Add(home.GetPanel());
                    pnlPanelCambios.Dock = DockStyle.Fill;
                    btnInspecionVisual.Enabled = true;
                    btnInspecionVisual.Visible = true;
                    btnApagar.Enabled = true;
                    btnApagar.Visible = true;
                }
                return false;
            }
            bool ok = await CapturaVisual.EsperarResultadoAsync();

            if (!_RealizarPruebaOBD) {
                pnlPanelCambios.Controls.Clear();
                CapturaVisual.Dispose();
                CapturaVisual = null;
                if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                    home = new HomeView();
                    home.panelX = pnlPanelCambios.Width;
                    home.panelY = pnlPanelCambios.Height;
                    home.InicializarTamanoYFuente();
                    pnlPanelCambios.Controls.Add(home.GetPanel());
                    pnlPanelCambios.Dock = DockStyle.Fill;
                    btnInspecionVisual.Enabled = true;
                    btnInspecionVisual.Visible = true;
                    btnApagar.Enabled = true;
                    btnApagar.Visible = true;
                }
                return false;
            }
            return _RealizarPruebaOBD;
        }
        #endregion

        #region Prueba OBD



        private async Task<bool> PruebaOBDPanel() {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();

            if (PruebaOBD == null || PruebaOBD.IsDisposed) {
                PruebaOBD = new frmOBD(Visual);
            }
            PruebaOBD._panelX = pnlPanelCambios.Width;
            PruebaOBD._panelY = pnlPanelCambios.Height;
            PruebaOBD.InicializarTamanoYFuente();
            //PruebaOBD._Visual = Visual;

            pnlPanelCambios.Controls.Add(PruebaOBD.GetPanel());
            
            bool ok = await PruebaOBD.EsperarResultadoAsync();
            if (ok) {
                pnlPanelCambios.Controls.Clear();
                CapturaVisual.Dispose();
                CapturaVisual = null;
                if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                    home = new HomeView();
                    home.panelX = pnlPanelCambios.Width;
                    home.panelY = pnlPanelCambios.Height;
                    home.InicializarTamanoYFuente();
                    pnlPanelCambios.Controls.Add(home.GetPanel());
                    pnlPanelCambios.Dock = DockStyle.Fill;
                    btnInspecionVisual.Enabled = true;
                    btnInspecionVisual.Visible = true;
                    btnApagar.Enabled = true;
                    btnApagar.Visible = true;
                }
                return false;
            }
            return false;
        }
        #endregion


        #region Apagar
        private void btnApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(
                "¿Desea apagar la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2  // ← "No" por defecto
            );
            if (result == DialogResult.Yes) {
                Application.Exit();
                //Process.Start("shutdown", "/s /t 0");
            }
        }
        #endregion


        #region Utilerias
        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }


        private void Frmcredenciales_AccesoObtenido(Guid accesoObtenido) {
            bool ok = accesoObtenido != Guid.Empty;
            if (ok) {
                //_AccesoIdObtenido = accesoObtenido;
                Visual.AccesoId = accesoObtenido;   
                pnlPanelCambios.Controls.Clear();
                frmcredenciales.Dispose();
                frmcredenciales = null;
            }
            _tcsAcceso?.TrySetResult(ok);
        }
        private string Leer(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;
            return CryptoHelper.Desencriptar(cifrado);
        }
        private Guid LeerGuid(string nombrePropiedad) {
            string plano = Leer(nombrePropiedad); // usa el método que desencripta
            if (string.IsNullOrWhiteSpace(plano))
                return Guid.Empty;

            return Guid.TryParse(plano, out var g) ? g : Guid.Empty;
        }
        private string LeerString(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;

            return CryptoHelper.Desencriptar(cifrado);
        }
        private short LeerShort(string nombrePropiedad, short defecto = 0) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return short.TryParse(plano, out var valor) ? valor : defecto;
        }

        private bool LeerBool(string nombrePropiedad, bool defecto = false) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return bool.TryParse(plano, out var valor) ? valor : defecto;
        }

        #endregion
    }
}
