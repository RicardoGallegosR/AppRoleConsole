namespace SQLSIVEV.Infrastructure.Config {
    public static class AppConfig {
        // Reutiliza una sola instancia
        private static readonly Lazy<RegWin> _reg = new(() => new RegWin());
        private static RegWin Reg => _reg.Value;

        public static class Sql {
            public static string SqlServerName() => Reg.SqlServerName;
            public static string BaseSql() => Reg.BaseSql;
            public static string SQL_USER() => Reg.SQL_USER;
            public static string SQL_PASS() => Reg.SQL_PASS;
            public static string APPNAME() => Reg.APPNAME;
        }

        public static class RollInicial {
            public static string APPROLE() => Reg.APPROLE;
            public static string APPROLE_PASS() => Reg.APPROLE_PASS;
        }

        public static class RollVisual {
            public static string APPROLE_VISUAL() => Reg.APPROLE_VISUAL;
            public static string APPROLE_PASS_VISUAL() => Reg.APPROLE_PASS_VISUAL;
        }

        public static class CredencialesRegEdit {
            public static short OpcionMenu() => Reg.OpcionMenuId;
            public static string EstacionId() => Reg.EstacionId;
            public static string ClaveAccesoId() => Reg.ClaveAccesoId;
        }
    }
}
