using SQLSIVEV.Comun;
using SQLSIVEV.Infrastructure.Services;
using FrmComun.Utils;
using DPFP_SMA.Utils;

namespace Apps_Regedit.Views.Verificentros {
    public partial class Visual : UserControl {
        private Regedit regedit = new("VISUAL");

        public Visual() {
            InitializeComponent();
            pnlFooter.BringToFront();
            
            if (ucAcciones1 == null) {
                Mostrar.Mensaje("Error", "ucAcciones1 ES NULL");
                return;
            }
            ucAcciones1.LeerClick += ucAcciones_LeerClick;
            ucAcciones1.GuardarClick += ucAcciones_GuardarClick;
            ucAcciones1.BuscarEstacionClick += ucAcciones_BuscarEstacionClick;
            ucAcciones1.BitacoraClick += ucAcciones_BitacoraClick;
        }

        #region Eventos de botones
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
            ucEstacion1.Centro = visual.dvar12.ToString();
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
            await ProbarConexionAsync();
        }
        private async Task ProbarConexionAsync() {

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

        private void Visual_Load(object sender, EventArgs e) {
            ActualizarEstadoBitacora();
        }
    }
}
