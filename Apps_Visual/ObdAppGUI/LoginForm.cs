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
            using (Serilog.Context.LogContext.PushProperty("Where", "Apps_Visual.ObdAppGUI.frmBASE")) {
                Log.Information("Inicio runId={RunId}. Carpeta usada={Dir}", AppRun.RunId, _usedDir);
            }
            if (LecturaRegedit()) {
                BloquearEstacion();
            }

            
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

        private void BloquearEstacion () {
            Log.Error("Estación No configurada");

        }










        /*
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

        */

        private async void LoginForm_Load(object sender, EventArgs e) {
            Application.Exit();
            Close();
            //await InicioAsync();
        }

        /*
        private async Task InicioAsync() {
            btnInspecionVisual.Enabled = false;

            if (_statusDlg != null && !_statusDlg.IsDisposed)
                _statusDlg.Close();

            _statusDlg = new frmMensajes { Mensaje = "Conectando a la base de datos…" };
            _statusDlg.Cerrar = false;
            _statusDlg.Show(this);

            this.UseWaitCursor = true;

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(8));
            try {

                var ok = await TestConexionAsync(/* cts.Token /*//*);

                if (ok == 1) {
                    _statusDlg.Mensaje = "Conectado.";
                    _statusDlg.Cerrar = true;
                    
                    btnInspecionVisual.Enabled = true;

                    // Deja ver el mensaje un instante y cierra
                    await Task.Delay(900);
                    if (_statusDlg is { IsDisposed: false }) _statusDlg.Close();
                } else {
                    _statusDlg.Mensaje = "No se pudo conectar.\n Manda ticket";
                }
            } catch (OperationCanceledException) {
                _statusDlg.Mensaje = "Tiempo de espera agotado al conectar.";

            } catch (Exception ex) {
                _statusDlg.Mensaje = "Error al conectar. \n Manda ticket";
                using var msm = new frmMensajes { Mensaje = $"Error: {ex.Message}" };
                msm.Show(this);
            } finally {
                this.UseWaitCursor = false;
                _statusDlg.Cerrar = true;
            }
        }




        private async Task<int> TestConexionAsync() {
            var conf = new RegWin();
            var appName = string.IsNullOrWhiteSpace(conf.DomainName)
        ? string.Empty
        : conf.DomainName.Trim();

            await using var conn = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
            await conn.OpenAsync();

            var repo = new SivevRepository();

            using (var scope = new AppRoleScope(conn, APPROLE, APPROLE_PASS)) {
                // Si tienes versión async del SP, úsala: await repo.SpAppRollClaveGetAsync(conn)
                var r = repo.SpAppRollClaveGet(conn);

                await repo.PrintIfMsgAsync(conn, "Fallo en SpAppRollClaveGet", r.MensajeId);
                if (r.MensajeId != 0)
                    return 0; // 0 = fallo

                var invertida = new string((r.ClaveAcceso ?? string.Empty).Reverse().ToArray());
                ClaveAcceso = invertida;
                RollAcceso = r.FuncionAplicacion;
            }

            return 1;
        }



        */

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







        

        private void AplicarTemaRecursivo(Control root, System.Drawing.Color color) {
            foreach (Control c in root.Controls) {
                if (c is Panel p) p.BackColor = color;
                if (c.HasChildren) AplicarTemaRecursivo(c, color);
            }
        }









    }
}
