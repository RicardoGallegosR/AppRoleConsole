namespace Apps_Captura.Frm {
    partial class Home {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            pnlPrincipal = new Panel();
            splitPrincipal = new SplitContainer();
            pnlInfo = new Panel();
            flpVistasAbiertas = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            msCaptura = new ToolStripMenuItem();
            msPassword = new ToolStripMenuItem();
            msHuella = new ToolStripMenuItem();
            msMeteorologica = new ToolStripMenuItem();
            msApagar = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.SuspendLayout();
            pnlInfo.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitPrincipal);
            pnlPrincipal.Controls.Add(menuStrip1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitPrincipal
            // 
            splitPrincipal.Dock = DockStyle.Fill;
            splitPrincipal.IsSplitterFixed = true;
            splitPrincipal.Location = new Point(0, 33);
            splitPrincipal.Name = "splitPrincipal";
            // 
            // splitPrincipal.Panel1
            // 
            splitPrincipal.Panel1.Controls.Add(pnlInfo);
            splitPrincipal.Size = new Size(800, 417);
            splitPrincipal.SplitterDistance = 105;
            splitPrincipal.SplitterWidth = 1;
            splitPrincipal.TabIndex = 1;
            splitPrincipal.TabStop = false;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(flpVistasAbiertas);
            pnlInfo.Dock = DockStyle.Fill;
            pnlInfo.Location = new Point(0, 0);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(105, 417);
            pnlInfo.TabIndex = 0;
            // 
            // flpVistasAbiertas
            // 
            flpVistasAbiertas.AutoScroll = true;
            flpVistasAbiertas.BackColor = Color.Crimson;
            flpVistasAbiertas.Dock = DockStyle.Fill;
            flpVistasAbiertas.FlowDirection = FlowDirection.TopDown;
            flpVistasAbiertas.Location = new Point(0, 0);
            flpVistasAbiertas.Name = "flpVistasAbiertas";
            flpVistasAbiertas.Size = new Size(105, 417);
            flpVistasAbiertas.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 14.25F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { msCaptura, msPassword, msHuella, msMeteorologica, msApagar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // msCaptura
            // 
            msCaptura.Name = "msCaptura";
            msCaptura.Size = new Size(91, 29);
            msCaptura.Text = "Captura";
            msCaptura.Click += msCaptura_Click;
            // 
            // msPassword
            // 
            msPassword.Enabled = false;
            msPassword.Name = "msPassword";
            msPassword.Size = new Size(120, 29);
            msPassword.Text = "Contraseña";
            // 
            // msHuella
            // 
            msHuella.Enabled = false;
            msHuella.Name = "msHuella";
            msHuella.Size = new Size(78, 29);
            msHuella.Text = "Huella";
            // 
            // msMeteorologica
            // 
            msMeteorologica.Enabled = false;
            msMeteorologica.Name = "msMeteorologica";
            msMeteorologica.Size = new Size(147, 29);
            msMeteorologica.Text = "Meteorologica";
            // 
            // msApagar
            // 
            msApagar.Enabled = false;
            msApagar.Name = "msApagar";
            msApagar.Size = new Size(85, 29);
            msApagar.Text = "Apagar";
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Crimson;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlPrincipal);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormClosing += Home_FormClosing;
            Load += Home_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            splitPrincipal.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem msCaptura;
        private ToolStripMenuItem msPassword;
        private ToolStripMenuItem msHuella;
        private ToolStripMenuItem msMeteorologica;
        private ToolStripMenuItem msApagar;
        private SplitContainer splitPrincipal;
        private Panel pnlInfo;
        private FlowLayoutPanel flpVistasAbiertas;
    }
}