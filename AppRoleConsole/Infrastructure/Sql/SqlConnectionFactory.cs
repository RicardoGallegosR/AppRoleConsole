namespace AppRoleConsole.Infrastructure.Sql;
using Microsoft.Data.SqlClient;

public static class SqlConnectionFactory {
    public static SqlConnection Create(string server, string db, string user, string pass, string appName) {
        var csb = new SqlConnectionStringBuilder
        {
            DataSource = server,
            InitialCatalog = db,
            UserID = user,
            Password = pass,
            Encrypt = false,
            TrustServerCertificate = true,
            ApplicationName = appName
        };
        return new SqlConnection(csb.ConnectionString);
    }
}
