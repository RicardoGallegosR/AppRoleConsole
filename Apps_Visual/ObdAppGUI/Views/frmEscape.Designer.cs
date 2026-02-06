namespace Apps_Visual.ObdAppGUI.Views {
    partial class frmEscape {
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
            pnlTexto = new Panel();
            lblMensaje = new Label();
            pnlFoother = new Panel();
            btnNo = new Button();
            btnSi = new Button();
            pnlPrincipal.SuspendLayout();
            pnlTexto.SuspendLayout();
            pnlFoother.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = SystemColors.Window;
            pnlPrincipal.Controls.Add(pnlTexto);
            pnlPrincipal.Controls.Add(pnlFoother);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Font = new Font("Segoe UI", 15.75F);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(784, 411);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlTexto
            // 
            pnlTexto.Controls.Add(lblMensaje);
            pnlTexto.Dock = DockStyle.Fill;
            pnlTexto.Location = new Point(0, 0);
            pnlTexto.Name = "pnlTexto";
            pnlTexto.Size = new Size(784, 311);
            pnlTexto.TabIndex = 1;
            // 
            // lblMensaje
            // 
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.FlatStyle = FlatStyle.Flat;
            lblMensaje.Font = new Font("Segoe UI", 18F);
            lblMensaje.Location = new Point(0, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(784, 311);
            lblMensaje.TabIndex = 0;
            lblMensaje.Text = "¿Desea detener la captura del vehículo y salir al menú principal?";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFoother
            // 
            pnlFoother.BackColor = Color.FromArgb(159, 34, 65);
            pnlFoother.Controls.Add(btnNo);
            pnlFoother.Controls.Add(btnSi);
            pnlFoother.Dock = DockStyle.Bottom;
            pnlFoother.Location = new Point(0, 311);
            pnlFoother.Name = "pnlFoother";
            pnlFoother.Size = new Size(784, 100);
            pnlFoother.TabIndex = 0;
            // 
            // btnNo
            // 
            btnNo.BackColor = Color.FromArgb(159, 34, 65);
            btnNo.Dock = DockStyle.Right;
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.ForeColor = Color.White;
            btnNo.Location = new Point(664, 0);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(120, 100);
            btnNo.TabIndex = 1;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = false;
            btnNo.Click += btnNo_Click;
            // 
            // btnSi
            // 
            btnSi.BackColor = Color.FromArgb(159, 34, 65);
            btnSi.Dock = DockStyle.Left;
            btnSi.FlatAppearance.BorderSize = 0;
            btnSi.ForeColor = Color.White;
            btnSi.Location = new Point(0, 0);
            btnSi.Name = "btnSi";
            btnSi.Size = new Size(120, 100);
            btnSi.TabIndex = 2;
            btnSi.Text = "Sí";
            btnSi.UseVisualStyleBackColor = false;
            btnSi.Click += btnSi_Click;
            // 
            // frmEscape
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 411);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            Name = "frmEscape";
            StartPosition = FormStartPosition.CenterScreen;
            pnlPrincipal.ResumeLayout(false);
            pnlTexto.ResumeLayout(false);
            pnlFoother.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlFoother;
        private Panel pnlTexto;
        private Button btnSi;
        private Label lblMensaje;
        private Button btnNo;
    }
}