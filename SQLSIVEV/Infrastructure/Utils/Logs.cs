using SQLSIVEV.Infrastructure.Devices.Obd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Utils {
    public sealed class Logs {
        public static string CrearRutaLogObd(string placa, string verificacionId, string centroServidor) {
            string servidor = $"SIVSRV{centroServidor}";
            string rutaServidor = $@"\\{servidor}\SIVEV_LogsOBD";
            string rutaLocal = @"C:\SIVEV\LogsOBD";
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            string hora = DateTime.Now.ToString("HHmmss");

            placa = LimpiarNombreArchivo(placa).ToUpperInvariant();
            verificacionId = LimpiarNombreArchivo(verificacionId).ToUpperInvariant();

            try {
                string carpetaServidor = Path.Combine(rutaServidor, fecha);
                Directory.CreateDirectory(carpetaServidor);

                return Path.Combine(carpetaServidor, $"{placa}_{verificacionId}_{hora}.txt");
            } catch (Exception ex){
                SivevLogger.Warning($"Error al crear ruta de log en servidor: {ex.Message}");
                ObdTxtLogger.LimpiarLogsAntiguos(rutaLocal, 15);
                string carpetaLocal = Path.Combine(rutaLocal, fecha);
                Directory.CreateDirectory(carpetaLocal);

                return Path.Combine( carpetaLocal, $"{placa}_{verificacionId}_{hora}_LOCAL.txt" );
            }
        }

        private static string LimpiarNombreArchivo(string texto) {
            if (string.IsNullOrWhiteSpace(texto))
                return "SIN_DATO";

            foreach (char c in Path.GetInvalidFileNameChars()) {
                texto = texto.Replace(c, '_');
            }
            return texto.Trim();
        }
    }
}
