namespace Apps_Visual.ObdAppGUI.Views {
    partial class HomeView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pnlHomeView = new Panel();
            scpDoble = new SplitContainer();
            pbxCarro = new PictureBox();
            lblVerificacionVehicularHomeView = new Label();
            lblSedemaTitulo = new Label();
            pnlHomeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scpDoble).BeginInit();
            scpDoble.Panel1.SuspendLayout();
            scpDoble.Panel2.SuspendLayout();
            scpDoble.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxCarro).BeginInit();
            SuspendLayout();
            // 
            // pnlHomeView
            // 
            pnlHomeView.BackColor = Color.White;
            pnlHomeView.Controls.Add(scpDoble);
            pnlHomeView.Controls.Add(lblSedemaTitulo);
            pnlHomeView.Location = new Point(0, 0);
            pnlHomeView.Name = "pnlHomeView";
            pnlHomeView.Size = new Size(900, 524);
            pnlHomeView.TabIndex = 0;
            // 
            // scpDoble
            // 
            scpDoble.Dock = DockStyle.Fill;
            scpDoble.Location = new Point(0, 128);
            scpDoble.Name = "scpDoble";
            // 
            // scpDoble.Panel1
            // 
            scpDoble.Panel1.Controls.Add(pbxCarro);
            // 
            // scpDoble.Panel2
            // 
            scpDoble.Panel2.Controls.Add(lblVerificacionVehicularHomeView);
            scpDoble.Size = new Size(900, 396);
            scpDoble.SplitterDistance = 536;
            scpDoble.TabIndex = 0;
            scpDoble.TabStop = false;
            // 
            // pbxCarro
            // 
            pbxCarro.Dock = DockStyle.Fill;
            pbxCarro.Image = Properties.Resources.coche_electrico;
            pbxCarro.Location = new Point(0, 0);
            pbxCarro.Name = "pbxCarro";
            pbxCarro.Size = new Size(536, 396);
            pbxCarro.SizeMode = PictureBoxSizeMode.CenterImage;
            pbxCarro.TabIndex = 1;
            pbxCarro.TabStop = false;
            // 
            // lblVerificacionVehicularHomeView
            // 
            lblVerificacionVehicularHomeView.Dock = DockStyle.Fill;
            lblVerificacionVehicularHomeView.Font = new Font("Segoe UI", 30F);
            lblVerificacionVehicularHomeView.ForeColor = Color.FromArgb(159, 34, 65);
            lblVerificacionVehicularHomeView.Location = new Point(0, 0);
            lblVerificacionVehicularHomeView.Name = "lblVerificacionVehicularHomeView";
            lblVerificacionVehicularHomeView.Size = new Size(360, 396);
            lblVerificacionVehicularHomeView.TabIndex = 0;
            lblVerificacionVehicularHomeView.Text = "VERIFICACIÓN\r\nVEHICULAR\r\n";
            lblVerificacionVehicularHomeView.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSedemaTitulo
            // 
            lblSedemaTitulo.Dock = DockStyle.Top;
            lblSedemaTitulo.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSedemaTitulo.ForeColor = Color.FromArgb(159, 34, 65);
            lblSedemaTitulo.Location = new Point(0, 0);
            lblSedemaTitulo.Name = "lblSedemaTitulo";
            lblSedemaTitulo.Size = new Size(900, 128);
            lblSedemaTitulo.TabIndex = 0;
            lblSedemaTitulo.Text = "SEDEMA";
            lblSedemaTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HomeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 524);
            Controls.Add(pnlHomeView);
            Name = "HomeView";
            Text = "HomeView";
            Load += HomeView_Load;
            pnlHomeView.ResumeLayout(false);
            scpDoble.Panel1.ResumeLayout(false);
            scpDoble.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scpDoble).EndInit();
            scpDoble.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxCarro).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHomeView;
        private Label lblSedemaTitulo;
        private Label lblVerificacionVehicularHomeView;
        private PictureBox pbxCarro;
        private SplitContainer scpDoble;
    }
}