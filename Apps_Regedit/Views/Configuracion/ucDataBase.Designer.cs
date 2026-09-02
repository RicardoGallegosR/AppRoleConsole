namespace Apps_Regedit.Views.Configuracion {
    partial class ucDataBase {
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
            tlpDatos = new TableLayoutPanel();
            txtAppRolePassword = new TextBox();
            txtAppRole = new TextBox();
            txtAppName = new TextBox();
            txtPassword = new TextBox();
            txtUser = new TextBox();
            txtDatabase = new TextBox();
            lblPassword = new Label();
            lblUser = new Label();
            lblDataBase = new Label();
            txtServer = new TextBox();
            lblServidor = new Label();
            lblAppName = new Label();
            lblAppRole = new Label();
            lblAppRolePassword = new Label();
            pnlPrincipal.SuspendLayout();
            tlpDatos.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(tlpDatos);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(723, 336);
            pnlPrincipal.TabIndex = 0;
            // 
            // tlpDatos
            // 
            tlpDatos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDatos.ColumnCount = 2;
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpDatos.Controls.Add(txtAppRolePassword, 1, 6);
            tlpDatos.Controls.Add(txtAppRole, 1, 5);
            tlpDatos.Controls.Add(txtAppName, 1, 4);
            tlpDatos.Controls.Add(txtPassword, 1, 3);
            tlpDatos.Controls.Add(txtUser, 1, 2);
            tlpDatos.Controls.Add(txtDatabase, 1, 1);
            tlpDatos.Controls.Add(lblPassword, 0, 3);
            tlpDatos.Controls.Add(lblUser, 0, 2);
            tlpDatos.Controls.Add(lblDataBase, 0, 1);
            tlpDatos.Controls.Add(txtServer, 1, 0);
            tlpDatos.Controls.Add(lblServidor, 0, 0);
            tlpDatos.Controls.Add(lblAppName, 0, 4);
            tlpDatos.Controls.Add(lblAppRole, 0, 5);
            tlpDatos.Controls.Add(lblAppRolePassword, 0, 6);
            tlpDatos.Dock = DockStyle.Fill;
            tlpDatos.Font = new Font("Segoe UI", 12F);
            tlpDatos.ForeColor = Color.FromArgb(45, 55, 65);
            tlpDatos.Location = new Point(0, 0);
            tlpDatos.Name = "tlpDatos";
            tlpDatos.Padding = new Padding(14, 10, 14, 10);
            tlpDatos.RowCount = 7;
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.Size = new Size(723, 336);
            tlpDatos.TabIndex = 0;
            // 
            // txtAppRolePassword
            // 
            txtAppRolePassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAppRolePassword.Location = new Point(228, 288);
            txtAppRolePassword.Margin = new Padding(6);
            txtAppRolePassword.Name = "txtAppRolePassword";
            txtAppRolePassword.Size = new Size(475, 29);
            txtAppRolePassword.TabIndex = 13;
            txtAppRolePassword.UseSystemPasswordChar = true;
            // 
            // txtAppRole
            // 
            txtAppRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAppRole.Location = new Point(228, 243);
            txtAppRole.Margin = new Padding(6);
            txtAppRole.Name = "txtAppRole";
            txtAppRole.Size = new Size(475, 29);
            txtAppRole.TabIndex = 12;
            // 
            // txtAppName
            // 
            txtAppName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAppName.Location = new Point(228, 198);
            txtAppName.Margin = new Padding(6);
            txtAppName.Name = "txtAppName";
            txtAppName.Size = new Size(475, 29);
            txtAppName.TabIndex = 11;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(228, 153);
            txtPassword.Margin = new Padding(6);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(475, 29);
            txtPassword.TabIndex = 10;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            txtUser.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUser.Location = new Point(228, 108);
            txtUser.Margin = new Padding(6);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(475, 29);
            txtUser.TabIndex = 9;
            // 
            // txtDatabase
            // 
            txtDatabase.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDatabase.Location = new Point(228, 63);
            txtDatabase.Margin = new Padding(6);
            txtDatabase.Name = "txtDatabase";
            txtDatabase.Size = new Size(475, 29);
            txtDatabase.TabIndex = 8;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Location = new Point(18, 149);
            lblPassword.Margin = new Padding(4);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(200, 37);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Dock = DockStyle.Fill;
            lblUser.Location = new Point(18, 104);
            lblUser.Margin = new Padding(4);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(200, 37);
            lblUser.TabIndex = 4;
            lblUser.Text = "Usuario";
            lblUser.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDataBase
            // 
            lblDataBase.AutoSize = true;
            lblDataBase.Dock = DockStyle.Fill;
            lblDataBase.Location = new Point(18, 59);
            lblDataBase.Margin = new Padding(4);
            lblDataBase.Name = "lblDataBase";
            lblDataBase.Size = new Size(200, 37);
            lblDataBase.TabIndex = 2;
            lblDataBase.Text = "Base de datos";
            lblDataBase.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtServer
            // 
            txtServer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtServer.Location = new Point(228, 18);
            txtServer.Margin = new Padding(6);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(475, 29);
            txtServer.TabIndex = 0;
            // 
            // lblServidor
            // 
            lblServidor.AutoSize = true;
            lblServidor.Dock = DockStyle.Fill;
            lblServidor.Location = new Point(18, 14);
            lblServidor.Margin = new Padding(4);
            lblServidor.Name = "lblServidor";
            lblServidor.Size = new Size(200, 37);
            lblServidor.TabIndex = 1;
            lblServidor.Text = "Servidor";
            lblServidor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Dock = DockStyle.Fill;
            lblAppName.Location = new Point(18, 194);
            lblAppName.Margin = new Padding(4);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(200, 37);
            lblAppName.TabIndex = 3;
            lblAppName.Text = "Nombre de aplicación\n";
            lblAppName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAppRole
            // 
            lblAppRole.AutoSize = true;
            lblAppRole.Dock = DockStyle.Fill;
            lblAppRole.Location = new Point(18, 239);
            lblAppRole.Margin = new Padding(4);
            lblAppRole.Name = "lblAppRole";
            lblAppRole.Size = new Size(200, 37);
            lblAppRole.TabIndex = 5;
            lblAppRole.Text = "AppRole";
            lblAppRole.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAppRolePassword
            // 
            lblAppRolePassword.AutoSize = true;
            lblAppRolePassword.Dock = DockStyle.Fill;
            lblAppRolePassword.Location = new Point(18, 284);
            lblAppRolePassword.Margin = new Padding(4);
            lblAppRolePassword.Name = "lblAppRolePassword";
            lblAppRolePassword.Size = new Size(200, 38);
            lblAppRolePassword.TabIndex = 7;
            lblAppRolePassword.Text = "Contraseña de AppRole";
            lblAppRolePassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ucDataBase
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucDataBase";
            Size = new Size(723, 336);
            Load += ucDataBase_Load;
            pnlPrincipal.ResumeLayout(false);
            tlpDatos.ResumeLayout(false);
            tlpDatos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private TableLayoutPanel tlpDatos;
        private TextBox txtServer;
        private Label lblServidor;
        private Label lblPassword;
        private Label lblUser;
        private Label lblDataBase;
        private Label lblAppName;
        private Label lblAppRole;
        private Label lblAppRolePassword;
        private TextBox txtAppRolePassword;
        private TextBox txtAppRole;
        private TextBox txtAppName;
        private TextBox txtPassword;
        private TextBox txtUser;
        private TextBox txtDatabase;
    }
}
