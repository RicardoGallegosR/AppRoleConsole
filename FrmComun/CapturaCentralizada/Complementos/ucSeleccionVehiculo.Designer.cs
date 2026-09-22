namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucSeleccionVehiculo {
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
            dgvVehiculos = new DataGridView();
            pblFoother = new Panel();
            flpSeleecionVehicular = new FlowLayoutPanel();
            btnSeleccionar = new Button();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).BeginInit();
            pblFoother.SuspendLayout();
            flpSeleecionVehicular.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(dgvVehiculos);
            pnlPrincipal.Controls.Add(pblFoother);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(938, 191);
            pnlPrincipal.TabIndex = 0;
            // 
            // dgvVehiculos
            // 
            dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVehiculos.Dock = DockStyle.Fill;
            dgvVehiculos.Location = new Point(0, 0);
            dgvVehiculos.Margin = new Padding(10);
            dgvVehiculos.Name = "dgvVehiculos";
            dgvVehiculos.ReadOnly = true;
            dgvVehiculos.Size = new Size(938, 141);
            dgvVehiculos.TabIndex = 1;
            // 
            // pblFoother
            // 
            pblFoother.Controls.Add(flpSeleecionVehicular);
            pblFoother.Dock = DockStyle.Bottom;
            pblFoother.Location = new Point(0, 141);
            pblFoother.Name = "pblFoother";
            pblFoother.Size = new Size(938, 50);
            pblFoother.TabIndex = 0;
            // 
            // flpSeleecionVehicular
            // 
            flpSeleecionVehicular.Controls.Add(btnSeleccionar);
            flpSeleecionVehicular.Dock = DockStyle.Fill;
            flpSeleecionVehicular.FlowDirection = FlowDirection.RightToLeft;
            flpSeleecionVehicular.Location = new Point(0, 0);
            flpSeleecionVehicular.Margin = new Padding(2);
            flpSeleecionVehicular.Name = "flpSeleecionVehicular";
            flpSeleecionVehicular.Padding = new Padding(10, 5, 10, 5);
            flpSeleecionVehicular.Size = new Size(938, 50);
            flpSeleecionVehicular.TabIndex = 0;
            flpSeleecionVehicular.WrapContents = false;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.BackColor = Color.Crimson;
            btnSeleccionar.FlatAppearance.BorderColor = Color.Crimson;
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSeleccionar.ForeColor = Color.White;
            btnSeleccionar.Location = new Point(740, 8);
            btnSeleccionar.Margin = new Padding(3, 3, 20, 3);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(158, 34);
            btnSeleccionar.TabIndex = 2;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.UseVisualStyleBackColor = false;
            // 
            // ucSeleccionVehiculo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucSeleccionVehiculo";
            Size = new Size(938, 191);
            pnlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).EndInit();
            pblFoother.ResumeLayout(false);
            flpSeleecionVehicular.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pblFoother;
        private FlowLayoutPanel flpSeleecionVehicular;
        private Button btnSeleccionar;
        private DataGridView dgvVehiculos;
    }
}
