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
        connApp.Open();




        var repo = new SivevRepository();
        conf.EstacionId = "BFFF8EA5-76A4-F011-811C-D09466400DBA";
        // Si tu SP requiere AppRole, activa el scope aquí.
        Console.WriteLine($"Estacion{conf.EstacionId}\nServer: {SERVER}\nBDD: {DB}\nuser: {SQL_USER}\nPass: {SQL_PASS}\napp: {appName}\nappRole: {RollAcceso}\nRolePass: {ClaveAcceso}");
        using (var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso)) {
            var estId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA"); // tu estación



            var r = await repo.SpAppAccesoIniciaAsync(
                    conn:        connApp,
                    estacionId:  estId,
                    opcionMenuId:151,
                    credencial:  16499,
                    password:    "PASS1234",
                    huella:      null
                );
            Console.WriteLine($"Return={r.ReturnCode}");
            Console.WriteLine($"MensajeId={r.MensajeId}");
            Console.WriteLine($"AccesoId={r.AccesoId}");

        }

        /*---------------------------VALIDA ESTACION----------------------------------------------------------------
         */


        return 0;
    }
}
