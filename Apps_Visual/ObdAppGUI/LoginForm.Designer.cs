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
            pnlPrincipal = new Panel();
            pnlPanelCambios = new Panel();
            pnlFooter = new Panel();
            pnlSeparadorFooterGrimsonII = new Panel();
            pnlSeparadorFooterGrimsonI = new Panel();
            pbxLogosVerificentros = new PictureBox();
            pbxLogosSEDEMA = new PictureBox();
            lblVerificaciónVehicularFoother = new Label();
            lblDGCA = new Label();
            lblSEDEMAFooter = new Label();
            lblCDMX = new Label();
            lblGobiernoDeLa = new Label();
            pnlSeparadorFooterAmarillo = new Panel();
            pnlMenuLateral = new Panel();
            panel2 = new Panel();
            btnApagar = new Button();
            panel1 = new Panel();
            btnInspecionVisual = new Button();
            pnlPrincipal.SuspendLayout();
            pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogosVerificentros).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogosSEDEMA).BeginInit();
            pnlMenuLateral.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
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
            pnlPanelCambios.BackColor = Color.Silver;
            pnlPanelCambios.Dock = DockStyle.Fill;
            pnlPanelCambios.Location = new Point(120, 0);
            pnlPanelCambios.Name = "pnlPanelCambios";
            pnlPanelCambios.Size = new Size(699, 398);
            pnlPanelCambios.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(pnlSeparadorFooterGrimsonII);
            pnlFooter.Controls.Add(pnlSeparadorFooterGrimsonI);
            pnlFooter.Controls.Add(pbxLogosVerificentros);
            pnlFooter.Controls.Add(pbxLogosSEDEMA);
            pnlFooter.Controls.Add(lblVerificaciónVehicularFoother);
            pnlFooter.Controls.Add(lblDGCA);
            pnlFooter.Controls.Add(lblSEDEMAFooter);
            pnlFooter.Controls.Add(lblCDMX);
            pnlFooter.Controls.Add(lblGobiernoDeLa);
            pnlFooter.Controls.Add(pnlSeparadorFooterAmarillo);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(120, 398);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(699, 68);
            pnlFooter.TabIndex = 1;
            // 
            // pnlSeparadorFooterGrimsonII
            // 
            pnlSeparadorFooterGrimsonII.BackColor = Color.FromArgb(159, 34, 65);
            pnlSeparadorFooterGrimsonII.Location = new Point(448, 15);
            pnlSeparadorFooterGrimsonII.Name = "pnlSeparadorFooterGrimsonII";
            pnlSeparadorFooterGrimsonII.Size = new Size(3, 50);
            pnlSeparadorFooterGrimsonII.TabIndex = 3;
            // 
            // pnlSeparadorFooterGrimsonI
            // 
            pnlSeparadorFooterGrimsonI.BackColor = Color.FromArgb(159, 34, 65);
            pnlSeparadorFooterGrimsonI.Location = new Point(295, 14);
            pnlSeparadorFooterGrimsonI.Name = "pnlSeparadorFooterGrimsonI";
            pnlSeparadorFooterGrimsonI.Size = new Size(5, 50);
            pnlSeparadorFooterGrimsonI.TabIndex = 2;
            // 
            // pbxLogosVerificentros
            // 
            pbxLogosVerificentros.Image = Properties.Resources.Logos_VERIFICENTROS;
            pbxLogosVerificentros.Location = new Point(459, 14);
            pbxLogosVerificentros.Name = "pbxLogosVerificentros";
            pbxLogosVerificentros.Size = new Size(41, 49);
            pbxLogosVerificentros.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxLogosVerificentros.TabIndex = 2;
            pbxLogosVerificentros.TabStop = false;
            // 
            // pbxLogosSEDEMA
            // 
            pbxLogosSEDEMA.Image = Properties.Resources.Logos_SEDEMA;
            pbxLogosSEDEMA.Location = new Point(48, 18);
            pbxLogosSEDEMA.Name = "pbxLogosSEDEMA";
            pbxLogosSEDEMA.Size = new Size(84, 45);
            pbxLogosSEDEMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxLogosSEDEMA.TabIndex = 6;
            pbxLogosSEDEMA.TabStop = false;
            pbxLogosSEDEMA.UseWaitCursor = true;
            pbxLogosSEDEMA.WaitOnLoad = true;
            // 
            // lblVerificaciónVehicularFoother
            // 
            lblVerificaciónVehicularFoother.AutoSize = true;
            lblVerificaciónVehicularFoother.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVerificaciónVehicularFoother.ForeColor = Color.FromArgb(159, 34, 65);
            lblVerificaciónVehicularFoother.Location = new Point(506, 23);
            lblVerificaciónVehicularFoother.Name = "lblVerificaciónVehicularFoother";
            lblVerificaciónVehicularFoother.Size = new Size(90, 30);
            lblVerificaciónVehicularFoother.TabIndex = 5;
            lblVerificaciónVehicularFoother.Text = "VERIFICACIÓN \r\nVEHICUALR";
            // 
            // lblDGCA
            // 
            lblDGCA.AutoSize = true;
            lblDGCA.Font = new Font("Segoe UI", 9F);
            lblDGCA.ForeColor = Color.FromArgb(159, 34, 65);
            lblDGCA.Location = new Point(319, 34);
            lblDGCA.Name = "lblDGCA";
            lblDGCA.Size = new Size(124, 30);
            lblDGCA.TabIndex = 4;
            lblDGCA.Text = "DIRECCION GENERAL \r\nDE CALIDAD DEL AIRE";
            // 
            // lblSEDEMAFooter
            // 
            lblSEDEMAFooter.AutoSize = true;
            lblSEDEMAFooter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSEDEMAFooter.ForeColor = Color.FromArgb(159, 34, 65);
            lblSEDEMAFooter.Location = new Point(319, 19);
            lblSEDEMAFooter.Name = "lblSEDEMAFooter";
            lblSEDEMAFooter.Size = new Size(54, 15);
            lblSEDEMAFooter.TabIndex = 3;
            lblSEDEMAFooter.Text = "SEDEMA";
            // 
            // lblCDMX
            // 
            lblCDMX.AutoSize = true;
            lblCDMX.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCDMX.ForeColor = Color.FromArgb(159, 34, 65);
            lblCDMX.Location = new Point(138, 39);
            lblCDMX.Name = "lblCDMX";
            lblCDMX.Size = new Size(151, 20);
            lblCDMX.TabIndex = 2;
            lblCDMX.Text = "CIUDAD DE MEXICO";
            // 
            // lblGobiernoDeLa
            // 
            lblGobiernoDeLa.AutoSize = true;
            lblGobiernoDeLa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGobiernoDeLa.ForeColor = Color.FromArgb(159, 34, 65);
            lblGobiernoDeLa.Location = new Point(138, 19);
            lblGobiernoDeLa.Name = "lblGobiernoDeLa";
            lblGobiernoDeLa.Size = new Size(134, 20);
            lblGobiernoDeLa.TabIndex = 1;
            lblGobiernoDeLa.Text = "GOBIERNO  DE  LA";
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
            pnlMenuLateral.Controls.Add(panel2);
            pnlMenuLateral.Controls.Add(panel1);
            pnlMenuLateral.Dock = DockStyle.Left;
            pnlMenuLateral.Location = new Point(0, 0);
            pnlMenuLateral.Name = "pnlMenuLateral";
            pnlMenuLateral.Size = new Size(120, 466);
            pnlMenuLateral.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnApagar);
            panel2.Location = new Point(0, 383);
            panel2.Name = "panel2";
            panel2.Size = new Size(120, 83);
            panel2.TabIndex = 1;
            // 
            // btnApagar
            // 
            btnApagar.Dock = DockStyle.Bottom;
            btnApagar.FlatAppearance.BorderSize = 0;
            btnApagar.FlatStyle = FlatStyle.Flat;
            btnApagar.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApagar.ForeColor = Color.Transparent;
            btnApagar.Location = new Point(0, 3);
            btnApagar.Name = "btnApagar";
            btnApagar.Size = new Size(120, 80);
            btnApagar.TabIndex = 1;
            btnApagar.Text = "Apagar";
            btnApagar.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnInspecionVisual);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(120, 466);
            panel1.TabIndex = 0;
            // 
            // btnInspecionVisual
            // 
            btnInspecionVisual.FlatAppearance.BorderSize = 0;
            btnInspecionVisual.FlatStyle = FlatStyle.Flat;
            btnInspecionVisual.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInspecionVisual.ForeColor = Color.Transparent;
            btnInspecionVisual.Location = new Point(3, 3);
            btnInspecionVisual.Name = "btnInspecionVisual";
            btnInspecionVisual.Size = new Size(117, 73);
            btnInspecionVisual.TabIndex = 0;
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
            pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogosVerificentros).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogosSEDEMA).EndInit();
            pnlMenuLateral.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
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
        private Panel panel2;
        private Panel panel1;
    }
}