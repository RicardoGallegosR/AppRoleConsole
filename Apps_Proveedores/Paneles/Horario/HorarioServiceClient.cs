using Apps_Proveedores.Modelos;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace Apps_Proveedores.Paneles.Horario {
    public static class HorarioServiceClient {

        private const string PipeName = "SIVEV_Proveedores";
        private const int TimeoutMs = 3000;

        private static readonly Encoding Utf8 =
        new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        public static Task<string> PingAsync() {
            return EnviarAsync("PING");
        }

        public static Task<string> ObtenerOrigenAsync() {
            return EnviarAsync("HORARIO_GET_SOURCE");
        }

        public static Task<string> EstablecerZonaCdmxAsync() {
            return EnviarAsync("HORARIO_SET_TIMEZONE");
        }
        public static async Task<EstadoSincronizacion?> ObtenerEstadoAsync() {
            string json = await EnviarAsync("HORARIO_GET_STATUS");
            return JsonSerializer.Deserialize<EstadoSincronizacion>(
                json
            );
        }
        public static Task<string> DesactivarHorarioVeranoAsync() {
            return EnviarAsync("HORARIO_DST_DISABLE");
        }

        public static Task<string> ResincronizarAsync() {
            return EnviarAsync("HORARIO_RESYNC");
        }
        public static Task<string> CorregirHoraYSincronizarAsync() {
            return EnviarAsync("HORARIO_CORREGIR");
        }
        private static async Task<string> EnviarAsync(string accion, CancellationToken cancellationToken = default) {
            await using NamedPipeClientStream pipe = new(serverName: ".", pipeName: PipeName, direction: PipeDirection.InOut, options: PipeOptions.Asynchronous);
            await pipe.ConnectAsync(TimeoutMs, cancellationToken);
            using StreamReader reader = new(pipe,Utf8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true);
            using StreamWriter writer = new(pipe, Utf8,                                         bufferSize: 1024, leaveOpen: true) {
                AutoFlush = true
            };
            await writer.WriteLineAsync(accion);
            string? respuesta = await reader.ReadLineAsync(cancellationToken);
            return respuesta ?? "SIN_RESPUESTA";
        }
    }
}
