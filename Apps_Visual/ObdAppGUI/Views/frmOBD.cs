using Microsoft.Data.SqlClient;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmOBD : Form {
        #region Declaracion de las variables
        private Size _formSizeInicial;
        private float _fontSizeInicial;
        public VisualRegistroWindows _Visual;

        private RBGR randy;
        private LecturasIniciales lecturasIniciales;
        private ObdMonitoresLuzMil obdMonitoresLuzMil;
        private ObdResultado ResultadoOBD;
        private TaskCompletionSource<bool>? _tcsResultado;

        #region Credenciales de la bdd

        public int _panelX = 0, _panelY = 0;

        #endregion

        #region Variables del store




        private ObdResultado o;
        #endregion

        #endregion
        public frmOBD() {
            _fontSizeInicial = this.Font.Size;

            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ResetForm();
            tlpPrincipal.Enabled = false;
            tlpPrincipal.Visible = false;
            btnFinalizarPruebaOBD.Enabled = false;
            this.Resize += frmCapturaVisual_Resize;
        }
        #region BOTON CONECTAR
        private async void btnConectar_Click(object sender, EventArgs e) {
            btnConectar.Text = "Conectando ...";
            lblLecturaOBD.Text = $"Leyendo Monitores de la placa: {_Visual.PlacaId}";
            tlpPrincipal.Enabled = false;
            tlpPrincipal.Visible = false;

            tlpMonitores.Enabled = false;
            tlpMonitores.Visible = false;

            btnFinalizarPruebaOBD.Enabled = false;
            await Task.Delay(500);
            randy = new RBGR();

            //LecturasIniciales a = randy.LecturasPrincipales();
            ResultadoOBD = randy.SpSetObd();
            //TOÑO



            #region Asignacion de Monitores
            lblCompletoComponent.Text = GetCompletoText(ResultadoOBD.Sc);
            lblDisponibleComponent.Text = GetDisponibleText(ResultadoOBD.Sc);

            lblCompletoMisfire.Text = GetCompletoText(ResultadoOBD.Sdciic);
            lblDisponibleMisfire.Text = GetDisponibleText(ResultadoOBD.Sdciic);

            lblCompletoFuelSystem.Text = GetCompletoText(ResultadoOBD.Secc);
            lblDisponibleFuelSystem.Text = GetDisponibleText(ResultadoOBD.Secc);

            lblCompletoCatalyst.Text = GetCompletoText(ResultadoOBD.Sso);
            lblDisponibleCatalyst.Text = GetDisponibleText(ResultadoOBD.Sso);

            lblCompletoHeatedCatalyst.Text = GetCompletoText(ResultadoOBD.Sci);
            lblDisponibleHeatedCatalyst.Text = GetDisponibleText(ResultadoOBD.Sci);

            lblCompletoEvaporativeSystem.Text = GetCompletoText(ResultadoOBD.Sccc);
            lblDisponibleEvaporativeSystem.Text = GetDisponibleText(ResultadoOBD.Sccc);

            lblCompletoSecondaryAirSystem.Text = GetCompletoText(ResultadoOBD.Se);
            lblDisponibleSecondaryAirSystem.Text = GetDisponibleText(ResultadoOBD.Se);

            lblCompletoAcRefrigerant.Text = GetCompletoText(ResultadoOBD.Ssa);
            lblDisponibleAcRefrigerant.Text = GetDisponibleText(ResultadoOBD.Ssa);

            lblCompletoEgerVvtSystem.Text = GetCompletoText(ResultadoOBD.Sfaa);
            lblDisponibleEgerVvtSystem.Text = GetDisponibleText(ResultadoOBD.Sfaa);

            lblCompletoOxygenSensor.Text = GetCompletoText(ResultadoOBD.Scso);
            lblDisponibleOxygenSensor.Text = GetDisponibleText(ResultadoOBD.Scso);

            lblCompletoOxygenSensorHeater.Text = GetCompletoText(ResultadoOBD.Srge);
            lblDisponibleOxygenSensorHeater.Text = GetDisponibleText(ResultadoOBD.Srge);


            // NUEVOS VALORES 
            lblrOdometroLuzMil.Text = $"{ResultadoOBD.distSinceClrKm} km";
            lblrRunTimeMil.Text = $"{ResultadoOBD.runTimeMilMin} min";
            /*
             public int? distMilKm { get; set; }
        public int? distSinceClrKm { get; set; }
        public int? runTimeMilMin { get; set; }
        public int? timeSinceClr { get; set; }
             */

            #endregion




            ///*
            // Valores Iniciales
            lblrVIN.Text = ResultadoOBD.VehiculoId;
            lblrProtocoloOBD.Text = ResultadoOBD.ProtocoloObd;
            lblrRPM.Text = ResultadoOBD.RpmOff.ToString();
            lblrCalId.Text = (ResultadoOBD.cal != null && ResultadoOBD.cal.Length > 0) ? string.Join(" || ", ResultadoOBD.cal) : "";
            lblrBateria.Text = ResultadoOBD.VoltsSwOff.ToString();

            // luz mil
            lblrLuzMil.Text = ResultadoOBD.Mil.ToString();
            //lblrOdometroLuzMil.Text = "Prueba 2";

            //dtc
            lblrModo3Lista.Text = ResultadoOBD.CodError;
            lblrModo7Lista.Text = ResultadoOBD.CodErrorPend;
            //lblrModoALista.Text = a.dtcList0A;


            // limpieza de dtc
            lblrOBDClear.Text = ResultadoOBD.distSinceClrKm.ToString(); // distMilKm
            //lblrRunTimeMil.Text = ResultadoOBD.runTimeMilMin.ToString();

            //CVN:
            lblrCalibrationVerificationNumber.Text = ResultadoOBD.cvn;
            //*/

            lblrOperacionMotor.Text = $"{ResultadoOBD.TiempoTotalSegundosOperacionMotor} seg";
            lblWarmsUp.Text = $"{ResultadoOBD.WarmUpsDesdeBorrado} Veces";







            //obdMonitoresLuzMil = randy.Monitores();

            btnConectar.Text = "Conectar";
            lblLecturaOBD.Text = $"Diagnótico OBD de la placa: {_Visual.PlacaId}";
            tlpPrincipal.Enabled = true;
            tlpPrincipal.Visible = true;

            tlpMonitores.Enabled = true;
            tlpMonitores.Visible = true;
            btnFinalizarPruebaOBD.Enabled = true;
        }
        #endregion


        #region ResetPanel
        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }

        private void ResetForm() {
            lblLecturaOBD.Text = $"Diagnostico OBD {_Visual.PlacaId}";
            lblrModoALista.Text = "";
            //lblrModo0ALista.Text = "";
            lblrModo7Lista.Text = "";
            //lblrFallas7.Text = "";
            lblrModo3Lista.Text = "";
            //lblrFallas3.Text = "";
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

        #region Primer store
        private async Task<CapturaInspeccionVisualNewSetResult> CapturaInspeccionVisual(
                                                                        string SERVER,
                                                                        string DB,
                                                                        string SQL_USER,
                                                                        string SQL_PASS,
                                                                        string appName,
                                                                        string APPROLE,
                                                                        string APPROLE_PASS,
                                                                        Guid estacionId,
                                                                        Guid AccesoId,
                                                                        Guid verificacionId,
                                                                        ObdResultado o
                                                                        ) {
            var repo = new SivevRepository();
            var result = new CapturaInspeccionVisualNewSetResult();

            try {
                using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                await connApp.OpenAsync();

                using (var scope = new AppRoleScope(connApp, APPROLE, APPROLE_PASS)) {
                    try {
                        var rObd = await repo.SpAppCapturaInspeccionObdSetAsync(
                        conn: connApp,
                        estacionId: estacionId,
                        accesoId: AccesoId,
                        verificacionId: verificacionId,

                        vehiculoId: o.VehiculoId,
                        tiConexionObd: o.ConexionOb,
                        protocoloObd: o.ProtocoloObd,
                        tiIntentos: o.Intentos,
                        tiMil: o.Mil,
                        siFallas: o.Fallas,
                        codError: o.CodError,
                        codErrorPend: o.CodErrorPend,
                        tiSdciic: o.Sdciic,
                        tiSecc: o.Secc,
                        tiSc: o.Sc,
                        tiSso: o.Sso,
                        tiSci: o.Sci,
                        tiSccc: o.Sccc,
                        tiSe: o.Se,
                        tiSsa: o.Ssa,
                        tiSfaa: o.Sfaa,
                        tiScso: o.Scso,
                        tiSrge: o.Srge,
                        voltsSwOff: o.VoltsSwOff,
                        voltsSwOn: o.VoltsSwOn,
                        rpmOff: o.RpmOff,
                        rpmOn: o.RpmOn,
                        rpmCheck: o.RpmCheck,
                        leeMonitores: o.LeeMonitores,
                        leeDtc: o.LeeDtc,
                        leeDtcPend: o.LeeDtcPend,
                        leeVin: o.LeeVin,
                        codigoProtocolo: 0
                        );

                        result.Resultado = rObd.Resultado;
                        result.MensajeId = rObd.MensajeId;


                        if (result.MensajeId != 0) {
                            var error = await repo.PrintIfMsgAsync( connApp, $"Error en CapturaInspeccionVisual MensajeId {result.MensajeId}",result.MensajeId);
                            var msg = error?.Mensaje ?? "Mensaje no disponible";
                            MostrarMensaje($"Error en CapturaInspeccionVisual MensajeId = {result.MensajeId}: {msg}");
                        }
                    } catch (Exception ex) {
                        MostrarMensaje($"Error de la BDD Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.CapturaInspeccionVisual: {ex.Message}");
                    }
                }
            } catch (Exception e) {
                MostrarMensaje($"Error en la conexion a la BDD Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.CapturaInspeccionVisual: {e.Message}");
            }

            return result;
        }
        #endregion

        #region Utils

        #region Tamaño de letra variable
        private void frmCapturaVisual_Resize(object sender, EventArgs e) {
            float factor = (float)this.Width / _formSizeInicial.Width;
            ///*
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 40f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 24f));
            float Titulo4 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 20f));
            //*/

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

        #endregion

        #region INSERTAR EN LA BDD
        private async void btnFinalizarPruebaOBD_Click(object sender, EventArgs e) {
            /*
            await CapturaInspeccionVisual(SERVER: _SERVER, DB: _DB, SQL_USER: _SQL_USER, SQL_PASS: _SQL_PASS,
                                    appName: _appName, APPROLE: _APPROLE, APPROLE_PASS: _APPROLE_PASS,
                                    estacionId: _estacionId, AccesoId: _accesoId, verificacionId: _verificacionId,
                                    o: ResultadoOBD);
            */
            _tcsResultado?.TrySetResult(true);

        }
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }
        #endregion

        private void frmOBD_Load(object sender, EventArgs e) {

        }

        private void lblModo3Lista_Click(object sender, EventArgs e) {

        }

        private void lblrModo7Lista_Click(object sender, EventArgs e) {

        }

        private void lblDTCClear_Click(object sender, EventArgs e) {

        }

        private void lblOBDClear_Click(object sender, EventArgs e) {

        }

        private void lblModo7Lista_Click(object sender, EventArgs e) {

        }

        private void lblModoALista_Click(object sender, EventArgs e) {

        }
    }
}
