using FrmComun.Utils;
using Microsoft.Data.SqlClient;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;
using SQLSIVEV.Domain.Models;
using static SQLSIVEV.Infrastructure.Config.AppConfig;

namespace FrmComun.CapturaCentralizada.Complementos {
     public sealed class CatalogosCaptura {
        private readonly SivevConnectionFactory _sql;
        private readonly string _roll;
        private readonly string _passRoll;
        private readonly Guid _estacionId;
        private readonly Guid _accesoId;
        private readonly short _opcionMenu;
        private readonly short _centro;



        public IReadOnlyList<CombustibleDto> Combustibles { get; private set; }
            = Array.Empty<CombustibleDto>();

        public IReadOnlyList<EntidadesFederativasDto> EntidadesFederativas { get; private set; }
            = Array.Empty<EntidadesFederativasDto>();

        public IReadOnlyList<MarcasDto> Marcas { get; private set; }
            = Array.Empty<MarcasDto>();

        public IReadOnlyList<TiposAdeudosDto> TiposAdeudos { get; private set; }
            = Array.Empty<TiposAdeudosDto>();

        public IReadOnlyList<TiposLineasCapturaDto> TiposLineasCaptura { get; private set; }
            = Array.Empty<TiposLineasCapturaDto>();

        public IReadOnlyList<SubmarcaDto> Submarcas { get; private set; } 
            = Array.Empty<SubmarcaDto>();



        public CatalogosCaptura(SivevConnectionFactory sql, string roll, string passRoll, Guid estacion, Guid accesoId, short opcionMenu, short centro) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = passRoll ?? throw new ArgumentNullException(nameof(passRoll));
            _estacionId = estacion;
            _accesoId = accesoId;
            _opcionMenu = opcionMenu;
            _centro = centro;
        }


        public async Task CargarAsync(CancellationToken ct = default) {
            Limpiar();

            Combustibles            = await ObtenerCombustiblesAsync(ct);
            EntidadesFederativas    = await ObtenerEntidadesFederativasAsync(ct);
            Marcas                  = await ObtenerMarcasAsync(ct);
            TiposAdeudos            = await ObtenerTiposAdeudosAsync(ct);
            TiposLineasCaptura      = await ObtenerTiposLineasCapturaAsync(ct);
        }

        public void Limpiar() {
            Combustibles = Array.Empty<CombustibleDto>();
            EntidadesFederativas = Array.Empty<EntidadesFederativasDto>();
            Marcas = Array.Empty<MarcasDto>();
            TiposAdeudos = Array.Empty<TiposAdeudosDto>();
            TiposLineasCaptura = Array.Empty<TiposLineasCapturaDto>();
            Submarcas = Array.Empty<SubmarcaDto>();
        }

        #region Metodos SQL
        #region Combustibles 
        private async Task<IReadOnlyList<CombustibleDto>> ObtenerCombustiblesAsync( CancellationToken ct = default) {
            int mensaje = 0;
            short resultado = 0;

            var combustibles = new List<CombustibleDto>();
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppCombustiblesGet(cnn: connApp, uiEstacionId: _estacionId, accesoId: _accesoId);

                    mensaje = r.MensajeId;
                    resultado = r.ResultadoId;
                    combustibles = r.Data;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, $"Error en SpAppCombustiblesGet {mensaje}",  mensaje);
                            var bitacora = Bitacora.ErroresSQL(
                                estacionId: _estacionId,
                                centro: _centro,
                                opcionMenuId: _opcionMenu,
                                descripcion: error.Mensaje,
                                codigoSql: mensaje
                            );
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje($"SpAppCombustiblesGet {mensaje}", error.Mensaje);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló la bitácora en SpAppCombustiblesGet: {logEx.Message}", SivevOrigen.Captura);
                        }
                    }

                }, ct);
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al obtener combustibles: {e.Message}",
                            codigoSql: mensaje
                        );
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de SpAppCombustiblesGet: {logEx}", SivevOrigen.Captura);
                }
                Mostrar.Mensaje("Error en SpAppCombustiblesGet", e.Message);
            }
            return combustibles;
        }
        #endregion
        #region Entidades Federativas 
        private async Task<IReadOnlyList<EntidadesFederativasDto>> ObtenerEntidadesFederativasAsync(CancellationToken ct = default) {
            int mensaje = 0;
            short resultado = 0;

            var entidadesFederativas = new List<EntidadesFederativasDto >();
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppEntidadesFederativasGet(cnn: connApp, uiEstacionId: _estacionId, accesoId: _accesoId);

                    mensaje = r.MensajeId;
                    resultado = r.ResultadoId;
                    entidadesFederativas = r.Data;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppEntidadesFederativasGet",  mensaje);
                            var bitacora = Bitacora.ErroresSQL(
                                estacionId: _estacionId,
                                centro: _centro,
                                opcionMenuId: _opcionMenu,
                                descripcion: error.Mensaje,
                                codigoSql: mensaje
                            );
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje("SpAppEntidadesFederativasGet", error.Mensaje);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló la bitácora en SpAppEntidadesFederativasGet: {logEx.Message}", SivevOrigen.Captura);
                        }
                    }

                }, ct);
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al obtener entidades federativas: {e.Message}",
                            codigoSql: mensaje
                        );
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de SpAppEntidadesFederativasGet: {logEx}", SivevOrigen.Captura);
                }
                Mostrar.Mensaje("Error en SpAppEntidadesFederativasGet", e.Message);
            }
            return entidadesFederativas;
        }
        #endregion
        #region Marcas
        private async Task<IReadOnlyList<MarcasDto>> ObtenerMarcasAsync(CancellationToken ct = default) {
            int mensaje = 0;
            short resultado = 0;

            var marcas = new List<MarcasDto >();
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppMarcasGet(cnn: connApp, uiEstacionId: _estacionId, accesoId: _accesoId);

                    mensaje = r.MensajeId;
                    resultado = r.ResultadoId;
                    marcas = r.Data;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppMarcasGet",  mensaje);
                            var bitacora = Bitacora.ErroresSQL(
                                estacionId: _estacionId,
                                centro: _centro,
                                opcionMenuId: _opcionMenu,
                                descripcion: error.Mensaje,
                                codigoSql: mensaje
                            );
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje("SpAppMarcasGet", error.Mensaje);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló la bitácora en SpAppMarcasGet: {logEx.Message}", SivevOrigen.Captura);
                        }
                    }

                }, ct);
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al obtener las marcas: {e.Message}",
                            codigoSql: mensaje
                        );
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de SpAppMarcasGet: {logEx}", SivevOrigen.Captura);
                }
                Mostrar.Mensaje("Error en SpAppMarcasGet", e.Message);
            }
            return marcas;
        }
        #endregion
        #region SubMarcas
        public async Task CargarSubmarcasAsync(int marcaId, int modelo,  CancellationToken ct = default) {
            Submarcas = await ObtenerSubmarcasAsync(marcaId, modelo, ct);
        }
        private async Task<IReadOnlyList<SubmarcaDto>> ObtenerSubmarcasAsync(int marcaId, int modelo, CancellationToken ct = default) {
            int mensaje = 0;
            var submarcas = new List<SubmarcaDto>();

            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppSubmarcasGet(
                        cnn: connApp,
                        uiEstacionId: _estacionId,
                        accesoId: _accesoId,
                        marcaId: marcaId,
                        modelo: checked((short)modelo)
                    );

                    mensaje = r.MensajeId;
                    submarcas = r.Data;

                    if (mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp,  $"Error en SpAppSubmarcasGet {mensaje}",     mensaje);
                        Mostrar.Mensaje("SpAppSubmarcasGet", error.Mensaje);
                    }

                }, ct);
            } catch (Exception e) {
                SivevLogger.Error($"Error en SpAppSubmarcasGet: {e}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error en SpAppSubmarcasGet", e.Message);
            }
            return submarcas;
        }
        #endregion
        #region Tipos de audeudos
        private async Task<IReadOnlyList<TiposAdeudosDto>> ObtenerTiposAdeudosAsync(CancellationToken ct = default) {
            int mensaje = 0;
            short resultado = 0;

            var tiposAdeudosDtos = new List<TiposAdeudosDto >();
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppTiposAdeudosGet(cnn: connApp, uiEstacionId: _estacionId, accesoId: _accesoId);

                    mensaje = r.MensajeId;
                    resultado = r.ResultadoId;
                    tiposAdeudosDtos = r.Data;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppTiposAdeudosGet",  mensaje);
                            var bitacora = Bitacora.ErroresSQL(
                                estacionId: _estacionId,
                                centro: _centro,
                                opcionMenuId: _opcionMenu,
                                descripcion: error.Mensaje,
                                codigoSql: mensaje
                            );
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje("SpAppTiposAdeudosGet", error.Mensaje);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló la bitácora en SpAppTiposAdeudosGet: {logEx.Message}", SivevOrigen.Captura);
                        }
                    }

                }, ct);
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al obtener entidades SpAppTiposAdeudosGet: {e.Message}",
                            codigoSql: mensaje
                        );
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de SpAppTiposAdeudosGet: {logEx}", SivevOrigen.Captura);
                }
                Mostrar.Mensaje("Error en SpAppTiposAdeudosGet", e.Message);
            }
            return tiposAdeudosDtos;
        }
        #endregion
        #region Tipos de audeudos
        private async Task<IReadOnlyList<TiposLineasCapturaDto>> ObtenerTiposLineasCapturaAsync(CancellationToken ct = default) {
            int mensaje = 0;
            short resultado = 0;

            var tiposAdeudosDtos = new List<TiposLineasCapturaDto>();
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppTiposLineasCapturaGet(cnn: connApp, uiEstacionId: _estacionId, accesoId: _accesoId);

                    mensaje = r.MensajeId;
                    resultado = r.ResultadoId;
                    tiposAdeudosDtos = r.Data;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppTiposLineasCapturaGet",  mensaje);
                            var bitacora = Bitacora.ErroresSQL(
                                estacionId: _estacionId,
                                centro: _centro,
                                opcionMenuId: _opcionMenu,
                                descripcion: error.Mensaje,
                                codigoSql: mensaje
                            );
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje("SpAppTiposLineasCapturaGet", error.Mensaje);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló la bitácora en SpAppTiposLineasCapturaGet: {logEx.Message}", SivevOrigen.Captura);
                        }
                    }

                }, ct);
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al obtener entidades federativas: {e.Message}",
                            codigoSql: mensaje
                        );
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de SpAppTiposAdeudosGet: {logEx}", SivevOrigen.Captura);
                }
                Mostrar.Mensaje("Error en SpAppTiposAdeudosGet", e.Message);
            }
            return tiposAdeudosDtos;
        }
        #endregion
        #endregion


        /*

        #region Consulta de Adeudos 
        private async void ucAccesoConsulta1_CrearVerificacion(object? sender, EventArgs e) {
            try {
                await EjecutarSqlAsync(async connApp => {
                    var repo = new SivevRepository();

                    var r = await repo.SpAppCapturaIniciaWebSrvNewAsync(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        placa:"",// txtPlaca.Text.Trim(),

                        pet: false,
                        consultasSemoviId: 0,

                        vin: string.Empty,
                        modelo: 0,
                        tipoServicio: string.Empty,
                        folioAuto: string.Empty,
                        fechaTC: new DateTime(1900, 1, 1),

                        testFM: false,

                        conexionWs: false,
                        conexionWebSrv: 1,

                        adeudoFotoCivicas: false,
                        adeudoTenencia: false,
                        adeudoInfraccion: false,
                        gdfNoRegistrado: false
                    );

                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"Error en SpAppCapturaIniciaWebSrvNew {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al iniciar verificación", error.Mensaje);
                        return;
                    }

                    if (r.VerificacionId is null) {
                        Mostrar.Mensaje("Error", "No se recibió un identificador de verificación.");
                        return;
                    }

                    _verificacionId = r.VerificacionId.Value;

                    // Continuar el flujo
                    FlujoGrama(EtapaCaptura.Vehiculo);
                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al iniciar verificación", ex.Message);
                SivevLogger.Error($"SpAppCapturaIniciaWebSrvNew: {ex}", SivevOrigen.Captura);
            }
        }
        #endregion
        */




        #region Ejecutar SQL
        private async Task EjecutarSqlAsync(Func<SqlConnection, Task> accion, CancellationToken ct = default) {

            await using var session = await _sql.OpenSessionAsync( new AppRoleConfig {
                Nombre = _roll,
                Password = _passRoll,
                Habilitado = true
            }
            );

            await accion(session.Connection);
        }

        private async Task<T> EjecutarSqlAsync<T>(Func<SqlConnection, Task<T>> accion, CancellationToken ct = default) {
            await using var session = await _sql.OpenSessionAsync( new AppRoleConfig {
                Nombre = _roll,
                Password = _passRoll,
                Habilitado = true
            }
            );
            return await accion(session.Connection);
        }
        #endregion
    }
}
