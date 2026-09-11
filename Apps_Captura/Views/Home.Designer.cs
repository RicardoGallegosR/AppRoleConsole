namespace Apps_Captura.Views {
    partial class Home {
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
            pnlPrincipal = new Panel();
            scPrincipal = new SplitContainer();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            lblPrograma = new Label();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scPrincipal).BeginInit();
            scPrincipal.Panel1.SuspendLayout();
            scPrincipal.Panel2.SuspendLayout();
            scPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.Controls.Add(scPrincipal);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(814, 470);
            pnlPrincipal.TabIndex = 0;
            // 
            // scPrincipal
            // 
            scPrincipal.BackColor = Color.White;
            scPrincipal.Dock = DockStyle.Fill;
            scPrincipal.IsSplitterFixed = true;
            scPrincipal.Location = new Point(0, 100);
            scPrincipal.Name = "scPrincipal";
            // 
            // scPrincipal.Panel1
            // 
            scPrincipal.Panel1.Controls.Add(iconPictureBox1);
            // 
            // scPrincipal.Panel2
            // 
            scPrincipal.Panel2.Controls.Add(lblPrograma);
            scPrincipal.Size = new Size(814, 370);
            scPrincipal.SplitterDistance = 406;
            scPrincipal.SplitterWidth = 1;
            scPrincipal.TabIndex = 0;
            scPrincipal.TabStop = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.White;
            iconPictureBox1.Dock = DockStyle.Fill;
            iconPictureBox1.ForeColor = Color.Crimson;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.TruckMonster;
            iconPictureBox1.IconColor = Color.Crimson;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 370;
            iconPictureBox1.Location = new Point(0, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(406, 370);
            iconPictureBox1.TabIndex = 0;
            iconPictureBox1.TabStop = false;
            // 
            // lblPrograma
            // 
            lblPrograma.Dock = DockStyle.Fill;
            lblPrograma.Font = new Font("Segoe UI Semibold", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPrograma.ForeColor = Color.Crimson;
            lblPrograma.Location = new Point(0, 0);
            lblPrograma.Name = "lblPrograma";
            lblPrograma.Size = new Size(407, 370);
            lblPrograma.TabIndex = 0;
            lblPrograma.Text = "Captura\r\nCentralizada\r\n";
            lblPrograma.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(814, 100);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 48F);
            lblTitulo.ForeColor = Color.Crimson;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(814, 100);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SEDEMA";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "Home";
            Size = new Size(814, 470);
            pnlPrincipal.ResumeLayout(false);
            scPrincipal.Panel1.ResumeLayout(false);
            scPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scPrincipal).EndInit();
            scPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer scPrincipal;
        private Panel pnlHeader;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label lblPrograma;
    }
}
