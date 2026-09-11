using Microsoft.Data.SqlClient;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Sql.Configuracion {
    public class RepositorioVerificaciones {
        public async Task<List<ResumenCertificadosCVEV>> ObtenerResumenCertificadosAsync(SqlConnection conn, IEnumerable<EmaPorCVEV> centros, int vigenciaId = 69, IProgress<ProgresoConsulta>? progreso = null, CancellationToken ct = default) {
            ArgumentNullException.ThrowIfNull(conn);
            ArgumentNullException.ThrowIfNull(centros);

            if (conn.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta.");

            var resultados = new List<ResumenCertificadosCVEV>();

            var centrosValidos = centros
                                        .Where(x => x.VerificentroId is >= 9100 and <= 9999)
                                        .GroupBy(x => x.VerificentroId)
                                        .Select(x => x.First())
                                        .OrderBy(x => x.VerificentroId)
                                        .ToList();

            int numeroCentro = 0;

            foreach (var centro in centrosValidos) {
                ct.ThrowIfCancellationRequested();

                int verificentroId = centro.VerificentroId;
                string servidorVfc = $"SIVSRV{verificentroId}";

                string query = $"""
                    SELECT
                        VV.NombreDominio,
                        SUM(CASE WHEN C.TipoId = 1 THEN 1 ELSE 0 END) AS RECHAZO,
                        SUM(CASE WHEN C.TipoId = 2 THEN 1 ELSE 0 END) AS DOS,
                        SUM(CASE WHEN C.TipoId = 3 THEN 1 ELSE 0 END) AS UNO,
                        SUM(CASE WHEN C.TipoId = 4 THEN 1 ELSE 0 END) AS CERO,
                        SUM(CASE WHEN C.TipoId = 5 THEN 1 ELSE 0 END) AS DB_CERO,
                        COUNT(*) AS Total
                    FROM [{servidorVfc}].[SIVEV].[Certificados].[Certificados]
                        AS C WITH (READUNCOMMITTED)
                    INNER JOIN
                        [{servidorVfc}].[SIVEV].[Verificentros].[Verificentros]
                        AS VV WITH (READUNCOMMITTED)
                            ON C.VerificentroId = VV.VerificentroId
                    WHERE C.EstatusId IN (8, 2, 6)
                      AND C.VigenciaId = @VigenciaId
                    GROUP BY VV.NombreDominio;
                 """;

                try {
                    using var cmd = conn.CreateCommand();

                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters.Add("@VerificentroId", SqlDbType.Int).Value = verificentroId;
                    cmd.Parameters.Add("@VigenciaId", SqlDbType.Int).Value = vigenciaId;

                    bool encontroDatos = false;

                    await using (var reader =
                        await cmd.ExecuteReaderAsync(ct)) {
                        while (await reader.ReadAsync(ct)) {
                            encontroDatos = true;

                            resultados.Add(new ResumenCertificadosCVEV {
                                //VerificentroId = verificentroId,
                                NombreDominio = reader["NombreDominio"] == DBNull.Value ? centro.Verificentro : Convert.ToString(reader["NombreDominio"]) ?? centro.Verificentro,
                                Rechazo = ObtenerInt(reader, "RECHAZO"),
                                Dos = ObtenerInt(reader, "DOS"),
                                Uno = ObtenerInt(reader, "UNO"),
                                Cero = ObtenerInt(reader, "CERO"),
                                DbCero = ObtenerInt(reader, "DB_CERO"),
                                Total = ObtenerInt(reader, "Total")
                            });
                        }
                    }

                    if (!encontroDatos) {
                        resultados.Add(new ResumenCertificadosCVEV {
                            //VerificentroId = verificentroId,
                            NombreDominio = string.IsNullOrWhiteSpace(centro.Verificentro) ? "DESCONOCIDO" : centro.Verificentro
                        });
                    }
                } catch (OperationCanceledException) {
                    throw;
                } catch (SqlException ex) {
                    SivevLogger.Error($"Error consultando {servidorVfc}. " + $"Número={ex.Number}, Mensaje={ex.Message}", SivevOrigen.Vicente);
                    resultados.Add(new ResumenCertificadosCVEV {
                        //VerificentroId = verificentroId,
                        NombreDominio = $"{centro.Verificentro} - ERROR"
                    });
                } finally {
                    numeroCentro++;
                    progreso?.Report(new ProgresoConsulta {
                        Actual = numeroCentro,
                        Total = centrosValidos.Count,
                        VerificentroId = verificentroId
                    });
                }
                await Task.Delay(250, ct);
            }
            return resultados;
        }
        private static int ObtenerInt(SqlDataReader reader, string columna) {
            int ordinal = reader.GetOrdinal(columna);
            return reader.IsDBNull(ordinal) ? 0 : Convert.ToInt32(reader.GetValue(ordinal));
        }


        public async Task<List<ReporteEma>> GenerarReporteEmaAsync(SqlConnection conn, EmaPorCVEV formControl, CancellationToken ct = default) {
            var lista = new List<ReporteEma>();
            const string query = @"

                SELECT DISTINCT
                    VV.NombreDominio AS Verificentro,
                    V.CertificadoId AS Folio,
                    CASE V.CertificadoTipoId 
                        WHEN 1 THEN 'RECHAZO'
                        WHEN 2 THEN 'DOS'
                        WHEN 3 THEN 'UNO'
                        WHEN 4 THEN 'CERO'
                        WHEN 5 THEN 'DBLCERO'
                        ELSE 'DESCONOCIDO'
                    END AS Holograma,
                    M.Marca,
                    V.PlacaId AS Placa,
                    CASE 
                        WHEN V.CombustibleId = 2 THEN 'NOM-045-SEMARNAT'
                        WHEN V.CombustibleId <> 2 
                             AND V.CausaRechazoId <> 5 
                             AND V.FechaPruebaInicial = V.FechaPruebaFinal 
                            THEN 'NOM-167-SEMARNAT'
                        ELSE 'NOM-047-SEMARNAT'
                    END AS Norma,
                    CONVERT(VARCHAR(10), V.FechaCapturaInicial, 111) AS FechaCaptura,
                    CONVERT(VARCHAR(8), V.FechaCapturaInicial, 108) AS HoraCaptura,
                    CASE WHEN (N.Nombre + ' ' + Paterno.Nombre + ' ' + Materno.Nombre) = 'DESCONOCIDO DESCONOCIDO DESCONOCIDO' THEN 'No Aplica' ELSE (N.Nombre + ' ' + Paterno.Nombre + ' ' + Materno.Nombre) END AS Tecnico
                FROM Sivev.Verificaciones.Verificaciones2024 V WITH (READUNCOMMITTED)
                INNER JOIN SIVEV.Equipos.Estaciones2024 E WITH (READUNCOMMITTED) ON V.EstacionCapturaId = E.EstacionId
                INNER JOIN Sivev.Vehiculos.Vehiculos VE WITH (READUNCOMMITTED) ON V.VehiculoId = VE.VehiculoId AND VE.Activo = 1
                INNER JOIN SIVEV.Verificentros.Verificentros VV WITH (READUNCOMMITTED) ON E.VerificentroId = VV.VerificentroId
                INNER JOIN Sivev.Vehiculos.ModelosVehiculares2024 MV WITH (READUNCOMMITTED) ON VE.ModeloVehicularId = MV.ModeloVehicularId
                INNER JOIN Sivev.Catalogos.Submarcas2024 SM WITH (READUNCOMMITTED)  ON MV.SubmarcaId = SM.SubmarcaId
                INNER JOIN Sivev.Catalogos.Marcas2024 M WITH (READUNCOMMITTED) ON SM.MarcaId = M.MarcaId
                OUTER APPLY (
                    SELECT TOP (1) BA.PersonalId
                    FROM Sivev.Bitacoras.Accesos BA WITH (READUNCOMMITTED)
                    WHERE BA.EstacionId = V.EstacionPruebaOBDId
                      AND BA.FechaInicial >= V.FechaCapturaInicial
                      AND BA.FechaInicial <= V.FechaImpresionFinal
                    ORDER BY BA.FechaInicial ASC
                ) BA1
                LEFT JOIN Sivev.RecursosHumanos.PersonalNew PER WITH (READUNCOMMITTED) ON BA1.PersonalId = PER.PersonalId
                LEFT JOIN Catalogos.EmpresasPersonas EP WITH (READUNCOMMITTED) ON PER.EmpresaPersonaId = EP.EmpresaPersonaId
                LEFT JOIN Catalogos.Nombres N WITH (READUNCOMMITTED) ON EP.NombreId = N.NombreId
                LEFT JOIN Catalogos.Nombres Paterno WITH (READUNCOMMITTED) ON EP.PaternoId = Paterno.NombreId
                LEFT JOIN Catalogos.Nombres Materno WITH (READUNCOMMITTED) ON EP.MaternoId = Materno.NombreId
                WHERE V.ResultadoId IN (3, 4)
                  AND V.CertificadoId > 1000000
                  AND V.FechaCapturaInicial >= @FechaInicio
                  AND V.FechaCapturaInicial < @FechaFin
                  AND (@Centro = 0 OR E.VerificentroId = @Centro)
                ORDER BY Folio;";

            using var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 180;

            cmd.Parameters.Add("@Centro", SqlDbType.Int).Value = formControl.VerificentroId;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = new DateTime(formControl.Fecha.Year, formControl.Fecha.Month, 1);
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = new DateTime(formControl.Fecha.Year, formControl.Fecha.Month, 1).AddMonths(1);

            using var reader = await cmd.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct)) {
                lista.Add(new ReporteEma {
                    Verificentro = reader["Verificentro"]?.ToString() ?? "",
                    folio = reader["Folio"] != DBNull.Value ? Convert.ToInt32(reader["Folio"]) : 0,
                    Holograma = reader["Holograma"]?.ToString() ?? "",
                    Marca = reader["Marca"]?.ToString() ?? "",
                    Placa = reader["Placa"]?.ToString() ?? "",
                    Norma = reader["Norma"]?.ToString() ?? "",
                    fechaCaptura = reader["FechaCaptura"]?.ToString() ?? "",
                    horaCaptura = reader["HoraCaptura"]?.ToString() ?? "",
                    Tecnico = reader["Tecnico"]?.ToString() ?? ""
                });
            }
            return lista;
        }

        public async Task<List<EmaPorCVEV>> ObtenerVerificentrosEmaAsync(SqlConnection conn, EmaPorCVEV formControl, CancellationToken ct = default) {
            var lista = new List<EmaPorCVEV>();
            const string query = @"
                DECLARE @FechaInicio DATETIME =
                    CAST( CAST(@Year AS VARCHAR(4)) + '-' + RIGHT('00' + CAST(@Month AS VARCHAR(2)), 2) + '-01' AS DATETIME);
                DECLARE @FechaFin DATETIME = DATEADD(MONTH, 1, @FechaInicio);
                SELECT DISTINCT 
                    E.VerificentroId,
                    VV.NombreDominio AS Verificentro
                FROM Sivev.Verificaciones.Verificaciones2024 V WITH (READUNCOMMITTED)
                    JOIN SIVEV.Equipos.Estaciones2024 E WITH (READUNCOMMITTED) ON V.EstacionCapturaId = E.EstacionId
                    JOIN SIVEV.Verificentros.Verificentros VV WITH (READUNCOMMITTED) ON E.VerificentroId = VV.VerificentroId
                WHERE V.FechaCapturaInicial >= @FechaInicio
                  AND V.FechaCapturaInicial < @FechaFin
                  AND E.VerificentroId BETWEEN 9100 AND 9999
                ORDER BY E.VerificentroId;";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = formControl.Fecha.Year;
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = formControl.Fecha.Month;
            using var reader = await cmd.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct)) {
                lista.Add(new EmaPorCVEV {
                    Fecha = formControl.Fecha,
                    VerificentroId = Convert.ToInt32(reader["VerificentroId"]),
                    Verificentro = reader["Verificentro"]?.ToString() ?? ""
                });
            }
            return lista;
        }

        public async Task<int> ObtenerViegenciaCertificadosAsync(SqlConnection conn, CancellationToken ct = default) {
            const string query = @"
                SELECT VigenciaId
                From Sivev.Certificados.Vigencias
                WHERE GETDATE() between FechaInicial AND FechaFinal;";
            using var cmd = new SqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync(ct);
            int VigenciaId = 0;
            if (await reader.ReadAsync(ct)) {
                VigenciaId = Convert.ToInt32(reader["VigenciaId"]);
            }
            return VigenciaId;
        }

    }
}
