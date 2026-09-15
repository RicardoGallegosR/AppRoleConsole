using Microsoft.Data.SqlClient;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using static SQLSIVEV.Infrastructure.Config.AppConfig;

namespace SQLSIVEV.Infrastructure.Sql {
    public sealed class AppRoleSqlExecutor {
        private readonly SivevConnectionFactory _sql;
        private readonly string _roll;
        private readonly string _passRoll;

        public AppRoleSqlExecutor(SivevConnectionFactory sql,  string roll,  string passRoll) {
            _sql = sql ??   throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = passRoll ??  throw new ArgumentNullException(nameof(passRoll));
        }

        public async Task EjecutarAsync(Func<SqlConnection, Task> accion, CancellationToken ct = default) {
            ArgumentNullException.ThrowIfNull(accion);
            ct.ThrowIfCancellationRequested();
            await using var session = await _sql.OpenSessionAsync( 
                new AppRoleConfig {
                    Nombre = _roll,
                    Password = _passRoll,
                    Habilitado = true
                });
            ct.ThrowIfCancellationRequested();
            await accion(session.Connection);
        }

        public async Task<T> EjecutarAsync<T>(Func<SqlConnection, Task<T>> accion, CancellationToken ct = default) {
            ArgumentNullException.ThrowIfNull(accion);

            ct.ThrowIfCancellationRequested();

            await using var session =  await _sql.OpenSessionAsync(
                new AppRoleConfig {
                    Nombre = _roll,
                    Password = _passRoll,
                    Habilitado = true
                });
            ct.ThrowIfCancellationRequested();
            return await accion(session.Connection);
        }
    }
}