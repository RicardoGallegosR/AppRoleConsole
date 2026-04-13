using AutoUpdaterDotNET;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Devices.Obd;
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
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Windows.UI.Composition;
//using System.Windows.Media;




namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmOBD : Form {
        #region Declaracion de las variables
        private Size _formSizeInicial;
        private float _fontSizeInicial;


        private RBGR randy;
        private InspeccionObd2Set R;
        private TaskCompletionSource<bool>? _tcsResultado;
        private bool _leyendoObd = false;
        private int _Intentos = 0;

        #region Credenciales de la bdd

        public int _panelX = 0, _panelY = 0;
        public VisualRegistroWindows _Visual;


        private int _intentosConexion = 0;
        private const int MAX_INTENTOS = 3;

        private int coQ = 0, coA = 0, coP = 0;
        private enum Modo { Mod0, Mod1, Mod2, Mod3 }
        private static Random _r = new Random();
        #endregion


        #endregion

        //*                      PRODUCCION
        public frmOBD(VisualRegistroWindows visual) {
            _fontSizeInicial = this.Font.Size;
            InitializeComponent();
            _Visual = visual ?? throw new ArgumentNullException(nameof(visual));

            WindowState = FormWindowState.Maximized;
            this.Resize += frmCapturaVisual_Resize;
            ResetForm();
            
        }
        //*/
        #region BOTON CONECTAR




        private async void btnConectar_Click(object sender, EventArgs e) {
            R = null;
            pbLecturaObd.Visible = true;

            pbLecturaObd.Minimum = 0;
            pbLecturaObd.Maximum = 100;
            pbLecturaObd.Value = 0;
            if (_leyendoObd)
                return;

            if (_intentosConexion >= MAX_INTENTOS) {
                btnConectar.Enabled = false;
                btnConectar.Text = "Sin intentos de conexión";
                lblLecturaOBD.Text = $"Se agotaron los {MAX_INTENTOS} intentos de conexión SBD.";
                var respuestaDefaulObd = new InspeccionObd2Set{
                    Intentos = _intentosConexion,
                    ConexionObd = false
                };
                RSet(OBD2_enviado: respuestaDefaulObd, _Visual_: _Visual);
                return;
            }
            btnConectar.Enabled = false;
            btnConectar.Visible = false;

            try {
                // Cuenta el intento al iniciar el proceso (así aunque falle, cuenta)
                _intentosConexion++;

                lblLecturaOBD.Text = $"Credencial {_Visual.dvar18} ha conectando SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión - Placa: {_Visual.dvar19}";

                await Task.Delay(500);

                // === Tu lógica de conexión/lectura ===
                randy = new RBGR();
                lblReporte.TextAlign = ContentAlignment.MiddleCenter;
                var progreso = new Progress<string>(msg => lblReporte.Text = msg);
                var porcentaje = new Progress<int>(p => { pbLecturaObd.Value = p; });

                R = await Task.Run(() => randy.SpSetObd(progreso, porcentaje));
                R.Intentos = _intentosConexion;

                lblLecturaOBD.Text = R.ConexionObd
                        ? $"Conexión OBD exitosa - Placa: {_Visual.dvar19}"
                        : $"No se pudo conectar (intento {_intentosConexion}/{MAX_INTENTOS}) - Placa: {_Visual.dvar19}";


                    RSet(OBD2_enviado: R, _Visual_: _Visual);
                
            } catch (Exception ex) {
                // Si falla, deja el intento contado y muestra mensaje
                lblLecturaOBD.Text = $"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}";
                SivevLogger.Error($"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}");
            } finally {
                // Si ya se agotaron intentos, bloquea definitivamente el botón
                if (_intentosConexion >= MAX_INTENTOS && !R.ConexionObd) {
                    btnConectar.Enabled = false;
                    btnConectar.Visible = true;
                    btnConectar.Text = "Sin intentos";
                    lblLecturaOBD.Text = $"Se agotaron los {MAX_INTENTOS} intentos de conexión SBD. Desconecte el dispositivo";
                    var respuestaDefaulObd = new InspeccionObd2Set{
                        Intentos = _intentosConexion,
                        ConexionObd = false
                    };
                    RSet(OBD2_enviado: respuestaDefaulObd, _Visual_: _Visual);
                    //_tcsResultado?.TrySetResult(true);
                } else {
                    // Si aún hay intentos o si ya conectó, restablece UI normal
                    btnConectar.Visible = true;
                    btnConectar.Enabled = true;
                    btnConectar.Text = "C O N E C T A R";

                    lblLecturaOBD.Text = $"Diagnóstico OBD de la placa: {_Visual.dvar19} intento {_intentosConexion}/{MAX_INTENTOS}";
                }

                btnConectar.Focus();
                //lblReporte.Text = "Conecte el escaner SBD en el vehículo.\r\nUna vez conectado presiona el botón conectar :D";
            }
        }

        private async void RSet(InspeccionObd2Set OBD2_enviado, VisualRegistroWindows _Visual_) {
            lblLecturaOBD.Text = $"Registrando valores de la placa: {_Visual.dvar19}";
            pbLecturaObd.Visible = false;
            var repo = new SivevRepository();
            var Resultado = await AccesoSqlObd2Set(OBD2: OBD2_enviado, _Visual_: _Visual);
            int _mensaje = Resultado.MensajeId;

            if (_mensaje != 0) {
                try {
                    using var connApp = SqlConnectionFactory.Create( server: _Visual.dvar1, db: _Visual.dvar2, user: _Visual.dvar3, pass: _Visual.dvar4, appName: _Visual.dvar5);
                    await connApp.OpenAsync();
                    using (var scope = new AppRoleScope(connApp, role: _Visual.dvar17, password: _Visual.dvar16.ToString().ToUpper())) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"btnConectar_Click", _mensaje);
                        var bitacora = NuevaBitacora( _Visual, descripcion: $"Resultado de SBD: {error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                        MostrarMensaje($"Resultado de SBD: {error.Mensaje}");
                        await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId:_Visual.dvar15, _AccesoId: _Visual.dvar20);
                    }
                    _tcsResultado?.TrySetResult(false);
                } catch (Exception ex) {
                    try {
                        var bitacora = NuevaBitacora( _Visual, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                    } catch (Exception logEx) {
                        SivevLogger.Error($"Falló en OBD en catch de placa {_Visual.dvar19}, GetAccesoSQL: {logEx.Message}");
                    }
                    MostrarMensaje($"Falló en OBD en catch de placa {_Visual.dvar19}: {ex.Message}");
                }

            }
            _tcsResultado?.TrySetResult(true);
        }
        #endregion


        #region ResetPanel
        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }

        private void ResetForm() {
            pbLecturaObd.Visible = false;
            _Visual.dvar22 = false;
            _Visual.dvar23 = false;
            _Visual.dvar24 = false;
            coQ = 0; coA = 0; coP = 0;
            if (_Visual is null) {
                MostrarMensaje("Visual no inicializado");
                SivevLogger.Error("Visual no inicializado");
                return;
            }

            lblLecturaOBD.Text = $"Diagnóstico SBD - Placa: {_Visual.dvar19} - Clave de Personal: {_Visual.dvar18}";
            lblReporte.TextAlign = ContentAlignment.TopLeft;
            lblReporte.Text =
                "• Localiza el conector de diagnóstico (DLC) del vehículo.\r\n\n" +
                "• Conecta el dispositivo SBD al DLC.\r\n\n" +
                "• Enciende el vehículo.\r\n\n" +
                "• Presiona \"CONECTAR\"";

            this.ActiveControl = pnlPrincipal;
            pnlPrincipal.Focus();
        }
        #endregion

        #region Utils

        #region Tamaño de letra variable
        private void frmCapturaVisual_Resize(object sender, EventArgs e) {
            
            float factor = (float)this.Width / _formSizeInicial.Width;
            
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 40f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 30f));
            float Titulo4 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 20f));
            

            lblLecturaOBD.Font = new Font(
                lblLecturaOBD.Font.FontFamily,
                Titulo3,
                lblLecturaOBD.Font.Style
            );
        }


        public void InicializarTamanoYFuente() {
            if (_panelX > 0 && _panelY > 0) {
                this.Size = new Size(_panelX, _panelY);
            }
            _formSizeInicial = this.Size;
        }
        #endregion


        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }
        private SpAppBitacoraErroresSet NuevaBitacora(VisualRegistroWindows V, string descripcion, int codigoSql = 0, int codigo = 0, [CallerMemberName] string callerMember = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0) {
            return new SpAppBitacoraErroresSet {
                EstacionId = V.dvar15,
                Centro = V.dvar12,
                NombreCpu = Environment.MachineName,
                OpcionMenuId = V.dvar8,
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
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }
        #endregion

        #region INSERTAR EN LA BDD
        private async Task<ResultadoSql> AccesoSqlObd2Set(InspeccionObd2Set OBD2, VisualRegistroWindows _Visual_, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            btnConectar.Visible = false;
            btnConectar.Enabled = false;
            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: _Visual_.dvar1, db: _Visual_.dvar2, user: _Visual_.dvar3, pass: _Visual_.dvar4, appName: _Visual_.dvar5);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: _Visual_.dvar17, password: _Visual_.dvar16.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppCapturaInspeccionObd2SetAsync(conn:connApp, V:_Visual_, obd:OBD2);

                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;


                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"MensajeId: {_mensaje}", _mensaje);
                        var bitacora = NuevaBitacora(_Visual_, descripcion: $"{error.Mensaje}", codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: _Visual_, A: bitacora, ct: ct);
                        btnConectar.Visible = true;
                        btnConectar.Enabled = true;
                        return new ResultadoSql {
                            MensajeId = _mensaje,
                            Resultado = _resultado

                        };
                    }
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( _Visual_, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(_Visual_, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Warning($"Falló la búsqueda de verificaciones en catch, GetAccesoSQLVerificaciones: {logEx.Message}");
                }
                MostrarMensaje($"{e.Message}");
                SivevLogger.Error($"Error en Get_Acceso_SQL_Verificaciones {e.Message}");
            }

            return new ResultadoSql {
                MensajeId = 0,
                Resultado = 0
            };
        }


        #endregion
        private void frmOBD_Load(object sender, EventArgs e) {

        }
    }
}
