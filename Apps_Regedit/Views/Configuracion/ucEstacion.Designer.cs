namespace Apps_Regedit.Views.Configuracion {
    partial class ucEstacion {
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
            tlpEstacion = new TableLayoutPanel();
            txtPasswordAutoLogin = new TextBox();
            txtUsuarioDeLinea = new TextBox();
            lblActivarLog = new Label();
            lblPasswordAutoLogin = new Label();
            lblUsuarioDeLinea = new Label();
            txtIp = new TextBox();
            txtCentro = new TextBox();
            txtCentroId = new TextBox();
            txtOpcionMenu = new TextBox();
            lblCentro = new Label();
            lblCentroId = new Label();
            lblOpcionMenu = new Label();
            txtEstacionId = new TextBox();
            lblEstacionId = new Label();
            lblIp = new Label();
            cbActivarLog = new CheckBox();
            tlpEstacion.SuspendLayout();
            SuspendLayout();
            // 
            // tlpEstacion
            // 
            tlpEstacion.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEstacion.BackColor = Color.White;
            tlpEstacion.ColumnCount = 2;
            tlpEstacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpEstacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpEstacion.Controls.Add(txtPasswordAutoLogin, 1, 6);
            tlpEstacion.Controls.Add(txtUsuarioDeLinea, 1, 5);
            tlpEstacion.Controls.Add(lblActivarLog, 0, 7);
            tlpEstacion.Controls.Add(lblPasswordAutoLogin, 0, 6);
            tlpEstacion.Controls.Add(lblUsuarioDeLinea, 0, 5);
            tlpEstacion.Controls.Add(txtIp, 1, 4);
            tlpEstacion.Controls.Add(txtCentro, 1, 3);
            tlpEstacion.Controls.Add(txtCentroId, 1, 2);
            tlpEstacion.Controls.Add(txtOpcionMenu, 1, 1);
            tlpEstacion.Controls.Add(lblCentro, 0, 3);
            tlpEstacion.Controls.Add(lblCentroId, 0, 2);
            tlpEstacion.Controls.Add(lblOpcionMenu, 0, 1);
            tlpEstacion.Controls.Add(txtEstacionId, 1, 0);
            tlpEstacion.Controls.Add(lblEstacionId, 0, 0);
            tlpEstacion.Controls.Add(lblIp, 0, 4);
            tlpEstacion.Controls.Add(cbActivarLog, 1, 7);
            tlpEstacion.Dock = DockStyle.Fill;
            tlpEstacion.Font = new Font("Segoe UI", 12F);
            tlpEstacion.ForeColor = Color.FromArgb(45, 55, 65);
            tlpEstacion.Location = new Point(0, 0);
            tlpEstacion.Name = "tlpEstacion";
            tlpEstacion.Padding = new Padding(14, 10, 14, 10);
            tlpEstacion.RowCount = 8;
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpEstacion.Size = new Size(466, 382);
            tlpEstacion.TabIndex = 0;
            // 
            // txtPasswordAutoLogin
            // 
            txtPasswordAutoLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPasswordAutoLogin.Location = new Point(151, 288);
            txtPasswordAutoLogin.Margin = new Padding(6);
            txtPasswordAutoLogin.Name = "txtPasswordAutoLogin";
            txtPasswordAutoLogin.Size = new Size(295, 29);
            txtPasswordAutoLogin.TabIndex = 7;
            txtPasswordAutoLogin.UseSystemPasswordChar = true;
            // 
            // txtUsuarioDeLinea
            // 
            txtUsuarioDeLinea.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUsuarioDeLinea.Location = new Point(151, 243);
            txtUsuarioDeLinea.Margin = new Padding(6);
            txtUsuarioDeLinea.Name = "txtUsuarioDeLinea";
            txtUsuarioDeLinea.Size = new Size(295, 29);
            txtUsuarioDeLinea.TabIndex = 6;
            // 
            // lblActivarLog
            // 
            lblActivarLog.AutoSize = true;
            lblActivarLog.Dock = DockStyle.Fill;
            lblActivarLog.Location = new Point(18, 329);
            lblActivarLog.Margin = new Padding(4);
            lblActivarLog.Name = "lblActivarLog";
            lblActivarLog.Size = new Size(123, 39);
            lblActivarLog.TabIndex = 0;
            lblActivarLog.Text = "Activar log TXT";
            lblActivarLog.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPasswordAutoLogin
            // 
            lblPasswordAutoLogin.AutoSize = true;
            lblPasswordAutoLogin.Dock = DockStyle.Fill;
            lblPasswordAutoLogin.Location = new Point(18, 284);
            lblPasswordAutoLogin.Margin = new Padding(4);
            lblPasswordAutoLogin.Name = "lblPasswordAutoLogin";
            lblPasswordAutoLogin.Size = new Size(123, 37);
            lblPasswordAutoLogin.TabIndex = 0;
            lblPasswordAutoLogin.Text = "Contraseña de línea";
            lblPasswordAutoLogin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuarioDeLinea
            // 
            lblUsuarioDeLinea.AutoSize = true;
            lblUsuarioDeLinea.Dock = DockStyle.Fill;
            lblUsuarioDeLinea.Location = new Point(18, 239);
            lblUsuarioDeLinea.Margin = new Padding(4);
            lblUsuarioDeLinea.Name = "lblUsuarioDeLinea";
            lblUsuarioDeLinea.Size = new Size(123, 37);
            lblUsuarioDeLinea.TabIndex = 0;
            lblUsuarioDeLinea.Text = "Usuario de línea";
            lblUsuarioDeLinea.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtIp
            // 
            txtIp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIp.Location = new Point(151, 198);
            txtIp.Margin = new Padding(6);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(295, 29);
            txtIp.TabIndex = 5;
            // 
            // txtCentro
            // 
            txtCentro.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCentro.Location = new Point(151, 153);
            txtCentro.Margin = new Padding(6);
            txtCentro.Name = "txtCentro";
            txtCentro.Size = new Size(295, 29);
            txtCentro.TabIndex = 4;
            // 
            // txtCentroId
            // 
            txtCentroId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCentroId.Location = new Point(151, 108);
            txtCentroId.Margin = new Padding(6);
            txtCentroId.Name = "txtCentroId";
            txtCentroId.Size = new Size(295, 29);
            txtCentroId.TabIndex = 3;
            // 
            // txtOpcionMenu
            // 
            txtOpcionMenu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtOpcionMenu.Location = new Point(151, 63);
            txtOpcionMenu.Margin = new Padding(6);
            txtOpcionMenu.Name = "txtOpcionMenu";
            txtOpcionMenu.Size = new Size(295, 29);
            txtOpcionMenu.TabIndex = 2;
            // 
            // lblCentro
            // 
            lblCentro.AutoSize = true;
            lblCentro.Dock = DockStyle.Fill;
            lblCentro.Location = new Point(18, 149);
            lblCentro.Margin = new Padding(4);
            lblCentro.Name = "lblCentro";
            lblCentro.Size = new Size(123, 37);
            lblCentro.TabIndex = 0;
            lblCentro.Text = "Centro";
            lblCentro.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCentroId
            // 
            lblCentroId.AutoSize = true;
            lblCentroId.Dock = DockStyle.Fill;
            lblCentroId.Location = new Point(18, 104);
            lblCentroId.Margin = new Padding(4);
            lblCentroId.Name = "lblCentroId";
            lblCentroId.Size = new Size(123, 37);
            lblCentroId.TabIndex = 0;
            lblCentroId.Text = "CentroId";
            lblCentroId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOpcionMenu
            // 
            lblOpcionMenu.AutoSize = true;
            lblOpcionMenu.Dock = DockStyle.Fill;
            lblOpcionMenu.Location = new Point(18, 59);
            lblOpcionMenu.Margin = new Padding(4);
            lblOpcionMenu.Name = "lblOpcionMenu";
            lblOpcionMenu.Size = new Size(123, 37);
            lblOpcionMenu.TabIndex = 0;
            lblOpcionMenu.Text = "Opción menú";
            lblOpcionMenu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEstacionId
            // 
            txtEstacionId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEstacionId.Location = new Point(151, 18);
            txtEstacionId.Margin = new Padding(6);
            txtEstacionId.Name = "txtEstacionId";
            txtEstacionId.Size = new Size(295, 29);
            txtEstacionId.TabIndex = 1;
            // 
            // lblEstacionId
            // 
            lblEstacionId.AutoSize = true;
            lblEstacionId.Dock = DockStyle.Fill;
            lblEstacionId.Location = new Point(18, 14);
            lblEstacionId.Margin = new Padding(4);
            lblEstacionId.Name = "lblEstacionId";
            lblEstacionId.Size = new Size(123, 37);
            lblEstacionId.TabIndex = 0;
            lblEstacionId.Text = "Estación Id";
            lblEstacionId.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblIp
            // 
            lblIp.AutoSize = true;
            lblIp.Dock = DockStyle.Fill;
            lblIp.Location = new Point(18, 194);
            lblIp.Margin = new Padding(4);
            lblIp.Name = "lblIp";
            lblIp.Size = new Size(123, 37);
            lblIp.TabIndex = 0;
            lblIp.Text = "IP";
            lblIp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbActivarLog
            // 
            cbActivarLog.AutoSize = true;
            cbActivarLog.Dock = DockStyle.Fill;
            cbActivarLog.Location = new Point(148, 328);
            cbActivarLog.Name = "cbActivarLog";
            cbActivarLog.Size = new Size(301, 41);
            cbActivarLog.TabIndex = 8;
            cbActivarLog.TextAlign = ContentAlignment.MiddleCenter;
            cbActivarLog.UseVisualStyleBackColor = true;
            // 
            // ucEstacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpEstacion);
            Name = "ucEstacion";
            Size = new Size(466, 382);
            Load += ucEstacion_Load;
            tlpEstacion.ResumeLayout(false);
            tlpEstacion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpEstacion;
        private TextBox txtIp;
        private TextBox txtCentro;
        private TextBox txtCentroId;
        private TextBox txtOpcionMenu;
        private Label lblCentro;
        private Label lblCentroId;
        private Label lblOpcionMenu;
        private TextBox txtEstacionId;
        private Label lblEstacionId;
        private Label lblIp;
        private Label lblActivarLog;
        private Label lblPasswordAutoLogin;
        private Label lblUsuarioDeLinea;
        private TextBox txtPasswordAutoLogin;
        private TextBox txtUsuarioDeLinea;
        private CheckBox cbActivarLog;
    }
}
