namespace Apps_Proveedores.Paneles {
    partial class HomeView {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeView));
            splitContainer1 = new SplitContainer();
            lblTitulo = new Label();
            splitContainer2 = new SplitContainer();
            pbxAdministrativa = new PictureBox();
            lblVerificacionVehicularHomeView = new Label();
            pnlPrincipal = new Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxAdministrativa).BeginInit();
            pnlPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lblTitulo);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(657, 434);
            splitContainer1.SplitterDistance = 84;
            splitContainer1.TabIndex = 0;
            splitContainer1.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Crimson;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(657, 84);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "S E D E M A ";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(pbxAdministrativa);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(lblVerificacionVehicularHomeView);
            splitContainer2.Size = new Size(657, 346);
            splitContainer2.SplitterDistance = 330;
            splitContainer2.TabIndex = 0;
            splitContainer2.TabStop = false;
            // 
            // pbxAdministrativa
            // 
            pbxAdministrativa.Dock = DockStyle.Fill;
            pbxAdministrativa.Image = (Image)resources.GetObject("pbxAdministrativa.Image");
            pbxAdministrativa.Location = new Point(0, 0);
            pbxAdministrativa.Name = "pbxAdministrativa";
            pbxAdministrativa.Size = new Size(330, 346);
            pbxAdministrativa.SizeMode = PictureBoxSizeMode.CenterImage;
            pbxAdministrativa.TabIndex = 0;
            pbxAdministrativa.TabStop = false;
            // 
            // lblVerificacionVehicularHomeView
            // 
            lblVerificacionVehicularHomeView.Dock = DockStyle.Fill;
            lblVerificacionVehicularHomeView.Font = new Font("Segoe UI", 30F);
            lblVerificacionVehicularHomeView.ForeColor = Color.Crimson;
            lblVerificacionVehicularHomeView.Location = new Point(0, 0);
            lblVerificacionVehicularHomeView.Name = "lblVerificacionVehicularHomeView";
            lblVerificacionVehicularHomeView.Size = new Size(323, 346);
            lblVerificacionVehicularHomeView.TabIndex = 1;
            lblVerificacionVehicularHomeView.Text = "Provedores";
            lblVerificacionVehicularHomeView.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(657, 434);
            pnlPrincipal.TabIndex = 1;
            // 
            // HomeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "HomeView";
            Size = new Size(657, 434);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxAdministrativa).EndInit();
            pnlPrincipal.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label lblTitulo;
        private SplitContainer splitContainer2;
        private PictureBox pbxAdministrativa;
        private Label lblVerificacionVehicularHomeView;
        private Panel pnlPrincipal;
    }
}
