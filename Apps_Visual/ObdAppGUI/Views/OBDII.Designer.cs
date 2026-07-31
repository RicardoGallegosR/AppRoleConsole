namespace Apps_Visual.ObdAppGUI.Views {
    partial class OBDII {
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
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlResumen = new Panel();
            lblReporte = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            _lblDTCPendientes = new Label();
            _lblDTCConfirmados = new Label();
            _lblProtocoloOBD = new Label();
            lblProtocoloOBD = new Label();
            lblDTCPendientes = new Label();
            lblDTCConfirmados = new Label();
            _lblModelo = new Label();
            lblModelo = new Label();
            _lblSubMarca = new Label();
            lblSubMarca = new Label();
            _lblMarca = new Label();
            lblMarca = new Label();
            lblResumen = new Label();
            pnlTiempoReal = new Panel();
            pnlRPM = new Panel();
            _lblValorRpm = new Label();
            pnlBateria = new Panel();
            _lblValorBateria = new Label();
            pnlFoother = new Panel();
            pbLecturaObd = new ProgressBar();
            pnlHeader = new Panel();
            splitContainer1 = new SplitContainer();
            lblLecturaOBD = new Label();
            btnConectar = new Button();
            pnlPrincipal.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlResumen.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlRPM.SuspendLayout();
            pnlBateria.SuspendLayout();
            pnlFoother.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(lblReporte);
            pnlPrincipal.Controls.Add(tableLayoutPanel1);
            pnlPrincipal.Controls.Add(pnlFoother);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(863, 531);
            pnlPrincipal.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pnlResumen, 1, 1);
            tableLayoutPanel1.Controls.Add(pnlTiempoReal, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlRPM, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlBateria, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 214);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(863, 292);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Visible = false;
            // 
            // pnlResumen
            // 
            pnlResumen.BackColor = Color.White;
            pnlResumen.Controls.Add(tableLayoutPanel2);
            pnlResumen.Controls.Add(lblResumen);
            pnlResumen.Dock = DockStyle.Fill;
            pnlResumen.Location = new Point(434, 149);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new Size(426, 140);
            pnlResumen.TabIndex = 0;
            pnlResumen.Visible = false;
            // 
            // lblReporte
            // 
            lblReporte.Dock = DockStyle.Fill;
            lblReporte.Font = new Font("Segoe UI", 20F);
            lblReporte.Location = new Point(0, 100);
            lblReporte.Name = "lblReporte";
            lblReporte.Size = new Size(863, 114);
            lblReporte.TabIndex = 0;
            lblReporte.Text = "Conecte el escaner SBD en el vehículo.\r\nUna vez conectado presiona el botón conectar. :D\r\n";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(_lblDTCPendientes, 1, 4);
            tableLayoutPanel2.Controls.Add(_lblDTCConfirmados, 1, 3);
            tableLayoutPanel2.Controls.Add(_lblProtocoloOBD, 1, 5);
            tableLayoutPanel2.Controls.Add(lblProtocoloOBD, 0, 5);
            tableLayoutPanel2.Controls.Add(lblDTCPendientes, 0, 4);
            tableLayoutPanel2.Controls.Add(lblDTCConfirmados, 0, 3);
            tableLayoutPanel2.Controls.Add(_lblModelo, 1, 2);
            tableLayoutPanel2.Controls.Add(lblModelo, 0, 2);
            tableLayoutPanel2.Controls.Add(_lblSubMarca, 1, 1);
            tableLayoutPanel2.Controls.Add(lblSubMarca, 0, 1);
            tableLayoutPanel2.Controls.Add(_lblMarca, 1, 0);
            tableLayoutPanel2.Controls.Add(lblMarca, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 21);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(426, 119);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // _lblDTCPendientes
            // 
            _lblDTCPendientes.AutoSize = true;
            _lblDTCPendientes.Dock = DockStyle.Fill;
            _lblDTCPendientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblDTCPendientes.Location = new Point(216, 76);
            _lblDTCPendientes.Name = "_lblDTCPendientes";
            _lblDTCPendientes.Size = new Size(207, 19);
            _lblDTCPendientes.TabIndex = 13;
            _lblDTCPendientes.Text = "DESCONOCIDO";
            _lblDTCPendientes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _lblDTCConfirmados
            // 
            _lblDTCConfirmados.AutoSize = true;
            _lblDTCConfirmados.Dock = DockStyle.Fill;
            _lblDTCConfirmados.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblDTCConfirmados.Location = new Point(216, 57);
            _lblDTCConfirmados.Name = "_lblDTCConfirmados";
            _lblDTCConfirmados.Size = new Size(207, 19);
            _lblDTCConfirmados.TabIndex = 12;
            _lblDTCConfirmados.Text = "DESCONOCIDO";
            _lblDTCConfirmados.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _lblProtocoloOBD
            // 
            _lblProtocoloOBD.AutoSize = true;
            _lblProtocoloOBD.Dock = DockStyle.Fill;
            _lblProtocoloOBD.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblProtocoloOBD.Location = new Point(216, 95);
            _lblProtocoloOBD.Name = "_lblProtocoloOBD";
            _lblProtocoloOBD.Size = new Size(207, 24);
            _lblProtocoloOBD.TabIndex = 11;
            _lblProtocoloOBD.Text = "DESCONOCIDO";
            _lblProtocoloOBD.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProtocoloOBD
            // 
            lblProtocoloOBD.AutoSize = true;
            lblProtocoloOBD.Dock = DockStyle.Fill;
            lblProtocoloOBD.Font = new Font("Segoe UI", 12F);
            lblProtocoloOBD.Location = new Point(3, 95);
            lblProtocoloOBD.Name = "lblProtocoloOBD";
            lblProtocoloOBD.Size = new Size(207, 24);
            lblProtocoloOBD.TabIndex = 10;
            lblProtocoloOBD.Text = "Protocolo OBD";
            lblProtocoloOBD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDTCPendientes
            // 
            lblDTCPendientes.AutoSize = true;
            lblDTCPendientes.Dock = DockStyle.Fill;
            lblDTCPendientes.Font = new Font("Segoe UI", 12F);
            lblDTCPendientes.Location = new Point(3, 76);
            lblDTCPendientes.Name = "lblDTCPendientes";
            lblDTCPendientes.Size = new Size(207, 19);
            lblDTCPendientes.TabIndex = 8;
            lblDTCPendientes.Text = "DTC's Pendientes";
            lblDTCPendientes.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDTCConfirmados
            // 
            lblDTCConfirmados.AutoSize = true;
            lblDTCConfirmados.Dock = DockStyle.Fill;
            lblDTCConfirmados.Font = new Font("Segoe UI", 12F);
            lblDTCConfirmados.Location = new Point(3, 57);
            lblDTCConfirmados.Name = "lblDTCConfirmados";
            lblDTCConfirmados.Size = new Size(207, 19);
            lblDTCConfirmados.TabIndex = 6;
            lblDTCConfirmados.Text = "DTC's Confirmados";
            lblDTCConfirmados.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblModelo
            // 
            _lblModelo.AutoSize = true;
            _lblModelo.Dock = DockStyle.Fill;
            _lblModelo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblModelo.Location = new Point(216, 38);
            _lblModelo.Name = "_lblModelo";
            _lblModelo.Size = new Size(207, 19);
            _lblModelo.TabIndex = 5;
            _lblModelo.Text = "DESCONOCIDO";
            _lblModelo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 12F);
            lblModelo.Location = new Point(3, 38);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(207, 19);
            lblModelo.TabIndex = 4;
            lblModelo.Text = "Modelo";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblSubMarca
            // 
            _lblSubMarca.AutoSize = true;
            _lblSubMarca.Dock = DockStyle.Fill;
            _lblSubMarca.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblSubMarca.Location = new Point(216, 19);
            _lblSubMarca.Name = "_lblSubMarca";
            _lblSubMarca.Size = new Size(207, 19);
            _lblSubMarca.TabIndex = 3;
            _lblSubMarca.Text = "DESCONOCIDO";
            _lblSubMarca.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubMarca
            // 
            lblSubMarca.AutoSize = true;
            lblSubMarca.Dock = DockStyle.Fill;
            lblSubMarca.Font = new Font("Segoe UI", 12F);
            lblSubMarca.Location = new Point(3, 19);
            lblSubMarca.Name = "lblSubMarca";
            lblSubMarca.Size = new Size(207, 19);
            lblSubMarca.TabIndex = 2;
            lblSubMarca.Text = "SubMarca";
            lblSubMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblMarca
            // 
            _lblMarca.AutoSize = true;
            _lblMarca.Dock = DockStyle.Fill;
            _lblMarca.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblMarca.Location = new Point(216, 0);
            _lblMarca.Name = "_lblMarca";
            _lblMarca.Size = new Size(207, 19);
            _lblMarca.TabIndex = 1;
            _lblMarca.Text = "DESCONOCIDO";
            _lblMarca.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 12F);
            lblMarca.Location = new Point(3, 0);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(207, 19);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "Marca";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblResumen
            // 
            lblResumen.Dock = DockStyle.Top;
            lblResumen.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblResumen.Location = new Point(0, 0);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(426, 21);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "RESUMEN OBDII";
            lblResumen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTiempoReal
            // 
            pnlTiempoReal.BackColor = Color.White;
            pnlTiempoReal.Dock = DockStyle.Fill;
            pnlTiempoReal.Location = new Point(3, 149);
            pnlTiempoReal.Name = "pnlTiempoReal";
            pnlTiempoReal.Size = new Size(425, 140);
            pnlTiempoReal.TabIndex = 1;
            // 
            // pnlRPM
            // 
            pnlRPM.BackColor = Color.White;
            pnlRPM.Controls.Add(_lblValorRpm);
            pnlRPM.Dock = DockStyle.Fill;
            pnlRPM.Location = new Point(3, 3);
            pnlRPM.Name = "pnlRPM";
            pnlRPM.Size = new Size(425, 140);
            pnlRPM.TabIndex = 2;
            // 
            // _lblValorRpm
            // 
            _lblValorRpm.Dock = DockStyle.Top;
            _lblValorRpm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            _lblValorRpm.Location = new Point(0, 0);
            _lblValorRpm.Name = "_lblValorRpm";
            _lblValorRpm.Size = new Size(425, 23);
            _lblValorRpm.TabIndex = 0;
            _lblValorRpm.Text = "RPM";
            _lblValorRpm.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBateria
            // 
            pnlBateria.BackColor = Color.White;
            pnlBateria.Controls.Add(_lblValorBateria);
            pnlBateria.Dock = DockStyle.Fill;
            pnlBateria.ForeColor = SystemColors.Control;
            pnlBateria.Location = new Point(434, 3);
            pnlBateria.Name = "pnlBateria";
            pnlBateria.Size = new Size(426, 140);
            pnlBateria.TabIndex = 3;
            // 
            // _lblValorBateria
            // 
            _lblValorBateria.Dock = DockStyle.Top;
            _lblValorBateria.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            _lblValorBateria.ForeColor = SystemColors.ControlText;
            _lblValorBateria.Location = new Point(0, 0);
            _lblValorBateria.Name = "_lblValorBateria";
            _lblValorBateria.Size = new Size(426, 21);
            _lblValorBateria.TabIndex = 0;
            _lblValorBateria.Text = "Bateria";
            _lblValorBateria.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFoother
            // 
            pnlFoother.Controls.Add(pbLecturaObd);
            pnlFoother.Dock = DockStyle.Bottom;
            pnlFoother.Location = new Point(0, 506);
            pnlFoother.Name = "pnlFoother";
            pnlFoother.Size = new Size(863, 25);
            pnlFoother.TabIndex = 3;
            // 
            // pbLecturaObd
            // 
            pbLecturaObd.Dock = DockStyle.Bottom;
            pbLecturaObd.Location = new Point(0, 2);
            pbLecturaObd.Name = "pbLecturaObd";
            pbLecturaObd.Size = new Size(863, 23);
            pbLecturaObd.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(splitContainer1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(863, 100);
            pnlHeader.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lblLecturaOBD);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnConectar);
            splitContainer1.Size = new Size(863, 100);
            splitContainer1.SplitterDistance = 692;
            splitContainer1.TabIndex = 0;
            // 
            // lblLecturaOBD
            // 
            lblLecturaOBD.BackColor = Color.White;
            lblLecturaOBD.Dock = DockStyle.Fill;
            lblLecturaOBD.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblLecturaOBD.Location = new Point(0, 0);
            lblLecturaOBD.Name = "lblLecturaOBD";
            lblLecturaOBD.Size = new Size(692, 100);
            lblLecturaOBD.TabIndex = 0;
            lblLecturaOBD.Text = "Diagnóstico OBD";
            lblLecturaOBD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConectar
            // 
            btnConectar.Dock = DockStyle.Fill;
            btnConectar.FlatStyle = FlatStyle.Flat;
            btnConectar.Font = new Font("Segoe UI", 20.25F);
            btnConectar.ForeColor = Color.Crimson;
            btnConectar.Location = new Point(0, 0);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(167, 100);
            btnConectar.TabIndex = 0;
            btnConectar.Text = "C O N E C T A R";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // OBDII
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "OBDII";
            Size = new Size(863, 531);
            pnlPrincipal.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            pnlResumen.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            pnlRPM.ResumeLayout(false);
            pnlBateria.ResumeLayout(false);
            pnlFoother.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlHeader;
        private SplitContainer splitContainer1;
        private Label lblLecturaOBD;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlFoother;
        private Panel pnlResumen;
        private Label lblResumen;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblMarca;
        private Label _lblMarca;
        private Label _lblProtocoloOBD;
        private Label lblProtocoloOBD;
        private Label _lblDTCPendientes;
        private Label lblDTCPendientes;
        private Label lblDTCConfirmados;
        private Label _lblDTCConfirmados;
        private Label lblModelo;
        private Label _lblModelo;
        private Label lblSubMarca;
        private Label _lblSubMarca;
        private Panel pnlTiempoReal;
        private Panel pnlRPM;
        private Panel pnlBateria;
        private Label lblReporte;
        private ProgressBar pbLecturaObd;
        public Button btnConectar;
        private Label _lblValorBateria;
        private Label _lblValorRpm;
    }
}
