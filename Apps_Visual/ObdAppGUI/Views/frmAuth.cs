using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmAuth : Form {
        public Guid estacionId = Guid.Empty, accesoId;
        public string SERVER = string.Empty, DB = string.Empty, SQL_USER = string.Empty, SQL_PASS = string.Empty,
            appName = string.Empty, APPROLE = string.Empty, APPROLE_PASS = string.Empty;
        public int credencial = 0, panelX = 0, panelY = 0;
        public short opcionMenu = 0;


        public frmAuth() {
            InitializeComponent();
            txbCredencial.TextChanged += (s, ev) => SanitizeByRegex(txbCredencial, @"[^0-9]");
            txbPassword.TextChanged += (s, ev) => SanitizeByRegex(txbPassword, @"[^a-zA-Z0-9]");
            txbCredencial.MaxLength = 6; 
            txbPassword.MaxLength = 32;
            txbCredencial.PreviewKeyDown += txbCredencial_PreviewKeyDown;
            txbCredencial.TextChanged += txbCredencial_TextChanged; // solo este
        }


        private async Task<CredencialExisteHuellaResult> CredencialExisteHuella(string SERVER, string DB, string SQL_USER, string SQL_PASS, string appName, string APPROLE, string APPROLE_PASS, Guid estacionId, short opcionMenu, int credencial) {
            int _mensaje = 0;
            short _resultado =  0;
            bool _existeHuella = false;
            byte[] _huella = Array.Empty<byte>();
            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                await connApp.OpenAsync();
                using var scope = new AppRoleScope(connApp, APPROLE, APPROLE_PASS);
                try {

                    // Abrir acceso
                    var rinicial = repo.SpAppCredencialExisteHuella(cnn:connApp,uiEstacionId: estacionId, siOpcionMenuId:opcionMenu,iCredencial:credencial);
                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;
                    _existeHuella = rinicial.ExisteHuella;
                    _huella = rinicial.Huella;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppCredencialExisteHuella", _mensaje);
                        using (var dlg = new frmMensajes($"Error en SpAppCredencialExisteHuella {error.Mensaje}")) {
                            dlg.StartPosition = FormStartPosition.CenterParent;
                            dlg.TopMost = true;
                            dlg.ShowDialog(this);
                        }
                    }

                } catch (Exception ex) {
                    using (var dlg = new frmMensajes($"Error en SpAppCredencialExisteHuella {ex.Message}")) {
                        dlg.StartPosition = FormStartPosition.CenterParent;
                        dlg.TopMost = true;
                        dlg.ShowDialog(this);
                    }
                } finally {
                    if (accesoId != Guid.Empty) {
                        var fin = await repo.SpAppAccesoFinAsync(connApp, estacionId, accesoId);
                        using (var dlg = new frmMensajes($"[Fin Acceso] Resultado={fin.Resultado} MensajeId={fin.MensajeId} Return={fin.ReturnCode}")) {
                            dlg.StartPosition = FormStartPosition.CenterParent;
                            dlg.TopMost = true;
                            dlg.ShowDialog(this);
                        }
                    }
                }
            } catch {
                Console.WriteLine("");
            }

            return new CredencialExisteHuellaResult {
                MensajeId = _mensaje,
                Resultado = _resultado,
                ExisteHuella = _existeHuella,
                Huella = _huella
            };
        }







        private int validaCredencialNumerico(string strcredencial) {
            if (int.TryParse(strcredencial, out int credencial)) {
                return credencial;
            } else {
                using (var dlg = new frmMensajes($"Solo números en la credencial")) {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.TopMost = true;
                    dlg.ShowDialog(this);
                }
            }
            return 0;
        }



        private void btnAcceder_Click(object sender, EventArgs e) {

        }

        private void txbCredencial_TextChanged(object sender, EventArgs e) {
            var tb = (TextBox)sender;
            int sel = tb.SelectionStart;
            string original = tb.Text;
            string limpio = new string(original.Where(char.IsDigit).ToArray());
            if (original != limpio) {
                int removidosIzq = original.Take(sel).Count(c => !char.IsDigit(c));
                tb.Text = limpio;
                tb.SelectionStart = Math.Max(sel - removidosIzq, 0);
            }
        }
        private void txbCredencial_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.IsInputKey = true;

                credencial = validaCredencialNumerico(txbCredencial.Text);
                
                bool IsSet(string s) => !string.IsNullOrWhiteSpace(s);

                if (IsSet(SERVER) && IsSet(DB) && IsSet(SQL_USER) && IsSet(SQL_PASS)
                       && IsSet(appName) && IsSet(APPROLE) && IsSet(APPROLE_PASS) && estacionId != Guid.Empty) {

                    /*
                    var r = await  CredencialExisteHuella(SERVER: SERVER, DB: DB, SQL_USER: SQL_USER,SQL_PASS: SQL_PASS,
                        appName: appName, APPROLE: APPROLE, APPROLE_PASS: APPROLE_PASS, estacionId: estacionId,
                        opcionMenu: opcionMenu, credencial: credencial);

                    */
                    /*
                     ##############################################################################
                     */
                    /*
                    var huellaInfo = (r.Huella == null) ? "null" : $"{r.Huella.Length} bytes";
                    using (var dlg = new frmMensajes(
                        $"MensajeId: {r.MensajeId}\nResultado: {r.Resultado}\nExisteHuella: {r.ExisteHuella}\nHuella: {huellaInfo}")) {
                        dlg.StartPosition = FormStartPosition.CenterParent;
                        dlg.TopMost = true;
                        dlg.ShowDialog(this);
                    }
                    */



                } else {
                    using (var dlg = new frmMensajes(
                            $"SERVER: {SERVER}\nDB: {DB}\nSQL_USER: {SQL_USER}\nSQL_PASS: {SQL_PASS}\nappName: {appName}\nAPPROLE_PASS: {APPROLE_PASS}\nestacionId: {estacionId}")) {
                        dlg.StartPosition = FormStartPosition.CenterParent;
                        dlg.TopMost = true;
                        dlg.ShowDialog(this);
                    }
                }
            }
        }


        private async void txbCredencial_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.V) {
                var clip = Clipboard.GetText();
                if (string.IsNullOrEmpty(clip) || !clip.All(char.IsDigit))
                    e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                var txt = txbCredencial.Text.Trim();
                if (int.TryParse(txt, out var valor) && valor > 100) {

                }
            } else {
                txbCredencial.SelectAll();
                txbCredencial.Focus();
            }
        }
    


        private void frmAuth_Load(object sender, EventArgs e) {
            
        }

        private void SanitizeByRegex(TextBox tb, string invalidPattern) {
            string original = tb.Text;
            int sel = tb.SelectionStart;

            string cleaned = Regex.Replace(original, invalidPattern, "");

            if (original != cleaned) {
                int left = Math.Min(sel, original.Length);
                int removedLeft = Regex.Matches(original.Substring(0, left), invalidPattern).Count;

                tb.Text = cleaned;
                tb.SelectionStart = Math.Max(sel - removedLeft, 0);
            }
        }

        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }
        private void ResetForm() {
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
        }

    }
}
