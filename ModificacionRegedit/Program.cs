using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Config.Cifrados;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Sql;
using System.Diagnostics;
using System.Security.Principal;


class Program {
    public static async Task<int> Main() {

        var ev = new estacionVisual  {
            SqlServerName = "SIVSRV9915",
            BaseDatos     = "SIVEV",
            SQL_USER      = "SivevCentros",
            SQL_PASS      = "CentrosSivev",
            APPNAME       = "SivAppVfcVisual",
            APPROLE       = "RollSivev",
            APPROLE_PASS  = "53CE7B6E-1426-403A-857E-A890BB63BFE6",//"53CE7B6E-1426-403A-857E-A890BB63BFE6"
            EstacionId    = "BFFF8EA5-76A4-F011-811C-D09466400DBA",
            opcionMenu    = 151,
            // Estos no van en el regEdit
            APPROLE_VISUAL        = "RollVfcVisual",
            APPROLE_PASS_VISUAL   = "95801B7A-4577-A5D0-952E-BD3D89757EA5",
            ClaveAccesoId   =      string.Empty
        };
        if (!EsAdministrador()) {
            var psi = new ProcessStartInfo
    {
                FileName = Environment.ProcessPath!,
                UseShellExecute = true,
                Verb = "runas" // eleva
            };
            Process.Start(psi);
            return 0; // salir del no-elevado
        }

        if (EsAdministrador()) {
            var reg = new RegWin();
            reg.ActualizaLlavesVisual(); // asegura subclave
            // ahora sí persistes asignando
            reg.SqlServerName = ev.SqlServerName;
            reg.BaseSql = ev.BaseDatos;
            reg.SQL_USER = ev.SQL_USER;
            reg.SQL_PASS = ev.SQL_PASS;
            reg.APPNAME = ev.APPNAME;
            reg.APPROLE = ev.APPROLE;
            reg.APPROLE_PASS = ev.APPROLE_PASS;
            reg.APPROLE_VISUAL = ev.APPROLE_VISUAL;      // setter persistente
            reg.APPROLE_PASS_VISUAL = ev.APPROLE_PASS_VISUAL; // setter persistente
            reg.ClaveAccesoId = ev.ClaveAccesoId;
            reg.EstacionId = ev.EstacionId;
            reg.OpcionMenuId = ev.opcionMenu;
        } else {
            Console.WriteLine("Ejecuta elevado o implementa fallback a HKCU.");
        }


        return 0;
    }

    private static bool EsAdministrador() {
        using var id = System.Security.Principal.WindowsIdentity.GetCurrent();
        var p = new System.Security.Principal.WindowsPrincipal(id);
        return p.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }
}
