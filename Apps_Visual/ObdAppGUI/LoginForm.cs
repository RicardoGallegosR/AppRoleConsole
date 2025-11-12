using Apps_Visual.ObdAppGUI.Views;
using Microsoft.Extensions.Configuration;
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
using Apps_Visual.UI.Theme;
using Microsoft.Data.SqlClient;



namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        private string SERVER, DB, SQL_USER, SQL_PASS, APPNAME, APPROLE, APPROLE_PASS,
            RollAccesoVisual, RollAccesoVisualAcceso, estacionId, claveAccesoId, accesoId,
            VerificacionId , PlacaId;

        private short opcionMenu;
        private int MensajeId, odometro;

        private byte ProtocoloVerificacionId, combustible,  tiTaponCombustible,
            tiTaponAceite, tiBayonetaAceite, tiPortafiltroAire, tiTuboEscape, tiFugasMotorTrans,
            tiNeumaticos,tiMotorGobernado;
        
        
        private readonly ILogger _baseLog;
        private readonly string  _usedDir;

        /*
        int credencial = 16499;
        string passCredencial = "PASS1234";
        */

        private HomeView home;
        private frmMensajes _statusDlg;
        #endregion


        public frmBASE() {
            InitializeComponent();
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
                        pnlHome();
                    }
                }
            };
        }

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

        private void pnlHome() {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();
            if (home == null || home.IsDisposed) {
                home = new HomeView();
            }
            home.panelX = pnlPanelCambios.Width;
            home.panelY = pnlPanelCambios.Height;
            pnlPanelCambios.Controls.Add(home.GetPanel());
            pnlPanelCambios.Dock = DockStyle.Fill;
        }

        

        private async void LoginForm_Load(object sender, EventArgs e) {
            //Application.Exit();
            //Close();
            //await InicioAsync();
        }

        

        private void btnInspecionVisual_Click(object sender, EventArgs e) {

        }

        private void btnApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("¿Desea apagar la aplicación?","Confirmar salida", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes) {
                Log.CloseAndFlush();
                Application.Exit();
                //Process.Start("shutdown", "/s /t 0");
            }
        }

        private void pnlPanelCambios_Paint(object sender, PaintEventArgs e) {

        }















    }
}
