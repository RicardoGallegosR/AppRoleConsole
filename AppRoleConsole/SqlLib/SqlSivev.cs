using Microsoft.Data.SqlClient;
using System.Data;

namespace AppRoleConsole.SqlLib {

    public sealed class SqlSivev {

        private readonly int _timeoutSeconds;
        public SqlSivev(int timeoutSeconds = 60) => _timeoutSeconds = timeoutSeconds;

        private SqlCommand cSpAppRollClaveGet(SqlConnection cnn) {
            var cmd = cnn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sivev.SpAppRollClaveGet";
            cmd.CommandTimeout = _timeoutSeconds;
            // Return value
            cmd.Parameters.Add(new SqlParameter("@iError", SqlDbType.Int) {                     Direction = ParameterDirection.ReturnValue });
            cmd.Parameters.Add(new SqlParameter("@iMensajeId", SqlDbType.Int) {                 Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@siResultado", SqlDbType.SmallInt) {           Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@vcClaveAcceso", SqlDbType.VarChar, 50) {      Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@vcFuncionAplicacion", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output });
            return cmd;
        }
        public sealed class SpAppRollClaveGetResult {
            public int Error { get; init; }
            public int MensajeId { get; init; }
            public short Resultado { get; init; }
            public string ClaveAcceso { get; init; } = "";
            public string FuncionAplicacion { get; init; } = "";
        }
        public SpAppRollClaveGetResult EjecutarSpAppRollClaveGet(SqlConnection cnn) {
            using var cmd = cSpAppRollClaveGet(cnn);
            cmd.ExecuteNonQuery();
            return new SpAppRollClaveGetResult {
                Error = (int)(cmd.Parameters["@iError"].Value ?? 0),
                MensajeId = (int)(cmd.Parameters["@iMensajeId"].Value ?? 0),
                Resultado = (short)(cmd.Parameters["@siResultado"].Value ?? (short)0),
                ClaveAcceso = (string)(cmd.Parameters["@vcClaveAcceso"].Value ?? string.Empty),
                FuncionAplicacion = (string)(cmd.Parameters["@vcFuncionAplicacion"].Value ?? string.Empty),
            };
        }

        /*
         ------------------------------------------------------------------------------------------------------------------------
         */



        private SqlCommand CreateSpAppCredencialExisteHuella( SqlConnection cnn, string uiEstacionId, int siOpcionMenuId, int iCredencial) {
            if (cnn is null) throw new ArgumentNullException(nameof(cnn));
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var cmd = cnn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppCredencialExisteHuella";
            cmd.CommandTimeout = _timeoutSeconds;

            // Return value
            cmd.Parameters.Add(new SqlParameter("@iError", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

            // Outputs
            cmd.Parameters.Add(new SqlParameter("@iMensajeId", SqlDbType.Int) {             Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@siResultado", SqlDbType.SmallInt) {       Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@bExisteHuella", SqlDbType.Bit) {          Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@vbHuella", SqlDbType.VarBinary, 10000) {  Direction = ParameterDirection.Output });

            // Inputs
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.VarChar, 36) {   Direction = ParameterDirection.Input, Value = uiEstacionId });
            cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.Int) {         Direction = ParameterDirection.Input, Value = siOpcionMenuId });
            cmd.Parameters.Add(new SqlParameter("@iCredencial", SqlDbType.Int) {            Direction = ParameterDirection.Input, Value = iCredencial });
            return cmd;
        }

        public sealed class SpAppCredencialExisteHuellaResult {
            public int Error { get; init; }
            public int MensajeId { get; init; }
            public short Resultado { get; init; }
            public bool ExisteHuella { get; init; }
            public byte[] Huella { get; init; } = Array.Empty<byte>();
        }

        public SpAppCredencialExisteHuellaResult EjecutarSpAppCredencialExisteHuella(SqlConnection cnn, string uiEstacionId, int siOpcionMenuId, int iCredencial) {
            using var cmd = CreateSpAppCredencialExisteHuella(cnn, uiEstacionId, siOpcionMenuId, iCredencial);
            cmd.ExecuteNonQuery();

            // Leer outputs con null-safety
            var error        = (int)(cmd.Parameters["@iError"].Value ?? 0);
            var mensajeId    = (int)(cmd.Parameters["@iMensajeId"].Value ?? 0);
            var resultado    = (short)(cmd.Parameters["@siResultado"].Value ?? (short)0);
            var existeHuella = false;
            if (cmd.Parameters["@bExisteHuella"].Value != DBNull.Value)
                existeHuella = Convert.ToBoolean(cmd.Parameters["@bExisteHuella"].Value);

            byte[] huella = Array.Empty<byte>();
            var vb = cmd.Parameters["@vbHuella"].Value;
            if (vb != DBNull.Value && vb is byte[] arr) huella = arr;

            return new SpAppCredencialExisteHuellaResult {
                Error = error,
                MensajeId = mensajeId,
                Resultado = resultado,
                ExisteHuella = existeHuella,
                Huella = huella
            };
        }
        /*
        ------------------------------------------------------------------------------------------------------------------------
        */


        private SqlCommand CreateSpAppAccesoInicia(SqlConnection cnn, string uiEstacionId, int iCredencial, short siOpcionMenuId, string? vcPassword, byte[]? vbHuella) {
            if (cnn is null) throw new ArgumentNullException(nameof(cnn));
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");
            var cmd = cnn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppAccesoInicia";
            cmd.CommandTimeout = _timeoutSeconds;

            // Return value
            cmd.Parameters.Add(new SqlParameter("@iError", SqlDbType.Int) {                 Direction = ParameterDirection.ReturnValue });

            // Outputs
            cmd.Parameters.Add(new SqlParameter("@iMensajeId", SqlDbType.Int) {             Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@siResultado", SqlDbType.SmallInt) {       Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.VarChar, 36) {     Direction = ParameterDirection.Output });

            // Inputs
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.VarChar, 36) {   Direction = ParameterDirection.Input, Value = uiEstacionId });
            cmd.Parameters.Add(new SqlParameter("@iCredencial", SqlDbType.Int) {            Direction = ParameterDirection.Input, Value = iCredencial });
            cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.SmallInt) {    Direction = ParameterDirection.Input, Value = siOpcionMenuId });

            var pPass = new SqlParameter("@vcPassword", SqlDbType.VarChar, 15) {            Direction = ParameterDirection.Input, IsNullable = true, Value = (object?)vcPassword ?? DBNull.Value };
            cmd.Parameters.Add(pPass);
            var pHuella = new SqlParameter("@vbHuella", SqlDbType.VarBinary, 2000) {        Direction = ParameterDirection.Input, IsNullable = true, Value = (object?)vbHuella ?? DBNull.Value };
            cmd.Parameters.Add(pHuella);
            return cmd;
        }
        public sealed class SpAppAccesoIniciaResult {
            public int Error { get; init; }
            public int MensajeId { get; init; }
            public short Resultado { get; init; }
            public string AccesoId { get; init; } = "";
        }

        public SpAppAccesoIniciaResult EjecutarSpAppAccesoInicia(SqlConnection cnn, string uiEstacionId, int iCredencial, short siOpcionMenuId, string? vcPassword, byte[]? vbHuella) {
            using var cmd = CreateSpAppAccesoInicia(cnn, uiEstacionId, iCredencial, siOpcionMenuId, vcPassword, vbHuella);
            cmd.ExecuteNonQuery();
            var res = new SpAppAccesoIniciaResult {
                Error     = (int)(cmd.Parameters["@iError"].Value ?? 0),
                MensajeId = (int)(cmd.Parameters["@iMensajeId"].Value ?? 0),
                Resultado = (short)(cmd.Parameters["@siResultado"].Value ?? (short)0),
                AccesoId  = (string)(cmd.Parameters["@uiAccesoId"].Value ?? string.Empty)
            };
            return res;
        }

        /*
        ------------------------------------------------------------------------------------------------------------------------
        */


        private SqlCommand CreateSpAppVerificacionVisualIni( SqlConnection cnn, string uiAccesoId, string uiEstacionId ) {
            if (cnn is null) throw new ArgumentNullException(nameof(cnn));
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var cmd = cnn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VfcVisual.SpAppVerificacionVisualIni";
            cmd.CommandTimeout = _timeoutSeconds;

            // Return value
            cmd.Parameters.Add(new SqlParameter("@iError", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

            // Outputs
            cmd.Parameters.Add(new SqlParameter("@iMensajeId", SqlDbType.Int) {                     Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@siResultado", SqlDbType.SmallInt) {               Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.VarChar, 36) {       Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@tiProtocoloVerificacionId", SqlDbType.TinyInt) {  Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("@vcPlacaId", SqlDbType.VarChar, 11) {              Direction = ParameterDirection.Output });

            // Inputs
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.VarChar, 36) {     Direction = ParameterDirection.Input, Value = uiAccesoId });
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.VarChar, 36) {   Direction = ParameterDirection.Input, Value = uiEstacionId });
            return cmd;
        }
        public sealed class SpAppVerificacionVisualIniResult {
            public int Error { get; init; }
            public int MensajeId { get; init; }
            public short Resultado { get; init; }
            public string VerificacionId { get; init; } = "";
            public byte ProtocoloVerificacionId { get; init; }
            public string PlacaId { get; init; } = "";
        }

        public SpAppVerificacionVisualIniResult EjecutarSpAppVerificacionVisualIni(SqlConnection cnn, string uiAccesoId, string uiEstacionId) {
            using var cmd = CreateSpAppVerificacionVisualIni(cnn, uiAccesoId, uiEstacionId);
            cmd.ExecuteNonQuery();
            var result = new SpAppVerificacionVisualIniResult  {
                Error = (int)(cmd.Parameters["@iError"].Value ?? 0),
                MensajeId = (int)(cmd.Parameters["@iMensajeId"].Value ?? 0),
                Resultado = (short)(cmd.Parameters["@siResultado"].Value ?? (short)0),
                VerificacionId = (string)(cmd.Parameters["@uiVerificacionId"].Value ?? string.Empty),
                ProtocoloVerificacionId = cmd.Parameters["@tiProtocoloVerificacionId"].Value is DBNull
                                            ? (byte)0
                                            : Convert.ToByte(cmd.Parameters["@tiProtocoloVerificacionId"].Value),
                PlacaId = (string)(cmd.Parameters["@vcPlacaId"].Value ?? string.Empty),
            };
            return result;
        }
        /*
        ------------------------------------------------------------------------------------------------------------------------
        */


   


        

       












        /*

        private static SqlConnection SqlConexion = new SqlConnection();
        private SqlDataAdapter cSpAppCapturaVisualGet;
        private SqlCommand cSpAppCapturaInspeccionVisualSet;
        private SqlCommand cSpAppCapturaInspeccionObdSet;
        private SqlCommand cSpAppTextoMensajeGet;
        private SqlCommand cSpAppAccesoFin;


        private void DefineSpAppCapturaVisualGet() {
            cSpAppCapturaVisualGet = new SqlDataAdapter("VfcVisual.SpAppCapturaVisualGet", SqlSivev.SqlConexion);
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Clear();
            cSpAppCapturaVisualGet.SelectCommand.CommandType = CommandType.StoredProcedure;
            //cSpAppCapturaVisualGet.SelectCommand.CommandTimeout = SqlSivev.mvarCommandTimeout;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@iError", SqlDbType.Int);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@iError"].Direction = ParameterDirection.ReturnValue;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@iMensajeId", SqlDbType.Int);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@iMensajeId"].Direction = ParameterDirection.Output;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@siResultado"].Direction = ParameterDirection.Output;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@uiEstacionId", SqlDbType.VarChar);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@uiEstacionId"].Direction = ParameterDirection.Input;
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@uiEstacionId"].Size = 36;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@uiAccesoId", SqlDbType.VarChar);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@uiAccesoId"].Direction = ParameterDirection.Input;
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@uiAccesoId"].Size = 36;
            cSpAppCapturaVisualGet.SelectCommand.Parameters.Add("@vcElemento", SqlDbType.VarChar);
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@vcElemento"].Direction = ParameterDirection.Input;
            cSpAppCapturaVisualGet.SelectCommand.Parameters["@vcElemento"].Size = 50;
        }
        private void DefineSpAppCapturaInspeccionVisualSet() {
            cSpAppCapturaInspeccionVisualSet = new SqlCommand();
            cSpAppCapturaInspeccionVisualSet.Parameters.Clear();
            cSpAppCapturaInspeccionVisualSet.CommandType = CommandType.StoredProcedure;
            cSpAppCapturaInspeccionVisualSet.CommandText = "VfcVisual.SpAppCapturaInspeccionVisualNewSet";
            cSpAppCapturaInspeccionVisualSet.Connection = SqlSivev.SqlConexion;
            //cSpAppCapturaInspeccionVisualSet.CommandTimeout = SqlSivev.mvarCommandTimeout;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@iError", SqlDbType.Int);
            cSpAppCapturaInspeccionVisualSet.Parameters["@iError"].Direction = ParameterDirection.ReturnValue;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@iMensajeId", SqlDbType.Int);
            cSpAppCapturaInspeccionVisualSet.Parameters["@iMensajeId"].Direction = ParameterDirection.Output;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@siResultado"].Direction = ParameterDirection.Output;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@uiVerificacionId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiVerificacionId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiVerificacionId"].Size = 36;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@uiEstacionId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiEstacionId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiEstacionId"].Size = 36;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@uiAccesoId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiAccesoId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters["@uiAccesoId"].Size = 36;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiTaponCombustible", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiTaponCombustible"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiTaponAceite", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiTaponAceite"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiBayonetaAceite", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiBayonetaAceite"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiPortafiltroAire", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiPortafiltroAire"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiTuboEscape", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiTuboEscape"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiFugasMotorTrans", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiFugasMotorTrans"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiNeumaticos", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiNeumaticos"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@tiComponentesEmisiones", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionVisualSet.Parameters["@tiComponentesEmisiones"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@iOdometro", SqlDbType.Int);
            cSpAppCapturaInspeccionVisualSet.Parameters["@iOdometro"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionVisualSet.Parameters.Add("@bCheckObd", SqlDbType.Bit);
            cSpAppCapturaInspeccionVisualSet.Parameters["@bCheckObd"].Direction = ParameterDirection.InputOutput;
        }

        private void DefineSpAppCapturaInspeccionObdSet() {
            cSpAppCapturaInspeccionObdSet = new SqlCommand();
            cSpAppCapturaInspeccionObdSet.Parameters.Clear();
            cSpAppCapturaInspeccionObdSet.CommandType = CommandType.StoredProcedure;
            cSpAppCapturaInspeccionObdSet.CommandText = "VfcVisual.SpAppCapturaInspeccionObdSet";
            cSpAppCapturaInspeccionObdSet.Connection = SqlSivev.SqlConexion;
            //cSpAppCapturaInspeccionObdSet.CommandTimeout = SqlSivev.mvarCommandTimeout;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@iError", SqlDbType.Int);
            cSpAppCapturaInspeccionObdSet.Parameters["@iError"].Direction = ParameterDirection.ReturnValue;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@iMensajeId", SqlDbType.Int);
            cSpAppCapturaInspeccionObdSet.Parameters["@iMensajeId"].Direction = ParameterDirection.Output;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siResultado"].Direction = ParameterDirection.Output;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@uiEstacionId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@uiEstacionId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@uiEstacionId"].Size = 36;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@uiAccesoId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@uiAccesoId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@uiAccesoId"].Size = 36;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@vcVehiculoId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@vcVehiculoId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@vcVehiculoId"].Size = 36;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@uiVerificacionId", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@uiVerificacionId"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@uiVerificacionId"].Size = 36;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSdciic", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSdciic"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSecc", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSecc"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSc", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSc"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSso", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSso"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSci", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSci"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSccc", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSccc"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSe", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSe"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSsa", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSsa"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSfaa", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSfaa"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiScso", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiScso"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiSrge", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiSrge"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiMil", SqlDbType.Bit);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiMil"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siFallas", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siFallas"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@vcCodigoError", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@vcCodigoError"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@vcCodigoError"].Size = 200;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@vcCodigoErrorPendiente", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@vcCodigoErrorPendiente"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@vcCodigoErrorPendiente"].Size = 200;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@vcProtocoloObd", SqlDbType.VarChar);
            cSpAppCapturaInspeccionObdSet.Parameters["@vcProtocoloObd"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters["@vcProtocoloObd"].Size = 50;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiIntentos", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiIntentos"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@tiConexionObd", SqlDbType.TinyInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@tiConexionObd"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@dVoltsSwOff", SqlDbType.Decimal);
            cSpAppCapturaInspeccionObdSet.Parameters["@dVoltsSwOff"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@dVoltsSwOn", SqlDbType.Decimal);
            cSpAppCapturaInspeccionObdSet.Parameters["@dVoltsSwOn"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siRpmOff", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siRpmOff"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siRpmOn", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siRpmOn"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siRpmCheck", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siRpmCheck"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@bLeeMonitores", SqlDbType.Bit);
            cSpAppCapturaInspeccionObdSet.Parameters["@bLeeMonitores"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@bLeeDtc", SqlDbType.Bit);
            cSpAppCapturaInspeccionObdSet.Parameters["@bLeeDtc"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@bLeeDtcPend", SqlDbType.Bit);
            cSpAppCapturaInspeccionObdSet.Parameters["@bLeeDtcPend"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@bLeeVin", SqlDbType.Bit);
            cSpAppCapturaInspeccionObdSet.Parameters["@bLeeVin"].Direction = ParameterDirection.Input;
            cSpAppCapturaInspeccionObdSet.Parameters.Add("@siCodigoProtocolo", SqlDbType.SmallInt);
            cSpAppCapturaInspeccionObdSet.Parameters["@siCodigoProtocolo"].Direction = ParameterDirection.Input;
        }


        private void DefineSpAppTextoMensajeGet() {
            cSpAppTextoMensajeGet = new SqlCommand();
            cSpAppTextoMensajeGet.Parameters.Clear();
            cSpAppTextoMensajeGet.CommandType = CommandType.StoredProcedure;
            cSpAppTextoMensajeGet.CommandText = "SivAppComun.SpAppTextoMensajeGet";
            cSpAppTextoMensajeGet.Connection = SqlSivev.SqlConexion;
            //cSpAppTextoMensajeGet.CommandTimeout = SqlSivev.mvarCommandTimeout;
            cSpAppTextoMensajeGet.Parameters.Add("@iError", SqlDbType.Int);
            cSpAppTextoMensajeGet.Parameters["@iError"].Direction = ParameterDirection.ReturnValue;
            cSpAppTextoMensajeGet.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            cSpAppTextoMensajeGet.Parameters["@siResultado"].Direction = ParameterDirection.Output;
            cSpAppTextoMensajeGet.Parameters.Add("@vcMensaje", SqlDbType.VarChar);
            cSpAppTextoMensajeGet.Parameters["@vcMensaje"].Direction = ParameterDirection.Output;
            cSpAppTextoMensajeGet.Parameters["@vcMensaje"].Size = 300;
            cSpAppTextoMensajeGet.Parameters.Add("@iMensajeId", SqlDbType.Int);
            cSpAppTextoMensajeGet.Parameters["@iMensajeId"].Direction = ParameterDirection.Input;
        }
        private void DefineSpAppAccesoFin() {
            cSpAppAccesoFin = new SqlCommand();
            cSpAppAccesoFin.Parameters.Clear();
            cSpAppAccesoFin.CommandType = CommandType.StoredProcedure;
            cSpAppAccesoFin.CommandText = "SivAppComun.SpAppAccesoFin";
            cSpAppAccesoFin.Connection = SqlSivev.SqlConexion;
            //cSpAppAccesoFin.CommandTimeout = SqlSivev.mvarCommandTimeout;
            cSpAppAccesoFin.Parameters.Add("@iError", SqlDbType.Int);
            cSpAppAccesoFin.Parameters["@iError"].Direction = ParameterDirection.ReturnValue;
            cSpAppAccesoFin.Parameters.Add("@iMensajeId", SqlDbType.Int);
            cSpAppAccesoFin.Parameters["@iMensajeId"].Direction = ParameterDirection.Output;
            cSpAppAccesoFin.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            cSpAppAccesoFin.Parameters["@siResultado"].Direction = ParameterDirection.Output;
            cSpAppAccesoFin.Parameters.Add("@uiAccesoId", SqlDbType.VarChar);
            cSpAppAccesoFin.Parameters["@uiAccesoId"].Direction = ParameterDirection.Input;
            cSpAppAccesoFin.Parameters["@uiAccesoId"].Size = 36;
            cSpAppAccesoFin.Parameters["@uiAccesoId"].Value = (object)"00000000-0000-0000-0000-000000000000";
            cSpAppAccesoFin.Parameters.Add("@uiEstacionId", SqlDbType.VarChar);
            cSpAppAccesoFin.Parameters["@uiEstacionId"].Direction = ParameterDirection.Input;
            cSpAppAccesoFin.Parameters["@uiEstacionId"].Size = 36;
        }
        */
        
    }
}
