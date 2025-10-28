using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Apps_Visual.ObdAppGUI.Views;




namespace Apps_Visual.ObdAppGUI {

    public partial class frmBASE : Form {
        private HomeView home;


        public frmBASE() {
            InitializeComponent();
            pnlHome();
        }

       
        private void pnlHome () {
            foreach (Control c in pnlPanelCambios.Controls)
                c.Dispose();
            pnlPanelCambios.Controls.Clear();
            if (home == null || home.IsDisposed) {
                home = new HomeView();
            }
            home.panelX = pnlPanelCambios.Width;
            home.panelY = pnlPanelCambios.Height;
            pnlPanelCambios.Controls.Add(home.GetPanel());
            pnlPanelCambios.Dock = DockStyle.Fill;
        }



        private void LoginForm_Load(object sender, EventArgs e) {

        }

        private void btnInspecionVisual_Click(object sender, EventArgs e) {

        }

        private void btnApagar_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("¿Desea apagar la aplicación?","Confirmar salida", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes) {
                Application.Exit();  
                //Process.Start("shutdown", "/s /t 0");
            }
        }

       

    }
}
