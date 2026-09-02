using Apps_Proveedores.Modelos;
using FontAwesome.Sharp;
using SQLSIVEV.Infrastructure.Utils;
using FrmComun.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Proveedores.Paneles.Horario {
    public partial class CambioHorario : UserControl {
        private readonly System.Windows.Forms.Timer tmrHora = new();
        private const string ZonaCdmx = "Central Standard Time (Mexico)";
        public CambioHorario() {
            InitializeComponent();
            CrearIconos();
            _lblEquipo.Text = Environment.MachineName;
            _lblDominio.Text = Environment.UserDomainName;
            _lblUsuario.Text = Environment.UserName;

            ActualizarFechaHora();
            ActualizarZonaHoraria();
            ConfigurarPaneles();
            ConfigurarBotonesAcciones();

            tmrHora.Interval = 1000;
            tmrHora.Tick += TmrHora_Tick;
            tmrHora.Start();
            ibActualizarEstado.TabStop = true;
            ibActualizarEstado.TabIndex = 0;

            Load += CambioHorario_Load;
        }
        #region Hora Actual

        private void TmrHora_Tick(object? sender, EventArgs e) {
            ActualizarFechaHora();
        }

        private void ActualizarFechaHora() {
            DateTime ahora = DateTime.Now;
            lblFechaSistema.Text = ahora.ToString("dd/MM/yyyy");
            lblHoraSistema.Text = ahora.ToString("HH:mm:ss");
        }
        #endregion
        #region Crear Iconos 
        private void CrearIconos() {
            ipbPCHeader.IconChar = IconChar.Desktop;
            ipbPCHeader.IconColor = Color.FromArgb(55, 70, 90);
            ipbPCHeader.IconSize = 42;
            ipbPCHeader.BackColor = Color.White;

            ipbReloj.IconChar = IconChar.Clock;
            ipbReloj.IconColor = Color.Crimson;
            ipbReloj.IconSize = 42;
            ipbReloj.BackColor = Color.White;
        }
        #endregion

        #region zona horaria
        private void ActualizarZonaHoraria() {

            TimeZoneInfo zonaActual = TimeZoneInfo.Local;

            lblZonaHorariaSistema.Text = zonaActual.Id;
            bool zonaCorrecta = string.Equals(zonaActual.Id, ZonaCdmx, StringComparison.OrdinalIgnoreCase);

            if (zonaCorrecta) {
                lblZonaHorariaEstadoSistema.Text = "Correcta";
                lblZonaHorariaEstadoSistema.ForeColor = Color.ForestGreen;
            } else {
                lblZonaHorariaEstadoSistema.Text = "Incorrecta";
                lblZonaHorariaEstadoSistema.ForeColor = Color.Crimson;
            }
        }
        #endregion
        #region Configuración de margenes en paneles
        private void PanelConBorde_Paint(object? sender, PaintEventArgs e) {
            if (sender is not Panel pnl)
                return;
            using Pen borde = new(Color.FromArgb(220, 220, 220), 1);
            e.Graphics.DrawRectangle(borde, 0, 0, pnl.ClientSize.Width - 1, pnl.ClientSize.Height - 1);
        }
        private void ConfigurarPaneles() {
            Panel[] paneles = {
                pnlFechaHora,
                pnlZonaHoraria,
                pnlSincronizacion,
                splitContainer3.Panel1,
                splitContainer3.Panel2,
                pnlFooter
            };

            foreach (Panel pnl in paneles) {
                pnl.Paint += PanelConBorde_Paint;
                pnl.Margin = new Padding(6);
                pnl.Padding = new Padding(8);
            }
        }
        #endregion
        #region Configurar botones
        private void ConfigurarBotonesAcciones() {
            IconButton[] botones = {
                ibActualizarEstado,
                ibSincronizar,
                ibZonaHoraria,
                ibHorarioDeVerano,
                ibCorregirYSincronizar
            };

            foreach (IconButton boton in botones) {
                boton.MouseEnter += BotonAccion_MouseEnter;
                boton.MouseLeave += BotonAccion_MouseLeave;
                boton.Enter += BotonAccion_Enter;
                boton.Leave += BotonAccion_Leave;

                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 1;
                boton.Cursor = Cursors.Hand;

                NormalizarBoton(boton);
            }
        }
        private void SeleccionarBoton(IconButton boton) {
            boton.BackColor = Color.Crimson;
            boton.ForeColor = Color.White;
            boton.IconColor = Color.White;
            boton.FlatAppearance.BorderColor = Color.Crimson;
        }

        private void NormalizarBoton(IconButton boton) {
            boton.BackColor = Color.White;
            boton.ForeColor = Color.FromArgb(45, 55, 65);
            boton.IconColor = Color.FromArgb(45, 55, 65);
            boton.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
        }
        private void BotonAccion_MouseEnter(object? sender, EventArgs e) {
            if (sender is IconButton boton) {
                SeleccionarBoton(boton);
            }
        }

        private void BotonAccion_MouseLeave(object? sender, EventArgs e) {
            if (sender is IconButton boton && !boton.Focused) {
                NormalizarBoton(boton);
            }
        }
        private void BotonAccion_Enter(object? sender, EventArgs e) {
            if (sender is IconButton boton) {
                SeleccionarBoton(boton);
            }
        }
        private void BotonAccion_Leave(object? sender, EventArgs e) {
            if (sender is not IconButton boton)
                return;
            Point mouse = boton.PointToClient(Cursor.Position);
            if (!boton.ClientRectangle.Contains(mouse)) {
                NormalizarBoton(boton);
            }
        }
        #endregion

        private async void CambioHorario_Load(object sender, EventArgs e) {
            await CargarEstadoSincronizacionAsync();
            BeginInvoke(new Action(() => {
                ibCorregirYSincronizar.Select();
                ibCorregirYSincronizar.Focus();
            }));
        }
        private async Task CargarEstadoSincronizacionAsync() {
            try {
                EstadoSincronizacion? estado =  await HorarioServiceClient.ObtenerEstadoAsync();
                if (estado == null)
                    return;

                lblEstadoDelServicioR.Text = estado.ServicioActivo ? "Ejecutándose" : "Detenido";
                lblOrigenDeLaHoraR.Text = estado.Origen;
                lblUltimaSincronizacionR.Text = estado.UltimaSincronizacion;
                lblResultadoDeSincronizacionR.Text = estado.Sincronizado ? "Sincronizado" : "No sincronizado";
                lblDireferenciaConElOrigenR.Text = estado.Diferencia;
                ActualizarEstadoVisual(estado);
                ActualizarEstadoGeneral(estado);
            } catch (Exception ex) {
                SivevLogger.Error($"Error consultando sincronización: {ex.Message}");
            }
        }
        private void ActualizarEstadoVisual(EstadoSincronizacion estado) {

            Color verde = Color.ForestGreen;
            Color rojo = Color.Crimson;
            Color gris = Color.FromArgb(55, 75, 70);

            // Servicio
            if (estado.ServicioActivo) {
                lblEstadoDelServicioR.ForeColor = verde;
            } else {
                lblEstadoDelServicioR.ForeColor = rojo;
            }

            // Resultado
            if (estado.Sincronizado) {
                lblResultadoDeSincronizacionR.ForeColor = verde;
            } else {
                lblResultadoDeSincronizacionR.ForeColor = rojo;
            }

            // Los demás son informativos
            lblOrigenDeLaHoraR.ForeColor = Color.Black;
            lblUltimaSincronizacionR.ForeColor = Color.Black;
            lblDireferenciaConElOrigenR.ForeColor = Color.Black;
        }
        #region Corregir y sincronizar
        private async void ibCorregirYSincronizar_Click(object sender, EventArgs e) {
            try {
                Botones();
                string resultado = await HorarioServiceClient.CorregirHoraYSincronizarAsync();
                await Task.Delay(1000);
                ActualizarZonaHoraria();
                await CargarEstadoSincronizacionAsync();
                if (resultado == "OK") {
                    Mostrar.Mensaje("Horario corregido", "La configuración de hora fue corregida y el equipo se sincronizó correctamente.");
                } else {
                    Mostrar.Mensaje("No fue posible completar la corrección", resultado);
                }
            } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"Ocurrió un error al corregir la hora y sincronizar.\n\n{ex.Message}");
            } finally {
                Botones(true);
            }
        }
        #endregion
        #region Sincronizar hora
        private async void ibSincronizar_Click(object sender, EventArgs e) {
            try {
                Botones();
                string resultado = await HorarioServiceClient.ResincronizarAsync();
                Mostrar.Mensaje("Sincronización", resultado);
                // Refrescamos los datos del dashboard
                await CargarEstadoSincronizacionAsync();
            } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"Ocurrió un error al sincronizar la hora.\n\n{ex.Message}");
                SivevLogger.Error( $"Error sincronizando hora: {ex}");
            } finally {
                Botones(true);
            }
        }
        #endregion
        #region Actualizar zona horaria
        private async void ibZonaHoraria_Click(object sender, EventArgs e) {
            try {
                Botones();
                TimeZoneInfo zonaActual = TimeZoneInfo.Local;
                // Ya está correcta
                if (string.Equals(zonaActual.Id, ZonaCdmx, StringComparison.OrdinalIgnoreCase)) {
                    Mostrar.Mensaje("Zona horaria correcta", "La zona horaria ya está configurada correctamente para Ciudad de México.");
                    ActualizarZonaHoraria();
                    return;
                }
                // Hay que corregirla mediante Service_Proveedores
                string resultado = await HorarioServiceClient.EstablecerZonaCdmxAsync();
                if (resultado.Equals("OK",StringComparison.OrdinalIgnoreCase)) {
                    ActualizarZonaHoraria();
                    Mostrar.Mensaje("Zona horaria actualizada", "La zona horaria se cambió correctamente a Ciudad de México.");
                } else {
                    Mostrar.Mensaje("Error al cambiar zona horaria", $"No fue posible cambiar la zona horaria.\n\n{resultado}");
                }
            } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"Ocurrió un error al establecer la zona horaria.\n\n{ex.Message}");
                SivevLogger.Error($"Error estableciendo zona horaria: {ex}");
            } finally {
                Botones(true);
            }
        }
        #endregion
        #region Boton de horario de verano 
        private async void ibHorarioDeVerano_Click(object sender, EventArgs e) {
            try {
                ibHorarioDeVerano.Enabled = false;
                string resultado = await HorarioServiceClient.DesactivarHorarioVeranoAsync();
                if (resultado == "YA_DESACTIVADO") {
                    Mostrar.Mensaje("Horario de verano", "El ajuste por horario de verano ya se encuentra desactivado.");
                } else if (resultado == "DESACTIVADO") {
                    Mostrar.Mensaje("Horario de verano desactivado", "El ajuste automático por horario de verano fue desactivado correctamente.");
                } else {
                    Mostrar.Mensaje("Error al modificar horario de verano", $"No fue posible desactivar el horario de verano.\n\n{resultado}");
                }
            } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"Ocurrió un error al modificar el horario de verano.\n\n{ex.Message}");
            } finally {
                ibHorarioDeVerano.Enabled = true;
            }
        }
        #endregion
        #region Actualizar estado de sincronización
        private async void ibActualizarEstado_Click(object sender, EventArgs e) {
            try {
                Botones();
                ActualizarZonaHoraria();
                await CargarEstadoSincronizacionAsync();
            } catch (Exception ex) {
                SivevLogger.Error($"Error actualizando estado de horario: {ex}");
            } finally {
                Botones(true);
            }
        }
        #endregion
        #region Actualizar estado de sincronización al iniciar
        private void ActualizarEstadoGeneral(EstadoSincronizacion estado) {
            bool zonaCorrecta = string.Equals(TimeZoneInfo.Local.Id, ZonaCdmx, StringComparison.OrdinalIgnoreCase);

            // TODO CORRECTO
            if (estado.ServicioActivo && estado.Sincronizado && zonaCorrecta) {

                lblEstadoGeneralTitulo.Text = "La hora del equipo es correcta.";
                lblEstadoGeneralDetalle.Text ="El equipo está sincronizado con el dominio.";
                ipbEstadoGeneral.IconChar =  IconChar.CircleCheck;
                ipbEstadoGeneral.IconColor = Color.ForestGreen;
                return;
            }

            // SERVICIO DETENIDO
            if (!estado.ServicioActivo) {
                lblEstadoGeneralTitulo.Text = "El servicio de hora no está disponible.";
                lblEstadoGeneralDetalle.Text ="El servicio W32Time se encuentra detenido.";
                ipbEstadoGeneral.IconChar = IconChar.CircleXmark;
                ipbEstadoGeneral.IconColor = Color.Crimson;
                return;
            }

            // ZONA INCORRECTA
            if (!zonaCorrecta) {
                lblEstadoGeneralTitulo.Text = "La zona horaria requiere corrección.";
                lblEstadoGeneralDetalle.Text =$"Configure la zona horaria en {ZonaCdmx}.";
                ipbEstadoGeneral.IconChar = IconChar.CircleExclamation;
                ipbEstadoGeneral.IconColor = Color.DarkOrange;
                return;
            }

            // NO SINCRONIZADO
            if (!estado.Sincronizado) {
                lblEstadoGeneralTitulo.Text ="La hora del equipo requiere atención.";
                lblEstadoGeneralDetalle.Text = "El equipo no se encuentra sincronizado con el dominio.";
                ipbEstadoGeneral.IconChar = IconChar.CircleExclamation;
                ipbEstadoGeneral.IconColor = Color.DarkOrange;
                return;
            }
        }
        #endregion
        #region Habilitar/Deshabilitar botones
        private void Botones(bool flag = false, string status = "DESCONOCIDO") {
            ibActualizarEstado.Enabled = flag;
            ibActualizarEstado.Visible = flag;

            ibHorarioDeVerano.Enabled = flag;
            ibHorarioDeVerano.Visible = flag;

            ibZonaHoraria.Enabled = flag;
            ibZonaHoraria.Visible = flag;

            ibSincronizar.Enabled = flag;
            ibSincronizar.Visible = flag;

            ibCorregirYSincronizar.Enabled = flag;
            ibCorregirYSincronizar.Visible = flag;

            if (flag) {
                ibCorregirYSincronizar.Select();
                ibCorregirYSincronizar.Focus();
            }
            if (!flag) {
                lblEstadoGeneralTitulo.Text = status;
            }
        }
        #endregion

    }
}
