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
        public Home() {
            InitializeComponent();
            MostrarVersion();
        }
        private void MostrarVersion() {
            _timerHora = new System.Windows.Forms.Timer();
            _timerHora.Interval = 1000; // 1 segundo
            try {
                var exe = Process.GetCurrentProcess().MainModule.FileName;
                var info = FileVersionInfo.GetVersionInfo(exe);
                lblVersion.Text = $"v{info.FileVersion}\n────────\n{DateTime.Now:HH:mm:ss}";
            } catch (Exception ex) {
                _timerHora.Tick += (s, ev) => {
                    var exe = Process.GetCurrentProcess().MainModule?.FileName;
                    var info = FileVersionInfo.GetVersionInfo(exe);

                    lblVersion.Text =
                        $"v{info.FileVersion}\n" +
                        $"────────\n" +
                        //$"{Visual_Core.dvar10}\n" +
                        $"{DateTime.Now:HH:mm:ss}";
                };

                _timerHora.Start();
                SivevLogger.Warning($"No se pudo obtener la versión del ejecutable: {ex.Message}");
            }
        }
    }
}

