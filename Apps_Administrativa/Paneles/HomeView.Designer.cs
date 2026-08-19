namespace Apps_Administrativa.Paneles {
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
            pnlPrincipal = new Panel();
            splitContainer1 = new SplitContainer();
            lblTitulo = new Label();
            splitContainer2 = new SplitContainer();
            pbxAdministrativa = new PictureBox();
            lblVerificacionVehicularHomeView = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxAdministrativa).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(834, 521);
            pnlPrincipal.TabIndex = 0;
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
            splitContainer1.Size = new Size(834, 521);
            splitContainer1.SplitterDistance = 102;
            splitContainer1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Crimson;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(834, 102);
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
            splitContainer2.Size = new Size(834, 415);
            splitContainer2.SplitterDistance = 419;
            splitContainer2.TabIndex = 0;
            // 
            // pbxAdministrativa
            // 
            pbxAdministrativa.Dock = DockStyle.Fill;
            pbxAdministrativa.Image = (Image)resources.GetObject("pbxAdministrativa.Image");
            pbxAdministrativa.Location = new Point(0, 0);
            pbxAdministrativa.Name = "pbxAdministrativa";
            pbxAdministrativa.Size = new Size(419, 415);
            pbxAdministrativa.SizeMode = PictureBoxSizeMode.AutoSize;
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
            lblVerificacionVehicularHomeView.Size = new Size(411, 415);
            lblVerificacionVehicularHomeView.TabIndex = 1;
            lblVerificacionVehicularHomeView.Text = "VERIFICACIÓN VEHICULAR\r\n";
            lblVerificacionVehicularHomeView.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HomeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "HomeView";
            Size = new Size(834, 521);
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxAdministrativa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label lblTitulo;
        private Label lblVerificacionVehicularHomeView;
        private PictureBox pbxAdministrativa;
    }
}
