using FrmComun.Utils;
using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Comun.Servicios;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucAccesoConsulta : UserControl {
        private VerificentroApiConfig? _verificentroApiConfig;
        private string? _centro;
        public event Action<bool>? CapturaCancelada;
        public bool Captura { get; private set; } = false;
        private VerificentroApiService? _verificentroApiService;
        public event EventHandler<CrearVerificacionEventArgs>? CrearVerificacion;
        public AppRoleSqlExecutor _sqlExecutor;
        public event EventHandler<ConsultaFotocivicasDto>? FotocivicasConsultadas;
        public event Action<FisicoMecanicaDto>? FisicoMecanicaConsultada;
        public event Action<CitasDto>? CitasConsultada;
        public Guid _estacionId { get; set; }
        public Guid _accesoId { get; set; }
        public Guid _bitacoraVisitaId { get; set; }
        public string Placa { get; set; }

        public string Monetarias {
            get => lblStatusMonetarias.Text;
            set => lblStatusMonetarias.Text = value;
        }

        public string Ambientales {
            get => lblStatusAmbientales.Text;
            set => lblStatusAmbientales.Text = value;
        }

        public string Tenencias {
            get => lblStatusTenencias.Text;
            set => lblStatusTenencias.Text = value;
        }

        public string Fotocivicas {
            get => lblStatusFotocivicas.Text;
            set => lblStatusFotocivicas.Text = value;
        }
        public bool PET => cbPET.Checked;
        
        public int? MotivoAccesoId {
            get => cbMotivoAcceso.SelectedValue is int id ? id : null;
            set => cbMotivoAcceso.SelectedValue = value;
        }

        public byte? byMotivoAccesoId {
            get => cbMotivoAcceso.SelectedValue is byte id ? id : null;
            set => cbMotivoAcceso.SelectedValue = value;
        }

        public void RegistoActivado(bool flag = false) {
            cbPET.Enabled = flag;
            cbMotivoAcceso.Enabled = flag;
        }


        public ucAccesoConsulta() {
            InitializeComponent();
            btnCrearVerificacion.Click += btnCrearVerificacion_Click;

            panelFisicoMecanica();

            
        }

        public void Configurar(VerificentroApiConfig config, string Centro) {
            _verificentroApiConfig = config;
            _verificentroApiService = new VerificentroApiService(config);
            _centro = Centro;
        }

        public void HabilitarCrearVerificacion(bool flag = false) {
            btnCrearVerificacion.Enabled = flag;
            btnCrearVerificacion.Visible = flag;
        }
        public void HabilitarBotonBusqueda(bool flag = false) {
            ibServicios.Enabled = flag;
            ibServicios.Visible = flag;
        }
        
        public void MotivosAccesoDefecto(byte defecto = 9) {
            // Seleccionar por defecto MotivoAccesoId = 9 (VERIFICACIÓN VEHICULAR)
            cbMotivoAcceso.SelectedValue = defecto;
        }




        private void btnCrearVerificacion_Click(object? sender, EventArgs e) {
            var datos = new DatosInicioVerificacion();
            CrearVerificacion?.Invoke(this, new CrearVerificacionEventArgs(datos));
        }

        public void CargarMotivosAcceso(IEnumerable<MotivosAccesoDto> motivosAccesos) {
            cbMotivoAcceso.DataSource = null;
            cbMotivoAcceso.DisplayMember = nameof(MotivosAccesoDto.MotivoAcceso);
            cbMotivoAcceso.ValueMember = nameof(MotivosAccesoDto.MotivoAccesoId);
            cbMotivoAcceso.DataSource = motivosAccesos.ToList();
        }

        private async void ibServicios_Click(object sender, EventArgs e) {
            await ConsultarCitasYAdesdos();
        }


        public async Task ConsultarCitasYAdesdos() {
            if (_verificentroApiService == null) {
                Mostrar.Mensaje("Advertencia", "El servicio de Verificentro API no ha sido configurado.");
                return;
            }

            try {

                Cursor.Current = Cursors.WaitCursor;

                // =========================================================
                // 1. CONSULTAR CITA
                // =========================================================

                cbPET.Enabled = false;
                cbMotivoAcceso.Enabled = false;


                var respuestaCitas = await _verificentroApiService.ConsultarCitaAsync(Placa, _centro);

                var datosCitas = JsonSerializer.Deserialize<CitasDto>(respuestaCitas, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });

                if (datosCitas == null) {
                    Mostrar.Mensaje("Error", "No fue posible interpretar la respuesta del servicio de citas.");
                    return;
                }
                CitasConsultada?.Invoke(datosCitas);



                // =========================================================
                // 3. MOSTRAR INFORMACIÓN DE CITA
                // =========================================================
                lblStatusCitas.Text = datosCitas.existeCita
                    // True
                    ? $"Cita: {datosCitas.fechaCita} {datosCitas.horaCita}"
                    // False
                    : $"Sin cita para el centro: {datosCitas.centro}\n" +
                      $"Placa: {datosCitas.placa}\n{datosCitas.mensaje}";


                // =========================================================
                // 4. CONSULTAR FINANZAS
                // =========================================================
                //var respuestaFotocivicas = await _verificentroApiService.ConsultarFotocivicasAsync(Placa);
                ConsultaFotocivicasDto datosFotocivicas;
                string? respuestaFotocivicas = "";

                try {
                     respuestaFotocivicas = await _verificentroApiService.ConsultarFotocivicasAsync(Placa);

                    datosFotocivicas = JsonSerializer.Deserialize<ConsultaFotocivicasDto>(
                            respuestaFotocivicas, new JsonSerializerOptions {
                                PropertyNameCaseInsensitive = true
                            }
                        ) ?? throw new JsonException("Respuesta vacía.");

                    datosFotocivicas.ConexionWebSrv = 1;

                }
                // Clasificación acordada para el HTTP 503
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable) {
                    datosFotocivicas = new ConsultaFotocivicasDto {
                        Placa = Placa,
                        ConexionWebSrv = 2
                    };
                    SivevLogger.Error($"Error HTTP al consultar SEDEMA: {ex.Message}", SivevOrigen.Captura);
                } catch (HttpRequestException ex) {
                    SivevLogger.Error($"Error HTTP al consultar Finanzas: {ex.Message}", SivevOrigen.Captura);
                    datosFotocivicas = new ConsultaFotocivicasDto {
                        Placa = Placa, 
                        ConexionWebSrv = 3
                    };
                }

                if (datosFotocivicas == null) {
                    SivevLogger.Error($"No fue posible interpretar la respuesta del servicio de finanzas:\n{respuestaFotocivicas}", SivevOrigen.Captura);
                    Mostrar.Mensaje("Error", "No fue posible interpretar la respuesta del servicio de finanzas.");
                    return;
                }


                lblStatusAmbientales.Text = datosFotocivicas.AdeudoSancion ? "Con adeudo" : "Sin adeudo";
                lblStatusFotocivicas.Text = datosFotocivicas.Fotocivicas.TieneAdeudo ? "Con adeudo" : "Sin adeudo";
                lblStatusTenencias.Text = datosFotocivicas.AdeudoTenencia ? "Con adeudo" : "Sin adeudo";
                lblStatusMonetarias.Text = datosFotocivicas.AdeudoInfraccion ? "Con adeudo" : "Sin adeudo";

                FotocivicasConsultadas?.Invoke(this, datosFotocivicas);
            } catch (Exception ex) {
                SivevLogger.Error($"Ocurrió un error durante la consulta:\n\n{ex.Message}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error", $"Ocurrió un error durante la consulta:\n\n{ex.Message}");

            } finally {
                Cursor.Current = Cursors.Default;
                cbPET.Enabled = true;
                cbMotivoAcceso.Enabled = true;
            }
        }

        private async void cbMotivoAcceso_SelectionChangeCommitted(object sender, EventArgs e) {
            if (cbMotivoAcceso.SelectedValue is byte motivoAccesoId && motivoAccesoId == 8) {
                
                panelFisicoMecanica(true);
                var FM = await _verificentroApiService.ConsultarFisicoMecanicaAsync(Placa);
                var datosFM = JsonSerializer.Deserialize<FisicoMecanicaDto>(FM, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                lblStatusFM.Text = datosFM?.documentalAprobada == true ? $"Origen: {datosFM.origen}\nPlaca: {datosFM.placa}\n{datosFM.mensaje}" : $"origen: {datosFM.origen}\nPlaca: {datosFM.placa}\n{datosFM.mensaje}";

                // Invocar el evento para notificar que se ha consultado FisicoMecanica
                FisicoMecanicaConsultada?.Invoke(datosFM);
            } else {
                panelFisicoMecanica();
            }
        }

        private void panelFisicoMecanica(bool flags = false) {
            pnlFM.Visible = flags;
            pnlFM.Enabled = flags;
        }
        
    }
}