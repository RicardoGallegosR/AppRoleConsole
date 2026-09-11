namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucVinModelo {
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
            pblFooter = new Panel();
            flpVinModelo = new FlowLayoutPanel();
            btnSeleccionVehiculo = new Button();
            tlpVinModelo = new TableLayoutPanel();
            lblModelo = new Label();
            lblVin = new Label();
            txtVin = new TextBox();
            nudModelo = new NumericUpDown();
            pnlPrincipal.SuspendLayout();
            pblFooter.SuspendLayout();
            flpVinModelo.SuspendLayout();
            tlpVinModelo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudModelo).BeginInit();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(tlpVinModelo);
            pnlPrincipal.Controls.Add(pblFooter);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.ForeColor = Color.Black;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(371, 134);
            pnlPrincipal.TabIndex = 0;
            // 
            // pblFooter
            // 
            pblFooter.Controls.Add(flpVinModelo);
            pblFooter.Dock = DockStyle.Bottom;
            pblFooter.Location = new Point(0, 84);
            pblFooter.Name = "pblFooter";
            pblFooter.Size = new Size(371, 50);
            pblFooter.TabIndex = 0;
            // 
            // flpVinModelo
            // 
            flpVinModelo.Controls.Add(btnSeleccionVehiculo);
            flpVinModelo.Dock = DockStyle.Fill;
            flpVinModelo.FlowDirection = FlowDirection.RightToLeft;
            flpVinModelo.Location = new Point(0, 0);
            flpVinModelo.Margin = new Padding(1);
            flpVinModelo.Name = "flpVinModelo";
            flpVinModelo.Padding = new Padding(5);
            flpVinModelo.Size = new Size(371, 50);
            flpVinModelo.TabIndex = 0;
            flpVinModelo.WrapContents = false;
            // 
            // btnSeleccionVehiculo
            // 
            btnSeleccionVehiculo.BackColor = SystemColors.Window;
            btnSeleccionVehiculo.FlatAppearance.BorderColor = Color.Crimson;
            btnSeleccionVehiculo.FlatStyle = FlatStyle.Flat;
            btnSeleccionVehiculo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSeleccionVehiculo.Location = new Point(210, 8);
            btnSeleccionVehiculo.Name = "btnSeleccionVehiculo";
            btnSeleccionVehiculo.Size = new Size(148, 34);
            btnSeleccionVehiculo.TabIndex = 3;
            btnSeleccionVehiculo.Text = "Selección de Vehiculo";
            btnSeleccionVehiculo.UseVisualStyleBackColor = false;
            // 
            // tlpVinModelo
            // 
            tlpVinModelo.ColumnCount = 2;
            tlpVinModelo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVinModelo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVinModelo.Controls.Add(txtVin, 1, 1);
            tlpVinModelo.Controls.Add(lblVin, 0, 1);
            tlpVinModelo.Controls.Add(lblModelo, 0, 0);
            tlpVinModelo.Controls.Add(nudModelo, 1, 0);
            tlpVinModelo.Dock = DockStyle.Fill;
            tlpVinModelo.Location = new Point(0, 0);
            tlpVinModelo.Name = "tlpVinModelo";
            tlpVinModelo.RowCount = 2;
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpVinModelo.Size = new Size(371, 84);
            tlpVinModelo.TabIndex = 1;
            // 
            // lblModelo
            // 
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 10.5F);
            lblModelo.Location = new Point(3, 0);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(179, 42);
            lblModelo.TabIndex = 0;
            lblModelo.Text = "AÑO MODELO";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVin
            // 
            lblVin.Dock = DockStyle.Fill;
            lblVin.Font = new Font("Segoe UI", 10.5F);
            lblVin.Location = new Point(3, 42);
            lblVin.Name = "lblVin";
            lblVin.Size = new Size(179, 42);
            lblVin.TabIndex = 0;
            lblVin.Text = "VIN";
            lblVin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtVin
            // 
            txtVin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtVin.Font = new Font("Segoe UI", 10.5F);
            txtVin.Location = new Point(185, 50);
            txtVin.Margin = new Padding(0);
            txtVin.Name = "txtVin";
            txtVin.Size = new Size(186, 26);
            txtVin.TabIndex = 2;
            // 
            // nudModelo
            // 
            nudModelo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudModelo.Location = new Point(188, 9);
            nudModelo.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudModelo.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudModelo.Name = "nudModelo";
            nudModelo.Size = new Size(180, 23);
            nudModelo.TabIndex = 1;
            nudModelo.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // ucVinModelo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucVinModelo";
            Size = new Size(371, 134);
            pnlPrincipal.ResumeLayout(false);
            pblFooter.ResumeLayout(false);
            flpVinModelo.ResumeLayout(false);
            tlpVinModelo.ResumeLayout(false);
            tlpVinModelo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudModelo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pblFooter;
        private FlowLayoutPanel flpVinModelo;
        private Button btnSeleccionVehiculo;
        private TableLayoutPanel tlpVinModelo;
        private Label lblVin;
        private Label lblModelo;
        private TextBox txtVin;
        private NumericUpDown nudModelo;
    }
}
