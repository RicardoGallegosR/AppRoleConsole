namespace Apps_Regedit.Formularios {
    partial class frmHome {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            pnlPrincipal = new Panel();
            splitPrincipal = new SplitContainer();
            flpVistasAbiertas = new FlowLayoutPanel();
            msPrincipal = new MenuStrip();
            msVerificentros = new ToolStripMenuItem();
            msVISUAL = new ToolStripMenuItem();
            msCAPTURA = new ToolStripMenuItem();
            msSSTPO = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).BeginInit();
            splitPrincipal.Panel1.SuspendLayout();
            splitPrincipal.SuspendLayout();
            msPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitPrincipal);
            pnlPrincipal.Controls.Add(msPrincipal);
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
            splitPrincipal.Panel1.Controls.Add(flpVistasAbiertas);
            splitPrincipal.Size = new Size(800, 417);
            splitPrincipal.SplitterDistance = 105;
            splitPrincipal.SplitterWidth = 1;
            splitPrincipal.TabIndex = 1;
            // 
            // flpVistasAbiertas
            // 
            flpVistasAbiertas.BackColor = Color.Crimson;
            flpVistasAbiertas.Dock = DockStyle.Fill;
            flpVistasAbiertas.FlowDirection = FlowDirection.TopDown;
            flpVistasAbiertas.Location = new Point(0, 0);
            flpVistasAbiertas.Name = "flpVistasAbiertas";
            flpVistasAbiertas.Size = new Size(105, 417);
            flpVistasAbiertas.TabIndex = 0;
            // 
            // msPrincipal
            // 
            msPrincipal.Font = new Font("Segoe UI", 14F);
            msPrincipal.Items.AddRange(new ToolStripItem[] { msVerificentros, msSSTPO });
            msPrincipal.Location = new Point(0, 0);
            msPrincipal.Name = "msPrincipal";
            msPrincipal.Size = new Size(800, 33);
            msPrincipal.TabIndex = 0;
            msPrincipal.Text = "menuStrip1";
            // 
            // msVerificentros
            // 
            msVerificentros.DropDownItems.AddRange(new ToolStripItem[] { msVISUAL, msCAPTURA });
            msVerificentros.Name = "msVerificentros";
            msVerificentros.Size = new Size(157, 29);
            msVerificentros.Text = "VERIFICENTROS";
            // 
            // msVISUAL
            // 
            msVISUAL.Name = "msVISUAL";
            msVISUAL.Size = new Size(165, 30);
            msVISUAL.Text = "VISUAL";
            msVISUAL.Click += msVISUAL_Click;
            // 
            // msCAPTURA
            // 
            msCAPTURA.Name = "msCAPTURA";
            msCAPTURA.Size = new Size(165, 30);
            msCAPTURA.Text = "CAPTURA";
            msCAPTURA.Click += msCAPTURA_Click;
            // 
            // msSSTPO
            // 
            msSSTPO.Name = "msSSTPO";
            msSSTPO.Size = new Size(79, 29);
            msSSTPO.Text = "SSTPO";
            // 
            // frmHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlPrincipal);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = msPrincipal;
            Name = "frmHome";
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            splitPrincipal.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPrincipal).EndInit();
            splitPrincipal.ResumeLayout(false);
            msPrincipal.ResumeLayout(false);
            msPrincipal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private MenuStrip msPrincipal;
        private ToolStripMenuItem msVerificentros;
        private ToolStripMenuItem msVISUAL;
        private ToolStripMenuItem msCAPTURA;
        private ToolStripMenuItem msSSTPO;
        private SplitContainer splitPrincipal;
        private FlowLayoutPanel flpVistasAbiertas;
    }
}