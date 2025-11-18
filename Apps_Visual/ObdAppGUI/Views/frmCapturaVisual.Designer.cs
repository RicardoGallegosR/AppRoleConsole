namespace Apps_Visual.ObdAppGUI.Views {
    partial class frmCapturaVisual {
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
            pnlCentral = new Panel();
            spcPrincipal = new SplitContainer();
            tlpTabla = new TableLayoutPanel();
            cbMotorGobernado = new CheckBox();
            cbComponentesEmisiones = new CheckBox();
            cbNeumaticos = new CheckBox();
            cbFugasMotorTrans = new CheckBox();
            cbTuboEscape = new CheckBox();
            cbPortaFiltroAire = new CheckBox();
            cbBayonetaAceite = new CheckBox();
            cbTaponAceite = new CheckBox();
            cbTaponCombustible = new CheckBox();
            lblPlaca = new Label();
            pnlCentralFooter = new Panel();
            btnSiguente = new Button();
            pnlTopListadoVisual = new Panel();
            lblTitulo = new Label();
            pnlPrincipal.SuspendLayout();
            pnlCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spcPrincipal).BeginInit();
            spcPrincipal.Panel1.SuspendLayout();
            spcPrincipal.Panel2.SuspendLayout();
            spcPrincipal.SuspendLayout();
            tlpTabla.SuspendLayout();
            pnlCentralFooter.SuspendLayout();
            pnlTopListadoVisual.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(pnlCentral);
            pnlPrincipal.Controls.Add(pnlTopListadoVisual);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(800, 534);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlCentral
            // 
            pnlCentral.BackColor = SystemColors.MenuHighlight;
            pnlCentral.Controls.Add(spcPrincipal);
            pnlCentral.Controls.Add(pnlCentralFooter);
            pnlCentral.Dock = DockStyle.Fill;
            pnlCentral.Location = new Point(0, 88);
            pnlCentral.Name = "pnlCentral";
            pnlCentral.Size = new Size(800, 446);
            pnlCentral.TabIndex = 1;
            // 
            // spcPrincipal
            // 
            spcPrincipal.BackColor = Color.White;
            spcPrincipal.Dock = DockStyle.Fill;
            spcPrincipal.Location = new Point(0, 0);
            spcPrincipal.Name = "spcPrincipal";
            // 
            // spcPrincipal.Panel1
            // 
            spcPrincipal.Panel1.Controls.Add(tlpTabla);
            // 
            // spcPrincipal.Panel2
            // 
            spcPrincipal.Panel2.BackColor = Color.White;
            spcPrincipal.Panel2.Controls.Add(lblPlaca);
            spcPrincipal.Size = new Size(800, 395);
            spcPrincipal.SplitterDistance = 548;
            spcPrincipal.TabIndex = 2;
            // 
            // tlpTabla
            // 
            tlpTabla.ColumnCount = 1;
            tlpTabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTabla.Controls.Add(cbMotorGobernado, 0, 8);
            tlpTabla.Controls.Add(cbComponentesEmisiones, 0, 7);
            tlpTabla.Controls.Add(cbNeumaticos, 0, 6);
            tlpTabla.Controls.Add(cbFugasMotorTrans, 0, 5);
            tlpTabla.Controls.Add(cbTuboEscape, 0, 4);
            tlpTabla.Controls.Add(cbPortaFiltroAire, 0, 3);
            tlpTabla.Controls.Add(cbBayonetaAceite, 0, 2);
            tlpTabla.Controls.Add(cbTaponAceite, 0, 1);
            tlpTabla.Controls.Add(cbTaponCombustible, 0, 0);
            tlpTabla.Dock = DockStyle.Fill;
            tlpTabla.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tlpTabla.Location = new Point(0, 0);
            tlpTabla.Name = "tlpTabla";
            tlpTabla.RowCount = 9;
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 11.261261F));
            tlpTabla.RowStyles.Add(new RowStyle(SizeType.Percent, 9.90991F));
            tlpTabla.Size = new Size(548, 395);
            tlpTabla.TabIndex = 0;
            // 
            // cbMotorGobernado
            // 
            cbMotorGobernado.AutoSize = true;
            cbMotorGobernado.Dock = DockStyle.Fill;
            cbMotorGobernado.ForeColor = Color.FromArgb(159, 34, 65);
            cbMotorGobernado.Location = new Point(3, 355);
            cbMotorGobernado.Name = "cbMotorGobernado";
            cbMotorGobernado.Size = new Size(542, 37);
            cbMotorGobernado.TabIndex = 9;
            cbMotorGobernado.Text = "No Contiene Motor Gobernado";
            cbMotorGobernado.UseVisualStyleBackColor = true;
            // 
            // cbComponentesEmisiones
            // 
            cbComponentesEmisiones.AutoSize = true;
            cbComponentesEmisiones.Dock = DockStyle.Fill;
            cbComponentesEmisiones.ForeColor = Color.FromArgb(159, 34, 65);
            cbComponentesEmisiones.Location = new Point(3, 311);
            cbComponentesEmisiones.Name = "cbComponentesEmisiones";
            cbComponentesEmisiones.Size = new Size(542, 38);
            cbComponentesEmisiones.TabIndex = 8;
            cbComponentesEmisiones.Text = "No Contiene Componentes de Emisiones";
            cbComponentesEmisiones.UseVisualStyleBackColor = true;
            // 
            // cbNeumaticos
            // 
            cbNeumaticos.AutoSize = true;
            cbNeumaticos.Dock = DockStyle.Fill;
            cbNeumaticos.ForeColor = Color.FromArgb(159, 34, 65);
            cbNeumaticos.Location = new Point(3, 267);
            cbNeumaticos.Name = "cbNeumaticos";
            cbNeumaticos.Size = new Size(542, 38);
            cbNeumaticos.TabIndex = 7;
            cbNeumaticos.Text = "No Contiene Neumaticos ";
            cbNeumaticos.UseVisualStyleBackColor = true;
            // 
            // cbFugasMotorTrans
            // 
            cbFugasMotorTrans.AutoSize = true;
            cbFugasMotorTrans.Dock = DockStyle.Fill;
            cbFugasMotorTrans.ForeColor = Color.FromArgb(159, 34, 65);
            cbFugasMotorTrans.Location = new Point(3, 223);
            cbFugasMotorTrans.Name = "cbFugasMotorTrans";
            cbFugasMotorTrans.Size = new Size(542, 38);
            cbFugasMotorTrans.TabIndex = 6;
            cbFugasMotorTrans.Text = "No Contiene  Fugas de Motor de transmisión";
            cbFugasMotorTrans.UseVisualStyleBackColor = true;
            // 
            // cbTuboEscape
            // 
            cbTuboEscape.AutoSize = true;
            cbTuboEscape.Dock = DockStyle.Fill;
            cbTuboEscape.ForeColor = Color.FromArgb(159, 34, 65);
            cbTuboEscape.Location = new Point(3, 179);
            cbTuboEscape.Name = "cbTuboEscape";
            cbTuboEscape.Size = new Size(542, 38);
            cbTuboEscape.TabIndex = 5;
            cbTuboEscape.Text = "No Contiene Sistema de Escape";
            cbTuboEscape.UseVisualStyleBackColor = true;
            // 
            // cbPortaFiltroAire
            // 
            cbPortaFiltroAire.AutoSize = true;
            cbPortaFiltroAire.Dock = DockStyle.Fill;
            cbPortaFiltroAire.ForeColor = Color.FromArgb(159, 34, 65);
            cbPortaFiltroAire.Location = new Point(3, 135);
            cbPortaFiltroAire.Name = "cbPortaFiltroAire";
            cbPortaFiltroAire.Size = new Size(542, 38);
            cbPortaFiltroAire.TabIndex = 4;
            cbPortaFiltroAire.Text = "No Contiene Porta Filtro de Aire";
            cbPortaFiltroAire.UseVisualStyleBackColor = true;
            // 
            // cbBayonetaAceite
            // 
            cbBayonetaAceite.AutoSize = true;
            cbBayonetaAceite.Dock = DockStyle.Fill;
            cbBayonetaAceite.ForeColor = Color.FromArgb(159, 34, 65);
            cbBayonetaAceite.Location = new Point(3, 91);
            cbBayonetaAceite.Name = "cbBayonetaAceite";
            cbBayonetaAceite.Size = new Size(542, 38);
            cbBayonetaAceite.TabIndex = 3;
            cbBayonetaAceite.Text = "No Contiene Bayoneta de Aceite";
            cbBayonetaAceite.UseVisualStyleBackColor = true;
            // 
            // cbTaponAceite
            // 
            cbTaponAceite.AutoSize = true;
            cbTaponAceite.Dock = DockStyle.Fill;
            cbTaponAceite.ForeColor = Color.FromArgb(159, 34, 65);
            cbTaponAceite.Location = new Point(3, 47);
            cbTaponAceite.Name = "cbTaponAceite";
            cbTaponAceite.Size = new Size(542, 38);
            cbTaponAceite.TabIndex = 2;
            cbTaponAceite.Text = "No Contiene Tapon de Aceite";
            cbTaponAceite.UseVisualStyleBackColor = true;
            // 
            // cbTaponCombustible
            // 
            cbTaponCombustible.AutoSize = true;
            cbTaponCombustible.Dock = DockStyle.Fill;
            cbTaponCombustible.ForeColor = Color.FromArgb(159, 34, 65);
            cbTaponCombustible.Location = new Point(3, 3);
            cbTaponCombustible.Name = "cbTaponCombustible";
            cbTaponCombustible.Size = new Size(542, 38);
            cbTaponCombustible.TabIndex = 1;
            cbTaponCombustible.Text = "No Contiene Tapon de Combustible";
            cbTaponCombustible.UseVisualStyleBackColor = true;
            // 
            // lblPlaca
            // 
            lblPlaca.Dock = DockStyle.Fill;
            lblPlaca.FlatStyle = FlatStyle.Flat;
            lblPlaca.Font = new Font("Segoe UI", 36F);
            lblPlaca.ForeColor = Color.FromArgb(159, 34, 65);
            lblPlaca.Location = new Point(0, 0);
            lblPlaca.Name = "lblPlaca";
            lblPlaca.Size = new Size(248, 395);
            lblPlaca.TabIndex = 0;
            lblPlaca.Text = "PlacaID";
            // 
            // pnlCentralFooter
            // 
            pnlCentralFooter.BackColor = Color.White;
            pnlCentralFooter.Controls.Add(btnSiguente);
            pnlCentralFooter.Dock = DockStyle.Bottom;
            pnlCentralFooter.Location = new Point(0, 395);
            pnlCentralFooter.Name = "pnlCentralFooter";
            pnlCentralFooter.Size = new Size(800, 51);
            pnlCentralFooter.TabIndex = 1;
            // 
            // btnSiguente
            // 
            btnSiguente.Dock = DockStyle.Right;
            btnSiguente.FlatAppearance.BorderSize = 0;
            btnSiguente.FlatStyle = FlatStyle.Flat;
            btnSiguente.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguente.ForeColor = Color.FromArgb(159, 34, 65);
            btnSiguente.Location = new Point(661, 0);
            btnSiguente.Name = "btnSiguente";
            btnSiguente.Size = new Size(139, 51);
            btnSiguente.TabIndex = 10;
            btnSiguente.Text = "Siguiente";
            btnSiguente.UseVisualStyleBackColor = true;
            // 
            // pnlTopListadoVisual
            // 
            pnlTopListadoVisual.BackColor = Color.White;
            pnlTopListadoVisual.Controls.Add(lblTitulo);
            pnlTopListadoVisual.Dock = DockStyle.Top;
            pnlTopListadoVisual.Location = new Point(0, 0);
            pnlTopListadoVisual.Name = "pnlTopListadoVisual";
            pnlTopListadoVisual.Size = new Size(800, 88);
            pnlTopListadoVisual.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 48F);
            lblTitulo.ForeColor = Color.FromArgb(159, 34, 65);
            lblTitulo.Location = new Point(159, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(524, 86);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Inspección Visual";
            // 
            // frmCapturaVisual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 534);
            Controls.Add(pnlPrincipal);
            Name = "frmCapturaVisual";
            Text = "frmCapturaVisual";
            Load += frmCapturaVisual_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlCentral.ResumeLayout(false);
            spcPrincipal.Panel1.ResumeLayout(false);
            spcPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spcPrincipal).EndInit();
            spcPrincipal.ResumeLayout(false);
            tlpTabla.ResumeLayout(false);
            tlpTabla.PerformLayout();
            pnlCentralFooter.ResumeLayout(false);
            pnlTopListadoVisual.ResumeLayout(false);
            pnlTopListadoVisual.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlTopListadoVisual;
        private Label lblTitulo;
        private Panel pnlCentral;
        private Panel pnlCentralFooter;
        private SplitContainer spcPrincipal;
        private Label lblPlaca;
        private Button btnSiguente;
        private TableLayoutPanel tlpTabla;
        private CheckBox cbTaponCombustible;
        private CheckBox cbTaponAceite;
        private CheckBox cbComponentesEmisiones;
        private CheckBox cbNeumaticos;
        private CheckBox cbFugasMotorTrans;
        private CheckBox cbTuboEscape;
        private CheckBox cbPortaFiltroAire;
        private CheckBox cbBayonetaAceite;
        private CheckBox cbMotorGobernado;
    }
}