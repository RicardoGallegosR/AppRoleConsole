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
        private Size _formSizeInicial;
        private float _fontSizeInicial;
        public int panelX = 0, panelY = 0;

        public HomeView() {
            InitializeComponent();
            _fontSizeInicial = this.Font.Size;
            this.Resize += frmAuth_Resize;
        }
        public Panel GetPanel() {
            ResetForm();
            return pnlHomeView;
        }
        private void ResetForm() {
            this.Resize += frmAuth_Resize;
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

        #region Tamaño de la letra en AUTOMATICO
        private void frmAuth_Resize(object sender, EventArgs e) {
            float factor = (float)this.Width / _formSizeInicial.Width;
            ///*
            float Titulo1 = Math.Max(24f, Math.Min(_fontSizeInicial * factor, 60f));
            float Titulo2 = Math.Max(20f, Math.Min(_fontSizeInicial * factor, 50f));
            float Titulo3 = Math.Max(12f, Math.Min(_fontSizeInicial * factor, 30f));
            //*/

            lblSedemaTitulo.Font = new Font(
                lblSedemaTitulo.Font.FontFamily,
                Titulo1,
                lblSedemaTitulo.Font.Style
            );

            lblVerificacionVehicularHomeView.Font = new Font(
                lblVerificacionVehicularHomeView.Font.FontFamily,
                Titulo2,
                lblVerificacionVehicularHomeView.Font.Style
            );
            //*/

        }
        public void InicializarTamanoYFuente() {
            if (panelX > 0 && panelY > 0) {
                this.Size = new Size(panelX, panelY);
            }
            _formSizeInicial = this.Size;
            _fontSizeInicial = lblSedemaTitulo.Font.Size;
        }
        #endregion
    }
}
