namespace SQLSIVEV.Infrastructure.Config;

public static class AppConfig {
    public static class Sql {
        public const string Server   = "192.168.16.8";
        public const string Database = "Sivev";
        public const string User     = "SivevCentros";
        public const string Pass     = "CentrosSivev";
    }

    public static class Security {
        public const string AppRole     = "RollSivev";
        public const string AppRolePass = "53CE7B6E-1426-403A-857E-A890BB63BFE6";
    }
}
