namespace Apps_Proveedores.Formularios {
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
            spC = new SplitContainer();
            lblVersion = new Label();
            ms = new MenuStrip();
            msHorario = new ToolStripMenuItem();
            msInstalaciones = new ToolStripMenuItem();
            msImpresoras = new ToolStripMenuItem();
            msDrivers = new ToolStripMenuItem();
            msPuertos = new ToolStripMenuItem();
            msCOM = new ToolStripMenuItem();
            msImpresoras1 = new ToolStripMenuItem();
            msServicios = new ToolStripMenuItem();
            msMEM = new ToolStripMenuItem();
            msCerrar = new ToolStripMenuItem();
            msApagar = new ToolStripMenuItem();
            msReiniciar = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spC).BeginInit();
            spC.Panel1.SuspendLayout();
            spC.SuspendLayout();
            ms.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(spC);
            pnlPrincipal.Controls.Add(ms);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Font = new Font("Segoe UI", 14F);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // spC
            // 
            spC.Dock = DockStyle.Fill;
            spC.IsSplitterFixed = true;
            spC.Location = new Point(0, 33);
            spC.Name = "spC";
            // 
            // spC.Panel1
            // 
            spC.Panel1.BackColor = Color.Crimson;
            spC.Panel1.Controls.Add(lblVersion);
            spC.Panel1MinSize = 40;
            spC.Size = new Size(800, 417);
            spC.SplitterDistance = 70;
            spC.SplitterWidth = 1;
            spC.TabIndex = 0;
            spC.TabStop = false;
            // 
            // lblVersion
            // 
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVersion.ForeColor = SystemColors.Window;
            lblVersion.Location = new Point(0, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(70, 417);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Version";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ms
            // 
            ms.Font = new Font("Segoe UI", 14F);
            ms.Items.AddRange(new ToolStripItem[] { msHorario, msInstalaciones, msPuertos, msServicios, msCerrar });
            ms.Location = new Point(0, 0);
            ms.Name = "ms";
            ms.Size = new Size(800, 33);
            ms.TabIndex = 1;
            ms.Text = "ms";
            // 
            // msHorario
            // 
            msHorario.Name = "msHorario";
            msHorario.Size = new Size(88, 29);
            msHorario.Text = "Horario";
            msHorario.Click += msHorario_Click;
            // 
            // msInstalaciones
            // 
            msInstalaciones.DropDownItems.AddRange(new ToolStripItem[] { msImpresoras, msDrivers });
            msInstalaciones.Name = "msInstalaciones";
            msInstalaciones.Size = new Size(133, 29);
            msInstalaciones.Text = "Instalaciones";
            // 
            // msImpresoras
            // 
            msImpresoras.Name = "msImpresoras";
            msImpresoras.Size = new Size(177, 30);
            msImpresoras.Text = "Impresoras";
            // 
            // msDrivers
            // 
            msDrivers.Name = "msDrivers";
            msDrivers.Size = new Size(177, 30);
            msDrivers.Text = "Drivers";
            // 
            // msPuertos
            // 
            msPuertos.DropDownItems.AddRange(new ToolStripItem[] { msCOM, msImpresoras1 });
            msPuertos.Name = "msPuertos";
            msPuertos.Size = new Size(88, 29);
            msPuertos.Text = "Puertos";
            // 
            // msCOM
            // 
            msCOM.Name = "msCOM";
            msCOM.Size = new Size(177, 30);
            msCOM.Text = "COM";
            // 
            // msImpresoras1
            // 
            msImpresoras1.Name = "msImpresoras1";
            msImpresoras1.Size = new Size(177, 30);
            msImpresoras1.Text = "Impresoras";
            // 
            // msServicios
            // 
            msServicios.DropDownItems.AddRange(new ToolStripItem[] { msMEM });
            msServicios.Name = "msServicios";
            msServicios.Size = new Size(98, 29);
            msServicios.Text = "Servicios";
            // 
            // msMEM
            // 
            msMEM.Name = "msMEM";
            msMEM.Size = new Size(128, 30);
            msMEM.Text = "MEM";
            // 
            // msCerrar
            // 
            msCerrar.DropDownItems.AddRange(new ToolStripItem[] { msApagar, msReiniciar });
            msCerrar.Name = "msCerrar";
            msCerrar.Size = new Size(77, 29);
            msCerrar.Text = "Cerrar";
            // 
            // msApagar
            // 
            msApagar.Name = "msApagar";
            msApagar.Size = new Size(180, 30);
            msApagar.Text = "Apagar";
            msApagar.Click += msApagar_Click;
            // 
            // msReiniciar
            // 
            msReiniciar.Name = "msReiniciar";
            msReiniciar.Size = new Size(180, 30);
            msReiniciar.Text = "Reiniciar";
            msReiniciar.Click += msReiniciar_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            Icon = (Icon)resources.GetObject("$Icon");
            MainMenuStrip = ms;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Load += Home_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            spC.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spC).EndInit();
            spC.ResumeLayout(false);
            ms.ResumeLayout(false);
            ms.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private MenuStrip ms;
        private ToolStripMenuItem msHorario;
        private ToolStripMenuItem msInstalaciones;
        private ToolStripMenuItem msImpresoras;
        private ToolStripMenuItem msDrivers;
        private ToolStripMenuItem msPuertos;
        private ToolStripMenuItem msCOM;
        private ToolStripMenuItem msImpresoras1;
        private ToolStripMenuItem msServicios;
        private ToolStripMenuItem msMEM;
        private ToolStripMenuItem msCerrar;
        private ToolStripMenuItem msApagar;
        private ToolStripMenuItem msReiniciar;
        private SplitContainer spC;
        private Label lblVersion;
    }
}