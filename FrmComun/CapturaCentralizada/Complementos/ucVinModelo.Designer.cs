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
            tlpVinModelo = new TableLayoutPanel();
            txtVin = new TextBox();
            lblVin = new Label();
            lblModelo = new Label();
            nudModelo = new NumericUpDown();
            pblFooter = new Panel();
            flpVinModelo = new FlowLayoutPanel();
            btnSeleccionVehiculo = new Button();
            lblConfirmarVin = new Label();
            lblPET = new Label();
            textBox1 = new TextBox();
            cbPET = new CheckBox();
            pnlPrincipal.SuspendLayout();
            tlpVinModelo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudModelo).BeginInit();
            pblFooter.SuspendLayout();
            flpVinModelo.SuspendLayout();
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
            pnlPrincipal.Size = new Size(437, 185);
            pnlPrincipal.TabIndex = 0;
            // 
            // tlpVinModelo
            // 
            tlpVinModelo.ColumnCount = 2;
            tlpVinModelo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVinModelo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVinModelo.Controls.Add(textBox1, 1, 2);
            tlpVinModelo.Controls.Add(lblPET, 0, 3);
            tlpVinModelo.Controls.Add(lblConfirmarVin, 0, 2);
            tlpVinModelo.Controls.Add(txtVin, 1, 1);
            tlpVinModelo.Controls.Add(lblVin, 0, 1);
            tlpVinModelo.Controls.Add(lblModelo, 0, 0);
            tlpVinModelo.Controls.Add(nudModelo, 1, 0);
            tlpVinModelo.Controls.Add(cbPET, 1, 3);
            tlpVinModelo.Dock = DockStyle.Fill;
            tlpVinModelo.Location = new Point(0, 0);
            tlpVinModelo.Name = "tlpVinModelo";
            tlpVinModelo.RowCount = 4;
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpVinModelo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpVinModelo.Size = new Size(437, 135);
            tlpVinModelo.TabIndex = 1;
            // 
            // txtVin
            // 
            txtVin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtVin.Font = new Font("Segoe UI", 10.5F);
            txtVin.Location = new Point(218, 36);
            txtVin.Margin = new Padding(0);
            txtVin.Name = "txtVin";
            txtVin.Size = new Size(219, 26);
            txtVin.TabIndex = 2;
            txtVin.TabStop = false;
            txtVin.Text = "3U5AFCFY5R2006740";
            // 
            // lblVin
            // 
            lblVin.Dock = DockStyle.Fill;
            lblVin.Font = new Font("Segoe UI", 10.5F);
            lblVin.Location = new Point(3, 33);
            lblVin.Name = "lblVin";
            lblVin.Size = new Size(212, 33);
            lblVin.TabIndex = 0;
            lblVin.Text = "VIN";
            lblVin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblModelo
            // 
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 10.5F);
            lblModelo.Location = new Point(3, 0);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(212, 33);
            lblModelo.TabIndex = 0;
            lblModelo.Text = "AÑO MODELO";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudModelo
            // 
            nudModelo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudModelo.Enabled = false;
            nudModelo.Location = new Point(221, 5);
            nudModelo.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudModelo.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudModelo.Name = "nudModelo";
            nudModelo.Size = new Size(213, 23);
            nudModelo.TabIndex = 1;
            nudModelo.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // pblFooter
            // 
            pblFooter.Controls.Add(flpVinModelo);
            pblFooter.Dock = DockStyle.Bottom;
            pblFooter.Location = new Point(0, 135);
            pblFooter.Name = "pblFooter";
            pblFooter.Size = new Size(437, 50);
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
            flpVinModelo.Size = new Size(437, 50);
            flpVinModelo.TabIndex = 0;
            flpVinModelo.WrapContents = false;
            // 
            // btnSeleccionVehiculo
            // 
            btnSeleccionVehiculo.BackColor = SystemColors.Window;
            btnSeleccionVehiculo.FlatAppearance.BorderColor = Color.Crimson;
            btnSeleccionVehiculo.FlatStyle = FlatStyle.Flat;
            btnSeleccionVehiculo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSeleccionVehiculo.Location = new Point(276, 8);
            btnSeleccionVehiculo.Name = "btnSeleccionVehiculo";
            btnSeleccionVehiculo.Size = new Size(148, 34);
            btnSeleccionVehiculo.TabIndex = 3;
            btnSeleccionVehiculo.Text = "Selección de Vehiculo";
            btnSeleccionVehiculo.UseVisualStyleBackColor = false;
            // 
            // lblConfirmarVin
            // 
            lblConfirmarVin.Dock = DockStyle.Fill;
            lblConfirmarVin.Font = new Font("Segoe UI", 10.5F);
            lblConfirmarVin.Location = new Point(3, 66);
            lblConfirmarVin.Name = "lblConfirmarVin";
            lblConfirmarVin.Size = new Size(212, 33);
            lblConfirmarVin.TabIndex = 0;
            lblConfirmarVin.Text = "VIN CONFIRMAR";
            lblConfirmarVin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPET
            // 
            lblPET.Dock = DockStyle.Fill;
            lblPET.Font = new Font("Segoe UI", 10.5F);
            lblPET.Location = new Point(3, 99);
            lblPET.Name = "lblPET";
            lblPET.Size = new Size(212, 36);
            lblPET.TabIndex = 0;
            lblPET.Text = "PET";
            lblPET.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Font = new Font("Segoe UI", 10.5F);
            textBox1.Location = new Point(218, 69);
            textBox1.Margin = new Padding(0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(219, 26);
            textBox1.TabIndex = 3;
            textBox1.TabStop = false;
            textBox1.Text = "3U5AFCFY5R2006740";
            // 
            // cbPET
            // 
            cbPET.AutoSize = true;
            cbPET.Dock = DockStyle.Fill;
            cbPET.Location = new Point(221, 102);
            cbPET.Name = "cbPET";
            cbPET.Size = new Size(213, 30);
            cbPET.TabIndex = 4;
            cbPET.Text = "Prueba de Evaluación Técnica";
            cbPET.UseVisualStyleBackColor = true;
            // 
            // ucVinModelo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucVinModelo";
            Size = new Size(437, 185);
            pnlPrincipal.ResumeLayout(false);
            tlpVinModelo.ResumeLayout(false);
            tlpVinModelo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudModelo).EndInit();
            pblFooter.ResumeLayout(false);
            flpVinModelo.ResumeLayout(false);
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
        private TextBox textBox1;
        private Label lblPET;
        private Label lblConfirmarVin;
        private CheckBox cbPET;
    }
}
