namespace Apps_Vicente.Forms {
    partial class fHome {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fHome));
            pnlPrincipal = new Panel();
            splitPrincipal = new SplitContainer();
            pnlInfo = new Panel();
            flpVistasAbiertas = new FlowLayoutPanel();
            ms = new MenuStrip();
            msReportes = new ToolStripMenuItem();
            msCertificados = new ToolStripMenuItem();
            msRemante = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.SuspendLayout();
            pnlInfo.SuspendLayout();
            ms.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitPrincipal);
            pnlPrincipal.Controls.Add(ms);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 1;
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
            // ms
            // 
            ms.Font = new Font("Segoe UI", 14.25F);
            ms.Items.AddRange(new ToolStripItem[] { msReportes });
            ms.Location = new Point(0, 0);
            ms.Name = "ms";
            ms.Size = new Size(800, 33);
            ms.TabIndex = 0;
            ms.Text = "menuStrip1";
            // 
            // msReportes
            // 
            msReportes.DropDownItems.AddRange(new ToolStripItem[] { msCertificados });
            msReportes.Name = "msReportes";
            msReportes.Size = new Size(97, 29);
            msReportes.Text = "Reportes";
            // 
            // msCertificados
            // 
            msCertificados.DropDownItems.AddRange(new ToolStripItem[] { msRemante });
            msCertificados.Name = "msCertificados";
            msCertificados.Size = new Size(184, 30);
            msCertificados.Text = "Certificados";
            // 
            // msRemante
            // 
            msRemante.Name = "msRemante";
            msRemante.Size = new Size(157, 30);
            msRemante.Text = "Remante";
            msRemante.Click += msRemante_Click;
            // 
            // fHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlPrincipal);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "fHome";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Load += fHome_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            splitPrincipal.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            ms.ResumeLayout(false);
            ms.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer splitPrincipal;
        private Panel pnlInfo;
        private FlowLayoutPanel flpVistasAbiertas;
        private MenuStrip ms;
        private ToolStripMenuItem msReportes;
        private ToolStripMenuItem msCertificados;
        private ToolStripMenuItem msRemante;
    }
}