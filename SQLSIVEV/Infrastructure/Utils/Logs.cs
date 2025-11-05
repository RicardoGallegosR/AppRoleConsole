using System.Runtime.CompilerServices;
using Serilog;

namespace SQLSIVEV.Infrastructure.Utils {
    public static class Logs {
        public static void Error(ErrorProcesoArgs args, Exception? ex = null) {
            var logger = Log
                .ForContext("Libreria", args.Libreria)
                .ForContext("Clase",    args.Clase)
                .ForContext("Metodo",   args.Metodo)
                .ForContext("ErrNum",   args.ErrNum)
                .ForContext("ErrDesc",  args.ErrDesc)
                .ForContext("SqlErr",   args.SqlErr);

            if (ex is null)
                logger.Error("{ErrDesc}");
            else
                logger.Error(ex, "{ErrDesc}");
        }

        // === Tus métodos anteriores (se pueden quedar) ===
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
    }
}
