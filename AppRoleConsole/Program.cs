using AppRoleConsole.Infrastructure.Config;
using AppRoleConsole.Infrastructure.Security;
using AppRoleConsole.Infrastructure.Sql;
using AppRoleConsole.Infrastructure.SystemInfo;


class Program {
    const string SERVER = AppConfig.Sql.Server;
    const string DB = AppConfig.Sql.Database;
    const string SQL_USER = AppConfig.Sql.User;
    const string SQL_PASS = AppConfig.Sql.Pass;

    const string APPROLE = AppConfig.Security.AppRole;
    const string APPROLE_PASS = AppConfig.Security.AppRolePass;

    static int Main() {
        using var datos = new DatosCpuGet();
        string version = "", idEquipo = "", serie = "";
        if (!datos.DatosC(ref version, ref idEquipo, ref serie)) {
            Console.Error.WriteLine("Error obteniendo datos de CPU: " + datos.DescripcionError);
            return 1;
        }

        Console.WriteLine($"Versión app : {version}");
        Console.WriteLine($"ID equipo   : {idEquipo}");
        Console.WriteLine($"Serie disco : {serie}");

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
        /*-------------------------------------------------------------------------------------------
         */

        Console.WriteLine("Intento de abrir la BDD con los roles originales");

        using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
        connApp.Open();
        var repoApp = new SivevRepository();

        Console.WriteLine($"Credenciales connApp: {connApp}\nRollAcceso: {RollAcceso}\nClaveAcceso:{ClaveAcceso}");

        using (var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso)) {
            var r2 = repoApp.SpAppCredencialExisteHuella(connApp,"BFFF8EA5-76A4-F011-811C-D09466400DBA",151,16499);
            Console.WriteLine($"MensajeId={r2.MensajeId}\nResultado={r2.Resultado}\nExisteHuella={r2.ExisteHuella}\nhuella {r2.Huella}");
            }
        connApp.Close();


        return 0;
    }
}
