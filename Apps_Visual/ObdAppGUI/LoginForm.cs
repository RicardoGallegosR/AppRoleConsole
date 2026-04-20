using Apps_Visual.ObdAppGUI.Views;
using Apps_Visual.UI.Theme;
using DPFP_SMA.Forms.Comun;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Devices;
using Serilog;
using Serilog.Exceptions;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static SQLSIVEV.Domain.Models.SpAppProgramOnResult;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

//using DPFP_SMA.Models;


namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        #region Variables para funcionamiento
        
        private DPFP_SMA.Models.VisualRegistroWindows Visual_48;
        private SQLSIVEV.Infrastructure.Services.VisualRegistroWindows Visual_Core;
        private readonly RegistroCrypto _reg = new();


        private string _placa = string.Empty;
        private Guid _verificacionId = Guid.Empty;

        private Guid _AccesoIdObtenido = Guid.Empty;
        private bool _RealizarPruebaOBD = false;
        
        private TaskCompletionSource<bool>? _tcsAcceso;

        private HomeView home;
        private frmAuth frmcredenciales;
        private VerificationForm frmverification;
        private frmCapturaVisual CapturaVisual;
        private frmOBD PruebaOBD;
        private bool _cerrandoAplicacion = false;
        private bool _bitacoraFinalizada = false;
        #endregion

        #region inicio
        public frmBASE() {

            InitializeComponent();
            btnInspecionVisual.Enabled = false;
            btnApagar.Enabled = false;

            this.Load += async (_, __) => {
                await Task.Delay(200);
                if (LecturaRegedit()) {
                    BloquearEstacionRegEdit();
                }else {
                    if (!await SpAppRollClaveGet()) {
                        BloquearEstacionSqlTest();
                    } else {
                        SivevLogger.Information("Se ha revisado el registro de windows y las configuraciones son correctas, problemas por aqui no son :)");
                        // Inicia Bitacora de Aplicaciones 

                        await IniciaBiatcoraAplicaciones(Visual_Core);


                        btnInspecionVisual.Focus();
                        pnlHome();
                    }
                }
            };
        }
        #endregion



        private async Task<SpAppProgramOnResult> IniciaBiatcoraAplicaciones(VisualRegistroWindows V, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            Guid _AccesoSql = Guid.Empty;
            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: V.dvar1, db: V.dvar2, user: V.dvar3, pass: V.dvar4, appName: V.dvar5);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: V.dvar17, password: V.dvar16.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppProgramOn(conn:connApp, estacionId: V.dvar15);
                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"frmLogin.IniciaBiatcoraAplicaciones", _mensaje);
                        var bitacora = NuevaBitacora( V, descripcion: $"{error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                        MostrarMensaje($"{error.Mensaje}");
                    }
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en IniciaBiatcoraAplicaciones: {logEx.Message}");
                }
                MostrarMensaje($"Error en IniciaBiatcoraAplicaciones: {e.Message}");
            }
            return new SpAppProgramOnResult {
                MensajeId = _mensaje,
                Resultado = _resultado,
            };
        }

        private async Task<SpAppProgramOffResult> FinalizaBiatcoraAplicaciones(VisualRegistroWindows V, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            Guid _AccesoSql = Guid.Empty;
            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create( server: V.dvar1, db: V.dvar2, user: V.dvar3, pass: V.dvar4, appName: V.dvar5);
                await connApp.OpenAsync(ct);
                using (var scope = new AppRoleScope(connApp, role: V.dvar17, password: V.dvar16.ToString().ToUpper())) {
                    var rinicial = await repo.SpAppProgramOff(conn:connApp, estacionId: V.dvar15);
                    _resultado = rinicial.Resultado;
                    _mensaje = rinicial.MensajeId;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"frmLogin.FinalizaBitacorasAplicaciones", _mensaje);
                        var bitacora = NuevaBitacora( V, descripcion: $"{error.Mensaje}", codigoSql: _mensaje, codigo: 0);
                        await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                        MostrarMensaje($"{error.Mensaje}");
                    }
                }
            } catch (Exception e) {
                try {
                    var bitacora = NuevaBitacora( V, descripcion: e.ToString(), codigoSql: 0, codigo: e.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(V, bitacora, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en FinalizaBiatcoraAplicaciones: {logEx.Message}");
                }
                MostrarMensaje($"Error en FinalizaBiatcoraAplicaciones: {e.Message}");
            }
            return new SpAppProgramOffResult {
                MensajeId = _mensaje,
                Resultado = _resultado,
            };
        }





        #region SpAppRollClaveGet
        private async Task< bool> SpAppRollClaveGet() {
            try {
                await using var conn = SqlConnectionFactory.Create( 
                    server:     Visual_Core.dvar1,
                    db:         Visual_Core.dvar2,
                    user:       Visual_Core.dvar3, 
                    pass:       Visual_Core.dvar4,
                    appName:    Visual_Core.dvar5
                );
                await conn.OpenAsync();

                using (var scope = new AppRoleScope(conn, Visual_Core.dvar6, Visual_Core.dvar7.ToString().ToUpper())) {
                    var repo = new SivevRepository();
                    var r = repo.SpAppRollClaveGet(conn);
                    var MensajesSQL = await  repo.PrintIfMsgAsync(conn, "Fallo en SpAppRollClaveGet", r.MensajeId);
                    if (r.MensajeId != 0) {
                        SivevLogger.Error($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}");
                        using (var dlg = new frmMensajes($"Error frmBASE.SpAppRollClaveGet en MensajeId {MensajesSQL.ToString()}")) {
                            dlg.StartPosition = FormStartPosition.CenterParent;
                            dlg.TopMost = true;
                            dlg.ShowDialog(this);
                        }
                        return false;
                    }
                    Visual_Core.dvar16 = Guid.Parse((r.ClaveAcceso ?? "").Reverse().ToArray());
                    Visual_Core.dvar17 = r.FuncionAplicacion;

                    SivevLogger.Information($"Valores regresados {Visual_Core.dvar17}");
                }



                return true;
            } catch (SqlException ex) {
                SivevLogger.Error($"Fallo de conexión a SQL {ex.Message}");
                using (var dlg = new frmMensajes($"Fallo de conexión a SQL {ex.Message}")) {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.TopMost = true;
                    dlg.ShowDialog(this);
                }
                return false;
            }
        }

        #region BloquearEstacionSqlTest
        private void BloquearEstacionSqlTest() {
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorSQLPanel;
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = true;
            btnApagar.Visible = true;
            btnApagar.Focus();
            btnInspecionVisual.ForeColor = AppColors.BloqueoPorSQLTexto;
            pnlLateralIzquierdoAbajo.BackColor = AppColors.BloqueoPorSQLPanel;
            btnApagar.ForeColor = AppColors.BloqueoPorSQLTexto;


            pnlSeparadorFooterGrimsonI.BackColor = AppColors.BloqueoPorSQLPanel;
            pnlSeparadorFooterGrimsonII.BackColor = AppColors.BloqueoPorSQLPanel;

            pnlSeparadorFooterAmarillo.BackColor = AppColors.BloqueoPorSqlPanelSecundario;

            lblCDMX.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblDGCA.ForeColor = AppColors.BloqueoPorSQLTexto;


            lblSEDEMAFooter.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblVerificaciónVehicularFoother.ForeColor = AppColors.BloqueoPorSQLTexto;
            lblGobiernoDeLa.ForeColor = AppColors.BloqueoPorSQLTexto;
        }
        #endregion

        #endregion

        #region Regedit
        private bool LecturaRegedit() {
            bool vacio(string s) => string.IsNullOrWhiteSpace(s);
            var lector = new GuardarWinRarConf();
            lector.CargarEnCryptoHelper();
            var conf = lector.GetConfig();
            CryptoHelper.Configurar(conf);
           
            Visual_Core = new VisualRegistroWindows{
                dvar1 = Leer("Server"),
                dvar2 = Leer("Database"),
                dvar3 = Leer("User"),
                dvar4 = Leer("Password"),
                dvar5 = Leer("AppName"),
                dvar6 = Leer("AppRole"),
                dvar7 = LeerGuid("AppRolePassword"),
                dvar8 = LeerShort("OpcionMenuId", 0),
                dvar9 = LeerBool("Relleno", false),
                dvar10 = Leer("UsuarioLinea"),
                dvar11 = Leer("Ip"),
                dvar12 = LeerShort("Centro", 0),
                dvar13 = Leer("ServidorVersionesControlador"),
                dvar14 = Leer("url"),
                dvar15 = LeerGuid("EstacionId"),
                dvar25 = Leer("v25")
            };

            //*
            SivevLogger.Information(
                $"|| Lectura de REGEDIT " +
                $"|| SERVER: {Visual_Core.dvar1}, " +
                $"|| DB: {Visual_Core.dvar2}, " +
                $"|| SQL_USER: {Visual_Core.dvar3}, " +
                $"|| APPNAME: {Visual_Core.dvar5}, " +
                $"|| APPROLE: {Visual_Core.dvar6}, " +
                $"|| RollAcceso: {Visual_Core.dvar7}, " +
                $"|| opcionMenu: {Visual_Core.dvar8}, " +
                $"|| estacionId: {Visual_Core.dvar15}, " +
                //$"|| v25: {Visual_Core.dvar25} " 
                ""
            );
            //MostrarMensaje($"{Visual_Core.dvar1}");
            //*/
            return vacio(Visual_Core.dvar1)
                 || vacio(Visual_Core.dvar2)
                 || vacio(Visual_Core.dvar3)
                 || vacio(Visual_Core.dvar4)
                 || vacio(Visual_Core.dvar5)
                 || vacio(Visual_Core.dvar6)
                 || vacio(Visual_Core.dvar15.ToString())
                 || Visual_Core.dvar8 <= 0;
        }

        private void BloquearEstacionRegEdit() {
            SivevLogger.Error("Estación no configurada");
            pnlLateralIzquierdoCentral.BackColor = AppColors.BloqueoPorRegEditPanel;
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = true;
            btnApagar.Visible = true;
            pnlLateralIzquierdoAbajo.BackColor = AppColors.BloqueoPorRegEditPanel;
            btnApagar.ForeColor = AppColors.BloqueoPorRegEditTexto;


            pnlSeparadorFooterGrimsonI.BackColor = AppColors.BloqueoPorRegEditTexto;
            pnlSeparadorFooterGrimsonII.BackColor = AppColors.BloqueoPorRegEditTexto;

            pnlSeparadorFooterAmarillo.BackColor = AppColors.BloqueoPorRegEditPanelSecundario;

            lblCDMX.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblDGCA.ForeColor = AppColors.BloqueoPorRegEditTexto;
            
            lblSEDEMAFooter.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblVerificaciónVehicularFoother.ForeColor = AppColors.BloqueoPorRegEditTexto;
            lblGobiernoDeLa.ForeColor = AppColors.BloqueoPorRegEditTexto;

            using (var dlg = new frmMensajes("Estación no configurada\n Contactar a Calidad del Aire")) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }



        }
        #endregion

        #region metodo home
        private void pnlHome() {
            SivevLogger.Information("pnlHome se accede para el inicio del panel de home");
            btnInspecionVisual.Enabled = true;
            btnInspecionVisual.Visible = true;
            btnInspecionVisual?.Select();
            btnInspecionVisual.Focus();

            btnApagar.Enabled = true;
            btnApagar.Visible = true;






            SivevLogger.Information("Se habilita btnInspecionVisual");

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();
            if (home == null || home.IsDisposed) {
                home = new HomeView();
            }
            home.panelX = pnlPanelCambios.Width;
            home.panelY = pnlPanelCambios.Height;
            home.InicializarTamanoYFuente();

            var p = home.GetPanel();
            p.Dock = DockStyle.Fill;
            pnlPanelCambios.Controls.Add(p);
        }
        #endregion

        #region Inicio de inspeccion visual
        private async void LoginForm_Load(object sender, EventArgs e) {
            bool matarExplorer = false; 
            if (matarExplorer) {
                foreach (var proc in Process.GetProcessesByName("explorer"))
                    proc.Kill();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            if (keyData == Keys.Escape && Visual_Core.dvar20 != Guid.Empty) {
                BeginInvoke(new Action(async () => {
                    var frmEscape = new frmEscape(visual: Visual_Core);
                    frmEscape.Show();
                    bool ok = await frmEscape.EsperarResultadoAsync();
                    if (!frmEscape.IsDisposed)
                        frmEscape.Close();
                    if (ok) {
                        pnlPanelCambios.Controls.Clear();
                        home = new HomeView();
                        home.panelX = pnlPanelCambios.Width;
                        home.panelY = pnlPanelCambios.Height;
                        home.InicializarTamanoYFuente();
                        pnlPanelCambios.Controls.Add(home.GetPanel());
                        pnlPanelCambios.Dock = DockStyle.Fill;

                        btnInspecionVisual.Enabled = true;
                        btnInspecionVisual.Visible = true;
                        btnInspecionVisual?.Select();
                        btnInspecionVisual.Focus();

                        btnApagar.Enabled = true;
                        btnApagar.Visible = true;

                        if (PruebaOBD != null && !PruebaOBD.IsDisposed) {
                            PruebaOBD.Parent?.Controls.Remove(PruebaOBD); // opcional pero recomendable si estaba embebido
                            PruebaOBD.Controls.Clear();
                            PruebaOBD.Dispose();
                        }
                        PruebaOBD = null;
                        if (CapturaVisual != null && !CapturaVisual.IsDisposed) {
                            CapturaVisual.Parent?.Controls.Remove(CapturaVisual);
                            CapturaVisual.Controls.Clear();
                            CapturaVisual.Dispose();
                        }
                        CapturaVisual = null;
                    }
                }));

                return true; // consumimos Escape
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private async void btnInspecionVisual_Click(object sender, EventArgs e){
            btnInspecionVisual.Enabled = false;
            btnInspecionVisual.Visible = false;
            btnApagar.Enabled = false;
            btnApagar.Visible = false;

            // Validamos Verificacion 
            bool accesoValido = await ValidaCredencial();


            if (!accesoValido) {
                SivevLogger.Information($"Acceso no valido: {accesoValido}");
                MostrarMensaje($"Acceso no valido: {accesoValido} o Acceso Cancelado");
                pnlHome();
                return;
            }
            //Visual_Core.dvar20 = accesoValido;
            SivevLogger.Information($"Se guarda con el accesoId: {Visual_Core.dvar20}");
            
                
            bool pruebaVisual = await ListadoVisual();
            
            if (!pruebaVisual) {
                btnApagar.Enabled = true;
                btnApagar.Visible = true;
                return;
            }
            

            await Task.Delay(200);

            bool PruebaOBD = await PruebaOBDPanel();
            if (!PruebaOBD) {
                SivevLogger.Information($"Se realizo la lectura de SBD de {Visual_Core.dvar19}: {!PruebaOBD}");
                return;
            }
        }


        #region Credenciales :D
        
        private async Task<bool> ValidaCredencial() {
            SivevLogger.Information("Inicializa ValidaCredencial()");
            _tcsAcceso = new TaskCompletionSource<bool>();

            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();

            pnlPanelCambios.Controls.Clear();

            if (frmcredenciales == null || frmcredenciales.IsDisposed) {
                frmcredenciales = new frmAuth();
                frmcredenciales.AccesoObtenido += Frmcredenciales_AccesoObtenido;
                /*
                frmcredenciales.SetCallbacks (credencial => {
                    if (int.TryParse(credencial, out var c)) {
                        frmcredenciales.credencial = c;
                    } else {
                        SivevLogger.Warning($"Credencial ingresada no es numérica: {credencial}");
                    }
                });
                */
            }
            frmcredenciales.panelX = pnlPanelCambios.Width;
            frmcredenciales.panelY = pnlPanelCambios.Height;
            frmcredenciales.InicializarTamanoYFuente();

            frmcredenciales._Visual = Visual_Core;
            pnlPanelCambios.Controls.Add(frmcredenciales.GetPanel());
            pnlPanelCambios.Select();


            pnlPanelCambios.BeginInvoke(new Action(() =>  {
                try {
                    frmcredenciales.txbCredencial?.Select();
                    frmcredenciales.txbCredencial?.Focus();
                    //Visual_Core.dvar18 = frmcredenciales.txbCredencial.Text;
                    SivevLogger.Information("Focus aplicado a txbCredencial");
                } catch (Exception ex) {
                    SivevLogger.Error($"No se pudo aplicar focus a txbCredencial {ex}");
                }
            }));

            bool ok = await _tcsAcceso.Task;
            return ok;
        }
        //*/
        #endregion

        #region Prueba Visual

        private async Task<bool> ListadoVisual() {
            _placa = string.Empty;
            _verificacionId = Guid.Empty;
            _RealizarPruebaOBD = false;

            SivevLogger.Information($"Entra a listado Visual con el Acceso: {Visual_Core.dvar20.ToString().ToUpper()}");
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();


            if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                CapturaVisual = new frmCapturaVisual();
                CapturaVisual.lblTitulo.Focus();
                SivevLogger.Information("Inicializa los SetCallbacks");
                CapturaVisual.SetCallbacks(
                    placa => { _placa = placa; },
                    verId => { _verificacionId = verId; },
                    pasaObd => { _RealizarPruebaOBD = pasaObd; }
                );
            }
            CapturaVisual.panelX = pnlPanelCambios.Width;
            CapturaVisual.panelY = pnlPanelCambios.Height;
            CapturaVisual.InicializarTamanoYFuente();
            CapturaVisual._Visual = Visual_Core;

            pnlPanelCambios.Controls.Add(CapturaVisual.GetPanel());
            bool VerificacionesDisponibles = await CapturaVisual.InicializarAsync();

            if (!VerificacionesDisponibles) {
                pnlPanelCambios.Controls.Clear();
                CapturaVisual.Dispose();
                CapturaVisual = null;
                if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                    home = new HomeView();
                    home.panelX = pnlPanelCambios.Width;
                    home.panelY = pnlPanelCambios.Height;
                    home.InicializarTamanoYFuente();
                    pnlPanelCambios.Controls.Add(home.GetPanel());
                    pnlPanelCambios.Dock = DockStyle.Fill;
                    btnInspecionVisual.Enabled = true;
                    btnInspecionVisual.Visible = true;
                    btnApagar.Enabled = true;
                    btnApagar.Visible = true;
                    btnInspecionVisual?.Select();
                    btnInspecionVisual.Focus();
                }
                return false;
            }
            pnlPanelCambios.BeginInvoke(new Action(() => {
                try {
                    CapturaVisual.lblTitulo?.Select();
                    CapturaVisual.lblTitulo?.Focus();
                    SivevLogger.Information("Focus aplicado a txbCredencial");
                } catch (Exception ex) {
                    SivevLogger.Error($"No se pudo aplicar focus a txbCredencial {ex}");
                }
            }));
            bool ok = await CapturaVisual.EsperarResultadoAsync();
            
           
            if (!_RealizarPruebaOBD) {
                pnlPanelCambios.Controls.Clear();
                CapturaVisual.Dispose();
                CapturaVisual = null;
                if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                    home = new HomeView();
                    home.panelX = pnlPanelCambios.Width;
                    home.panelY = pnlPanelCambios.Height;
                    home.InicializarTamanoYFuente();
                    pnlPanelCambios.Controls.Add(home.GetPanel());
                    pnlPanelCambios.Dock = DockStyle.Fill;
                    btnInspecionVisual.Enabled = true;
                    btnInspecionVisual.Visible = true;
                    btnInspecionVisual?.Select();
                    btnInspecionVisual.Focus();
                    Visual_Core.dvar19 = _placa;
                    Visual_Core.dvar21 = _verificacionId;
                    SivevLogger.Information($"No pasa a prueba OBD la placa {Visual_Core.dvar19}: {_RealizarPruebaOBD}, verificación: {Visual_Core.dvar21}");
                }
                return false;
            }
            return _RealizarPruebaOBD;
        }
        #endregion

        #region Prueba OBD



        private async Task<bool> PruebaOBDPanel() {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();
            if (PruebaOBD == null || PruebaOBD.IsDisposed) {
                PruebaOBD = new frmOBD(Visual_Core);
            }
            PruebaOBD._panelX = pnlPanelCambios.Width;
            PruebaOBD._panelY = pnlPanelCambios.Height;
            PruebaOBD.InicializarTamanoYFuente();
            PruebaOBD._Visual = Visual_Core;
           
            pnlPanelCambios.Controls.Add(PruebaOBD.GetPanel());

            pnlPanelCambios.BeginInvoke(new Action(() => {
                this.Activate();
                PruebaOBD.btnConectar.Visible = true;
                PruebaOBD.btnConectar.Enabled = true;
                if (!PruebaOBD.btnConectar.Focus())
                    SivevLogger.Warning("Focus() devolvió false en PruebaOBD.btnConectar");

                SivevLogger.Information($"CanFocus={PruebaOBD.btnConectar.CanFocus}");
            }));
            bool ok = await PruebaOBD.EsperarResultadoAsync();
            pnlPanelCambios.Controls.Clear();
            CapturaVisual.Dispose();
            CapturaVisual = null;
            if (CapturaVisual == null || CapturaVisual.IsDisposed) {
                home = new HomeView();
                home.panelX = pnlPanelCambios.Width;
                home.panelY = pnlPanelCambios.Height;
                home.InicializarTamanoYFuente();
                pnlPanelCambios.Controls.Add(home.GetPanel());
                pnlPanelCambios.Dock = DockStyle.Fill;
                
                btnInspecionVisual.Enabled = true;
                btnInspecionVisual.Visible = true;
                btnInspecionVisual?.Select();
                btnInspecionVisual.Focus();

                btnApagar.Enabled = true;
                btnApagar.Visible = true;

            }
            PruebaOBD.Controls.Clear();
            PruebaOBD.Dispose();
            PruebaOBD = null;
            if (ok) {
                return true;
            }else {
                return false;
            }
        }
        #endregion


        #region Apagar
        private async void btnApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(
                "¿Desea apagar la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
                await CerrarAplicacionAsync(apagarEquipo: true);
        }
        private async Task CerrarAplicacionAsync(bool apagarEquipo = false) {
            if (_cerrandoAplicacion) return;
            _cerrandoAplicacion = true;

            try {
                if (!_bitacoraFinalizada) {
                    await FinalizaBiatcoraAplicaciones(Visual_Core);
                    _bitacoraFinalizada = true;
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error al finalizar bitácora: {ex.Message}");
            }

            if (apagarEquipo)
                Process.Start("shutdown", "/s /t 0");
            else
                Application.Exit();
        }
        #endregion


        #region Utilerias
        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }

        private void Frmcredenciales_AccesoObtenido(Guid accesoObtenido) {
            bool ok = accesoObtenido != Guid.Empty;
            if (ok) {
                //_AccesoIdObtenido = accesoObtenido;
                Visual_Core.dvar20 = accesoObtenido;
                pnlPanelCambios.Controls.Clear();
                frmcredenciales.Dispose();
                frmcredenciales = null;
                //frmverification.Close();
                //frmverification=null;
            }
            _tcsAcceso?.TrySetResult(ok);
        }

        private string Leer(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;
            return CryptoHelper.Desencriptar(cifrado);
        }
        private Guid LeerGuid(string nombrePropiedad) {
            string plano = Leer(nombrePropiedad); // usa el método que desencripta
            if (string.IsNullOrWhiteSpace(plano))
                return Guid.Empty;

            return Guid.TryParse(plano, out var g) ? g : Guid.Empty;
        }
        private string LeerString(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;

            return CryptoHelper.Desencriptar(cifrado);
        }
        private short LeerShort(string nombrePropiedad, short defecto = 0) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return short.TryParse(plano, out var valor) ? valor : defecto;
        }

        private bool LeerBool(string nombrePropiedad, bool defecto = false) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return bool.TryParse(plano, out var valor) ? valor : defecto;
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

        #endregion
    }
}
