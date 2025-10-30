using Microsoft.Data.SqlClient;
using System.Data;
using SQLSIVEV.Domain.Models;


namespace SQLSIVEV.Infrastructure.Sql {
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

        public CredencialExisteHuellaResult SpAppCredencialExisteHuella(SqlConnection cnn, Guid uiEstacionId, short siOpcionMenuId, int iCredencial) {
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

            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = uiEstacionId });
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
        public SpAppChecaCpuResult SpAppChecaCpu(SqlConnection conn, string estacionId, string aplicacion, string version, string identificadorEquipo, string serieDisco) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppChecaCpu";   // ajusta esquema/nombre si difiere
            cmd.CommandTimeout = _timeout;

            // Return value
            var pRet = cmd.Parameters.Add("@iError", SqlDbType.Int);
            pRet.Direction = ParameterDirection.ReturnValue;

            // Outputs
            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            // Inputs (ajusta tamaños según definición real del SP)
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.VarChar, 36) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@vcAplicacion", SqlDbType.VarChar, 64) { Value = aplicacion });
            cmd.Parameters.Add(new SqlParameter("@vcVersion", SqlDbType.VarChar, 32) { Value = version });
            cmd.Parameters.Add(new SqlParameter("@vcIdentificadorEquipo", SqlDbType.VarChar, 64) { Value = identificadorEquipo });
            cmd.Parameters.Add(new SqlParameter("@vcSerieDisco", SqlDbType.VarChar, 64) { Value = serieDisco });

            cmd.ExecuteNonQuery();

            return new SpAppChecaCpuResult {
                ReturnCode = (int)(pRet.Value ?? 0),
                MensajeId = (int)(pMsg.Value ?? 0),
                Resultado = (short)(pRes.Value ?? (short)0)
            };
        }

        public SpBitacoraAplicacionesIniciaResult SpBitacoraAplicacionesInicia(SqlConnection conn, Guid estacionId) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivSpComun.SpBitacoraAplicacionesInicia";
            cmd.CommandTimeout = _timeout;

            var pRet = cmd.Parameters.Add("@ReturnCode", SqlDbType.Int);
            pRet.Direction = ParameterDirection.ReturnValue;

            // IN
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });

            // OUT
            var pOut = cmd.Parameters.Add("@uiBitacoraAplicacionId", SqlDbType.UniqueIdentifier);
            pOut.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            // Convertir el OUTPUT (puede venir DBNull => Guid.Empty)
            Guid bitId = Guid.Empty;
            if (pOut.Value is Guid g) bitId = g;
            else if (pOut.Value is System.Data.SqlTypes.SqlGuid sg && !sg.IsNull) bitId = sg.Value;

            return new SpBitacoraAplicacionesIniciaResult {
                ReturnCode = (int)(pRet.Value ?? 0),
                BitacoraAplicacionId = bitId
            };
        }


        public async Task<SpBitacoraAplicacionesIniciaResult> SpBitacoraAplicacionesIniciaAsync(
            SqlConnection conn, Guid estacionId, CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivSpComun.SpBitacoraAplicacionesInicia";
            cmd.CommandTimeout = _timeout;

            var pRet = cmd.Parameters.Add("@ReturnCode", SqlDbType.Int);
            pRet.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });

            var pOut = cmd.Parameters.Add("@uiBitacoraAplicacionId", SqlDbType.UniqueIdentifier);
            pOut.Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync(ct);

            Guid bitId = Guid.Empty;
            if (pOut.Value is Guid g) bitId = g;
            else if (pOut.Value is System.Data.SqlTypes.SqlGuid sg && !sg.IsNull) bitId = sg.Value;

            return new SpBitacoraAplicacionesIniciaResult {
                ReturnCode = (int)(pRet.Value ?? 0),
                BitacoraAplicacionId = bitId
            };
        }



        public SpAppProgramOnResult SpAppProgramOn(SqlConnection conn, Guid estacionId) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppProgramOn";
            cmd.CommandTimeout = _timeout;

            // return value (el SP retorna @@ERROR)
            var pRet = cmd.Parameters.Add("@ReturnCode", SqlDbType.Int);
            pRet.Direction = ParameterDirection.ReturnValue;

            // outputs
            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            // input
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });

            cmd.ExecuteNonQuery();

            return new SpAppProgramOnResult {
                ReturnCode = (int)(pRet.Value ?? 0),
                MensajeId = (int)(pMsg.Value ?? 0),
                Resultado = (short)(pRes.Value ?? (short)0),
            };
        }




        public async Task<AccesoIniciaResult> SpAppAccesoIniciaAsync(SqlConnection conn, Guid estacionId, short opcionMenuId, int credencial, string password, byte[] huella, CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SivAppComun.SpAppAccesoInicia";
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetros de entrada
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.SmallInt) { Value = opcionMenuId });
            cmd.Parameters.Add(new SqlParameter("@iCredencial", SqlDbType.Int) { Value = credencial });
            cmd.Parameters.Add(new SqlParameter("@vcPassword", SqlDbType.VarChar, 15) { Value = (object?)password ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vbHuella", SqlDbType.VarBinary, -1) { Value = (object?)huella ?? Array.Empty<byte>() });

            // Parámetros de salida
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) {
                Direction = ParameterDirection.Output
            };
            var pAccesoId = new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);
            cmd.Parameters.Add(pAccesoId);

            await cmd.ExecuteNonQueryAsync(ct);

            return new AccesoIniciaResult {
                ReturnCode = (short)(pResultado.Value ?? (short)0),
                MensajeId = (int)(pMensajeId.Value ?? 0),
                AccesoId = pAccesoId.Value is Guid g ? g : Guid.Empty
            };
        }

        public async Task<VerificacionVisualIniResult> SpAppVerificacionVisualIniAsync(SqlConnection conn, Guid estacionId,Guid accesoId, CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppVerificacionVisualIni";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = accesoId });

            // Salidas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) {
                Direction = ParameterDirection.Output
            };
            var pVerificacionId = new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) {
                Direction = ParameterDirection.Output
            };
            var pProtocolo = new SqlParameter("@tiProtocoloVerificacionId", SqlDbType.TinyInt) {
                Direction = ParameterDirection.Output
            };
            var pPlaca = new SqlParameter("@vcPlacaId", SqlDbType.VarChar, 11) {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);
            cmd.Parameters.Add(pVerificacionId);
            cmd.Parameters.Add(pProtocolo);
            cmd.Parameters.Add(pPlaca);

            await cmd.ExecuteNonQueryAsync(ct);

            // Mapear resultados
            var res = new VerificacionVisualIniResult  {
                MensajeId = (int)(pMensajeId.Value ?? 0),
                Resultado = (short)(pResultado.Value ?? (short)0),
                VerificacionId = pVerificacionId.Value is Guid g ? g : Guid.Empty,
                ProtocoloVerificacionId = pProtocolo.Value is byte b ? b : (byte)0,
                PlacaId = pPlaca.Value?.ToString() ?? "DESCONOCIDO"
            };

            return res;
        }


        public async Task<AccesoFinResult> SpAppAccesoFinAsync(SqlConnection conn, Guid _EstacionId, Guid _AccesoId, CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SivAppComun.SpAppAccesoFin";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = _EstacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = _AccesoId });

            // Salidas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);

            // Valor de retorno (RETURN @@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            await cmd.ExecuteNonQueryAsync(ct);

            return new AccesoFinResult {
                Resultado = (short)(pResultado.Value ?? (short)0),
                MensajeId = (int)(pMensajeId.Value ?? 0),
                ReturnCode = pReturn.Value is int rc ? rc : 0
            };
        }

        public async Task<CapturaVisualGetResult> SpAppCapturaVisualGetAsync(SqlConnection conn, Guid estacionId, Guid accesoId, Guid verificacionId, string? elemento, byte? tiCombustible, 
                        CancellationToken ct = default) {

            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaVisualGet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = accesoId });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = verificacionId });
            cmd.Parameters.Add(new SqlParameter("@vcElemento", SqlDbType.VarChar, 50) { Value = (object?)elemento ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@tiCombustible", SqlDbType.TinyInt) { Value = (object?)tiCombustible ?? 1 });

            // Salidas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);

            // RETURN(@@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            var res = new CapturaVisualGetResult();

            // Ejecuta y lee el resultset
            using (var rdr = await cmd.ExecuteReaderAsync(ct)) {
                while (await rdr.ReadAsync(ct)) {
                    res.Items.Add(new CapturaVisualItem {
                        CapturaVisualId = rdr.GetInt16(0),
                        Elemento = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                        Despliegue = !rdr.IsDBNull(2) && rdr.GetBoolean(2),
                    });
                }
            }

            // Mapear outputs
            res.MensajeId = pMensajeId.Value == DBNull.Value ? 0 : Convert.ToInt32(pMensajeId.Value);
            res.Resultado = pResultado.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value);
            res.ReturnCode = pReturn.Value == DBNull.Value ? 0 : Convert.ToInt32(pReturn.Value);

            return res;
        }


        public async Task<AppTextoMensajeResult> SpAppTextoMensajeGetAsync( SqlConnection conn, int mensajeId,  CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SivAppComun.SpAppTextoMensajeGet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            cmd.Parameters.Add(new SqlParameter("@iMensajeId", SqlDbType.Int) { Value = mensajeId });

            // Salidas
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt)  {
                Direction = ParameterDirection.Output
            };
            var pMensaje = new SqlParameter("@vcMensaje", SqlDbType.VarChar, 300)  {
                Direction = ParameterDirection.Output,
                // por si el proc no asigna:
                Value = "DESCONOCIDO"
            };

            cmd.Parameters.Add(pResultado);
            cmd.Parameters.Add(pMensaje);

            // RETURN(@@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            // No hay resultset, solo ejecuta
            await cmd.ExecuteNonQueryAsync(ct);

            // Mapear outputs
            var res = new AppTextoMensajeResult{
                MensajeId = mensajeId,
                Resultado = pResultado.Value is short s ? s : Convert.ToInt16(pResultado.Value ?? 0),
                Mensaje = pMensaje.Value == DBNull.Value ? "DESCONOCIDO" : (string)pMensaje.Value,
                ReturnCode = pReturn.Value is int r ? r : Convert.ToInt32(pReturn.Value ?? 0),
            };

            return res;
        }



        public async Task<CapturaInspeccionVisualNewSetResult> SpAppCapturaInspeccionVisualNewSetAsync(SqlConnection conn, Guid verificacionId, Guid estacionId, Guid accesoId, byte tiTaponCombustible, byte tiTaponAceite, byte tiBayonetaAceite, byte tiPortafiltroAire, byte tiTuboEscape, byte tiFugasMotorTrans, byte tiNeumaticos, byte tiComponentesEmisiones, byte tiMotorGobernado, int odometro, CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaInspeccionVisualNewSet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas (alineadas a los tipos del SP)
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier)    { Value = verificacionId });
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier)        { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier)          { Value = accesoId });

            cmd.Parameters.Add(new SqlParameter("@tiTaponCombustible", SqlDbType.TinyInt)           { Value = tiTaponCombustible });
            cmd.Parameters.Add(new SqlParameter("@tiTaponAceite", SqlDbType.TinyInt)                { Value = tiTaponAceite });
            cmd.Parameters.Add(new SqlParameter("@tiBayonetaAceite", SqlDbType.TinyInt)             { Value = tiBayonetaAceite });
            cmd.Parameters.Add(new SqlParameter("@tiPortafiltroAire", SqlDbType.TinyInt)            { Value = tiPortafiltroAire });
            cmd.Parameters.Add(new SqlParameter("@tiTuboEscape", SqlDbType.TinyInt)                 { Value = tiTuboEscape });
            cmd.Parameters.Add(new SqlParameter("@tiFugasMotorTrans", SqlDbType.TinyInt)            { Value = tiFugasMotorTrans });
            cmd.Parameters.Add(new SqlParameter("@tiNeumaticos", SqlDbType.TinyInt)                 { Value = tiNeumaticos });
            cmd.Parameters.Add(new SqlParameter("@tiComponentesEmisiones", SqlDbType.TinyInt)       { Value = tiComponentesEmisiones });
            cmd.Parameters.Add(new SqlParameter("@tiMotorGobernado", SqlDbType.TinyInt)             { Value = tiMotorGobernado });

            cmd.Parameters.Add(new SqlParameter("@iOdometro", SqlDbType.Int) { Value = odometro });

            // Salidas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { 
                Direction = ParameterDirection.Output, Value = 0
            };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { 
                Direction = ParameterDirection.Output, Value = 0 
            };
            var pCheckObd = new SqlParameter("@bCheckObd", SqlDbType.Bit) {
                Direction = ParameterDirection.Output, Value = false 
            };

            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);
            cmd.Parameters.Add(pCheckObd);

            // RETURN(@@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            await cmd.ExecuteNonQueryAsync(ct);

            var res = new CapturaInspeccionVisualNewSetResult {
                MensajeId  = pMensajeId.Value  == DBNull.Value ? 0 : Convert.ToInt32(pMensajeId.Value),
                Resultado  = pResultado.Value  == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value),
                CheckObd   = pCheckObd.Value   != DBNull.Value && Convert.ToBoolean(pCheckObd.Value),
                ReturnCode = pReturn.Value     == DBNull.Value ? 0 : Convert.ToInt32(pReturn.Value)
            };

            return res;
        }





        public async Task<CapturaInspeccionObdSetResult> SpAppCapturaInspeccionObdSetAsync(
                                                                                            SqlConnection conn, Guid estacionId, Guid accesoId, Guid verificacionId,
                                                                                            string vehiculoId,            // @vcVehiculoId (VIN leído por OBD o 'DESCONOCIDO')
                                                                                            byte tiConexionObd,           // @tiConexionObd
                                                                                            string protocoloObd,          // @vcProtocoloObd (p.ej. 'ISO 15765-4 CAN 11/500')
                                                                                            byte tiIntentos,              // @tiIntentos
                                                                                            byte tiMil,                   // @tiMil (bits: usr/obd)
                                                                                            byte siFallas,                // @siFallas  (tinyint en SP, sí: usa byte)
                                                                                            string codError,              // @vcCodigoError
                                                                                            string codErrorPend,          // @vcCodigoErrorPendiente
                                                                                            byte tiSdciic, byte tiSecc, byte tiSc, byte tiSso, byte tiSci, byte tiSccc, byte tiSe, byte tiSsa, byte tiSfaa, byte tiScso, byte tiSrge,
                                                                                            decimal voltsSwOff,           // @dVoltsSwOff  decimal(4,1)
                                                                                            decimal voltsSwOn,            // @dVoltsSwOn   decimal(4,1)
                                                                                            short rpmOff,                 // @siRpmOff     smallint
                                                                                            short rpmOn,                  // @siRpmOn      smallint
                                                                                            short rpmCheck,               // @siRpmCheck   smallint
                                                                                            bool leeMonitores,            // @bLeeMonitores
                                                                                            bool leeDtc,                  // @bLeeDtc
                                                                                            bool leeDtcPend,              // @bLeeDtcPend
                                                                                            bool leeVin,                  // @bLeeVin
                                                                                            short codigoProtocolo,        // @siCodigoProtocolo (smallint)
                                                                                            CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaInspeccionObdSet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas GUID
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier)        { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier)          { Value = accesoId });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier)    { Value = verificacionId });

            // Cadenas
            cmd.Parameters.Add(new SqlParameter("@vcVehiculoId", SqlDbType.VarChar, 17)             { Value = string.IsNullOrWhiteSpace(vehiculoId) ? "DESCONOCIDO" : vehiculoId.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcProtocoloObd", SqlDbType.VarChar, 100)          { Value = string.IsNullOrWhiteSpace(protocoloObd) ? "DESCONOCIDO" : protocoloObd.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoError", SqlDbType.VarChar, 300)           { Value = codError ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPendiente", SqlDbType.VarChar, 300)  { Value = codErrorPend ?? "DESCONOCIDO" });

            // TinyInt (byte)
            cmd.Parameters.Add(new SqlParameter("@tiConexionObd", SqlDbType.TinyInt)                { Value = tiConexionObd });
            cmd.Parameters.Add(new SqlParameter("@tiIntentos", SqlDbType.TinyInt)                   { Value = tiIntentos });
            cmd.Parameters.Add(new SqlParameter("@tiMil", SqlDbType.TinyInt)                        { Value = tiMil });
            cmd.Parameters.Add(new SqlParameter("@siFallas", SqlDbType.TinyInt)                     { Value = siFallas }); // SP lo define tinyint

            cmd.Parameters.Add(new SqlParameter("@tiSdciic", SqlDbType.TinyInt)                     { Value = tiSdciic });
            cmd.Parameters.Add(new SqlParameter("@tiSecc", SqlDbType.TinyInt)                       { Value = tiSecc });
            cmd.Parameters.Add(new SqlParameter("@tiSc", SqlDbType.TinyInt)                         { Value = tiSc });
            cmd.Parameters.Add(new SqlParameter("@tiSso", SqlDbType.TinyInt)                        { Value = tiSso });
            cmd.Parameters.Add(new SqlParameter("@tiSci", SqlDbType.TinyInt)                        { Value = tiSci });
            cmd.Parameters.Add(new SqlParameter("@tiSccc", SqlDbType.TinyInt)                       { Value = tiSccc });
            cmd.Parameters.Add(new SqlParameter("@tiSe", SqlDbType.TinyInt)                         { Value = tiSe });
            cmd.Parameters.Add(new SqlParameter("@tiSsa", SqlDbType.TinyInt)                        { Value = tiSsa });
            cmd.Parameters.Add(new SqlParameter("@tiSfaa", SqlDbType.TinyInt)                       { Value = tiSfaa });
            cmd.Parameters.Add(new SqlParameter("@tiScso", SqlDbType.TinyInt)                       { Value = tiScso });
            cmd.Parameters.Add(new SqlParameter("@tiSrge", SqlDbType.TinyInt)                       { Value = tiSrge });

            // Decimales
            var pOff = new SqlParameter("@dVoltsSwOff", SqlDbType.Decimal)                          { Precision = 4, Scale = 1, Value = voltsSwOff };
            var pOn  = new SqlParameter("@dVoltsSwOn",  SqlDbType.Decimal)                          { Precision = 4, Scale = 1, Value = voltsSwOn };
            cmd.Parameters.Add(pOff);
            cmd.Parameters.Add(pOn);

            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siRpmOff", SqlDbType.SmallInt)                    { Value = rpmOff });
            cmd.Parameters.Add(new SqlParameter("@siRpmOn", SqlDbType.SmallInt)                     { Value = rpmOn });
            cmd.Parameters.Add(new SqlParameter("@siRpmCheck", SqlDbType.SmallInt)                  { Value = rpmCheck });

            // Bits
            cmd.Parameters.Add(new SqlParameter("@bLeeMonitores", SqlDbType.Bit)                    { Value = leeMonitores });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtc", SqlDbType.Bit)                          { Value = leeDtc });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPend", SqlDbType.Bit)                      { Value = leeDtcPend });
            cmd.Parameters.Add(new SqlParameter("@bLeeVin", SqlDbType.Bit)                          { Value = leeVin });

            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siCodigoProtocolo", SqlDbType.SmallInt)           { Value = codigoProtocolo });

            // Outputs
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int)                         { Direction = ParameterDirection.Output, Value = 0 };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt)                   { Direction = ParameterDirection.Output, Value = 0 };
            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);

            // RETURN(@@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            await cmd.ExecuteNonQueryAsync(ct);

            return new CapturaInspeccionObdSetResult {
                MensajeId = pMensajeId.Value == DBNull.Value ? 0 : Convert.ToInt32(pMensajeId.Value),
                Resultado = pResultado.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value),
                ReturnCode = pReturn.Value == DBNull.Value ? 0 : Convert.ToInt32(pReturn.Value)
            };
        }























    }
}
