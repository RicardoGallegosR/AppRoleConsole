using Apps_Regedit.Views;
using Apps_Regedit.Views.Verificentros;
using FrmComun.Utils;
using SQLSIVEV.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Regedit.Formularios {
    public partial class frmHome : Form {
        private BarraLateral _barraLateral;
        public frmHome() {
            InitializeComponent();
            _barraLateral = new BarraLateral(
                flpVistasAbiertas,
                splitPrincipal.Panel2
            );
            _barraLateral.CrearCabecera(
                "SMA", "Regedit", () => {
               _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
           });
            /*
            _barraLateral.CrearCabecera(() => {
                _barraLateral.MostrarVista("Home", "Home", () => new Views.Home(), mostrarEnMenu: false);
            });
            */
            _barraLateral.MostrarVista("Home", "Home", () => new Views.Home ());
        }
        #region Verificentros
        private async void msCAPTURA_Click(object sender, EventArgs e) {
            _barraLateral.MostrarVista("Captura", "Captura", () => new Captura());
        }

        private async void msVISUAL_Click(object sender, EventArgs e) {
            _barraLateral.MostrarVista("Visual", "Visual", () => new Visual());
        }
        #endregion


    }
}
