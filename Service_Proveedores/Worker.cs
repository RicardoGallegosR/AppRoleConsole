using Service_Proveedores.Horarios;
using SQLSIVEV.Infrastructure.Utils;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Service_Proveedores;

public class Worker : BackgroundService {
    private const string EventSource = "Service_Proveedores";
    private const string PipeName = "SIVEV_Proveedores";

    protected override async Task ExecuteAsync( CancellationToken stoppingToken) {
        SivevLogger.Information("Servicio iniciado :D", EventSource);

        while (!stoppingToken.IsCancellationRequested) {
            try {
                await EsperarClienteAsync(stoppingToken);
            } catch (OperationCanceledException) {
                break;
            } catch (Exception ex) {
                SivevLogger.Error($"Error en Service_Proveedores: {ex.Message}", EventSource);
            }
        }
        SivevLogger.Information("Servicio detenido :(", EventSource);
    }

    private static async Task EsperarClienteAsync(CancellationToken cancellationToken) {
        Encoding utf8 = new UTF8Encoding(false);
        PipeSecurity seguridad = new();
        SecurityIdentifier everyone = new(WellKnownSidType.WorldSid, null);
        seguridad.AddAccessRule(new PipeAccessRule(everyone, PipeAccessRights.ReadWrite, AccessControlType.Allow));
        await using NamedPipeServerStream pipe = NamedPipeServerStreamAcl.Create(PipeName,PipeDirection.InOut,1,PipeTransmissionMode.Byte,PipeOptions.Asynchronous,1024,1024,seguridad);
        SivevLogger.Information("Esperando cliente...", EventSource);
        await pipe.WaitForConnectionAsync(cancellationToken);
        SivevLogger.Information("Cliente conectado.", EventSource);

        using StreamReader reader = new(pipe,utf8, false, 1024, leaveOpen: true);
        using StreamWriter writer = new(pipe,utf8,        1024, leaveOpen: true) {
            AutoFlush = true
        };
        string? solicitud = await reader.ReadLineAsync(cancellationToken);
        string accion = solicitud?.Trim().ToUpperInvariant() ?? "";

        string respuesta = accion switch {
            "PING" => "PONG",
            "HORARIO_GET_SOURCE" => await HorarioService.ObtenerOrigenAsync(),
            "HORARIO_GET_STATUS" => JsonSerializer.Serialize( await HorarioService.ObtenerEstadoSincronizacionAsync()),
            "HORARIO_RESYNC" => await HorarioService.ResincronizarAsync(),
            "HORARIO_SET_TIMEZONE" => await HorarioService.EstablecerZonaCdmxAsync(),
            "HORARIO_CORREGIR" => await HorarioService.DesactivarHorarioVeranoAsync(),
            "HORARIO_DST_TOGGLE" => await HorarioService.AlternarHorarioVeranoAsync(),
            "HORARIO_DST_DISABLE" => await HorarioService.DesactivarHorarioVeranoAsync(),
            _
        => "ACCION_NO_RECONOCIDA"
        };
        await writer.WriteLineAsync(respuesta);
        SivevLogger.Information($"Solicitud: {accion} | Respuesta: {respuesta}", EventSource);
    }
}