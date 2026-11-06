using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Utils;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace SQLSIVEV.Comun.Servicios {
    public class VerificentroApiService {
        private readonly VerificentroApiConfig _config;
        private readonly HttpClient _client;

        public VerificentroApiService(VerificentroApiConfig config) {
            _config = config;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(_config.Accept)
            );

            _client.DefaultRequestHeaders.Add("x-api-key", _config.ApiKey);
        }

        public async Task<FisicoMecanicaDto> ConsultarFisicoMecanicaAsync(string placa) {
            var (Contenido, Status) = await GetAsync($"documental?Placa={placa}");
            var datosFM = JsonSerializer.Deserialize<FisicoMecanicaDto>(Contenido, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            }) ?? throw new JsonException("Respuesta inválida de físico-mecánica.");

            datosFM.status = Status;
            return datosFM;
        }

        public async Task<string> ConsultarFotocivicasAsync(string placa) {
            var (contenido, _) = await GetAsync($"fotocivicas?Placa={Uri.EscapeDataString(placa)}");
            return contenido;
        }

        public async Task<string> ConsultarCitaAsync(string placa, string centro) {
            var (contenido, _) = await GetAsync($"cita?Placa={Uri.EscapeDataString(placa)}&Centro={Uri.EscapeDataString(centro)}");

            return contenido;
        }

        private async Task<(string Contenido, short Status)> GetAsync(string endpoint) {
            string dnsUrl = $"http://{_config.DnsHost}{_config.Path}{endpoint}";
            string ipUrl = $"http://{_config.FallbackIp}{_config.Path}{endpoint}";

            try {
                return await EjecutarGetAsync(dnsUrl);
            } catch (HttpRequestException ex) when (ex.StatusCode == null) {
                // Solo aplicar fallback ante errores de transporte.
                return await EjecutarGetAsync(ipUrl);
            }
        }

        private async Task<(string Contenido, short Status)> EjecutarGetAsync(string url) {
            using var response = await _client.GetAsync(url);

            string contenido = await response.Content.ReadAsStringAsync();
            short status = (short)response.StatusCode;

            if (!response.IsSuccessStatusCode) {
                SivevLogger.Error(
                    $"Error HTTP {(int)response.StatusCode} " +
                    $"({response.ReasonPhrase})\n" +
                    $"URL: {url}\n" +
                    $"Respuesta: {WebServicesHelp.DecodificarUnicode(contenido)}",
                    SivevOrigen.WebServicesCaptura
                );

                throw new HttpRequestException($"Error HTTP {(int)response.StatusCode} ({response.ReasonPhrase})", null, response.StatusCode);
            }

            return (contenido, status);
        }
    }
}