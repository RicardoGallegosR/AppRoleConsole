using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Sql.Vicente {
    public sealed class SivevConnectionFactory {
        private readonly string _connectionString;
        public SivevConnectionFactory(string connectionString) {
            _connectionString = connectionString;
        }
        public SqlConnection Create() {
            return new SqlConnection(_connectionString);
        }
        public async Task<SqlConnection> OpenAsync(CancellationToken cancellationToken = default) {
            SqlConnection conn = Create();
            try {
                await conn.OpenAsync(cancellationToken);
                return conn;
            } catch {
                await conn.DisposeAsync();
                throw;
            }
        }
        public async Task<SivevDbSession> OpenSessionAsync(AppRoleConfig? appRole = null, CancellationToken cancellationToken = default) {

            SqlConnection conn = await OpenAsync(cancellationToken);
            var session = new SivevDbSession(conn, appRole);

            try {
                await session.AplicarAppRoleAsync(cancellationToken);
                return session;
            } catch {
                await session.DisposeAsync();
                throw;
            }
        }
    }
    public sealed class AppRoleConfig {
        public string Nombre { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Habilitado { get; set; } = false;
    }
    public sealed class SivevDbSession : IAsyncDisposable {
        private byte[]? _cookie;
        private readonly AppRoleConfig? _appRole;
        public SqlConnection Connection { get; }

        internal SivevDbSession(SqlConnection connection, AppRoleConfig? appRole) {
            Connection = connection;
            _appRole = appRole;
        }

        internal async Task AplicarAppRoleAsync(CancellationToken cancellationToken) {

            if (_appRole == null || !_appRole.Habilitado)
                return;

            using SqlCommand cmd = new("sys.sp_setapprole", Connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rolename", _appRole.Nombre);
            cmd.Parameters.AddWithValue("@password", _appRole.Password);
            cmd.Parameters.AddWithValue("@fCreateCookie", true);

            SqlParameter cookie = cmd.Parameters.Add("@cookie", SqlDbType.VarBinary,  8000);
            cookie.Direction = ParameterDirection.Output;
            await cmd.ExecuteNonQueryAsync(cancellationToken);
            _cookie = cookie.Value as byte[];
        }

        private async Task QuitarAppRoleAsync() {
            if (_cookie == null)
                return;

            using SqlCommand cmd = new("sys.sp_unsetapprole", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cookie", SqlDbType.VarBinary, 8000).Value = _cookie;
            await cmd.ExecuteNonQueryAsync();
            _cookie = null;
        }

        public async ValueTask DisposeAsync() {
            try {
                if (Connection.State == ConnectionState.Open) {
                    await QuitarAppRoleAsync();
                }
            } finally {
                await Connection.DisposeAsync();
            }
        }
    }
}
