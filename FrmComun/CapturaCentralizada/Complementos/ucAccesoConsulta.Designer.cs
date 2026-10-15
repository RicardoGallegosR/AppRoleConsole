namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucAccesoConsulta {
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
            tableLayoutPanel2 = new TableLayoutPanel();
            flpPlacasServicios = new FlowLayoutPanel();
            lblPlaca = new Label();
            cbPET = new CheckBox();
            ibServicios = new FontAwesome.Sharp.IconButton();
            btnCrearVerificacion = new Button();
            scWeb = new SplitContainer();
            pnlCitas = new Panel();
            lblStatusCitas = new Label();
            pnlMargen = new Panel();
            lblInfCitas = new Label();
            pnlFM = new Panel();
            lblStatusFM = new Label();
            pnlMargen2 = new Panel();
            lblInfFM = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlTipoAccso = new Panel();
            cbMotivoAcceso = new ComboBox();
            lblTipoAcceso = new Label();
            pnlMonetarias = new Panel();
            lblStatusMonetarias = new Label();
            lblMonetarias = new Label();
            pnlAmbientales = new Panel();
            lblStatusAmbientales = new Label();
            lblAmbientales = new Label();
            pnlTenencias = new Panel();
            lblStatusTenencias = new Label();
            lblTenencias = new Label();
            pnlFotocivicas = new Panel();
            lblStatusFotocivicas = new Label();
            lblFotocivicas = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scPrincipal).BeginInit();
            scPrincipal.Panel1.SuspendLayout();
            scPrincipal.Panel2.SuspendLayout();
            scPrincipal.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flpPlacasServicios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scWeb).BeginInit();
            scWeb.Panel1.SuspendLayout();
            scWeb.Panel2.SuspendLayout();
            scWeb.SuspendLayout();
            pnlCitas.SuspendLayout();
            pnlFM.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlTipoAccso.SuspendLayout();
            pnlMonetarias.SuspendLayout();
            pnlAmbientales.SuspendLayout();
            pnlTenencias.SuspendLayout();
            pnlFotocivicas.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(scPrincipal);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(861, 327);
            pnlPrincipal.TabIndex = 0;
            // 
            // scPrincipal
            // 
            scPrincipal.Dock = DockStyle.Fill;
            scPrincipal.IsSplitterFixed = true;
            scPrincipal.Location = new Point(0, 0);
            scPrincipal.Name = "scPrincipal";
            // 
            // scPrincipal.Panel1
            // 
            scPrincipal.Panel1.Controls.Add(tableLayoutPanel2);
            // 
            // scPrincipal.Panel2
            // 
            scPrincipal.Panel2.AutoScroll = true;
            scPrincipal.Panel2.Controls.Add(tableLayoutPanel1);
            scPrincipal.Size = new Size(861, 327);
            scPrincipal.SplitterDistance = 417;
            scPrincipal.SplitterWidth = 1;
            scPrincipal.TabIndex = 0;
            scPrincipal.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(flpPlacasServicios, 0, 0);
            tableLayoutPanel2.Controls.Add(scWeb, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel2.Size = new Size(417, 327);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // flpPlacasServicios
            // 
            flpPlacasServicios.Controls.Add(lblPlaca);
            flpPlacasServicios.Controls.Add(cbPET);
            flpPlacasServicios.Controls.Add(ibServicios);
            flpPlacasServicios.Controls.Add(btnCrearVerificacion);
            flpPlacasServicios.Dock = DockStyle.Fill;
            flpPlacasServicios.Location = new Point(3, 3);
            flpPlacasServicios.Name = "flpPlacasServicios";
            flpPlacasServicios.Size = new Size(411, 43);
            flpPlacasServicios.TabIndex = 0;
            // 
            // lblPlaca
            // 
            lblPlaca.Font = new Font("Segoe UI", 12F);
            lblPlaca.ForeColor = Color.FromArgb(45, 55, 65);
            lblPlaca.Location = new Point(3, 0);
            lblPlaca.Name = "lblPlaca";
            lblPlaca.Size = new Size(46, 30);
            lblPlaca.TabIndex = 0;
            lblPlaca.Text = "PET";
            // 
            // cbPET
            // 
            cbPET.AutoSize = true;
            cbPET.Dock = DockStyle.Fill;
            cbPET.Location = new Point(55, 3);
            cbPET.Name = "cbPET";
            cbPET.Size = new Size(181, 30);
            cbPET.TabIndex = 5;
            cbPET.Text = "Prueba de Evaluación Técnica";
            cbPET.UseVisualStyleBackColor = true;
            // 
            // ibServicios
            // 
            ibServicios.BackColor = Color.Crimson;
            ibServicios.FlatAppearance.BorderSize = 0;
            ibServicios.FlatStyle = FlatStyle.Flat;
            ibServicios.IconChar = FontAwesome.Sharp.IconChar.Search;
            ibServicios.IconColor = Color.White;
            ibServicios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibServicios.IconSize = 30;
            ibServicios.Location = new Point(242, 3);
            ibServicios.Name = "ibServicios";
            ibServicios.Size = new Size(37, 30);
            ibServicios.TabIndex = 2;
            ibServicios.UseVisualStyleBackColor = false;
            ibServicios.Click += ibServicios_Click;
            // 
            // btnCrearVerificacion
            // 
            btnCrearVerificacion.FlatAppearance.BorderColor = Color.Crimson;
            btnCrearVerificacion.FlatStyle = FlatStyle.Flat;
            btnCrearVerificacion.Font = new Font("Segoe UI", 12F);
            btnCrearVerificacion.Location = new Point(285, 3);
            btnCrearVerificacion.Name = "btnCrearVerificacion";
            btnCrearVerificacion.Size = new Size(89, 30);
            btnCrearVerificacion.TabIndex = 3;
            btnCrearVerificacion.Text = "Continuar";
            btnCrearVerificacion.UseVisualStyleBackColor = true;
            // 
            // scWeb
            // 
            scWeb.Dock = DockStyle.Fill;
            scWeb.Location = new Point(3, 52);
            scWeb.Name = "scWeb";
            scWeb.Orientation = Orientation.Horizontal;
            // 
            // scWeb.Panel1
            // 
            scWeb.Panel1.Controls.Add(pnlCitas);
            // 
            // scWeb.Panel2
            // 
            scWeb.Panel2.Controls.Add(pnlFM);
            scWeb.Size = new Size(411, 272);
            scWeb.SplitterDistance = 98;
            scWeb.SplitterWidth = 1;
            scWeb.TabIndex = 0;
            scWeb.TabStop = false;
            // 
            // pnlCitas
            // 
            pnlCitas.Controls.Add(lblStatusCitas);
            pnlCitas.Controls.Add(pnlMargen);
            pnlCitas.Controls.Add(lblInfCitas);
            pnlCitas.Dock = DockStyle.Fill;
            pnlCitas.Location = new Point(0, 0);
            pnlCitas.Name = "pnlCitas";
            pnlCitas.Size = new Size(411, 98);
            pnlCitas.TabIndex = 1;
            // 
            // lblStatusCitas
            // 
            lblStatusCitas.Dock = DockStyle.Fill;
            lblStatusCitas.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblStatusCitas.ForeColor = Color.Black;
            lblStatusCitas.Location = new Point(0, 23);
            lblStatusCitas.Name = "lblStatusCitas";
            lblStatusCitas.Size = new Size(411, 75);
            lblStatusCitas.TabIndex = 2;
            lblStatusCitas.Text = ".";
            // 
            // pnlMargen
            // 
            pnlMargen.BackColor = Color.Crimson;
            pnlMargen.Dock = DockStyle.Top;
            pnlMargen.Location = new Point(0, 21);
            pnlMargen.Name = "pnlMargen";
            pnlMargen.Size = new Size(411, 2);
            pnlMargen.TabIndex = 1;
            // 
            // lblInfCitas
            // 
            lblInfCitas.Dock = DockStyle.Top;
            lblInfCitas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfCitas.ForeColor = Color.Black;
            lblInfCitas.Location = new Point(0, 0);
            lblInfCitas.Name = "lblInfCitas";
            lblInfCitas.Size = new Size(411, 21);
            lblInfCitas.TabIndex = 0;
            lblInfCitas.Text = "INFORMACIÓN DEL SERVICIO DE CITAS";
            lblInfCitas.TextAlign = ContentAlignment.TopCenter;
            // 
            // pnlFM
            // 
            pnlFM.Controls.Add(lblStatusFM);
            pnlFM.Controls.Add(pnlMargen2);
            pnlFM.Controls.Add(lblInfFM);
            pnlFM.Dock = DockStyle.Fill;
            pnlFM.Location = new Point(0, 0);
            pnlFM.Name = "pnlFM";
            pnlFM.Size = new Size(411, 173);
            pnlFM.TabIndex = 0;
            // 
            // lblStatusFM
            // 
            lblStatusFM.Dock = DockStyle.Fill;
            lblStatusFM.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblStatusFM.ForeColor = Color.Black;
            lblStatusFM.Location = new Point(0, 43);
            lblStatusFM.Name = "lblStatusFM";
            lblStatusFM.Size = new Size(411, 130);
            lblStatusFM.TabIndex = 3;
            lblStatusFM.Text = ".";
            // 
            // pnlMargen2
            // 
            pnlMargen2.BackColor = Color.Crimson;
            pnlMargen2.Dock = DockStyle.Top;
            pnlMargen2.Location = new Point(0, 41);
            pnlMargen2.Name = "pnlMargen2";
            pnlMargen2.Size = new Size(411, 2);
            pnlMargen2.TabIndex = 2;
            // 
            // lblInfFM
            // 
            lblInfFM.Dock = DockStyle.Top;
            lblInfFM.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfFM.ForeColor = Color.Black;
            lblInfFM.Location = new Point(0, 0);
            lblInfFM.Name = "lblInfFM";
            lblInfFM.Size = new Size(411, 41);
            lblInfFM.TabIndex = 1;
            lblInfFM.Text = "INFORMACIÓN DEL SERVICIO DE\r\n FISICO MECANICA\r\n";
            lblInfFM.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlTipoAccso, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlMonetarias, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlAmbientales, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlTenencias, 0, 3);
            tableLayoutPanel1.Controls.Add(pnlFotocivicas, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(443, 327);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTipoAccso
            // 
            pnlTipoAccso.Controls.Add(cbMotivoAcceso);
            pnlTipoAccso.Controls.Add(lblTipoAcceso);
            pnlTipoAccso.Dock = DockStyle.Fill;
            pnlTipoAccso.Location = new Point(3, 3);
            pnlTipoAccso.Name = "pnlTipoAccso";
            pnlTipoAccso.Size = new Size(437, 54);
            pnlTipoAccso.TabIndex = 0;
            // 
            // cbMotivoAcceso
            // 
            cbMotivoAcceso.Dock = DockStyle.Fill;
            cbMotivoAcceso.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMotivoAcceso.Font = new Font("Segoe UI", 11F);
            cbMotivoAcceso.ForeColor = Color.FromArgb(45, 55, 65);
            cbMotivoAcceso.FormattingEnabled = true;
            cbMotivoAcceso.Location = new Point(0, 20);
            cbMotivoAcceso.Name = "cbMotivoAcceso";
            cbMotivoAcceso.Size = new Size(437, 28);
            cbMotivoAcceso.TabIndex = 1;
            cbMotivoAcceso.SelectionChangeCommitted += cbMotivoAcceso_SelectionChangeCommitted;
            // 
            // lblTipoAcceso
            // 
            lblTipoAcceso.Dock = DockStyle.Top;
            lblTipoAcceso.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTipoAcceso.ForeColor = Color.Black;
            lblTipoAcceso.Location = new Point(0, 0);
            lblTipoAcceso.Name = "lblTipoAcceso";
            lblTipoAcceso.Size = new Size(437, 20);
            lblTipoAcceso.TabIndex = 0;
            lblTipoAcceso.Text = "Tipo de acceso";
            lblTipoAcceso.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlMonetarias
            // 
            pnlMonetarias.Controls.Add(lblStatusMonetarias);
            pnlMonetarias.Controls.Add(lblMonetarias);
            pnlMonetarias.Dock = DockStyle.Fill;
            pnlMonetarias.Location = new Point(3, 63);
            pnlMonetarias.Name = "pnlMonetarias";
            pnlMonetarias.Size = new Size(437, 59);
            pnlMonetarias.TabIndex = 1;
            // 
            // lblStatusMonetarias
            // 
            lblStatusMonetarias.Dock = DockStyle.Top;
            lblStatusMonetarias.Font = new Font("Segoe UI", 10.5F);
            lblStatusMonetarias.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusMonetarias.Location = new Point(0, 20);
            lblStatusMonetarias.Name = "lblStatusMonetarias";
            lblStatusMonetarias.Size = new Size(437, 39);
            lblStatusMonetarias.TabIndex = 1;
            lblStatusMonetarias.Text = "STATUS MONETARIAS\r\n";
            lblStatusMonetarias.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblMonetarias
            // 
            lblMonetarias.Dock = DockStyle.Top;
            lblMonetarias.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMonetarias.ForeColor = Color.Black;
            lblMonetarias.Location = new Point(0, 0);
            lblMonetarias.Name = "lblMonetarias";
            lblMonetarias.Size = new Size(437, 20);
            lblMonetarias.TabIndex = 0;
            lblMonetarias.Text = "Infracciones Monetarias";
            lblMonetarias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlAmbientales
            // 
            pnlAmbientales.Controls.Add(lblStatusAmbientales);
            pnlAmbientales.Controls.Add(lblAmbientales);
            pnlAmbientales.Dock = DockStyle.Fill;
            pnlAmbientales.Location = new Point(3, 128);
            pnlAmbientales.Name = "pnlAmbientales";
            pnlAmbientales.Size = new Size(437, 59);
            pnlAmbientales.TabIndex = 2;
            // 
            // lblStatusAmbientales
            // 
            lblStatusAmbientales.Dock = DockStyle.Top;
            lblStatusAmbientales.Font = new Font("Segoe UI", 10.5F);
            lblStatusAmbientales.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusAmbientales.Location = new Point(0, 20);
            lblStatusAmbientales.Name = "lblStatusAmbientales";
            lblStatusAmbientales.Size = new Size(437, 39);
            lblStatusAmbientales.TabIndex = 0;
            lblStatusAmbientales.Text = "STATUS AMBIENTALES";
            lblStatusAmbientales.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblAmbientales
            // 
            lblAmbientales.Dock = DockStyle.Top;
            lblAmbientales.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAmbientales.ForeColor = Color.Black;
            lblAmbientales.Location = new Point(0, 0);
            lblAmbientales.Name = "lblAmbientales";
            lblAmbientales.Size = new Size(437, 20);
            lblAmbientales.TabIndex = 0;
            lblAmbientales.Text = "Infracciones Ambientales";
            lblAmbientales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTenencias
            // 
            pnlTenencias.AutoSize = true;
            pnlTenencias.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlTenencias.Controls.Add(lblStatusTenencias);
            pnlTenencias.Controls.Add(lblTenencias);
            pnlTenencias.Dock = DockStyle.Fill;
            pnlTenencias.Location = new Point(3, 193);
            pnlTenencias.Name = "pnlTenencias";
            pnlTenencias.Size = new Size(437, 39);
            pnlTenencias.TabIndex = 0;
            // 
            // lblStatusTenencias
            // 
            lblStatusTenencias.Dock = DockStyle.Top;
            lblStatusTenencias.Font = new Font("Segoe UI", 10.5F);
            lblStatusTenencias.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusTenencias.Location = new Point(0, 20);
            lblStatusTenencias.Name = "lblStatusTenencias";
            lblStatusTenencias.Size = new Size(437, 19);
            lblStatusTenencias.TabIndex = 0;
            lblStatusTenencias.Text = "STATUS TENENCIAS";
            lblStatusTenencias.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTenencias
            // 
            lblTenencias.Dock = DockStyle.Top;
            lblTenencias.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenencias.ForeColor = Color.Black;
            lblTenencias.Location = new Point(0, 0);
            lblTenencias.Name = "lblTenencias";
            lblTenencias.Size = new Size(437, 20);
            lblTenencias.TabIndex = 0;
            lblTenencias.Text = "Infracciones Tenencias";
            lblTenencias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFotocivicas
            // 
            pnlFotocivicas.Controls.Add(lblStatusFotocivicas);
            pnlFotocivicas.Controls.Add(lblFotocivicas);
            pnlFotocivicas.Dock = DockStyle.Fill;
            pnlFotocivicas.Location = new Point(3, 238);
            pnlFotocivicas.Name = "pnlFotocivicas";
            pnlFotocivicas.Size = new Size(437, 86);
            pnlFotocivicas.TabIndex = 4;
            // 
            // lblStatusFotocivicas
            // 
            lblStatusFotocivicas.Dock = DockStyle.Top;
            lblStatusFotocivicas.Font = new Font("Segoe UI", 10.5F);
            lblStatusFotocivicas.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusFotocivicas.Location = new Point(0, 20);
            lblStatusFotocivicas.Name = "lblStatusFotocivicas";
            lblStatusFotocivicas.Size = new Size(437, 45);
            lblStatusFotocivicas.TabIndex = 0;
            lblStatusFotocivicas.Text = "STATUS FOTOCIIVCAS";
            lblStatusFotocivicas.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblFotocivicas
            // 
            lblFotocivicas.Dock = DockStyle.Top;
            lblFotocivicas.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFotocivicas.ForeColor = Color.Black;
            lblFotocivicas.Location = new Point(0, 0);
            lblFotocivicas.Name = "lblFotocivicas";
            lblFotocivicas.Size = new Size(437, 20);
            lblFotocivicas.TabIndex = 0;
            lblFotocivicas.Text = "Infracciones Fotocivicas ";
            lblFotocivicas.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ucAccesoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucAccesoConsulta";
            Size = new Size(861, 327);
            pnlPrincipal.ResumeLayout(false);
            scPrincipal.Panel1.ResumeLayout(false);
            scPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scPrincipal).EndInit();
            scPrincipal.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            flpPlacasServicios.ResumeLayout(false);
            flpPlacasServicios.PerformLayout();
            scWeb.Panel1.ResumeLayout(false);
            scWeb.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scWeb).EndInit();
            scWeb.ResumeLayout(false);
            pnlCitas.ResumeLayout(false);
            pnlFM.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pnlTipoAccso.ResumeLayout(false);
            pnlMonetarias.ResumeLayout(false);
            pnlAmbientales.ResumeLayout(false);
            pnlTenencias.ResumeLayout(false);
            pnlFotocivicas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer scPrincipal;
        private Label lblTipoAcceso;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlTipoAccso;
        private ComboBox cbMotivoAcceso;
        private Label lblTenencias;
        private Label lblAmbientales;
        private Label lblMonetarias;
        private Label lblFotocivicas;
        private Panel pnlMonetarias;
        private Label lblStatusMonetarias;
        private Panel pnlAmbientales;
        private Label lblStatusAmbientales;
        private Label lblStatusFotocivicas;
        private Panel pnlTenencias;
        private Label lblStatusTenencias;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel pnlFotocivicas;
        private FlowLayoutPanel flpPlacasServicios;
        private Label lblPlaca;
        private FontAwesome.Sharp.IconButton ibServicios;
        private Panel pnlCitas;
        private Panel pnlMargen;
        private Label lblInfCitas;
        private Button btnCrearVerificacion;
        private Label lblStatusCitas;
        private SplitContainer scWeb;
        private Panel pnlFM;
        private Label lblInfFM;
        private Label lblStatusFM;
        private Panel pnlMargen2;
        private CheckBox cbPET;
    }
}
