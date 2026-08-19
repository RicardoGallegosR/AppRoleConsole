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

namespace Apps_Captura.Frm {
    public partial class Home : Form {
        private System.Windows.Forms.Timer _timerHora;
        private string _versionTexto = "vDESCONOCIDA";
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
    }
}

