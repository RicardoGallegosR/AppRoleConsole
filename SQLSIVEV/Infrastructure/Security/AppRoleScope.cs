using Microsoft.Data.SqlClient;
using System.Data;

namespace SQLSIVEV.Infrastructure.Security {
    public sealed class AppRoleScope : IDisposable {
        private readonly SqlConnection _conn;
        private readonly byte[] _cookie;

        public AppRoleScope(SqlConnection conn, string role, string password) {
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_setapprole";
            cmd.Parameters.Add(new SqlParameter("@rolename", SqlDbType.NVarChar, 128) { Value = role });
            cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 128) { Value = password });
            cmd.Parameters.Add(new SqlParameter("@fCreateCookie", SqlDbType.Bit) { Value = true });
            var pCookie = new SqlParameter("@cookie", SqlDbType.VarBinary, 8000) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pCookie);
            cmd.ExecuteNonQuery();
            _cookie = (byte[])(pCookie.Value ?? throw new InvalidOperationException("sp_setapprole no devolvió cookie."));
        }

        public void Dispose() {
            if (_conn.State == ConnectionState.Open && _cookie is not null) {
                try {
                    using var cmd = _conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_unsetapprole";
                    cmd.Parameters.Add(new SqlParameter("@cookie", SqlDbType.VarBinary, 8000) { Value = _cookie });
                    cmd.ExecuteNonQuery();
                } catch { /* swallow */ }
            }
        }
    }
}