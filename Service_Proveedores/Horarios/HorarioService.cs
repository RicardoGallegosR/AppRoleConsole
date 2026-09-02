using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service_Proveedores.Horarios {
    public class EstadoSincronizacion {
        public bool ServicioActivo { get; set; }
        public string Origen { get; set; } = "No disponible";
        public string UltimaSincronizacion { get; set; } = "No disponible";
        public bool Sincronizado { get; set; }
        public string Diferencia { get; set; } = "No disponible";
    }

    public static class HorarioService {
        public static async Task<string> ObtenerOrigenAsync() {
            var resultado = await EjecutarAsync("w32tm.exe", "/query /source");
            return resultado.ExitCode == 0 ? resultado.Output.Trim() : "No disponible";
        }

        public static async Task<string> ObtenerEstadoAsync() {
            var resultado = await EjecutarAsync("w32tm.exe", "/query /status");
            return resultado.ExitCode == 0 ? resultado.Output.Trim() : resultado.Error.Trim();
        }

        public static async Task<string> ResincronizarAsync() {
            var resultado = await EjecutarAsync("w32tm.exe","/resync /rediscover");
            if (resultado.ExitCode == 0)
                return resultado.Output.Trim();

            return string.IsNullOrWhiteSpace(resultado.Error)
                ? resultado.Output.Trim()
                : resultado.Error.Trim();
        }

        public static async Task<string> EstablecerZonaCdmxAsync() {
            var resultado = await EjecutarAsync("tzutil.exe", "/s \"Central Standard Time (Mexico)\"");

            if (resultado.ExitCode == 0)
                return "OK";

            return string.IsNullOrWhiteSpace(resultado.Error) ? "ERROR" : resultado.Error.Trim();
        }

        public static async Task<EstadoSincronizacion> ObtenerEstadoSincronizacionAsync() {

            EstadoSincronizacion estado = new();

            // ─────────────────────────────────
            // Servicio W32Time
            // ─────────────────────────────────
            try {
                using ServiceController servicio = new("w32time");
                estado.ServicioActivo = servicio.Status == ServiceControllerStatus.Running;
            } catch {
                estado.ServicioActivo = false;
            }

            // ─────────────────────────────────
            // Origen
            // ─────────────────────────────────
            var source = await EjecutarAsync("w32tm.exe", "/query /source");

            if (source.ExitCode == 0) {
                estado.Origen = source.Output.Trim();
            } else {
                estado.Origen = "No disponible";
            }

            // ─────────────────────────────────
            // Estado W32Time
            // ─────────────────────────────────
            var status = await EjecutarAsync("w32tm.exe","/query /status");
            estado.Sincronizado = estado.ServicioActivo && source.ExitCode == 0 && status.ExitCode == 0 && !EsRelojLocal(estado.Origen);
            estado.UltimaSincronizacion =ObtenerUltimaSincronizacion(status.Output);

            // ─────────────────────────────────
            // Diferencia contra origen
            // ─────────────────────────────────
            if (estado.Sincronizado) {
                estado.Diferencia = await ObtenerDiferenciaAsync(estado.Origen);
            }
            return estado;
        }

        private static async Task<string> ObtenerDiferenciaAsync(string origen) {
            var resultado = await EjecutarAsync("w32tm.exe", $"/stripchart /computer:\"{origen}\" /dataonly /samples:1");

            if (resultado.ExitCode != 0)
                return "No disponible";

            Match match = Regex.Match(resultado.Output, @"([+-]\d+[.,]\d+)\s*s", RegexOptions.IgnoreCase);

            if (!match.Success)
                return "No disponible";

            string valorTexto = match.Groups[1].Value.Replace(',', '.');

            if (!double.TryParse(valorTexto,NumberStyles.Float,CultureInfo.InvariantCulture, out double segundos)) {
                return "No disponible";
            }
            return $"{segundos:+0.000;-0.000;0.000} segundos";
        }

        private static bool EsRelojLocal(string origen) {

            if (string.IsNullOrWhiteSpace(origen))
                return true;

            string texto = origen.ToLowerInvariant();

            return
                texto.Contains("local cmos") ||
                texto.Contains("free-running") ||
                texto.Contains("reloj cmos") ||
                texto.Contains("reloj local") ||
                texto.Contains("no disponible");
        }

        private static string ObtenerUltimaSincronizacion(
            string status) {

            if (string.IsNullOrWhiteSpace(status))
                return "No disponible";

            string[] lineas = status.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string linea in lineas) {

                // Inglés
                if (linea.Contains("Last Successful Sync Time",StringComparison.OrdinalIgnoreCase)) {
                    return ObtenerValorLinea(linea);
                }

                // Español.
                // Usamos "sincronizaci" para que funcione
                // incluso si los acentos llegan mal codificados.
                if (linea.Contains("sincronizaci", StringComparison.OrdinalIgnoreCase) && linea.Contains("correcta",StringComparison.OrdinalIgnoreCase)) {
                    return ObtenerValorLinea(linea);
                }
            }

            return "No disponible";
        }

        private static string ObtenerValorLinea(string linea) {
            int pos = linea.IndexOf(':');
            if (pos < 0 || pos >= linea.Length - 1)
                return "No disponible";
            return linea[(pos + 1)..].Trim();
        }

        private static async Task<(int ExitCode,string Output,string Error)>EjecutarAsync(string archivo,string argumentos) {
            ProcessStartInfo psi = new() {
                FileName = archivo,
                Arguments = argumentos,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using Process proceso = new() {
                StartInfo = psi
            };

            proceso.Start();
            string output =await proceso.StandardOutput.ReadToEndAsync();
            string error = await proceso.StandardError.ReadToEndAsync();
            await proceso.WaitForExitAsync();
            return (proceso.ExitCode, output, error);
        }


        public static async Task<string> DesactivarHorarioVeranoAsync() {
            const string zona = "Central Standard Time (Mexico)";
            try {
                using RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\TimeZoneInformation");

                object? valor = key?.GetValue("DynamicDaylightTimeDisabled");
                bool yaDesactivado = valor is int i && i == 1;

                if (yaDesactivado)
                    return "YA_DESACTIVADO";
            } catch {
                // Si no podemos determinarlo, tzutil lo establecerá.
            }
            var resultado = await EjecutarAsync("tzutil.exe", $"/s \"{zona}_dstoff\"");
            if (resultado.ExitCode != 0) {
                return string.IsNullOrWhiteSpace(resultado.Error)
                    ? "ERROR"
                    : resultado.Error.Trim();
            }
            return "DESACTIVADO";
        }

        public static async Task<string> AlternarHorarioVeranoAsync() {
            const string zona = "Central Standard Time (Mexico)";
            bool deshabilitado = false;
            try {
                using RegistryKey? key = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Control\TimeZoneInformation");
                object? valor = key?.GetValue("DynamicDaylightTimeDisabled");
                deshabilitado = valor is int i && i == 1;
            } catch {
                // Si no podemos determinarlo, dejamos false
            }
            string argumentos;
            if (deshabilitado) {
                // Actualmente está deshabilitado → activarlo
                argumentos = $"/s \"{zona}\"";
            } else {
                // Actualmente está habilitado → desactivarlo
                argumentos = $"/s \"{zona}_dstoff\"";
            }
            var resultado = await EjecutarAsync("tzutil.exe", argumentos);
            if (resultado.ExitCode != 0) {
                return string.IsNullOrWhiteSpace(resultado.Error)
                    ? "ERROR"
                    : resultado.Error.Trim();
            }
            return deshabilitado
                ? "ACTIVADO"
                : "DESACTIVADO";
        }









    }
}
