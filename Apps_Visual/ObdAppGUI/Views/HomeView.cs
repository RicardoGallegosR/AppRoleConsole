using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class HomeView : Form {
        public int panelX = 0, panelY = 0;

        public HomeView() {
            InitializeComponent();
        }
        public Panel GetPanel() {
            ResetForm();
            return pnlHomeView;
        }
        private void ResetForm() {
            if (panelX == 0 && panelY == 0) {
                pnlHomeView.Size = new Size(Width, Height);
                pnlHomeView.Location = new Point((int)Math.Ceiling(.004 * Width), 0);
            } else {
                pnlHomeView.Size = new Size((int)Math.Ceiling(.98 * panelX), (int)Math.Ceiling(.95 * panelY));
                pnlHomeView.Location = new Point((int)Math.Ceiling(.004 * panelX), 0);
            }
        }
        private void HomeView_Load(object sender, EventArgs e) {
            
        }
    }
}
