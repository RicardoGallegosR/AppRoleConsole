using FrmComun.Utils;
using Microsoft.IdentityModel.Tokens;
using SQLSIVEV.Comun;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Utils;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Configuracion;
using Apps_Regedit.Services;

namespace Apps_Regedit.Views.Verificentros {
    public partial class Visual : UserControl {
        private Regedit regedit = new("VISUAL");
        private const string UrlVisual = "http://192.168.16.233/ClickOnce/Visual/Apps_Visual.application";
        public Visual() {
            InitializeComponent();
            pnlFooter.BringToFront();
            
            ucAcciones1.LeerClick += ucAcciones_LeerClick;
            ucAcciones1.GuardarClick += ucAcciones_GuardarClick;
            ucAcciones1.BuscarEstacionClick += ucAcciones_BuscarEstacionClick;
            ucAcciones1.BitacoraClick += ucAcciones_BitacoraClick;
            ucAcciones1.AutoLogonClick += ucAcciones_AutoLogonClick;
        }

        #region Eventos de botones
        #region AutoLogon
        private void ucAcciones_AutoLogonClick(object? sender, EventArgs e) {
            ConfigurarAutoLogon();
        }
        private void ConfigurarAutoLogon() {
            try {
                WindowsAutoLogon.Configurar(ucEstacion1.Usuario, ucEstacion1.PasswordUsuario);
                SivevLogger.Information($"AutoLogon configurado correctamente para {ucEstacion1.Usuario}.", SivevOrigen.Configurador);
                Mostrar.Mensaje("AutoLogon", "AutoLogon configurado correctamente.");
            } catch (Exception ex) {
                SivevLogger.Error($"Error al configurar AutoLogon: {ex.Message}", SivevOrigen.Configurador);
                Mostrar.Mensaje("Error", $"Ocurrió un error al configurar el AutoLogon.\n\n{ex.Message}");
            }
        }
        #endregion
        #region Leer Registro
        private void ucAcciones_LeerClick(object? sender, EventArgs e) {
            LeerConfiguracion();
        }
        private void LeerConfiguracion() {
            try {
                var visual = new VisualRegistroWindows {

                    // Strings simples
                    dvar1 = regedit.LeerString("Server"),
                    dvar2 = regedit.LeerString("Database"),
                    dvar3 = regedit.LeerString("User"),
                    dvar4 = regedit.LeerString("Password"),
                    dvar5 = regedit.LeerString("AppName"),
                    dvar6 = regedit.LeerString("AppRole"),
                    dvar12= regedit.LeerShort("Centro"),
                    dvar7 = regedit.LeerGuid("AppRolePassword"),
                    dvar26 = regedit.LeerBool("v26"),
                    dvar8 = regedit.LeerShort("OpcionMenuId"),
                    dvar15 = regedit.LeerGuid("EstacionId"),
                    dvar10 = regedit.LeerString("UsuarioLinea"),
                    dvar11 = regedit.LeerString("Ip")
                };
                CargarValoresFormulario(visual);
            } catch (Exception ex) {
                SivevLogger.Error($"Error al leer y desencriptar configuración desde el registro.\n{ex.Message}");
                Mostrar.Mensaje("Error", $"Ocurrió un error al leer la configuración.\n\n{ex.Message}");
            }
        }
        #region Cargar Valores en formulario 
        private void CargarValoresFormulario(VisualRegistroWindows visual) {
            ucDataBase1.Server = visual.dvar1;
            ucDataBase1.Database = visual.dvar2;
            ucDataBase1.User = visual.dvar3;
            ucDataBase1.Password = visual.dvar4;
            ucDataBase1.AppName = visual.dvar5;
            ucDataBase1.AppRole = visual.dvar6;
            ucDataBase1.AppRolePassword = visual.dvar7.ToString().ToUpper();

            ucDataBase1.SoloLectura = true;

            ucEstacion1.Usuario = visual.dvar10;
            ucEstacion1.OpcionMenu = visual.dvar8.ToString();
            ucEstacion1.Estacion = visual.dvar15.ToString().ToUpper();
            ucEstacion1.CentroId = visual.dvar12.ToString();
            ucEstacion1.Centro = visual.dvar30;
            ucEstacion1.Log = visual.dvar26;

            ucEstacion1.SoloLectura = true;

        }
        #endregion
        #endregion

        #region Guardar Registro
        private void ucAcciones_GuardarClick(object? sender, EventArgs e) {
            GuardarConfiguracion();
        }
        private void GuardarConfiguracion() {

        }
        #endregion

        #region Probar Conexion
        private async void ucAcciones_BuscarEstacionClick(object? sender, EventArgs e) {
            await BuscarEstacionAsync();
        }
        private async Task BuscarEstacionAsync() {
            string servidor = ucDataBase1.Server.Trim();
            string ip = ucEstacion1.IP.Trim();

            if (string.IsNullOrWhiteSpace(servidor) ||
                servidor.Equals("SIVSRV", StringComparison.OrdinalIgnoreCase)) {
                Mostrar.Mensaje("Error", "El campo Servidor no puede estar vacío.");

                ucDataBase1.Server = Red.ObtenerIP192ServerPrincipal();
                return;
            }

            if (string.IsNullOrWhiteSpace(ip)) {
                Mostrar.Mensaje("Error", "La IP de la estación no puede estar vacía.");
                return;
            }

            try {

                ucAcciones1.BuscarEstacionHabilitado = false;
                ucAcciones1.TextoBuscarEstacion = "Buscando...";
                Mostrar.Mensaje("Buscando estación", $"Se está buscando la estación con IP {ip} en el servidor {servidor}. Esto puede tardar unos segundos...");
                var visual = await Task.Run(() => BuscarEstacion(servidor, ip));
                Mostrar.Mensaje("Estación encontrada", $"Se encontró la estación con IP {ip} en el servidor {servidor}.\n\nCentro: {visual.dvar30}\nEstación: {visual.dvar15}\nUsuario: {visual.dvar10}");
                CargarValoresFormulario(visual);
            } catch (Exception ex) {
                SivevLogger.Error($"Error al buscar la estación {ip}. {ex.Message}", SivevOrigen.Configurador);
                Mostrar.Mensaje("Error",$"No fue posible obtener la configuración de la estación.\n\n{ex.Message}");
            } finally {

                ucAcciones1.BuscarEstacionHabilitado = true;
                ucAcciones1.TextoBuscarEstacion = "Buscar estación";
            }
        }

        private async Task<VisualRegistroWindows> BuscarEstacion(string servidor, string ip) {

            cnx conexionInicial = new cnx {
                Servidor = servidor,
                BDD = "SIVEV",
                User = "SivevCentros",
                Pass = "CentrosSivev",
                AppName = "SivAppVfcRegistro",
            };

            var repo = new SivevRepository();
            var r = await repo.ObtenerEstacionPorIpAsync(ip: ip, conf: conexionInicial, aplicacionId: 48);

            if (r == null)
                throw new InvalidOperationException($"No se encontró una estación activa para la IP {ip}.");

            char ultimoDigito = ip.Last(char.IsDigit);

            return new VisualRegistroWindows {
                dvar1 = conexionInicial.Servidor,
                dvar2 = conexionInicial.BDD,
                dvar3 = conexionInicial.User,
                dvar4 = conexionInicial.Pass,
                dvar5 = r.Aplicacion,
                dvar6 = "RollSivev",
                dvar7 = Guid.Parse("53CE7B6E-1426-403A-857E-A890BB63BFE6"),
                dvar8 = 151,
                dvar10 = $"Linea0{ultimoDigito}Visual",
                dvar11 = ip,
                dvar12 = r.CentroId,
                dvar15 = r.EstacionId,
                dvar30 = r.Centro,
            };
        }
        #endregion


        #region Bitacora
        private void ucAcciones_BitacoraClick(object? sender, EventArgs e) {
            CrearBitacoras();
        }
        private void CrearBitacoras() {

            // Bloquear inmediatamente para evitar doble clic
            ucAcciones1.BitacoraHabilitada = false;
            ucAcciones1.TextoBitacora = "Creando...";

            try {
                bool resultado =  SivevEventLogInstaller.CrearFuentes(out string mensaje);

                if (!resultado) {
                    Mostrar.Mensaje("Error", mensaje);
                    ActualizarEstadoBitacora();
                    return;
                }

                Mostrar.Mensaje("Bitácoras", mensaje);
                ActualizarEstadoBitacora();

            } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"No se pudieron crear las bitácoras.\n\n{ex.Message}");
                ActualizarEstadoBitacora();
            }
        }
        private void ActualizarEstadoBitacora() {
            try {
                if (SivevEventLogInstaller.TodasLasFuentesExisten()) {
                    ucAcciones1.TextoBitacora = "Bitácoras creadas";
                    ucAcciones1.BitacoraHabilitada = false;
                    return;
                }

                List<string> faltantes = SivevEventLogInstaller.ObtenerFuentesFaltantes();
                ucAcciones1.TextoBitacora = $"Crear bitácoras ({faltantes.Count})";
                ucAcciones1.BitacoraHabilitada = true;

            } catch {
                ucAcciones1.TextoBitacora = "Crear bitácoras";
                ucAcciones1.BitacoraHabilitada = true;
            }
        }
        #endregion

        #endregion

        #region Activador de directivas de seguridad
        
        private void VerificarActivador() {
            try {
                var activador = new ActivadorBatCreator("http://192.168.16.233/ClickOnce/Visual/Apps_Visual.application", "DPDevTS.bat");

                if (activador.Exists())
                    return;

                BatCreationResult resultado = activador.CreateOrUpdate();

                if (!resultado.Success) {
                    SivevLogger.Error($"No fue posible crear el activador de Visual: {resultado.Message}", SivevOrigen.Configurador);
                    Mostrar.Mensaje("Activador",  resultado.Message);
                    return;
                }
                SivevLogger.Information($"Activador de Visual creado en {resultado.FilePath}.",SivevOrigen.Configurador);
            } catch (Exception ex) {
                SivevLogger.Error($"Error al verificar el activador de Visual: {ex.Message}", SivevOrigen.Configurador);
            }
        }
        #endregion


        private void Visual_Load(object sender, EventArgs e) {
            ActualizarEstadoBitacora();
            VerificarActivador();
        }
    }
}
