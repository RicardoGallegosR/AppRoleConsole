namespace Apps_Administrativa.Paneles.FisicoMecanica {
    partial class CargarCertificados {
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlPrincipal = new Panel();
            splitContainer1 = new SplitContainer();
            dgvRegistro = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnGuardarRegistro = new Button();
            btnEliminar = new Button();
            btnAgregar = new Button();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRegistro).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(953, 519);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvRegistro);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer1.Size = new Size(953, 519);
            splitContainer1.SplitterDistance = 841;
            splitContainer1.TabIndex = 0;
            // 
            // dgvRegistro
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvRegistro.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvRegistro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvRegistro.DefaultCellStyle = dataGridViewCellStyle2;
            dgvRegistro.Dock = DockStyle.Fill;
            dgvRegistro.Location = new Point(0, 0);
            dgvRegistro.Name = "dgvRegistro";
            dgvRegistro.Size = new Size(841, 519);
            dgvRegistro.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(242, 242, 242);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnGuardarRegistro, 0, 2);
            tableLayoutPanel1.Controls.Add(btnEliminar, 0, 1);
            tableLayoutPanel1.Controls.Add(btnAgregar, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(108, 519);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnGuardarRegistro
            // 
            btnGuardarRegistro.AutoSize = true;
            btnGuardarRegistro.BackColor = Color.FromArgb(46, 125, 50);
            btnGuardarRegistro.Dock = DockStyle.Fill;
            btnGuardarRegistro.FlatAppearance.BorderSize = 0;
            btnGuardarRegistro.FlatStyle = FlatStyle.Flat;
            btnGuardarRegistro.Font = new Font("Segoe UI", 14F);
            btnGuardarRegistro.ForeColor = Color.White;
            btnGuardarRegistro.Location = new Point(3, 209);
            btnGuardarRegistro.Name = "btnGuardarRegistro";
            btnGuardarRegistro.Size = new Size(102, 97);
            btnGuardarRegistro.TabIndex = 2;
            btnGuardarRegistro.Text = "Guardar";
            btnGuardarRegistro.UseVisualStyleBackColor = false;
            btnGuardarRegistro.Click += btnGuardarRegistro_Click;
            btnGuardarRegistro.MouseDown += btnGuardarRegistro_MouseDown;
            btnGuardarRegistro.MouseEnter += btnGuardarRegistro_MouseEnter;
            btnGuardarRegistro.MouseLeave += btnGuardarRegistro_MouseLeave;
            btnGuardarRegistro.MouseUp += btnGuardarRegistro_MouseUp;
            // 
            // btnEliminar
            // 
            btnEliminar.AutoSize = true;
            btnEliminar.BackColor = Color.FromArgb(190, 24, 60);
            btnEliminar.Dock = DockStyle.Fill;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 14F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(3, 106);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(102, 97);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            btnEliminar.MouseEnter += btnEliminar_MouseEnter;
            btnEliminar.MouseLeave += btnEliminar_MouseLeave;
            // 
            // btnAgregar
            // 
            btnAgregar.AutoSize = true;
            btnAgregar.BackColor = Color.FromArgb(31, 78, 121);
            btnAgregar.Dock = DockStyle.Fill;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 14F);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(3, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(102, 97);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            btnAgregar.MouseEnter += btnAgregar_MouseEnter;
            btnAgregar.MouseLeave += btnAgregar_MouseLeave;
            // 
            // CargarCertificados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "CargarCertificados";
            Size = new Size(953, 519);
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRegistro).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer splitContainer1;
        private DataGridView dgvRegistro;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnAgregar;
        private Button btnGuardarRegistro;
        private Button btnEliminar;
    }
}
