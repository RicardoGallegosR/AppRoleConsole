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
            txtPlaca = new TextBox();
            ibServicios = new FontAwesome.Sharp.IconButton();
            btnCrearVerificacion = new Button();
            pnlCitas = new Panel();
            pnlMargen = new Panel();
            lblInfCitas = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlTipoAccso = new Panel();
            comboBox1 = new ComboBox();
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
            pnlCitas.SuspendLayout();
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
            pnlPrincipal.Size = new Size(669, 351);
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
            scPrincipal.Panel2.Controls.Add(tableLayoutPanel1);
            scPrincipal.Size = new Size(669, 351);
            scPrincipal.SplitterDistance = 325;
            scPrincipal.SplitterWidth = 1;
            scPrincipal.TabIndex = 0;
            scPrincipal.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(flpPlacasServicios, 0, 0);
            tableLayoutPanel2.Controls.Add(pnlCitas, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel2.Size = new Size(325, 351);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // flpPlacasServicios
            // 
            flpPlacasServicios.Controls.Add(lblPlaca);
            flpPlacasServicios.Controls.Add(txtPlaca);
            flpPlacasServicios.Controls.Add(ibServicios);
            flpPlacasServicios.Controls.Add(btnCrearVerificacion);
            flpPlacasServicios.Dock = DockStyle.Fill;
            flpPlacasServicios.Location = new Point(3, 3);
            flpPlacasServicios.Name = "flpPlacasServicios";
            flpPlacasServicios.Size = new Size(319, 46);
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
            lblPlaca.Text = "Placa";
            // 
            // txtPlaca
            // 
            txtPlaca.Font = new Font("Segoe UI", 12F);
            txtPlaca.ForeColor = Color.FromArgb(45, 55, 65);
            txtPlaca.Location = new Point(55, 3);
            txtPlaca.MaxLength = 11;
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(100, 29);
            txtPlaca.TabIndex = 1;
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
            ibServicios.Location = new Point(161, 3);
            ibServicios.Name = "ibServicios";
            ibServicios.Size = new Size(37, 30);
            ibServicios.TabIndex = 2;
            ibServicios.UseVisualStyleBackColor = false;
            // 
            // btnCrearVerificacion
            // 
            btnCrearVerificacion.FlatAppearance.BorderColor = Color.Crimson;
            btnCrearVerificacion.FlatStyle = FlatStyle.Flat;
            btnCrearVerificacion.Font = new Font("Segoe UI", 12F);
            btnCrearVerificacion.Location = new Point(204, 3);
            btnCrearVerificacion.Name = "btnCrearVerificacion";
            btnCrearVerificacion.Size = new Size(57, 30);
            btnCrearVerificacion.TabIndex = 3;
            btnCrearVerificacion.Text = "Cont";
            btnCrearVerificacion.UseVisualStyleBackColor = true;
            // 
            // pnlCitas
            // 
            pnlCitas.Controls.Add(pnlMargen);
            pnlCitas.Controls.Add(lblInfCitas);
            pnlCitas.Dock = DockStyle.Fill;
            pnlCitas.Location = new Point(3, 55);
            pnlCitas.Name = "pnlCitas";
            pnlCitas.Size = new Size(319, 293);
            pnlCitas.TabIndex = 1;
            // 
            // pnlMargen
            // 
            pnlMargen.BackColor = Color.Crimson;
            pnlMargen.Dock = DockStyle.Top;
            pnlMargen.Location = new Point(0, 21);
            pnlMargen.Name = "pnlMargen";
            pnlMargen.Size = new Size(319, 2);
            pnlMargen.TabIndex = 1;
            // 
            // lblInfCitas
            // 
            lblInfCitas.Dock = DockStyle.Top;
            lblInfCitas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfCitas.ForeColor = Color.Black;
            lblInfCitas.Location = new Point(0, 0);
            lblInfCitas.Name = "lblInfCitas";
            lblInfCitas.Size = new Size(319, 21);
            lblInfCitas.TabIndex = 0;
            lblInfCitas.Text = "INFORMACIÓN DEL SERVICIO DE CITAS";
            lblInfCitas.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlTipoAccso, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlMonetarias, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlAmbientales, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlTenencias, 0, 3);
            tableLayoutPanel1.Controls.Add(pnlFotocivicas, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(343, 351);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTipoAccso
            // 
            pnlTipoAccso.Controls.Add(comboBox1);
            pnlTipoAccso.Controls.Add(lblTipoAcceso);
            pnlTipoAccso.Dock = DockStyle.Fill;
            pnlTipoAccso.Location = new Point(3, 3);
            pnlTipoAccso.Name = "pnlTipoAccso";
            pnlTipoAccso.Size = new Size(337, 64);
            pnlTipoAccso.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 11F);
            comboBox1.ForeColor = Color.FromArgb(45, 55, 65);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0, 20);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(337, 28);
            comboBox1.TabIndex = 1;
            // 
            // lblTipoAcceso
            // 
            lblTipoAcceso.Dock = DockStyle.Top;
            lblTipoAcceso.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTipoAcceso.ForeColor = Color.Black;
            lblTipoAcceso.Location = new Point(0, 0);
            lblTipoAcceso.Name = "lblTipoAcceso";
            lblTipoAcceso.Size = new Size(337, 20);
            lblTipoAcceso.TabIndex = 0;
            lblTipoAcceso.Text = "Tipo de acceso";
            lblTipoAcceso.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlMonetarias
            // 
            pnlMonetarias.Controls.Add(lblStatusMonetarias);
            pnlMonetarias.Controls.Add(lblMonetarias);
            pnlMonetarias.Dock = DockStyle.Fill;
            pnlMonetarias.Location = new Point(3, 73);
            pnlMonetarias.Name = "pnlMonetarias";
            pnlMonetarias.Size = new Size(337, 64);
            pnlMonetarias.TabIndex = 1;
            // 
            // lblStatusMonetarias
            // 
            lblStatusMonetarias.Dock = DockStyle.Fill;
            lblStatusMonetarias.Font = new Font("Segoe UI", 10.5F);
            lblStatusMonetarias.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusMonetarias.Location = new Point(0, 20);
            lblStatusMonetarias.Name = "lblStatusMonetarias";
            lblStatusMonetarias.Size = new Size(337, 44);
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
            lblMonetarias.Size = new Size(337, 20);
            lblMonetarias.TabIndex = 0;
            lblMonetarias.Text = "Infracciones Monetarias";
            lblMonetarias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlAmbientales
            // 
            pnlAmbientales.Controls.Add(lblStatusAmbientales);
            pnlAmbientales.Controls.Add(lblAmbientales);
            pnlAmbientales.Dock = DockStyle.Fill;
            pnlAmbientales.Location = new Point(3, 143);
            pnlAmbientales.Name = "pnlAmbientales";
            pnlAmbientales.Size = new Size(337, 64);
            pnlAmbientales.TabIndex = 2;
            // 
            // lblStatusAmbientales
            // 
            lblStatusAmbientales.Dock = DockStyle.Fill;
            lblStatusAmbientales.Font = new Font("Segoe UI", 10.5F);
            lblStatusAmbientales.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusAmbientales.Location = new Point(0, 20);
            lblStatusAmbientales.Name = "lblStatusAmbientales";
            lblStatusAmbientales.Size = new Size(337, 44);
            lblStatusAmbientales.TabIndex = 2;
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
            lblAmbientales.Size = new Size(337, 20);
            lblAmbientales.TabIndex = 0;
            lblAmbientales.Text = "Infracciones Ambientales";
            lblAmbientales.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTenencias
            // 
            pnlTenencias.Controls.Add(lblStatusTenencias);
            pnlTenencias.Controls.Add(lblTenencias);
            pnlTenencias.Dock = DockStyle.Fill;
            pnlTenencias.Location = new Point(3, 213);
            pnlTenencias.Name = "pnlTenencias";
            pnlTenencias.Size = new Size(337, 64);
            pnlTenencias.TabIndex = 3;
            // 
            // lblStatusTenencias
            // 
            lblStatusTenencias.Dock = DockStyle.Fill;
            lblStatusTenencias.Font = new Font("Segoe UI", 10.5F);
            lblStatusTenencias.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusTenencias.Location = new Point(0, 20);
            lblStatusTenencias.Name = "lblStatusTenencias";
            lblStatusTenencias.Size = new Size(337, 44);
            lblStatusTenencias.TabIndex = 3;
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
            lblTenencias.Size = new Size(337, 20);
            lblTenencias.TabIndex = 0;
            lblTenencias.Text = "Infracciones Tenencias";
            lblTenencias.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFotocivicas
            // 
            pnlFotocivicas.Controls.Add(lblStatusFotocivicas);
            pnlFotocivicas.Controls.Add(lblFotocivicas);
            pnlFotocivicas.Dock = DockStyle.Fill;
            pnlFotocivicas.Location = new Point(3, 283);
            pnlFotocivicas.Name = "pnlFotocivicas";
            pnlFotocivicas.Size = new Size(337, 65);
            pnlFotocivicas.TabIndex = 4;
            // 
            // lblStatusFotocivicas
            // 
            lblStatusFotocivicas.Dock = DockStyle.Fill;
            lblStatusFotocivicas.Font = new Font("Segoe UI", 10.5F);
            lblStatusFotocivicas.ForeColor = Color.FromArgb(45, 55, 65);
            lblStatusFotocivicas.Location = new Point(0, 20);
            lblStatusFotocivicas.Name = "lblStatusFotocivicas";
            lblStatusFotocivicas.Size = new Size(337, 45);
            lblStatusFotocivicas.TabIndex = 4;
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
            lblFotocivicas.Size = new Size(337, 20);
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
            Size = new Size(669, 351);
            pnlPrincipal.ResumeLayout(false);
            scPrincipal.Panel1.ResumeLayout(false);
            scPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scPrincipal).EndInit();
            scPrincipal.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            flpPlacasServicios.ResumeLayout(false);
            flpPlacasServicios.PerformLayout();
            pnlCitas.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
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
        private ComboBox comboBox1;
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
        private TextBox txtPlaca;
        private FontAwesome.Sharp.IconButton ibServicios;
        private Panel pnlCitas;
        private Panel pnlMargen;
        private Label lblInfCitas;
        private Button btnCrearVerificacion;
    }
}
