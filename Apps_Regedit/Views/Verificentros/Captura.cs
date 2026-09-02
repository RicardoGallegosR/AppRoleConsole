using Apps_Regedit.Services;
using FrmComun.Utils;
using SQLSIVEV.Comun;
using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Configuracion;
using SQLSIVEV.Infrastructure.Utils;

namespace Apps_Regedit.Views.Verificentros {
    public partial class Captura : UserControl {
        public Captura() {
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

        }
        #endregion


        #region Guardar Registro
        private void ucAcciones_GuardarClick(object? sender, EventArgs e) {
            GuardarConfiguracion();
        }
        private void GuardarConfiguracion() {

        }
        #endregion


        #region Probar Conexión
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
                var captura = await Task.Run(() => BuscarEstacion(servidor, ip));
                CargarValoresFormulario(captura);
                if (!ConfiguracionCaptura.ValidarAccesoRuta(captura.dvar20, out string mensaje)) {
                    Mostrar.Mensaje("Acceso a ruta", mensaje);
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error al buscar la estación {ip}. {ex.Message}", SivevOrigen.Configurador);
                Mostrar.Mensaje("Error", $"No fue posible obtener la configuración de la estación.\n\n{ex.Message}");
            } finally {

                ucAcciones1.BuscarEstacionHabilitado = true;
                ucAcciones1.TextoBuscarEstacion = "Buscar estación";
            }
        }

        private async Task<CapturaRegistroWindows> BuscarEstacion(string servidor, string ip) {

            cnx conexionInicial = new cnx {
                Servidor = servidor,
                BDD = "SIVEV",
                User = "SivevCentros",
                Pass = "CentrosSivev",
                AppName = "SivAppVfcRegistro",
            };

            var repo = new SivevRepository();
            var r = await repo.ObtenerEstacionPorIpAsync(ip: ip, conf: conexionInicial, aplicacionId: 1);

            if (r == null)
                throw new InvalidOperationException($"No se encontró una estación activa para la IP {ip}.");

            string[] partesIp = ip.Split('.');

            if (partesIp.Length != 4 ||
                !int.TryParse(partesIp[3], out int ultimoOcteto)) {
                throw new InvalidOperationException("La IP no tiene un formato válido.");
            }

            if (ultimoOcteto < 18 || ultimoOcteto > 20) {
                throw new InvalidOperationException(
                    "La IP de Captura debe estar entre .18 y .20."
                );
            }

            int numeroLinea = ultimoOcteto - 17;

            return new CapturaRegistroWindows {
                dvar1 = conexionInicial.Servidor,
                dvar2 = conexionInicial.BDD,
                dvar3 = conexionInicial.User,
                dvar4 = conexionInicial.Pass,
                dvar5 = r.Aplicacion,
                dvar6 = "RollSivev",
                dvar7 = Guid.Parse("53CE7B6E-1426-403A-857E-A890BB63BFE6"),
                dvar8 = 101,
                dvar10 = $"Linea0{numeroLinea}Captura",
                dvar11 = ip,
                dvar12 = r.CentroId,
                dvar15 = r.EstacionId,
                dvar18 = r.Centro,
                dvar20 = $@"\\SIVSRV{r.CentroId}\EscaneoCaptura"
            };
        }

        

        #endregion
        #region Abrir Bitácora
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
        #region Cargar Valores en formulario 
        private void CargarValoresFormulario(CapturaRegistroWindows captura) {
            ucDataBase1.Server = captura.dvar1;
            ucDataBase1.Database = captura.dvar2;
            ucDataBase1.User = captura.dvar3;
            ucDataBase1.Password = captura.dvar4;
            ucDataBase1.AppName = captura.dvar5;
            ucDataBase1.AppRole = captura.dvar6;
            ucDataBase1.AppRolePassword = captura.dvar7.ToString().ToUpper();

            ucDataBase1.SoloLectura = true;

            ucEstacion1.Usuario = captura.dvar10;
            ucEstacion1.OpcionMenu = captura.dvar8.ToString();
            ucEstacion1.Estacion = captura.dvar15.ToString().ToUpper();
            ucEstacion1.CentroId = captura.dvar12.ToString();
            ucEstacion1.Centro = captura.dvar18;
            ucEstacion1.Log = captura.dvar19;

            ucEstacion1.SoloLectura = true;

            txtRutaEscaneos.Text = captura.dvar20;
        }
        #endregion
        #region Activador de directivas de seguridad
        private void VerificarActivador() {
            try {
                var activador = new ActivadorBatCreator("http://192.168.16.233/ClickOnce/Captura/Apps_Captura.application", "DPDevTs.bat", @"C:\Program Files (x86)\Common Files");

                if (activador.Exists())
                    return;

                BatCreationResult resultado = activador.CreateOrUpdate();

                if (!resultado.Success) {
                    SivevLogger.Error($"No fue posible crear el activador de Captura: {resultado.Message}", SivevOrigen.Configurador);
                    Mostrar.Mensaje("Activador", resultado.Message);
                    return;
                }
                SivevLogger.Information($"Activador de Captura creado en {resultado.FilePath}.", SivevOrigen.Configurador);
            } catch (Exception ex) {
                SivevLogger.Error($"Error al verificar el activador de Captura: {ex.Message}", SivevOrigen.Configurador);
            }
        }
        #endregion
        private void Captura_Load(object sender, EventArgs e) {
            ActualizarEstadoBitacora();
            VerificarActivador();
        }
    }
}
