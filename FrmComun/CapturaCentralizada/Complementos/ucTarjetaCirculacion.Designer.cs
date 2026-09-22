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
            lblApellidoMaterno = new Label();
            txtNombre = new TextBox();
            txtApellidoMaterno = new TextBox();
            lblNombre = new Label();
            lblClaveVehicular = new Label();
            lblTubosEscape = new Label();
            lblFolioTC = new Label();
            lblModelo = new Label();
            nudTubosEscape = new NumericUpDown();
            txtClaveVehicular = new TextBox();
            nudModelo = new NumericUpDown();
            txtFolioTC = new TextBox();
            lblFTC = new Label();
            lblCombustible = new Label();
            lblSubMarca = new Label();
            lblMarca = new Label();
            dtpFTC = new DateTimePicker();
            cbCombustibles = new ComboBox();
            cbSubmarcas = new ComboBox();
            cbMarcas = new ComboBox();
            lblApellidoPaterno = new Label();
            lblTipoPersona = new Label();
            txtApellidoPaterno = new TextBox();
            cbTipoPersona = new CheckBox();
            pnlFooter = new Panel();
            flpSeleecionVehicular = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnEditar = new Button();
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
            tlpTarjetaCirculacion.Controls.Add(lblApellidoMaterno, 2, 1);
            tlpTarjetaCirculacion.Controls.Add(txtNombre, 1, 0);
            tlpTarjetaCirculacion.Controls.Add(txtApellidoMaterno, 3, 1);
            tlpTarjetaCirculacion.Controls.Add(lblNombre, 0, 0);
            tlpTarjetaCirculacion.Controls.Add(lblClaveVehicular, 2, 5);
            tlpTarjetaCirculacion.Controls.Add(lblTubosEscape, 2, 4);
            tlpTarjetaCirculacion.Controls.Add(lblFolioTC, 2, 3);
            tlpTarjetaCirculacion.Controls.Add(lblModelo, 2, 2);
            tlpTarjetaCirculacion.Controls.Add(nudTubosEscape, 3, 4);
            tlpTarjetaCirculacion.Controls.Add(txtClaveVehicular, 3, 5);
            tlpTarjetaCirculacion.Controls.Add(nudModelo, 3, 2);
            tlpTarjetaCirculacion.Controls.Add(txtFolioTC, 3, 3);
            tlpTarjetaCirculacion.Controls.Add(lblFTC, 0, 5);
            tlpTarjetaCirculacion.Controls.Add(lblCombustible, 0, 4);
            tlpTarjetaCirculacion.Controls.Add(lblSubMarca, 0, 3);
            tlpTarjetaCirculacion.Controls.Add(lblMarca, 0, 2);
            tlpTarjetaCirculacion.Controls.Add(dtpFTC, 1, 5);
            tlpTarjetaCirculacion.Controls.Add(cbCombustibles, 1, 4);
            tlpTarjetaCirculacion.Controls.Add(cbSubmarcas, 1, 3);
            tlpTarjetaCirculacion.Controls.Add(cbMarcas, 1, 2);
            tlpTarjetaCirculacion.Controls.Add(lblApellidoPaterno, 0, 1);
            tlpTarjetaCirculacion.Controls.Add(lblTipoPersona, 2, 0);
            tlpTarjetaCirculacion.Controls.Add(txtApellidoPaterno, 1, 1);
            tlpTarjetaCirculacion.Controls.Add(cbTipoPersona, 3, 0);
            tlpTarjetaCirculacion.Dock = DockStyle.Fill;
            tlpTarjetaCirculacion.Location = new Point(0, 0);
            tlpTarjetaCirculacion.Name = "tlpTarjetaCirculacion";
            tlpTarjetaCirculacion.RowCount = 6;
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpTarjetaCirculacion.Size = new Size(789, 215);
            tlpTarjetaCirculacion.TabIndex = 1;
            // 
            // lblApellidoMaterno
            // 
            lblApellidoMaterno.Dock = DockStyle.Fill;
            lblApellidoMaterno.Font = new Font("Segoe UI", 10.5F);
            lblApellidoMaterno.ForeColor = Color.FromArgb(45, 55, 65);
            lblApellidoMaterno.Location = new Point(397, 35);
            lblApellidoMaterno.Name = "lblApellidoMaterno";
            lblApellidoMaterno.Size = new Size(191, 35);
            lblApellidoMaterno.TabIndex = 17;
            lblApellidoMaterno.Text = "APELLIDO MATERNO";
            lblApellidoMaterno.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNombre.Font = new Font("Segoe UI", 10.5F);
            txtNombre.Location = new Point(197, 4);
            txtNombre.Margin = new Padding(0);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(197, 26);
            txtNombre.TabIndex = 1;
            // 
            // txtApellidoMaterno
            // 
            txtApellidoMaterno.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtApellidoMaterno.Font = new Font("Segoe UI", 10.5F);
            txtApellidoMaterno.Location = new Point(591, 39);
            txtApellidoMaterno.Margin = new Padding(0);
            txtApellidoMaterno.Name = "txtApellidoMaterno";
            txtApellidoMaterno.Size = new Size(198, 26);
            txtApellidoMaterno.TabIndex = 19;
            // 
            // lblNombre
            // 
            lblNombre.Dock = DockStyle.Fill;
            lblNombre.Font = new Font("Segoe UI", 10.5F);
            lblNombre.ForeColor = Color.FromArgb(45, 55, 65);
            lblNombre.Location = new Point(3, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(191, 35);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "NOMBRE";
            lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblClaveVehicular
            // 
            lblClaveVehicular.Dock = DockStyle.Fill;
            lblClaveVehicular.Font = new Font("Segoe UI", 10.5F);
            lblClaveVehicular.Location = new Point(397, 175);
            lblClaveVehicular.Name = "lblClaveVehicular";
            lblClaveVehicular.Size = new Size(191, 40);
            lblClaveVehicular.TabIndex = 0;
            lblClaveVehicular.Text = "CLAVE VEHICULAR";
            lblClaveVehicular.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTubosEscape
            // 
            lblTubosEscape.Dock = DockStyle.Fill;
            lblTubosEscape.Font = new Font("Segoe UI", 10.5F);
            lblTubosEscape.Location = new Point(397, 140);
            lblTubosEscape.Name = "lblTubosEscape";
            lblTubosEscape.Size = new Size(191, 35);
            lblTubosEscape.TabIndex = 0;
            lblTubosEscape.Text = "TUBOS DE ESCAPE";
            lblTubosEscape.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFolioTC
            // 
            lblFolioTC.Dock = DockStyle.Fill;
            lblFolioTC.Font = new Font("Segoe UI", 10.5F);
            lblFolioTC.Location = new Point(397, 105);
            lblFolioTC.Name = "lblFolioTC";
            lblFolioTC.Size = new Size(191, 35);
            lblFolioTC.TabIndex = 0;
            lblFolioTC.Text = "FOLIO TARJETA CIRCULACION";
            lblFolioTC.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblModelo
            // 
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 10.5F);
            lblModelo.Location = new Point(397, 70);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(191, 35);
            lblModelo.TabIndex = 0;
            lblModelo.Text = "MODELO";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudTubosEscape
            // 
            nudTubosEscape.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudTubosEscape.Location = new Point(594, 146);
            nudTubosEscape.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            nudTubosEscape.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTubosEscape.Name = "nudTubosEscape";
            nudTubosEscape.Size = new Size(192, 23);
            nudTubosEscape.TabIndex = 11;
            nudTubosEscape.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // txtClaveVehicular
            // 
            txtClaveVehicular.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtClaveVehicular.Font = new Font("Segoe UI", 10.5F);
            txtClaveVehicular.Location = new Point(591, 182);
            txtClaveVehicular.Margin = new Padding(0);
            txtClaveVehicular.Name = "txtClaveVehicular";
            txtClaveVehicular.Size = new Size(198, 26);
            txtClaveVehicular.TabIndex = 9;
            // 
            // nudModelo
            // 
            nudModelo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nudModelo.Enabled = false;
            nudModelo.Location = new Point(594, 76);
            nudModelo.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudModelo.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudModelo.Name = "nudModelo";
            nudModelo.Size = new Size(192, 23);
            nudModelo.TabIndex = 10;
            nudModelo.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // txtFolioTC
            // 
            txtFolioTC.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFolioTC.Font = new Font("Segoe UI", 10.5F);
            txtFolioTC.Location = new Point(591, 109);
            txtFolioTC.Margin = new Padding(0);
            txtFolioTC.Name = "txtFolioTC";
            txtFolioTC.Size = new Size(198, 26);
            txtFolioTC.TabIndex = 7;
            // 
            // lblFTC
            // 
            lblFTC.Font = new Font("Segoe UI", 10.5F);
            lblFTC.Location = new Point(3, 175);
            lblFTC.Name = "lblFTC";
            lblFTC.Size = new Size(191, 40);
            lblFTC.TabIndex = 0;
            lblFTC.Text = "FECHA TARJETA CIRCULACION";
            lblFTC.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCombustible
            // 
            lblCombustible.Font = new Font("Segoe UI", 10.5F);
            lblCombustible.Location = new Point(3, 140);
            lblCombustible.Name = "lblCombustible";
            lblCombustible.Size = new Size(191, 35);
            lblCombustible.TabIndex = 0;
            lblCombustible.Text = "COMBUSTIBLE";
            lblCombustible.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubMarca
            // 
            lblSubMarca.Font = new Font("Segoe UI", 10.5F);
            lblSubMarca.ForeColor = Color.FromArgb(45, 55, 65);
            lblSubMarca.Location = new Point(3, 105);
            lblSubMarca.Name = "lblSubMarca";
            lblSubMarca.Size = new Size(191, 35);
            lblSubMarca.TabIndex = 0;
            lblSubMarca.Text = "SUBMARCA";
            lblSubMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMarca
            // 
            lblMarca.Font = new Font("Segoe UI", 10.5F);
            lblMarca.ForeColor = Color.FromArgb(45, 55, 65);
            lblMarca.Location = new Point(3, 70);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(191, 35);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "MARCA";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpFTC
            // 
            dtpFTC.Location = new Point(200, 178);
            dtpFTC.Name = "dtpFTC";
            dtpFTC.Size = new Size(191, 23);
            dtpFTC.TabIndex = 12;
            // 
            // cbCombustibles
            // 
            cbCombustibles.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbCombustibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCombustibles.Font = new Font("Segoe UI", 12F);
            cbCombustibles.ForeColor = Color.Black;
            cbCombustibles.FormattingEnabled = true;
            cbCombustibles.Location = new Point(200, 143);
            cbCombustibles.Name = "cbCombustibles";
            cbCombustibles.Size = new Size(191, 29);
            cbCombustibles.TabIndex = 15;
            // 
            // cbSubmarcas
            // 
            cbSubmarcas.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbSubmarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSubmarcas.Font = new Font("Segoe UI", 12F);
            cbSubmarcas.ForeColor = Color.Black;
            cbSubmarcas.FormattingEnabled = true;
            cbSubmarcas.Location = new Point(200, 108);
            cbSubmarcas.Name = "cbSubmarcas";
            cbSubmarcas.Size = new Size(191, 29);
            cbSubmarcas.TabIndex = 14;
            // 
            // cbMarcas
            // 
            cbMarcas.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbMarcas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMarcas.Font = new Font("Segoe UI", 12F);
            cbMarcas.ForeColor = Color.Black;
            cbMarcas.FormattingEnabled = true;
            cbMarcas.Location = new Point(200, 73);
            cbMarcas.Name = "cbMarcas";
            cbMarcas.Size = new Size(191, 29);
            cbMarcas.TabIndex = 13;
            // 
            // lblApellidoPaterno
            // 
            lblApellidoPaterno.Dock = DockStyle.Fill;
            lblApellidoPaterno.Font = new Font("Segoe UI", 10.5F);
            lblApellidoPaterno.ForeColor = Color.FromArgb(45, 55, 65);
            lblApellidoPaterno.Location = new Point(3, 35);
            lblApellidoPaterno.Name = "lblApellidoPaterno";
            lblApellidoPaterno.Size = new Size(191, 35);
            lblApellidoPaterno.TabIndex = 16;
            lblApellidoPaterno.Text = "APELLIDO PATERNO";
            lblApellidoPaterno.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTipoPersona
            // 
            lblTipoPersona.Dock = DockStyle.Fill;
            lblTipoPersona.Font = new Font("Segoe UI", 10.5F);
            lblTipoPersona.ForeColor = Color.FromArgb(45, 55, 65);
            lblTipoPersona.Location = new Point(397, 0);
            lblTipoPersona.Name = "lblTipoPersona";
            lblTipoPersona.Size = new Size(191, 35);
            lblTipoPersona.TabIndex = 20;
            lblTipoPersona.Text = "TIPO PERSONA";
            lblTipoPersona.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtApellidoPaterno
            // 
            txtApellidoPaterno.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtApellidoPaterno.Font = new Font("Segoe UI", 10.5F);
            txtApellidoPaterno.Location = new Point(197, 39);
            txtApellidoPaterno.Margin = new Padding(0);
            txtApellidoPaterno.Name = "txtApellidoPaterno";
            txtApellidoPaterno.Size = new Size(197, 26);
            txtApellidoPaterno.TabIndex = 18;
            // 
            // cbTipoPersona
            // 
            cbTipoPersona.AutoSize = true;
            cbTipoPersona.Dock = DockStyle.Fill;
            cbTipoPersona.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbTipoPersona.Location = new Point(594, 3);
            cbTipoPersona.Name = "cbTipoPersona";
            cbTipoPersona.Size = new Size(192, 29);
            cbTipoPersona.TabIndex = 21;
            cbTipoPersona.Text = "Moral";
            cbTipoPersona.UseVisualStyleBackColor = true;
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
            btnGuardar.Text = "Guardar/Siguiente";
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
        private Label lblNombre;
        private TextBox textBox8;
        private TextBox txtClaveVehicular;
        private TextBox txtFolioTC;
        private TextBox txtNombre;
        private NumericUpDown nudTubosEscape;
        private NumericUpDown nudModelo;
        private DateTimePicker dtpFTC;
        private ComboBox cbMarcas;
        private ComboBox cbCombustibles;
        private ComboBox cbSubmarcas;
        private Label lblApellidoPaterno;
        private Label lblApellidoMaterno;
        private TextBox txtApellidoMaterno;
        private TextBox txtApellidoPaterno;
        private Label lblTipoPersona;
        private CheckBox cbTipoPersona;
    }
}
