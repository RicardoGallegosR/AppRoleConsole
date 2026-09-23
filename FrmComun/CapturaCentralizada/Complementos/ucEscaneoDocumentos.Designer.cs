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
            pnlDocumento = new Panel();
            flpPaginas = new FlowLayoutPanel();
            pbDocumento = new PictureBox();
            lblEstadoScanner = new Label();
            flpAcciones = new FlowLayoutPanel();
            cbScanners = new ComboBox();
            ibActualizar = new FontAwesome.Sharp.IconButton();
            ibEscanear = new FontAwesome.Sharp.IconButton();
            ibRecortar = new FontAwesome.Sharp.IconButton();
            ibAgregar = new FontAwesome.Sharp.IconButton();
            ibEliminar = new FontAwesome.Sharp.IconButton();
            ibAceptar = new FontAwesome.Sharp.IconButton();
            ibCancelar = new FontAwesome.Sharp.IconButton();
            ibZoomMas = new FontAwesome.Sharp.IconButton();
            ibZoomMenos = new FontAwesome.Sharp.IconButton();
            ibGuardar = new FontAwesome.Sharp.IconButton();
            pnlPrincipal.SuspendLayout();
            pnlDocumento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbDocumento).BeginInit();
            flpAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(pnlDocumento);
            pnlPrincipal.Controls.Add(lblEstadoScanner);
            pnlPrincipal.Controls.Add(flpAcciones);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Font = new Font("Segoe UI", 10.5F);
            pnlPrincipal.ForeColor = Color.FromArgb(45, 55, 65);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(605, 531);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlDocumento
            // 
            pnlDocumento.AutoScroll = true;
            pnlDocumento.Controls.Add(flpPaginas);
            pnlDocumento.Controls.Add(pbDocumento);
            pnlDocumento.Dock = DockStyle.Fill;
            pnlDocumento.Location = new Point(0, 37);
            pnlDocumento.Name = "pnlDocumento";
            pnlDocumento.Size = new Size(463, 494);
            pnlDocumento.TabIndex = 2;
            // 
            // flpPaginas
            // 
            flpPaginas.AutoScroll = true;
            flpPaginas.Dock = DockStyle.Bottom;
            flpPaginas.Location = new Point(0, 374);
            flpPaginas.Name = "flpPaginas";
            flpPaginas.Size = new Size(463, 120);
            flpPaginas.TabIndex = 1;
            flpPaginas.WrapContents = false;
            // 
            // pbDocumento
            // 
            pbDocumento.BackColor = Color.White;
            pbDocumento.Location = new Point(0, 0);
            pbDocumento.Name = "pbDocumento";
            pbDocumento.Size = new Size(202, 192);
            pbDocumento.SizeMode = PictureBoxSizeMode.AutoSize;
            pbDocumento.TabIndex = 0;
            pbDocumento.TabStop = false;
            // 
            // lblEstadoScanner
            // 
            lblEstadoScanner.Dock = DockStyle.Top;
            lblEstadoScanner.Font = new Font("Segoe UI", 20F);
            lblEstadoScanner.Location = new Point(0, 0);
            lblEstadoScanner.Name = "lblEstadoScanner";
            lblEstadoScanner.Size = new Size(463, 37);
            lblEstadoScanner.TabIndex = 0;
            lblEstadoScanner.TextAlign = ContentAlignment.TopCenter;
            // 
            // flpAcciones
            // 
            flpAcciones.Controls.Add(cbScanners);
            flpAcciones.Controls.Add(ibActualizar);
            flpAcciones.Controls.Add(ibEscanear);
            flpAcciones.Controls.Add(ibRecortar);
            flpAcciones.Controls.Add(ibAgregar);
            flpAcciones.Controls.Add(ibEliminar);
            flpAcciones.Controls.Add(ibAceptar);
            flpAcciones.Controls.Add(ibCancelar);
            flpAcciones.Controls.Add(ibZoomMas);
            flpAcciones.Controls.Add(ibZoomMenos);
            flpAcciones.Controls.Add(ibGuardar);
            flpAcciones.Dock = DockStyle.Right;
            flpAcciones.FlowDirection = FlowDirection.TopDown;
            flpAcciones.Location = new Point(463, 0);
            flpAcciones.Margin = new Padding(1);
            flpAcciones.Name = "flpAcciones";
            flpAcciones.Padding = new Padding(5);
            flpAcciones.Size = new Size(142, 531);
            flpAcciones.TabIndex = 1;
            flpAcciones.WrapContents = false;
            // 
            // cbScanners
            // 
            cbScanners.BackColor = Color.White;
            cbScanners.DropDownStyle = ComboBoxStyle.DropDownList;
            cbScanners.ForeColor = Color.Black;
            cbScanners.FormattingEnabled = true;
            cbScanners.Location = new Point(8, 8);
            cbScanners.Name = "cbScanners";
            cbScanners.Size = new Size(121, 27);
            cbScanners.TabIndex = 1;
            // 
            // ibActualizar
            // 
            ibActualizar.BackColor = Color.White;
            ibActualizar.Enabled = false;
            ibActualizar.FlatAppearance.BorderColor = Color.Crimson;
            ibActualizar.FlatStyle = FlatStyle.Flat;
            ibActualizar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibActualizar.ForeColor = Color.FromArgb(45, 55, 65);
            ibActualizar.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            ibActualizar.IconColor = Color.Crimson;
            ibActualizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibActualizar.IconSize = 40;
            ibActualizar.ImageAlign = ContentAlignment.MiddleLeft;
            ibActualizar.Location = new Point(8, 41);
            ibActualizar.Name = "ibActualizar";
            ibActualizar.Size = new Size(123, 53);
            ibActualizar.TabIndex = 5;
            ibActualizar.Text = "Actualizar escaner";
            ibActualizar.TextAlign = ContentAlignment.MiddleRight;
            ibActualizar.UseVisualStyleBackColor = false;
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
            ibEscanear.Location = new Point(8, 100);
            ibEscanear.Name = "ibEscanear";
            ibEscanear.Size = new Size(123, 47);
            ibEscanear.TabIndex = 2;
            ibEscanear.Text = "Escanear";
            ibEscanear.TextAlign = ContentAlignment.MiddleRight;
            ibEscanear.UseVisualStyleBackColor = false;
            ibEscanear.Click += ibEscanear_Click;
            // 
            // ibRecortar
            // 
            ibRecortar.BackColor = Color.White;
            ibRecortar.Enabled = false;
            ibRecortar.FlatAppearance.BorderColor = Color.Crimson;
            ibRecortar.FlatStyle = FlatStyle.Flat;
            ibRecortar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibRecortar.ForeColor = Color.FromArgb(45, 55, 65);
            ibRecortar.IconChar = FontAwesome.Sharp.IconChar.CropAlt;
            ibRecortar.IconColor = Color.Crimson;
            ibRecortar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibRecortar.IconSize = 40;
            ibRecortar.ImageAlign = ContentAlignment.MiddleLeft;
            ibRecortar.Location = new Point(8, 153);
            ibRecortar.Name = "ibRecortar";
            ibRecortar.Size = new Size(123, 44);
            ibRecortar.TabIndex = 6;
            ibRecortar.Text = "Recortar";
            ibRecortar.TextAlign = ContentAlignment.MiddleRight;
            ibRecortar.UseVisualStyleBackColor = false;
            // 
            // ibAgregar
            // 
            ibAgregar.BackColor = Color.White;
            ibAgregar.Enabled = false;
            ibAgregar.FlatAppearance.BorderColor = Color.Crimson;
            ibAgregar.FlatStyle = FlatStyle.Flat;
            ibAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibAgregar.ForeColor = Color.FromArgb(45, 55, 65);
            ibAgregar.IconChar = FontAwesome.Sharp.IconChar.FileCirclePlus;
            ibAgregar.IconColor = Color.Crimson;
            ibAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibAgregar.IconSize = 40;
            ibAgregar.ImageAlign = ContentAlignment.MiddleLeft;
            ibAgregar.Location = new Point(8, 203);
            ibAgregar.Name = "ibAgregar";
            ibAgregar.Size = new Size(123, 44);
            ibAgregar.TabIndex = 7;
            ibAgregar.Text = "Agregar";
            ibAgregar.TextAlign = ContentAlignment.MiddleRight;
            ibAgregar.UseVisualStyleBackColor = false;
            // 
            // ibEliminar
            // 
            ibEliminar.BackColor = Color.White;
            ibEliminar.Enabled = false;
            ibEliminar.FlatAppearance.BorderColor = Color.Crimson;
            ibEliminar.FlatStyle = FlatStyle.Flat;
            ibEliminar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibEliminar.ForeColor = Color.FromArgb(45, 55, 65);
            ibEliminar.IconChar = FontAwesome.Sharp.IconChar.FileCircleXmark;
            ibEliminar.IconColor = Color.Crimson;
            ibEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibEliminar.IconSize = 40;
            ibEliminar.ImageAlign = ContentAlignment.MiddleLeft;
            ibEliminar.Location = new Point(8, 253);
            ibEliminar.Name = "ibEliminar";
            ibEliminar.Size = new Size(123, 44);
            ibEliminar.TabIndex = 8;
            ibEliminar.Text = "Eliminar";
            ibEliminar.TextAlign = ContentAlignment.MiddleRight;
            ibEliminar.UseVisualStyleBackColor = false;
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
            ibAceptar.Location = new Point(8, 303);
            ibAceptar.Name = "ibAceptar";
            ibAceptar.Size = new Size(123, 44);
            ibAceptar.TabIndex = 3;
            ibAceptar.Text = "Aceptar";
            ibAceptar.TextAlign = ContentAlignment.MiddleRight;
            ibAceptar.UseVisualStyleBackColor = false;
            ibAceptar.Click += ibAceptar_Click;
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
            ibCancelar.Location = new Point(8, 353);
            ibCancelar.Name = "ibCancelar";
            ibCancelar.Size = new Size(123, 44);
            ibCancelar.TabIndex = 4;
            ibCancelar.Text = "Cancelar";
            ibCancelar.TextAlign = ContentAlignment.MiddleRight;
            ibCancelar.UseVisualStyleBackColor = false;
            ibCancelar.Click += ibCancelar_Click;
            // 
            // ibZoomMas
            // 
            ibZoomMas.BackColor = Color.White;
            ibZoomMas.FlatAppearance.BorderColor = Color.Crimson;
            ibZoomMas.FlatStyle = FlatStyle.Flat;
            ibZoomMas.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibZoomMas.ForeColor = Color.FromArgb(45, 55, 65);
            ibZoomMas.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibZoomMas.IconColor = Color.Crimson;
            ibZoomMas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibZoomMas.IconSize = 40;
            ibZoomMas.ImageAlign = ContentAlignment.MiddleLeft;
            ibZoomMas.Location = new Point(8, 403);
            ibZoomMas.Name = "ibZoomMas";
            ibZoomMas.Size = new Size(123, 44);
            ibZoomMas.TabIndex = 11;
            ibZoomMas.Text = "Zoom +";
            ibZoomMas.TextAlign = ContentAlignment.MiddleRight;
            ibZoomMas.UseVisualStyleBackColor = false;
            // 
            // ibZoomMenos
            // 
            ibZoomMenos.BackColor = Color.White;
            ibZoomMenos.FlatAppearance.BorderColor = Color.Crimson;
            ibZoomMenos.FlatStyle = FlatStyle.Flat;
            ibZoomMenos.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibZoomMenos.ForeColor = Color.FromArgb(45, 55, 65);
            ibZoomMenos.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
            ibZoomMenos.IconColor = Color.Crimson;
            ibZoomMenos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibZoomMenos.IconSize = 40;
            ibZoomMenos.ImageAlign = ContentAlignment.MiddleLeft;
            ibZoomMenos.Location = new Point(8, 453);
            ibZoomMenos.Name = "ibZoomMenos";
            ibZoomMenos.Size = new Size(123, 44);
            ibZoomMenos.TabIndex = 10;
            ibZoomMenos.Text = "Zoom -";
            ibZoomMenos.TextAlign = ContentAlignment.MiddleRight;
            ibZoomMenos.UseVisualStyleBackColor = false;
            // 
            // ibGuardar
            // 
            ibGuardar.BackColor = Color.White;
            ibGuardar.FlatAppearance.BorderColor = Color.Crimson;
            ibGuardar.FlatStyle = FlatStyle.Flat;
            ibGuardar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ibGuardar.ForeColor = Color.FromArgb(45, 55, 65);
            ibGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            ibGuardar.IconColor = Color.Crimson;
            ibGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibGuardar.IconSize = 40;
            ibGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            ibGuardar.Location = new Point(8, 503);
            ibGuardar.Name = "ibGuardar";
            ibGuardar.Size = new Size(123, 44);
            ibGuardar.TabIndex = 9;
            ibGuardar.Text = "Guardar";
            ibGuardar.TextAlign = ContentAlignment.MiddleRight;
            ibGuardar.UseVisualStyleBackColor = false;
            // 
            // ucEscaneoDocumentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucEscaneoDocumentos";
            Size = new Size(605, 531);
            Load += ucEscaneoDocumentos_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlDocumento.ResumeLayout(false);
            pnlDocumento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbDocumento).EndInit();
            flpAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private PictureBox pbDocumento;
        private FlowLayoutPanel flpAcciones;
        private FontAwesome.Sharp.IconButton ibEscanear;
        private FontAwesome.Sharp.IconButton ibAceptar;
        private FontAwesome.Sharp.IconButton ibCancelar;
        private ComboBox cbScanners;
        private FontAwesome.Sharp.IconButton ibActualizar;
        private Label lblEstadoScanner;
        private FontAwesome.Sharp.IconButton ibRecortar;
        private FontAwesome.Sharp.IconButton ibAgregar;
        private Panel pnlDocumento;
        private FlowLayoutPanel flpPaginas;
        private FontAwesome.Sharp.IconButton ibEliminar;
        private FontAwesome.Sharp.IconButton ibGuardar;
        private FontAwesome.Sharp.IconButton ibZoomMas;
        private FontAwesome.Sharp.IconButton ibZoomMenos;
    }
}
