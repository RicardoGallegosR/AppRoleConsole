using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLSIVEV.Infrastructure.Utils;

namespace Apps_Proveedores.Formularios {
    public partial class Home : Form {
        private System.Windows.Forms.Timer _timerHora;
        private string _versionTexto = "vDESCONOCIDA";
        private bool _cerrandoAplicacion = false;
        private bool _bitacoraFinalizada = false;

        public Home() {
            InitializeComponent();
            MostrarVersion();
        }
        #region Mostrar versión y hora
        private void MostrarVersion() {
            try {
                string exe = Application.ExecutablePath;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(exe);
                _versionTexto = $"v{info.FileVersion ?? Application.ProductVersion}";
            } catch (Exception ex) {
                _versionTexto = "vDESCONOCIDA";

                SivevLogger.Warning($"No se pudo obtener la versión del ejecutable: {ex.Message}");
            }
            // Evita crear varios timers si MostrarVersion se llama más de una vez.
            _timerHora?.Stop();
            _timerHora?.Dispose();
            _timerHora = new System.Windows.Forms.Timer { Interval = 1000 };
            _timerHora.Tick += TimerHora_Tick;

            // Muestra los datos inmediatamente, sin esperar el primer segundo.
            ActualizarVersionYHora();
            _timerHora.Start();
        }

        private void TimerHora_Tick(object? sender, EventArgs e) {
            ActualizarVersionYHora();
        }

        private void ActualizarVersionYHora() {
            lblVersion.Text =
                $"Proveedores\n" +
                $"{_versionTexto}\n" +
                $"{DateTime.Now:HH:mm:ss}";
        }
        #endregion

        #region Cargar Home
        private void Home_Load(object sender, EventArgs e) {
            CargarHome();
            ms.TabStop = true;
            ms.Focus();
            msHorario.Select();

        }
        private void CargarHome() {
            spC.Panel2.Controls.Clear();
            var control = new Paneles.HomeView();
            control.Dock = DockStyle.Fill;
            spC.Panel2.Controls.Add(control);
        }
        #endregion

        #region Horario
        private void msHorario_Click(object sender, EventArgs e) {
            spC.Panel2.Controls.Clear();
            var control = new Paneles.Horario.CambioHorario();
            control.Dock = DockStyle.Fill;
            spC.Panel2.Controls.Add(control);
        }
        #endregion

        private async void msApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(
                "¿Desea apagar la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
                await CerrarAplicacionAsync(apagarEquipo: true);
        }
        private async Task CerrarAplicacionAsync(bool apagarEquipo = false) {
            if (_cerrandoAplicacion) return;
            _cerrandoAplicacion = true;
            try {
                if (!_bitacoraFinalizada) {
                    //await FinalizaBiatcoraAplicaciones(Visual_Core);
                    await Task.Delay(100); // Simulación de finalización de bitácora
                    _bitacoraFinalizada = true;
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error al finalizar bitácora: {ex.Message}");
            }
            if (apagarEquipo)
                Process.Start("shutdown", "/s /t 0");
            else
                Application.Exit();
        }
        private async Task ReiniciarAplicacionAsync(bool apagarEquipo = false) {
            if (_cerrandoAplicacion) return;
            _cerrandoAplicacion = true;
            try {
                if (!_bitacoraFinalizada) {
                    //await FinalizaBiatcoraAplicaciones(Visual_Core);
                    await Task.Delay(100); // Simulación de finalización de bitácora
                    _bitacoraFinalizada = true;
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error al finalizar bitácora: {ex.Message}");
            }
            if (apagarEquipo)
                Process.Start("shutdown", "/r /t 0");
            else
                Application.Exit();
        }

        private async void msReiniciar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(
                "¿Desea reiniciar la aplicación?",
                "Confirmar reinicio",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
                await ReiniciarAplicacionAsync(apagarEquipo: true);
        }
    }
}
