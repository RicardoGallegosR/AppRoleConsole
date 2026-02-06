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
        private InspeccionObd2Set ResultadoOBD;
        private TaskCompletionSource<bool>? _tcsResultado;
        private bool _leyendoObd = false;
        private int _Intentos = 0;

        #region Credenciales de la bdd

        public int _panelX = 0, _panelY = 0;
        public VisualRegistroWindows _Visual;
        bool conexionObd = false;
        #endregion


        #endregion

        //*                      PRODUCCION
        public frmOBD(VisualRegistroWindows visual) {
            _fontSizeInicial = this.Font.Size;
            InitializeComponent();
            _Visual = visual ?? throw new ArgumentNullException(nameof(visual));

            WindowState = FormWindowState.Maximized;
            tlpPrincipal.Enabled = false;
            tlpPrincipal.Visible = false;
            this.Resize += frmCapturaVisual_Resize;
            ResetForm();
        }
        //*/
        #region BOTON CONECTAR
        private async void btnConectar_Click(object sender, EventArgs e) {
            //_Intentos++;
            //conexionObd = false;
            btnConectar.Visible = false;
            if (_leyendoObd) {

                return;
            }
            _leyendoObd = true;
            try {

                lblLecturaOBD.Text = $"Iniciando placa: {_Visual.PlacaId}";
                tlpPrincipal.Enabled = false;
                tlpPrincipal.Visible = false;
                tlpMonitores.Enabled = false;
                tlpMonitores.Visible = false;

                lblLecturaOBD.Text = $"Leyendo Monitores de la placa: {_Visual.PlacaId}";

                await Task.Delay(500);
                lblLecturaOBD.Text = $"Leyendo OBD Monitores de la placa: {_Visual.PlacaId}";
                randy = new RBGR();
                ResultadoOBD = randy.SpSetObd();
                var defaultOBD = new InspeccionObd2Set();
                //if (defaultOBD.Equals(ResultadoOBD) && _Intentos < 3 && _Intentos >= 0 && !conexionObd) {
                    ResultadoOBD.Intentos = _Intentos;
                    ResultadoOBD.ConexionObd = 1;
                    ResultadoObdSet(OBD2: ResultadoOBD, _Visual_: _Visual, intento: 1, TrySetResult: true);
                    //conexionObd = true;
                //}
                /*
                if (3-_Intentos != 1 && !conexionObd) {
                    MostrarMensaje($"Advertencia personalId {_Visual.Credencial}.- Asegurate que se encuentre conectado el OBD. Te quedan {3 - _Intentos} intento.");
                } else {
                    MostrarMensaje($"Advertencia personalId {_Visual.Credencial}.- Asegurate que se encuentre conectado el OBD. Te queda {3 - _Intentos} intentos.");
                }
                if (3-_Intentos <= 0 && !conexionObd) {
                    ResultadoObdSet(OBD2: defaultOBD, _Visual_: _Visual, intento: _Intentos, TrySetResult: true);
                }//*/
            } finally {
                btnConectar.Enabled = true;
                btnConectar.Visible = true;
                _leyendoObd = false;

                btnConectar.Text = "Conectar";
                lblLecturaOBD.Text = $"Diagnóstico OBD de la placa: {_Visual.PlacaId}";
                tlpPrincipal.Enabled = false;
                tlpPrincipal.Visible = false;

                tlpMonitores.Enabled = false;
                tlpMonitores.Visible = false;
                btnConectar.Focus();
            }
        }

        private async void ResultadoObdSet(InspeccionObd2Set OBD2, VisualRegistroWindows _Visual_, int intento, bool TrySetResult) {
            lblLecturaOBD.Text = $"Registrando valores de la placa: {_Visual.PlacaId}";
            var repo = new SivevRepository();

            var Resultado = await AccesoSqlObd2Set(OBD2: ResultadoOBD, _Visual_: _Visual);

            int _mensaje = Resultado.MensajeId;

            if (_mensaje != 0) {
                try {
                    using var connApp = SqlConnectionFactory.Create( server: _Visual.Server, db: _Visual.Database, user: _Visual.User, pass: _Visual.Password, appName: _Visual.AppName);
                    await connApp.OpenAsync();
                    using (var scope = new AppRoleScope(connApp, role: _Visual.RollVisual, password: _Visual.RollVisualAcceso.ToString().ToUpper())) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"btnConectar_Click", _mensaje);
                        var bitacora = NuevaBitacora( _Visual, descripcion: $"Resultado de OBD: {error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                        MostrarMensaje($"Resultado de OBD: {error.Mensaje}");
                        await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId: _Visual.EstacionId, _AccesoId: _Visual.AccesoId);
                    }
                    _tcsResultado?.TrySetResult(false);
                } catch (Exception ex) {
                    try {
                        var bitacora = NuevaBitacora( _Visual, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                    } catch (Exception logEx) {
                        SivevLogger.Error($"Falló en OBD en catch de placa {_Visual.PlacaId}, GetAccesoSQL: {logEx.Message}");
                    }
                    MostrarMensaje($"Falló en OBD en catch de placa {_Visual.PlacaId}: {ex.Message}");
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
            //*
            btnConectar?.Select();
            btnConectar?.Focus();


            if (_Visual is null) {
                MostrarMensaje("Visual no inicializado");
                SivevLogger.Error("Visual no inicializado");
                return;
            }

            lblLecturaOBD.Text = $"Diagnostico OBD {_Visual.PlacaId}";
            //*/

            lblrModoALista.Text = "";
            lblrModo7Lista.Text = "";
            lblrModo3Lista.Text = "";
            lblrCalId.Text = "";
            lblrRPM.Text = "";
            lblrBateria.Text = "";
            lblrOdometroLuzMil.Text = "";
            lblrProtocoloOBD.Text = "";
            lblrLuzMil.Text = "";
            lblrVIN.Text = "";
            lblrOBDClear.Text = "";
            lblrCalibrationVerificationNumber.Text = "";
            lblrRunTimeMil.Text = "";
            lblrDTCClear.Text = "";
            tlpMonitores.Visible = false;

            lblBoostPressure.Visible = false;
            lblBoostPressure.Enabled = false;
            lblDisponibleBoostPressure.Visible = false;
            lblDisponibleBoostPressure.Enabled = false;
            lblCompletoBoostPressure.Visible = false;
            lblCompletoBoostPressure.Enabled = false;


            lblExhaustGasSensor.Visible = false;
            lblExhaustGasSensor.Enabled = false;
            lblDisponibleExhaustGasSensor.Visible = false;
            lblDisponibleExhaustGasSensor.Enabled = false;
            lblCompletoExhaustGasSensor.Visible = false;
            lblCompletoExhaustGasSensor.Enabled = false;



            lblNmhcCatalyst.Visible = false;
            lblNmhcCatalyst.Enabled = false;
            lblDisponibleNmhcCatalyst.Visible = false;
            lblDisponibleNmhcCatalyst.Enabled = false;
            lblCompletoNmhcCatalyst.Visible = false;
            lblCompletoNmhcCatalyst.Enabled = false;


            lblPmFilter.Visible = false;
            lblPmFilter.Enabled = false;
            lblDisponiblePmFilter.Visible = false;
            lblDisponiblePmFilter.Enabled = false;
            lblCompletoPmFilter.Visible = false;
            lblCompletoPmFilter.Enabled = false;


            lblNoxScrAftertreatment.Visible = false;
            lblNoxScrAftertreatment.Enabled = false;
            lblDisponibleNoxScrAftertreatment.Visible = false;
            lblDisponibleNoxScrAftertreatment.Enabled = false;
            lblCompletoNoxScrAftertreatment.Visible = false;
            lblCompletoNoxScrAftertreatment.Enabled = false;

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
            _fontSizeInicial = lblMonitorTitulo.Font.Size;
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
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }
        #endregion

        #region INSERTAR EN LA BDD
        private async Task<ResultadoSql> AccesoSqlObd2Set(InspeccionObd2Set OBD2, VisualRegistroWindows _Visual_, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;

            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: _Visual_.Server, db: _Visual_.Database, user: _Visual_.User, pass: _Visual_.Password, appName: _Visual_.AppName);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: _Visual_.RollVisual, password: _Visual_.RollVisualAcceso.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppCapturaInspeccionObd2SetAsync(conn:connApp, V:_Visual_, obd:OBD2);

                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;


                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"MensajeId: {_mensaje}", _mensaje);
                        var bitacora = NuevaBitacora(_Visual_, descripcion: $"{error.Mensaje}", codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: _Visual_, A: bitacora, ct: ct);
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
