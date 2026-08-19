namespace FrmComun.FrmUtils {
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
            splitContainer1 = new SplitContainer();
            btnCerrar = new Button();
            splitContainer2 = new SplitContainer();
            lblTitulo = new Label();
            lblMensajes = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(822, 477);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnCerrar);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(822, 477);
            splitContainer1.SplitterDistance = 103;
            splitContainer1.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Crimson;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.Font = new Font("Segoe UI", 20F);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(0, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(103, 477);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(lblTitulo);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(lblMensajes);
            splitContainer2.Size = new Size(715, 477);
            splitContainer2.SplitterDistance = 86;
            splitContainer2.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 24F);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(715, 86);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "¿Qué es Lorem Ipsum?";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMensajes
            // 
            lblMensajes.Dock = DockStyle.Fill;
            lblMensajes.Font = new Font("Segoe UI", 16F);
            lblMensajes.Location = new Point(0, 0);
            lblMensajes.Name = "lblMensajes";
            lblMensajes.Size = new Size(715, 387);
            lblMensajes.TabIndex = 0;
            lblMensajes.Text = resources.GetString("lblMensajes.Text");
            // 
            // frmMensajes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(822, 477);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMensajes";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer splitContainer1;
        private Button btnCerrar;
        private SplitContainer splitContainer2;
        private Label lblTitulo;
        private Label lblMensajes;
    }
}