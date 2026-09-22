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
            tcPrincipal = new TabControl();
            tpRegistroVehicular = new TabPage();
            tlpFila1 = new TableLayoutPanel();
            gbAcceso = new GroupBox();
            ucAccesoConsulta1 = new FrmComun.CapturaCentralizada.Complementos.ucAccesoConsulta();
            pnlRegistro = new Panel();
            gbAcciones = new GroupBox();
            gbVinModelo = new GroupBox();
            ucVinModelo1 = new FrmComun.CapturaCentralizada.Complementos.ucVinModelo();
            gbVisitante = new GroupBox();
            ucVisitante1 = new FrmComun.CapturaCentralizada.Complementos.ucVisitante();
            tpTC = new TabPage();
            tlpFila2 = new TableLayoutPanel();
            gbTarjetaCirculacion = new GroupBox();
            ucTarjetaCirculacion1 = new FrmComun.CapturaCentralizada.Complementos.ucTarjetaCirculacion();
            gbSeleccionVehicular = new GroupBox();
            ucSeleccionVehiculo1 = new FrmComun.CapturaCentralizada.Complementos.ucSeleccionVehiculo();
            tpDocumentosAdicionales = new TabPage();
            tlpFila3 = new TableLayoutPanel();
            gbEscaneoDocumentos = new GroupBox();
            ucEscaneoDocumentos1 = new FrmComun.CapturaCentralizada.Complementos.ucEscaneoDocumentos();
            gbDocumentosAdicionales = new GroupBox();
            ucDocumentosAdicionales1 = new FrmComun.CapturaCentralizada.Complementos.ucDocumentosAdicionales();
            pnlHeader = new Panel();
            tlpHeader = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblPET = new Label();
            cbPET = new CheckBox();
            tlpPlacaHeader = new TableLayoutPanel();
            lblPlaca = new Label();
            txtPlaca = new TextBox();
            tlpLinea = new TableLayoutPanel();
            txtLinea = new TextBox();
            lblLinea = new Label();
            ipbLogo = new FontAwesome.Sharp.IconPictureBox();
            pnlPrincipal.SuspendLayout();
            tcPrincipal.SuspendLayout();
            tpRegistroVehicular.SuspendLayout();
            tlpFila1.SuspendLayout();
            gbAcceso.SuspendLayout();
            pnlRegistro.SuspendLayout();
            gbVinModelo.SuspendLayout();
            gbVisitante.SuspendLayout();
            tpTC.SuspendLayout();
            tlpFila2.SuspendLayout();
            gbTarjetaCirculacion.SuspendLayout();
            gbSeleccionVehicular.SuspendLayout();
            tpDocumentosAdicionales.SuspendLayout();
            tlpFila3.SuspendLayout();
            gbEscaneoDocumentos.SuspendLayout();
            gbDocumentosAdicionales.SuspendLayout();
            pnlHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tlpPlacaHeader.SuspendLayout();
            tlpLinea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ipbLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.WhiteSmoke;
            pnlPrincipal.Controls.Add(tcPrincipal);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.ForeColor = Color.FromArgb(1, 118, 71);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(1009, 706);
            pnlPrincipal.TabIndex = 0;
            // 
            // tcPrincipal
            // 
            tcPrincipal.Controls.Add(tpRegistroVehicular);
            tcPrincipal.Controls.Add(tpTC);
            tcPrincipal.Controls.Add(tpDocumentosAdicionales);
            tcPrincipal.Dock = DockStyle.Fill;
            tcPrincipal.Font = new Font("Segoe UI", 14F);
            tcPrincipal.Location = new Point(0, 100);
            tcPrincipal.Name = "tcPrincipal";
            tcPrincipal.SelectedIndex = 0;
            tcPrincipal.Size = new Size(1009, 606);
            tcPrincipal.TabIndex = 2;
            // 
            // tpRegistroVehicular
            // 
            tpRegistroVehicular.Controls.Add(tlpFila1);
            tpRegistroVehicular.Location = new Point(4, 34);
            tpRegistroVehicular.Name = "tpRegistroVehicular";
            tpRegistroVehicular.Padding = new Padding(3);
            tpRegistroVehicular.Size = new Size(1001, 568);
            tpRegistroVehicular.TabIndex = 0;
            tpRegistroVehicular.Text = "Registro Vehicular";
            tpRegistroVehicular.UseVisualStyleBackColor = true;
            // 
            // tlpFila1
            // 
            tlpFila1.ColumnCount = 2;
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpFila1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tlpFila1.Controls.Add(gbAcceso, 1, 0);
            tlpFila1.Controls.Add(pnlRegistro, 0, 0);
            tlpFila1.Dock = DockStyle.Fill;
            tlpFila1.Location = new Point(3, 3);
            tlpFila1.Name = "tlpFila1";
            tlpFila1.RowCount = 1;
            tlpFila1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila1.Size = new Size(995, 562);
            tlpFila1.TabIndex = 1;
            // 
            // gbAcceso
            // 
            gbAcceso.BackColor = Color.White;
            gbAcceso.Controls.Add(ucAccesoConsulta1);
            gbAcceso.Dock = DockStyle.Fill;
            gbAcceso.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbAcceso.ForeColor = Color.Crimson;
            gbAcceso.Location = new Point(397, 5);
            gbAcceso.Margin = new Padding(5);
            gbAcceso.Name = "gbAcceso";
            gbAcceso.Padding = new Padding(10);
            gbAcceso.Size = new Size(593, 552);
            gbAcceso.TabIndex = 1;
            gbAcceso.TabStop = false;
            gbAcceso.Text = "ACCESO / CONSULTA";
            // 
            // ucAccesoConsulta1
            // 
            ucAccesoConsulta1.Ambientales = "STATUS AMBIENTALES";
            ucAccesoConsulta1.Dock = DockStyle.Fill;
            ucAccesoConsulta1.Fotocivicas = "STATUS FOTOCIIVCAS";
            ucAccesoConsulta1.Location = new Point(10, 30);
            ucAccesoConsulta1.Monetarias = "STATUS MONETARIAS\r\n";
            ucAccesoConsulta1.Name = "ucAccesoConsulta1";
            ucAccesoConsulta1.Placa = "";
            ucAccesoConsulta1.Size = new Size(573, 512);
            ucAccesoConsulta1.TabIndex = 0;
            ucAccesoConsulta1.Tenencias = "STATUS TENENCIAS";
            // 
            // pnlRegistro
            // 
            pnlRegistro.Controls.Add(gbAcciones);
            pnlRegistro.Controls.Add(gbVinModelo);
            pnlRegistro.Controls.Add(gbVisitante);
            pnlRegistro.Dock = DockStyle.Fill;
            pnlRegistro.Location = new Point(3, 3);
            pnlRegistro.Name = "pnlRegistro";
            pnlRegistro.Size = new Size(386, 556);
            pnlRegistro.TabIndex = 4;
            // 
            // gbAcciones
            // 
            gbAcciones.BackColor = SystemColors.Window;
            gbAcciones.Dock = DockStyle.Fill;
            gbAcciones.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbAcciones.ForeColor = Color.Crimson;
            gbAcciones.Location = new Point(0, 419);
            gbAcciones.Margin = new Padding(5);
            gbAcciones.Name = "gbAcciones";
            gbAcciones.Padding = new Padding(10);
            gbAcciones.Size = new Size(386, 137);
            gbAcciones.TabIndex = 3;
            gbAcciones.TabStop = false;
            gbAcciones.Text = "ACCIONES";
            // 
            // gbVinModelo
            // 
            gbVinModelo.BackColor = SystemColors.Window;
            gbVinModelo.Controls.Add(ucVinModelo1);
            gbVinModelo.Dock = DockStyle.Top;
            gbVinModelo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbVinModelo.ForeColor = Color.Crimson;
            gbVinModelo.Location = new Point(0, 227);
            gbVinModelo.Margin = new Padding(5);
            gbVinModelo.Name = "gbVinModelo";
            gbVinModelo.Padding = new Padding(10);
            gbVinModelo.Size = new Size(386, 192);
            gbVinModelo.TabIndex = 2;
            gbVinModelo.TabStop = false;
            gbVinModelo.Text = "VIN / MODELO";
            // 
            // ucVinModelo1
            // 
            ucVinModelo1.Dock = DockStyle.Fill;
            ucVinModelo1.Location = new Point(10, 30);
            ucVinModelo1.Name = "ucVinModelo1";
            ucVinModelo1.Size = new Size(366, 152);
            ucVinModelo1.TabIndex = 0;
            // 
            // gbVisitante
            // 
            gbVisitante.BackColor = Color.White;
            gbVisitante.Controls.Add(ucVisitante1);
            gbVisitante.Dock = DockStyle.Top;
            gbVisitante.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            gbVisitante.ForeColor = Color.Crimson;
            gbVisitante.Location = new Point(0, 0);
            gbVisitante.Margin = new Padding(5);
            gbVisitante.Name = "gbVisitante";
            gbVisitante.Padding = new Padding(10);
            gbVisitante.Size = new Size(386, 227);
            gbVisitante.TabIndex = 0;
            gbVisitante.TabStop = false;
            gbVisitante.Text = "VISITANTE";
            // 
            // ucVisitante1
            // 
            ucVisitante1.Dock = DockStyle.Fill;
            ucVisitante1.Location = new Point(10, 30);
            ucVisitante1.Name = "ucVisitante1";
            ucVisitante1.Size = new Size(366, 187);
            ucVisitante1.TabIndex = 0;
            // 
            // tpTC
            // 
            tpTC.Controls.Add(tlpFila2);
            tpTC.Location = new Point(4, 34);
            tpTC.Name = "tpTC";
            tpTC.Padding = new Padding(3);
            tpTC.Size = new Size(1001, 568);
            tpTC.TabIndex = 1;
            tpTC.Text = "Tarjeta de Circulación";
            tpTC.UseVisualStyleBackColor = true;
            // 
            // tlpFila2
            // 
            tlpFila2.ColumnCount = 2;
            tlpFila2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.4572868F));
            tlpFila2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.5427132F));
            tlpFila2.Controls.Add(gbTarjetaCirculacion, 1, 0);
            tlpFila2.Controls.Add(gbSeleccionVehicular, 0, 0);
            tlpFila2.Dock = DockStyle.Fill;
            tlpFila2.Location = new Point(3, 3);
            tlpFila2.Name = "tlpFila2";
            tlpFila2.RowCount = 1;
            tlpFila2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpFila2.Size = new Size(995, 562);
            tlpFila2.TabIndex = 2;
            // 
            // gbTarjetaCirculacion
            // 
            gbTarjetaCirculacion.BackColor = SystemColors.Window;
            gbTarjetaCirculacion.Controls.Add(ucTarjetaCirculacion1);
            gbTarjetaCirculacion.Dock = DockStyle.Fill;
            gbTarjetaCirculacion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbTarjetaCirculacion.ForeColor = Color.Crimson;
            gbTarjetaCirculacion.Location = new Point(517, 5);
            gbTarjetaCirculacion.Margin = new Padding(5);
            gbTarjetaCirculacion.Name = "gbTarjetaCirculacion";
            gbTarjetaCirculacion.Padding = new Padding(10);
            gbTarjetaCirculacion.Size = new Size(473, 552);
            gbTarjetaCirculacion.TabIndex = 2;
            gbTarjetaCirculacion.TabStop = false;
            gbTarjetaCirculacion.Text = "TARJETA DE CIRCUALCIÓN";
            // 
            // ucTarjetaCirculacion1
            // 
            ucTarjetaCirculacion1.Dock = DockStyle.Fill;
            ucTarjetaCirculacion1.Location = new Point(10, 30);
            ucTarjetaCirculacion1.Name = "ucTarjetaCirculacion1";
            ucTarjetaCirculacion1.Size = new Size(453, 512);
            ucTarjetaCirculacion1.TabIndex = 0;
            // 
            // gbSeleccionVehicular
            // 
            gbSeleccionVehicular.BackColor = SystemColors.Window;
            gbSeleccionVehicular.Controls.Add(ucSeleccionVehiculo1);
            gbSeleccionVehicular.Dock = DockStyle.Fill;
            gbSeleccionVehicular.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbSeleccionVehicular.ForeColor = Color.Crimson;
            gbSeleccionVehicular.Location = new Point(5, 5);
            gbSeleccionVehicular.Margin = new Padding(5);
            gbSeleccionVehicular.Name = "gbSeleccionVehicular";
            gbSeleccionVehicular.Padding = new Padding(10);
            gbSeleccionVehicular.Size = new Size(502, 552);
            gbSeleccionVehicular.TabIndex = 1;
            gbSeleccionVehicular.TabStop = false;
            gbSeleccionVehicular.Text = "SELECCION DE VEHICULO";
            // 
            // ucSeleccionVehiculo1
            // 
            ucSeleccionVehiculo1.Dock = DockStyle.Fill;
            ucSeleccionVehiculo1.Location = new Point(10, 30);
            ucSeleccionVehiculo1.Name = "ucSeleccionVehiculo1";
            ucSeleccionVehiculo1.Size = new Size(482, 512);
            ucSeleccionVehiculo1.TabIndex = 0;
            // 
            // tpDocumentosAdicionales
            // 
            tpDocumentosAdicionales.Controls.Add(tlpFila3);
            tpDocumentosAdicionales.Location = new Point(4, 34);
            tpDocumentosAdicionales.Name = "tpDocumentosAdicionales";
            tpDocumentosAdicionales.Padding = new Padding(3);
            tpDocumentosAdicionales.Size = new Size(1001, 568);
            tpDocumentosAdicionales.TabIndex = 2;
            tpDocumentosAdicionales.Text = "Documentos Adicionales";
            tpDocumentosAdicionales.UseVisualStyleBackColor = true;
            // 
            // tlpFila3
            // 
            tlpFila3.ColumnCount = 2;
            tlpFila3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.9799F));
            tlpFila3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.0201F));
            tlpFila3.Controls.Add(gbEscaneoDocumentos, 1, 0);
            tlpFila3.Controls.Add(gbDocumentosAdicionales, 0, 0);
            tlpFila3.Dock = DockStyle.Fill;
            tlpFila3.Location = new Point(3, 3);
            tlpFila3.Name = "tlpFila3";
            tlpFila3.RowCount = 1;
            tlpFila3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFila3.Size = new Size(995, 562);
            tlpFila3.TabIndex = 3;
            // 
            // gbEscaneoDocumentos
            // 
            gbEscaneoDocumentos.BackColor = SystemColors.Window;
            gbEscaneoDocumentos.Controls.Add(ucEscaneoDocumentos1);
            gbEscaneoDocumentos.Dock = DockStyle.Fill;
            gbEscaneoDocumentos.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbEscaneoDocumentos.ForeColor = Color.Crimson;
            gbEscaneoDocumentos.Location = new Point(562, 5);
            gbEscaneoDocumentos.Margin = new Padding(5);
            gbEscaneoDocumentos.Name = "gbEscaneoDocumentos";
            gbEscaneoDocumentos.Padding = new Padding(10);
            gbEscaneoDocumentos.Size = new Size(428, 552);
            gbEscaneoDocumentos.TabIndex = 2;
            gbEscaneoDocumentos.TabStop = false;
            gbEscaneoDocumentos.Text = "ESCANEO DE DOCUMENTOS";
            // 
            // ucEscaneoDocumentos1
            // 
            ucEscaneoDocumentos1.Dock = DockStyle.Fill;
            ucEscaneoDocumentos1.Location = new Point(10, 30);
            ucEscaneoDocumentos1.Name = "ucEscaneoDocumentos1";
            ucEscaneoDocumentos1.Size = new Size(408, 512);
            ucEscaneoDocumentos1.TabIndex = 0;
            // 
            // gbDocumentosAdicionales
            // 
            gbDocumentosAdicionales.BackColor = SystemColors.Window;
            gbDocumentosAdicionales.Controls.Add(ucDocumentosAdicionales1);
            gbDocumentosAdicionales.Dock = DockStyle.Fill;
            gbDocumentosAdicionales.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic);
            gbDocumentosAdicionales.ForeColor = Color.Crimson;
            gbDocumentosAdicionales.Location = new Point(5, 5);
            gbDocumentosAdicionales.Margin = new Padding(5);
            gbDocumentosAdicionales.Name = "gbDocumentosAdicionales";
            gbDocumentosAdicionales.Padding = new Padding(10);
            gbDocumentosAdicionales.Size = new Size(547, 552);
            gbDocumentosAdicionales.TabIndex = 1;
            gbDocumentosAdicionales.TabStop = false;
            gbDocumentosAdicionales.Text = "DOCUMENTOS ADICIONALES";
            // 
            // ucDocumentosAdicionales1
            // 
            ucDocumentosAdicionales1.Dock = DockStyle.Fill;
            ucDocumentosAdicionales1.Location = new Point(10, 30);
            ucDocumentosAdicionales1.Name = "ucDocumentosAdicionales1";
            ucDocumentosAdicionales1.Size = new Size(527, 512);
            ucDocumentosAdicionales1.TabIndex = 0;
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
            tlpHeader.ColumnCount = 4;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpHeader.Controls.Add(tableLayoutPanel1, 2, 0);
            tlpHeader.Controls.Add(tlpPlacaHeader, 0, 0);
            tlpHeader.Controls.Add(tlpLinea, 1, 0);
            tlpHeader.Controls.Add(ipbLogo, 3, 0);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(0, 0);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 1;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHeader.Size = new Size(1009, 100);
            tlpHeader.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblPET, 0, 0);
            tableLayoutPanel1.Controls.Add(cbPET, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(507, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(246, 94);
            tableLayoutPanel1.TabIndex = 2;
            tableLayoutPanel1.TabStop = true;
            // 
            // lblPET
            // 
            lblPET.Dock = DockStyle.Fill;
            lblPET.Font = new Font("Segoe UI", 28F);
            lblPET.ForeColor = Color.Crimson;
            lblPET.Location = new Point(0, 0);
            lblPET.Margin = new Padding(0, 0, 10, 0);
            lblPET.Name = "lblPET";
            lblPET.Padding = new Padding(0, 0, 12, 0);
            lblPET.Size = new Size(113, 94);
            lblPET.TabIndex = 1;
            lblPET.Text = "PET";
            lblPET.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbPET
            // 
            cbPET.AutoSize = true;
            cbPET.Dock = DockStyle.Left;
            cbPET.ForeColor = Color.Black;
            cbPET.Location = new Point(126, 3);
            cbPET.Name = "cbPET";
            cbPET.Size = new Size(117, 88);
            cbPET.TabIndex = 2;
            cbPET.Text = "Prueba de Evaluación Técnica";
            cbPET.TextAlign = ContentAlignment.MiddleCenter;
            cbPET.UseVisualStyleBackColor = true;
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
            tlpPlacaHeader.Size = new Size(246, 94);
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
            lblPlaca.Size = new Size(117, 94);
            lblPlaca.TabIndex = 0;
            lblPlaca.Text = "Placa";
            lblPlaca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPlaca
            // 
            txtPlaca.BorderStyle = BorderStyle.FixedSingle;
            txtPlaca.CharacterCasing = CharacterCasing.Upper;
            txtPlaca.Dock = DockStyle.Fill;
            txtPlaca.Font = new Font("Segoe UI", 28F);
            txtPlaca.Location = new Point(123, 16);
            txtPlaca.Margin = new Padding(0, 16, 0, 16);
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(123, 57);
            txtPlaca.TabIndex = 1;
            txtPlaca.Text = "75F880";
            // 
            // tlpLinea
            // 
            tlpLinea.ColumnCount = 2;
            tlpLinea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLinea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLinea.Controls.Add(txtLinea, 1, 0);
            tlpLinea.Controls.Add(lblLinea, 0, 0);
            tlpLinea.Dock = DockStyle.Fill;
            tlpLinea.Location = new Point(255, 3);
            tlpLinea.Name = "tlpLinea";
            tlpLinea.RowCount = 1;
            tlpLinea.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLinea.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLinea.Size = new Size(246, 94);
            tlpLinea.TabIndex = 0;
            tlpLinea.TabStop = true;
            // 
            // txtLinea
            // 
            txtLinea.BorderStyle = BorderStyle.FixedSingle;
            txtLinea.Dock = DockStyle.Fill;
            txtLinea.Font = new Font("Segoe UI", 28F);
            txtLinea.Location = new Point(123, 16);
            txtLinea.Margin = new Padding(0, 16, 0, 16);
            txtLinea.Name = "txtLinea";
            txtLinea.Size = new Size(123, 57);
            txtLinea.TabIndex = 0;
            txtLinea.TabStop = false;
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
            lblLinea.Size = new Size(113, 94);
            lblLinea.TabIndex = 1;
            lblLinea.Text = "Línea";
            lblLinea.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ipbLogo
            // 
            ipbLogo.BackColor = Color.WhiteSmoke;
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
            // ucRegistroVehicular
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucRegistroVehicular";
            Size = new Size(1009, 706);
            pnlPrincipal.ResumeLayout(false);
            tcPrincipal.ResumeLayout(false);
            tpRegistroVehicular.ResumeLayout(false);
            tlpFila1.ResumeLayout(false);
            gbAcceso.ResumeLayout(false);
            pnlRegistro.ResumeLayout(false);
            gbVinModelo.ResumeLayout(false);
            gbVisitante.ResumeLayout(false);
            tpTC.ResumeLayout(false);
            tlpFila2.ResumeLayout(false);
            gbTarjetaCirculacion.ResumeLayout(false);
            gbSeleccionVehicular.ResumeLayout(false);
            tpDocumentosAdicionales.ResumeLayout(false);
            tlpFila3.ResumeLayout(false);
            gbEscaneoDocumentos.ResumeLayout(false);
            gbDocumentosAdicionales.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
        
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblPET;
        private CheckBox cbPET;
        private TabControl tcPrincipal;
        private TabPage tpRegistroVehicular;
        private TabPage tpTC;
        private TabPage tpDocumentosAdicionales;
        private TableLayoutPanel tlpFila1;
        private GroupBox gbAcciones;
        private GroupBox gbVinModelo;
        private GroupBox gbAcceso;
        private GroupBox gbVisitante;
        private TableLayoutPanel tlpFila2;
        private GroupBox gbTarjetaCirculacion;
        private GroupBox gbSeleccionVehicular;
        private TableLayoutPanel tlpFila3;
        private GroupBox gbEscaneoDocumentos;
        private GroupBox gbDocumentosAdicionales;
        private Panel pnlRegistro;
        private Complementos.ucAccesoConsulta ucAccesoConsulta1;
        private Complementos.ucVinModelo ucVinModelo1;
        private Complementos.ucVisitante ucVisitante1;
        private Complementos.ucSeleccionVehiculo ucSeleccionVehiculo1;
        private Complementos.ucTarjetaCirculacion ucTarjetaCirculacion1;
        private Complementos.ucEscaneoDocumentos ucEscaneoDocumentos1;
        private Complementos.ucDocumentosAdicionales ucDocumentosAdicionales1;
    }
}
