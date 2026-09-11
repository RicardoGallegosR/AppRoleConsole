using Microsoft.Data.SqlClient;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLSIVEV.Infrastructure.Sql;
using FrmComun.Utils;

namespace FrmComun.Login {
    public sealed class AplicacionLifecycleService {
        public event Action<Guid, string>? AppRoleObtenido;
        private readonly SivevConnectionFactory _sql;

        private readonly string _roleBootstrap;
        private readonly string _passwordBootstrap;

        private string _roleAplicacion = string.Empty;
        private string _passwordRoleAplicacion = string.Empty;

        private readonly Guid _estacionId;
        private readonly short _centro;
        private readonly short _opcionMenu;
        private readonly string _origen;


        public AplicacionLifecycleService(SivevConnectionFactory sql, string roleBootstrap, string passwordBootstrap, Guid estacionId, short centro, short opcionMenu, string origen) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));

            _roleBootstrap = roleBootstrap ?? throw new ArgumentNullException(nameof(roleBootstrap));
            _passwordBootstrap = passwordBootstrap ?? throw new ArgumentNullException(nameof(passwordBootstrap));
            _estacionId = estacionId;
            _centro = centro;
            _opcionMenu = opcionMenu;
            _origen = origen;
        }

        public async Task<bool> InicializarAsync(CancellationToken ct = default) {
            try {
                await using var session = await _sql.OpenSessionAsync(
                    new AppRoleConfig {
                        Nombre = _roleBootstrap,
                        Password = _passwordBootstrap,
                        Habilitado = true
                    }
                );

                var repo = new SivevRepository();

                var r = repo.SpAppRollClaveGet(session.Connection);

                if (r.MensajeId != 0) {
                    var mensajeSql = await repo.PrintIfMsgAsync(session.Connection, "AplicacionLifecycleService.InicializarAsync", r.MensajeId);
                    Mostrar.Mensaje("Error al obtener AppRole", mensajeSql.Mensaje);
                    SivevLogger.Error($"Error en SpAppRollClaveGet.\nMensajeId: {r.MensajeId}. {mensajeSql.Mensaje}", _origen);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(r.ClaveAcceso) || string.IsNullOrWhiteSpace(r.FuncionAplicacion)) {
                    SivevLogger.Error("SpAppRollClaveGet no regresó rol o clave.", _origen);
                    return false;
                }

                string claveInvertida = new string(r.ClaveAcceso.Reverse().ToArray());

                Guid passwordRole = Guid.Parse(claveInvertida);
                _passwordRoleAplicacion = passwordRole.ToString().ToUpper();
                _roleAplicacion = r.FuncionAplicacion;

                // Entregamos los valores a la aplicación
                AppRoleObtenido?.Invoke(passwordRole, _roleAplicacion);
                SivevLogger.Information($"AppRole obtenido: {_roleAplicacion}", _origen);

                return true;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al obtener AppRole: {ex}", _origen);
                return false;
            }
        }



        public async Task<SpAppProgramOnResult> IniciarAsync(CancellationToken ct = default) {
            int mensaje = 100;
            short resultado = 0;

            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async conn => {
                    var r = await repo.SpAppProgramOn(conn: conn, estacionId: _estacionId);
                    resultado = r.Resultado;
                    mensaje = r.MensajeId;

                    if (mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(conn, "CicloVidaAplicacion.IniciarAsync", mensaje);
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId,  centro: _centro, opcionMenuId: _opcionMenu, descripcion: error.Mensaje, codigoSql: mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(conn, bitacora, ct);
                    }
                });
            } catch (Exception ex) {
                SivevLogger.Error($"Error al iniciar aplicación: {ex}", _origen);
            }
            return new SpAppProgramOnResult {
                MensajeId = mensaje,
                Resultado = resultado
            };
        }
        public async Task<SpAppProgramOffResult> FinalizarAsync(CancellationToken ct = default) {
            int mensaje = 100;
            short resultado = 0;

            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async conn => {
                    var r = await repo.SpAppProgramOff( conn: conn, estacionId: _estacionId);
                    resultado = r.Resultado;
                    mensaje = r.MensajeId;

                    if (mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(conn,"CicloVidaAplicacion.FinalizarAsync", mensaje);
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu, descripcion: error.Mensaje, codigoSql: mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(conn, bitacora, ct);
                        SivevLogger.Error($"Error al finalizar aplicación. MensajeId: {mensaje}. {error.Mensaje}", _origen );
                    }

                }, ct);
            } catch (Exception ex) {

                try {
                    await EjecutarSqlAsync(async conn => {
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(conn, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error( $"Falló la bitácora en CicloVidaAplicacion.FinalizarAsync: {logEx}", _origen);
                }
                SivevLogger.Error($"Error en CicloVidaAplicacion.FinalizarAsync: {ex}", _origen);
            }

            return new SpAppProgramOffResult {
                MensajeId = mensaje,
                Resultado = resultado
            };
        }

        private async Task EjecutarSqlAsync(Func<SqlConnection, Task> accion, CancellationToken ct = default) {
            if (string.IsNullOrWhiteSpace(_roleAplicacion) || string.IsNullOrWhiteSpace(_passwordRoleAplicacion)) {
                throw new InvalidOperationException("El AppRole de la aplicación no ha sido inicializado.");
            }
            await using var session = await _sql.OpenSessionAsync(
                new AppRoleConfig {
                    Nombre = _roleAplicacion,
                    Password = _passwordRoleAplicacion,
                    Habilitado = true
                }
            );
            await accion(session.Connection);
        }

    }
}

