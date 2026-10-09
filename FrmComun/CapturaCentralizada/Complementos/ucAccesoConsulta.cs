using FrmComun.Utils;
using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Comun.Servicios;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public string Placa {
            get => txtPlaca.Text;
            set => txtPlaca.Text = value;
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

        public void HabilitarCrearVerificacion(bool habilitar = true) {
            btnCrearVerificacion.Enabled = habilitar;
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
            if (_verificentroApiService == null) {
                Mostrar.Mensaje("Advertencia", "El servicio de Verificentro API no ha sido configurado.");
                return;
            }

            try {
                
                Cursor.Current = Cursors.WaitCursor;

                // =========================================================
                // 1. CONSULTAR CITA
                // =========================================================
                
                var respuestaCitas = await _verificentroApiService.ConsultarCitaAsync(txtPlaca.Text, _centro);

                var datosCitas = JsonSerializer.Deserialize<CitasDto>(respuestaCitas, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });

                if (datosCitas == null) {
                    Mostrar.Mensaje("Error", "No fue posible interpretar la respuesta del servicio de citas.");
                    return;
                }

                /*
                    Guardar aquí en SIVEV el resultado de la consulta de cita.
                */


                // =========================================================
                // 2. VALIDAR RESPUESTA DE CITAS
                // =========================================================
                switch (datosCitas.codigoRespuesta) {
                    // CASOS QUE NO PUEDEN CONTINUAR
                    
                    case "V03":
                    case "V05":
                        Mostrar.Mensaje("Acceso rechazado",$"Centro: {datosCitas.centro}\nFecha: {datosCitas.fechaCita} {datosCitas.horaCita}\n\n{datosCitas.mensaje}");
                        StatusCaptura(true);
                        return;


                    // CASOS QUE SÍ PUEDEN CONTINUAR
                    case "V06":
                    case "S01":
                        break;


                    // RESPUESTA NO CONTEMPLADA
                    default:
                        SivevLogger.Warning($"Respuesta de citas no reconocida:\n{datosCitas.codigoRespuesta}\n\n{datosCitas.mensaje}", SivevOrigen.Captura);
                        Mostrar.Mensaje("Advertencia", $"Respuesta de citas no reconocida:\n{datosCitas.codigoRespuesta}\n\n{datosCitas.mensaje}");
                        return;
                }


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
                var respuestaFotocivicas = await _verificentroApiService.ConsultarFotocivicasAsync(txtPlaca.Text);

                var datosFotocivicas = JsonSerializer.Deserialize<ConsultaFotocivicasDto>(respuestaFotocivicas, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });

                if (datosFotocivicas == null) {
                    SivevLogger.Error($"No fue posible interpretar la respuesta del servicio de finanzas:\n{respuestaFotocivicas}", SivevOrigen.Captura);
                    Mostrar.Mensaje("Error", "No fue posible interpretar la respuesta del servicio de finanzas.");
                    return;
                }


                lblStatusAmbientales.Text = datosFotocivicas.AdeudoSancion ? "Con adeudo" : "Sin adeudo";
                lblStatusFotocivicas.Text = datosFotocivicas.Fotocivicas.TieneAdeudo ? "Con adeudo" : "Sin adeudo";
                lblStatusTenencias.Text = datosFotocivicas.AdeudoTenencia ? "Con adeudo" : "Sin adeudo";
                lblStatusMonetarias.Text = datosFotocivicas.AdeudoInfraccion ? "Con adeudo" : "Sin adeudo";




                /*
                    Guardar aquí en SIVEV el resultado de Finanzas.
                */


                // =========================================================
                // 5. VALIDAR ADEUDOS
                // =========================================================

                if (datosFotocivicas.AdeudoTenencia) {

                    string tenencias = string.Join(", ",datosFotocivicas.Tenencias ?? []);
                    string mensaje = DecodificarUnicode(datosFotocivicas.MensajeTenencia ?? string.Empty);
                    mensaje = mensaje.Replace("|", Environment.NewLine);
                    Mostrar.Mensaje("Acceso rechazado", $"El vehículo presenta adeudo de tenencia.\n\nEjercicio(s): {tenencias}\n\n{mensaje}");
                    StatusCaptura(true);
                    return;
                }



                //int sancionesPendientes = datosFotocivicas.Sanciones?.Count(x => x.Status == "NO PAGADA") ?? 0;
                int sancionesPendientes = datosFotocivicas.Sanciones?.Count() ?? 0;
                if (datosFotocivicas.AdeudoSancion) {
                    Mostrar.Mensaje("Acceso rechazado", $"El vehículo presenta sanciones pendientes.\n{sancionesPendientes}");
                    StatusCaptura(true);
                    return;
                }







                int economicasPendientes = datosFotocivicas.Economicas?.Count(x => x.Status == "NO PAGADA") ?? 0;
                if (datosFotocivicas.AdeudoInfraccion || economicasPendientes > 0) {
                    Mostrar.Mensaje("Acceso rechazado", $"El vehículo presenta sanciones económicas pendientes.\n\nPendientes: {economicasPendientes}");
                    StatusCaptura(true);
                    return;
                }




                if (datosFotocivicas.Fotocivicas?.TieneAdeudo == true) {
                    Mostrar.Mensaje(
                        "Acceso rechazado",
                        $"El vehículo presenta adeudos de Fotocívicas.\n\n" +
                        $"Cantidad de puntos: {datosFotocivicas.Fotocivicas.Puntos}\n" +
                        $"Curso en línea: {datosFotocivicas.Fotocivicas.DebeEnLinea}\n" +
                        $"Curso presencial: {datosFotocivicas.Fotocivicas.DebePresencial}\n" +
                        $"Amonestaciones: {datosFotocivicas.Fotocivicas.DebeAmonestaciones}"
                    );
                    StatusCaptura(true);
                    return;
                }


                
                






                //Mostrar.Mensaje("Consulta concluida", "No se encontraron impedimentos para continuar con la verificación.");










            } catch (Exception ex) {
                SivevLogger.Error($"Ocurrió un error durante la consulta:\n\n{ex.Message}",SivevOrigen.Captura);
                Mostrar.Mensaje("Error", $"Ocurrió un error durante la consulta:\n\n{ex.Message}");

            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        private async void cbMotivoAcceso_SelectionChangeCommitted(object sender, EventArgs e) {
            if (cbMotivoAcceso.SelectedValue is byte motivoAccesoId && motivoAccesoId == 8) {
                
                panelFisicoMecanica(true);
                var FM = await _verificentroApiService.ConsultarFisicoMecanicaAsync(txtPlaca.Text);
                var datosFM = JsonSerializer.Deserialize<FisicoMecanicaDto>(FM, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                lblStatusFM.Text = datosFM?.documentalAprobada == true ? $"Origen: {datosFM.origen}\nPlaca: {datosFM.placa}\n{datosFM.mensaje}" : $"origen: {datosFM.origen}\nPlaca: {datosFM.placa}\n{datosFM.mensaje}";

                if (datosFM?.documentalAprobada == false){
                    StatusCaptura(true);
                }

            } else {
                panelFisicoMecanica();
            }
        }

        private void panelFisicoMecanica(bool flags = false) {
            pnlFM.Visible = flags;
            pnlFM.Enabled = flags;
        }
        private static string DecodificarUnicode(string texto) {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            return Regex.Replace(texto, @"u([0-9a-fA-F]{4})",
                match => {
                    int codigo = Convert.ToInt32(match.Groups[1].Value, 16);
                    return ((char)codigo).ToString();
                });
        }


        #region Cancelar prueba de verificación
        private void StatusCaptura(bool status) {
            Captura = status;
            CapturaCancelada?.Invoke(status);
        }




        #endregion




    }
}