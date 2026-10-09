using Apps_Captura.Views;
using FrmComun.CapturaCentralizada;
using FrmComun.Login;
using FrmComun.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SQLSIVEV.Comun;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static SQLSIVEV.Infrastructure.Config.AppConfig;
using Microsoft.Extensions.Configuration;
using SQLSIVEV.Comun.Configuracion;
using System.IO;


namespace Apps_Captura.Frm {
    public partial class Home : Form {
        private BarraLateral _barraLateral;
        private readonly CryptoHelper32 conf;
        private Regedit regedit;
        private readonly CapturaRegistroWindows _capturaRegistroWindows;
        private DateTime _ultimaActividad = DateTime.Now;
        private ActividadAplicacionFilter? _filtroActividad;
        private readonly System.Windows.Forms.Timer _tmrInactividad = new();
        private string _vistaActiva = "Home";
        private static readonly TimeSpan _tiempoInactividad =  TimeSpan.FromMinutes(5);

        private readonly SivevConnectionFactory _sql;
        private readonly AplicacionLifecycleService _cicloVida;
        private bool _cerrando;
        private ucRegistroVehicular? _registroVehicular;
        private bool _cancelandoCaptura;
        public event Action? CancelacionSolicitada;




        private readonly VerificentroApiConfig _verificentroApiConfig;
        public Home() {
            InitializeComponent();
            #region Configuración de la barra lateral y regedit
            _barraLateral = new BarraLateral(
                flpVistasAbiertas,
                splitPrincipal.Panel2
            );

            conf = new CryptoHelper32 {
                Password = "1Mx;7m47>T((=1Wh+65W;xS(53uNS{",
                SaltText = "S4&8YSv6E7ONR*8l",
                Iterations = 100_001,
                KeySizeBits = 256,
                WinRarConfRegistryPath = @"SOFTWARE\WinRAR\Capabilities"
            };
            regedit = new Regedit("CAPTURA", conf);
            _capturaRegistroWindows = LeerConfiguracion();
            ConfigurarInactividad();

            _barraLateral.CrearCabecera(
            _capturaRegistroWindows.dvar18.ToString(), "CAPTURA", () => {
                _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
            });

            _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
            #endregion

            #region Configuración de SQL
            SqlConnectionStringBuilder csb = new() {
                DataSource = _capturaRegistroWindows.dvar1,
                InitialCatalog =  _capturaRegistroWindows.dvar2,

                // Autenticación SQL
                UserID = _capturaRegistroWindows.dvar3,
                Password = _capturaRegistroWindows.dvar4,
                IntegratedSecurity = false,

                // TLS
                Encrypt = false,
                TrustServerCertificate = true,
                PersistSecurityInfo = false,

                // Identificación de la aplicación en SQL Server
                ApplicationName = _capturaRegistroWindows.dvar5,

                // Pooling
                Pooling = true,
                MinPoolSize = 0,
                MaxPoolSize = 100,
                ConnectTimeout = 15,
                LoadBalanceTimeout = 0,

                MultipleActiveResultSets = false
            };
            _sql = new SivevConnectionFactory(
                csb.ConnectionString
            );
            #endregion

            #region Variables de configuración del centro
            _cicloVida = new AplicacionLifecycleService(
                _sql,
                _capturaRegistroWindows.dvar6,
                _capturaRegistroWindows.dvar7.ToString().ToUpper(),
                _capturaRegistroWindows.dvar15,
                _capturaRegistroWindows.dvar12,
                _capturaRegistroWindows.dvar8,
                SivevOrigen.Captura
            );

            _cicloVida.AppRoleObtenido += (password, role) => {
                _capturaRegistroWindows.dvar16 = password;
                _capturaRegistroWindows.dvar17 = role;
            };
            #endregion

            #region Variables de configuración del servicio de Verificentro
            var configuration = new ConfigurationBuilder().AddJsonFile(
                Path.Combine(AppContext.BaseDirectory, "appsettings.json"),
                optional: false,
                reloadOnChange: true)
            .Build();

            var section = configuration.GetSection("VerificentroApi");

            _verificentroApiConfig = new VerificentroApiConfig {
                DnsHost = section["DnsHost"] ?? string.Empty,
                FallbackIp = section["FallbackIp"] ?? string.Empty,
                Path = section["Path"] ?? string.Empty,
                Accept = section["Accept"] ?? "application/json",
                ApiKey = section["ApiKey"] ?? string.Empty
            };
            #endregion

            
        }
        #region bloqueo de pantalla por inactividad
        private void ConfigurarInactividad() {
            _ultimaActividad = DateTime.Now;
            _filtroActividad = new ActividadAplicacionFilter();
            _filtroActividad.ActividadDetectada += () => {
                _ultimaActividad = DateTime.Now;
            };
            Application.AddMessageFilter(_filtroActividad);
            _tmrInactividad.Interval = 10_000; // revisar cada 10 segundos
            _tmrInactividad.Tick += TmrInactividad_Tick;
            _tmrInactividad.Start();
        }
        private void TmrInactividad_Tick(object? sender, EventArgs e) {
            if (_vistaActiva == "Home")
                return;

            if (DateTime.Now - _ultimaActividad < _tiempoInactividad)
                return;

            _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
            _ultimaActividad = DateTime.Now;
        }
        #endregion
        #region Lectura de regedit 
        private CapturaRegistroWindows LeerConfiguracion() {
            CapturaRegistroWindows captura = new();
            try {
                captura = new CapturaRegistroWindows {

                    // Strings simples
                    dvar1 = regedit.LeerString("Server"),
                    dvar2 = regedit.LeerString("Database"),
                    dvar3 = regedit.LeerString("User"),
                    dvar4 = regedit.LeerString("Password"),
                    dvar5 = regedit.LeerString("AppName"),
                    dvar6 = regedit.LeerString("AppRole"),
                    dvar12 = regedit.LeerShort("CentroId"),
                    dvar7 = regedit.LeerGuid("AppRolePassword"),
                    dvar19 = regedit.LeerBool("Log"),
                    dvar8 = regedit.LeerShort("OpcionMenuId"),
                    dvar15 = regedit.LeerGuid("EstacionId"),
                    dvar10 = regedit.LeerString("UsuarioLinea"),
                    dvar11 = regedit.LeerString("Ip"),
                    dvar20 = regedit.LeerString("RutaEscaneos"),
                    dvar18 = regedit.LeerString("Centro")
                };

            } catch (Exception ex) {
                SivevLogger.Error($"Error al leer y desencriptar configuración desde el registro.\n{ex.Message}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error", $"Ocurrió un error al leer la configuración.\n\n{ex.Message}");
            }
            return captura;
        }
        #endregion

        #region ucAuth
        private async void msCaptura_Click(object sender, EventArgs e) {
            HabilitarPestañas();
            try {
                Guid accesoId = await IniciaCaptura();
                if (accesoId == Guid.Empty) {
                    SivevLogger.Information($"Acceso no valido: {accesoId}");
                    Mostrar.Mensaje($"Acceso no valido: {accesoId} o Acceso Cancelado");
                    _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
                    return;
                }
                SivevLogger.Information($"Se guarda con el accesoId: {accesoId}", SivevOrigen.Captura);
                _barraLateral.EliminarVista("Registro");


                registroVehicular();







            } catch (Exception ex) {
                SivevLogger.Error($"Error al iniciar captura: {ex}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error", $"No fue posible iniciar la captura.\n{ex.Message}");
            }             
        }
        private async Task<Guid> IniciaCaptura() {
            _capturaRegistroWindows.dvar21 = 0;
            _capturaRegistroWindows.dvar22 = Guid.Empty;

            var tcs = new TaskCompletionSource<Guid>(TaskCreationOptions.RunContinuationsAsynchronously);

            _barraLateral.MostrarVista("Registro", "Registro", () => {
                var auth = new ucAuth(
                    _sql,
                    _capturaRegistroWindows.dvar17,
                    _capturaRegistroWindows.dvar16.ToString().ToUpper(),
                    _capturaRegistroWindows.dvar8,
                    _capturaRegistroWindows.dvar15,
                    _capturaRegistroWindows.dvar12
                );

                auth.CredencialChanged += credencial => {
                    _capturaRegistroWindows.dvar21 = credencial;
                };

                auth.AccesoObtenido += accesoId => {
                    _capturaRegistroWindows.dvar22 = accesoId;
                    tcs.TrySetResult(accesoId);
                };
                return auth;
            },
                mostrarEnMenu: true
            );
            return await tcs.Task;
        }

        #endregion

        #region Registro Vehicular 
        private void registroVehicular() {
            string rutaBase = ObtenerRutaBaseEscaneo();

            _barraLateral.MostrarVista("RegistroVehicular", "RegistroVehicular", () => {
                 _registroVehicular  = new ucRegistroVehicular(
                    sql: _sql,
                    roll: _capturaRegistroWindows.dvar17,
                    passRoll: _capturaRegistroWindows.dvar16.ToString().ToUpper(),
                    opcionMenu: _capturaRegistroWindows.dvar8,
                    estacionId: _capturaRegistroWindows.dvar15,
                    accesoId: _capturaRegistroWindows.dvar22,
                    centro: _capturaRegistroWindows.dvar12,
                    ruta: rutaBase,
                    verificentroApiConfig: _verificentroApiConfig,
                    centroDomain: _capturaRegistroWindows.dvar18.ToUpper()
                );
                _registroVehicular.CancelacionSolicitada += async () => {
                    await CancelarCapturaAsync();
                };
                return _registroVehicular;
            });
        }

        private string ObtenerRutaBaseEscaneo() {
            const string rutaLocal = @"C:\SIVEV\ESCANER";

            //string? rutaConfigurada = _capturaRegistroWindows.dvar20?.Trim();
            string? rutaConfigurada = rutaLocal;

            if (!string.IsNullOrWhiteSpace(rutaConfigurada) &&  RutaDisponibleParaEscritura(rutaConfigurada)) {
                return rutaConfigurada;
            }

            // Fallback local
            Directory.CreateDirectory(rutaLocal);
            /*
            Mostrar.Mensaje("Ruta de escaneo",
                "No se encontró o no se tienen permisos sobre la ruta base de escaneo.\n\n" +
                $"Los documentos se guardarán temporalmente" +
                "Genere un ticket de soporte para corregir la ruta y eliminar este mensaje."
             );
            */
            SivevLogger.Warning(
                $"No fue posible utilizar la ruta de escaneo configurada: " +
                $"'{rutaConfigurada ?? "SIN CONFIGURAR"}'. " +
                $"Se utilizará la ruta local '{rutaLocal}'.",
                SivevOrigen.Captura);

            return rutaLocal;
        }
        private static bool RutaDisponibleParaEscritura(string ruta) {
            try {
                if (!Directory.Exists(ruta))
                    return false;

                string archivoPrueba = Path.Combine(ruta, $".sivev_test_{Guid.NewGuid():N}.tmp");
                using (File.Create(archivoPrueba)) { }
                File.Delete(archivoPrueba);
                return true;
            } catch {
                return false;
            }
        }
        #endregion

        #region Inicia y fin de captura
        private async void Home_Load(object sender, EventArgs e) {
            bool inicializado = await _cicloVida.InicializarAsync();

            if (!inicializado) {
                Mostrar.Mensaje("Error", "No fue posible inicializar la aplicación.");
                Application.Exit();
                return;
            }

            var r = await _cicloVida.IniciarAsync();
            if (r.MensajeId != 0) {
                Mostrar.Mensaje("ERROR AL ABRIR LA APLICACIÓN", $"{r.MensajeId}");
                Application.Exit();
                return;
            }
        }

        private async void Home_FormClosing(object sender, FormClosingEventArgs e) {
            if (_cerrando)
                return;

            e.Cancel = true;
            _cerrando = true;
            await _cicloVida.FinalizarAsync();
            Close();
        }
        #endregion


        #region Esc y alt F4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            if (keyData == Keys.Escape &&
                _capturaRegistroWindows.dvar22 != Guid.Empty) {

                if (!_cancelandoCaptura) {
                    BeginInvoke(new Action(async () => {
                        await CancelarCapturaAsync(confirmar: true);
                    }));
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void HabilitarPestañas(bool flag= false) {
            msCaptura.Enabled = flag;
        }


        private async Task CancelarCapturaAsync(bool confirmar = false) {
            if (_cancelandoCaptura)
                return;

            _cancelandoCaptura = true;

            try {
                if (confirmar) {
                    bool cancelar = Mostrar.Pregunta(
                        titulo: "Advertencia",
                        pregunta: "¿Está seguro de que desea cancelar la captura?",
                        textoAceptar: "Sí, cancelar",
                        textoNegativo: "No"
                    );

                    if (!cancelar)
                        return;
                }

                if (_registroVehicular == null) {
                    Mostrar.Mensaje("Error", "No se encontró la captura activa.");
                    return;
                }

                bool cancelado = await _registroVehicular.CancelarAccesoAsync();

                if (!cancelado)
                    return;

                _barraLateral.Limpiar();
                _registroVehicular = null;
                _capturaRegistroWindows.dvar21 = 0;
                _capturaRegistroWindows.dvar22 = Guid.Empty;

                HabilitarPestañas(true);

                _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);

            } catch (Exception ex) {
                SivevLogger.Error($"Error al cancelar captura: {ex}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error", ex.Message);
            } finally {
                _cancelandoCaptura = false;
            }
        }

        #endregion
    }
}

