using LiveChartsCore.SkiaSharpView;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using FrmComun.Utils;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WinForms;

using SkiaSharp;



namespace Apps_Visual.ObdAppGUI.Views {
    public partial class OBDII : UserControl {
        #region Declaracion de las variables
        private Size _formSizeInicial;
        private float _fontSizeInicial;
        public VisualRegistroWindows _Visual;

        private RBGR randy;
        private InspeccionObd2Set R;
        private TaskCompletionSource<bool>? _tcsResultado;
        private bool _leyendoObd = false;
        private int _Intentos = 0;

        private int Contador = 0;
        #endregion

        #region Credenciales de la bdd
        private int _intentosConexion = 0;
        private const int MAX_INTENTOS = 3;
        #endregion

        public OBDII(VisualRegistroWindows visual) {
            InitializeComponent();
            _Visual = visual ?? throw new ArgumentNullException(nameof(visual));
            InicializarGraficosObd();
            GraficosPanel(false);
        }
        #region Graficos
        private PieChart? _gaugeRpm;
        private PieChart? _gaugeBateria;
        private CartesianChart? _graficaTiempoReal;

        private NeedleVisual? _agujaRpm;
        private NeedleVisual? _agujaBateria;

        private readonly ObservableCollection<double> _valoresRpm = new();
        private readonly ObservableCollection<double> _valoresBateria = new();

        private const int MaxPuntosGrafica = 100;
        #endregion

        #region BTN Conectar
        private async void btnConectar_Click(object sender, EventArgs e) {
            R = null;
            
            tableLayoutPanel2.Visible = true;
            pnlResumen.Visible = true;
            lblResumen.Visible = false;

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

                await Task.Delay(300);

                if (_Visual.dvar26) {
                    var verificacionId = _Visual.dvar21.ToString().ToUpper();
                    var placa = _Visual.dvar19.ToString().ToUpper();
                    var centroServidor = _Visual.dvar12.ToString();
                    var archivo = Logs.CrearRutaLogObd(placa, verificacionId, centroServidor);
                    var logger = new ObdTxtLogger(archivo, verificacionId);
                    logger.EncabezadoSesion(verificacionId: verificacionId, placa: placa);
                    randy = new RBGR(logger);
                } else {
                    randy = new RBGR();
                }

                lblReporte.TextAlign = ContentAlignment.MiddleCenter;
                
                var progreso = new Progress<string>(msg => {lblReporte.Text = msg;});
                var porcentaje = new Progress<int>(p => { pbLecturaObd.Value = p; });
                var rpmProgress = new Progress<int>(rpm =>{ActualizarRpm(rpm);});
                var batteryProgress = new Progress<double>(volt =>{ActualizarBateria(volt); });

                R = await Task.Run(() => randy.SpSetObd(progreso, porcentaje, rpmProgress, batteryProgress));
                R.Intentos = _intentosConexion;

                /*
                    UDS configuracion PRUEBAS:  
                 
                //MostrarMensaje($"Configurando UDS {_Visual.dvar25}, contador: {coA}, cred: {_Visual.dvar18}, cred1: {_Visual.dvar27}, cred2: {_Visual.dvar28}");
                if (_Visual.dvar25 && coA == 3 && (_Visual.dvar18.Equals(_Visual.dvar27) || _Visual.dvar18.Equals(_Visual.dvar28))) {
                    R.CodigoError = "";
                    R.CodigoErrorPendiente = "";
                    R.Sdciic = 1;
                    R.Secc = 1;
                    R.Sc = 1;
                    R.Sso = 1;
                    R.Sci = 1;

                }
                //*/



                lblLecturaOBD.Text = R.ConexionObd
                        ? $"Conexión OBD exitosa - Placa: {_Visual.dvar19}"
                        : $"No se pudo conectar (intento {_intentosConexion}/{MAX_INTENTOS}) - Placa: {_Visual.dvar19}";

                //*
                if (R.ConexionObd && R.Intentos <= MAX_INTENTOS) {
                    RSet(OBD2_enviado: R, _Visual_: _Visual);
                }
                //*/
            } catch (Exception ex) {
                lblLecturaOBD.Text = $"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}";
                SivevLogger.Error($"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}");
            } finally {
                // Si ya se agotaron intentos, bloquea definitivamente el botón
                if (_intentosConexion >= MAX_INTENTOS && (R == null || !R.ConexionObd)) {
                    btnConectar.Enabled = false;
                    btnConectar.Visible = false;
                    btnConectar.Text = "Sin intentos";
                    lblLecturaOBD.Text = $"Se agotaron los {MAX_INTENTOS} intentos de conexión SBD. Desconecte el dispositivo";
                    var respuestaDefaulObd = new InspeccionObd2Set{
                        Intentos = _intentosConexion,
                        ConexionObd = false
                    };
                    RSet(OBD2_enviado: respuestaDefaulObd, _Visual_: _Visual);
                    //_tcsResultado?.TrySetResult(true);
                } 
                btnConectar.Focus();
                await Task.Delay(5000);
                GraficosPanel();
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

                        var RespuestaOBD = await repo.SpAppDatosVehiculoObdNewGetSetAsync(V: _Visual);

                        //await Task.Delay(5000);
                        ResultadoOBDII resultadoOBDII = new ResultadoOBDII{
                            Titulo = $"{error.Mensaje}",
                            Marca = RespuestaOBD.Marca,
                            SubMarca = RespuestaOBD.SubMarca,
                            Modelo = RespuestaOBD.Modelo,
                            DTCConfirmado = RespuestaOBD.DTCConfirmado,
                            DTCPendiente = RespuestaOBD.DTCPendiente,
                            Protocolo = RespuestaOBD.Protocolo
                        };
                        Mostrar.MensajesResultadoOBDII(this,resultadoOBDII);
                        await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId: _Visual.dvar15, _AccesoId: _Visual.dvar20);
                    }
                    _tcsResultado?.TrySetResult(false);
                } catch (Exception ex) {
                    try {
                        var bitacora = NuevaBitacora( _Visual, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                    } catch (Exception logEx) {
                        SivevLogger.Error($"Falló en OBD en catch de placa {_Visual.dvar19}, GetAccesoSQL: {logEx.Message}");
                    }
                    Mostrar.Mensaje($"Falló en OBD en catch de placa {_Visual.dvar19}: {ex.Message}");
                }

            }
            _tcsResultado?.TrySetResult(true);
        }
        #endregion
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }
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
                    ///await Task.Delay(5000);
                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;


                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"MensajeId: {_mensaje}", _mensaje);
                        var bitacora = NuevaBitacora(_Visual_, descripcion: $"{error.Mensaje}", codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync(V: _Visual_, A: bitacora, ct: ct);

                        //btnConectar.Visible = true;
                        //btnConectar.Enabled = true;
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
                Mostrar.Mensaje($"{e.Message}");
                SivevLogger.Error($"Error en Get_Acceso_SQL_Verificaciones {e.Message}");
            }

            return new ResultadoSql {
                MensajeId = 0,
                Resultado = 0
            };
        }


        #endregion

        #region Mensajes y bitacora
        /*
        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }
        */
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
        #endregion

        #region creacion de graficos 
        private void InicializarGraficosObd() {
            CrearGaugeRpm();
            CrearGaugeBateria();
            CrearGraficaTiempoReal();
        }

        private void GraficosPanel(bool flags = false) {
            pnlRPM.Visible = flags;
            pnlBateria.Visible = flags;
            pnlTiempoReal.Visible = flags;
            pnlResumen.Visible = flags;
        }


        private void CrearGaugeRpm() {
            pnlRPM.Controls.Clear();

            _agujaRpm = new NeedleVisual {
                Value = 0
            };

            _gaugeRpm = new PieChart {
                Dock = DockStyle.Fill,

                Series = GaugeGenerator.BuildAngularGaugeSections(
                    new GaugeItem(1500, serie => ConfigurarSeccionGauge(serie,new SKColor(76, 178, 74))),
                    new GaugeItem(1000, serie => ConfigurarSeccionGauge(serie,new SKColor(244, 178, 58))),
                    new GaugeItem(3000, serie => ConfigurarSeccionGauge(serie,new SKColor(235, 49, 53)))),

                VisualElements =[new AngularTicksVisual {
                    Labeler = valor => $"{valor / 1000:0.#}",
                    LabelsSize = 20,
                    LabelsOuterOffset = 8,
                    OuterOffset = 55,
                    TicksLength = 12
                },_agujaRpm],

                InitialRotation = -225,
                MaxAngle = 270,

                MinValue = 0,
                MaxValue = 4500,

                //LegendPosition = LegendPosition.Bottom,
                //BackColor = Color.White
            };
            pnlRPM.Controls.Add(_gaugeRpm);
            AgregarEtiquetaGauge(pnlRPM,"RPM","0",  out _lblValorRpm);
        }
        private static void ConfigurarSeccionGauge( PieSeries<ObservableValue> serie, SKColor color) {
            serie.OuterRadiusOffset = 60;
            serie.MaxRadialColumnWidth = 18;
            serie.CornerRadius = 0;

            serie.Fill = new SolidColorPaint(color);
        }
        private void CrearGaugeBateria() {
            pnlBateria.Controls.Clear();

            _agujaBateria = new NeedleVisual {
                Value = 0
            };

            _gaugeBateria = new PieChart {
                Dock = DockStyle.Fill,
                Series = GaugeGenerator.BuildAngularGaugeSections(new GaugeItem(11.8,
                        serie => ConfigurarSeccionGauge(serie, new SKColor(220, 53, 69))),

                    new GaugeItem(1.0, serie => ConfigurarSeccionGauge(serie, new SKColor(255, 193, 7))),
                    new GaugeItem(2.0, serie => ConfigurarSeccionGauge(serie, new SKColor(48, 170, 75))),
                    new GaugeItem(3.2, serie => ConfigurarSeccionGauge(serie, new SKColor(255, 193, 7)))),
                    
                VisualElements = [new AngularTicksVisual {
                    Labeler = valor => valor.ToString("0.#"),
                    LabelsSize = 15,
                    LabelsOuterOffset = 8,
                    OuterOffset = 55,
                    TicksLength = 12
                }, _agujaBateria],
                InitialRotation = -225,
                MaxAngle = 270,
                MinValue = 0,
                MaxValue = 18,
                LegendPosition = LegendPosition.Hidden,
                BackColor = Color.White
            };
            _gaugeBateria.BackColor = Color.White;

            pnlBateria.Controls.Add(_gaugeBateria);
            AgregarEtiquetaGauge( pnlBateria, "V", "0.0", out _lblValorBateria);
        }
        private static void AgregarEtiquetaGauge(Panel panel, string unidad, string valorInicial, out Label lblValor) {
            lblValor = new Label {
                Text = valorInicial,
                AutoSize = false,
                Width = 160,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 22F,FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 38, 48),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Bottom,
                Dock = DockStyle.Bottom
            };

            var lblUnidad = new Label {
                Text = unidad,
                AutoSize = false,
                Width = 160,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI",11F,FontStyle.Regular),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Bottom
            };

            void Reubicar() {
                int centroX = (panel.ClientSize.Width - panel.Width) / 2;
                panel.Location = new Point(centroX, panel.ClientSize.Height - 78);
                lblUnidad.Location = new Point(centroX, panel.ClientSize.Height - 38);
            }

            panel.Controls.Add(lblValor);
            panel.Controls.Add(lblUnidad);

            lblValor.BringToFront();
            lblUnidad.BringToFront();

            panel.Resize += (_, _) => Reubicar();

            Reubicar();
        }
        private void CrearGraficaTiempoReal() {
            pnlTiempoReal.Controls.Clear();

            var azul = new SKColor(20, 100, 230);
            var verde = new SKColor(45, 170, 70);
            var gris = new SKColor(100, 110, 120);
            var rejilla = new SKColor(225, 228, 232);

            _graficaTiempoReal = new CartesianChart {
                Dock = DockStyle.Fill,
                BackColor = Color.White,

                Series =[new LineSeries<double> {
                    Name = "RPM",
                    Values = _valoresRpm,
                    Stroke = new SolidColorPaint(azul, 3),
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    ScalesYAt = 0
                },

                new LineSeries<double> {
                    Name = "Voltaje",
                    Values = _valoresBateria,
                    Stroke = new SolidColorPaint(verde, 3),
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    ScalesYAt = 1
                }],

                XAxes = [new Axis {
                    Name = "Muestras",
                    LabelsPaint = new SolidColorPaint(gris),
                    SeparatorsPaint = new SolidColorPaint(rejilla),
                    MinStep = 10
                }],

                YAxes =[new Axis {
                    Name = "RPM",
                    MinLimit = 0,
                    MaxLimit = 4500,
                    LabelsPaint = new SolidColorPaint(azul),
                    NamePaint = new SolidColorPaint(azul),
                    SeparatorsPaint = new SolidColorPaint(rejilla),
                    Labeler = valor => valor.ToString("0")
                },

                new Axis {
                    Name = "Voltaje (V)",
                    MinLimit = 0,
                    MaxLimit = 18,
                    Position = AxisPosition.End,
                    LabelsPaint = new SolidColorPaint(verde),
                    NamePaint = new SolidColorPaint(verde),
                    ShowSeparatorLines = false,
                    Labeler = valor => valor.ToString("0.0")
                }],

                LegendPosition = LegendPosition.Top
            };
            pnlTiempoReal.Controls.Add(_graficaTiempoReal);
        }
        private void ActualizarRpm(int rpm) {
            if (_agujaRpm is null)
                return;

            rpm = Math.Clamp(rpm, 0, 4500);
            _agujaRpm.Value = rpm;
            if (_lblValorRpm is not null)
                _lblValorRpm.Text = rpm.ToString("N0");
            _valoresRpm.Add(rpm);
            RecortarColecciones();
        }
        private void ActualizarBateria(double voltaje) {
            if (_agujaBateria is null)
                return;
            if (double.IsNaN(voltaje) || double.IsInfinity(voltaje)) {
                return;
            }
            voltaje = Math.Clamp(voltaje, 0, 18);
            _agujaBateria.Value = voltaje;

            if (_lblValorBateria is not null)
                _lblValorBateria.Text = voltaje.ToString("0.0");
            _valoresBateria.Add(voltaje);
            RecortarColecciones();
        }
        private void RecortarColecciones() {
            while (_valoresRpm.Count > MaxPuntosGrafica)
                _valoresRpm.RemoveAt(0);

            while (_valoresBateria.Count > MaxPuntosGrafica)
                _valoresBateria.RemoveAt(0);
        }
        #endregion
    }
}
