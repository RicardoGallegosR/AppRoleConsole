using FrmComun.Utils;
using Microsoft.Data.SqlClient;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Sql.Configuracion;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SQLSIVEV.Infrastructure.Utils.Excel;

namespace Apps_Vicente.Views.Certificados {
    public partial class ucRemanente : UserControl {
        private readonly SivevConnectionFactory _sql;
        private const string AppRoleName  = "RollSmaHologramas2026";
        private const string AppRolePassword  = "2603F568-5274-4BCD-B5C5-0EA5883F156A";
        private List<EmaPorCVEV> _dataVerificentro = new();
        private string _ultimaRutaGenerada = string.Empty;
        private int _segundosRestantes = 90;
        private System.Windows.Forms.Timer _timerProgreso;

        public ucRemanente(SivevConnectionFactory sql) {
            InitializeComponent();
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            Activador();
            //OrdenarPaneles();
            BuscarVerificentro();
            BuscarVigencia();
            txbVigencia.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(tb: txbVigencia, invalidPattern: @"[^0-9]");
            txbVigencia.MaxLength = 2;
            flowLayoutPanel1.Resize += (s, e) => AjustarControlesPanelDerecho();
            AjustarControlesPanelDerecho();
            txbVigencia.Enabled = false;
        }
        #region Buscar Verificentros
        private async void BuscarVerificentro(CancellationToken ct = default) {
            Activador();
            try {
                await using SqlConnection conn =  await _sql.OpenAsync();
                await using var session = await _sql.OpenSessionAsync(
                    new AppRoleConfig {
                        Nombre = AppRoleName,
                        Password = AppRolePassword,
                        Habilitado = true
                    }
                );
                var repo = new RepositorioVerificaciones();
                var filtro = new EmaPorCVEV { Fecha = DateTime.Now };
                _dataVerificentro = await repo.ObtenerVerificentrosEmaAsync(conn, filtro);
                _dataVerificentro.Insert(0, new EmaPorCVEV {
                    VerificentroId = 0,
                    Verificentro = "Todos"
                });
                cbVerificentro.DataSource = null;
                cbVerificentro.DisplayMember = nameof(EmaPorCVEV.Descripcion);
                cbVerificentro.DataSource = _dataVerificentro;
                ActualizarTituloReporte();

            } catch (Exception ex) {
                Mostrar.Mensaje("Error en BuscarResultados", ex.Message);
                SivevLogger.Error($"Error en BuscarResultados: {ex.Message}", SivevOrigen.Vicente);
            }
            Activador(true);
        }
        //*/
        #endregion
        private void ActualizarTituloReporte() {
            lblReporteAGenerar.Text = $"Remanente de los centros\nJefe esto va tomar entre uno y dos minutos por reporte :(";
        }
        #region Inicializadores y Activadores
        private void Activador(bool flag = false) {
            cbVerificentro.Enabled = flag;
            cbVerificentro.Visible = flag;
            lblVerificentro.Visible = flag;
            pnlHeader.Visible = flag;
            splitContainer1.Panel1.Enabled = flag;
            splitContainer1.Panel1.Visible = flag;

            splitContainer1.Panel2.Enabled = flag;  
            splitContainer1.Panel2.Visible = true;

            flowLayoutPanel1.Enabled = flag;
            flowLayoutPanel1.Visible = true;
        }
        private void AjustarControlesPanelDerecho() {

            int ancho =
        flowLayoutPanel1.ClientSize.Width
        - flowLayoutPanel1.Padding.Horizontal;

            foreach (Control control in flowLayoutPanel1.Controls) {

                control.Width =
                    ancho - control.Margin.Horizontal;
            }
        }
        #endregion
        #region Buscar Vigencia private
        private async void BuscarVigencia(CancellationToken ct = default) {
            try {
                await using SqlConnection conn =  await _sql.OpenAsync();
                await using var session = await _sql.OpenSessionAsync(
                    new AppRoleConfig {
                        Nombre = AppRoleName,
                        Password = AppRolePassword,
                        Habilitado = true
                    }
                );
                var repo = new RepositorioVerificaciones();
                var filtro = new EmaPorCVEV {
                    Fecha = DateTime.Now
                };
                var Vigencia = await repo.ObtenerViegenciaCertificadosAsync(conn, ct);
                txbVigencia.Text = Vigencia.ToString();
            } catch (Exception ex) {
                Mostrar.Mensaje("Error en buscar vigencia", ex.Message);
                SivevLogger.Error($"Error en buscar vigencia: {ex}", SivevOrigen.Vicente);
            }
        }
        #endregion
        private async Task ExportarReporte(CancellationToken ct = default) {
            ActivadorResultados();
            try {
                await using SqlConnection conn =  await _sql.OpenAsync();
                await using var session = await _sql.OpenSessionAsync(
                    new AppRoleConfig {
                        Nombre = AppRoleName,
                        Password = AppRolePassword,
                        Habilitado = true
                    }
                );
                var repo = new RepositorioVerificaciones();
                var filtro = new EmaPorCVEV {
                    Fecha = DateTime.Now
                };
                var centros = await repo.ObtenerVerificentrosEmaAsync(conn,filtro,ct);

                var centrosValidos = centros
                                            .Where(x => x.VerificentroId is >= 9100 and <= 9999)
                                            .GroupBy(x => x.VerificentroId)
                                            .Select(x => x.First())
                                            .OrderBy(x => x.VerificentroId)
                                            .ToList();

                List<EmaPorCVEV> centrosAConsultar;
                if (cbVerificentro.SelectedItem is not EmaPorCVEV seleccionado) {
                    Mostrar.Mensaje("Verificentro", "Selecciona un verificentro válido.");
                    return;
                }
                if (seleccionado.VerificentroId == 0) {
                    // Opción "Todos"
                    centrosAConsultar = await repo.ObtenerVerificentrosEmaAsync(conn, filtro, ct);
                } else {
                    // Solo el verificentro seleccionado en cbVerificentro
                    centrosAConsultar = new List<EmaPorCVEV> {
                        seleccionado
                    };
                }

                if (centrosAConsultar.Count == 0) {
                    DetenerProgreso("No se encontraron verificentros para consultar.");
                    return;
                }
                IniciarProgreso(centrosAConsultar.Count);
                var progreso = new Progress<ProgresoConsulta>(p => {
                    ActualizarProgreso(p.Actual, p.Total, p.VerificentroId );
                });

                var resultados = await repo.ObtenerResumenCertificadosAsync(conn, centrosAConsultar, vigenciaId:Convert.ToInt32(txbVigencia.Text), progreso: progreso, ct: ct);
                DetenerProgreso($"Consulta terminada. " + $"Se procesaron {centrosAConsultar.Count} verificentros.");
                DataTable dt = ToDataTable(resultados);
                _ultimaRutaGenerada = Exportar.ReporteRemanente(@"C:\Remanente\", filtro, dt);
                llblAbrirArchivo.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            } catch (OperationCanceledException) {
                lblPrgreso.Text = "Consulta cancelada.";
                SivevLogger.Warning("Consulta cancelada por el usuario.", SivevOrigen.Vicente);
            } catch (Exception ex) {
                Mostrar.Mensaje("Error en ExportarReporte", ex.Message);
                SivevLogger.Error($"Error en ExportarReporte: {ex}", SivevOrigen.Vicente);
            } finally {
                ActivadorResultados(true);
            }
        }
        public static DataTable ToDataTable<T>(List<T> items) {
            var dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();
            foreach (var prop in props) {
                Type tipo = Nullable.GetUnderlyingType(prop.PropertyType)
                     ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, tipo);
            }
            foreach (var item in items) {
                var values = props.Select(p => p.GetValue(item) ?? DBNull.Value).ToArray();
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        private void ActivadorResultados(bool flag = false) {
            if (!flag) {
                btnGenerarReporte.Text = "Generando...";
            } else {
                btnGenerarReporte.Text = "Generar Reporte nuevo";
            }
            /*
            llblAbrirArchivo.Visible = flag;
            btnGenerarReporte.Visible = flag;
            cbVerificentro.Enabled = flag;
            */

            llblAbrirArchivo.Enabled = flag;
            btnGenerarReporte.Enabled = flag;
            cbVerificentro.Enabled = flag;

        }
        private void IniciarProgreso(int totalCentros) {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = Math.Max(totalCentros, 1);
            progressBar1.Value = 0;

            lblPrgreso.Text =
                $"Preparando consulta de {totalCentros} verificentros...";
        }

        private void ActualizarProgreso(int actual, int total, int verificentroId) {
            if (InvokeRequired) {
                BeginInvoke(() => ActualizarProgreso(actual, total, verificentroId));
                return;
            }
            int valorSeguro = Math.Clamp(actual, progressBar1.Minimum, progressBar1.Maximum);
            progressBar1.Value = valorSeguro;
            int porcentaje = total > 0 ? (int)Math.Round(actual * 100.0 / total) : 0;
            lblPrgreso.Text = $"Consultando verificentro {verificentroId}... " + $"{actual} de {total} ({porcentaje}%)";
        }

        private void DetenerProgreso(string mensajeFinal) {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = progressBar1.Maximum;

            lblPrgreso.Text = mensajeFinal;
        }

        private void llblAbrirArchivo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (string.IsNullOrWhiteSpace(_ultimaRutaGenerada))
                return;
            string carpeta = Path.GetDirectoryName(_ultimaRutaGenerada);
            if (Directory.Exists(carpeta)) {
                Process.Start("explorer.exe", carpeta);
            }
        }

        private async void btnGenerarReporte_Click(object sender, EventArgs e) {
            await ExportarReporte();
        }
    }
}
