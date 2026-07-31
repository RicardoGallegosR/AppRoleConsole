namespace FrmComun.FrmUtils {
    partial class frmMensajeResultadoOBD {
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
            splitContainer1 = new SplitContainer();
            btnCerrar = new Button();
            splitContainer2 = new SplitContainer();
            pnlTitulo = new Panel();
            lblTitulo = new Label();
            pnlResumen = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            _lblDTCPendientes = new Label();
            _lblDTCConfirmado = new Label();
            _lblProtocolo = new Label();
            lblProtocolo = new Label();
            lblDTCPendientes = new Label();
            lblDTCConfirmado = new Label();
            _lblModelo = new Label();
            lblModelo = new Label();
            _lblSubMarca = new Label();
            lblSubMarca = new Label();
            _lblMarca = new Label();
            lblMarca = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            pnlTitulo.SuspendLayout();
            pnlResumen.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(834, 406);
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
            splitContainer1.Size = new Size(834, 406);
            splitContainer1.SplitterDistance = 98;
            splitContainer1.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Crimson;
            btnCerrar.Dock = DockStyle.Fill;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 20F);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(0, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(98, 406);
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
            splitContainer2.Panel1.Controls.Add(pnlTitulo);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(pnlResumen);
            splitContainer2.Size = new Size(732, 406);
            splitContainer2.SplitterDistance = 94;
            splitContainer2.TabIndex = 0;
            // 
            // pnlTitulo
            // 
            pnlTitulo.Controls.Add(lblTitulo);
            pnlTitulo.Dock = DockStyle.Fill;
            pnlTitulo.Location = new Point(0, 0);
            pnlTitulo.Name = "pnlTitulo";
            pnlTitulo.Size = new Size(732, 94);
            pnlTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(732, 94);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Titulo";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlResumen
            // 
            pnlResumen.BackColor = Color.White;
            pnlResumen.Controls.Add(tableLayoutPanel2);
            pnlResumen.Dock = DockStyle.Fill;
            pnlResumen.Location = new Point(0, 0);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new Size(732, 308);
            pnlResumen.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(_lblDTCPendientes, 1, 4);
            tableLayoutPanel2.Controls.Add(_lblDTCConfirmado, 1, 3);
            tableLayoutPanel2.Controls.Add(_lblProtocolo, 1, 5);
            tableLayoutPanel2.Controls.Add(lblProtocolo, 0, 5);
            tableLayoutPanel2.Controls.Add(lblDTCPendientes, 0, 4);
            tableLayoutPanel2.Controls.Add(lblDTCConfirmado, 0, 3);
            tableLayoutPanel2.Controls.Add(_lblModelo, 1, 2);
            tableLayoutPanel2.Controls.Add(lblModelo, 0, 2);
            tableLayoutPanel2.Controls.Add(_lblSubMarca, 1, 1);
            tableLayoutPanel2.Controls.Add(lblSubMarca, 0, 1);
            tableLayoutPanel2.Controls.Add(_lblMarca, 1, 0);
            tableLayoutPanel2.Controls.Add(lblMarca, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.Size = new Size(732, 308);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // _lblDTCPendientes
            // 
            _lblDTCPendientes.AutoSize = true;
            _lblDTCPendientes.Dock = DockStyle.Fill;
            _lblDTCPendientes.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblDTCPendientes.Location = new Point(369, 204);
            _lblDTCPendientes.Name = "_lblDTCPendientes";
            _lblDTCPendientes.Size = new Size(360, 51);
            _lblDTCPendientes.TabIndex = 13;
            _lblDTCPendientes.Text = "DESCONOCIDO";
            _lblDTCPendientes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _lblDTCConfirmado
            // 
            _lblDTCConfirmado.AutoSize = true;
            _lblDTCConfirmado.Dock = DockStyle.Fill;
            _lblDTCConfirmado.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblDTCConfirmado.Location = new Point(369, 153);
            _lblDTCConfirmado.Name = "_lblDTCConfirmado";
            _lblDTCConfirmado.Size = new Size(360, 51);
            _lblDTCConfirmado.TabIndex = 12;
            _lblDTCConfirmado.Text = "DESCONOCIDO";
            _lblDTCConfirmado.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _lblProtocolo
            // 
            _lblProtocolo.AutoSize = true;
            _lblProtocolo.Dock = DockStyle.Fill;
            _lblProtocolo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblProtocolo.Location = new Point(369, 255);
            _lblProtocolo.Name = "_lblProtocolo";
            _lblProtocolo.Size = new Size(360, 53);
            _lblProtocolo.TabIndex = 11;
            _lblProtocolo.Text = "DESCONOCIDO";
            _lblProtocolo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProtocolo
            // 
            lblProtocolo.AutoSize = true;
            lblProtocolo.Dock = DockStyle.Fill;
            lblProtocolo.Font = new Font("Segoe UI", 16F);
            lblProtocolo.Location = new Point(3, 255);
            lblProtocolo.Name = "lblProtocolo";
            lblProtocolo.Size = new Size(360, 53);
            lblProtocolo.TabIndex = 10;
            lblProtocolo.Text = "Protocolo OBD";
            lblProtocolo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDTCPendientes
            // 
            lblDTCPendientes.AutoSize = true;
            lblDTCPendientes.Dock = DockStyle.Fill;
            lblDTCPendientes.Font = new Font("Segoe UI", 16F);
            lblDTCPendientes.Location = new Point(3, 204);
            lblDTCPendientes.Name = "lblDTCPendientes";
            lblDTCPendientes.Size = new Size(360, 51);
            lblDTCPendientes.TabIndex = 8;
            lblDTCPendientes.Text = "DTC's Pendientes";
            lblDTCPendientes.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDTCConfirmado
            // 
            lblDTCConfirmado.AutoSize = true;
            lblDTCConfirmado.Dock = DockStyle.Fill;
            lblDTCConfirmado.Font = new Font("Segoe UI", 16F);
            lblDTCConfirmado.Location = new Point(3, 153);
            lblDTCConfirmado.Name = "lblDTCConfirmado";
            lblDTCConfirmado.Size = new Size(360, 51);
            lblDTCConfirmado.TabIndex = 6;
            lblDTCConfirmado.Text = "DTC's Confirmados";
            lblDTCConfirmado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblModelo
            // 
            _lblModelo.AutoSize = true;
            _lblModelo.Dock = DockStyle.Fill;
            _lblModelo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblModelo.Location = new Point(369, 102);
            _lblModelo.Name = "_lblModelo";
            _lblModelo.Size = new Size(360, 51);
            _lblModelo.TabIndex = 5;
            _lblModelo.Text = "DESCONOCIDO";
            _lblModelo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Dock = DockStyle.Fill;
            lblModelo.Font = new Font("Segoe UI", 16F);
            lblModelo.Location = new Point(3, 102);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(360, 51);
            lblModelo.TabIndex = 4;
            lblModelo.Text = "Modelo";
            lblModelo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblSubMarca
            // 
            _lblSubMarca.AutoSize = true;
            _lblSubMarca.Dock = DockStyle.Fill;
            _lblSubMarca.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblSubMarca.Location = new Point(369, 51);
            _lblSubMarca.Name = "_lblSubMarca";
            _lblSubMarca.Size = new Size(360, 51);
            _lblSubMarca.TabIndex = 3;
            _lblSubMarca.Text = "DESCONOCIDO";
            _lblSubMarca.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubMarca
            // 
            lblSubMarca.AutoSize = true;
            lblSubMarca.Dock = DockStyle.Fill;
            lblSubMarca.Font = new Font("Segoe UI", 16F);
            lblSubMarca.Location = new Point(3, 51);
            lblSubMarca.Name = "lblSubMarca";
            lblSubMarca.Size = new Size(360, 51);
            lblSubMarca.TabIndex = 2;
            lblSubMarca.Text = "SubMarca";
            lblSubMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblMarca
            // 
            _lblMarca.AutoSize = true;
            _lblMarca.Dock = DockStyle.Fill;
            _lblMarca.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblMarca.Location = new Point(369, 0);
            _lblMarca.Name = "_lblMarca";
            _lblMarca.Size = new Size(360, 51);
            _lblMarca.TabIndex = 1;
            _lblMarca.Text = "DESCONOCIDO";
            _lblMarca.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 16F);
            lblMarca.Location = new Point(3, 0);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(360, 51);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "Marca";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmMensajeResultadoOBD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(834, 406);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "frmMensajeResultadoOBD";
            ShowIcon = false;
            ShowInTaskbar = false;
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
            pnlTitulo.ResumeLayout(false);
            pnlResumen.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private SplitContainer splitContainer1;
        private Button btnCerrar;
        private SplitContainer splitContainer2;
        private Panel pnlTitulo;
        private Label lblTitulo;
        private Panel pnlResumen;
        private TableLayoutPanel tableLayoutPanel2;
        private Label _lblDTCPendientes;
        private Label _lblDTCConfirmado;
        private Label _lblProtocolo;
        private Label lblProtocolo;
        private Label lblDTCPendientes;
        private Label lblDTCConfirmado;
        private Label _lblModelo;
        private Label lblModelo;
        private Label _lblSubMarca;
        private Label lblSubMarca;
        private Label _lblMarca;
        private Label lblMarca;
    }
}