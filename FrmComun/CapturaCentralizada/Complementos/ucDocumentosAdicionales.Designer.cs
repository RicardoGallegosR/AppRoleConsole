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
            btnEscanear = new Button();
            btnAgregar = new Button();
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
            flpVisita.Controls.Add(btnEscanear);
            flpVisita.Controls.Add(btnAgregar);
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
            // btnEscanear
            // 
            btnEscanear.AutoSize = true;
            btnEscanear.BackColor = Color.White;
            btnEscanear.FlatAppearance.BorderColor = Color.Crimson;
            btnEscanear.FlatStyle = FlatStyle.Flat;
            btnEscanear.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnEscanear.ForeColor = Color.Black;
            btnEscanear.Location = new Point(749, 8);
            btnEscanear.Margin = new Padding(12, 3, 3, 3);
            btnEscanear.Name = "btnEscanear";
            btnEscanear.Padding = new Padding(10, 0, 0, 0);
            btnEscanear.Size = new Size(93, 34);
            btnEscanear.TabIndex = 0;
            btnEscanear.Text = "Escanear";
            btnEscanear.UseVisualStyleBackColor = false;
            // 
            // btnAgregar
            // 
            btnAgregar.AutoSize = true;
            btnAgregar.BackColor = Color.White;
            btnAgregar.FlatAppearance.BorderColor = Color.Crimson;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(646, 8);
            btnAgregar.Margin = new Padding(12, 3, 3, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Padding = new Padding(10, 0, 0, 0);
            btnAgregar.Size = new Size(88, 34);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
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
        private Button btnEscanear;
        private Button btnAgregar;
    }
}
