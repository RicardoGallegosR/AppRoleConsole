using Microsoft.Data.SqlClient;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Security;

namespace SQLSIVEV.Infrastructure.Utils;

public static class MessageUtils {
    /// Imprime el catálogo si MensajeId != 0. 
    /// Si pasas appRole/appRolePass, abre AppRoleScope (sin await) solo para esta llamada.
    /// 
//    using var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso);


public static async Task<AppTextoMensajeResult?> PrintIfMsgAsync( this SivevRepository repo, SqlConnection conn, string contexto, int mensajeId, bool soloSiHayError = true, string? appRole = null,
                                                                        string appRolePass = "",  CancellationToken ct = default) {
        if (soloSiHayError && mensajeId == 0)
            return null;
        if (!string.IsNullOrWhiteSpace(appRole)) {
            using var _scope = new AppRoleScope(conn, appRole, appRolePass);
            var msgScoped = await repo.SpAppTextoMensajeGetAsync(conn: conn, mensajeId: mensajeId, ct);
            SivevLogger.Error($"\n{contexto}, code {mensajeId}_: {msgScoped.Mensaje}");
            return msgScoped;
        }

        var msg = await repo.SpAppTextoMensajeGetAsync(conn: conn, mensajeId: mensajeId, ct);
        SivevLogger.Error($"\n{contexto}, code {mensajeId}: {msg.Mensaje}");
        return msg;
    }
}
