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
//using System.Windows.Media;




namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmOBD : Form {
        #region Declaracion de las variables
        private Size _formSizeInicial;
        private float _fontSizeInicial;


        private RBGR randy;
        private RBGR randy2;
        private LecturasIniciales lecturasIniciales;
        private ObdMonitoresLuzMil obdMonitoresLuzMil;
        private InspeccionObd2Set ResultadoOBD;
        private TryHandshakeGet ResultadoTryHandshake;
        private TaskCompletionSource<bool>? _tcsResultado;
        private bool _leyendoObd = false;


        #region Credenciales de la bdd

        public int _panelX = 0, _panelY = 0;
        private VisualRegistroWindows _Visual ;

        #endregion


        #endregion
        public frmOBD() {
            _fontSizeInicial = this.Font.Size;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            tlpPrincipal.Enabled = false;
            tlpPrincipal.Visible = false;
            this.Resize += frmCapturaVisual_Resize;
            ResetForm();

            _Visual = new VisualRegistroWindows{
                PlacaId = "L71AAP",
                EstacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA"),
                Server = "SIVSRV9915",
                Database = "Sivev",
                User = "SivevCentros",
                Password = "CentrosSivev",
                AppName = "SivAppVfcVisual",
                RollVisual = "RollVfcVisual",
                RollVisualAcceso = Guid.Parse("95801B7A-4577-A5D0-952E-BD3D89757EA5"),
                VerificacionId = Guid.Parse("25996E89-CAC4-F011-80F0-D094661AE320"),
                AccesoId = Guid.Parse("2EEEE5A7-29DC-F011-811D-D09466400DBA")

            };
        }


        /*
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

                //await Task.Delay(500);
                //randy = new RBGR();


                lblLecturaOBD.Text = $"TryHandshake Monitores de la placa: {_Visual.PlacaId}";
                //ResultadoTryHandshake = randy.TryHandshake();

                await Task.Delay(500);
                //if (ResultadoTryHandshake.ConexionOb == 1/*ResultadoTryHandshake.Intentos < 4*/ /* && ResultadoTryHandshake.ConexionOb == 0 && ResultadoTryHandshake.VoltsSwOff != 0*/) {
                    lblLecturaOBD.Text = $"Leyendo OBD Monitores de la placa: {_Visual.PlacaId}";
                    await Task.Delay(500);
                    randy = new RBGR();
                    ResultadoOBD = randy.SpSetObd();
                //TOÑO

                #region Asignacion de Monitores
                //*

                    lblCompletoComponent.Text = GetCompletoText(ResultadoOBD.Sci);
                    lblDisponibleComponent.Text = GetDisponibleText(ResultadoOBD.Sci);

                    lblCompletoMisfire.Text = GetCompletoText(ResultadoOBD.Sdciic);
                    lblDisponibleMisfire.Text = GetDisponibleText(ResultadoOBD.Sdciic);

                    lblCompletoFuelSystem.Text = GetCompletoText(ResultadoOBD.Sc);
                    lblDisponibleFuelSystem.Text = GetDisponibleText(ResultadoOBD.Sc);

                    lblCompletoCatalyst.Text = GetCompletoText(ResultadoOBD.Secc);
                    lblDisponibleCatalyst.Text = GetDisponibleText(ResultadoOBD.Secc);

                    lblCompletoHeatedCatalyst.Text = GetCompletoText(ResultadoOBD.Sccc);
                    lblDisponibleHeatedCatalyst.Text = GetDisponibleText(ResultadoOBD.Sccc);

                    lblCompletoEvaporativeSystem.Text = GetCompletoText(ResultadoOBD.Se);
                    lblDisponibleEvaporativeSystem.Text = GetDisponibleText(ResultadoOBD.Se);

                    lblCompletoSecondaryAirSystem.Text = GetCompletoText(ResultadoOBD.Ssa);
                    lblDisponibleSecondaryAirSystem.Text = GetDisponibleText(ResultadoOBD.Ssa);

                    lblCompletoAcRefrigerant.Text = GetCompletoText(ResultadoOBD.Sfaa);
                    lblDisponibleAcRefrigerant.Text = GetDisponibleText(ResultadoOBD.Sfaa);

                    lblCompletoEgerVvtSystem.Text = GetCompletoText(ResultadoOBD.Srge);
                    lblDisponibleEgerVvtSystem.Text = GetDisponibleText(ResultadoOBD.Srge);

                    lblCompletoOxygenSensor.Text = GetCompletoText(ResultadoOBD.Sso);
                    lblDisponibleOxygenSensor.Text = GetDisponibleText(ResultadoOBD.Sso);

                    lblCompletoOxygenSensorHeater.Text = GetCompletoText(ResultadoOBD.Scso);
                    lblDisponibleOxygenSensorHeater.Text = GetDisponibleText(ResultadoOBD.Scso);
                
                    #endregion

                    #region labels
                    // NUEVOS VALORES 
                    lblrOdometroLuzMil.Text = $"{ResultadoOBD.Dist_MIL_On} km";
                    lblrRunTimeMil.Text = $"{ResultadoOBD.Tpo_Borrado_DTC} min";

                    
                    // Valores Iniciales
                    lblrVIN.Text = ResultadoOBD.VehiculoId;
                    lblrProtocoloOBD.Text = ResultadoOBD.ProtocoloObd;
                    lblrRPM.Text = ResultadoOBD.RpmOff.ToString();
                    lblrCalId.Text = ResultadoOBD.IDs_Adic;
                    lblrBateria.Text = ResultadoOBD.VoltsSwOff.ToString();
                    
                    // luz mil
                    lblrLuzMil.Text = ResultadoOBD.Mil.ToString();
                    //lblrOdometroLuzMil.Text = "Prueba 2";

                    //dtc
                    lblrModo3Lista.Text = ResultadoOBD.CodigoError;
                    lblrModo7Lista.Text = ResultadoOBD.CodigoErrorPendiente;
                    lblrModoALista.Text = ResultadoOBD.CodigoErrorPermanente;
                    //lblrModoALista.Text = a.dtcList0A;


                    // limpieza de dtc
                    lblrOBDClear.Text = ResultadoOBD.Tpo_Borrado_DTC.ToString(); // distMilKm
                                                                                 //lblrRunTimeMil.Text = ResultadoOBD.runTimeMilMin.ToString();

                    //CVN:
                    lblrCalibrationVerificationNumber.Text = ResultadoOBD.Lista_CVN;
                    
                    
                    lblrOperacionMotor.Text = $"{ResultadoOBD.Tpo_Arranque} seg";
                    //lblrWarmsUp.Text = $"{ResultadoOBD.WarmUpsDesdeBorrado} Veces";
                    lblrDTCClear.Text = $"{ResultadoOBD.Dist_Borrado_DTC}";

                    lblrNormativaObdVehiculo.Text = $"{ResultadoOBD.NEV}";
                    lblrIatCCoolantTempC.Text = $"{ResultadoOBD.TR} veces";
                    lblrStftB1.Text = $"{ResultadoOBD.STFT_B1} Veces";
                    lblrLTFT.Text = $"{ResultadoOBD.LTFT_B1} veces";

                    lblrIatC.Text = $"{ResultadoOBD.IAT} °C ";
                    lblrMafGs.Text = $"{ResultadoOBD.MAF}";

                   ///lblrMafKgH.Text = $"{ResultadoOBD.MafKgH}";
                    lblrTps.Text = $"{ResultadoOBD.TPS}";
                    lblrTimingAdvance.Text = $"{ResultadoOBD.AvanceEnc} ss";

                    lblrO2S1_V.Text = $"{ResultadoOBD.Volt_O2}";
                    //lblrO2S2_V.Text = $"{ResultadoOBD.O2S2_V}";

                    lblrFuelLevel.Text = $"{ResultadoOBD.NivelComb}";
                    lblrBarometricPressure.Text = $"{ResultadoOBD.Pres_Baro}";

                    //lblrFuelLevel.Text = $"{ResultadoOBD.FuelType}";
                    lblrIntFuelType.Text = $"{ResultadoOBD.Combustible0151Id}";

                    lblrIntTipoCombustible0907.Text = $"{ResultadoOBD.Combustible0907Id}";
                    lblrEcuAddress.Text = $"{ResultadoOBD.Dir_ECU}";
                    //lblrEcuAddressInt.Text = $"{ResultadoOBD.EcuAddressInt}";


                    lblrlblEmissionCode.Text = $"{ResultadoOBD.Req_Emisiones}";

                    lblrPids_01_20.Text = ResultadoOBD.PIDS_Sup_01_20;
                    lblrPids_21_40.Text = ResultadoOBD.PIDS_Sup_21_40;
                    lblrPids_41_60.Text = ResultadoOBD.PIDS_Sup_41_60;

                #endregion

                /*
                ResultadoOBD.Intentos = ResultadoTryHandshake.Intentos;
                ResultadoOBD.ProtocoloObd = ResultadoTryHandshake.ProtocoloObd;
                ResultadoOBD.ConexionObd = ResultadoTryHandshake.ConexionOb;
                ResultadoOBD.VoltsSwOff = ResultadoTryHandshake.VoltsSwOff;
                ResultadoOBD.RpmOff = ResultadoTryHandshake.RpmOff;
                */


                ResultadoOBD.Intentos = 1;
                ResultadoOBD.ConexionObd = 1;
                #endregion
                //MostrarMensaje($" ResultadoOBD.Intentos = {ResultadoTryHandshake.Intentos} ResultadoOBD.ProtocoloObd = {ResultadoTryHandshake.ProtocoloObd};  ResultadoOBD.ConexionObd = {ResultadoTryHandshake.ConexionOb};               ResultadoOBD.VoltsSwOff = {ResultadoTryHandshake.VoltsSwOff}, {ResultadoOBD.RpmOff}");


                //MostrarMensaje($" ResultadoOBD.RpmOff = {ResultadoOBD.RpmOff} ResultadoOBD.PIDS_Sup_01_20 = {ResultadoOBD.PIDS_Sup_01_20};");




                lblLecturaOBD.Text = $"Registrando valores de la placa: {_Visual.PlacaId}";
                    /*
                var Resultado = await AccesoSqlObd2Set(OBD2: ResultadoOBD, _Visual_: _Visual);

                    int _mensaje = Resultado.MensajeId;

                    if (_mensaje != 0) {
                        var repo = new SivevRepository();
                        try {
                            using var connApp = SqlConnectionFactory.Create( server: _Visual.Server, db: _Visual.Database, user: _Visual.User, pass: _Visual.Password, appName: _Visual.AppName);
                            await connApp.OpenAsync();
                            using (var scope = new AppRoleScope(connApp, role: _Visual.RollVisual, password: _Visual.RollVisualAcceso.ToString().ToUpper())) {
                                var error = await repo.PrintIfMsgAsync(connApp, $"btnConectar_Click", _mensaje);
                                var bitacora = NuevaBitacora( _Visual, descripcion: $"Resultado de OBD: {error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                                await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                                MostrarMensaje($"Resultado de OBD: {error.Mensaje}");
                            }
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
                    */
                /*} 
                 else {
                    var repo = new SivevRepository();
                    try {
                        var bitacora = NuevaBitacora( V:_Visual, descripcion: e.ToString(), codigoSql: 0, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                    } catch (Exception logEx) {
                        SivevLogger.Warning($"Falló la búsqueda de verificaciones en catch, GetAccesoSQLVerificaciones: {logEx.Message}");
                    }
                    MostrarMensaje($"No existe Conexion con OBD");
                    SivevLogger.Error($"Error No existe Conexion con OBD");
                }//*/

            } finally {
                btnConectar.Enabled = true;
                btnConectar.Visible = true;
                _leyendoObd = false;

                btnConectar.Text = "Conectar";
                lblLecturaOBD.Text = $"Diagnóstico OBD de la placa: {_Visual.PlacaId}";
                tlpPrincipal.Enabled = true;
                tlpPrincipal.Visible = true;

                tlpMonitores.Enabled = true;
                tlpMonitores.Visible = true;
            }
        }


        #region ResetPanel
        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }

        private void ResetForm() {
            /*
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
            /*
            float factor = (float)this.Width / _formSizeInicial.Width;
            
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 40f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 24f));
            float Titulo4 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 20f));
            

            lblLecturaOBD.Font = new Font(
                lblLecturaOBD.Font.FontFamily,
                Titulo1,
                lblLecturaOBD.Font.Style
            );


            pnlTopPrincipal.Font = new Font(
                pnlTopPrincipal.Font.FontFamily,
                Titulo1,
                pnlTopPrincipal.Font.Style
            );

            pnlFooterPrincipal.Font = new Font(
                pnlFooterPrincipal.Font.FontFamily,
                Titulo2,
                pnlFooterPrincipal.Font.Style
            );


            pnlPrincipal.Font = new Font(
                pnlPrincipal.Font.FontFamily,
                Titulo4,
                pnlPrincipal.Font.Style
            );

            lblMonitorTitulo.Font = new Font(
                lblMonitorTitulo.Font.FontFamily,
                Titulo4,
                lblMonitorTitulo.Font.Style
            );
            lblDisponiple.Font = new Font(
                lblDisponiple.Font.FontFamily,
                Titulo4,
                lblDisponiple.Font.Style
            );
            lblCompleto.Font = new Font(
                lblCompleto.Font.FontFamily,
                Titulo4,
                lblCompleto.Font.Style
            );

            splitContainer1.Font = new Font(
                splitContainer1.Font.FontFamily,
                Titulo4,
                splitContainer1.Font.Style
            );


            lblTitulo2.Font = new Font(
                lblTitulo2.Font.FontFamily,
                Titulo4,
                lblTitulo2.Font.Style
            );

            lblResultado.Font = new Font(
                lblResultado.Font.FontFamily,
                Titulo4,
                lblResultado.Font.Style
            );

            lblVin.Font = new Font(
                lblVin.Font.FontFamily,
                Titulo4,
                lblVin.Font.Style
            );

            lblProtocoloOBD.Font = new Font(
                lblProtocoloOBD.Font.FontFamily,
                Titulo4,
                lblProtocoloOBD.Font.Style
            );

            lblBateria.Font = new Font(
                lblBateria.Font.FontFamily,
                Titulo4,
                lblBateria.Font.Style
            );
            lblRMP.Font = new Font(
                lblRMP.Font.FontFamily,
                Titulo4,
                lblRMP.Font.Style
            );

            lblCalId.Font = new Font(
                lblCalId.Font.FontFamily,
                Titulo4,
                lblCalId.Font.Style
            );

            lblDTCClear.Font = new Font(
                lblDTCClear.Font.FontFamily,
                Titulo4,
                lblDTCClear.Font.Style
            );

            lblOBDClear.Font = new Font(
                lblOBDClear.Font.FontFamily,
                Titulo4,
                lblOBDClear.Font.Style
            );

            lblLuzMil.Font = new Font(
                lblLuzMil.Font.FontFamily,
                Titulo4,
                lblLuzMil.Font.Style
            );

            lblOdometroLuzMil.Font = new Font(
                lblOdometroLuzMil.Font.FontFamily,
                Titulo4,
                lblOdometroLuzMil.Font.Style
            );
            lblModo3Lista.Font = new Font(
                lblModo3Lista.Font.FontFamily,
                Titulo4,
                lblModo3Lista.Font.Style
            );

            lblModo7Lista.Font = new Font(
                lblModo7Lista.Font.FontFamily,
                Titulo4,
                lblModo7Lista.Font.Style
            );

            lblModoALista.Font = new Font(
                lblModoALista.Font.FontFamily,
                Titulo4,
                lblModoALista.Font.Style
            );
            lblCalibrationVerificationNumber.Font = new Font(
                lblCalibrationVerificationNumber.Font.FontFamily,
                Titulo4,
                lblCalibrationVerificationNumber.Font.Style
            );

            lblRunTimeMil.Font = new Font(
                lblRunTimeMil.Font.FontFamily,
                Titulo4,
                lblRunTimeMil.Font.Style
            );



            lblrVIN.Font = new Font(
                lblrVIN.Font.FontFamily,
                Titulo4,
                lblrVIN.Font.Style
            );

            lblrProtocoloOBD.Font = new Font(
                lblrProtocoloOBD.Font.FontFamily,
                Titulo4,
                lblrProtocoloOBD.Font.Style
            );

            lblrBateria.Font = new Font(
                lblrBateria.Font.FontFamily,
                Titulo4,
                lblrBateria.Font.Style
            );
            lblrRPM.Font = new Font(
                lblrRPM.Font.FontFamily,
                Titulo4,
                lblrRPM.Font.Style
            );

            lblrCalId.Font = new Font(
                lblrCalId.Font.FontFamily,
                Titulo4,
                lblrCalId.Font.Style
            );

            lblrDTCClear.Font = new Font(
                lblrDTCClear.Font.FontFamily,
                Titulo4,
                lblrDTCClear.Font.Style
            );

            lblrOBDClear.Font = new Font(
                lblrOBDClear.Font.FontFamily,
                Titulo4,
                lblrOBDClear.Font.Style
            );

            lblrLuzMil.Font = new Font(
                lblrLuzMil.Font.FontFamily,
                Titulo4,
                lblrLuzMil.Font.Style
            );

            lblrOdometroLuzMil.Font = new Font(
                lblrOdometroLuzMil.Font.FontFamily,
                Titulo4,
                lblrOdometroLuzMil.Font.Style
            );
            lblrModo3Lista.Font = new Font(
                lblrModo3Lista.Font.FontFamily,
                Titulo4,
                lblrModo3Lista.Font.Style
            );

            lblrModo7Lista.Font = new Font(
                lblrModo7Lista.Font.FontFamily,
                Titulo4,
                lblrModo7Lista.Font.Style
            );

            lblrModoALista.Font = new Font(
                lblrModoALista.Font.FontFamily,
                Titulo4,
                lblrModoALista.Font.Style
            );
            lblrCalibrationVerificationNumber.Font = new Font(
                lblrCalibrationVerificationNumber.Font.FontFamily,
                Titulo4,
                lblrCalibrationVerificationNumber.Font.Style
            );

            lblrRunTimeMil.Font = new Font(
                lblrRunTimeMil.Font.FontFamily,
                Titulo4,
                lblrRunTimeMil.Font.Style
            );

            */

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
        private (bool avail, bool comp) UnmapMonitorCode(byte? code) {
            return code switch {
                1 => (true, true),  // disponible, completo
                2 => (false, false),  // no disponible, completo
                3 => (true, false), // disponible, no completo
                _ => (false, false), // no disponible, no completo
            };
        }

        private string GetDisponibleText(byte? code) {
            var (avail, _) = UnmapMonitorCode(code);
            return avail ? "Sí" : "No";
        }

        private string GetCompletoText(byte? code) {
            var (_, comp) = UnmapMonitorCode(code);
            return comp ? "Sí" : "No";
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
        private async Task<InspeccionObdGet> AccesoSqlObd2Set(InspeccionObd2Set OBD2, VisualRegistroWindows _Visual_, CancellationToken ct = default) {
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
                        return new InspeccionObdGet {
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

            return new InspeccionObdGet {
                MensajeId = 0,
                Resultado = 0
            };
        }


        #endregion
        private void frmOBD_Load(object sender, EventArgs e) {

        }
    }
}
