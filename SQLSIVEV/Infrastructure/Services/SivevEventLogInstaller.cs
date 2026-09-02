using System.Diagnostics;


namespace SQLSIVEV.Infrastructure.Services {
    public static class SivevEventLogInstaller {

        public const string LogName = "SIVEV";

        public static readonly string[] Sources = {
            "VISUAL",
            "CAPTURA",
            "PROVEEDOR",
            "EMISIONES",
            "ADMINISTRATIVA",
            "CONFIGURADOR"
        };

        public static bool TodasLasFuentesExisten() {
            try {
                return Sources.All(EventLog.SourceExists);
            } catch {
                return false;
            }
        }

        public static List<string> ObtenerFuentesFaltantes() {
            List<string> faltantes = new();
            foreach (string source in Sources) {
                if (!EventLog.SourceExists(source)) {
                    faltantes.Add(source);
                }
            }
            return faltantes;
        }

        public static bool CrearFuentes(out string mensaje) {
            try {
                List<string> creadas = new();
                foreach (string source in Sources) {
                    if (EventLog.SourceExists(source))
                        continue;

                    EventSourceCreationData data = new(source, LogName);
                    EventLog.CreateEventSource(data);
                    creadas.Add(source);
                }
                mensaje = creadas.Count == 0
                    ? "Las bitácoras ya estaban configuradas."
                    : $"Bitácoras creadas correctamente: {string.Join(", ", creadas)}";
                return true;
            } catch (Exception ex) {
                mensaje = $"No fue posible configurar las bitácoras.\n\n{ex.Message}";
                return false;
            }
        }
    }
}
