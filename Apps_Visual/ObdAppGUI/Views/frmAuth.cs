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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmAuth : Form {
        private Guid accesoId = Guid.Empty;
        private Guid estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");
        private string SERVER = "192.168.16.8";
        private string DB = "SIVEV";
        private string SQL_USER = "SivevCentros";
        private string SQL_PASS = "CentrosSivev";
        private string appName = "SivAppVfcVisual";
        private string APPROLE = "RollVfcVisual";
        private string APPROLE_PASS = "95801B7A-4577-A5D0-952E-BD3D89757EA5";
        int credencial = 0;


        public frmAuth() {
            InitializeComponent();
            txbCredencial.TextChanged += (s, ev) => SanitizeByRegex(txbCredencial, @"[^0-9]");
            txbPassword.TextChanged += (s, ev) => SanitizeByRegex(txbPassword, @"[^a-zA-Z0-9]");
            txbCredencial.MaxLength = 10;  // ej. 10 dígitos
            txbPassword.MaxLength = 32;  // ej. 32 chars

        }


        private async Task<CredencialExisteHuellaResult> CredencialExisteHuella(string SERVER, string DB, string SQL_USER, string SQL_PASS, string appName, string APPROLE, string APPROLE_PASS, Guid estacionId, short opcionMenu, int credencial) {
            int _mensaje = 0;
            short _resultado =  0;
            bool _existeHuella = false;
            byte[] _huella = Array.Empty<byte>();
            var repo = new SivevRepository();

            MessageBox.Show($"Antes de esntrar");
            MessageBox.Show($"SERVER: {SERVER}\nDB: {DB}\nSQL_USER: {SQL_USER}\nSQL_PASS: {SQL_PASS}\nappName: {appName}");

            try {
                using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                await connApp.OpenAsync();
                MessageBox.Show($"abre la bdd");

                using var scope = new AppRoleScope(connApp, APPROLE, APPROLE_PASS);
                MessageBox.Show($"me enrolo");

                try {

                    // Abrir acceso
                    var rinicial = repo.SpAppCredencialExisteHuella(cnn:connApp,uiEstacionId: estacionId, siOpcionMenuId:opcionMenu,iCredencial:credencial);
                    MessageBox.Show($"al hacer la consulta ");

                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;
                    _existeHuella = rinicial.ExisteHuella;
                    _huella = rinicial.Huella;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppCredencialExisteHuella", _mensaje);
                        frmMensajes msm = new frmMensajes();
                        msm.Mensaje = error.Mensaje;
                        msm.Show();
                    }

                } catch (Exception ex) {
                    Console.WriteLine($"Excepción: {ex.Message}");
                } finally {
                    if (accesoId != Guid.Empty) {
                        var fin = await repo.SpAppAccesoFinAsync(connApp, estacionId, accesoId);
                       MessageBox.Show($"[Fin Acceso] Resultado={fin.Resultado} MensajeId={fin.MensajeId} Return={fin.ReturnCode}");
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
                MessageBox.Show("Por favor ingresa solo números en la credencial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return 0;
        }



        private void btnAcceder_Click(object sender, EventArgs e) {
            credencial = validaCredencialNumerico(txbCredencial.Text);
            var r =  CredencialExisteHuella(SERVER, DB, SQL_USER, SQL_PASS, appName, APPROLE, APPROLE_PASS, estacionId, opcionMenu: 151, credencial: credencial);


            MessageBox.Show($"resultado = {r.Result}");
        }

        private void txbCredencial_TextChanged(object sender, EventArgs e) {
            var tb = (TextBox)sender;

            int sel = tb.SelectionStart;
            string original = tb.Text;

            string limpio = new string(original.Where(char.IsDigit).ToArray());

            if (original != limpio) {
                // ¿Cuántos no-dígitos había a la izquierda del cursor?
                int removidosIzq = original.Take(sel).Count(c => !char.IsDigit(c));

                tb.Text = limpio;

                // Recoloca el cursor donde “debería” quedar
                tb.SelectionStart = Math.Max(sel - removidosIzq, 0);
            }
        }

        private void txbCredencial_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.V) {
                var clip = Clipboard.GetText();
                if (string.IsNullOrEmpty(clip) || !clip.All(char.IsDigit))
                    e.SuppressKeyPress = true; // cancela el pegado
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

    }
}
