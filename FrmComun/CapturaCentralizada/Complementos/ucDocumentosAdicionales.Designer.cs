namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucDocumentosAdicionales {
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
            flpVisita = new FlowLayoutPanel();
            btnAgregar = new Button();
            btnEliminar = new Button();
            dgvDocumentos = new DataGridView();
            pnlPrincipal.SuspendLayout();
            flpVisita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocumentos).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(flpVisita);
            pnlPrincipal.Controls.Add(dgvDocumentos);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(855, 166);
            pnlPrincipal.TabIndex = 0;
            // 
            // flpVisita
            // 
            flpVisita.Controls.Add(btnAgregar);
            flpVisita.Controls.Add(btnEliminar);
            flpVisita.Dock = DockStyle.Bottom;
            flpVisita.FlowDirection = FlowDirection.RightToLeft;
            flpVisita.Location = new Point(0, 116);
            flpVisita.Margin = new Padding(1);
            flpVisita.Name = "flpVisita";
            flpVisita.Padding = new Padding(5);
            flpVisita.Size = new Size(855, 50);
            flpVisita.TabIndex = 1;
            flpVisita.WrapContents = false;
            // 
            // btnAgregar
            // 
            btnAgregar.AutoSize = true;
            btnAgregar.BackColor = Color.White;
            btnAgregar.FlatAppearance.BorderColor = Color.Crimson;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(754, 8);
            btnAgregar.Margin = new Padding(12, 3, 3, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Padding = new Padding(10, 0, 0, 0);
            btnAgregar.Size = new Size(88, 34);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.AutoSize = true;
            btnEliminar.BackColor = Color.White;
            btnEliminar.FlatAppearance.BorderColor = Color.Crimson;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.Black;
            btnEliminar.Location = new Point(646, 8);
            btnEliminar.Margin = new Padding(12, 3, 3, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Padding = new Padding(10, 0, 0, 0);
            btnEliminar.Size = new Size(93, 34);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // dgvDocumentos
            // 
            dgvDocumentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocumentos.Dock = DockStyle.Fill;
            dgvDocumentos.Location = new Point(0, 0);
            dgvDocumentos.Margin = new Padding(10);
            dgvDocumentos.Name = "dgvDocumentos";
            dgvDocumentos.Size = new Size(855, 166);
            dgvDocumentos.TabIndex = 0;
            dgvDocumentos.CellContentClick += dgvDocumentos_CellContentClick;
            // 
            // ucDocumentosAdicionales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucDocumentosAdicionales";
            Size = new Size(855, 166);
            pnlPrincipal.ResumeLayout(false);
            flpVisita.ResumeLayout(false);
            flpVisita.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocumentos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private DataGridView dgvDocumentos;
        private FlowLayoutPanel flpVisita;
        private Button btnAgregar;
        private Button btnEliminar;
    }
}
