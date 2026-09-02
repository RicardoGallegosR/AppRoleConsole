using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace SQLSIVEV.Infrastructure.Utils {
    public static class SivevOrigen {
        public const string Visual = "VISUAL";
        public const string Captura = "CAPTURA";
        public const string Proveedor = "PROVEEDOR";
        public const string Emisiones = "EMISIONES";
        public const string Administrativa = "ADMINISTRATIVA";
        public const string Configurador = "CONFIGURADOR";
    }

    public static class SivevLogger {
        private const string DefaultEventSource = "VISUAL";
        private const string EventLogName = "SIVEV";


        public static bool InicializarOrigen(string origen = DefaultEventSource) {
            try {
                if (!EventLog.SourceExists(origen)) {
                    var data = new EventSourceCreationData(origen, EventLogName);
                    EventLog.CreateEventSource(data);
                }
                return true;
            } catch (Exception ex) {
                Debug.WriteLine(
                    $"No se pudo inicializar el EventLog.\n" +
                    $"Source: {origen}\n" +
                    $"Log: {EventLogName}\n" +
                    $"Error: {ex}"
                );
                return false;
            }
        }


        public static void Information(string mensaje, string origen = DefaultEventSource) {
            Escribir(mensaje, EventLogEntryType.Information, origen);
        }

        public static void Warning(string mensaje,string origen = DefaultEventSource) {
            Escribir(mensaje, EventLogEntryType.Warning, origen);
        }

        public static void Error(string mensaje, string origen = DefaultEventSource) {
            Escribir(mensaje, EventLogEntryType.Error, origen);
        }

        private static void Escribir(string mensaje, EventLogEntryType tipo, string origen) {
            try {
                EventLog.WriteEntry(origen, mensaje, tipo);
            } catch (Exception ex) {
                Debug.WriteLine($"No se pudo escribir en el log SIVEV: {ex.Message}"
                );
            }
        }
    }
}