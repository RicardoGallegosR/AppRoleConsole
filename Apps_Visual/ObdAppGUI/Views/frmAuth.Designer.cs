namespace Apps_Visual.ObdAppGUI.Views {
    partial class frmAuth {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pnlPrincipal = new Panel();
            panel1 = new Panel();
            tlpCredenciales = new TableLayoutPanel();
            lblCredencial = new Label();
            lblPassword = new Label();
            txbCredencial = new TextBox();
            txbPassword = new TextBox();
            btnAcceder = new Button();
            pnlHeader = new Panel();
            lblTituloLogin = new Label();
            pnlPrincipal.SuspendLayout();
            panel1.SuspendLayout();
            tlpCredenciales.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = SystemColors.ButtonFace;
            pnlPrincipal.Controls.Add(panel1);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tlpCredenciales);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 79);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 371);
            panel1.TabIndex = 2;
            // 
            // tlpCredenciales
            // 
            tlpCredenciales.ColumnCount = 1;
            tlpCredenciales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCredenciales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpCredenciales.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpCredenciales.Controls.Add(lblCredencial, 0, 0);
            tlpCredenciales.Controls.Add(lblPassword, 0, 2);
            tlpCredenciales.Controls.Add(txbCredencial, 0, 1);
            tlpCredenciales.Controls.Add(txbPassword, 0, 3);
            tlpCredenciales.Controls.Add(btnAcceder, 0, 5);
            tlpCredenciales.Dock = DockStyle.Fill;
            tlpCredenciales.Location = new Point(0, 0);
            tlpCredenciales.Name = "tlpCredenciales";
            tlpCredenciales.RowCount = 6;
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.RowStyles.Add(new RowStyle());
            tlpCredenciales.Size = new Size(800, 371);
            tlpCredenciales.TabIndex = 0;
            // 
            // lblCredencial
            // 
            lblCredencial.Dock = DockStyle.Fill;
            lblCredencial.Font = new Font("Segoe UI", 20F);
            lblCredencial.Location = new Point(3, 0);
            lblCredencial.Name = "lblCredencial";
            lblCredencial.Size = new Size(794, 71);
            lblCredencial.TabIndex = 0;
            lblCredencial.Text = "Credencial";
            lblCredencial.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Font = new Font("Segoe UI", 20F);
            lblPassword.Location = new Point(3, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(794, 71);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "Contraseña";
            lblPassword.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txbCredencial
            // 
            txbCredencial.Dock = DockStyle.Fill;
            txbCredencial.Font = new Font("Segoe UI", 20F);
            txbCredencial.Location = new Point(3, 74);
            txbCredencial.Name = "txbCredencial";
            txbCredencial.Size = new Size(794, 43);
            txbCredencial.TabIndex = 1;
            txbCredencial.TextAlign = HorizontalAlignment.Center;
            // 
            // txbPassword
            // 
            txbPassword.Dock = DockStyle.Fill;
            txbPassword.Font = new Font("Segoe UI", 20F);
            txbPassword.Location = new Point(3, 194);
            txbPassword.Name = "txbPassword";
            txbPassword.Size = new Size(794, 43);
            txbPassword.TabIndex = 2;
            txbPassword.TextAlign = HorizontalAlignment.Center;
            txbPassword.UseSystemPasswordChar = true;
            txbPassword.KeyDown += txbPassword_KeyDown;
            // 
            // btnAcceder
            // 
            btnAcceder.BackColor = Color.FromArgb(159, 34, 65);
            btnAcceder.Dock = DockStyle.Top;
            btnAcceder.FlatStyle = FlatStyle.Flat;
            btnAcceder.Font = new Font("Segoe UI", 18F);
            btnAcceder.ForeColor = Color.White;
            btnAcceder.Location = new Point(3, 243);
            btnAcceder.Name = "btnAcceder";
            btnAcceder.Size = new Size(794, 125);
            btnAcceder.TabIndex = 3;
            btnAcceder.Text = "Acceder";
            btnAcceder.UseVisualStyleBackColor = false;
            btnAcceder.Click += btnAcceder_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = SystemColors.Window;
            pnlHeader.Controls.Add(lblTituloLogin);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(800, 79);
            pnlHeader.TabIndex = 1;
            // 
            // lblTituloLogin
            // 
            lblTituloLogin.BackColor = Color.Transparent;
            lblTituloLogin.Dock = DockStyle.Fill;
            lblTituloLogin.Font = new Font("Segoe UI", 48F);
            lblTituloLogin.ForeColor = Color.FromArgb(159, 34, 65);
            lblTituloLogin.Location = new Point(0, 0);
            lblTituloLogin.Name = "lblTituloLogin";
            lblTituloLogin.Size = new Size(800, 79);
            lblTituloLogin.TabIndex = 0;
            lblTituloLogin.Text = "INICIAR SESIÓN";
            lblTituloLogin.TextAlign = ContentAlignment.TopCenter;
            // 
            // frmAuth
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlPrincipal);
            Name = "frmAuth";
            Text = "frmAuth";
            Load += frmAuth_Load;
            pnlPrincipal.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tlpCredenciales.ResumeLayout(false);
            tlpCredenciales.PerformLayout();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private TableLayoutPanel tlpCredenciales;
        private Panel pnlHeader;
        private Button btnAcceder;
        private Label lblTituloLogin;
        private Label lblCredencial;
        private Label lblPassword;
        private TextBox txbPassword;
        private Panel panel1;
        public TextBox txbCredencial;
    }
}