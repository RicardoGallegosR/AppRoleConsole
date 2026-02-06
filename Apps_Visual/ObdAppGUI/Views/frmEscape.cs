using SQLSIVEV.Domain.Models;
//using DPFP_SMA.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLSIVEV.Infrastructure.Utils;
using SQLSIVEV.Infrastructure.Security;

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmEscape : Form {
        public VisualRegistroWindows _Visual;
        private bool _Cerrar = true;
        private TaskCompletionSource<bool>? _tcsResultado;

        public frmEscape(VisualRegistroWindows visual) {
            _Visual = visual ?? throw new ArgumentNullException(nameof(visual));
            InitializeComponent();
        }

        private void btnNo_Click(object sender, EventArgs e) => Close();
        
        public bool Cerrar {
            get => _Cerrar;
            set {
                _Cerrar = value;
                btnNo.Enabled = _Cerrar;
            }
        }
        public Task<bool> EsperarResultadoAsync() {
            _tcsResultado = new TaskCompletionSource<bool>();
            return _tcsResultado.Task;
        }

        private async void btnSi_Click(object sender, EventArgs e) {
            int MensajeId = 0, Resultado = 0;
            var repo = new SivevRepository ();

            try {
                var r = await repo.SpAppCapturaAbandonaAsync(V: _Visual);
                MensajeId = r.MensajeId;
                Resultado = r.Resultado;
                //MostrarMensaje($"Respuesta:{MensajeId}");
                if (MensajeId != 0) {
                    try {
                        using var connApp = SqlConnectionFactory.Create( server: _Visual.Server, db: _Visual.Database, user: _Visual.User, pass: _Visual.Password, appName: _Visual.AppName);
                        await connApp.OpenAsync();
                        using (var scope = new AppRoleScope(connApp, role: _Visual.RollVisual, password: _Visual.RollVisualAcceso.ToString().ToUpper())) {
                            var error = await repo.PrintIfMsgAsync(connApp, $"btnConectar_Click", MensajeId);
                            var bitacora = NuevaBitacora( _Visual, descripcion: $"Resultado de OBD: {error.Mensaje}", codigoSql: MensajeId, codigo: 0);
                            await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                            MostrarMensaje($"Resultado de OBD: {error.Mensaje}");
                            await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId: _Visual.EstacionId, _AccesoId: _Visual.AccesoId);
                        }
                        _tcsResultado?.TrySetResult(true);
                    } catch (Exception ex) {
                        try {
                            var bitacora = NuevaBitacora( _Visual, descripcion: ex.ToString(), codigoSql: 0, codigo: ex.HResult);
                            await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                        } catch (Exception logEx) {
                            SivevLogger.Error($"Falló en OBD en catch de placa {_Visual.PlacaId}, GetAccesoSQL: {logEx.Message}");
                        }
                        MostrarMensaje($"Falló en OBD en catch de placa {_Visual.PlacaId}: {ex.Message}");
                    }

                }

            } catch (Exception ex) {
                try {
                    var bitacora = NuevaBitacora( _Visual, descripcion: e.ToString(), codigoSql: 0, codigo: ex.HResult);
                    await repo.SpSpAppBitacoraErroresSetAsync(_Visual, bitacora);
                } catch (Exception logEx) {
                    SivevLogger.Warning($"Falló la búsqueda de verificaciones en catch, GetAccesoSQLVerificaciones: {logEx.Message}");
                }
                MostrarMensaje($"{ex.Message}");
                SivevLogger.Error($"Error en btnSi_Click.-{ex.Message}");
            }
            Close();
        }


        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }

        private SpAppBitacoraErroresSet NuevaBitacora(VisualRegistroWindows V, string descripcion, int codigoSql = 0, int codigo = 0, [CallerMemberName] string callerMember = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0) {
            return new SpAppBitacoraErroresSet {
                EstacionId = V.EstacionId,
                Centro = V.Centro,
                NombreCpu = Environment.MachineName,
                OpcionMenuId = V.OpcionMenuId,
                FechaError = DateTime.Now,
                Libreria = Path.GetFileName(callerFile),
                Clase = Path.GetFileNameWithoutExtension(callerFile),
                Metodo = callerMember,
                CodigoErrorSql = codigoSql,
                CodigoError = codigo,
                DescripcionError = descripcion,
                LineaCodigo = callerLine,
                LastDllError = 0,
                SourceError = "DESCONOCIDO"
            };
        }

    }
}
