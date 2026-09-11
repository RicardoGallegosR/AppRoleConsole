namespace FrmComun.CapturaCentralizada {
    partial class ucRegistroVehicular {
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
            tlpBody = new TableLayoutPanel();
            tlpFila1 = new TableLayoutPanel();
            gbAcciones = new GroupBox();
            gbVinModelo = new GroupBox();
            ucVinModelo1 = new FrmComun.CapturaCentralizada.Complementos.ucVinModelo();
            gbAcceso = new GroupBox();
            gbVisitante = new GroupBox();
            ucVisitante1 = new FrmComun.CapturaCentralizada.Complementos.ucVisitante();
            tlpFila2 = new TableLayoutPanel();
            gbTarjetaCirculacion = new GroupBox();
            gbSeleccionVehicular = new GroupBox();
            ucSeleccionVehiculo1 = new FrmComun.CapturaCentralizada.Complementos.ucSeleccionVehiculo();
            tlpFila3 = new TableLayoutPanel();
            gbEscaneoDocumentos = new GroupBox();
            gbDocumentosAdicionales = new GroupBox();
            pnlHeader = new Panel();
            tlpHeader = new TableLayoutPanel();
            tlpPlacaHeader = new TableLayoutPanel();
            lblPlaca = new Label();
            txtPlaca = new TextBox();
            tlpLinea = new TableLayoutPanel();
            txtLinea = new TextBox();
            lblLinea = new Label();
            ipbLogo = new FontAwesome.Sharp.IconPictureBox();
            ucTarjetaCirculacion1 = new FrmComun.CapturaCentralizada.Complementos.ucTarjetaCirculacion();
            pnlPrincipal.SuspendLayout();
            tlpBody.SuspendLayout();
            tlpFila1.SuspendLayout();
            gbVinModelo.SuspendLayout();
            gbVisitante.SuspendLayout();
            tlpFila2.SuspendLayout();
            gbTarjetaCirculacion.SuspendLayout();
            gbSeleccionVehicular.SuspendLayout();
            tlpFila3.SuspendLayout();
            pnlHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            tlpPlacaHeader.SuspendLayout();
            tlpLinea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ipbLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(tlpBody);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.ForeColor = Color.FromArgb(1, 118, 71);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(1009, 579);
            pnlPrincipal.TabIndex = 0;
            // 
            // tlpBody
            // 
            tlpBody.ColumnCount = 1;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBody.Controls.Add(tlpFila1, 0, 0);
            tlpBody.Controls.Add(tlpFila2, 0, 1);
            tlpBody.Controls.Add(tlpFila3, 0, 2);
            tlpBody.Dock = DockStyle.Fill;
            tlpBody.ForeColor = Color.Black;
            tlpBody.Location = new Point(0, 100);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 3;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 37F));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 29F));
            tlpBody.Size = new Size(1009, 479);
            tlpBody.TabIndex = 1;
            // 
            // tlpFila1
            // 
            tlpFila1.ColumnCount = 4;
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39F));
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13F));
            tlpFila1.Controls.Add(gbAcciones, 3, 0);
            tlpFila1.Controls.Add(gbVinModelo, 2, 0);
            tlpFila1.Controls.Add(gbAcceso, 1, 0);
            tlpFila1.Controls.Add(gbVisitante, 0, 0);
            tlpFila1.Dock = DockStyle.Fill;
            tlpFila1.Location = new Point(3, 3);
            tlpFila1.Name = "tlpFila1";
            tlpFila1.RowCount = 1;
            tlpFila1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila1.Size = new Size(1003, 171);
            tlpFila1.TabIndex = 0;
            // 
            // gbAcciones
            // 
            gbAcciones.Dock = DockStyle.Fill;
            gbAcciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbAcciones.ForeColor = Color.Crimson;
            gbAcciones.Location = new Point(876, 5);
            gbAcciones.Margin = new Padding(5);
            gbAcciones.Name = "gbAcciones";
            gbAcciones.Padding = new Padding(10);
            gbAcciones.Size = new Size(122, 161);
            gbAcciones.TabIndex = 3;
            gbAcciones.TabStop = false;
            gbAcciones.Text = "ACCIONES";
            // 
            // gbVinModelo
            // 
            gbVinModelo.Controls.Add(ucVinModelo1);
            gbVinModelo.Dock = DockStyle.Fill;
            gbVinModelo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbVinModelo.ForeColor = Color.Crimson;
            gbVinModelo.Location = new Point(676, 5);
            gbVinModelo.Margin = new Padding(5);
            gbVinModelo.Name = "gbVinModelo";
            gbVinModelo.Padding = new Padding(10);
            gbVinModelo.Size = new Size(190, 161);
            gbVinModelo.TabIndex = 2;
            gbVinModelo.TabStop = false;
            gbVinModelo.Text = "VIN / MODELO";
            // 
            // ucVinModelo1
            // 
            ucVinModelo1.Dock = DockStyle.Fill;
            ucVinModelo1.Location = new Point(10, 30);
            ucVinModelo1.Name = "ucVinModelo1";
            ucVinModelo1.Size = new Size(170, 121);
            ucVinModelo1.TabIndex = 0;
            // 
            // gbAcceso
            // 
            gbAcceso.Dock = DockStyle.Fill;
            gbAcceso.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbAcceso.ForeColor = Color.Crimson;
            gbAcceso.Location = new Point(285, 5);
            gbAcceso.Margin = new Padding(5);
            gbAcceso.Name = "gbAcceso";
            gbAcceso.Padding = new Padding(10);
            gbAcceso.Size = new Size(381, 161);
            gbAcceso.TabIndex = 1;
            gbAcceso.TabStop = false;
            gbAcceso.Text = "ACCESO / CONSULTA";
            // 
            // gbVisitante
            // 
            gbVisitante.Controls.Add(ucVisitante1);
            gbVisitante.Dock = DockStyle.Fill;
            gbVisitante.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            gbVisitante.ForeColor = Color.Crimson;
            gbVisitante.Location = new Point(5, 5);
            gbVisitante.Margin = new Padding(5);
            gbVisitante.Name = "gbVisitante";
            gbVisitante.Padding = new Padding(10);
            gbVisitante.Size = new Size(270, 161);
            gbVisitante.TabIndex = 0;
            gbVisitante.TabStop = false;
            gbVisitante.Text = "VISITANTE";
            // 
            // ucVisitante1
            // 
            ucVisitante1.Dock = DockStyle.Fill;
            ucVisitante1.Location = new Point(10, 30);
            ucVisitante1.Name = "ucVisitante1";
            ucVisitante1.Size = new Size(250, 121);
            ucVisitante1.TabIndex = 0;
            // 
            // tlpFila2
            // 
            tlpFila2.ColumnCount = 2;
            tlpFila2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            tlpFila2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tlpFila2.Controls.Add(gbTarjetaCirculacion, 1, 0);
            tlpFila2.Controls.Add(gbSeleccionVehicular, 0, 0);
            tlpFila2.Dock = DockStyle.Fill;
            tlpFila2.Location = new Point(3, 180);
            tlpFila2.Name = "tlpFila2";
            tlpFila2.RowCount = 1;
            tlpFila2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFila2.Size = new Size(1003, 156);
            tlpFila2.TabIndex = 1;
            // 
            // gbTarjetaCirculacion
            // 
            gbTarjetaCirculacion.Controls.Add(ucTarjetaCirculacion1);
            gbTarjetaCirculacion.Dock = DockStyle.Fill;
            gbTarjetaCirculacion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbTarjetaCirculacion.ForeColor = Color.Crimson;
            gbTarjetaCirculacion.Location = new Point(566, 5);
            gbTarjetaCirculacion.Margin = new Padding(5);
            gbTarjetaCirculacion.Name = "gbTarjetaCirculacion";
            gbTarjetaCirculacion.Padding = new Padding(10);
            gbTarjetaCirculacion.Size = new Size(432, 146);
            gbTarjetaCirculacion.TabIndex = 2;
            gbTarjetaCirculacion.TabStop = false;
            gbTarjetaCirculacion.Text = "TARJETA DE CIRCUALCIÓN";
            // 
            // gbSeleccionVehicular
            // 
            gbSeleccionVehicular.Controls.Add(ucSeleccionVehiculo1);
            gbSeleccionVehicular.Dock = DockStyle.Fill;
            gbSeleccionVehicular.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbSeleccionVehicular.ForeColor = Color.Crimson;
            gbSeleccionVehicular.Location = new Point(5, 5);
            gbSeleccionVehicular.Margin = new Padding(5);
            gbSeleccionVehicular.Name = "gbSeleccionVehicular";
            gbSeleccionVehicular.Padding = new Padding(10);
            gbSeleccionVehicular.Size = new Size(551, 146);
            gbSeleccionVehicular.TabIndex = 1;
            gbSeleccionVehicular.TabStop = false;
            gbSeleccionVehicular.Text = "SELECCION DE VEHICULO";
            // 
            // ucSeleccionVehiculo1
            // 
            ucSeleccionVehiculo1.Dock = DockStyle.Fill;
            ucSeleccionVehiculo1.Location = new Point(10, 30);
            ucSeleccionVehiculo1.Name = "ucSeleccionVehiculo1";
            ucSeleccionVehiculo1.Size = new Size(531, 106);
            ucSeleccionVehiculo1.TabIndex = 0;
            // 
            // tlpFila3
            // 
            tlpFila3.ColumnCount = 2;
            tlpFila3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            tlpFila3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tlpFila3.Controls.Add(gbEscaneoDocumentos, 1, 0);
            tlpFila3.Controls.Add(gbDocumentosAdicionales, 0, 0);
            tlpFila3.Dock = DockStyle.Fill;
            tlpFila3.Location = new Point(3, 342);
            tlpFila3.Name = "tlpFila3";
            tlpFila3.RowCount = 1;
            tlpFila3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila3.Size = new Size(1003, 134);
            tlpFila3.TabIndex = 2;
            // 
            // gbEscaneoDocumentos
            // 
            gbEscaneoDocumentos.Dock = DockStyle.Fill;
            gbEscaneoDocumentos.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbEscaneoDocumentos.ForeColor = Color.Crimson;
            gbEscaneoDocumentos.Location = new Point(576, 5);
            gbEscaneoDocumentos.Margin = new Padding(5);
            gbEscaneoDocumentos.Name = "gbEscaneoDocumentos";
            gbEscaneoDocumentos.Padding = new Padding(10);
            gbEscaneoDocumentos.Size = new Size(422, 124);
            gbEscaneoDocumentos.TabIndex = 2;
            gbEscaneoDocumentos.TabStop = false;
            gbEscaneoDocumentos.Text = "ESCANEO DE DOCUMENTOS";
            // 
            // gbDocumentosAdicionales
            // 
            gbDocumentosAdicionales.Dock = DockStyle.Fill;
            gbDocumentosAdicionales.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbDocumentosAdicionales.ForeColor = Color.Crimson;
            gbDocumentosAdicionales.Location = new Point(5, 5);
            gbDocumentosAdicionales.Margin = new Padding(5);
            gbDocumentosAdicionales.Name = "gbDocumentosAdicionales";
            gbDocumentosAdicionales.Padding = new Padding(10);
            gbDocumentosAdicionales.Size = new Size(561, 124);
            gbDocumentosAdicionales.TabIndex = 1;
            gbDocumentosAdicionales.TabStop = false;
            gbDocumentosAdicionales.Text = "DOCUMENTOS ADICIONALES";
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(tlpHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1009, 100);
            pnlHeader.TabIndex = 0;
            // 
            // tlpHeader
            // 
            tlpHeader.ColumnCount = 3;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpHeader.Controls.Add(tlpPlacaHeader, 0, 0);
            tlpHeader.Controls.Add(tlpLinea, 1, 0);
            tlpHeader.Controls.Add(ipbLogo, 2, 0);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(0, 0);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 1;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHeader.Size = new Size(1009, 100);
            tlpHeader.TabIndex = 0;
            // 
            // tlpPlacaHeader
            // 
            tlpPlacaHeader.ColumnCount = 2;
            tlpPlacaHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPlacaHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPlacaHeader.Controls.Add(lblPlaca, 0, 0);
            tlpPlacaHeader.Controls.Add(txtPlaca, 1, 0);
            tlpPlacaHeader.Dock = DockStyle.Fill;
            tlpPlacaHeader.Location = new Point(3, 3);
            tlpPlacaHeader.Name = "tlpPlacaHeader";
            tlpPlacaHeader.RowCount = 1;
            tlpPlacaHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpPlacaHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpPlacaHeader.Size = new Size(347, 94);
            tlpPlacaHeader.TabIndex = 0;
            // 
            // lblPlaca
            // 
            lblPlaca.Dock = DockStyle.Fill;
            lblPlaca.Font = new Font("Segoe UI", 36F);
            lblPlaca.ForeColor = Color.Crimson;
            lblPlaca.Location = new Point(3, 0);
            lblPlaca.Name = "lblPlaca";
            lblPlaca.Padding = new Padding(0, 0, 12, 0);
            lblPlaca.Size = new Size(167, 94);
            lblPlaca.TabIndex = 0;
            lblPlaca.Text = "Placa";
            lblPlaca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPlaca
            // 
            txtPlaca.BorderStyle = BorderStyle.FixedSingle;
            txtPlaca.Dock = DockStyle.Fill;
            txtPlaca.Font = new Font("Segoe UI", 28F);
            txtPlaca.Location = new Point(173, 16);
            txtPlaca.Margin = new Padding(0, 16, 0, 16);
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(174, 57);
            txtPlaca.TabIndex = 1;
            // 
            // tlpLinea
            // 
            tlpLinea.ColumnCount = 2;
            tlpLinea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLinea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLinea.Controls.Add(txtLinea, 1, 0);
            tlpLinea.Controls.Add(lblLinea, 0, 0);
            tlpLinea.Dock = DockStyle.Fill;
            tlpLinea.Location = new Point(356, 3);
            tlpLinea.Name = "tlpLinea";
            tlpLinea.RowCount = 1;
            tlpLinea.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLinea.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLinea.Size = new Size(296, 94);
            tlpLinea.TabIndex = 0;
            tlpLinea.TabStop = true;
            // 
            // txtLinea
            // 
            txtLinea.BorderStyle = BorderStyle.FixedSingle;
            txtLinea.Dock = DockStyle.Fill;
            txtLinea.Font = new Font("Segoe UI", 28F);
            txtLinea.Location = new Point(148, 16);
            txtLinea.Margin = new Padding(0, 16, 0, 16);
            txtLinea.Name = "txtLinea";
            txtLinea.Size = new Size(148, 57);
            txtLinea.TabIndex = 2;
            // 
            // lblLinea
            // 
            lblLinea.Dock = DockStyle.Fill;
            lblLinea.Font = new Font("Segoe UI", 28F);
            lblLinea.ForeColor = Color.Crimson;
            lblLinea.Location = new Point(0, 0);
            lblLinea.Margin = new Padding(0, 0, 10, 0);
            lblLinea.Name = "lblLinea";
            lblLinea.Padding = new Padding(0, 0, 12, 0);
            lblLinea.Size = new Size(138, 94);
            lblLinea.TabIndex = 1;
            lblLinea.Text = "Línea";
            lblLinea.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ipbLogo
            // 
            ipbLogo.BackColor = Color.White;
            ipbLogo.Dock = DockStyle.Right;
            ipbLogo.ForeColor = Color.FromArgb(12, 142, 87);
            ipbLogo.IconChar = FontAwesome.Sharp.IconChar.Pagelines;
            ipbLogo.IconColor = Color.FromArgb(12, 142, 87);
            ipbLogo.IconFont = FontAwesome.Sharp.IconFont.Brands;
            ipbLogo.IconSize = 94;
            ipbLogo.Location = new Point(908, 3);
            ipbLogo.Name = "ipbLogo";
            ipbLogo.Size = new Size(98, 94);
            ipbLogo.TabIndex = 1;
            ipbLogo.TabStop = false;
            // 
            // ucTarjetaCirculacion1
            // 
            ucTarjetaCirculacion1.Dock = DockStyle.Fill;
            ucTarjetaCirculacion1.Location = new Point(10, 30);
            ucTarjetaCirculacion1.Name = "ucTarjetaCirculacion1";
            ucTarjetaCirculacion1.Size = new Size(412, 106);
            ucTarjetaCirculacion1.TabIndex = 0;
            // 
            // ucRegistroVehicular
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucRegistroVehicular";
            Size = new Size(1009, 579);
            pnlPrincipal.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            tlpFila1.ResumeLayout(false);
            gbVinModelo.ResumeLayout(false);
            gbVisitante.ResumeLayout(false);
            tlpFila2.ResumeLayout(false);
            gbTarjetaCirculacion.ResumeLayout(false);
            gbSeleccionVehicular.ResumeLayout(false);
            tlpFila3.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            tlpPlacaHeader.ResumeLayout(false);
            tlpPlacaHeader.PerformLayout();
            tlpLinea.ResumeLayout(false);
            tlpLinea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ipbLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlHeader;
        private TableLayoutPanel tlpHeader;
        private TableLayoutPanel tlpPlacaHeader;
        private Label lblPlaca;
        private TextBox txtPlaca;
        private TableLayoutPanel tlpLinea;
        private TextBox txtLinea;
        private Label lblLinea;
        private FontAwesome.Sharp.IconPictureBox ipbLogo;
        private TableLayoutPanel tlpBody;
        private TableLayoutPanel tlpFila1;
        private TableLayoutPanel tlpFila2;
        private TableLayoutPanel tlpFila3;
        private GroupBox gbVisitante;
        private GroupBox gbAcciones;
        private GroupBox gbVinModelo;
        private GroupBox gbAcceso;
        private GroupBox gbTarjetaCirculacion;
        private GroupBox gbSeleccionVehicular;
        private GroupBox gbEscaneoDocumentos;
        private GroupBox gbDocumentosAdicionales;
        private Complementos.ucVinModelo ucVinModelo1;
        private Complementos.ucVisitante ucVisitante1;
        private Complementos.ucSeleccionVehiculo ucSeleccionVehiculo1;
        private Complementos.ucTarjetaCirculacion ucTarjetaCirculacion1;
    }
}
