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
            splitContainer1 = new SplitContainer();
            pnlInfo = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblVersion = new Label();
            menuStrip1 = new MenuStrip();
            msCaptura = new ToolStripMenuItem();
            msPassword = new ToolStripMenuItem();
            msHuella = new ToolStripMenuItem();
            msMeteorologica = new ToolStripMenuItem();
            msApagar = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlInfo.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Controls.Add(menuStrip1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlInfo);
            splitContainer1.Size = new Size(800, 417);
            splitContainer1.SplitterDistance = 60;
            splitContainer1.TabIndex = 1;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(tableLayoutPanel1);
            pnlInfo.Dock = DockStyle.Fill;
            pnlInfo.Location = new Point(0, 0);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(60, 417);
            pnlInfo.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Crimson;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblVersion, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(60, 417);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblVersion.Location = new Point(3, 138);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(54, 138);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Version";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
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
            // 
            // msPassword
            // 
            msPassword.Name = "msPassword";
            msPassword.Size = new Size(120, 29);
            msPassword.Text = "Contraseña";
            // 
            // msHuella
            // 
            msHuella.Name = "msHuella";
            msHuella.Size = new Size(78, 29);
            msHuella.Text = "Huella";
            // 
            // msMeteorologica
            // 
            msMeteorologica.Name = "msMeteorologica";
            msMeteorologica.Size = new Size(147, 29);
            msMeteorologica.Text = "Meteorologica";
            // 
            // msApagar
            // 
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
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
        private SplitContainer splitContainer1;
        private Panel pnlInfo;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblVersion;
    }
}