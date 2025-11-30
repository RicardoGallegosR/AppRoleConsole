using Apps_Visual.ObdAppGUI.Views;
using Apps_Visual.UI.Theme;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Devices;
using Serilog;
using Serilog.Exceptions;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Security;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;



namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        private string SERVER, DB, SQL_USER, SQL_PASS, APPNAME, APPROLE, APPROLE_PASS,
            RollAccesoVisual, RollAccesoVisualAcceso, estacionId, claveAccesoId;
        
        private string _placa = string.Empty;
        private Guid _verificacionId = Guid.Empty;

        private Guid _estacionId = Guid.Empty;
        private Guid _AccesoIdObtenido = Guid.Empty;
        private bool _RealizarPruebaOBD = false;
        private short opcionMenu;
        private TaskCompletionSource<bool>? _tcsAcceso;


        // OBTENCION DE CREDENCIALES VISUAL




        
        
        private readonly ILogger _baseLog;
        private readonly string  _usedDir;


        private HomeView home;
        private frmAuth frmcredenciales;
        private frmCapturaVisual CapturaVisual;
        private frmOBD PruebaOBD;
        #endregion

        #region inicio
        public frmBASE() {
            InitializeComponent();
            btnInspecionVisual.Enabled = false;

            _usedDir = Logs.Init(AppRun.RunId, "frmBASE");


            this.Load += async (_, __) => {
                using (Serilog.Context.LogContext.PushProperty("Where", "Apps_Visual.ObdAppGUI.frmBASE"))
                    Log.Information("Inicio runId={RunId}. Carpeta usada={Dir}", AppRun.RunId, _usedDir);


                await Task.Delay(200);
                if (LecturaRegedit()) {
                    BloquearEstacionRegEdit();
                }else {
                    if (!await SpAppRollClaveGet()) {
                        BloquearEstacionSqlTest();
                    } else {
                        Log.Information("Se ha revisado el registro de windows y las configuraciones son correctas, problemas por aqui no son :)");
                        pnlHome();
                        Log.CloseAndFlush();
                    }
                }
            };
        }
        #endregion

        #region SpAppRollClaveGet
        private async Task< bool> SpAppRollClaveGet() {
            try {
                await using var conn = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, APPNAME);
                await conn.OpenAsync();

                //MessageBox.Show($"SERVER: {SERVER}, DB: {DB}, SQL_USER: {SQL_USER}, SQL_PASS: {SQL_PASS}, APPNAME: {APPNAME}");
                //MessageBox.Show($"APPROLE: {APPROLE}, APPROLE_PASS: {APPROLE_PASS.ToString()}");

                using (var scope = new AppRoleScope(conn, APPROLE, "53CE7B6E-1426-403A-857E-A890BB63BFE6")) {
                    var repo = new SivevRepository();
                    var r = repo.SpAppRollClaveGet(conn);
                    var MensajesSQL = await  repo.PrintIfMsgAsync(conn, "Fallo en SpAppRollClaveGet", r.MensajeId);
                    if (r.MensajeId != 0) {
                        Log.Error($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}");
                        using (var dlg = new frmMensajes($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}")) {
                            dlg.StartPosition = FormStartPosition.CenterParent;
                            dlg.TopMost = true;
                            dlg.ShowDialog(this);
                        }
                        return false;
                    }
                    RollAccesoVisualAcceso = new string((r.ClaveAcceso ?? "").Reverse().ToArray());
                    RollAccesoVisual = r.FuncionAplicacion;
                    Log.Information($"Valores regresados {Logs.Mask(RollAccesoVisualAcceso)} y {Logs.Mask(RollAccesoVisual)}");
                }



                return true;
            } catch (SqlException ex) {
                Log.Error(ex, $"Fallo de conexión a SQL {ex.Message}");
                using (var dlg = new frmMensajes($"Fallo de conexión a SQL {ex.Message}")) {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.TopMost = true;
                    dlg.ShowDialog(this);
                }
                return false;
            }
        }






        private void BloquearEstacionSqlTest() {
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorSQLPanel;
            btnInspecionVisual.Enabled = false;
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

        #region Regedit
        private bool LecturaRegedit() {
            bool vacio(string s) => string.IsNullOrWhiteSpace(s);

            SERVER = AppConfig.Sql.SqlServerName() ?? "";
            DB = AppConfig.Sql.BaseSql() ?? "";
            SQL_USER = AppConfig.Sql.SQL_USER() ?? "";
            SQL_PASS = AppConfig.Sql.SQL_PASS() ?? "";
            APPNAME = AppConfig.Sql.APPNAME() ?? "";

            APPROLE = AppConfig.RollInicial.APPROLE() ?? "";
            APPROLE_PASS = AppConfig.RollInicial.APPROLE_PASS() ?? "";

            RollAccesoVisual = AppConfig.RollVisual.APPROLE_VISUAL() ?? "";
            RollAccesoVisualAcceso = AppConfig.RollVisual.APPROLE_PASS_VISUAL() ?? "";

            opcionMenu = AppConfig.CredencialesRegEdit.OpcionMenu();
            estacionId = AppConfig.CredencialesRegEdit.EstacionId() ?? "";
            claveAccesoId = AppConfig.CredencialesRegEdit.ClaveAccesoId() ?? "";

            _estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");

            using (Serilog.Context.LogContext.PushProperty("Where", "Apps_Visual.ObdAppGUI.LecturaRegedit")) {
                Log.Information(
                "|| Lectura de REGEDIT " +
                "|| SERVER: {SERVER}, " +
                "|| DB: {DB}, " +
                "|| SQL_USER: {SQL_USER}, " +
                "|| SQL_PASS: {SQL_PASS}, " +
                "|| APPNAME: {APPNAME}, " +
                "|| APPROLE: {APPROLE}, " +
                "|| APPROLE_PASS: {APPROLE_PASS}, " +
                "|| RollAccesoVisual: {RollAccesoVisual}, " +
                "|| RollAccesoVisualAcceso: {RollAccesoVisualAcceso}, " +
                "|| opcionMenu: {opcionMenu}, " +
                "|| estacionId: {estacionId}, " +
                "|| AccesoId: {claveAccesoId} " +
                "|| DirLogs: {UsedDir}",
                SERVER, DB, SQL_USER, Logs.Mask(SQL_PASS),
                APPNAME, APPROLE, Logs.Mask(APPROLE_PASS),
                RollAccesoVisual, RollAccesoVisualAcceso, opcionMenu, Logs.Mask(estacionId),
                 Logs.Mask(claveAccesoId), _usedDir);
            
            }
            return vacio(SERVER)
                 || vacio(DB)
                 || vacio(SQL_USER)
                 || vacio(SQL_PASS)
                 || vacio(APPNAME)
                 || vacio(APPROLE)
                 || vacio(APPROLE_PASS)
                 || vacio(estacionId)
                 || opcionMenu <= 0;
        }

        private void BloquearEstacionRegEdit() {
            Log.Error("Estación no configurada");
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorRegEditPanel;
            btnInspecionVisual.Enabled = false;
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
            Log.Information("pnlHome se accede para el inicio del panel de home");
            btnInspecionVisual.Enabled = true;
            Log.Information("Se habilita btnInspecionVisual");

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

        #endregion

        private async void btnInspecionVisual_Click(object sender, EventArgs e){
            btnInspecionVisual.Enabled = false;
            var traceId   = Guid.NewGuid().ToString("N");
            var tsStamp   = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            var shortId   = traceId[..6];

            var baseDir   = Logs.GetBaseDirFromRun(AppRun.RunId);
            var auditDir  = Path.Combine(baseDir, "audit");
            Directory.CreateDirectory(auditDir);

            var auditFile = Path.Combine(auditDir, $"InspeccionVisual-{AppRun.RunId}-{tsStamp}-{shortId}.log");

            using var audit = Logs.CreateAuditLogger(auditFile, metodo: "btnInspeccionVisual_Click");
            
            using (Serilog.Context.LogContext.PushProperty("Where", "Apps_Visual.ObdAppGUI.frmBASE.btnInspeccionVisual_Click"))
            using (Serilog.Context.LogContext.PushProperty("TraceId", traceId))
            using (Serilog.Context.LogContext.PushProperty("Evento", "Visual.Inspeccion"))
            using (Serilog.Context.LogContext.PushProperty("Canal", "Audit")) {
                audit.Information("Se inicializa prueba.");

                //await ValidarHuella();
                


                // Validamos Verificacion 
                bool accesoValido = await ValidaCredencial();

                if (!accesoValido) {
                    audit.Information($"Acceso no valido: {accesoValido}");
                    return;
                }
                audit.Information($"Se guarda con el accesoId: {_AccesoIdObtenido}");
                
                bool pruebaVisual = await ListadoVisual();
                if (!pruebaVisual) {
                    audit.Information($"No pasa a prueba OBD: {pruebaVisual}");
                    return;
                }

                bool PruebaOBD = await PruebaOBDPanel();
                if (!PruebaOBD) {
                    audit.Information($"No pasa la prueba OBD: {PruebaOBD}");
                    return;
                }
            }
        }

        #region Credenciales :D
        private async Task<bool> ValidaCredencial() {
            _tcsAcceso = new TaskCompletionSource<bool>();

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
                
            pnlPanelCambios.Controls.Clear();
            ///*
            if (frmcredenciales == null || frmcredenciales.IsDisposed) {
                frmcredenciales = new frmAuth();
                frmcredenciales.AccesoObtenido += Frmcredenciales_AccesoObtenido;
            }
            frmcredenciales.panelX = pnlPanelCambios.Width;
            frmcredenciales.panelY = pnlPanelCambios.Height;
            frmcredenciales.InicializarTamanoYFuente();


            ///*
            frmcredenciales.estacionId = _estacionId;


            frmcredenciales.SERVER = SERVER;
            frmcredenciales.DB = DB;
            frmcredenciales.SQL_USER = SQL_USER;
            frmcredenciales.SQL_PASS = SQL_PASS;
            frmcredenciales.appName = APPNAME;
            frmcredenciales.APPROLE = RollAccesoVisual;
            frmcredenciales.APPROLE_PASS = RollAccesoVisualAcceso;
            frmcredenciales.opcionMenu = opcionMenu;
            


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
            CapturaVisual.SERVER = SERVER;
            CapturaVisual.DB = DB;
            CapturaVisual.SQL_USER = SQL_USER;
            CapturaVisual.SQL_PASS = SQL_PASS;
            CapturaVisual.appName = APPNAME;
            CapturaVisual.APPROLE = RollAccesoVisual;
            CapturaVisual.APPROLE_PASS = RollAccesoVisualAcceso;
            CapturaVisual._accesoId = _AccesoIdObtenido;
            CapturaVisual._estacionId = _estacionId;

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
                }
                return false;
            }
            //MostrarMensaje($"PLACA: {_placa}, VERIFICACION: {_verificacionId}, OBD: {_RealizarPruebaOBD}");

            return _RealizarPruebaOBD;
        }
        #endregion

        #region Prueba OBD
        private async Task<bool> PruebaOBDPanel() {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();

            if (PruebaOBD == null || PruebaOBD.IsDisposed) {
                PruebaOBD = new frmOBD();
            }
            PruebaOBD._panelX = pnlPanelCambios.Width;
            PruebaOBD._panelY = pnlPanelCambios.Height;
            PruebaOBD.InicializarTamanoYFuente();
            PruebaOBD._SERVER = SERVER;
            PruebaOBD._DB = DB;
            PruebaOBD._SQL_USER = SQL_USER;
            PruebaOBD._SQL_PASS = SQL_PASS;
            PruebaOBD._appName = APPNAME;
            PruebaOBD._APPROLE = RollAccesoVisual;
            PruebaOBD._APPROLE_PASS = RollAccesoVisualAcceso;
            PruebaOBD._placa = _placa;
            PruebaOBD._verificacionId = _verificacionId;
            PruebaOBD._accesoId = _AccesoIdObtenido;
            PruebaOBD._estacionId = _estacionId;
            
            
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
                Log.CloseAndFlush();
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
                _AccesoIdObtenido = accesoObtenido;
                pnlPanelCambios.Controls.Clear();
                frmcredenciales.Dispose();
                frmcredenciales = null;
            }
            _tcsAcceso?.TrySetResult(ok);
        }
        #endregion
    }
}
