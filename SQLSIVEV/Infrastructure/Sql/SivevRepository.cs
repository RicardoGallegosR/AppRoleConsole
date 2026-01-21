using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Utils;
using System.Data;
using System.Linq.Expressions;


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

        public async Task<VerificacionVisualIniResult> SpAppVerificacionVisualIniAsync(SqlConnection conn, Guid estacionId, Guid accesoId, CancellationToken ct = default) {
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


        public async Task<AppTextoMensajeResult> SpAppTextoMensajeGetAsync(SqlConnection conn, int mensajeId, CancellationToken ct = default) {
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
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = verificacionId });
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = accesoId });

            cmd.Parameters.Add(new SqlParameter("@tiTaponCombustible", SqlDbType.TinyInt) { Value = tiTaponCombustible });
            cmd.Parameters.Add(new SqlParameter("@tiTaponAceite", SqlDbType.TinyInt) { Value = tiTaponAceite });
            cmd.Parameters.Add(new SqlParameter("@tiBayonetaAceite", SqlDbType.TinyInt) { Value = tiBayonetaAceite });
            cmd.Parameters.Add(new SqlParameter("@tiPortafiltroAire", SqlDbType.TinyInt) { Value = tiPortafiltroAire });
            cmd.Parameters.Add(new SqlParameter("@tiTuboEscape", SqlDbType.TinyInt) { Value = tiTuboEscape });
            cmd.Parameters.Add(new SqlParameter("@tiFugasMotorTrans", SqlDbType.TinyInt) { Value = tiFugasMotorTrans });
            cmd.Parameters.Add(new SqlParameter("@tiNeumaticos", SqlDbType.TinyInt) { Value = tiNeumaticos });
            cmd.Parameters.Add(new SqlParameter("@tiComponentesEmisiones", SqlDbType.TinyInt) { Value = tiComponentesEmisiones });
            cmd.Parameters.Add(new SqlParameter("@tiMotorGobernado", SqlDbType.TinyInt) { Value = tiMotorGobernado });

            cmd.Parameters.Add(new SqlParameter("@iOdometro", SqlDbType.Int) { Value = odometro });

            // Salidas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) {
                Direction = ParameterDirection.Output,
                Value = 0
            };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) {
                Direction = ParameterDirection.Output,
                Value = 0
            };
            var pCheckObd = new SqlParameter("@bCheckObd", SqlDbType.Bit) {
                Direction = ParameterDirection.Output,
                Value = false
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
                                                                                            byte? tiConexionObd,           // @tiConexionObd
                                                                                            string protocoloObd,          // @vcProtocoloObd (p.ej. 'ISO 15765-4 CAN 11/500')
                                                                                            byte? tiIntentos,              // @tiIntentos
                                                                                            byte? tiMil,                   // @tiMil (bits: usr/obd)
                                                                                            byte? siFallas,                // @siFallas  (tinyint en SP, sí: usa byte)
                                                                                            string codError,              // @vcCodigoError
                                                                                            string codErrorPend,          // @vcCodigoErrorPendiente
                                                                                            byte? tiSdciic,
                                                                                            byte? tiSecc,
                                                                                            byte? tiSc,
                                                                                            byte? tiSso,
                                                                                            byte? tiSci,
                                                                                            byte? tiSccc,
                                                                                            byte? tiSe,
                                                                                            byte? tiSsa,
                                                                                            byte? tiSfaa,
                                                                                            byte? tiScso,
                                                                                            byte? tiSrge,
                                                                                            decimal? voltsSwOff,           // @dVoltsSwOff  decimal(4,1)
                                                                                            decimal? voltsSwOn,            // @dVoltsSwOn   decimal(4,1)
                                                                                            short? rpmOff,                 // @siRpmOff     smallint
                                                                                            short? rpmOn,                  // @siRpmOn      smallint
                                                                                            short? rpmCheck,               // @siRpmCheck   smallint
                                                                                            bool? leeMonitores,            // @bLeeMonitores
                                                                                            bool? leeDtc,                  // @bLeeDtc
                                                                                            bool? leeDtcPend,              // @bLeeDtcPend
                                                                                            bool? leeVin,                  // @bLeeVin
                                                                                            short? codigoProtocolo,        // @siCodigoProtocolo (smallint)
                                                                                            CancellationToken ct = default) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaInspeccionObdSet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas GUID
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = accesoId });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = verificacionId });

            // Cadenas
            cmd.Parameters.Add(new SqlParameter("@vcVehiculoId", SqlDbType.VarChar, 17) { Value = string.IsNullOrWhiteSpace(vehiculoId) ? "DESCONOCIDO" : vehiculoId.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcProtocoloObd", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(protocoloObd) ? "DESCONOCIDO" : protocoloObd.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoError", SqlDbType.VarChar, 300) { Value = codError ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPendiente", SqlDbType.VarChar, 300) { Value = codErrorPend ?? "DESCONOCIDO" });

            // TinyInt (byte)
            cmd.Parameters.Add(new SqlParameter("@tiConexionObd", SqlDbType.TinyInt) { Value = tiConexionObd });
            cmd.Parameters.Add(new SqlParameter("@tiIntentos", SqlDbType.TinyInt) { Value = tiIntentos });
            cmd.Parameters.Add(new SqlParameter("@tiMil", SqlDbType.TinyInt) { Value = tiMil });
            cmd.Parameters.Add(new SqlParameter("@siFallas", SqlDbType.TinyInt) { Value = siFallas }); // SP lo define tinyint

            cmd.Parameters.Add(new SqlParameter("@tiSdciic", SqlDbType.TinyInt) { Value = tiSdciic });
            cmd.Parameters.Add(new SqlParameter("@tiSecc", SqlDbType.TinyInt) { Value = tiSecc });
            cmd.Parameters.Add(new SqlParameter("@tiSc", SqlDbType.TinyInt) { Value = tiSc });
            cmd.Parameters.Add(new SqlParameter("@tiSso", SqlDbType.TinyInt) { Value = tiSso });
            cmd.Parameters.Add(new SqlParameter("@tiSci", SqlDbType.TinyInt) { Value = tiSci });
            cmd.Parameters.Add(new SqlParameter("@tiSccc", SqlDbType.TinyInt) { Value = tiSccc });
            cmd.Parameters.Add(new SqlParameter("@tiSe", SqlDbType.TinyInt) { Value = tiSe });
            cmd.Parameters.Add(new SqlParameter("@tiSsa", SqlDbType.TinyInt) { Value = tiSsa });
            cmd.Parameters.Add(new SqlParameter("@tiSfaa", SqlDbType.TinyInt) { Value = tiSfaa });
            cmd.Parameters.Add(new SqlParameter("@tiScso", SqlDbType.TinyInt) { Value = tiScso });
            cmd.Parameters.Add(new SqlParameter("@tiSrge", SqlDbType.TinyInt) { Value = tiSrge });

            // Decimales
            var pOff = new SqlParameter("@dVoltsSwOff", SqlDbType.Decimal)                          { Precision = 4, Scale = 1, Value = voltsSwOff };
            var pOn  = new SqlParameter("@dVoltsSwOn",  SqlDbType.Decimal)                          { Precision = 4, Scale = 1, Value = voltsSwOn };
            cmd.Parameters.Add(pOff);
            cmd.Parameters.Add(pOn);

            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siRpmOff", SqlDbType.SmallInt) { Value = rpmOff });
            cmd.Parameters.Add(new SqlParameter("@siRpmOn", SqlDbType.SmallInt) { Value = rpmOn });
            cmd.Parameters.Add(new SqlParameter("@siRpmCheck", SqlDbType.SmallInt) { Value = rpmCheck });

            // Bits
            cmd.Parameters.Add(new SqlParameter("@bLeeMonitores", SqlDbType.Bit) { Value = leeMonitores });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtc", SqlDbType.Bit) { Value = leeDtc });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPend", SqlDbType.Bit) { Value = leeDtcPend });
            cmd.Parameters.Add(new SqlParameter("@bLeeVin", SqlDbType.Bit) { Value = leeVin });

            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siCodigoProtocolo", SqlDbType.SmallInt) { Value = codigoProtocolo });

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


        public async Task<InspeccionObdGet> SpAppCapturaInspeccionObd2SetAsync(SqlConnection conn, VisualRegistroWindows V, InspeccionObd2Set obd, CancellationToken ct = default) {
            int _MensajeId = 100;
            short _Resultado = 100;
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaInspeccionObd2Set";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas GUID
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = V.EstacionId });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = V.AccesoId });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = V.VerificacionId });

            // Cadenas
            cmd.Parameters.Add(new SqlParameter("@vcVehiculoId", SqlDbType.VarChar, 17) { Value = string.IsNullOrWhiteSpace(obd.VehiculoId) ? "DESCONOCIDO" : obd.VehiculoId.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcProtocoloObd", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(obd.ProtocoloObd) ? "DESCONOCIDO" : obd.ProtocoloObd.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoError", SqlDbType.VarChar, 300) { Value = obd.CodigoError ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPendiente", SqlDbType.VarChar, 300) { Value = obd.CodigoErrorPendiente ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPermanente", SqlDbType.VarChar, 300) { Value = obd.CodigoErrorPermanente ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcDir_ECU", SqlDbType.VarChar, 32) { Value = obd.Dir_ECU ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcID_Calib", SqlDbType.VarChar, 32) { Value = obd.ID_Calib ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcIDs_Adic", SqlDbType.VarChar, 64) { Value = obd.IDs_Adic ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcNumVerifCalib", SqlDbType.VarChar, 16) { Value = obd.NumVerifCalib });
            cmd.Parameters.Add(new SqlParameter("@vcLista_CVN", SqlDbType.VarChar, 200) { Value = obd.Lista_CVN });
            cmd.Parameters.Add(new SqlParameter("@vcEst_Mon_DTC_Borrado", SqlDbType.VarChar, 100) { Value = obd.Est_Mon_DTC_Borrado ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_01_20", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_01_20 });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_21_40", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_21_40 });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_41_60", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_41_60 });

            // TinyInt (byte)
            cmd.Parameters.Add(new SqlParameter("@tiNEV", SqlDbType.TinyInt) { Value = 0 });
            
            cmd.Parameters.Add(new SqlParameter("@tiConexionObd", SqlDbType.TinyInt) { Value = obd.ConexionObd });
            cmd.Parameters.Add(new SqlParameter("@tiIntentos", SqlDbType.TinyInt) { Value = obd.Intentos });
            cmd.Parameters.Add(new SqlParameter("@tiMil", SqlDbType.TinyInt) { Value = obd.Mil });
            cmd.Parameters.Add(new SqlParameter("@siFallas", SqlDbType.TinyInt) { Value = obd.Fallas }); // SP lo define tinyint
            cmd.Parameters.Add(new SqlParameter("@tiSdciic", SqlDbType.TinyInt) { Value = obd.Sdciic });
            cmd.Parameters.Add(new SqlParameter("@tiSecc", SqlDbType.TinyInt) { Value = obd.Secc });
            cmd.Parameters.Add(new SqlParameter("@tiSc", SqlDbType.TinyInt) { Value = obd.Sc });
            cmd.Parameters.Add(new SqlParameter("@tiSso", SqlDbType.TinyInt) { Value = obd.Sso });
            cmd.Parameters.Add(new SqlParameter("@tiSci", SqlDbType.TinyInt) { Value = obd.Sci });
            cmd.Parameters.Add(new SqlParameter("@tiSccc", SqlDbType.TinyInt) { Value = obd.Sccc });
            cmd.Parameters.Add(new SqlParameter("@tiSe", SqlDbType.TinyInt) { Value = obd.Se });
            cmd.Parameters.Add(new SqlParameter("@tiSsa", SqlDbType.TinyInt) { Value = obd.Ssa });
            cmd.Parameters.Add(new SqlParameter("@tiSfaa", SqlDbType.TinyInt) { Value = obd.Sfaa });
            cmd.Parameters.Add(new SqlParameter("@tiScso", SqlDbType.TinyInt) { Value = obd.Scso });
            cmd.Parameters.Add(new SqlParameter("@tiSrge", SqlDbType.TinyInt) { Value = obd.Srge });
            cmd.Parameters.Add(new SqlParameter("@tiSpsa", SqlDbType.TinyInt) { Value = obd.Spsa });
            cmd.Parameters.Add(new SqlParameter("@tiSge", SqlDbType.TinyInt) { Value = obd.Sge });
            cmd.Parameters.Add(new SqlParameter("@tiSchnm", SqlDbType.TinyInt) { Value = obd.Schnm });
            cmd.Parameters.Add(new SqlParameter("@tiSfp", SqlDbType.TinyInt) { Value = obd.Sfp });
            cmd.Parameters.Add(new SqlParameter("@tiSscrron", SqlDbType.TinyInt) { Value = obd.Sscrron });
            cmd.Parameters.Add(new SqlParameter("@tiReq_Emisiones", SqlDbType.TinyInt) { Value = obd.Req_Emisiones });
            cmd.Parameters.Add(new SqlParameter("@tiCombustible0151Id", SqlDbType.TinyInt) { Value = obd.Combustible0151Id });
            cmd.Parameters.Add(new SqlParameter("@tiCombustible0907Id", SqlDbType.TinyInt) { Value = obd.Combustible0907Id });

            // Decimales
            cmd.Parameters.Add(new SqlParameter("@dVoltsSwOff", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = obd.VoltsSwOff });
            cmd.Parameters.Add(new SqlParameter("@dVoltsSwOn", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = obd.VoltsSwOn });
            cmd.Parameters.Add(new SqlParameter("@dSTFT_B1", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.STFT_B1 });
            cmd.Parameters.Add(new SqlParameter("@dLTFT_B1", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.LTFT_B1 });
            cmd.Parameters.Add(new SqlParameter("@dMAF", SqlDbType.Decimal) { Precision = 8, Scale = 3, Value = obd.MAF });
            cmd.Parameters.Add(new SqlParameter("@dTPS", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.TPS });
            cmd.Parameters.Add(new SqlParameter("@dAvanceEnc", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.AvanceEnc });
            cmd.Parameters.Add(new SqlParameter("@dVolt_O2", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd.Volt_O2 });
            cmd.Parameters.Add(new SqlParameter("@dNivelComb", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.NivelComb });
            cmd.Parameters.Add(new SqlParameter("@dCCM", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.CCM });


            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siIAT", SqlDbType.SmallInt) { Value = obd.IAT });
            cmd.Parameters.Add(new SqlParameter("@siRpmOn", SqlDbType.SmallInt) { Value = obd.RpmOn });
            cmd.Parameters.Add(new SqlParameter("@siRpmOff", SqlDbType.SmallInt) { Value = obd.RpmOff });
            cmd.Parameters.Add(new SqlParameter("@siRpmCheck", SqlDbType.SmallInt) { Value = obd.RpmCheck });
            cmd.Parameters.Add(new SqlParameter("@siCodigoProtocolo", SqlDbType.SmallInt) { Value = obd.CodigoProtocolo });
            cmd.Parameters.Add(new SqlParameter("@siVelVeh", SqlDbType.SmallInt) { Value = obd.VelVeh });
            cmd.Parameters.Add(new SqlParameter("@siPres_Baro", SqlDbType.SmallInt) { Value = obd.Pres_Baro });
            cmd.Parameters.Add(new SqlParameter("@siTR", SqlDbType.SmallInt) { Value = obd.TR });


            // Bits
            cmd.Parameters.Add(new SqlParameter("@bLeeMonitores", SqlDbType.Bit) { Value = obd.LeeMonitores });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtc", SqlDbType.Bit) { Value = obd.LeeDtc });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPend", SqlDbType.Bit) { Value = obd.LeeDtcPend });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPerm", SqlDbType.Bit) { Value = obd.LeeDtcPerm });
            cmd.Parameters.Add(new SqlParameter("@bLeeVin", SqlDbType.Bit) { Value = obd.LeeVin });


            // Ints
            cmd.Parameters.Add(new SqlParameter("@intTpo_Arranque", SqlDbType.Int) { Value = obd.Tpo_Arranque });
            cmd.Parameters.Add(new SqlParameter("@intMotorTipoId", SqlDbType.Int) { Value = obd.MotorTipoId ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intDist_MIL_On", SqlDbType.Int) { Value = obd.Dist_MIL_On });
            cmd.Parameters.Add(new SqlParameter("@intDist_Borrado_DTC", SqlDbType.Int) { Value = obd.Dist_Borrado_DTC });
            cmd.Parameters.Add(new SqlParameter("@intTpo_MIL_On", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@intTpo_Borrado_DTC", SqlDbType.Int) { Value = obd.Tpo_Borrado_DTC });

            // Outputs
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { Direction = ParameterDirection.Output, Value = 0 };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { Direction = ParameterDirection.Output, Value = 0 };
            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);

            // RETURN(@@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
            cmd.Parameters.Add(pReturn);

            await cmd.ExecuteNonQueryAsync(ct);


            _MensajeId = pMensajeId.Value == DBNull.Value ? 0 : Convert.ToInt32(pMensajeId.Value);
            _Resultado = pResultado.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value);


            return new InspeccionObdGet {
                MensajeId = _MensajeId,
                Resultado = _Resultado
            };
        }

        public async Task<SpAppBitacoraErroresSet> SpSpAppBitacoraErroresSetAsync(VisualRegistroWindows V, SpAppBitacoraErroresSet A, CancellationToken ct = default) {
            short _Resultado = 0;
            int   _MensajeId = 0;
            try {
                using (var connApp = SqlConnectionFactory.Create(server: V.Server, db: V.Database, user: V.User, pass: V.Password, appName: V.AppName)) {
                    if (connApp.State != ConnectionState.Open) await connApp.OpenAsync(ct);
                    using (var scope = new AppRoleScope(connApp, role: V.RollVisual, password: V.RollVisualAcceso.ToString().ToUpper())) {

                        using var cmd = connApp.CreateCommand();
                        cmd.CommandText = "SivAppComun.SpAppBitacoraErroresSet";
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Entradas
                        cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = A.EstacionId });
                        cmd.Parameters.Add(new SqlParameter("@siCentro", SqlDbType.SmallInt) { Value = A.Centro });
                        cmd.Parameters.Add(new SqlParameter("@vcNombreCpu", SqlDbType.VarChar, 25) { Value = A.NombreCpu });
                        cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.SmallInt) { Value = A.OpcionMenuId });
                        cmd.Parameters.Add(new SqlParameter("@dtFechaError", SqlDbType.DateTime) { Value = A.FechaError });
                        cmd.Parameters.Add(new SqlParameter("@vcLibreria", SqlDbType.VarChar, 50) { Value = A.Libreria });
                        cmd.Parameters.Add(new SqlParameter("@vcClase", SqlDbType.VarChar, 50) { Value = A.Clase });
                        cmd.Parameters.Add(new SqlParameter("@vcMetodo", SqlDbType.VarChar, 50) { Value = A.Metodo });
                        cmd.Parameters.Add(new SqlParameter("@iCodigoErrorSql", SqlDbType.Int) { Value = A.CodigoErrorSql });
                        cmd.Parameters.Add(new SqlParameter("@iCodigoError", SqlDbType.Int) { Value = A.CodigoError });
                        cmd.Parameters.Add(
                            new SqlParameter("@vcDescripcionError", SqlDbType.VarChar, 500) {
                                Value = A.DescripcionError?.Length > 500
                                    ? A.DescripcionError.Substring(0, 500)
                                    : (object?)A.DescripcionError ?? DBNull.Value
                            }
                        );
                        cmd.Parameters.Add(new SqlParameter("@iLineaCodigo", SqlDbType.Int) { Value = A.LineaCodigo });
                        cmd.Parameters.Add(new SqlParameter("@iLastDllError", SqlDbType.Int) { Value = A.LastDllError });
                        cmd.Parameters.Add(new SqlParameter("@vcSourceError", SqlDbType.VarChar, 50) { Value = A.SourceError });

                        var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { Direction = ParameterDirection.Output };

                        cmd.Parameters.Add(pMensajeId);
                        cmd.Parameters.Add(pResultado);

                        // Valor de retorno (RETURN @@ERROR)
                        var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };
                        cmd.Parameters.Add(pReturn);


                        await cmd.ExecuteNonQueryAsync(ct);
                        _Resultado = (pResultado.Value == DBNull.Value) ? (short)0 : Convert.ToInt16(pResultado.Value);
                        _MensajeId = (pMensajeId.Value == DBNull.Value) ? 0 : Convert.ToInt32(pMensajeId.Value);

                    }
                }
            } catch (Exception e) {
                SivevLogger.Error($"SivSpComun.SpAppBitacoraErroresSet {e}");
            }
            return new SpAppBitacoraErroresSet {
                Resultado = _Resultado,
                MensajeId = _MensajeId,
            };
        }
    }
}
