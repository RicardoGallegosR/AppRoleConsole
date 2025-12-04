using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Utils {
    public static class SivevLogger {
        private const string EventSource   = "VISUAL";
        private const string EventLogName  = "SIVEV"; // Solo referencia, por claridad

        public static void Information(string mensaje) {
            Escribir(mensaje, EventLogEntryType.Information);
        }

        public static void Warning(string mensaje) {
            Escribir(mensaje, EventLogEntryType.Warning);
        }

        public static void Error(string mensaje) {
            Escribir(mensaje, EventLogEntryType.Error);
        }

        private static void Escribir(string mensaje, EventLogEntryType tipo) {
            try {
                EventLog.WriteEntry(EventSource, mensaje, tipo);
            } catch (Exception ex) {
                Debug.WriteLine($"No se pudo escribir en el log SIVEV: {ex.Message}");
            }
        }
    }
}

