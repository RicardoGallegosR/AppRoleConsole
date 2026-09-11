namespace FrmComun.CapturaCentralizada.Complementos {
    partial class ucVisitante {
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
            tlpVisitante = new TableLayoutPanel();
            txtApellidoM = new TextBox();
            txtApellidoP = new TextBox();
            lblApellidoM = new Label();
            lblApellidoP = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            pblFooter = new Panel();
            flpVisita = new FlowLayoutPanel();
            btnAcceso = new Button();
            pnlPrincipal.SuspendLayout();
            tlpVisitante.SuspendLayout();
            pblFooter.SuspendLayout();
            flpVisita.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(tlpVisitante);
            pnlPrincipal.Controls.Add(pblFooter);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.ForeColor = Color.FromArgb(45, 55, 65);
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(368, 189);
            pnlPrincipal.TabIndex = 0;
            // 
            // tlpVisitante
            // 
            tlpVisitante.ColumnCount = 2;
            tlpVisitante.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVisitante.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpVisitante.Controls.Add(txtApellidoM, 1, 2);
            tlpVisitante.Controls.Add(txtApellidoP, 1, 1);
            tlpVisitante.Controls.Add(lblApellidoM, 0, 2);
            tlpVisitante.Controls.Add(lblApellidoP, 0, 1);
            tlpVisitante.Controls.Add(lblNombre, 0, 0);
            tlpVisitante.Controls.Add(txtNombre, 1, 0);
            tlpVisitante.Dock = DockStyle.Fill;
            tlpVisitante.Location = new Point(0, 0);
            tlpVisitante.Name = "tlpVisitante";
            tlpVisitante.RowCount = 3;
            tlpVisitante.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpVisitante.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpVisitante.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpVisitante.Size = new Size(368, 139);
            tlpVisitante.TabIndex = 1;
            // 
            // txtApellidoM
            // 
            txtApellidoM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtApellidoM.Font = new Font("Segoe UI", 10.5F);
            txtApellidoM.Location = new Point(184, 102);
            txtApellidoM.Margin = new Padding(0);
            txtApellidoM.Name = "txtApellidoM";
            txtApellidoM.Size = new Size(184, 26);
            txtApellidoM.TabIndex = 3;
            // 
            // txtApellidoP
            // 
            txtApellidoP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtApellidoP.Font = new Font("Segoe UI", 10.5F);
            txtApellidoP.Location = new Point(184, 56);
            txtApellidoP.Margin = new Padding(0);
            txtApellidoP.Name = "txtApellidoP";
            txtApellidoP.Size = new Size(184, 26);
            txtApellidoP.TabIndex = 2;
            // 
            // lblApellidoM
            // 
            lblApellidoM.Dock = DockStyle.Fill;
            lblApellidoM.Font = new Font("Segoe UI", 10.5F);
            lblApellidoM.Location = new Point(3, 92);
            lblApellidoM.Name = "lblApellidoM";
            lblApellidoM.Size = new Size(178, 47);
            lblApellidoM.TabIndex = 0;
            lblApellidoM.Text = "APELLIDO MATERNO";
            lblApellidoM.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblApellidoP
            // 
            lblApellidoP.Dock = DockStyle.Fill;
            lblApellidoP.Font = new Font("Segoe UI", 10.5F);
            lblApellidoP.Location = new Point(3, 46);
            lblApellidoP.Name = "lblApellidoP";
            lblApellidoP.Size = new Size(178, 46);
            lblApellidoP.TabIndex = 0;
            lblApellidoP.Text = "APELLIDO PATERNO";
            lblApellidoP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            lblNombre.Dock = DockStyle.Fill;
            lblNombre.Font = new Font("Segoe UI", 10.5F);
            lblNombre.Location = new Point(3, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(178, 46);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "NOMBRE";
            lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNombre.Font = new Font("Segoe UI", 10.5F);
            txtNombre.Location = new Point(184, 10);
            txtNombre.Margin = new Padding(0);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(184, 26);
            txtNombre.TabIndex = 1;
            // 
            // pblFooter
            // 
            pblFooter.Controls.Add(flpVisita);
            pblFooter.Dock = DockStyle.Bottom;
            pblFooter.Location = new Point(0, 139);
            pblFooter.Name = "pblFooter";
            pblFooter.Size = new Size(368, 50);
            pblFooter.TabIndex = 0;
            // 
            // flpVisita
            // 
            flpVisita.Controls.Add(btnAcceso);
            flpVisita.Dock = DockStyle.Fill;
            flpVisita.FlowDirection = FlowDirection.RightToLeft;
            flpVisita.Location = new Point(0, 0);
            flpVisita.Margin = new Padding(1);
            flpVisita.Name = "flpVisita";
            flpVisita.Padding = new Padding(5);
            flpVisita.Size = new Size(368, 50);
            flpVisita.TabIndex = 0;
            flpVisita.WrapContents = false;
            // 
            // btnAcceso
            // 
            btnAcceso.AutoSize = true;
            btnAcceso.BackColor = Color.White;
            btnAcceso.FlatAppearance.BorderColor = Color.Crimson;
            btnAcceso.FlatStyle = FlatStyle.Flat;
            btnAcceso.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnAcceso.ForeColor = Color.Black;
            btnAcceso.Location = new Point(280, 8);
            btnAcceso.Name = "btnAcceso";
            btnAcceso.Padding = new Padding(5, 0, 0, 0);
            btnAcceso.Size = new Size(75, 34);
            btnAcceso.TabIndex = 0;
            btnAcceso.Text = "Acceso";
            btnAcceso.UseVisualStyleBackColor = false;
            // 
            // ucVisitante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "ucVisitante";
            Size = new Size(368, 189);
            pnlPrincipal.ResumeLayout(false);
            tlpVisitante.ResumeLayout(false);
            tlpVisitante.PerformLayout();
            pblFooter.ResumeLayout(false);
            flpVisita.ResumeLayout(false);
            flpVisita.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private TableLayoutPanel tlpVisitante;
        private Panel pblFooter;
        private FlowLayoutPanel flpVisita;
        private Button btnAcceso;
        private Label lblApellidoM;
        private Label lblApellidoP;
        private Label lblNombre;
        private TextBox txtNombre;
        private TextBox txtApellidoM;
        private TextBox txtApellidoP;
    }
}
