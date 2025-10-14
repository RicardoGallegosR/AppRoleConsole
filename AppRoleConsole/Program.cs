using AppRoleConsole.Domain.Models;
using AppRoleConsole.Infrastructure.Config;
using AppRoleConsole.Infrastructure.Security;
using AppRoleConsole.Infrastructure.Sql;
using AppRoleConsole.Infrastructure.SystemInfo;
using Microsoft.Data.SqlClient;


class Program {
    const string SERVER = AppConfig.Sql.Server;
    const string DB = AppConfig.Sql.Database;
    const string SQL_USER = AppConfig.Sql.User;
    const string SQL_PASS = AppConfig.Sql.Pass;

    const string APPROLE = AppConfig.Security.AppRole;
    const string APPROLE_PASS = AppConfig.Security.AppRolePass;

    public static async Task<int> Main() {

        string RollAcceso = "RollVfcVisual";
        string ClaveAcceso = "95801B7A-4577-A5D0-952E-BD3D89757EA5";
        
        var conf = new RegWin();
        var appName = string.IsNullOrWhiteSpace(conf.DomainName) ? "" : conf.DomainName.Trim();

        Guid accesoId = Guid.Empty;
        Guid estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");

        /*
                using var conn = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                conn.Open();

                var repo = new SivevRepository();

                using (var scope = new AppRoleScope(conn, APPROLE, APPROLE_PASS)) {
                    var r = repo.SpAppRollClaveGet(conn);
                    Console.WriteLine($"Return={r.ReturnCode}, MsgId={r.MensajeId}, Res={r.Resultado}");
                    Console.WriteLine($"Func='{r.FuncionAplicacion}', Clave='{r.ClaveAcceso}'");
                    //invertida = new string((r.ClaveAcceso ?? "").Reverse().ToArray());
                    ClaveAcceso = r.ClaveAcceso;
                    RollAcceso = r.FuncionAplicacion;
                }
                conn.Close();
                */


        /*---------------------------VALIDA BITACORA----------------------------------------------------------------
         */
        using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
        await connApp.OpenAsync();

        var repo = new SivevRepository();
        using var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso); 

        try {
            // 1) Abrir acceso
            var r = await repo.SpAppAccesoIniciaAsync( conn:connApp, estacionId:estacionId, opcionMenuId:151, credencial:  16499, password: "PASS1234", huella: null);

            if (r.MensajeId != 0 || r.AccesoId == Guid.Empty) {
                Console.WriteLine($"No se pudo iniciar acceso. MensajeId={r.MensajeId}, Return={r.ReturnCode}");
                return 0; 
            }

            accesoId = r.AccesoId;

            // 2) Visual Ini
            var r2 = await repo.SpAppVerificacionVisualIniAsync(conn:connApp, estacionId: estacionId, accesoId:accesoId);

            Console.WriteLine($"Resultado={r2.Resultado}  MensajeId={r2.MensajeId}");
            Console.WriteLine($"VerificacionId={r2.VerificacionId}");
            Console.WriteLine($"ProtocoloVerificacionId={r2.ProtocoloVerificacionId}");
            Console.WriteLine($"PlacaId={r2.PlacaId}");

            switch (r2.Resultado) {
                case < 0:
                    Console.WriteLine("Flujo con error (<0).");
                    break;

                case 0:
                    Console.WriteLine("Continuar flujo (=0).");
                    break;

                case 1:
                    Console.WriteLine("No hay pruebas");
                    break;
                default:
                    Console.WriteLine($"Otro: {r2.Resultado}");
                    break;
            }
        } catch (Exception ex) {
            Console.WriteLine($"Excepción: {ex.Message}");
        } finally {
            if (accesoId != Guid.Empty) {
                var fin = await repo.SpAppAccesoFinAsync(connApp, estacionId, accesoId);
                Console.WriteLine($"[Fin Acceso] Resultado={fin.Resultado} MensajeId={fin.MensajeId} Return={fin.ReturnCode}");
            }
        }

        /*---------------------------VALIDA SpAppVerificacionVisualIniAsync ----------------------------------------------------------------
         */

        return 0;
    }
}
