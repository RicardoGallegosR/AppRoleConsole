using Apps_Vicente.Views;
using Apps_Vicente.Views.Certificados;
using FrmComun.Utils;
using Microsoft.Data.SqlClient;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SQLSIVEV.Infrastructure.Config.AppConfig;

namespace Apps_Vicente.Forms {
    public partial class fHome : Form {
        private System.Windows.Forms.Timer _timerHora;
        private string _versionTexto = "vDESCONOCIDA";
        private BarraLateral _barraLateral;
        private readonly Dictionary<string, Button> _botonesVistas = new();
        private readonly SivevConnectionFactory _sql;

        public fHome() {
            InitializeComponent();
            SqlConnectionStringBuilder csb = new() {
                DataSource = "192.168.16.8",
                InitialCatalog = "SIVEV",

                // Autenticación SQL
                UserID = "AreaTecnica",
                Password = "2019&AreaTecnica",
                IntegratedSecurity = false,

                // TLS
                Encrypt = false,
                TrustServerCertificate = true,
                PersistSecurityInfo = false,

                // Identificación de la aplicación en SQL Server
                ApplicationName = "Apps_Vicente",

                // Pooling
                Pooling = true,
                MinPoolSize = 0,
                MaxPoolSize = 100,
                ConnectTimeout = 15,
                LoadBalanceTimeout = 0,

                MultipleActiveResultSets = false
            };
            _sql = new SivevConnectionFactory(
                csb.ConnectionString
            );

            _barraLateral = new BarraLateral(
               flpVistasAbiertas,
               splitPrincipal.Panel2
           );
            /*
            _barraLateral.  CrearCabecera(() => {
                _barraLateral.MostrarVista("Home", "Home", () => new ucHome(), mostrarEnMenu: false);
            });
            */
            _barraLateral.CrearCabecera(
               "SMA", "Vicente", () => {
                   _barraLateral.MostrarVista("Home", "Home", () => new ucHome(), mostrarEnMenu: false);
               });

        }

        private void fHome_Load(object sender, EventArgs e) {
            CargarHome();
            ms.TabStop = true;
            ms.Focus();
            msReportes.Select();
            
        }
        private void CargarHome() {
            splitPrincipal.Panel2.Controls.Clear();
            var control = new ucHome();
            control.Dock = DockStyle.Fill;
            splitPrincipal.Panel2.Controls.Add(control);
        }

        private void msRemante_Click(object sender, EventArgs e) {
            _barraLateral.MostrarVista("Remanente", "Remanente", () => new  ucRemanente(sql: _sql));
        }

    }
}
