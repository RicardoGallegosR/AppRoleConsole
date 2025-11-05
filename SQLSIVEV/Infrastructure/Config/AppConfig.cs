
namespace SQLSIVEV.Infrastructure.Config {

    public static class AppConfig {

        public static class Sql {

            public const string Server   = "192.168.16.8";
            public const string Database = "Sivev";
            public const string User     = "SivevCentros";
            public const string Pass     = "CentrosSivev";

        }


        public class CredencialesRegEdit {

            public static int OpcionMenu() { return new RegWin().OpcionMenuId; }

            public static Guid estacionId() { return new RegWin().EstacionId; }

            public static string SqlServerName() { return new RegWin().SqlServerName; }
            public static string BaseSql() { return new RegWin().BaseSql; }
            public static string appName() { return new RegWin().DomainName; }






        }

        public class Security {
            public const string _AppRole     = "RollSivev";
            public const string _AppRolePass = "53CE7B6E-1426-403A-857E-A890BB63BFE6";

            public static string AppRole() => new RegWin().AppRole;

            public static string AppRolePass() => new RegWin().AppRolePass;
        }










    }
}