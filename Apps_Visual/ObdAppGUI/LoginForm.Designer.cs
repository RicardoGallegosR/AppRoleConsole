namespace Apps_Visual.ObdAppGUI {
    partial class frmBASE {
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
            lblGobiernoDeLa = new Label();
            pnlPrincipal = new Panel();
            pnlPanelCambios = new Panel();
            pnlFooter = new Panel();
            pnlCentralFooter = new Panel();
            pnlFooterCentral = new Panel();
            pnlFooterCentro = new Panel();
            lblSEDEMAFooter = new Label();
            lblDGCA = new Label();
            pnlFooterDerecho = new Panel();
            pnlSeparadorFooterGrimsonII = new Panel();
            lblVerificaciónVehicularFoother = new Label();
            pbxLogosVerificentros = new PictureBox();
            panel1 = new Panel();
            pnlFooterIquierdo = new Panel();
            lblCDMX = new Label();
            pnlSeparadorFooterGrimsonI = new Panel();
            pbxLogosSEDEMA = new PictureBox();
            pnlSeparadorFooterAmarillo = new Panel();
            pnlMenuLateral = new Panel();
            pnlLateralIzquierdoAbajo = new Panel();
            btnApagar = new Button();
            pnlLateralIzquierdoCentral = new Panel();
            btnInspecionVisual = new Button();
            pnlPrincipal.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlCentralFooter.SuspendLayout();
            pnlFooterCentral.SuspendLayout();
            pnlFooterCentro.SuspendLayout();
            pnlFooterDerecho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogosVerificentros).BeginInit();
            panel1.SuspendLayout();
            pnlFooterIquierdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogosSEDEMA).BeginInit();
            pnlMenuLateral.SuspendLayout();
            pnlLateralIzquierdoAbajo.SuspendLayout();
            pnlLateralIzquierdoCentral.SuspendLayout();
            SuspendLayout();
            // 
            // lblGobiernoDeLa
            // 
            lblGobiernoDeLa.Dock = DockStyle.Top;
            lblGobiernoDeLa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGobiernoDeLa.ForeColor = Color.FromArgb(159, 34, 65);
            lblGobiernoDeLa.Location = new Point(0, 0);
            lblGobiernoDeLa.Name = "lblGobiernoDeLa";
            lblGobiernoDeLa.Size = new Size(166, 20);
            lblGobiernoDeLa.TabIndex = 0;
            lblGobiernoDeLa.Text = "GOBIERNO  DE  LA";
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = SystemColors.MenuHighlight;
            pnlPrincipal.Controls.Add(pnlPanelCambios);
            pnlPrincipal.Controls.Add(pnlFooter);
            pnlPrincipal.Controls.Add(pnlMenuLateral);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(819, 466);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlPanelCambios
            // 
            pnlPanelCambios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPanelCambios.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlPanelCambios.BackColor = Color.White;
            pnlPanelCambios.Location = new Point(120, 0);
            pnlPanelCambios.Name = "pnlPanelCambios";
            pnlPanelCambios.Size = new Size(699, 398);
            pnlPanelCambios.TabIndex = 0;
            pnlPanelCambios.Paint += pnlPanelCambios_Paint;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(pnlCentralFooter);
            pnlFooter.Controls.Add(pnlSeparadorFooterAmarillo);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(120, 398);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(699, 68);
            pnlFooter.TabIndex = 1;
            // 
            // pnlCentralFooter
            // 
            pnlCentralFooter.BackColor = Color.DarkGray;
            pnlCentralFooter.Controls.Add(pnlFooterCentral);
            pnlCentralFooter.Dock = DockStyle.Fill;
            pnlCentralFooter.Location = new Point(0, 10);
            pnlCentralFooter.Name = "pnlCentralFooter";
            pnlCentralFooter.Size = new Size(699, 58);
            pnlCentralFooter.TabIndex = 1;
            // 
            // pnlFooterCentral
            // 
            pnlFooterCentral.BackColor = Color.WhiteSmoke;
            pnlFooterCentral.Controls.Add(pnlFooterCentro);
            pnlFooterCentral.Controls.Add(pnlFooterDerecho);
            pnlFooterCentral.Controls.Add(panel1);
            pnlFooterCentral.Dock = DockStyle.Fill;
            pnlFooterCentral.Location = new Point(0, 0);
            pnlFooterCentral.Name = "pnlFooterCentral";
            pnlFooterCentral.Size = new Size(699, 58);
            pnlFooterCentral.TabIndex = 2;
            // 
            // pnlFooterCentro
            // 
            pnlFooterCentro.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlFooterCentro.BackColor = SystemColors.Window;
            pnlFooterCentro.Controls.Add(lblSEDEMAFooter);
            pnlFooterCentro.Controls.Add(lblDGCA);
            pnlFooterCentro.Location = new Point(265, 0);
            pnlFooterCentro.Name = "pnlFooterCentro";
            pnlFooterCentro.Size = new Size(287, 58);
            pnlFooterCentro.TabIndex = 0;
            // 
            // lblSEDEMAFooter
            // 
            lblSEDEMAFooter.Dock = DockStyle.Top;
            lblSEDEMAFooter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSEDEMAFooter.ForeColor = Color.FromArgb(159, 34, 65);
            lblSEDEMAFooter.Location = new Point(0, 0);
            lblSEDEMAFooter.Name = "lblSEDEMAFooter";
            lblSEDEMAFooter.Size = new Size(287, 15);
            lblSEDEMAFooter.TabIndex = 0;
            lblSEDEMAFooter.Text = "SEDEMA";
            lblSEDEMAFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDGCA
            // 
            lblDGCA.Dock = DockStyle.Bottom;
            lblDGCA.Font = new Font("Segoe UI", 9F);
            lblDGCA.ForeColor = Color.FromArgb(159, 34, 65);
            lblDGCA.Location = new Point(0, 28);
            lblDGCA.Name = "lblDGCA";
            lblDGCA.Size = new Size(287, 30);
            lblDGCA.TabIndex = 0;
            lblDGCA.Text = "DIRECCION GENERAL \r\nDE CALIDAD DEL AIRE";
            lblDGCA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFooterDerecho
            // 
            pnlFooterDerecho.Controls.Add(pnlSeparadorFooterGrimsonII);
            pnlFooterDerecho.Controls.Add(lblVerificaciónVehicularFoother);
            pnlFooterDerecho.Controls.Add(pbxLogosVerificentros);
            pnlFooterDerecho.Dock = DockStyle.Right;
            pnlFooterDerecho.Location = new Point(552, 0);
            pnlFooterDerecho.Name = "pnlFooterDerecho";
            pnlFooterDerecho.Size = new Size(147, 58);
            pnlFooterDerecho.TabIndex = 1;
            // 
            // pnlSeparadorFooterGrimsonII
            // 
            pnlSeparadorFooterGrimsonII.BackColor = Color.FromArgb(159, 34, 65);
            pnlSeparadorFooterGrimsonII.Dock = DockStyle.Left;
            pnlSeparadorFooterGrimsonII.Location = new Point(0, 0);
            pnlSeparadorFooterGrimsonII.Name = "pnlSeparadorFooterGrimsonII";
            pnlSeparadorFooterGrimsonII.Size = new Size(3, 58);
            pnlSeparadorFooterGrimsonII.TabIndex = 3;
            // 
            // lblVerificaciónVehicularFoother
            // 
            lblVerificaciónVehicularFoother.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblVerificaciónVehicularFoother.AutoSize = true;
            lblVerificaciónVehicularFoother.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVerificaciónVehicularFoother.ForeColor = Color.FromArgb(159, 34, 65);
            lblVerificaciónVehicularFoother.Location = new Point(54, 16);
            lblVerificaciónVehicularFoother.Name = "lblVerificaciónVehicularFoother";
            lblVerificaciónVehicularFoother.Size = new Size(90, 30);
            lblVerificaciónVehicularFoother.TabIndex = 0;
            lblVerificaciónVehicularFoother.Text = "VERIFICACIÓN \r\nVEHICUALR";
            // 
            // pbxLogosVerificentros
            // 
            pbxLogosVerificentros.Image = Properties.Resources.Logos_VERIFICENTROS;
            pbxLogosVerificentros.Location = new Point(9, 3);
            pbxLogosVerificentros.Name = "pbxLogosVerificentros";
            pbxLogosVerificentros.Size = new Size(41, 49);
            pbxLogosVerificentros.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxLogosVerificentros.TabIndex = 2;
            pbxLogosVerificentros.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSkyBlue;
            panel1.Controls.Add(pnlFooterIquierdo);
            panel1.Controls.Add(pnlSeparadorFooterGrimsonI);
            panel1.Controls.Add(pbxLogosSEDEMA);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(265, 58);
            panel1.TabIndex = 0;
            // 
            // pnlFooterIquierdo
            // 
            pnlFooterIquierdo.BackColor = Color.WhiteSmoke;
            pnlFooterIquierdo.Controls.Add(lblGobiernoDeLa);
            pnlFooterIquierdo.Controls.Add(lblCDMX);
            pnlFooterIquierdo.Dock = DockStyle.Fill;
            pnlFooterIquierdo.Location = new Point(89, 0);
            pnlFooterIquierdo.Name = "pnlFooterIquierdo";
            pnlFooterIquierdo.Size = new Size(166, 58);
            pnlFooterIquierdo.TabIndex = 1;
            // 
            // lblCDMX
            // 
            lblCDMX.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblCDMX.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCDMX.ForeColor = Color.FromArgb(159, 34, 65);
            lblCDMX.Location = new Point(0, 29);
            lblCDMX.Name = "lblCDMX";
            lblCDMX.Size = new Size(170, 20);
            lblCDMX.TabIndex = 0;
            lblCDMX.Text = "CIUDAD DE MEXICO";
            // 
            // pnlSeparadorFooterGrimsonI
            // 
            pnlSeparadorFooterGrimsonI.BackColor = Color.FromArgb(159, 34, 65);
            pnlSeparadorFooterGrimsonI.Dock = DockStyle.Right;
            pnlSeparadorFooterGrimsonI.Location = new Point(255, 0);
            pnlSeparadorFooterGrimsonI.Name = "pnlSeparadorFooterGrimsonI";
            pnlSeparadorFooterGrimsonI.Size = new Size(10, 58);
            pnlSeparadorFooterGrimsonI.TabIndex = 2;
            // 
            // pbxLogosSEDEMA
            // 
            pbxLogosSEDEMA.BackColor = Color.White;
            pbxLogosSEDEMA.Dock = DockStyle.Left;
            pbxLogosSEDEMA.Image = Properties.Resources.Logos_SEDEMA;
            pbxLogosSEDEMA.Location = new Point(0, 0);
            pbxLogosSEDEMA.Name = "pbxLogosSEDEMA";
            pbxLogosSEDEMA.Size = new Size(89, 58);
            pbxLogosSEDEMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxLogosSEDEMA.TabIndex = 6;
            pbxLogosSEDEMA.TabStop = false;
            // 
            // pnlSeparadorFooterAmarillo
            // 
            pnlSeparadorFooterAmarillo.BackColor = Color.FromArgb(188, 149, 92);
            pnlSeparadorFooterAmarillo.Dock = DockStyle.Top;
            pnlSeparadorFooterAmarillo.Location = new Point(0, 0);
            pnlSeparadorFooterAmarillo.Name = "pnlSeparadorFooterAmarillo";
            pnlSeparadorFooterAmarillo.Size = new Size(699, 10);
            pnlSeparadorFooterAmarillo.TabIndex = 0;
            // 
            // pnlMenuLateral
            // 
            pnlMenuLateral.BackColor = Color.FromArgb(159, 34, 65);
            pnlMenuLateral.Controls.Add(pnlLateralIzquierdoAbajo);
            pnlMenuLateral.Controls.Add(pnlLateralIzquierdoCentral);
            pnlMenuLateral.Dock = DockStyle.Left;
            pnlMenuLateral.Location = new Point(0, 0);
            pnlMenuLateral.Name = "pnlMenuLateral";
            pnlMenuLateral.Size = new Size(120, 466);
            pnlMenuLateral.TabIndex = 0;
            // 
            // pnlLateralIzquierdoAbajo
            // 
            pnlLateralIzquierdoAbajo.Controls.Add(btnApagar);
            pnlLateralIzquierdoAbajo.Dock = DockStyle.Bottom;
            pnlLateralIzquierdoAbajo.Location = new Point(0, 383);
            pnlLateralIzquierdoAbajo.Name = "pnlLateralIzquierdoAbajo";
            pnlLateralIzquierdoAbajo.Size = new Size(120, 83);
            pnlLateralIzquierdoAbajo.TabIndex = 1;
            // 
            // btnApagar
            // 
            btnApagar.Dock = DockStyle.Fill;
            btnApagar.FlatAppearance.BorderSize = 0;
            btnApagar.FlatStyle = FlatStyle.Flat;
            btnApagar.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApagar.ForeColor = Color.Transparent;
            btnApagar.Location = new Point(0, 0);
            btnApagar.Name = "btnApagar";
            btnApagar.Size = new Size(120, 83);
            btnApagar.TabIndex = 0;
            btnApagar.Text = "Apagar";
            btnApagar.UseVisualStyleBackColor = true;
            btnApagar.Click += btnApagar_Click;
            // 
            // pnlLateralIzquierdoCentral
            // 
            pnlLateralIzquierdoCentral.Controls.Add(btnInspecionVisual);
            pnlLateralIzquierdoCentral.Dock = DockStyle.Fill;
            pnlLateralIzquierdoCentral.Location = new Point(0, 0);
            pnlLateralIzquierdoCentral.Name = "pnlLateralIzquierdoCentral";
            pnlLateralIzquierdoCentral.Size = new Size(120, 466);
            pnlLateralIzquierdoCentral.TabIndex = 0;
            // 
            // btnInspecionVisual
            // 
            btnInspecionVisual.FlatAppearance.BorderSize = 0;
            btnInspecionVisual.FlatStyle = FlatStyle.Flat;
            btnInspecionVisual.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInspecionVisual.ForeColor = Color.Transparent;
            btnInspecionVisual.Location = new Point(0, 3);
            btnInspecionVisual.Name = "btnInspecionVisual";
            btnInspecionVisual.Size = new Size(117, 73);
            btnInspecionVisual.TabIndex = 1;
            btnInspecionVisual.Text = "Inspección Visual";
            btnInspecionVisual.UseVisualStyleBackColor = true;
            btnInspecionVisual.Click += btnInspecionVisual_Click;
            // 
            // frmBASE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(819, 466);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "frmBASE";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Load += LoginForm_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlCentralFooter.ResumeLayout(false);
            pnlFooterCentral.ResumeLayout(false);
            pnlFooterCentro.ResumeLayout(false);
            pnlFooterDerecho.ResumeLayout(false);
            pnlFooterDerecho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogosVerificentros).EndInit();
            panel1.ResumeLayout(false);
            pnlFooterIquierdo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxLogosSEDEMA).EndInit();
            pnlMenuLateral.ResumeLayout(false);
            pnlLateralIzquierdoAbajo.ResumeLayout(false);
            pnlLateralIzquierdoCentral.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlMenuLateral;
        private Button btnInspecionVisual;
        private Panel pnlFooter;
        private Panel pnlSeparadorFooterAmarillo;
        private Button btnApagar;
        private Label lblCDMX;
        private Label lblGobiernoDeLa;
        private Label lblDGCA;
        private Label lblSEDEMAFooter;
        private PictureBox pbxLogosSEDEMA;
        private Label lblVerificaciónVehicularFoother;
        private Panel pnlSeparadorFooterGrimsonI;
        private PictureBox pbxLogosVerificentros;
        private Panel pnlSeparadorFooterGrimsonII;
        private Panel pnlPanelCambios;
        private Panel pnlLateralIzquierdoAbajo;
        private Panel pnlLateralIzquierdoCentral;
        private Panel pnlCentralFooter;
        private Panel pnlFooterCentral;
        private Panel panel1;
        private Panel pnlFooterIquierdo;
        private Panel pnlFooterDerecho;
        private Panel pnlFooterCentro;
    }
}