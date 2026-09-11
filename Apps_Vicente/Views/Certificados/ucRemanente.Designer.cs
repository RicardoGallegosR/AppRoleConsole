namespace Apps_Vicente.Views.Certificados {
    partial class ucRemanente {
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
            dataGridView1 = new DataGridView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblReporteAGenerar = new Label();
            btnGenerarReporte = new Button();
            llblAbrirArchivo = new LinkLabel();
            lblPrgreso = new Label();
            progressBar1 = new ProgressBar();
            pnlHeader = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblVerificentro = new Label();
            lblVigencia = new Label();
            txbVigencia = new TextBox();
            cbVerificentro = new ComboBox();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            pnlHeader.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(775, 458);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Font = new Font("Segoe UI", 14F);
            splitContainer1.ForeColor = Color.FromArgb(248, 250, 252);
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 100);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.ForeColor = Color.Black;
            splitContainer1.Panel1.Margin = new Padding(4);
            splitContainer1.Panel1.Padding = new Padding(6);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.AliceBlue;
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer1.Panel2.Margin = new Padding(4);
            splitContainer1.Panel2.Padding = new Padding(6);
            splitContainer1.Size = new Size(775, 358);
            splitContainer1.SplitterDistance = 389;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(248, 250, 252);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(6, 6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(377, 346);
            dataGridView1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblReporteAGenerar);
            flowLayoutPanel1.Controls.Add(btnGenerarReporte);
            flowLayoutPanel1.Controls.Add(llblAbrirArchivo);
            flowLayoutPanel1.Controls.Add(lblPrgreso);
            flowLayoutPanel1.Controls.Add(progressBar1);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(6, 6);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(373, 346);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblReporteAGenerar
            // 
            lblReporteAGenerar.Dock = DockStyle.Fill;
            lblReporteAGenerar.ForeColor = Color.Black;
            lblReporteAGenerar.Location = new Point(3, 0);
            lblReporteAGenerar.Name = "lblReporteAGenerar";
            lblReporteAGenerar.Size = new Size(367, 135);
            lblReporteAGenerar.TabIndex = 0;
            lblReporteAGenerar.Text = "Generar";
            lblReporteAGenerar.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnGenerarReporte
            // 
            btnGenerarReporte.AutoSize = true;
            btnGenerarReporte.BackColor = Color.FromArgb(22, 140, 74);
            btnGenerarReporte.Dock = DockStyle.Fill;
            btnGenerarReporte.FlatAppearance.BorderSize = 0;
            btnGenerarReporte.FlatStyle = FlatStyle.Flat;
            btnGenerarReporte.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarReporte.Location = new Point(3, 138);
            btnGenerarReporte.Name = "btnGenerarReporte";
            btnGenerarReporte.Size = new Size(367, 45);
            btnGenerarReporte.TabIndex = 1;
            btnGenerarReporte.Text = "Generar Reporte Excel";
            btnGenerarReporte.UseVisualStyleBackColor = false;
            btnGenerarReporte.Click += btnGenerarReporte_Click;
            // 
            // llblAbrirArchivo
            // 
            llblAbrirArchivo.Dock = DockStyle.Fill;
            llblAbrirArchivo.LinkColor = SystemColors.HotTrack;
            llblAbrirArchivo.Location = new Point(3, 186);
            llblAbrirArchivo.Name = "llblAbrirArchivo";
            llblAbrirArchivo.Size = new Size(367, 45);
            llblAbrirArchivo.TabIndex = 2;
            llblAbrirArchivo.TabStop = true;
            llblAbrirArchivo.Text = "Abrir Carpeta :D";
            llblAbrirArchivo.TextAlign = ContentAlignment.MiddleCenter;
            llblAbrirArchivo.LinkClicked += llblAbrirArchivo_LinkClicked;
            // 
            // lblPrgreso
            // 
            lblPrgreso.Dock = DockStyle.Fill;
            lblPrgreso.ForeColor = Color.Black;
            lblPrgreso.Location = new Point(3, 231);
            lblPrgreso.Name = "lblPrgreso";
            lblPrgreso.Size = new Size(367, 90);
            lblPrgreso.TabIndex = 4;
            lblPrgreso.Text = "Progreso del remanente ";
            lblPrgreso.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Fill;
            progressBar1.Location = new Point(376, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(0, 45);
            progressBar1.TabIndex = 3;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(230, 235, 241);
            pnlHeader.Controls.Add(tableLayoutPanel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(775, 100);
            pnlHeader.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblVerificentro, 1, 0);
            tableLayoutPanel1.Controls.Add(lblVigencia, 0, 0);
            tableLayoutPanel1.Controls.Add(txbVigencia, 0, 1);
            tableLayoutPanel1.Controls.Add(cbVerificentro, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(775, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblVerificentro
            // 
            lblVerificentro.AutoSize = true;
            lblVerificentro.Dock = DockStyle.Fill;
            lblVerificentro.Font = new Font("Segoe UI", 14F);
            lblVerificentro.Location = new Point(390, 0);
            lblVerificentro.Name = "lblVerificentro";
            lblVerificentro.Size = new Size(382, 50);
            lblVerificentro.TabIndex = 1;
            lblVerificentro.Text = "Verificentro";
            lblVerificentro.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVigencia
            // 
            lblVigencia.AutoSize = true;
            lblVigencia.Dock = DockStyle.Fill;
            lblVigencia.Font = new Font("Segoe UI", 14F);
            lblVigencia.Location = new Point(3, 0);
            lblVigencia.Name = "lblVigencia";
            lblVigencia.Size = new Size(381, 50);
            lblVigencia.TabIndex = 0;
            lblVigencia.Text = "Vigencia";
            lblVigencia.TextAlign = ContentAlignment.MiddleCenter;
            lblVigencia.Visible = false;
            // 
            // txbVigencia
            // 
            txbVigencia.Dock = DockStyle.Fill;
            txbVigencia.Font = new Font("Segoe UI", 14F);
            txbVigencia.Location = new Point(3, 53);
            txbVigencia.Name = "txbVigencia";
            txbVigencia.Size = new Size(381, 32);
            txbVigencia.TabIndex = 2;
            txbVigencia.Visible = false;
            // 
            // cbVerificentro
            // 
            cbVerificentro.Dock = DockStyle.Fill;
            cbVerificentro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVerificentro.Font = new Font("Segoe UI", 14F);
            cbVerificentro.FormattingEnabled = true;
            cbVerificentro.Location = new Point(390, 53);
            cbVerificentro.Name = "cbVerificentro";
            cbVerificentro.Size = new Size(382, 33);
            cbVerificentro.TabIndex = 3;
            // 
            // ucRemanente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucRemanente";
            Size = new Size(775, 458);
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            pnlHeader.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlHeader;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblVigencia;
        private Label lblVerificentro;
        private TextBox txbVigencia;
        private SplitContainer splitContainer1;
        private ComboBox cbVerificentro;
        private DataGridView dataGridView1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblReporteAGenerar;
        private Button btnGenerarReporte;
        private LinkLabel llblAbrirArchivo;
        private ProgressBar progressBar1;
        private Label lblPrgreso;
    }
}
