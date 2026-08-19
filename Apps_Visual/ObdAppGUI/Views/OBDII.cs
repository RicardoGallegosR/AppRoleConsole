using LiveChartsCore.SkiaSharpView;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Devices.Obd;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using Microsoft.Data.SqlClient;
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
            lblReporte.Text =
                "Localiza el conector de diagnóstico (DLC) del vehículo.\r\n\n" +
                "Conecta el dispositivo SBD al DLC.\r\n\n" +
                "Enciende el vehículo.\r\n\n" +
                "Presiona \"CONECTAR\"";
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

            if (_intentosConexion > MAX_INTENTOS) {
                btnConectar.Enabled = false;
                btnConectar.Text = "Sin intentos de conexión";
                lblLecturaOBD.Text = $"Se agotaron los {MAX_INTENTOS} intentos de conexión SBD.";
                var respuestaDefaulObd = new InspeccionObd2Set{
                    Intentos = _intentosConexion,
                    ConexionObd = false
                };
                await RSet(obd2: respuestaDefaulObd, visual: _Visual);
                return;
            }
            btnConectar.Enabled = false;
            btnConectar.Visible = false;

            try {
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
                R._VersionSoftware = _Visual.dvar29;
                //Mostrar.Mensaje($"Resultado de SBD: {R.PIDS_Sup_01_20} - Placa: {_Visual.dvar19}");

                /*
                    UDS configuracion PRUEBAS:  
                //*/
                
                if (R.ConexionObd && R.Intentos <= MAX_INTENTOS) {
                    lblLecturaOBD.Text = $"Conexión OBD exitosa - Placa: {_Visual.dvar19} - Intento: {R.Intentos}";
                    await RSet(obd2: R, visual: _Visual);

                }
                if (!R.ConexionObd && R.Intentos <= MAX_INTENTOS) {
                    lblLecturaOBD.Text = $"Conexión OBD fracasada - Placa: {_Visual.dvar19} - Intento: {R.Intentos}";
                    btnConectar.Enabled = true;
                    btnConectar.Visible = true;
                    btnConectar.Focus();

                    lblReporte.Text =
                "Localiza el conector de diagnóstico (DLC) del vehículo.\r\n\n" +
                "Conecta el dispositivo SBD al DLC.\r\n\n" +
                "Enciende el vehículo.\r\n\n" +
                "Presiona \"CONECTAR\"";
                }
            } catch (Exception ex) {
                lblLecturaOBD.Text = $"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}";
                SivevLogger.Error($"Error SBD (intento {_intentosConexion}/{MAX_INTENTOS}) de conexión: {ex.Message}");
            } finally {
                if (_intentosConexion >= MAX_INTENTOS && (R == null || !R.ConexionObd)) {
                    btnConectar.Enabled = false;
                    btnConectar.Visible = false;
                    btnConectar.Text = "Sin intentos";
                    lblLecturaOBD.Text = $"Se agotaron los {MAX_INTENTOS} intentos de conexión SBD. Desconecte el dispositivo";
                    var respuestaDefaulObd = new InspeccionObd2Set{
                        Intentos = _intentosConexion,
                        ConexionObd = false
                    };
                    await RSet(obd2: respuestaDefaulObd, visual: _Visual);
                }
                btnConectar.Focus();
                await Task.Delay(5000);
            }
        }

        /*
         11/08/2026 "Nuevo RSET con conection Pool"
         */
        private async Task RSet(InspeccionObd2Set obd2, VisualRegistroWindows visual, CancellationToken ct = default) {
            pbLecturaObd.Visible = false;
            var repo = new SivevRepository();
            await using var connApp = SqlConnectionFactory.Create( server: visual.dvar1, db: visual.dvar2, user: visual.dvar3, pass: visual.dvar4, appName: visual.dvar5);

            try {
                await connApp.OpenAsync(ct);
                using var scope = new AppRoleScope(connApp, role: visual.dvar17,  password: visual.dvar16.ToString().ToUpper());

                try {
                    // ─────────────────────────────────────
                    // REGISTRAR OBD
                    // ─────────────────────────────────────
                    
                    ResultadoSql resultado = await AccesoSqlObd2Set(connApp, repo, obd2, visual, ct);

                    // ─────────────────────────────────────
                    // LEER RESULTADOS
                    // ─────────────────────────────────────
                    await Task.Delay(100);
                    lblReporte.Text = "Agarrando señal ...";
                    var MesajeSQL = await repo.MensajeIdSQL(connApp, "RSet",  resultado.MensajeId, ct:ct);
                    var respuestaOBD = await repo.SpAppDatosVehiculoObdNewGetSetAsync(connApp, visual, ct);
                    
                    var resultadoOBDII = new ResultadoOBDII {
                        Titulo = $"{MesajeSQL.Mensaje}",
                        Marca = respuestaOBD.Marca,
                        SubMarca = respuestaOBD.SubMarca,
                        Modelo = respuestaOBD.Modelo,
                        DTCConfirmado = respuestaOBD.DTCConfirmado,
                        DTCPendiente = respuestaOBD.DTCPendiente,
                        Protocolo = respuestaOBD.Protocolo,
                        MensajeId = respuestaOBD.MensajeId,
                        Resultado = respuestaOBD.Resultado
                    };
                    //Mostrar.Mensaje("", $"Mensaje: {respuestaOBD.MensajeId}, Resultado: {respuestaOBD.Resultado}");
                    Mostrar.MensajesResultadoOBDII(this, resultadoOBDII);


                    _tcsResultado?.TrySetResult(true);
                    return;
                } catch(SqlException ex) {
                    foreach (SqlError error in ex.Errors) {
                        SivevLogger.Error(
                            $"SQL ERROR " +
                            $"Number={error.Number}, " +
                            $"Procedure={error.Procedure}, " +
                            $"Line={error.LineNumber}, " +
                            $"State={error.State}, " +
                            $"Class={error.Class}, " +
                            $"Message={error.Message}"
                        );
                    }
                    try {
                        var bitacora = Bitacora.ErroresSQL(visual, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                    } catch (Exception logEx) {
                        SivevLogger.Error($"Falló bitácora OBD placa {visual.dvar19}: {logEx.Message}");
                    }
                    Mostrar.Mensaje("Error en RSET", $"Falló en OBD placa {visual.dvar19}: {ex.Message}");
                    _tcsResultado?.TrySetResult(false); 
                }
                
                catch (Exception ex) {
                    Mostrar.Mensaje("Error en RSET", $"Falló en OBD placa {visual.dvar19}: {ex.Message}");
                    _tcsResultado?.TrySetResult(false);
                    
                }
            } catch (Exception ex) {
                SivevLogger.Error($"No fue posible establecer sesión SQL OBD: {ex}");
                Mostrar.Mensaje("Error de conexión SQL", ex.Message);
                _tcsResultado?.TrySetResult(false);
            }
        }
        #endregion
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }
        #region INSERTAR EN LA BDD

        private async Task<ResultadoSql> AccesoSqlObd2Set(SqlConnection connApp, SivevRepository repo, InspeccionObd2Set obd2, VisualRegistroWindows visual, CancellationToken ct = default) {
            //SivevLogger.Information("OBD SQL [1] Antes de SpAppCapturaInspeccionObd2SetAsync");
            var rinicial = await repo.SpAppCapturaInspeccionObd2SetAsync(conn:connApp, V:visual, obd:obd2, ct:ct);
            //SivevLogger.Information($"OBD SQL [2] Terminó SpAppCapturaInspeccionObd2SetAsync. " + $"MensajeId={rinicial.MensajeId}, Resultado={rinicial.Resultado}");
            int mensaje = rinicial.MensajeId;
            short resultado = rinicial.Resultado;
            if (mensaje != 0) {
                //SivevLogger.Information( $"OBD SQL [3] Antes de PrintIfMsgAsync. MensajeId={mensaje}");
                var error = await repo.MensajeIdSQL(connApp, $"MensajeId: {mensaje}", mensaje, ct: ct);
                var bitacora = Bitacora.ErroresSQL(visual, descripcion: error.Mensaje, codigoSql: mensaje);
                //SivevLogger.Information($"OBD SQL [4] Terminó PrintIfMsgAsync: {error.Mensaje}");
                await repo.SpSpAppBitacoraErroresSetAsyncPool(connApp:connApp, visual: visual, bitacora: bitacora, ct: ct);
            }
            //SivevLogger.Information($"OBD SQL [7] Regresando {mensaje}/{resultado}");
            return new ResultadoSql {
                MensajeId = mensaje,
                Resultado = resultado
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

        #region Graficos de RPM
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
        #endregion
        #region Graficos de Bateria
        private void CrearGaugeBateria() {
            pnlBateria.Controls.Clear();

            _agujaBateria = new NeedleVisual {
                Value = 0
            };

            _gaugeBateria = new PieChart {
                Dock = DockStyle.Fill,
                Series = GaugeGenerator.BuildAngularGaugeSections(new GaugeItem(11.8, serie => ConfigurarSeccionGauge(serie, new SKColor(220, 53, 69))),

                    new GaugeItem(1.0, serie => ConfigurarSeccionGauge(serie, new SKColor(255, 193, 7))),
                    new GaugeItem(2.0, serie => ConfigurarSeccionGauge(serie, new SKColor(48, 170, 75))),
                    new GaugeItem(3.2, serie => ConfigurarSeccionGauge(serie, new SKColor(255, 193, 7)))),
                    
                VisualElements = [new AngularTicksVisual {
                    Labeler = valor => valor.ToString("0.#"),
                    LabelsSize = 15,
                    LabelsOuterOffset = 8,
                    OuterOffset = 55,
                    TicksLength = 12
                }, 
                _agujaBateria],
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
        #endregion
    }
}
