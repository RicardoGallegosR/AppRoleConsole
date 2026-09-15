namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucEscaneoDocumentos {
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
            flpAcciones = new FlowLayoutPanel();
            ibEscanear = new FontAwesome.Sharp.IconButton();
            ibAceptar = new FontAwesome.Sharp.IconButton();
            ibCancelar = new FontAwesome.Sharp.IconButton();
            pbDocumento = new PictureBox();
            pnlPrincipal.SuspendLayout();
            flpAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbDocumento).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(flpAcciones);
            pnlPrincipal.Controls.Add(pbDocumento);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Font = new Font("Segoe UI", 10.5F);
            pnlPrincipal.ForeColor = Color.FromArgb(45, 55, 65);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(553, 179);
            pnlPrincipal.TabIndex = 0;
            // 
            // flpAcciones
            // 
            flpAcciones.Controls.Add(ibEscanear);
            flpAcciones.Controls.Add(ibAceptar);
            flpAcciones.Controls.Add(ibCancelar);
            flpAcciones.Dock = DockStyle.Right;
            flpAcciones.FlowDirection = FlowDirection.TopDown;
            flpAcciones.Location = new Point(411, 0);
            flpAcciones.Margin = new Padding(1);
            flpAcciones.Name = "flpAcciones";
            flpAcciones.Padding = new Padding(5);
            flpAcciones.Size = new Size(142, 179);
            flpAcciones.TabIndex = 1;
            flpAcciones.WrapContents = false;
            // 
            // ibEscanear
            // 
            ibEscanear.BackColor = Color.Crimson;
            ibEscanear.FlatAppearance.BorderColor = Color.Crimson;
            ibEscanear.FlatAppearance.BorderSize = 0;
            ibEscanear.FlatStyle = FlatStyle.Flat;
            ibEscanear.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibEscanear.ForeColor = Color.White;
            ibEscanear.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            ibEscanear.IconColor = Color.White;
            ibEscanear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibEscanear.IconSize = 40;
            ibEscanear.ImageAlign = ContentAlignment.MiddleLeft;
            ibEscanear.Location = new Point(8, 8);
            ibEscanear.Name = "ibEscanear";
            ibEscanear.Size = new Size(123, 47);
            ibEscanear.TabIndex = 1;
            ibEscanear.Text = "Escanear";
            ibEscanear.TextAlign = ContentAlignment.MiddleRight;
            ibEscanear.UseVisualStyleBackColor = false;
            // 
            // ibAceptar
            // 
            ibAceptar.BackColor = Color.White;
            ibAceptar.Enabled = false;
            ibAceptar.FlatAppearance.BorderColor = Color.Crimson;
            ibAceptar.FlatStyle = FlatStyle.Flat;
            ibAceptar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibAceptar.ForeColor = Color.FromArgb(45, 55, 65);
            ibAceptar.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            ibAceptar.IconColor = Color.Crimson;
            ibAceptar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibAceptar.IconSize = 40;
            ibAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            ibAceptar.Location = new Point(8, 61);
            ibAceptar.Name = "ibAceptar";
            ibAceptar.Size = new Size(123, 44);
            ibAceptar.TabIndex = 2;
            ibAceptar.Text = "Aceptar";
            ibAceptar.TextAlign = ContentAlignment.MiddleRight;
            ibAceptar.UseVisualStyleBackColor = false;
            // 
            // ibCancelar
            // 
            ibCancelar.BackColor = Color.White;
            ibCancelar.FlatAppearance.BorderColor = Color.Crimson;
            ibCancelar.FlatStyle = FlatStyle.Flat;
            ibCancelar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibCancelar.ForeColor = Color.FromArgb(45, 55, 65);
            ibCancelar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            ibCancelar.IconColor = Color.Crimson;
            ibCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibCancelar.IconSize = 40;
            ibCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            ibCancelar.Location = new Point(8, 111);
            ibCancelar.Name = "ibCancelar";
            ibCancelar.Size = new Size(123, 44);
            ibCancelar.TabIndex = 3;
            ibCancelar.Text = "Cancelar";
            ibCancelar.TextAlign = ContentAlignment.MiddleRight;
            ibCancelar.UseVisualStyleBackColor = false;
            // 
            // pbDocumento
            // 
            pbDocumento.Dock = DockStyle.Fill;
            pbDocumento.Location = new Point(0, 0);
            pbDocumento.Name = "pbDocumento";
            pbDocumento.Size = new Size(553, 179);
            pbDocumento.SizeMode = PictureBoxSizeMode.Zoom;
            pbDocumento.TabIndex = 0;
            pbDocumento.TabStop = false;
            // 
            // ucEscaneoDocumentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucEscaneoDocumentos";
            Size = new Size(553, 179);
            pnlPrincipal.ResumeLayout(false);
            flpAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbDocumento).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private PictureBox pbDocumento;
        private FlowLayoutPanel flpAcciones;
        private FontAwesome.Sharp.IconButton ibEscanear;
        private FontAwesome.Sharp.IconButton ibAceptar;
        private FontAwesome.Sharp.IconButton ibCancelar;
    }
}
