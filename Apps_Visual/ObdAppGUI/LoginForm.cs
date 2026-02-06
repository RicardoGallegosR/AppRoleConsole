using Apps_Visual.ObdAppGUI.Views;
using Apps_Visual.UI.Theme;
using DPFP_SMA.Forms.Comun;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

//using DPFP_SMA.Models;


namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        
        private DPFP_SMA.Models.VisualRegistroWindows Visual_48;
        private SQLSIVEV.Infrastructure.Services.VisualRegistroWindows Visual_Core;
        private readonly RegistroCrypto _reg = new();


        private string _placa = string.Empty;
        private Guid _verificacionId = Guid.Empty;

        private Guid _AccesoIdObtenido = Guid.Empty;
        private bool _RealizarPruebaOBD = false;
        
        private TaskCompletionSource<bool>? _tcsAcceso;

        private HomeView home;
        private frmAuth frmcredenciales;
        private VerificationForm frmverification;
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
                        btnInspecionVisual.Focus();
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
                    server:     Visual_Core.Server,
                    db:         Visual_Core.Database,
                    user:       Visual_Core.User, 
                    pass:       Visual_Core.Password,
                    appName:    Visual_Core.AppName
                );
                await conn.OpenAsync();

                using (var scope = new AppRoleScope(conn, Visual_Core.AppRole, Visual_Core.AppRolePassword.ToString().ToUpper())) {
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
                    Visual_Core.RollVisualAcceso = Guid.Parse((r.ClaveAcceso ?? "").Reverse().ToArray());
                    Visual_Core.RollVisual = r.FuncionAplicacion;

                    SivevLogger.Information($"Valores regresados {Visual_Core.RollVisual}");
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
            btnApagar.Focus();
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
           
            Visual_Core = new VisualRegistroWindows{
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
                $"|| SERVER: {Visual_Core.Server}, " +
                $"|| DB: {Visual_Core.Database}, " +
                $"|| SQL_USER: {Visual_Core.User}, " +
                $"|| APPNAME: {Visual_Core.Password}, " +
                $"|| APPROLE: {Visual_Core.AppName}, " +
                $"|| RollAcceso: {Visual_Core.AppRole}, " +
                $"|| opcionMenu: {Visual_Core.OpcionMenuId}, " +
                $"|| estacionId: {Visual_Core.EstacionId}, " 
            );

            return vacio(Visual_Core.Server)
                 || vacio(Visual_Core.Database)
                 || vacio(Visual_Core.User)
                 || vacio(Visual_Core.Password)
                 || vacio(Visual_Core.AppName)
                 || vacio(Visual_Core.AppRole)
                 || vacio(Visual_Core.EstacionId.ToString())
                 || Visual_Core.OpcionMenuId <= 0;
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
            btnInspecionVisual?.Select();
            btnInspecionVisual.Focus();

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

            var p = home.GetPanel();
            p.Dock = DockStyle.Fill;
            pnlPanelCambios.Controls.Add(p);
        }
        #endregion

        #region Inicio de inspeccion visual
        private async void LoginForm_Load(object sender, EventArgs e) {
            //Application.Exit();
            //Close();
            //await InicioAsync();
            bool matarExplorer = false; 
            if (matarExplorer) {
                foreach (var proc in Process.GetProcessesByName("explorer"))
                    proc.Kill();
            }

            //var v = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "dev";

            //lblVerificaciónVehicularFoother.Text = $"Versión {v}";
        }

        /* Control de teclas */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            if (keyData == Keys.Escape && Visual_Core.AccesoId != Guid.Empty) {
                BeginInvoke(new Action(async () => {
                    var frmEscape = new frmEscape(visual: Visual_Core);
                    // Si quieres modal, usa ShowDialog y NO necesitas EsperarResultadoAsync.
                    // Si tu diseño es async, entonces Show (no modal) sí tiene sentido:
                    frmEscape.Show();
                    bool ok = await frmEscape.EsperarResultadoAsync();
                    if (!frmEscape.IsDisposed)
                        frmEscape.Close();
                    if (ok) {
                        pnlPanelCambios.Controls.Clear();
                        home = new HomeView();
                        home.panelX = pnlPanelCambios.Width;
                        home.panelY = pnlPanelCambios.Height;
                        home.InicializarTamanoYFuente();
                        pnlPanelCambios.Controls.Add(home.GetPanel());
                        pnlPanelCambios.Dock = DockStyle.Fill;

                        btnInspecionVisual.Enabled = true;
                        btnInspecionVisual.Visible = true;
                        btnInspecionVisual?.Select();
                        btnInspecionVisual.Focus();

                        btnApagar.Enabled = true;
                        btnApagar.Visible = true;

                        if (PruebaOBD != null && !PruebaOBD.IsDisposed) {
                            PruebaOBD.Parent?.Controls.Remove(PruebaOBD); // opcional pero recomendable si estaba embebido
                            PruebaOBD.Controls.Clear();
                            PruebaOBD.Dispose();
                        }
                        PruebaOBD = null;

                        // Cerrar CapturaVisual si aplica
                        if (CapturaVisual != null && !CapturaVisual.IsDisposed) {
                            CapturaVisual.Parent?.Controls.Remove(CapturaVisual);
                            CapturaVisual.Controls.Clear();
                            CapturaVisual.Dispose();
                        }
                        CapturaVisual = null;
                    }
                }));

                return true; // consumimos Escape
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
                MostrarMensaje($"Acceso no valido: {accesoValido} o Acceso Cancelado");
                pnlHome();
                return;
            }
            //Visual_Core.AccesoId = accesoValido;
            SivevLogger.Information($"Se guarda con el accesoId: {Visual_Core.AccesoId}");
            
                
            bool pruebaVisual = await ListadoVisual();
            if (!pruebaVisual) {
                SivevLogger.Information($"No pasa a prueba OBD la placa {_placa}: {pruebaVisual}");
                return;
            }
            Visual_Core.PlacaId = _placa;
            Visual_Core.VerificacionId = _verificacionId;

            await Task.Delay(200);

            bool PruebaOBD = await PruebaOBDPanel();
            if (!PruebaOBD) {
                SivevLogger.Information($"No pasa la prueba OBD la placa {Visual_Core.PlacaId}: {PruebaOBD}");
                return;
            }
        }


        #region Credenciales :D
        /*
        private async Task<bool> ValidaCredencial() {
            _tcsAcceso = new TaskCompletionSource<bool>();
            bool ok = false;

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
                
            pnlPanelCambios.Controls.Clear();

            if (frmverification == null || frmverification.IsDisposed) {
                Visual_48 = new DPFP_SMA.Models.VisualRegistroWindows {
                    Server = Visual_Core.Server,
                    Database = Visual_Core.Database,
                    User = Visual_Core.User,
                    Password = Visual_Core.Password,
                    AppName = Visual_Core.AppName,
                    AppRole = Visual_Core.AppRole,
                    AppRolePassword = Visual_Core.AppRolePassword,
                    OpcionMenuId = Visual_Core.OpcionMenuId,
                    Relleno = Visual_Core.Relleno,
                    UsuarioLinea = Visual_Core.UsuarioLinea,
                    Ip = Visual_Core.Ip,
                    Centro = Visual_Core.Centro,
                    ServidorVersionesControlador = Visual_Core.ServidorVersionesControlador,
                    Url = Visual_Core.Url,
                    EstacionId = Visual_Core.EstacionId,
                    RollVisualAcceso = Visual_Core.RollVisualAcceso,
                    RollVisual = Visual_Core.RollVisual,
                };
                frmverification?.Dispose();
                frmverification = new VerificationForm(visual: Visual_48);
                frmverification.AccesoObtenido += Frmcredenciales_AccesoObtenido;
                frmverification.ShowDialog(this);
            }
            ok = await _tcsAcceso.Task;

            return ok;
            
        }
        */



        /*
        private async Task<bool> ValidaCredencial() {
            _tcsAcceso = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            // Limpia panel actual
            foreach (Control c in pnlPanelCambios.Controls) c.Dispose();
            pnlPanelCambios.Controls.Clear();

            // Siempre crea un dialog nuevo (es lo más estable en WinForms)
            Visual_48 = new DPFP_SMA.Models.VisualRegistroWindows {
                Server = Visual_Core.Server,
                Database = Visual_Core.Database,
                User = Visual_Core.User,
                Password = Visual_Core.Password,
                AppName = Visual_Core.AppName,
                AppRole = Visual_Core.AppRole,
                AppRolePassword = Visual_Core.AppRolePassword,
                OpcionMenuId = Visual_Core.OpcionMenuId,
                Relleno = Visual_Core.Relleno,
                UsuarioLinea = Visual_Core.UsuarioLinea,
                Ip = Visual_Core.Ip,
                Centro = Visual_Core.Centro,
                ServidorVersionesControlador = Visual_Core.ServidorVersionesControlador,
                Url = Visual_Core.Url,
                EstacionId = Visual_Core.EstacionId,
                RollVisualAcceso = Visual_Core.RollVisualAcceso,
                RollVisual = Visual_Core.RollVisual,
            };

            using (var dlg = new VerificationForm(visual: Visual_48)) {
                dlg.AccesoObtenido += Frmcredenciales_AccesoObtenido;

                // Si el usuario cierra la ventana sin autenticarse, NO te quedes colgado
                dlg.FormClosed += (_, __) => _tcsAcceso?.TrySetResult(false);

                dlg.ShowDialog(this);

                // Espera resultado (ya garantizado que termina)
                return await _tcsAcceso.Task;
            }
        }
        //*/
        //*
        private async Task<bool> ValidaCredencial() {
            SivevLogger.Information("Inicializa ValidaCredencial()");
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

            frmcredenciales._Visual = Visual_Core;
            pnlPanelCambios.Controls.Add(frmcredenciales.GetPanel());
            pnlPanelCambios.Select();

            pnlPanelCambios.BeginInvoke(new Action(() =>  {
                try {
                    frmcredenciales.txbCredencial?.Select();
                    frmcredenciales.txbCredencial?.Focus();
                    SivevLogger.Information("Focus aplicado a txbCredencial");
                } catch (Exception ex) {
                    SivevLogger.Error($"No se pudo aplicar focus a txbCredencial {ex}");
                }
            }));

            bool ok = await _tcsAcceso.Task;
            return ok;
        }
        //*/
        #endregion

        #region Prueba Visual

        private async Task<bool> ListadoVisual() {
            SivevLogger.Information($"Entra a listado Visual con el Acceso: {Visual_Core.AccesoId.ToString().ToUpper()}");
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();


            if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                CapturaVisual = new frmCapturaVisual();
                CapturaVisual.lblTitulo.Focus();
                SivevLogger.Information("Inicializa los SetCallbacks");
                CapturaVisual.SetCallbacks(
                    placa => { _placa = placa; },
                    verId => { _verificacionId = verId; },
                    pasaObd => { _RealizarPruebaOBD = pasaObd; }
                );
            }
            CapturaVisual.panelX = pnlPanelCambios.Width;
            CapturaVisual.panelY = pnlPanelCambios.Height;
            CapturaVisual.InicializarTamanoYFuente();
            CapturaVisual._Visual = Visual_Core;

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
                    btnInspecionVisual?.Select();
                    btnInspecionVisual.Focus();
                }
                return false;
            }
            pnlPanelCambios.BeginInvoke(new Action(() => {
                try {
                    CapturaVisual.lblTitulo?.Select();
                    CapturaVisual.lblTitulo?.Focus();
                    SivevLogger.Information("Focus aplicado a txbCredencial");
                } catch (Exception ex) {
                    SivevLogger.Error($"No se pudo aplicar focus a txbCredencial {ex}");
                }
            }));
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
                    btnInspecionVisual?.Select();
                    btnInspecionVisual.Focus();
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
                PruebaOBD = new frmOBD(Visual_Core);
            }
            PruebaOBD._panelX = pnlPanelCambios.Width;
            PruebaOBD._panelY = pnlPanelCambios.Height;
            PruebaOBD.InicializarTamanoYFuente();
            PruebaOBD._Visual = Visual_Core;
           
            pnlPanelCambios.Controls.Add(PruebaOBD.GetPanel());

            pnlPanelCambios.BeginInvoke(new Action(() => {
                this.Activate();
                PruebaOBD.btnConectar.Visible = true;
                PruebaOBD.btnConectar.Enabled = true;
                if (!PruebaOBD.btnConectar.Focus())
                    SivevLogger.Warning("Focus() devolvió false en PruebaOBD.btnConectar");

                SivevLogger.Information($"CanFocus={PruebaOBD.btnConectar.CanFocus}");
            }));


            bool ok = await PruebaOBD.EsperarResultadoAsync();

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
                btnInspecionVisual?.Select();
                btnInspecionVisual.Focus();

                btnApagar.Enabled = true;
                btnApagar.Visible = true;

            }
            PruebaOBD.Controls.Clear();
            PruebaOBD.Dispose();
            PruebaOBD = null;
            if (ok) {
                
                return true;
            }else {
                return false;
            }
        }
        #endregion


        #region Apagar
        private void btnApagar_Click(object sender, EventArgs e) {
            var result = System.Windows.Forms.MessageBox.Show(
                "¿Desea apagar la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2  // ← "No" por defecto
            );
            if (result == DialogResult.Yes) {
                //Application.Exit();
                Process.Start("shutdown", "/s /t 0");
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


        /// <summary>
        /// GENERA UN BUG REVISAR A DETALLE
        /// </summary>
        /// <param name="accesoObtenido"></param>
        private void Frmcredenciales_AccesoObtenido(Guid accesoObtenido) {
            bool ok = accesoObtenido != Guid.Empty;
            if (ok) {
                //_AccesoIdObtenido = accesoObtenido;
                Visual_Core.AccesoId = accesoObtenido;
                pnlPanelCambios.Controls.Clear();
                frmcredenciales.Dispose();
                frmcredenciales = null;
                //frmverification.Close();
                //frmverification=null;
            }
            _tcsAcceso?.TrySetResult(ok);
        }
        /// <summary>
        /// Posible correccion pero aun no la ingreso
        /// </summary>
        /// <param name="nombrePropiedad"></param>
        /// <returns></returns>

        /*
         private void Frmcredenciales_AccesoObtenido(Guid accesoObtenido){
            // 1) Si el evento viene de otro hilo, brinca al hilo UI
            if (this.InvokeRequired)   {
                this.BeginInvoke(new Action(() => Frmcredenciales_AccesoObtenido(accesoObtenido)));
                return;
            }

            bool ok = accesoObtenido != Guid.Empty;

            if (ok)    {
                // 2) Primero guarda el acceso
                Visual_Core.AccesoId = accesoObtenido;

                // 3) Completa el TCS ANTES de destruir UI (para no quedar colgado si algo falla al limpiar)
                _tcsAcceso?.TrySetResult(true);

                // 4) Limpieza UI: hazla "después" del ciclo actual de mensajes (evita re-entrancy)
                this.BeginInvoke(new Action(() =>        {
                    try            {
                        // Mejor liberar controles (evitas fugas)
                        foreach (Control c in pnlPanelCambios.Controls)
                            c.Dispose();

                        pnlPanelCambios.Controls.Clear();

                        if (frmcredenciales != null && !frmcredenciales.IsDisposed)                {
                            frmcredenciales.AccesoObtenido -= Frmcredenciales_AccesoObtenido;
                            frmcredenciales.Dispose();
                        }

                        frmcredenciales = null;
                    } catch (Exception ex) {
                        SivevLogger.Error(ex, "Error limpiando UI tras AccesoObtenido");
                        // No regresamos false porque el acceso ya fue válido y el flujo debe seguir.
                    }
                }));

                return;
            }

            // Acceso inválido / cancelado
            Visual_Core.AccesoId = Guid.Empty;
            _tcsAcceso?.TrySetResult(false);
        }

         */













        /*
        private void Frmcredenciales_AccesoObtenido(Guid accesoObtenido) {
            bool ok = accesoObtenido != Guid.Empty;
            //Visual_Core.AccesoId = accesoObtenido;
            _tcsAcceso?.TrySetResult(ok);

            if (ok) {
                Visual_Core.AccesoId = accesoObtenido;
                SivevLogger.Information($"Guardar acceso solo si es válido Visual_Core.AccesoId: {Visual_Core.AccesoId}, accesoObtenido {accesoObtenido}");
            }
            
            SivevLogger.Information($"Acceso para Visual_Core = {Visual_Core.AccesoId}");

            if (!this.IsHandleCreated) return;

            this.BeginInvoke(new Action(() => {
                foreach (Control c in pnlPanelCambios.Controls)
                    c.Dispose();
                pnlPanelCambios.Controls.Clear();

                //*
                // Cierra/limpia el dialog siempre
                if (frmverification != null) {
                    try { frmverification.AccesoObtenido -= Frmcredenciales_AccesoObtenido; } catch { }
                    frmverification.Dispose();
                    frmverification = null;
                }
                //
                // Si estás usando frmcredenciales en algún flujo alterno, igual límpialo
                if (frmcredenciales != null) {
                    try { frmcredenciales.AccesoObtenido -= Frmcredenciales_AccesoObtenido; } catch { }
                    SivevLogger.Information("Cierra frmcredenciales");
                    frmcredenciales.Dispose();
                    frmcredenciales = null;
                }

                // Si fue cancelado/incorrecto, regresa a Home aquí mismo (más confiable)
                if (!ok) {
                    SivevLogger.Information("Fue cancelado en Frmcredenciales_AccesoObtenido  o salio algo mal asi que regresamos a home");
                    pnlHome();
                }
                   
            }));
        }
        */
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
