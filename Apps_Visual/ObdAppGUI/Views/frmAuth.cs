using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmAuth : Form {
        public event Action<Guid> AccesoObtenido;
        public VisualRegistroWindows _Visual;      
        
        private Size _formSizeInicial;
        private float _fontSizeInicial;

        public int credencial = 0, panelX = 0, panelY = 0;
        public short opcionMenu = 0;

        public bool ExisteHuella;
        public byte[] Huella;


        public frmAuth() {
            InitializeComponent();
            txbCredencial.TextChanged += (s, ev) => SanitizeByRegex(txbCredencial, @"[^0-9]");
            txbPassword.TextChanged += (s, ev) => SanitizeByRegex(txbPassword, @"[^a-zA-Z0-9]");
            txbCredencial.MaxLength = 6;
            txbPassword.MaxLength = 32;
            txbCredencial.Focus();

            txbCredencial.PreviewKeyDown += txbCredencial_PreviewKeyDown;
            txbCredencial.TextChanged += txbCredencial_TextChanged; 
            _fontSizeInicial = this.Font.Size;
            ResetForm();
        }


        private void btnAcceder_Click(object sender, EventArgs e) {
            ActivacionBotonAcceder();
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
        private async void txbCredencial_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.IsInputKey = true;
                credencial = validaCredencialNumerico(txbCredencial.Text);

                bool IsSet(string s) => !string.IsNullOrWhiteSpace(s);

                if (IsSet(_Visual.Server)       && IsSet(_Visual.Database)      && IsSet(_Visual.User)      && 
                    IsSet(_Visual.Password)     && IsSet(_Visual.AppName)       && IsSet(_Visual.AppRole)   && 
                    IsSet(_Visual.AppRolePassword.ToString())) {

                    var r = await CredencialExisteHuella(V:_Visual, credencial:credencial);

                    if (r.MensajeId == 0) {
                        lblCredencial.Enabled = false;
                        txbCredencial.Enabled = false;

                        ExisteHuella = r.ExisteHuella;
                        Huella = r.Huella;

                        if (!ExisteHuella) {
                            btnAcceder.Enabled = true;
                            btnAcceder.Visible = true;
                            txbPassword.Enabled = true;
                            txbPassword.Visible = true;
                            txbPassword.Focus();
                            lblPassword.Visible = true;

                        } else {
                            MostrarMensaje($"Se debe crear la libreria de huella");
                        }
                    } else {
                        txbCredencial.Text = string.Empty;
                        txbCredencial.Focus();
                    }
                } else {
                    SivevLogger.Information(($"Apps_Visual.ObdAppGUI.Views.txbCredencial_PreviewKeyDown\n server: {_Visual.Server}, db: {_Visual.Database}, user: {_Visual.User}, pass: {_Visual.Password}, appName: {_Visual.AppName}"));
                }
            }
        }

        private void txbCredencial_KeyDown(object sender, KeyEventArgs e) {
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


        private async void ActivacionBotonAcceder() {
            btnAcceder.Enabled = false;
            txbPassword.Enabled = false;
            lblPassword.Enabled = false;
            txbCredencial.Focus();

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
        }

        private async Task<AccesoIniciaResult>  GetAccesoSQL (VisualRegistroWindows V, int credencial, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            Guid _AccesoSql = Guid.Empty;
            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppAccesoIniciaAsync( conn:connApp, estacionId: V.EstacionId, opcionMenuId:V.OpcionMenuId,
                                                                    credencial:credencial,password:txbPassword.Text, huella:Huella);
                    _resultado = rinicial.ReturnCode;
                    _mensaje = rinicial.MensajeId;
                    _AccesoSql = rinicial.AccesoId;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"SpAppCredencialExisteHuella", _mensaje);
                        var bitacora = NuevaBitacora( V, descripcion: $"Credencial: {credencial}, {error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                        MostrarMensaje($"Credencial: {credencial} {error.Mensaje}");
                        ResetForm();
                    }
                 }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de credencial {credencial}, GetAccesoSQL: {logEx.Message}");
                }
                MostrarMensaje($"Error en GetAccesoSQL con la credencial {credencial}: {e.Message}");
            }
            return new AccesoIniciaResult {
                MensajeId = _mensaje,
                ReturnCode = _resultado,
                AccesoId = _AccesoSql
            };
        }


        private async Task<CredencialExisteHuellaResult> CredencialExisteHuella(VisualRegistroWindows V, int credencial, CancellationToken ct = default) {
            int mensaje = 100;
            string _msm = string.Empty;
            short resultado = 0;
            bool existeHuella = false;
            byte[] huella = Array.Empty<byte>();

            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName);
                await connApp.OpenAsync(ct);
                using var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper());

                var r = repo.SpAppCredencialExisteHuella(cnn: connApp, uiEstacionId: V.EstacionId, siOpcionMenuId: V.OpcionMenuId, iCredencial: credencial);

                resultado = r.Resultado;
                mensaje = r.MensajeId;
                existeHuella = r.ExisteHuella;
                huella = r.Huella;


                if (mensaje != 0) {
                    try {
                        var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppCredencialExisteHuella", mensaje);
                        _msm = error.Mensaje;
                        var bitacora = NuevaBitacora(V, descripcion: $"Credencial: {credencial}, {error.Mensaje}", codigoSql: mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: V, A: bitacora, ct: ct);
                    } catch (Exception logEx) {
                        _msm = logEx.Message;
                        SivevLogger.Error($"Falló la bitácora en CredencialExisteHuella: {logEx.Message}");
                    } finally {
                        MostrarMensaje($"Credencial: {credencial}, {_msm}");
                    }
                    
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de CredencialExisteHuella: {logEx}");
                }
                MostrarMensaje($"Error en SpAppCredencialExisteHuella con la credencial {credencial}: {e.Message}");
            }

            return new CredencialExisteHuellaResult {
                MensajeId = mensaje,
                Resultado = resultado,
                ExisteHuella = existeHuella,
                Huella = huella
            };
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
            this.Resize += frmAuth_Resize;
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



        private void txbPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (txbPassword.Text.Length < 4) {
                    MostrarMensaje("Debe tener mínimo 4 caracteres.");
                    return;
                }else {
                    ActivacionBotonAcceder();
                }
            }
        }

        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }

        private int validaCredencialNumerico(string strcredencial) {
            if (int.TryParse(strcredencial, out int credencial)) {
                return credencial;
            } else {
                MostrarMensaje($"Solo números en la credencial");
            }
            return 0;
        }

        #region Tamaño de la letra en AUTOMATICO
        private void frmAuth_Resize(object sender, EventArgs e) {
            float factor = (float)this.Width / _formSizeInicial.Width;
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 35f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 40f));
            
            lblTituloLogin.Font = new Font(
                lblTituloLogin.Font.FontFamily,
                Titulo1,
                lblTituloLogin.Font.Style
            );

            lblCredencial.Font = new Font(
                lblCredencial.Font.FontFamily,
                Titulo3,
                lblCredencial.Font.Style
            );
            lblPassword.Font = new Font(
                lblPassword.Font.FontFamily,
                Titulo3,
                lblPassword.Font.Style
            );
            txbCredencial.Font = new Font(
                txbCredencial.Font.FontFamily,
                Titulo3,
                txbCredencial.Font.Style
            );
            txbPassword.Font = new Font(
                txbPassword.Font.FontFamily,
                Titulo3,
                txbPassword.Font.Style
            );

            btnAcceder.Font = new Font(
                btnAcceder.Font.FontFamily,
                Titulo3,
                btnAcceder.Font.Style
            );

        }
        public void InicializarTamanoYFuente() {
            if (panelX > 0 && panelY > 0) {
                this.Size = new Size(panelX, panelY);
            }
            _formSizeInicial = this.Size;
            _fontSizeInicial = lblTituloLogin.Font.Size;
        }

        private SpAppBitacoraErroresSet NuevaBitacora( VisualRegistroWindows V, string descripcion, int codigoSql = 0, int codigo = 0, [CallerMemberName] string callerMember = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0) {
            return new SpAppBitacoraErroresSet {
                EstacionId = V.EstacionId,
                Centro = V.Centro,
                NombreCpu = Environment.MachineName,
                OpcionMenuId = V.OpcionMenuId,
                FechaError = DateTime.Now,
                Libreria = Path.GetFileName(callerFile),
                Clase = Path.GetFileNameWithoutExtension(callerFile),
                Metodo = callerMember,
                CodigoErrorSql = codigoSql,
                CodigoError = codigo,
                DescripcionError = descripcion,
                LineaCodigo = callerLine,
                LastDllError = 0,
                SourceError = "DESCONOCIDO"
            };
        }
        #endregion

    }
}
