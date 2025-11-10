using Serilog;
using Serilog.Events;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Serilog.Sinks.EventLog;
using Serilog.Events;


namespace SQLSIVEV.Infrastructure.Utils {
    public static class Logs {
        public static void Error(ErrorProcesoArgs args, Exception? ex = null) {
            var where = $"[{args.Libreria}.{args.Clase}.{args.Metodo}]";
            var logger = Log
                .ForContext("Libreria", args.Libreria)
                .ForContext("Clase",    args.Clase)
                .ForContext("Metodo",   args.Metodo)
                .ForContext("ErrNum",   args.ErrNum)
                .ForContext("ErrDesc",  args.ErrDesc)
                .ForContext("SqlErr",   args.SqlErr)
                .ForContext("Where",    where);   // <<--- clave

            var msg = args.ToString();       
            if (ex is null) logger.Error("{Msg}", msg);
            else logger.Error(ex, "{Msg}", msg);
        }
        public static void Info(object? sender,
            string errDesc,
            int errNum = 0,
            int? sqlErr = null,
            [CallerMemberName] string metodo = "",
            [CallerFilePath] string filePath = "") {
            Write(sender, errDesc, errNum, sqlErr, metodo, filePath, ex: null);
        }

        public static void Error(object? sender,
            string errDesc,
            int errNum = 0,
            int? sqlErr = null,
            Exception? ex = null,
            [CallerMemberName] string metodo = "",
            [CallerFilePath] string filePath = "") {
            Write(sender, errDesc, errNum, sqlErr, metodo, filePath, ex);
        }

        private static void Write(object? sender, string errDesc, int errNum, int? sqlErr,
            string metodo, string filePath, Exception? ex) {
            var clase = Path.GetFileNameWithoutExtension(filePath);
            var tipo = sender?.GetType();
            var libreria = tipo?.Assembly?.GetName()?.Name ?? "";
            var formulario = TryGetNameProperty(sender) ?? tipo?.Name ?? "";

            var logger = Log.ForContext("Formulario", formulario)
                            .ForContext("Libreria",   libreria)
                            .ForContext("Clase",      clase)
                            .ForContext("Metodo",     metodo)
                            .ForContext("ErrNum",     errNum)
                            .ForContext("ErrDesc",    errDesc)
                            .ForContext("SqlErr",     sqlErr);

            if (ex is null)
                logger.Information("{ErrDesc}");
            else
                logger.Error(ex, "{ErrDesc}");
        }

        private static string? TryGetNameProperty(object? sender) {
            if (sender is null) return null;
            var prop = sender.GetType().GetProperty("Name");
            return prop?.GetValue(sender)?.ToString();
        }


        public static string Init(string runId, string metodo, string? forcedDir = @"C:\LogsSMA") {
            var primary   = forcedDir;
            var secondary = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LogsSMA");
            var tertiary  = Path.Combine(Path.GetTempPath(), "LogsSMA");

            var logDir = EnsureWritableDirectory(primary)
              ?? EnsureWritableDirectory(secondary)
              ?? EnsureWritableDirectory(tertiary)
              ?? AppContext.BaseDirectory;

            // (Opcional) SelfLog solo en pruebas
            // var selfLogFile = Path.Combine(logDir, "serilog-selflog.txt");
            // Serilog.Debugging.SelfLog.Enable(msg => { try { File.AppendAllText(selfLogFile, msg + Environment.NewLine, Encoding.UTF8); } catch { } });

            var logFile = Path.Combine(logDir, $"sma-{runId}.log"); // 1 archivo por ejecución

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("App", metodo)
                .Enrich.WithProperty("Machine", Environment.MachineName)
                .Enrich.WithProperty("UserShort", Environment.UserName.ToUpperInvariant())
                .Enrich.WithProperty("RunId", runId)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    path: logFile,
                    rollingInterval: RollingInterval.Infinite, // sin rotación por día
                    shared: true,
                    encoding: Encoding.UTF8,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} | {Level:u3} | {Where} | {UserShort} | {Message:lj}{NewLine}{Exception}"
                )
                //*
                 .WriteTo.EventLog(
                    source: "SIVEV-Visual",
                    logName: "SIVEV",
                    manageEventSource: true,
                    restrictedToMinimumLevel: LogEventLevel.Information
                )//*/
                .CreateLogger();

            Log.Information("Serilog inicializado. Dir={LogDir} RunId={RunId}", logDir, runId);
            return logDir;
        }

        private static string? EnsureWritableDirectory(string? dir) {
            if (string.IsNullOrWhiteSpace(dir)) return null;
            try {
                Directory.CreateDirectory(dir);
                var probe = Path.Combine(dir, ".probe");
                File.WriteAllText(probe, "ok");
                File.Delete(probe);
                return dir;
            } catch { return null; }
        }

        // Helpers opcionales
        public static string Mask(string? v, bool secret = true)
            => string.IsNullOrEmpty(v) ? "" : (secret ? (v.Length <= 6 ? "***" : $"{v[..3]}****{v[^3..]}") : v);
    }
















}

