namespace Apps_Visual.ObdAppGUI.Views {
    partial class frmMensajes {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMensajes));
            pnlPrincipal = new Panel();
            pnlMenu = new Panel();
            btnCerrar = new Button();
            txbMensaje = new TextBox();
            pnlPrincipal.SuspendLayout();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(pnlMenu);
            pnlPrincipal.Controls.Add(txbMensaje);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(159, 34, 65);
            pnlMenu.Controls.Add(btnCerrar);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(116, 450);
            pnlMenu.TabIndex = 1;
            // 
            // btnCerrar
            // 
            btnCerrar.AutoSize = true;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 20F);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(0, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(116, 450);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // txbMensaje
            // 
            txbMensaje.BackColor = Color.White;
            txbMensaje.CausesValidation = false;
            txbMensaje.Dock = DockStyle.Right;
            txbMensaje.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txbMensaje.Location = new Point(135, 0);
            txbMensaje.Multiline = true;
            txbMensaje.Name = "txbMensaje";
            txbMensaje.ReadOnly = true;
            txbMensaje.RightToLeft = RightToLeft.No;
            txbMensaje.ScrollBars = ScrollBars.Vertical;
            txbMensaje.Size = new Size(665, 450);
            txbMensaje.TabIndex = 0;
            txbMensaje.TabStop = false;
            txbMensaje.Text = resources.GetString("txbMensaje.Text");
            txbMensaje.TextAlign = HorizontalAlignment.Center;
            // 
            // frmMensajes
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "frmMensajes";
            StartPosition = FormStartPosition.CenterScreen;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlMenu;
        private Button btnCerrar;
        private TextBox txbMensaje;
    }
}