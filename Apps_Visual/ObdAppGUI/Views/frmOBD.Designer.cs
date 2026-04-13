namespace Apps_Visual.ObdAppGUI.Views {
    partial class frmOBD {
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
            pnlTopPrincipal = new Panel();
            lblLecturaOBD = new Label();
            btnConectar = new Button();
            pnlPrincipal = new Panel();
            pnlBoton = new Panel();
            lblReporte = new Label();
            pbLecturaObd = new ProgressBar();
            pnlTopPrincipal.SuspendLayout();
            pnlPrincipal.SuspendLayout();
            pnlBoton.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopPrincipal
            // 
            pnlTopPrincipal.BackColor = Color.White;
            pnlTopPrincipal.Controls.Add(lblLecturaOBD);
            pnlTopPrincipal.Dock = DockStyle.Top;
            pnlTopPrincipal.Location = new Point(0, 0);
            pnlTopPrincipal.Name = "pnlTopPrincipal";
            pnlTopPrincipal.Size = new Size(840, 126);
            pnlTopPrincipal.TabIndex = 0;
            // 
            // lblLecturaOBD
            // 
            lblLecturaOBD.Dock = DockStyle.Fill;
            lblLecturaOBD.FlatStyle = FlatStyle.Flat;
            lblLecturaOBD.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblLecturaOBD.ForeColor = Color.Black;
            lblLecturaOBD.ImageAlign = ContentAlignment.BottomLeft;
            lblLecturaOBD.Location = new Point(0, 0);
            lblLecturaOBD.Name = "lblLecturaOBD";
            lblLecturaOBD.Size = new Size(840, 126);
            lblLecturaOBD.TabIndex = 0;
            lblLecturaOBD.Text = "Diagnóstico OBD";
            lblLecturaOBD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConectar
            // 
            btnConectar.Dock = DockStyle.Right;
            btnConectar.FlatAppearance.BorderSize = 2;
            btnConectar.FlatStyle = FlatStyle.Flat;
            btnConectar.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConectar.ForeColor = Color.FromArgb(159, 34, 65);
            btnConectar.Location = new Point(543, 0);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(297, 100);
            btnConectar.TabIndex = 1;
            btnConectar.Text = "C O N E C T A R";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(pnlBoton);
            pnlPrincipal.Controls.Add(lblReporte);
            pnlPrincipal.Controls.Add(pbLecturaObd);
            pnlPrincipal.Controls.Add(pnlTopPrincipal);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(840, 516);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlBoton
            // 
            pnlBoton.Controls.Add(btnConectar);
            pnlBoton.Dock = DockStyle.Bottom;
            pnlBoton.Location = new Point(0, 393);
            pnlBoton.Name = "pnlBoton";
            pnlBoton.Size = new Size(840, 100);
            pnlBoton.TabIndex = 2;
            // 
            // lblReporte
            // 
            lblReporte.Dock = DockStyle.Fill;
            lblReporte.Font = new Font("Segoe UI", 28F);
            lblReporte.ImageAlign = ContentAlignment.MiddleLeft;
            lblReporte.Location = new Point(0, 126);
            lblReporte.Name = "lblReporte";
            lblReporte.Size = new Size(840, 367);
            lblReporte.TabIndex = 0;
            lblReporte.Text = "Conecte el escaner SBD en el vehículo.\r\nUna vez conectado presiona el botón conectar. :D";
            lblReporte.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pbLecturaObd
            // 
            pbLecturaObd.Dock = DockStyle.Bottom;
            pbLecturaObd.Location = new Point(0, 493);
            pbLecturaObd.Name = "pbLecturaObd";
            pbLecturaObd.Size = new Size(840, 23);
            pbLecturaObd.TabIndex = 0;
            // 
            // frmOBD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 516);
            Controls.Add(pnlPrincipal);
            Name = "frmOBD";
            Text = "frmOBD";
            Load += frmOBD_Load;
            pnlTopPrincipal.ResumeLayout(false);
            pnlPrincipal.ResumeLayout(false);
            pnlBoton.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lblrRunTimeMillblrModoALista;
        private Panel pnlTopPrincipal;
        public Button btnConectar;
        private Label lblLecturaOBD;
        private Panel pnlPrincipal;
        private Label lblReporte;
        private ProgressBar pbLecturaObd;
        private Panel pnlBoton;
    }
}