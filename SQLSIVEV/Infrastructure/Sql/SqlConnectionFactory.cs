using Microsoft.Data.SqlClient;

namespace SQLSIVEV.Infrastructure.Sql {
    public static class SqlConnectionFactory {
        public static SqlConnection Create(string server, string db, string user, string pass, string appName,
            int minPool = 0, int maxPool = 100, int connectTimeout = 15, int loadBalanceTimeout = 0) {
            var csb = new SqlConnectionStringBuilder {
                DataSource = server,
                InitialCatalog = db,
                UserID = user,
                Password = pass,
                Encrypt = false,
                TrustServerCertificate = true,
                ApplicationName = appName,


                // Seguridad / TLS
                PersistSecurityInfo = false,

                // Pooling
                Pooling = true,                 // (default = true, lo pongo explícito)
                MinPoolSize = minPool,          // default 0
                MaxPoolSize = maxPool,          // default 100
                ConnectTimeout = connectTimeout,// segundos
                LoadBalanceTimeout = loadBalanceTimeout, // tiempo máx (seg) que una conexión queda en el pool (0 = sin límite)
                MultipleActiveResultSets = false // pon true si de verdad lo necesitas


            };
            return new SqlConnection(csb.ConnectionString);
        }
    }
}