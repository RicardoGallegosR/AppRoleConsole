using DocumentFormat.OpenXml.Presentation;
using Microsoft.Data.SqlClient;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql.Configuracion;
using SQLSIVEV.Infrastructure.Utils;
using System.Data;
using System.Linq.Expressions;
using static SQLSIVEV.Domain.Models.SpAppProgramOnResult;


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

        #region Vin Modelo
        public async Task<CapturaVinModeloResult> SpAppCapturaVinModeloSetAsync( SqlConnection cnn, Guid estacionId, Guid accesoId, Guid verificacionId, string vin, int odometro = 0, CancellationToken ct = default) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VfcCaptura.SpAppCapturaVinModeloSet";

            cmd.CommandTimeout = _timeout;

            var pMensaje = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMensaje.Direction = ParameterDirection.Output;

            var pResultado = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);

            pResultado.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = estacionId;
            cmd.Parameters.Add("@uiAccesoId",   SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiVerificacionId",SqlDbType.UniqueIdentifier).Value = verificacionId;
            cmd.Parameters.Add("@vcVin", SqlDbType.VarChar, 17).Value = vin;

            var pModelo = cmd.Parameters.Add("@siModelo", SqlDbType.SmallInt);
            pModelo.Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@iOdometro", SqlDbType.Int).Value = odometro;

            var pMarcaId = cmd.Parameters.Add("@iMarcaId",SqlDbType.Int);
            pMarcaId.Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync(ct);

            return new CapturaVinModeloResult {
                MensajeId =     pMensaje.Value == DBNull.Value ? 0          : Convert.ToInt32(pMensaje.Value),
                ResultadoId =   pResultado.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value),
                Modelo =        pModelo.Value == DBNull.Value  ? (short)0   : Convert.ToInt16(pModelo.Value),
                MarcaId =       pMarcaId.Value == DBNull.Value ? 0          : Convert.ToInt32(pMarcaId.Value)
            };
        }
        #endregion



        


        #region bitacora de adeudos
        public async Task<CapturaIniciaResult> SpAppCapturaIniciaWebSrvNewAsync(SqlConnection cnn, Guid estacionId, Guid accesoId, string placa, bool pet, int consultasSemoviId, string vin, short modelo, string tipoServicio, string folioAuto, DateTime fechaTC, bool testFM, bool conexionWs, byte conexionWebSrv, bool adeudoFotoCivicas, bool adeudoTenencia, bool adeudoInfraccion, bool gdfNoRegistrado, CancellationToken ct = default) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VfcCaptura.SpAppCapturaIniciaWebSrvNew";
            cmd.CommandTimeout = _timeout;

            var pMensaje = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMensaje.Direction = ParameterDirection.Output;

            var pResultado = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pResultado.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = estacionId;
            cmd.Parameters.Add("@uiAccesoId",   SqlDbType.UniqueIdentifier).Value = accesoId;
            
            var pVerificacionId = cmd.Parameters.Add("@uiVerificacionId", SqlDbType.VarChar,36);

            pVerificacionId.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@vcPlacaId", SqlDbType.VarChar,20).Value = placa;
            cmd.Parameters.Add("@bPet", SqlDbType.Bit).Value = pet;

            var pTipoVerificacion = cmd.Parameters.Add("@tiTipoVerificacionId",SqlDbType.TinyInt);

            pTipoVerificacion.Direction = ParameterDirection.Output;

            var pEstado = cmd.Parameters.Add("@tiEstadoId", SqlDbType.TinyInt);
            pEstado.Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@iConsultasSemoviId", SqlDbType.Int).Value = consultasSemoviId;
            cmd.Parameters.Add("@vcVin", SqlDbType.VarChar,  50).Value = vin;
            cmd.Parameters.Add("@siModelo", SqlDbType.SmallInt).Value = modelo;
            cmd.Parameters.Add("@vcTipoServicio", SqlDbType.VarChar,  50).Value = tipoServicio;
            cmd.Parameters.Add("@vcFolioAuto", SqlDbType.VarChar, 50).Value = folioAuto;
            cmd.Parameters.Add("@dtFechaTC", SqlDbType.DateTime).Value = fechaTC;
            cmd.Parameters.Add("@bTestFM", SqlDbType.Bit).Value = testFM;
            cmd.Parameters.Add("@bConexionWs", SqlDbType.Bit).Value = conexionWs;
            cmd.Parameters.Add("@tiConexionWebSrv", SqlDbType.TinyInt).Value = conexionWebSrv;
            cmd.Parameters.Add("@bAdeudoGdfFotoCivicas", SqlDbType.Bit).Value = adeudoFotoCivicas;
            cmd.Parameters.Add("@bAdeudoTenencia", SqlDbType.Bit).Value = adeudoTenencia;
            cmd.Parameters.Add("@bAdeudoInfraccion",SqlDbType.Bit).Value = adeudoInfraccion;
            cmd.Parameters.Add("@bGdfNoRegiostrado",SqlDbType.Bit).Value = gdfNoRegistrado;

            await cmd.ExecuteNonQueryAsync(ct);

            int mensajeId = pMensaje.Value == DBNull.Value ? 0 : Convert.ToInt32(pMensaje.Value);
            short resultadoId = pResultado.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pResultado.Value);
            byte tipoVerificacionId = pTipoVerificacion.Value == DBNull.Value ? (byte)0 : Convert.ToByte(pTipoVerificacion.Value);
            byte estadoId = pEstado.Value == DBNull.Value ? (byte)0 : Convert.ToByte(pEstado.Value);
            Guid? verificacionId = null;

            if (pVerificacionId.Value != DBNull.Value && Guid.TryParse(pVerificacionId.Value?.ToString(), out Guid guid)) {
                verificacionId = guid;
            }

            return new CapturaIniciaResult {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                VerificacionId = verificacionId,
                TipoVerificacionId = tipoVerificacionId,
                EstadoId = estadoId
            };
        }
        #endregion
        #region Captura de datos de Tarjeta de circulación
        public async Task<SpAppCapturaDatosSetResult> SpAppCapturaDatosSetAsync(SqlConnection cnn, Guid estacionId,  Guid accesoId, Guid verificacionId, Guid verificacionAntId, int submarcaId, byte combustibleId, bool esEmpresa, string nombre, string apelPaterno, string apelMaterno, string tarjetaFolio, DateTime tarjetaFecha, short tubosEscape, byte[] imagenFactura, byte[] imagenTarjetaCirculacion, CancellationToken ct = default) {
            ArgumentNullException.ThrowIfNull(cnn);
            using var cmd = new SqlCommand("VfcCaptura.SpAppCapturaDatosSet", cnn) {
                CommandType = CommandType.StoredProcedure
            };

            // OUTPUT
            var pMensaje = new SqlParameter("@iMensajeId", SqlDbType.Int){
                Direction = ParameterDirection.InputOutput,
                Value = 0
            };

            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) {
                Direction = ParameterDirection.InputOutput,
                Value = (short)0
            };

            cmd.Parameters.Add(pMensaje);
            cmd.Parameters.Add(pResultado);

            // Identificadores
            cmd.Parameters.Add("@uiEstacionId",         SqlDbType.UniqueIdentifier).Value = estacionId;
            cmd.Parameters.Add("@uiAccesoId",           SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiVerificacionId",     SqlDbType.UniqueIdentifier).Value = verificacionId;
            cmd.Parameters.Add("@uiVerificacionAntId",  SqlDbType.UniqueIdentifier).Value = verificacionAntId;

            // Vehículo
            cmd.Parameters.Add("@iSubmarcaId",      SqlDbType.Int).Value = submarcaId;
            cmd.Parameters.Add("@tiCombustibleId",  SqlDbType.TinyInt).Value = combustibleId;

            // Propietario
            cmd.Parameters.Add("@bEsEmpresa",   SqlDbType.Bit).Value = esEmpresa;
            cmd.Parameters.Add("@vcNombre",     SqlDbType.VarChar,50).Value = nombre;
            cmd.Parameters.Add("@vcApelPaterno",SqlDbType.VarChar, 50).Value = apelPaterno;
            cmd.Parameters.Add("@vcApelMaterno",SqlDbType.VarChar,  50).Value = apelMaterno;

            // Tarjeta de circulación
            cmd.Parameters.Add("@vcTarjetaFolio", SqlDbType.VarChar, 12).Value = tarjetaFolio;
            cmd.Parameters.Add("@sdTarjetaFecha", SqlDbType.SmallDateTime).Value = tarjetaFecha;
            cmd.Parameters.Add("@siTubosEscape",  SqlDbType.SmallInt).Value = tubosEscape;

            // Imágenes
            cmd.Parameters.Add("@imagenFactura",SqlDbType.Image).Value = imagenFactura ?? Array.Empty<byte>();
            cmd.Parameters.Add("@imagenTarjetaCirculacion", SqlDbType.Image).Value = imagenTarjetaCirculacion ?? Array.Empty<byte>();

            await cmd.ExecuteNonQueryAsync(ct);

            return new SpAppCapturaDatosSetResult {
                MensajeId = Convert.ToInt32(pMensaje.Value),
                ResultadoId = Convert.ToInt16(pResultado.Value)
            };
        }
        #endregion




        #region Combustibles, EntidadesFederativas, Marcas, TiposAdeudos, TiposLineasCaptura para captura centralizada
        #region documentos adicionales 
        public StoreResult<List<DocumentoAdicionalDto>> SpAppCapturaDocumentosAdicionalesGet(SqlConnection cnn, Guid estacionId, Guid accesoId, Guid verificacionId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var documentos = new List<DocumentoAdicionalDto>();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VfcCaptura.SpAppCapturaDocumentosAdicionalesGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiEstacionId",SqlDbType.UniqueIdentifier).Value = estacionId;
            cmd.Parameters.Add("@uiAccesoId",  SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiVerificacionId", SqlDbType.UniqueIdentifier).Value = verificacionId;

            using (var reader = cmd.ExecuteReader()) {
                int ordDocumentoId = reader.GetOrdinal("DocumentoId");
                int ordTipoDocumentoId = reader.GetOrdinal("TipoDocumentoId");
                int ordTituloFolioDocumento = reader.GetOrdinal("TituloFolioDocumento");
                int ordReferencia =           reader.GetOrdinal("Referencia");
                int ordTipoAdeudoId =         reader.GetOrdinal("TipoAdeudoId");
                int ordAdeudoId =             reader.GetOrdinal("AdeudoId");
                int ordRequiereImagen =       reader.GetOrdinal("RequiereImagen");

                while (reader.Read()) {
                    documentos.Add(new DocumentoAdicionalDto {
                        DocumentoId = reader.IsDBNull(ordDocumentoId) ? Guid.Empty : reader.GetGuid(ordDocumentoId),
                        TipoDocumentoId = reader.IsDBNull(ordTipoDocumentoId) ? 0: Convert.ToInt32(reader.GetValue(ordTipoDocumentoId)),
                        TituloFolioDocumento = reader.IsDBNull(ordTituloFolioDocumento) ? string.Empty : reader.GetString(ordTituloFolioDocumento),
                        Referencia =  reader.IsDBNull(ordReferencia)  ? string.Empty : reader.GetString(ordReferencia),
                        TipoAdeudoId =reader.IsDBNull(ordTipoAdeudoId)? 0 : Convert.ToInt32(reader.GetValue(ordTipoAdeudoId)),
                        AdeudoId =    reader.IsDBNull(ordAdeudoId) ? Guid.Empty   : reader.GetGuid(ordAdeudoId),
                        RequiereImagen = !reader.IsDBNull(ordRequiereImagen) && Convert.ToBoolean( reader.GetValue(ordRequiereImagen))
                    });
                }
            }

            // Los OUTPUT se leen después de cerrar el DataReader.
            int mensajeId =     pMsg.Value == DBNull.Value ? 0 : Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<DocumentoAdicionalDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = documentos
            };
        }

        #endregion

        public StoreResult<List<CombustibleDto>> SpAppCombustiblesGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var combustibles = new List<CombustibleDto>();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppCombustiblesGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiAccesoId",   SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;

            using (var reader = cmd.ExecuteReader()) {
                int ordCombustibleId = reader.GetOrdinal("CombustibleId");
                int ordCombustible = reader.GetOrdinal("Combustible");
                int ordFactorCalculo = reader.GetOrdinal("FactorCalculo");
                int ordModuloEquipoId = reader.GetOrdinal("ModuloEquipoId");

                while (reader.Read()) {
                    combustibles.Add(new CombustibleDto {
                        CombustibleId =     reader.IsDBNull(ordCombustibleId)   ?(byte)0  : reader.GetByte(ordCombustibleId),
                        Combustible =       reader.IsDBNull(ordCombustible)     ? string.Empty : reader.GetString(ordCombustible),
                        FactorCalculo =     reader.IsDBNull(ordFactorCalculo)   ? 0m : reader.GetDecimal(ordFactorCalculo), 
                        ModuloEquipoId =    reader.IsDBNull(ordModuloEquipoId)  ?(byte)0 : reader.GetByte(ordModuloEquipoId)
                    });
                }
            }

            // IMPORTANTE:
            // los parámetros OUTPUT se leen después de cerrar el DataReader.
            
            int mensajeId = pMsg.Value == DBNull.Value ? 0: Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);
            
            return new StoreResult<List<CombustibleDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = combustibles
            };
        }


        public StoreResult<List<EntidadesFederativasDto>> SpAppEntidadesFederativasGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var entidadesFederativas = new List<EntidadesFederativasDto >();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppEntidadesFederativasGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiAccesoId", SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;

            using (var reader = cmd.ExecuteReader()) {
                int ordEntidadFederativaId = reader.GetOrdinal("EntidadFederativaId");
                int ordEntidadFederativa = reader.GetOrdinal("EntidadFederativa");
                int Abreviacion = reader.GetOrdinal("Abreviacion");

                while (reader.Read()) {
                    entidadesFederativas.Add(new EntidadesFederativasDto {
                        EntidadFederativaId = reader.IsDBNull(ordEntidadFederativaId) ? (byte)0 : reader.GetByte(ordEntidadFederativaId),
                        EntidadFederativa   = reader.IsDBNull(ordEntidadFederativa) ? string.Empty : reader.GetString(ordEntidadFederativa),
                        Abreviacion         = reader.IsDBNull(Abreviacion) ? string.Empty : reader.GetString(Abreviacion)   
                    });
                }
            }

            // IMPORTANTE:
            // los parámetros OUTPUT se leen después de cerrar el DataReader.

            int mensajeId = pMsg.Value == DBNull.Value ? 0: Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<EntidadesFederativasDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = entidadesFederativas
            };
        }


        public StoreResult<List<MarcasDto>> SpAppMarcasGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var marcas = new List<MarcasDto >();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppMarcasGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiAccesoId", SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;

            using (var reader = cmd.ExecuteReader()) {
                int MarcaId = reader.GetOrdinal("MarcaId");
                int Marca = reader.GetOrdinal("Marca");
                int TipoMarcaId = reader.GetOrdinal("TipoMarcaId");

                while (reader.Read()) {
                    marcas.Add(new MarcasDto {
                        MarcaId = reader.IsDBNull(MarcaId) ? 0 : reader.GetInt32(MarcaId),
                        Marca = reader.IsDBNull(Marca) ? string.Empty : reader.GetString(Marca),
                        TipoMarcaId = reader.IsDBNull(TipoMarcaId) ? (byte)0 : reader.GetByte(TipoMarcaId)
                    });
                }
            }

            // IMPORTANTE:
            // los parámetros OUTPUT se leen después de cerrar el DataReader.

            int mensajeId = pMsg.Value == DBNull.Value ? 0: Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<MarcasDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = marcas
            };
        }
        public StoreResult<List<SubmarcaDto>> SpAppSubmarcasGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId,  int marcaId,    short modelo) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var submarcas = new List<SubmarcaDto>();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppSubmarcasGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado",SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;
            cmd.Parameters.Add("@uiAccesoId",   SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@iMarcaId",     SqlDbType.Int             ).Value = marcaId;
            cmd.Parameters.Add("@siModelo",     SqlDbType.SmallInt        ).Value = modelo;

            using (var reader = cmd.ExecuteReader()) {
                int ordSubmarcaId = reader.GetOrdinal("SubmarcaId");
                int ordSubmarca   = reader.GetOrdinal("Submarca");

                while (reader.Read()) {
                    submarcas.Add(new SubmarcaDto {
                        SubmarcaId = reader.IsDBNull(ordSubmarcaId) ? 0 : Convert.ToInt32(reader.GetValue(ordSubmarcaId)),
                        Submarca = reader.IsDBNull(ordSubmarca)     ? string.Empty : reader.GetString(ordSubmarca)
                    });
                }
            }
            int mensajeId = pMsg.Value == DBNull.Value ? 0 : Convert.ToInt32(pMsg.Value);
            short resultadoId =  pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<SubmarcaDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = submarcas
            };
        }

        public StoreResult<List<TiposAdeudosDto>> SpAppTiposAdeudosGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var tiposAdeudos = new List<TiposAdeudosDto >();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppTiposAdeudosGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiAccesoId", SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;

            using (var reader = cmd.ExecuteReader()) {
                int TipoAdeudoId = reader.GetOrdinal("TipoAdeudoId");
                int TipoAdeudo = reader.GetOrdinal("TipoAdeudo");

                while (reader.Read()) {
                    tiposAdeudos.Add(new TiposAdeudosDto {
                        TipoAdeudoId = reader.IsDBNull(TipoAdeudoId) ? (byte)0 : reader.GetByte(TipoAdeudoId),
                        TipoAdeudo = reader.IsDBNull(TipoAdeudo) ? string.Empty : reader.GetString(TipoAdeudo   )
                    });
                }
            }

            // IMPORTANTE:
            // los parámetros OUTPUT se leen después de cerrar el DataReader.

            int mensajeId = pMsg.Value == DBNull.Value ? 0: Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<TiposAdeudosDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = tiposAdeudos
            };
        }

        public StoreResult<List<TiposLineasCapturaDto>> SpAppTiposLineasCapturaGet(SqlConnection cnn, Guid uiEstacionId, Guid accesoId) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var tiposAdeudos = new List<TiposLineasCapturaDto >();

            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppTiposLineasCapturaGet";
            cmd.CommandTimeout = _timeout;

            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiAccesoId", SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = uiEstacionId;

            using (var reader = cmd.ExecuteReader()) {
                int TipoLineaCapturaId = reader.GetOrdinal("TipoLineaCapturaId");
                int TipoLineaCaptura = reader.GetOrdinal("TipoLineaCaptura");
                int Vigencia = reader.GetOrdinal("Vigencia");
                int Mascara = reader.GetOrdinal("Mascara");
                int Conciliable = reader.GetOrdinal("Conciliable");
                int Verificable = reader.GetOrdinal("Verificable");
                int Url = reader.GetOrdinal("Url");

                while (reader.Read()) {
                    tiposAdeudos.Add(new TiposLineasCapturaDto {
                        TipoLineaCapturaId = reader.IsDBNull(TipoLineaCapturaId) ? (byte)0 : reader.GetByte(TipoLineaCapturaId),
                        TipoLineaCaptura = reader.IsDBNull(TipoLineaCaptura) ? string.Empty : reader.GetString(TipoLineaCaptura),
                        Vigencia = reader.IsDBNull(Vigencia) ? (byte)0 : reader.GetByte(Vigencia),
                        Mascara = reader.IsDBNull(Mascara) ? string.Empty : reader.GetString(Mascara),
                        Conciliable = reader.IsDBNull(Conciliable) ? false : reader.GetBoolean(Conciliable),
                        Verificable = reader.IsDBNull(Verificable) ? false : reader.GetBoolean(Verificable),
                        Url = reader.IsDBNull(Url) ? string.Empty : reader.GetString(Url)
                    });
                }
            }

            // IMPORTANTE:
            // los parámetros OUTPUT se leen después de cerrar el DataReader.

            int mensajeId = pMsg.Value == DBNull.Value ? 0: Convert.ToInt32(pMsg.Value);
            short resultadoId = pRes.Value == DBNull.Value ? (short)0 : Convert.ToInt16(pRes.Value);

            return new StoreResult<List<TiposLineasCapturaDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = tiposAdeudos
            };
        }

        #region Verificacion Anterior
        public async Task<StoreResult<List<VerificacionAnteriorDto>>> SpAppCapturaVerificacionesAnterioresGetAsync(SqlConnection cnn, Guid estacionId,Guid accesoId, Guid verificacionId, CancellationToken ct = default) {
            if (cnn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var verificaciones = new List<VerificacionAnteriorDto>();
            using var cmd = cnn.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VfcCaptura.SpAppCapturaVerificacionesAnterioresGet";
            cmd.CommandTimeout = _timeout;

            var pMensaje = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMensaje.Direction = ParameterDirection.Output;

            var pResultado = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pResultado.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@uiEstacionId", SqlDbType.UniqueIdentifier).Value = estacionId;
            cmd.Parameters.Add("@uiAccesoId",   SqlDbType.UniqueIdentifier).Value = accesoId;
            cmd.Parameters.Add("@uiVerificacionId",SqlDbType.UniqueIdentifier).Value = verificacionId;

            using (var reader = await cmd.ExecuteReaderAsync(ct)) {
                int ordVerificacionAntId = reader.GetOrdinal("VerificacionAntId");
                int ordFecha = reader.GetOrdinal("Fecha");
                int ordVencimiento = reader.GetOrdinal("Vencimiento");
                int ordPlaca = reader.GetOrdinal("Placa");
                int ordVin = reader.GetOrdinal("Vin");
                int ordMarca = reader.GetOrdinal("Marca");
                int ordSubMarca = reader.GetOrdinal("SubMarca");
                int ordModelo = reader.GetOrdinal("Modelo");
                int ordCombustible = reader.GetOrdinal("Combustible");
                int ordMarcaId = reader.GetOrdinal("MarcaId");
                int ordSubMarcaId = reader.GetOrdinal("SubMarcaId");
                int ordCombustibleId =  reader.GetOrdinal("CombustibleId");
                int ordNombre = reader.GetOrdinal("Nombre");
                int ordApelPaterno = reader.GetOrdinal("ApelPaterno");
                int ordApelMaterno = reader.GetOrdinal("ApelMaterno");
                int ordTarjetaFolio = reader.GetOrdinal("TarjetaFolio");
                int ordTarjetaFecha = reader.GetOrdinal("TarjetaFecha");
                int ordCertificadoFolio = reader.GetOrdinal("CertificadoFolio");

                while (await reader.ReadAsync(ct)) {
                    verificaciones.Add(
                        new VerificacionAnteriorDto {
                            VerificacionAntId =  reader.IsDBNull(ordVerificacionAntId) ? Guid.Empty : reader.GetGuid(ordVerificacionAntId),
                            Fecha =              reader.IsDBNull(ordFecha)             ? null       : reader.GetDateTime(ordFecha),
                            Vencimiento =        reader.IsDBNull(ordVencimiento)       ? null       : reader.GetDateTime(ordVencimiento),
                            Placa =              reader.IsDBNull(ordPlaca)             ? string.Empty: reader.GetString(ordPlaca),
                            Vin =                reader.IsDBNull(ordVin)               ? string.Empty: reader.GetString(ordVin),
                            Marca =              reader.IsDBNull(ordMarca)             ? string.Empty: reader.GetString(ordMarca),
                            SubMarca =           reader.IsDBNull(ordSubMarca)          ? string.Empty: reader.GetString(ordSubMarca),
                            Modelo =             reader.IsDBNull(ordModelo)            ? (short)0    : Convert.ToInt16(reader.GetValue(ordModelo)),
                            Combustible =        reader.IsDBNull(ordCombustible)       ? string.Empty: reader.GetString(ordCombustible),
                            MarcaId =            reader.IsDBNull(ordMarcaId)           ? 0           : Convert.ToInt32(reader.GetValue(ordMarcaId)),
                            SubMarcaId =         reader.IsDBNull(ordSubMarcaId)        ? 0           : Convert.ToInt32(reader.GetValue(ordSubMarcaId)),
                            CombustibleId =      reader.IsDBNull(ordCombustibleId)     ? (byte)0     : Convert.ToByte(reader.GetValue(ordCombustibleId)),
                            Nombre =             reader.IsDBNull(ordNombre)            ? string.Empty: reader.GetString(ordNombre),
                            ApelPaterno =        reader.IsDBNull(ordApelPaterno)       ? string.Empty: reader.GetString(ordApelPaterno),
                            ApelMaterno =        reader.IsDBNull(ordApelMaterno)       ? string.Empty: reader.GetString(ordApelMaterno),
                            TarjetaFolio =       reader.IsDBNull(ordTarjetaFolio)      ? string.Empty: Convert.ToString(reader.GetValue(ordTarjetaFolio)) ?? string.Empty,
                            TarjetaFecha =       reader.IsDBNull(ordTarjetaFecha)      ? null        : reader.GetDateTime(ordTarjetaFecha),
                            CertificadoFolio =   reader.IsDBNull(ordCertificadoFolio)  ? 0           : Convert.ToInt32(reader.GetValue(ordCertificadoFolio))
                        });
                }
            }

            int mensajeId =     pMensaje.Value      == DBNull.Value ? 0         : Convert.ToInt32(pMensaje.Value);
            short resultadoId = pResultado.Value    == DBNull.Value ? (short) 0 : Convert.ToInt16(pResultado.Value);

            return new StoreResult<List<VerificacionAnteriorDto>> {
                MensajeId = mensajeId,
                ResultadoId = resultadoId,
                Data = verificaciones
            };
        }
        #endregion



        #endregion

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


        public async Task<SpAppProgramOnResult> SpAppProgramOn(SqlConnection conn, Guid estacionId) {
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


        public async Task<SpAppProgramOffResult> SpAppProgramOff(SqlConnection conn, Guid estacionId) {
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SivAppComun.SpAppProgramOff";
            cmd.CommandTimeout = _timeout;
            // outputs
            var pMsg = cmd.Parameters.Add("@iMensajeId", SqlDbType.Int);
            pMsg.Direction = ParameterDirection.Output;

            var pRes = cmd.Parameters.Add("@siResultado", SqlDbType.SmallInt);
            pRes.Direction = ParameterDirection.Output;

            // input
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = estacionId });

            cmd.ExecuteNonQuery();

            return new SpAppProgramOffResult {
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

        public async Task<CapturaVisualGetResult> SpAppCapturaVisualGetAsync(SqlConnection conn, Guid estacionId, Guid accesoId, Guid verificacionId, string? elemento, byte? tiCombustible, CancellationToken ct = default) {
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


        public async Task<ResultadoSql> SpAppCapturaInspeccionObd2SetAsync(SqlConnection conn, VisualRegistroWindows V, InspeccionObd2Set obd, CancellationToken ct = default) {
            int _MensajeId = 100;
            short _Resultado = 100;
            if (conn is null) throw new ArgumentNullException(nameof(conn));
            if (conn.State != ConnectionState.Open) await conn.OpenAsync(ct);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppCapturaInspeccionObd2Set";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas GUID
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar15 });
            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value =V.dvar20 });
            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar21 });

            // Cadenas
            cmd.Parameters.Add(new SqlParameter("@vcVehiculoId", SqlDbType.VarChar, 17) { Value = string.IsNullOrWhiteSpace(obd.VehiculoId) ? "DESCONOCIDO" : obd.VehiculoId.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcProtocoloObd", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(obd.ProtocoloObd) ? "DESCONOCIDO" : obd.ProtocoloObd.Trim() });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoError", SqlDbType.VarChar, 300) { Value = obd.CodigoError ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPendiente", SqlDbType.VarChar, 300) { Value = obd.CodigoErrorPendiente ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcCodigoErrorPermanente", SqlDbType.VarChar, 300) { Value = obd.CodigoErrorPermanente ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcDir_ECU", SqlDbType.VarChar, 32) { Value = obd.Dir_ECU ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcIDs_Adic", SqlDbType.VarChar, 64) { Value = obd.IDs_Adic ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcNumVerifCalib", SqlDbType.VarChar, 16) { Value = obd.ReadCvnsRobusto });
            cmd.Parameters.Add(new SqlParameter("@vcLista_CVN", SqlDbType.VarChar, 200) { Value = obd.ReadCvnsRobusto });
            cmd.Parameters.Add(new SqlParameter("@vcEst_Mon_DTC_Borrado", SqlDbType.VarChar, 100) { Value = obd.Est_Mon_DTC_Borrado ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_01_20", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_01_20 ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_21_40", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_21_40 ?? "DESCONOCIDO" });
            cmd.Parameters.Add(new SqlParameter("@vcPIDS_Sup_41_60", SqlDbType.VarChar, 300) { Value = obd.PIDS_Sup_41_60 ?? "DESCONOCIDO" });

            // TinyInt (byte)
            cmd.Parameters.Add(new SqlParameter("@tiNEV", SqlDbType.TinyInt) { Value = obd.NEV ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiConexionObd", SqlDbType.TinyInt) { Value = obd.ConexionObd });
            cmd.Parameters.Add(new SqlParameter("@tiIntentos", SqlDbType.TinyInt) { Value = obd.Intentos });
            cmd.Parameters.Add(new SqlParameter("@tiMil", SqlDbType.TinyInt) { Value = obd.Mil ?? 0});
            cmd.Parameters.Add(new SqlParameter("@siFallas", SqlDbType.TinyInt) { Value = obd.Fallas ?? 0 }); // SP lo define tinyint
            cmd.Parameters.Add(new SqlParameter("@tiSdciic", SqlDbType.TinyInt) { Value = obd.Sdciic ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSecc", SqlDbType.TinyInt) { Value = obd.Secc ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSc", SqlDbType.TinyInt) { Value = obd.Sc ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSso", SqlDbType.TinyInt) { Value = obd.Sso ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSci", SqlDbType.TinyInt) { Value = obd.Sci ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSccc", SqlDbType.TinyInt) { Value = obd.Sccc ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSe", SqlDbType.TinyInt) { Value = obd.Se ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSsa", SqlDbType.TinyInt) { Value = obd.Ssa ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSfaa", SqlDbType.TinyInt) { Value = obd.Sfaa ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiScso", SqlDbType.TinyInt) { Value = obd.Scso ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSrge", SqlDbType.TinyInt) { Value = obd.Srge ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSpsa", SqlDbType.TinyInt) { Value = obd.Spsa ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSge", SqlDbType.TinyInt) { Value = obd.Sge ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSchnm", SqlDbType.TinyInt) { Value = obd.Schnm ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSfp", SqlDbType.TinyInt) { Value = obd.Sfp ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiSscrron", SqlDbType.TinyInt) { Value = obd.Sscrron ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiReq_Emisiones", SqlDbType.TinyInt) { Value = obd.Req_Emisiones ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiCombustible0151Id", SqlDbType.TinyInt) { Value = obd.Combustible0151Id ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@tiCombustible0907Id", SqlDbType.TinyInt) { Value = obd.Combustible0907Id ?? 0 });

            // Decimales
            cmd.Parameters.Add(new SqlParameter("@dVoltsSwOff", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = obd.VoltsSwOff ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dVoltsSwOn", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = obd.VoltsSwOn ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dSTFT_B1", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.STFT_B1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dLTFT_B1", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.LTFT_B1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dMAF", SqlDbType.Decimal) { Precision = 8, Scale = 3, Value = obd.MAF ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dTPS", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.TPS ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dAvanceEnc", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.AvanceEnc ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dVolt_O2", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd.Volt_O2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dVolt_O2_S2", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd.Volt_O2_S2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dNivelComb", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.NivelComb ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dCCM", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd.CCM ?? 0 });


            // Smallint
            cmd.Parameters.Add(new SqlParameter("@siIAT", SqlDbType.SmallInt) { Value = obd.IAT ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siRpmOn", SqlDbType.SmallInt) { Value = obd.RpmOn ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siRpmOff", SqlDbType.SmallInt) { Value = obd.RpmOff ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siRpmCheck", SqlDbType.SmallInt) { Value = obd.RpmCheck ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siCodigoProtocolo", SqlDbType.SmallInt) { Value = obd.CodigoProtocolo ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siVelVeh", SqlDbType.SmallInt) { Value = obd.VelVeh ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siPres_Baro", SqlDbType.SmallInt) { Value = obd.Pres_Baro ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siTR", SqlDbType.SmallInt) { Value = obd.TR ?? 0 });


            // Bits
            cmd.Parameters.Add(new SqlParameter("@bLeeMonitores", SqlDbType.Bit) { Value = obd.LeeMonitores ?? false    });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtc", SqlDbType.Bit) { Value = obd.LeeDtc ?? false });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPend", SqlDbType.Bit) { Value = obd.LeeDtcPend ?? false });
            cmd.Parameters.Add(new SqlParameter("@bLeeDtcPerm", SqlDbType.Bit) { Value = obd.LeeDtcPerm ?? false });
            cmd.Parameters.Add(new SqlParameter("@bLeeVin", SqlDbType.Bit) { Value = obd.LeeVin ?? false });


            // Ints
            cmd.Parameters.Add(new SqlParameter("@intTpo_Arranque", SqlDbType.Int) { Value = obd.TiempoDeArranque ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intMotorTipoId", SqlDbType.Int) { Value = obd.MotorTipoId ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intDist_MIL_On", SqlDbType.Int) { Value = obd.Dist_MIL_On ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intDist_Borrado_DTC", SqlDbType.Int) { Value = obd.Dist_Borrado_DTC ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intTpo_MIL_On", SqlDbType.Int) { Value = obd.Tpo_MIL_On ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intTpo_Borrado_DTC", SqlDbType.Int) { Value = obd.Tpo_Borrado_DTC ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intID_Calib", SqlDbType.Int) { Value = obd.ID_Calib ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intCalentamientosPostDTC", SqlDbType.Int) { Value = obd.WarmUpsDesdeBorrado ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intNumVerifCalib", SqlDbType.Int) { Value = obd.ReadCvnMessageCount ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@intTpoMotor", SqlDbType.Int) { Value = obd.TiempoMotorEnMarchaSeg ?? 0 });

            
            //BigInt
            cmd.Parameters.Add(new SqlParameter("@biOdometro", SqlDbType.BigInt) { Value = obd.Odometro ?? 0 });

            // NUEVOS VALORES 
            /*
            cmd.Parameters.Add(new SqlParameter("@vcVersionSoftware", SqlDbType.VarChar, 20) { Value = string.IsNullOrWhiteSpace(obd.VehiculoId) ? "DESCONOCIDO" : obd._VersionSoftware.Trim() });
            // Decimal
            cmd.Parameters.Add(new SqlParameter("@dLambda", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._Lambda ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB2S1", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B2S1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB2S2", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B2S2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB1S1", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B1S1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB1S2", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B1S2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dSTFT_B2", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd._StftB2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dLTFT_B2", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd._LtftB2  ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB1S1_V", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B1S1_V ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB1S2_V", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B1S2_V ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB2S1_V", SqlDbType.Decimal) { Precision = 5, Scale = 3, Value = obd._B2S1_V ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dB2S2_V", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd._B2S2_V ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dRelativeAcceleratorPedalPosition", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd._RelativeAcceleratorPedalPosition ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@dAbsoluteLoadValue", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = obd._AbsoluteLoadValue ?? 0 });
            //smallint
            cmd.Parameters.Add(new SqlParameter("@siEGT_B1S1", SqlDbType.SmallInt) { Value = obd._EgtB1S1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siEGT_B2S1", SqlDbType.SmallInt) { Value = obd._EgtB2S1 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siEGT_B1S2", SqlDbType.SmallInt) { Value = obd._EgtB1S2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siEGT_B2S2", SqlDbType.SmallInt) { Value = obd._EgtB2S2 ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siMAP", SqlDbType.SmallInt) { Value = obd._Map ?? 0 });
            
            cmd.Parameters.Add(new SqlParameter("@siAmbientAirTemperature", SqlDbType.SmallInt) { Value = obd._AmbientAirTemperature ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siEngineOilTemperature", SqlDbType.SmallInt) { Value = obd._EngineOilTemperature ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siEngineCoolantTemperature", SqlDbType.SmallInt) { Value = obd._EngineCoolantTemperature ?? 0 });
            cmd.Parameters.Add(new SqlParameter("@siIntakeAirTemperature", SqlDbType.SmallInt) { Value = obd._IntakeAirTemperature ?? 0 });
            */

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


            return new ResultadoSql {
                MensajeId = _MensajeId,
                Resultado = _Resultado
            };
        }
        public async Task<ResultadoSql> SpSpAppBitacoraErroresSetAsyncPool(SqlConnection connApp, VisualRegistroWindows visual,SpAppBitacoraErroresSet bitacora, CancellationToken ct = default) { 
            short _Resultado = 0;
            int   _MensajeId = 0;
            if (connApp is null) {
                SivevLogger.Error($"SpAppBitacoraErroresSetAsyncPool: connApp is null");
                throw new ArgumentNullException(nameof(connApp));
            }
            if (connApp.State != ConnectionState.Open) {
                SivevLogger.Error($"SpAppBitacoraErroresSetAsyncPool: connApp is not open");
                throw new InvalidOperationException("La conexión SQL debe estar abierta y enrolada al AppRole.");
            }

            using var cmd = connApp.CreateCommand();
            cmd.CommandText = "SivAppComun.SpAppBitacoraErroresSet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = bitacora.EstacionId });
            cmd.Parameters.Add(new SqlParameter("@siCentro", SqlDbType.SmallInt) { Value = bitacora.Centro });
            cmd.Parameters.Add(new SqlParameter("@vcNombreCpu", SqlDbType.VarChar, 25) { Value = bitacora.NombreCpu });
            cmd.Parameters.Add(new SqlParameter("@siOpcionMenuId", SqlDbType.SmallInt) { Value = bitacora.OpcionMenuId });
            cmd.Parameters.Add(new SqlParameter("@dtFechaError", SqlDbType.DateTime) { Value = bitacora.FechaError });
            cmd.Parameters.Add(new SqlParameter("@vcLibreria", SqlDbType.VarChar, 50) { Value = bitacora.Libreria });
            cmd.Parameters.Add(new SqlParameter("@vcClase", SqlDbType.VarChar, 50) { Value = bitacora.Clase });
            cmd.Parameters.Add(new SqlParameter("@vcMetodo", SqlDbType.VarChar, 50) { Value = bitacora.Metodo });
            cmd.Parameters.Add(new SqlParameter("@iCodigoErrorSql", SqlDbType.Int) { Value = bitacora.CodigoErrorSql });
            cmd.Parameters.Add(new SqlParameter("@iCodigoError", SqlDbType.Int) { Value = bitacora.CodigoError });
            cmd.Parameters.Add(new SqlParameter("@vcDescripcionError", SqlDbType.VarChar, 500) { Value = bitacora.DescripcionError?.Length > 500 ? bitacora.DescripcionError.Substring(0, 500) : (object?)bitacora.DescripcionError ?? DBNull.Value});
            cmd.Parameters.Add(new SqlParameter("@iLineaCodigo", SqlDbType.Int) { Value = bitacora.LineaCodigo });
            cmd.Parameters.Add(new SqlParameter("@iLastDllError", SqlDbType.Int) { Value = bitacora.LastDllError });
            cmd.Parameters.Add(new SqlParameter("@vcSourceError", SqlDbType.VarChar, 50) { Value = bitacora.SourceError });
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

            return new ResultadoSql {
                Resultado = _Resultado,
                MensajeId = _MensajeId,
            };
        }


        public async Task<ResultadoSql> SpAppCapturaAbandonaAsync(VisualRegistroWindows V, CancellationToken ct = default) {
            short _Resultado = 0;
            int   _MensajeId = 0;
            try {
                using (var connApp = SqlConnectionFactory.Create(server: V.dvar1, db: V.dvar2, user: V.dvar3, pass: V.dvar4, appName: V.dvar5)) {
                    if (connApp.State != ConnectionState.Open) await connApp.OpenAsync(ct);
                    using (var scope = new AppRoleScope(connApp, role: V.dvar17, password: V.dvar16.ToString().ToUpper())) {

                        using (var cmd = connApp.CreateCommand()) {
                            cmd.CommandText = "VfcVisual.SpAppCapturaAbandona";
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Entradas
                            cmd.Parameters.Add(new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value =V.dvar20 });
                            cmd.Parameters.Add(new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar15 });
                            cmd.Parameters.Add(new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar21 });

                            // Salidas
                            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { Direction = ParameterDirection.Output };

                            cmd.Parameters.Add(pMensajeId);
                            cmd.Parameters.Add(pResultado);

                            await cmd.ExecuteNonQueryAsync(ct);
                            _Resultado = (pResultado.Value == DBNull.Value) ? (short)0 : Convert.ToInt16(pResultado.Value);
                            _MensajeId = (pMensajeId.Value == DBNull.Value) ? 0 : Convert.ToInt32(pMensajeId.Value);

                        }

                    }
                }
            } catch (Exception e) {
                SivevLogger.Error($"VfcVisual.SpAppCapturaAbandona {e.Message}");
            }
            return new ResultadoSql {
                Resultado = _Resultado,
                MensajeId = _MensajeId,
            };
        }

        public async Task<SpAppBitacoraErroresSet> SpSpAppBitacoraErroresSetAsync(VisualRegistroWindows V, SpAppBitacoraErroresSet A, CancellationToken ct = default) {
            short _Resultado = 0;
            int   _MensajeId = 0;
            try {
                using (var connApp = SqlConnectionFactory.Create(server: V.dvar1, db: V.dvar2, user: V.dvar3, pass: V.dvar4, appName: V.dvar5)) {
                    if (connApp.State != ConnectionState.Open) await connApp.OpenAsync(ct);
                    using (var scope = new AppRoleScope(connApp, role: V.dvar17, password: V.dvar16.ToString().ToUpper())) {

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


        public async Task<SpAppBitacoraErroresSet> SpSpAppBitacoraErroresSetAsync2026(SqlConnection cnn, SpAppBitacoraErroresSet A, CancellationToken ct = default) {
            short _Resultado = 0;
            int   _MensajeId = 0;
            try {
                        using var cmd = cnn.CreateCommand();
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

                    
                
            } catch (Exception e) {
                SivevLogger.Error($"SivSpComun.SpAppBitacoraErroresSet {e}");
            }
            return new SpAppBitacoraErroresSet {
                Resultado = _Resultado,
                MensajeId = _MensajeId,
            };
        }






        public async Task<SpAppDatosVehiculoObdNewSet> SpAppDatosVehiculoObdNewGetSetAsync(SqlConnection connApp, VisualRegistroWindows V, CancellationToken ct = default) {
            if (connApp is null) {
                SivevLogger.Error($"SpAppDatosVehiculoObdNewGetSetAsync: connApp is null");
                throw new ArgumentNullException(nameof(connApp));
            }
            if (connApp.State != ConnectionState.Open) {
                SivevLogger.Error($"SpAppDatosVehiculoObdNewGetSetAsync: connApp is not open");
                throw new InvalidOperationException("La conexión SQL debe estar abierta y enrolada al AppRole.");
            }
            var resultado = new SpAppDatosVehiculoObdNewSet();
            using var cmd = connApp.CreateCommand();
            cmd.CommandText = "VfcVisual.SpAppDatosVehiculoObdNewGet";
            cmd.CommandType = CommandType.StoredProcedure;

            // Entradas
            var pEstacionId = new SqlParameter("@uiEstacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar15 };
            var pAccesoId = new SqlParameter("@uiAccesoId", SqlDbType.UniqueIdentifier) { Value = V.dvar20 };
            var pVerificacionId = new SqlParameter("@uiVerificacionId", SqlDbType.UniqueIdentifier) { Value = V.dvar21 };

            // Salidaas
            var pMensajeId = new SqlParameter("@iMensajeId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var pResultado = new SqlParameter("@siResultado", SqlDbType.SmallInt) { Direction = ParameterDirection.Output };

            // Valor de retorno (RETURN @@ERROR)
            var pReturn = new SqlParameter { Direction = ParameterDirection.ReturnValue };

            cmd.Parameters.Add(pMensajeId);
            cmd.Parameters.Add(pResultado);
            cmd.Parameters.Add(pEstacionId);
            cmd.Parameters.Add(pAccesoId);
            cmd.Parameters.Add(pVerificacionId);
            cmd.Parameters.Add(pReturn);

            await using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, ct)) {
                if (await reader.ReadAsync(ct)) {
                    resultado.Marca = ObtenerString(reader, "Marca");
                    resultado.SubMarca = ObtenerString(reader, "SubMarca");
                    resultado.Modelo = ObtenerString(reader, "Modelo");
                    resultado.DTCConfirmado = ObtenerString(reader, "CodigoError");
                    resultado.DTCPendiente = ObtenerString(reader, "CodigoErrorPendientes");
                    resultado.Protocolo = ObtenerString(reader, "ProtocoloObd");
                }
            }
            resultado.MensajeId = pMensajeId.Value == DBNull.Value ? 0 : Convert.ToInt32(pMensajeId.Value);
            resultado.Resultado = pResultado.Value == DBNull.Value ? 0 : Convert.ToInt32(pResultado.Value);
            int codigoRetorno = pReturn.Value == DBNull.Value? 0 : Convert.ToInt32(pReturn.Value);
            if (codigoRetorno != 0) {
                SivevLogger.Error($"VfcVisual.SpAppDatosVehiculoObdNewGet retornó el código {codigoRetorno}");
            }
            return resultado;
        }

        private static string ObtenerString(SqlDataReader reader, string nombreColumna) {
            int ordinal = reader.GetOrdinal(nombreColumna);
            if (reader.IsDBNull(ordinal))
                return string.Empty;
            return Convert.ToString(reader.GetValue(ordinal)) ?? string.Empty;
        }
        //*/

        public async Task<EstacionEncontrada> ObtenerEstacionPorIpAsync(string ip, cnx conf, int aplicacionId) {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("La IP no puede venir vacía.", nameof(ip));

            if (!System.Net.IPAddress.TryParse(ip, out _))
                throw new InvalidOperationException("La IP no tiene un formato válido.");

            string[] partesIp = ip.Split('.');
            int ultimoOctetoIp = int.Parse(partesIp[3]);

            string cadenaConexion =
                $"Server={conf.Servidor};" +
                $"Database={conf.BDD};" +
                $"User Id={conf.User};" +
                $"Password={conf.Pass};" +
                $"Application Name={conf.AppName};" +
                $"TrustServerCertificate=True;";

            using var conn = new SqlConnection(cadenaConexion);
            await conn.OpenAsync();
            var resultado = new EstacionEncontrada();

            // Estación
            using (var cmd = new SqlCommand(@"
                SELECT EstacionId, E.VerificentroId AS CentroId, CVV.NombreDominio AS Centro
                FROM Sivev.Equipos.Estaciones E
                    JOIN Sivev.Verificentros.Verificentros  CVV ON CVV.VerificentroId = E.VerificentroId
                WHERE TipoEstacionId = @TipoEstacionId
                     AND E.Activo = 1;", conn)) {

                cmd.Parameters.Add("@TipoEstacionId", SqlDbType.Int).Value = ultimoOctetoIp;

                using var rd = await cmd.ExecuteReaderAsync();

                if (!await rd.ReadAsync())
                    throw new InvalidOperationException($"No se encontró una estación activa para la IP {ip}.");

                resultado.EstacionId = rd.GetGuid(rd.GetOrdinal("EstacionId"));
                resultado.CentroId = Convert.ToInt16(rd["CentroId"]);
                resultado.Centro = Convert.ToString(rd["Centro"]) ?? string.Empty;
            }

            // Aplicación
            using (var cmd = new SqlCommand(@"
                SELECT Aplicacion
                FROM Sivev.Aplicaciones.Aplicaciones
                WHERE AplicacionId = @AplicacionId;", conn)) {

                cmd.Parameters.Add("@AplicacionId", SqlDbType.Int).Value = aplicacionId;
                object? aplicacion =  await cmd.ExecuteScalarAsync();
                resultado.Aplicacion = aplicacion?.ToString() ?? string.Empty;
            }

            return resultado;
        }
    }
}
