using Apps_Visual.ObdAppGUI.Views;
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



namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        const string SERVER = AppConfig.Sql.Server;
        const string DB = AppConfig.Sql.Database;
        const string SQL_USER = AppConfig.Sql.User;
        const string SQL_PASS = AppConfig.Sql.Pass;

        const string APPROLE = AppConfig.Security.AppRole;
        const string APPROLE_PASS = AppConfig.Security.AppRolePass;

        string RollAcceso = string.Empty;
        string ClaveAcceso = string.Empty;
        int MensajeId = 0;

        Guid accesoId = Guid.Empty;
        Guid VerificacionId = Guid.Empty;
        byte ProtocoloVerificacionId = 0;
        string PlacaId = string.Empty;
        byte combustible = 1;

        /*
        Guid estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");

        short opcionMenu = 151;
        int credencial = 16499;
        string passCredencial = "PASS1234";
        */

        byte tiTaponCombustible = 0;
        byte tiTaponAceite = 0;
        byte tiBayonetaAceite = 0;
        byte tiPortafiltroAire = 0;
        byte tiTuboEscape = 0;
        byte tiFugasMotorTrans = 0;
        byte tiNeumaticos = 0;
        byte tiComponentesEmisiones = 0;
        byte tiMotorGobernado = 0;
        int odometro = 0;














        private HomeView home;
        private frmMensajes _statusDlg;
        #endregion



















        public frmBASE() {
            var conf = new RegWin();
            var appName = string.IsNullOrWhiteSpace(conf.DomainName) ? "" : conf.DomainName.Trim();
            var repo = new SivevRepository();





            InitializeComponent();
            pnlHome();
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
            await InicioAsync();
        }


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

                var ok = await TestConexionAsync(/* cts.Token */);

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





        private void btnInspecionVisual_Click(object sender, EventArgs e) {

        }

        private void btnApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("¿Desea apagar la aplicación?","Confirmar salida", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes) {
                Application.Exit();
                //Process.Start("shutdown", "/s /t 0");
            }
        }

        private void pnlPanelCambios_Paint(object sender, PaintEventArgs e) {

        }
    }
}
