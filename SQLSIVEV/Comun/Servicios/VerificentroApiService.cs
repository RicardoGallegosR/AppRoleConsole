using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Infrastructure.Utils;
using System.Net.Http.Headers;

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

        public async Task<string> ConsultarFisicoMecanicaAsync(string placa) {
            return await GetAsync($"documental?Placa={placa}");
        }

        public async Task<string> ConsultarFotocivicasAsync(string placa) {
            return await GetAsync($"fotocivicas?Placa={placa}");
        }

        public async Task<string> ConsultarCitaAsync(string placa, string centro) {
            return await GetAsync($"cita?Placa={placa}&Centro={centro}");
        }

        /*
        private async Task<string> GetAsync(string endpoint) {
            
            string dnsUrl = $"http://{_config.DnsHost}{_config.Path}{endpoint}";
            string ipUrl =  $"http://{_config.FallbackIp}{_config.Path}{endpoint}";

            try {
                return await _client.GetStringAsync(dnsUrl);
            } catch (HttpRequestException) {
                return await _client.GetStringAsync(ipUrl);
            }
        }
        */

        private async Task<string> GetAsync(string endpoint) {
            string dnsUrl = $"http://{_config.DnsHost}{_config.Path}{endpoint}";
            string ipUrl = $"http://{_config.FallbackIp}{_config.Path}{endpoint}";

            try {
                using var response = await _client.GetAsync(dnsUrl);

                string contenido = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    SivevLogger.Error(
                        $"Error HTTP {(int)response.StatusCode} " +
                        $"({response.ReasonPhrase})\n" +
                        $"URL: {dnsUrl}\n" +
                        $"Respuesta: {contenido}", SivevOrigen.Captura);
                    throw new Exception(
                        $"Error HTTP {(int)response.StatusCode} " +
                        $"({response.ReasonPhrase})\n" +
                        $"URL: {dnsUrl}\n" +
                        $"Respuesta: {contenido}");
                }

                return contenido;
            } catch (HttpRequestException ex)
                  when (ex.StatusCode == null) {
                using var response = await _client.GetAsync(ipUrl);

                string contenido = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    SivevLogger.Error(
                        $"Error HTTP {(int)response.StatusCode} " +
                        $"({response.ReasonPhrase})\n" +
                        $"URL: {ipUrl}\n" +
                        $"Respuesta: {contenido}", SivevOrigen.Captura);
                    throw new Exception(
                        $"Error HTTP {(int)response.StatusCode} " +
                        $"({response.ReasonPhrase})\n" +
                        $"URL: {ipUrl}\n" +
                        $"Respuesta: {contenido}");
                }

                return contenido;
            }
        }

    }
}