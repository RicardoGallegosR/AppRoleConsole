namespace FrmComun.FrmUtils {
    partial class frmPreguntasSiNo {
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
            pnlBody = new Panel();
            lblPregunta = new Label();
            pnlFoother = new Panel();
            flpBotones = new FlowLayoutPanel();
            btnNo = new Button();
            btnSi = new Button();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            pnlPrincipal.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlFoother.SuspendLayout();
            flpBotones.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(pnlBody);
            pnlPrincipal.Controls.Add(pnlFoother);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Padding = new Padding(7);
            pnlPrincipal.Size = new Size(800, 450);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlBody
            // 
            pnlBody.Controls.Add(lblPregunta);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(7, 81);
            pnlBody.Name = "pnlBody";
            pnlBody.Size = new Size(786, 292);
            pnlBody.TabIndex = 2;
            // 
            // lblPregunta
            // 
            lblPregunta.BackColor = Color.White;
            lblPregunta.Dock = DockStyle.Fill;
            lblPregunta.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPregunta.Location = new Point(0, 0);
            lblPregunta.Name = "lblPregunta";
            lblPregunta.Size = new Size(786, 292);
            lblPregunta.TabIndex = 0;
            lblPregunta.Text = "Pregunta?";
            lblPregunta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFoother
            // 
            pnlFoother.Controls.Add(flpBotones);
            pnlFoother.Dock = DockStyle.Bottom;
            pnlFoother.Location = new Point(7, 373);
            pnlFoother.Name = "pnlFoother";
            pnlFoother.Size = new Size(786, 70);
            pnlFoother.TabIndex = 1;
            // 
            // flpBotones
            // 
            flpBotones.BackColor = Color.White;
            flpBotones.Controls.Add(btnNo);
            flpBotones.Controls.Add(btnSi);
            flpBotones.Dock = DockStyle.Fill;
            flpBotones.FlowDirection = FlowDirection.RightToLeft;
            flpBotones.Location = new Point(0, 0);
            flpBotones.Name = "flpBotones";
            flpBotones.Size = new Size(786, 70);
            flpBotones.TabIndex = 0;
            flpBotones.WrapContents = false;
            // 
            // btnNo
            // 
            btnNo.BackColor = Color.Crimson;
            btnNo.FlatAppearance.BorderColor = Color.Black;
            btnNo.FlatStyle = FlatStyle.Flat;
            btnNo.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnNo.ForeColor = Color.White;
            btnNo.Location = new Point(561, 15);
            btnNo.Margin = new Padding(15);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(210, 40);
            btnNo.TabIndex = 1;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = false;
            // 
            // btnSi
            // 
            btnSi.FlatAppearance.BorderColor = Color.Crimson;
            btnSi.FlatStyle = FlatStyle.Flat;
            btnSi.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSi.Location = new Point(339, 15);
            btnSi.Margin = new Padding(15);
            btnSi.Name = "btnSi";
            btnSi.Size = new Size(192, 40);
            btnSi.TabIndex = 2;
            btnSi.Text = "Si";
            btnSi.UseVisualStyleBackColor = true;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(7, 7);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(786, 74);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.White;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 36F);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(786, 74);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Titulo";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmPreguntasSiNo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            Name = "frmPreguntasSiNo";
            pnlPrincipal.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlFoother.ResumeLayout(false);
            flpBotones.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlBody;
        private Label lblPregunta;
        private Panel pnlFoother;
        private Panel pnlHeader;
        private Label lblTitulo;
        private FlowLayoutPanel flpBotones;
        private Button btnNo;
        private Button btnSi;
    }
}