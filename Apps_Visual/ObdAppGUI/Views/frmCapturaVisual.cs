using Apps_Visual.UI.Theme;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Sprache;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
//clbPrincipal

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmCapturaVisual : Form {

        #region Declaracion de variables :D
        private readonly Dictionary<short, CheckBox> _mapCvToCheckBox = new();
        private Size _formSizeInicial;
        private float _fontSizeInicial;
        private TaskCompletionSource<bool>? _tcsResultado;
        public VisualRegistroWindows _Visual;


        public string _placa = string.Empty;

        public event Action<bool> HabilitarPruebas;
        public event Action<bool> _checkOBD;
        public event Action<string> _placa2;
        public event Action<Guid> _verificacionId2;


        private Guid _verificacionId = Guid.Empty;
        public byte _protocoloVerificacíon;

        public int panelX = 450, panelY = 450;

        private byte tiTaponCombustible = 0, tiTaponAceite = 0, tiBayonetaAceite = 0, tiPortafiltroAire = 0,
             tiTuboEscape = 0, tiFugasMotorTrans = 0, tiNeumaticos = 0, tiComponentesEmisiones = 0,
             tiMotorGobernado = 0;

        private int _MensajeSQL = 0;
        public int odometro = 0;

        #endregion

        #region Constructores :D
        public frmCapturaVisual() {
            InitializeComponent();
            txbOdometro.TextChanged += TxbOdometro_TextChanged;
            txbOdometro.TextChanged += (s, ev) => SanitizeByRegex(txbOdometro, @"[^0-9]");
            txbOdometro.MaxLength = 7;

            //_formSizeInicial = this.Size;
            _fontSizeInicial = this.Font.Size;

            ConfigurarCheckBox(cbTaponCombustible, "Contiene Tapon de Combustible");
            ConfigurarCheckBox(cbTaponAceite, "Contiene Tapon de Aceite");
            ConfigurarCheckBox(cbBayonetaAceite, "Contiene Bayoneta de Aceite");
            ConfigurarCheckBox(cbPortaFiltroAire, "Contiene Porta filtro de Aire");
            ConfigurarCheckBox(cbTuboEscape, "Contiene Tubo de Escape");
            ConfigurarCheckBox(cbFugasMotorTrans, "Contiene Fugas de Motor o Transmisión");
            ConfigurarCheckBox(cbNeumaticos, "Contiene Neumáticos");
            ConfigurarCheckBox(cbComponentesEmisiones, "Contiene Componentes de Emisiones");
            ConfigurarCheckBox(cbMotorGobernado, "Contiene Motor de Gobernado");
            InicializarMapaCapturaVisual();
            txbOdometro.KeyDown += txbOdometro_KeyDown;
            ResetForm();

        }



        #endregion


        private async void btnSiguente_Click(object sender, EventArgs e) {
            QuizVisual();
        }

        private async void QuizVisual() {
            tiTaponCombustible = ValorCheckboxNegado(cbTaponCombustible);
            tiTaponAceite = ValorCheckboxNegado(cbTaponAceite);
            tiBayonetaAceite = ValorCheckboxNegado(cbBayonetaAceite);
            tiPortafiltroAire = ValorCheckboxNegado(cbPortaFiltroAire);
            tiTuboEscape = ValorCheckboxNegado(cbTuboEscape);
            tiFugasMotorTrans = ValorCheckboxNegado(cbFugasMotorTrans);
            tiNeumaticos = ValorCheckboxNegado(cbNeumaticos);
            tiComponentesEmisiones = ValorCheckboxNegado(cbComponentesEmisiones);
            tiMotorGobernado = ValorCheckboxNegado(cbMotorGobernado);



            if (int.TryParse(txbOdometro.Text, out int _odometro)
                   && _odometro > 0
                   && lblPlaca.Text != "PlacaID") {
                odometro = _odometro;
            }

            var r = await CapturaInspeccionVisual (
                        V:_Visual,
                        tiTaponCombustible:tiTaponCombustible,
                        tiTaponAceite: tiTaponAceite,
                        tiBayonetaAceite: tiBayonetaAceite,
                        tiPortafiltroAire: tiPortafiltroAire,
                        tiTuboEscape: tiTuboEscape,
                        tiFugasMotorTrans: tiFugasMotorTrans,
                        tiNeumaticos: tiNeumaticos,
                        tiComponentesEmisiones: tiComponentesEmisiones,
                        tiMotorGobernado:tiMotorGobernado,
                        odometro:odometro);
            if (r.MensajeId == 0) {
                await Task.Delay(500);

                _verificacionId2?.Invoke(_verificacionId);
                _placa2?.Invoke(_placa);
                _checkOBD?.Invoke(r.CheckObd);

                _tcsResultado?.TrySetResult(true);
            } else {
                _tcsResultado?.TrySetResult(false);
            }
        }


        #region Configuración Inicial :D

        #region Modificar los checbox conforme a la BDD
        private void InicializarMapaCapturaVisual() {
            _mapCvToCheckBox[2] = cbBayonetaAceite;
            _mapCvToCheckBox[11] = cbBayonetaAceite;
            _mapCvToCheckBox[18] = cbBayonetaAceite;
            _mapCvToCheckBox[26] = cbBayonetaAceite;

            _mapCvToCheckBox[8] = cbComponentesEmisiones;
            _mapCvToCheckBox[17] = cbComponentesEmisiones;
            _mapCvToCheckBox[24] = cbComponentesEmisiones;
            _mapCvToCheckBox[32] = cbComponentesEmisiones;


            // REVISAR EL 6
            _mapCvToCheckBox[6] = cbFugasMotorTrans;
            _mapCvToCheckBox[15] = cbFugasMotorTrans;
            _mapCvToCheckBox[22] = cbFugasMotorTrans;
            _mapCvToCheckBox[30] = cbFugasMotorTrans;

            _mapCvToCheckBox[4] = cbPortaFiltroAire;
            _mapCvToCheckBox[13] = cbPortaFiltroAire;
            _mapCvToCheckBox[20] = cbPortaFiltroAire;
            _mapCvToCheckBox[28] = cbPortaFiltroAire;

            _mapCvToCheckBox[5] = cbTuboEscape;
            _mapCvToCheckBox[9] = cbTuboEscape;
            _mapCvToCheckBox[14] = cbTuboEscape;
            _mapCvToCheckBox[21] = cbTuboEscape;
            _mapCvToCheckBox[29] = cbTuboEscape;

            _mapCvToCheckBox[1] = cbTaponCombustible;
            _mapCvToCheckBox[25] = cbTaponCombustible;

            _mapCvToCheckBox[3] = cbTaponAceite;
            _mapCvToCheckBox[12] = cbTaponAceite;
            _mapCvToCheckBox[27] = cbTaponAceite;

            _mapCvToCheckBox[10] = cbMotorGobernado;
            
            
            _mapCvToCheckBox[7] = cbNeumaticos;
            _mapCvToCheckBox[31] = cbNeumaticos;




        }
        #endregion

        #region formatear

        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }

        private async void ResetForm() {
            this.Resize += frmCapturaVisual_Resize;
            cbBayonetaAceite.Enabled = false;
            cbBayonetaAceite.Visible = false;
            cbBayonetaAceite.Checked = false;

            cbComponentesEmisiones.Enabled = false;
            cbComponentesEmisiones.Visible = false;
            cbComponentesEmisiones.Checked = false;

            cbFugasMotorTrans.Visible = false;
            cbFugasMotorTrans.Enabled = false;
            cbFugasMotorTrans.Checked = false;

            cbMotorGobernado.Enabled = false;
            cbMotorGobernado.Visible = false;
            cbMotorGobernado.Checked = false;

            cbNeumaticos.Enabled = false;
            cbNeumaticos.Visible = false;
            cbNeumaticos.Checked = false;

            cbPortaFiltroAire.Enabled = false;
            cbPortaFiltroAire.Visible = false;
            cbPortaFiltroAire.Checked = false;

            cbTaponAceite.Enabled = false;
            cbTaponAceite.Visible = false;
            cbTaponAceite.Checked = false;

            cbTaponCombustible.Enabled = false;
            cbTaponCombustible.Visible = false;
            cbTaponCombustible.Checked = false;

            cbTuboEscape.Enabled = false;
            cbTuboEscape.Visible = false;
            cbTuboEscape.Checked = false;

            btnSiguente.Enabled = false;

            txbOdometro.Enabled = false;
            txbOdometro.Visible = false;

            lblTitulo.Visible = false;
            lblOdometro.Visible = false;
            lblPlaca.Visible = false;

            if (panelX == 0 && panelY == 0) {
                pnlPrincipal.Size = new Size(Width, Height);
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * Width), 0);
            } else {
                pnlPrincipal.Size = new Size((int)Math.Ceiling(.98 * panelX), (int)Math.Ceiling(.95 * panelY));
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * panelX), 0);
            }
        }
        #endregion


        public async Task<bool> InicializarAsync() {
            SivevLogger.Information("Entra a InicializarAsync()");
            lblTitulo.Visible = true;
            lblTitulo.Text = "Buscando Verificaciones disponibles";
            bool IsSet(string s) => !string.IsNullOrWhiteSpace(s);

            if (IsSet(_Visual.Server) && IsSet(_Visual.Database) && IsSet(_Visual.User) &&
                IsSet(_Visual.Password) && IsSet(_Visual.AppName) && IsSet(_Visual.RollVisual) &&
                _Visual.RollVisualAcceso != Guid.Empty && _Visual.EstacionId != Guid.Empty &&
                _Visual.AccesoId != Guid.Empty) {

                var r = await GetAccesoSQLVerificaciones(V:_Visual);
                _MensajeSQL = r.MensajeId;

                if (_MensajeSQL == 0) {
                    await Task.Delay(500);
                    lblTitulo.Text = "Inspección Visual";
                    lblPlaca.Visible = true;
                    lblOdometro.Visible = true;
                    txbOdometro.Visible = true;
                    txbOdometro.Enabled = true;

                    lblPlaca.Text = r.PlacaId;
                    _placa = r.PlacaId;
                    _verificacionId = r.VerificacionId;
                    _Visual.VerificacionId = _verificacionId;
                    _protocoloVerificacíon = r.ProtocoloVerificacionId;

                    var r2 = await BanderasAEvaluar(V:_Visual,elemento: "DESCONOCIDO", combustible: 0 );
                    if (r2.MensajeId == 0) {
                        AplicarCapturaVisual(r2.Items);
                    }
                    return true;
                } else {
                    if (_MensajeSQL != 0) {
                        var repo = new SivevRepository();
                        CancellationToken ct = default;
                        using (var connApp = SqlConnectionFactory.Create(server: _Visual.Server, db: _Visual.Database, user: _Visual.User, pass: _Visual.Password, appName: _Visual.AppName)) {
                            await connApp.OpenAsync(ct);
                            using (var scope = new AppRoleScope(connApp, _Visual.RollVisual, _Visual.RollVisualAcceso.ToString().ToUpper())) {
                                var error = await repo.PrintIfMsgAsync(connApp, "GetAccesoSQLVerificaciones", _MensajeSQL);
                                var fin = await repo.SpAppAccesoFinAsync(connApp, _Visual.EstacionId,_Visual.AccesoId);
                                var bitacora = NuevaBitacora(_Visual, descripcion: $"{error.Mensaje}", codigoSql: _MensajeSQL);
                                await repo.SpSpAppBitacoraErroresSetAsync(V: _Visual, A: bitacora, ct: ct);
                                SivevLogger.Information($"Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.InicializarAsync.GetAccesoSQLVerificaciones, {error.Mensaje} se finaliza el acceso.");
                            }
                        }

                    }
                    HabilitarPruebas?.Invoke(false);
                    return false;
                }
            } else {
                SivevLogger.Information($"Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.InicializarAsync\n Hay algo que no se valido revisar los datos server: {_Visual.Server}, db: {_Visual.Database}, user: {_Visual.User}, pass: {_Visual.Password}, appName: {_Visual.AppName}, Acceso: {_Visual.AccesoId.ToString().ToUpper()}");
                MostrarMensaje($"Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.InicializarAsync\n Hay algo que no se valido revisar los datos server: {_Visual.Server}, db: {_Visual.Database}, user: {_Visual.User}, pass: {_Visual.Password}, appName: {_Visual.AppName}, Acceso: {_Visual.AccesoId.ToString().ToUpper()}");
                foreach (Control c in pnlPrincipal.Controls)
                    c.Dispose();
                pnlPrincipal.Controls.Clear();
                HabilitarPruebas?.Invoke(false);
                return false;
            }
        }




        #endregion

        #region SQL
        #region primer store 
        private async Task<VerificacionVisualIniResult> GetAccesoSQLVerificaciones(VisualRegistroWindows V, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            Guid _verificacion = Guid.Empty;
            byte _protocoloVerificacionId = (byte)0;

            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppVerificacionVisualIniAsync( conn:connApp, estacionId:V.EstacionId, accesoId:V.AccesoId);

                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;
                    _verificacion = rinicial.VerificacionId;
                    _protocoloVerificacionId = rinicial.ProtocoloVerificacionId;
                    _placa = rinicial.PlacaId;


                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"MensajeId: {_mensaje}", _mensaje);
                        var bitacora = NuevaBitacora(V, descripcion: $"{error.Mensaje}", codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: V, A: bitacora, ct: ct);
                        MostrarMensaje($"{error.Mensaje}");
                        return new VerificacionVisualIniResult {
                            MensajeId = _mensaje,
                            Resultado = _resultado,
                            VerificacionId = Guid.Empty,
                            ProtocoloVerificacionId = 0,
                            PlacaId = "DESCONOCIDO"
                        };
                    }
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Warning($"Falló la búsqueda de verificaciones en catch, GetAccesoSQLVerificaciones: {logEx.Message}");
                }
                MostrarMensaje($"{e.Message}");
                SivevLogger.Error($"Error en Get_Acceso_SQL_Verificaciones {e.Message}");
            }

            return new VerificacionVisualIniResult {
                MensajeId = 0,
                Resultado = 0,
                VerificacionId = _verificacion,
                ProtocoloVerificacionId = _protocoloVerificacionId,
                PlacaId = _placa
            };
        }

        #endregion

        #region Segundo store
        private async Task<CapturaVisualGetResult> BanderasAEvaluar(VisualRegistroWindows V, string? elemento, byte combustible, CancellationToken ct = default) {
            var repo = new SivevRepository();
            var result = new CapturaVisualGetResult();
            try {
                using var connApp = SqlConnectionFactory.Create( server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper())) {
                    var rbanderas = await repo.SpAppCapturaVisualGetAsync(conn: connApp, estacionId: V.EstacionId, accesoId: V.AccesoId, verificacionId: V.VerificacionId, elemento: elemento, tiCombustible: combustible);

                    //MostrarMensaje($"estacionId: {V.EstacionId}, accesoId: {V.AccesoId}, verificacionId: {V.VerificacionId}, elemento: {elemento}, tiCombustible: {combustible}");

                    result.Resultado = rbanderas.Resultado;
                    result.MensajeId = rbanderas.MensajeId;
                    result.ReturnCode = rbanderas.ReturnCode;
                    result.Items.AddRange(rbanderas.Items);

                    if (result.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync( connApp, $"Error en SpAppCapturaVisualGetAsync MensajeId {result.MensajeId}",result.MensajeId);
                        var msg = error?.Mensaje ?? "Mensaje no disponible";
                        var bitacora = NuevaBitacora(V, descripcion: $"{error.Mensaje}", codigoSql: result.MensajeId);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: V, A: bitacora, ct: ct);
                        MostrarMensaje($"Error SQL en SpAppCapturaVisualGetAsync MensajeId = {result.MensajeId}: {msg}");
                    }

                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Warning($"Falló la búsqueda de banderas en catch, BanderasAEvaluar: {logEx.Message}");
                }
                MostrarMensaje($"{e.Message}");
                SivevLogger.Error($"Error en Apps_Visual.ObdAppGUI.Views.Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.BanderasAEvaluar: {e.Message}");

                result.MensajeId = -2;
                result.Resultado = -2;
                result.ReturnCode = -2;
            }

            return result;
        }
        #endregion

        #region Tercer store
        private async Task<CapturaInspeccionVisualNewSetResult> CapturaInspeccionVisual(VisualRegistroWindows V, byte tiTaponCombustible, byte tiTaponAceite, byte tiBayonetaAceite, byte tiPortafiltroAire, byte tiTuboEscape, byte tiFugasMotorTrans, byte tiNeumaticos, byte tiComponentesEmisiones, byte tiMotorGobernado, int odometro, CancellationToken ct = default) {
            var repo = new SivevRepository();
            var result = new CapturaInspeccionVisualNewSetResult();

            try {
                using var connApp = SqlConnectionFactory.Create(server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName);
                await connApp.OpenAsync(ct);

                using (var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper())) {
                    var r = await repo.SpAppCapturaInspeccionVisualNewSetAsync(
                                                                                conn: connApp,
                                                                                verificacionId: V.VerificacionId,
                                                                                estacionId: V.EstacionId,
                                                                                accesoId: V.AccesoId,

                                                                                tiTaponCombustible:tiTaponCombustible,
                                                                                tiTaponAceite: tiTaponAceite,

                                                                                tiBayonetaAceite: tiBayonetaAceite,
                                                                                tiPortafiltroAire: tiPortafiltroAire,

                                                                                tiTuboEscape: tiTuboEscape,
                                                                                tiFugasMotorTrans: tiFugasMotorTrans,

                                                                                tiNeumaticos: tiNeumaticos,
                                                                                tiComponentesEmisiones: tiComponentesEmisiones,

                                                                                tiMotorGobernado:tiMotorGobernado,
                                                                                odometro:odometro );
                    result.Resultado = r.Resultado;
                    result.MensajeId = r.MensajeId;
                    result.ReturnCode = r.ReturnCode;
                    result.CheckObd = r.CheckObd;

                    if (result.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync( connApp, $"Error en CapturaInspeccionVisual MensajeId {result.MensajeId}",result.MensajeId);
                        var msg = error?.Mensaje ?? "Mensaje no disponible";
                        var bitacora = NuevaBitacora(V, descripcion: $"{error.Mensaje}", codigoSql: result.MensajeId);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: V, A: bitacora, ct: ct);
                        MostrarMensaje($"Error en CapturaInspeccionVisual MensajeId = {result.MensajeId}: {msg}");
                    }
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Warning($"Falló la búsqueda de banderas en catch, BanderasAEvaluar: {logEx.Message}");
                }
                MostrarMensaje($"Error en la conexion a la BDD Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.CapturaInspeccionVisual: {e.Message}");
                SivevLogger.Error($"Error en la conexion a la BDD Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.CapturaInspeccionVisual: {e.Message}");
            }

            return result;
        }
        #endregion
        #endregion

        #region utils

        #region Tamaño de la letra en AUTOMATICO
        private void frmCapturaVisual_Resize(object sender, EventArgs e) {
            float factor = (float)this.Width / _formSizeInicial.Width;
            ///*
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 60f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 20f));
            //*/


            lblTitulo.Font = new Font(
                lblTitulo.Font.FontFamily,
                Titulo1,
                lblTitulo.Font.Style
            );


            lblPlaca.Font = new Font(
                lblPlaca.Font.FontFamily,
                Titulo1,
                lblPlaca.Font.Style
            );

            cbBayonetaAceite.Font = new Font(
                cbBayonetaAceite.Font.FontFamily,
                Titulo3,
                cbBayonetaAceite.Font.Style
            );

            cbComponentesEmisiones.Font = new Font(
                cbComponentesEmisiones.Font.FontFamily,
                Titulo3,
                cbComponentesEmisiones.Font.Style
            );

            cbFugasMotorTrans.Font = new Font(
                cbFugasMotorTrans.Font.FontFamily,
                Titulo3,
                cbFugasMotorTrans.Font.Style
            );

            cbMotorGobernado.Font = new Font(
                cbMotorGobernado.Font.FontFamily,
                Titulo3,
                cbMotorGobernado.Font.Style
            );


            cbNeumaticos.Font = new Font(
                cbNeumaticos.Font.FontFamily,
                Titulo3,
                cbNeumaticos.Font.Style
            );

            cbPortaFiltroAire.Font = new Font(
                cbPortaFiltroAire.Font.FontFamily,
                Titulo3,
                cbPortaFiltroAire.Font.Style
            );

            cbTaponAceite.Font = new Font(
                cbTaponAceite.Font.FontFamily,
                Titulo3,
                cbTaponAceite.Font.Style
            );

            cbTuboEscape.Font = new Font(
                cbTuboEscape.Font.FontFamily,
                Titulo3,
                cbTuboEscape.Font.Style
            );

            cbTaponCombustible.Font = new Font(
                cbTaponCombustible.Font.FontFamily,
                Titulo3,
                cbTaponCombustible.Font.Style
            );

            btnSiguente.Font = new Font(
                btnSiguente.Font.FontFamily,
                Titulo3,
                btnSiguente.Font.Style
            );

            lblOdometro.Font = new Font(
                lblOdometro.Font.FontFamily,
                Titulo3,
                lblOdometro.Font.Style
            );

            txbOdometro.Font = new Font(
                txbOdometro.Font.FontFamily,
                Titulo2,
                txbOdometro.Font.Style
            );

        }
        public void InicializarTamanoYFuente() {
            if (panelX > 0 && panelY > 0) {
                this.Size = new Size(panelX, panelY);
            }
            _formSizeInicial = this.Size;
            _fontSizeInicial = lblTitulo.Font.Size;
        }



        private void txbOdometro_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true; // para que no haga beep ni brinque de línea

                if (int.TryParse(txbOdometro.Text, out int odometro)
                    && odometro > 0
                    && lblPlaca.Text != "PlacaID") {
                    btnSiguente.Enabled = true;
                    QuizVisual();
                } else {
                    btnSiguente.Enabled = false;
                }
            }
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
        #endregion
        private SpAppBitacoraErroresSet NuevaBitacora(VisualRegistroWindows V, string descripcion, int codigoSql = 0, int codigo = 0, [CallerMemberName] string callerMember = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0) {
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
        private void AplicarCapturaVisual(IReadOnlyList<CapturaVisualItem> items) {

            var allChecks = new[] {
                cbBayonetaAceite, cbComponentesEmisiones, cbFugasMotorTrans,
                cbNeumaticos, cbPortaFiltroAire, cbTuboEscape,
                cbTaponCombustible, cbTaponAceite, cbMotorGobernado
            };

            foreach (var chk in allChecks) {
                chk.Visible = false;
                chk.Enabled = false;
                chk.Checked = false;
            }

            foreach (var it in items) {
                if (!_mapCvToCheckBox.TryGetValue(it.CapturaVisualId, out var chk))
                    continue;

                chk.Text = "NO CONTIENE " + it.Elemento;
                chk.Visible = it.Despliegue;
                chk.Enabled = it.Despliegue;
                chk.Checked = !it.Despliegue;
            }
        }
        private byte ValorCheckboxNegado(CheckBox chk) {
            // Si no es visible, siempre es 0 (false)
            if (!chk.Visible)
                return 0;

            // Si es visible, invertimos el Checked:
            // Checked = true  → 0
            // Checked = false → 1
            return chk.Checked ? (byte)0 : (byte)1;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e) {
            var chk = (CheckBox)sender;
            var textoBase = chk.Tag.ToString();

            if (chk.Checked) {
                chk.Text = textoBase;
                chk.ForeColor = AppColors.AprobadoInspeccionVisual;
            } else {
                chk.Text = "NO " + textoBase;
                chk.ForeColor = AppColors.black;
            }
        }

        public void SetCallbacks(Action<string> placaCallback, Action<Guid> verificacionCallback, Action<bool> checkOBDCall) {
            _placa2 = placaCallback;
            _verificacionId2 = verificacionCallback;
            _checkOBD = checkOBDCall;
        }

        // El padre va a esperar esta Task
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }


        private void CheckBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                var chk = (CheckBox)sender;
                chk.Checked = !chk.Checked;
            }
        }


        private void ConfigurarCheckBox(CheckBox chk, string textoBase) {
            chk.Tag = textoBase;

            chk.Text = "NO " + textoBase;
            chk.ForeColor = AppColors.InstitucionalPrimario;

            chk.CheckedChanged += CheckBox_CheckedChanged;
            chk.KeyDown += CheckBox_KeyDown;
        }
        private void TxbOdometro_TextChanged(object sender, EventArgs e) {
            // Si NO está vacío, habilita; si está vacío, deshabilita
            btnSiguente.Enabled = !string.IsNullOrWhiteSpace(txbOdometro.Text);
        }

        private async void frmCapturaVisual_Load(object sender, EventArgs e) {
            //Close();
        }
        #endregion

        # region MostrarMensaje
        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }
        #endregion

    }
}