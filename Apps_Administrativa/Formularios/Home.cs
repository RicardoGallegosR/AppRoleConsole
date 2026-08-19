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
using Apps_Administrativa.Paneles.FisicoMecanica;

namespace Apps_Administrativa.Formularios {
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
                $"Administrativa\n" +
                $"{_versionTexto}\n" +
                $"{DateTime.Now:HH:mm:ss}";
        }
        #endregion
        #region Apagar
        private async void msSalir_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(
                "¿Desea salir de la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes) {
                //await CerrarAplicacionAsync(apagarEquipo: true);
                Application.Exit();
            }

        }
        /*
        private async Task CerrarAplicacionAsync(bool apagarEquipo = false) {
            if (_cerrandoAplicacion) return;
            _cerrandoAplicacion = true;
            try {
                if (!_bitacoraFinalizada) {
                    await FinalizaBiatcoraAplicaciones(Visual_Core);
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
        //*/
        #endregion

        #region Paneles
        #region Fisico Mecanica
        private void msCapturaDePruebas_Click(object sender, EventArgs e) {
            splitContainer1.Panel2.Controls.Clear();
            var control = new CargarCertificados();
            control.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(control);
        }
        #endregion
        #region Cargar Home
        private void Home_Load(object sender, EventArgs e) {
            CargarHome();
        }
        private void CargarHome() {
            splitContainer1.Panel2.Controls.Clear();
            var control = new Paneles.HomeView();
            control.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(control);
        }
        #endregion
        #region Configuración de estación
        private void msConfiguraciónDeEstación_Click(object sender, EventArgs e) {

        }
        #endregion
        #endregion



    }
}
