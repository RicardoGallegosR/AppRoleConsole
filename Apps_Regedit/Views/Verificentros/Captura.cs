using FrmComun.Utils;
using SQLSIVEV.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Regedit.Views.Verificentros {
    public partial class Captura : UserControl {
        private const string EventLogName = "SIVEV";

        public Captura() {
            InitializeComponent();
            pnlFooter.BringToFront();

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
            await ProbarConexionAsync();
        }
        private async Task ProbarConexionAsync() {

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

        private void Captura_Load(object sender, EventArgs e) {
            ActualizarEstadoBitacora();
        }
    }
}
