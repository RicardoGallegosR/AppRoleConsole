namespace Apps_Vicente.Views {
    partial class ucHome {
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
            splitContainer1 = new SplitContainer();
            lblSubDireccion = new Label();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(827, 488);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 100);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(iconPictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lblSubDireccion);
            splitContainer1.Size = new Size(827, 388);
            splitContainer1.SplitterDistance = 413;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 1;
            splitContainer1.TabStop = false;
            // 
            // lblSubDireccion
            // 
            lblSubDireccion.Dock = DockStyle.Fill;
            lblSubDireccion.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Italic);
            lblSubDireccion.ForeColor = Color.Crimson;
            lblSubDireccion.Location = new Point(0, 0);
            lblSubDireccion.Name = "lblSubDireccion";
            lblSubDireccion.Size = new Size(413, 388);
            lblSubDireccion.TabIndex = 1;
            lblSubDireccion.Text = "SUBDIRECCION DE COORDINACIÓN, NORMATIVIDAD Y ATENCIÓN CIUDADANA";
            lblSubDireccion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(827, 100);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI Semibold", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Crimson;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(827, 100);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SEDEMA";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = SystemColors.Control;
            iconPictureBox1.Dock = DockStyle.Fill;
            iconPictureBox1.ForeColor = Color.Crimson;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.LaughSquint;
            iconPictureBox1.IconColor = Color.Crimson;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 388;
            iconPictureBox1.Location = new Point(0, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(413, 388);
            iconPictureBox1.TabIndex = 0;
            iconPictureBox1.TabStop = false;
            // 
            // ucHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucHome";
            Size = new Size(827, 488);
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlHeader;
        private SplitContainer splitContainer1;
        private Label lblSubDireccion;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}
