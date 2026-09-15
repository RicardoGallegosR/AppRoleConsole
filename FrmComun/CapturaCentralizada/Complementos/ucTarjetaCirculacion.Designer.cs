namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucTarjetaCirculacion {
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
            tlpTarjetaCirculacion = new TableLayoutPanel();
            nudTubosEscape = new NumericUpDown();
            nudModelo = new NumericUpDown();
            txtClaveVehicular = new TextBox();
            txtFolioTC = new TextBox();
            txtPropietario = new TextBox();
            lblFTC = new Label();
            lblClaveVehicular = new Label();
            lblCombustible = new Label();
            lblTubosEscape = new Label();
            lblSubMarca = new Label();
            lblFolioTC = new Label();
            lblMarca = new Label();
            lblModelo = new Label();
            lblPropietario = new Label();
            dtpFTC = new DateTimePicker();
            cbMarcas = new ComboBox();
            pnlFooter = new Panel();
            flpSeleecionVehicular = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnEditar = new Button();
            cbSubmarcas = new ComboBox();
            cbCombustibles = new ComboBox();
            pnlPrincipal.SuspendLayout();
            tlpTarjetaCirculacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTubosEscape).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudModelo).BeginInit();
            pnlFooter.SuspendLayout();
            flpSeleecionVehicular.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(tlpTarjetaCirculacion);
            pnlPrincipal.Controls.Add(pnlFooter);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.ForeColor = Color.FromArgb(45, 55, 65);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(789, 265);
            pnlPrincipal.TabIndex = 0;
            // 
            // tlpTarjetaCirculacion
            // 
            tlpTarjetaCirculacion.ColumnCount = 4;
            tlpTarjetaCirculacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetaCirculacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetaCirculacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetaCirculacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetaCirculacion.Controls.Add(cbCombustibles, 1, 3);
            tlpTarjetaCirculacion.Controls.Add(cbSubmarcas, 1, 2);
            tlpTarjetaCirculacion.Controls.Add(nudTubosEscape, 3, 2);
            tlpTarjetaCirculacion.Controls.Add(nudModelo, 3, 0);
            tlpTarjetaCirculacion.Controls.Add(txtClaveVehicular, 3, 3);
            tlpTarjetaCirculacion.Controls.Add(txtFolioTC, 3, 1);
            tlpTarjetaCirculacion.Controls.Add(txtPropietario, 1, 0);
            tlpTarjetaCirculacion.Controls.Add(lblFTC, 0, 4);
            tlpTarjetaCirculacion.Controls.Add(lblClaveVehicular, 2, 3);
            tlpTarjetaCirculacion.Controls.Add(lblCombustible, 0, 3);
            tlpTarjetaCirculacion.Controls.Add(lblTubosEscape, 2, 2);
            tlpTarjetaCirculacion.Controls.Add(lblSubMarca, 0, 2);
            tlpTarjetaCirculacion.Controls.Add(lblFolioTC, 2, 1);
            tlpTarjetaCirculacion.Controls.Add(lblMarca, 0, 1);
            tlpTarjetaCirculacion.Controls.Add(lblModelo, 2, 0);
            tlpTarjetaCirculacion.Controls.Add(lblPropietario, 0, 0);
            tlpTarjetaCirculacion.Controls.Add(dtpFTC, 1, 4);
            tlpTarjetaCirculacion.Controls.Add(cbMarcas, 1, 1);
            tlpTarjetaCirculacion.Dock = DockStyle.Fill;
            tlpTarjetaCirculacion.Location = new Point(0, 0);
            tlpTarjetaCirculacion.Name = "tlpTarjetaCirculacion";
            tlpTarjetaCirculacion.RowCount = 5;
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tlpTarjetaCirculacion.Size = new Size(789, 215);
            tlpTarjetaCirculacion.TabIndex = 1;
            // 
            // nudTubosEscape
            // 
            nudTubosEscape.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudTubosEscape.Location = new Point(594, 96);
            nudTubosEscape.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            nudTubosEscape.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTubosEscape.Name = "nudTubosEscape";
            nudTubosEscape.Size = new Size(192, 23);
            nudTubosEscape.TabIndex = 11;
            nudTubosEscape.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // nudModelo
            // 
            nudModelo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudModelo.Location = new Point(594, 10);
            nudModelo.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudModelo.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudModelo.Name = "nudModelo";
            nudModelo.Size = new Size(192, 23);
            nudModelo.TabIndex = 10;
            nudModelo.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // txtClaveVehicular
            // 
            txtClaveVehicular.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtClaveVehicular.Font = new Font("Segoe UI", 10.5F);
            txtClaveVehicular.Location = new Point(591, 137);
            txtClaveVehicular.Margin = new Padding(0);
            txtClaveVehicular.Name = "txtClaveVehicular";
            txtClaveVehicular.Size = new Size(198, 26);
            txtClaveVehicular.TabIndex = 9;
            // 
            // txtFolioTC
            // 
            txtFolioTC.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFolioTC.Font = new Font("Segoe UI", 10.5F);
            txtFolioTC.Location = new Point(591, 51);
            txtFolioTC.Margin = new Padding(0);
            txtFolioTC.Name = "txtFolioTC";
            txtFolioTC.Size = new Size(198, 26);
            txtFolioTC.TabIndex = 7;
            // 
            // txtPropietario
            // 
            txtPropietario.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPropietario.Font = new Font("Segoe UI", 10.5F);
            txtPropietario.Location = new Point(197, 8);
            txtPropietario.Margin = new Padding(0);
            txtPropietario.Name = "txtPropietario";
            txtPropietario.Size = new Size(197, 26);
            txtPropietario.TabIndex = 1;
            // 
            // lblFTC
            // 
            lblFTC.Dock = DockStyle.Fill;
            lblFTC.Font = new Font("Segoe UI", 10.5F);
            lblFTC.Location = new Point(3, 172);
            lblFTC.Name = "lblFTC";
            lblFTC.Size = new Size(191, 43);
            lblFTC.TabIndex = 0;
            lblFTC.Text = "FECHA TARJETA CIRCULACION";
            lblFTC.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblClaveVehicular
            // 
            lblClaveVehicular.Dock = DockStyle.Fill;
            lblClaveVehicular.Font = new Font("Segoe UI", 10.5F);
            lblClaveVehicular.Location = new Point(397, 129);
            lblClaveVehicular.Name = "lblClaveVehicular";
            lblClaveVehicular.Size = new Size(191, 43);
            lblClaveVehicular.TabIndex = 0;
            lblClaveVehicular.Text = "CLAVE VEHICULAR";
            lblClaveVehicular.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCombustible
            // 
            lblCombustible.Dock = DockStyle.Fill;
            lblCombustible.Font = new Font("Segoe UI", 10.5F);
            lblCombustible.Location = new Point(3, 129);
            lblCombustible.Name = "lblCombustible";
            lblCombustible.Size = new Size(191, 43);
            lblCombustible.TabIndex = 0;
            lblCombustible.Text = "COMBUSTIBLE";
            lblCombustible.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTubosEscape
            // 
            lblTubosEscape.Dock = DockStyle.Fill;
            lblTubosEscape.Font = new Font("Segoe UI", 10.5F);
            lblTubosEscape.Location = new Point(397, 86);
            lblTubosEscape.Name = "lblTubosEscape";
            lblTubosEscape.Size = new Size(191, 43);
            lblTubosEscape.TabIndex = 0;
            lblTubosEscape.Text = "TUBOS DE ESCAPE";
            lblTubosEscape.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubMarca
            // 
            lblSubMarca.Dock = DockStyle.Fill;
            lblSubMarca.Font = new Font("Segoe UI", 10.5F);
            lblSubMarca.ForeColor = Color.FromArgb(45, 55, 65);
            lblSubMarca.Location = new Point(3, 86);
            lblSubMarca.Name = "lblSubMarca";
            lblSubMarca.Size = new Size(191, 43);
            lblSubMarca.TabIndex = 0;
            lblSubMarca.Text = "SUBMARCA";
            lblSubMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFolioTC
            // 
            lblFolioTC.Dock = DockStyle.Fill;
            lblFolioTC.Font = new Font("Segoe UI", 10.5F);
            lblFolioTC.Location = new Point(397, 43);
            lblFolioTC.Name = "lblFolioTC";
            lblFolioTC.Size = new Size(191, 43);
            lblFolioTC.TabIndex = 0;
            lblFolioTC.Text = "FOLIO TARJETA CIRCULACION";
            lblFolioTC.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMarca
            // 
            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 10.5F);
            lblMarca.ForeColor = Color.FromArgb(45, 55, 65);
            lblMarca.Location = new Point(3, 43);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(191, 43);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "MARCA";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblModelo
            // 
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 10.5F);
            lblModelo.Location = new Point(397, 0);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(191, 43);
            lblModelo.TabIndex = 0;
            lblModelo.Text = "MODELO";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPropietario
            // 
            lblPropietario.Dock = DockStyle.Fill;
            lblPropietario.Font = new Font("Segoe UI", 10.5F);
            lblPropietario.ForeColor = Color.FromArgb(45, 55, 65);
            lblPropietario.Location = new Point(3, 0);
            lblPropietario.Name = "lblPropietario";
            lblPropietario.Size = new Size(191, 43);
            lblPropietario.TabIndex = 0;
            lblPropietario.Text = "PROPIETARIO";
            lblPropietario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpFTC
            // 
            dtpFTC.Location = new Point(200, 175);
            dtpFTC.Name = "dtpFTC";
            dtpFTC.Size = new Size(191, 23);
            dtpFTC.TabIndex = 12;
            // 
            // cbMarcas
            // 
            cbMarcas.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbMarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMarcas.Font = new Font("Segoe UI", 12F);
            cbMarcas.ForeColor = Color.Black;
            cbMarcas.FormattingEnabled = true;
            cbMarcas.Location = new Point(200, 53);
            cbMarcas.Name = "cbMarcas";
            cbMarcas.Size = new Size(191, 29);
            cbMarcas.TabIndex = 13;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(flpSeleecionVehicular);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 215);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(789, 50);
            pnlFooter.TabIndex = 0;
            // 
            // flpSeleecionVehicular
            // 
            flpSeleecionVehicular.Controls.Add(btnGuardar);
            flpSeleecionVehicular.Controls.Add(btnEditar);
            flpSeleecionVehicular.Dock = DockStyle.Fill;
            flpSeleecionVehicular.FlowDirection = FlowDirection.RightToLeft;
            flpSeleecionVehicular.Location = new Point(0, 0);
            flpSeleecionVehicular.Margin = new Padding(2);
            flpSeleecionVehicular.Name = "flpSeleecionVehicular";
            flpSeleecionVehicular.Padding = new Padding(10, 5, 10, 5);
            flpSeleecionVehicular.Size = new Size(789, 50);
            flpSeleecionVehicular.TabIndex = 1;
            flpSeleecionVehicular.WrapContents = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Crimson;
            btnGuardar.FlatAppearance.BorderColor = Color.Crimson;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(591, 8);
            btnGuardar.Margin = new Padding(3, 3, 20, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(158, 34);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar cambios";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.FlatAppearance.BorderColor = Color.Crimson;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnEditar.Location = new Point(476, 8);
            btnEditar.Margin = new Padding(3, 3, 20, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(92, 34);
            btnEditar.TabIndex = 0;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // cbSubmarcas
            // 
            cbSubmarcas.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbSubmarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSubmarcas.Font = new Font("Segoe UI", 12F);
            cbSubmarcas.ForeColor = Color.Black;
            cbSubmarcas.FormattingEnabled = true;
            cbSubmarcas.Location = new Point(200, 96);
            cbSubmarcas.Name = "cbSubmarcas";
            cbSubmarcas.Size = new Size(191, 29);
            cbSubmarcas.TabIndex = 14;
            // 
            // cbCombustibles
            // 
            cbCombustibles.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbCombustibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCombustibles.Font = new Font("Segoe UI", 12F);
            cbCombustibles.ForeColor = Color.Black;
            cbCombustibles.FormattingEnabled = true;
            cbCombustibles.Location = new Point(200, 139);
            cbCombustibles.Name = "cbCombustibles";
            cbCombustibles.Size = new Size(191, 29);
            cbCombustibles.TabIndex = 15;
            // 
            // ucTarjetaCirculacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucTarjetaCirculacion";
            Size = new Size(789, 265);
            pnlPrincipal.ResumeLayout(false);
            tlpTarjetaCirculacion.ResumeLayout(false);
            tlpTarjetaCirculacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTubosEscape).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudModelo).EndInit();
            pnlFooter.ResumeLayout(false);
            flpSeleecionVehicular.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlFooter;
        private FlowLayoutPanel flpSeleecionVehicular;
        private Button btnGuardar;
        private Button btnEditar;
        private TableLayoutPanel tlpTarjetaCirculacion;
        private Label lblFolioTC;
        private Label lblFTC;
        private Label lblTubosEscape;
        private Label label24;
        private Label label22;
        private Label label20;
        private Label label18;
        private Label label16;
        private Label lblClaveVehicular;
        private Label lblCombustible;
        private Label label10;
        private Label lblSubMarca;
        private Label label6;
        private Label lblMarca;
        private Label lblModelo;
        private Label lblPropietario;
        private TextBox textBox8;
        private TextBox txtClaveVehicular;
        private TextBox txtFolioTC;
        private TextBox txtPropietario;
        private NumericUpDown nudTubosEscape;
        private NumericUpDown nudModelo;
        private DateTimePicker dtpFTC;
        private ComboBox cbMarcas;
        private ComboBox cbCombustibles;
        private ComboBox cbSubmarcas;
    }
}
