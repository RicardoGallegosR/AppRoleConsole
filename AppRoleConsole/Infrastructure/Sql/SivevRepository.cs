using Microsoft.Data.SqlClient;
using System.Data;
using AppRoleConsole.Domain.Models;


namespace AppRoleConsole.Infrastructure.Sql {
    public sealed class SivevRepository {
        private readonly int _timeout;
        public SivevRepository(int timeoutSeconds = 60) => _timeout = timeoutSeconds;

        public SpAppRollClaveGetResult SpAppRollClaveGet(SqlConnection conn) {
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sivev.SpAppRollClaveGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int); pMsg.Direction = ParameterDirection.InputOutput; pMsg.Value = 0;
            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt); pRes.Direction = ParameterDirection.InputOutput; pRes.Value = 0;
            var pFun = cmd.Parameters.Add("@vcFuncionAplicacion", SqlDbType.VarChar, 50); pFun.Direction = ParameterDirection.InputOutput; pFun.Value = "";
            var pKey = cmd.Parameters.Add("@vcClaveAcceso", SqlDbType.VarChar, 50); pKey.Direction = ParameterDirection.InputOutput; pKey.Value = "";

            cmd.ExecuteNonQuery();

            return new SpAppRollClaveGetResult {
                MensajeId = (int)(pMsg.Value ?? 0),
                Resultado = (short)(pRes.Value ?? (short)0),
                FuncionAplicacion = (string)(pFun.Value ?? string.Empty),
                ClaveAcceso = (string)(pKey.Value ?? string.Empty)
            };
        }

        public CredencialExisteHuellaResult SpAppCredencialExisteHuella(SqlConnection cnn, string uiEstacionId, short siOpcionMenuId, int iCredencial) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = cnn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppCredencialExisteHuella";
            cmd.CommandTimeout = _timeout;

            var pErr = cmd.Parameters.Add("@iError", SqlDbType.Int); pErr.Direction = ParameterDirection.ReturnValue;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.VarChar, 36) { Value = uiEstacionId });
            cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.Int) { Value = siOpcionMenuId }); // o SmallInt si aplica
            cmd.Parameters.Add(new SqlParameter("@iCredencial", SqlDbType.Int) { Value = iCredencial });

            var pExiste = cmd.Parameters.Add("@bExisteHuella", SqlDbType.Bit);
            pExiste.Direction = ParameterDirection.Output;

            var pHuella = cmd.Parameters.Add("@vbHuella", SqlDbType.VarBinary, 10_000); // ajusta al tamaño real en BD
            pHuella.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            var existe = pExiste.Value != DBNull.Value && Convert.ToBoolean(pExiste.Value);
            var huella = (pHuella.Value is byte[] b) ? b : Array.Empty<byte>();

            return new CredencialExisteHuellaResult {
                MensajeId = (int)(pMsg.Value ?? 0),
                Resultado = (short)(pRes.Value ?? (short)0),
                ExisteHuella = existe,
                Huella = huella
            };


        }
    }
}