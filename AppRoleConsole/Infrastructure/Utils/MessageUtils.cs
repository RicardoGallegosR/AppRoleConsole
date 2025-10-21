// Infrastructure/Utils/MessageUtils.cs
using Microsoft.Data.SqlClient;
using AppRoleConsole.Domain.Models;
using AppRoleConsole.Infrastructure.Sql;
using AppRoleConsole.Infrastructure.Security;

namespace AppRoleConsole.Infrastructure.Utils;

public static class MessageUtils {
    /// Imprime el catálogo si MensajeId != 0. 
    /// Si pasas appRole/appRolePass, abre AppRoleScope (sin await) solo para esta llamada.
    /// 
//    using var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso);


public static async Task<AppTextoMensajeResult?> PrintIfMsgAsync( this SivevRepository repo, SqlConnection conn, string contexto, int mensajeId, bool soloSiHayError = true, string? appRole = null,
                                                                        string? appRolePass = null,  CancellationToken ct = default) {
        if (soloSiHayError && mensajeId == 0)
            return null;

        if (!string.IsNullOrWhiteSpace(appRole)) {

            using var _scope = new AppRoleScope(conn, appRole, appRolePass);
            var msgScoped = await repo.SpAppTextoMensajeGetAsync(conn: conn, mensajeId: mensajeId, ct);
            Console.WriteLine($"\n{contexto}, code {mensajeId}: {msgScoped.Mensaje}");
            return msgScoped;
        }

        var msg = await repo.SpAppTextoMensajeGetAsync(conn: conn, mensajeId: mensajeId, ct);
        Console.WriteLine($"\n{contexto}, code {mensajeId}: {msg.Mensaje}");
        return msg;
    }
}
