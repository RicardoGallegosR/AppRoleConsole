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
            pnlPrincipal = new Panel();
            splitContainer1 = new SplitContainer();
            tlpPrincipal = new TableLayoutPanel();
            lblResultado = new Label();
            lblRunTimeMil = new Label();
            lblrRunTimeMil = new Label();
            lblCalibrationVerificationNumber = new Label();
            lblrCalibrationVerificationNumber = new Label();
            lblModoALista = new Label();
            lblrModoALista = new Label();
            lblModo7Lista = new Label();
            lblrModo7Lista = new Label();
            lblModo3Lista = new Label();
            lblrModo3Lista = new Label();
            lblOdometroLuzMil = new Label();
            lblrOdometroLuzMil = new Label();
            lblLuzMil = new Label();
            lblOBDClear = new Label();
            lblDTCClear = new Label();
            lblCalId = new Label();
            lblRMP = new Label();
            lblBateria = new Label();
            lblProtocoloOBD = new Label();
            lblVin = new Label();
            lblrLuzMil = new Label();
            lblrOBDClear = new Label();
            lblrDTCClear = new Label();
            lblrCalId = new Label();
            lblrRPM = new Label();
            lblrBateria = new Label();
            lblrProtocoloOBD = new Label();
            lblrVIN = new Label();
            lblTitulo2 = new Label();
            tlpMonitores = new TableLayoutPanel();
            lblCompletoMisfire = new Label();
            lblDisponibleMisfire = new Label();
            lblMisfire = new Label();
            lblCompletoHeatedCatalyst = new Label();
            lblDisponibleHeatedCatalyst = new Label();
            lblHeatedCatalyst = new Label();
            lblCompletoFuelSystem = new Label();
            lblDisponibleFuelSystem = new Label();
            lblFuelSystem = new Label();
            lblCompletoAcRefrigerant = new Label();
            lblDisponibleAcRefrigerant = new Label();
            lblAcRefrigerant = new Label();
            lblCompleto = new Label();
            lblDisponiple = new Label();
            lblMonitorTitulo = new Label();
            lblDisponibleSecondaryAirSystem = new Label();
            lblCompletoSecondaryAirSystem = new Label();
            lblCatalyst = new Label();
            lblComponent = new Label();
            lblEvaporativeSystem = new Label();
            lblDisponibleCatalyst = new Label();
            lblCompletoCatalyst = new Label();
            lblDisponibleComponent = new Label();
            lblCompletoComponent = new Label();
            lblDisponibleEvaporativeSystem = new Label();
            lblCompletoEvaporativeSystem = new Label();
            lblOxygenSensorHeater = new Label();
            lblSecondaryAirSystem = new Label();
            lblOxygenSensor = new Label();
            lblDisponibleOxygenSensor = new Label();
            lblCompletoOxygenSensor = new Label();
            lblDisponibleOxygenSensorHeater = new Label();
            lblCompletoOxygenSensorHeater = new Label();
            lblEgerVvtSystem = new Label();
            lblDisponibleEgerVvtSystem = new Label();
            lblCompletoEgerVvtSystem = new Label();
            lblBoostPressure = new Label();
            lblDisponibleBoostPressure = new Label();
            lblCompletoBoostPressure = new Label();
            lblExhaustGasSensor = new Label();
            lblDisponibleExhaustGasSensor = new Label();
            lblCompletoExhaustGasSensor = new Label();
            lblNmhcCatalyst = new Label();
            lblDisponibleNmhcCatalyst = new Label();
            lblCompletoNmhcCatalyst = new Label();
            lblPmFilter = new Label();
            lblDisponiblePmFilter = new Label();
            lblCompletoPmFilter = new Label();
            lblNoxScrAftertreatment = new Label();
            lblDisponibleNoxScrAftertreatment = new Label();
            lblCompletoNoxScrAftertreatment = new Label();
            pnlFooterPrincipal = new Panel();
            btnFinalizarPruebaOBD = new Button();
            pnlTopPrincipal = new Panel();
            btnConectar = new Button();
            lblLecturaOBD = new Label();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tlpPrincipal.SuspendLayout();
            tlpMonitores.SuspendLayout();
            pnlFooterPrincipal.SuspendLayout();
            pnlTopPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Controls.Add(pnlFooterPrincipal);
            pnlPrincipal.Controls.Add(pnlTopPrincipal);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(1572, 788);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 126);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tlpPrincipal);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tlpMonitores);
            splitContainer1.Size = new Size(1572, 589);
            splitContainer1.SplitterDistance = 835;
            splitContainer1.TabIndex = 0;
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.ColumnCount = 2;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpPrincipal.Controls.Add(lblResultado, 1, 0);
            tlpPrincipal.Controls.Add(lblRunTimeMil, 0, 14);
            tlpPrincipal.Controls.Add(lblrRunTimeMil, 1, 14);
            tlpPrincipal.Controls.Add(lblCalibrationVerificationNumber, 0, 13);
            tlpPrincipal.Controls.Add(lblrCalibrationVerificationNumber, 1, 13);
            tlpPrincipal.Controls.Add(lblModoALista, 0, 12);
            tlpPrincipal.Controls.Add(lblrModoALista, 1, 12);
            tlpPrincipal.Controls.Add(lblModo7Lista, 0, 11);
            tlpPrincipal.Controls.Add(lblrModo7Lista, 1, 11);
            tlpPrincipal.Controls.Add(lblModo3Lista, 0, 10);
            tlpPrincipal.Controls.Add(lblrModo3Lista, 1, 10);
            tlpPrincipal.Controls.Add(lblOdometroLuzMil, 0, 9);
            tlpPrincipal.Controls.Add(lblrOdometroLuzMil, 1, 9);
            tlpPrincipal.Controls.Add(lblLuzMil, 0, 8);
            tlpPrincipal.Controls.Add(lblOBDClear, 0, 7);
            tlpPrincipal.Controls.Add(lblDTCClear, 0, 6);
            tlpPrincipal.Controls.Add(lblCalId, 0, 5);
            tlpPrincipal.Controls.Add(lblRMP, 0, 4);
            tlpPrincipal.Controls.Add(lblBateria, 0, 3);
            tlpPrincipal.Controls.Add(lblProtocoloOBD, 0, 2);
            tlpPrincipal.Controls.Add(lblVin, 0, 1);
            tlpPrincipal.Controls.Add(lblrLuzMil, 1, 8);
            tlpPrincipal.Controls.Add(lblrOBDClear, 1, 7);
            tlpPrincipal.Controls.Add(lblrDTCClear, 1, 6);
            tlpPrincipal.Controls.Add(lblrCalId, 1, 5);
            tlpPrincipal.Controls.Add(lblrRPM, 1, 4);
            tlpPrincipal.Controls.Add(lblrBateria, 1, 3);
            tlpPrincipal.Controls.Add(lblrProtocoloOBD, 1, 2);
            tlpPrincipal.Controls.Add(lblrVIN, 1, 1);
            tlpPrincipal.Controls.Add(lblTitulo2, 0, 0);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.RowCount = 15;
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.Size = new Size(835, 589);
            tlpPrincipal.TabIndex = 2;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResultado.Location = new Point(253, 0);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(62, 15);
            lblResultado.TabIndex = 41;
            lblResultado.Text = "Resultado";
            // 
            // lblRunTimeMil
            // 
            lblRunTimeMil.AutoSize = true;
            lblRunTimeMil.Font = new Font("Segoe UI", 9F);
            lblRunTimeMil.ForeColor = Color.Black;
            lblRunTimeMil.Location = new Point(3, 210);
            lblRunTimeMil.Name = "lblRunTimeMil";
            lblRunTimeMil.Size = new Size(78, 15);
            lblRunTimeMil.TabIndex = 13;
            lblRunTimeMil.Text = "Run time Mil:";
            // 
            // lblrRunTimeMil
            // 
            lblrRunTimeMil.AutoSize = true;
            lblrRunTimeMil.Font = new Font("Segoe UI", 9F);
            lblrRunTimeMil.Location = new Point(253, 210);
            lblrRunTimeMil.Name = "lblrRunTimeMil";
            lblrRunTimeMil.Size = new Size(88, 15);
            lblrRunTimeMil.TabIndex = 24;
            lblrRunTimeMil.Text = "lblrRunTimeMil";
            // 
            // lblCalibrationVerificationNumber
            // 
            lblCalibrationVerificationNumber.AutoSize = true;
            lblCalibrationVerificationNumber.Font = new Font("Segoe UI", 9F);
            lblCalibrationVerificationNumber.ForeColor = Color.Black;
            lblCalibrationVerificationNumber.Location = new Point(3, 195);
            lblCalibrationVerificationNumber.Name = "lblCalibrationVerificationNumber";
            lblCalibrationVerificationNumber.Size = new Size(34, 15);
            lblCalibrationVerificationNumber.TabIndex = 16;
            lblCalibrationVerificationNumber.Text = "CVN:";
            // 
            // lblrCalibrationVerificationNumber
            // 
            lblrCalibrationVerificationNumber.AutoSize = true;
            lblrCalibrationVerificationNumber.Font = new Font("Segoe UI", 9F);
            lblrCalibrationVerificationNumber.Location = new Point(253, 195);
            lblrCalibrationVerificationNumber.Name = "lblrCalibrationVerificationNumber";
            lblrCalibrationVerificationNumber.Size = new Size(185, 15);
            lblrCalibrationVerificationNumber.TabIndex = 0;
            lblrCalibrationVerificationNumber.Text = "lblrCalibrationVerificationNumber";
            // 
            // lblModoALista
            // 
            lblModoALista.AutoSize = true;
            lblModoALista.Font = new Font("Segoe UI", 9F);
            lblModoALista.ForeColor = Color.Black;
            lblModoALista.Location = new Point(3, 180);
            lblModoALista.Name = "lblModoALista";
            lblModoALista.Size = new Size(106, 15);
            lblModoALista.TabIndex = 0;
            lblModoALista.Text = "Permanentes Lista:";
            // 
            // lblrModoALista
            // 
            lblrModoALista.AutoSize = true;
            lblrModoALista.Font = new Font("Segoe UI", 9F);
            lblrModoALista.Location = new Point(253, 180);
            lblrModoALista.Name = "lblrModoALista";
            lblrModoALista.Size = new Size(88, 15);
            lblrModoALista.TabIndex = 39;
            lblrModoALista.Text = "lblrModoALista";
            // 
            // lblModo7Lista
            // 
            lblModo7Lista.AutoSize = true;
            lblModo7Lista.Font = new Font("Segoe UI", 9F);
            lblModo7Lista.ForeColor = Color.Black;
            lblModo7Lista.Location = new Point(3, 165);
            lblModo7Lista.Name = "lblModo7Lista";
            lblModo7Lista.Size = new Size(95, 15);
            lblModo7Lista.TabIndex = 12;
            lblModo7Lista.Text = "Pendientes Lista:";
            // 
            // lblrModo7Lista
            // 
            lblrModo7Lista.AutoSize = true;
            lblrModo7Lista.Font = new Font("Segoe UI", 9F);
            lblrModo7Lista.Location = new Point(253, 165);
            lblrModo7Lista.Name = "lblrModo7Lista";
            lblrModo7Lista.Size = new Size(86, 15);
            lblrModo7Lista.TabIndex = 0;
            lblrModo7Lista.Text = "lblrModo7Lista";
            // 
            // lblModo3Lista
            // 
            lblModo3Lista.AutoSize = true;
            lblModo3Lista.Font = new Font("Segoe UI", 9F);
            lblModo3Lista.ForeColor = Color.Black;
            lblModo3Lista.Location = new Point(3, 150);
            lblModo3Lista.Name = "lblModo3Lista";
            lblModo3Lista.Size = new Size(106, 15);
            lblModo3Lista.TabIndex = 10;
            lblModo3Lista.Text = "Confirmados Lista:";
            // 
            // lblrModo3Lista
            // 
            lblrModo3Lista.AutoSize = true;
            lblrModo3Lista.Font = new Font("Segoe UI", 9F);
            lblrModo3Lista.Location = new Point(253, 150);
            lblrModo3Lista.Name = "lblrModo3Lista";
            lblrModo3Lista.Size = new Size(86, 15);
            lblrModo3Lista.TabIndex = 28;
            lblrModo3Lista.Text = "lblrModo3Lista";
            // 
            // lblOdometroLuzMil
            // 
            lblOdometroLuzMil.AutoSize = true;
            lblOdometroLuzMil.BackColor = Color.Transparent;
            lblOdometroLuzMil.Font = new Font("Segoe UI", 9F);
            lblOdometroLuzMil.ForeColor = Color.Black;
            lblOdometroLuzMil.Location = new Point(3, 135);
            lblOdometroLuzMil.Name = "lblOdometroLuzMil";
            lblOdometroLuzMil.Size = new Size(126, 15);
            lblOdometroLuzMil.TabIndex = 7;
            lblOdometroLuzMil.Text = "Odometro con luz Mil:";
            // 
            // lblrOdometroLuzMil
            // 
            lblrOdometroLuzMil.AutoSize = true;
            lblrOdometroLuzMil.Font = new Font("Segoe UI", 9F);
            lblrOdometroLuzMil.Location = new Point(253, 135);
            lblrOdometroLuzMil.Name = "lblrOdometroLuzMil";
            lblrOdometroLuzMil.Size = new Size(114, 15);
            lblrOdometroLuzMil.TabIndex = 20;
            lblrOdometroLuzMil.Text = "lblrOdometroLuzMil";
            // 
            // lblLuzMil
            // 
            lblLuzMil.AutoSize = true;
            lblLuzMil.Font = new Font("Segoe UI", 9F);
            lblLuzMil.ForeColor = Color.Black;
            lblLuzMil.Location = new Point(3, 120);
            lblLuzMil.Name = "lblLuzMil";
            lblLuzMil.Size = new Size(48, 15);
            lblLuzMil.TabIndex = 9;
            lblLuzMil.Text = "Luz Mil:";
            // 
            // lblOBDClear
            // 
            lblOBDClear.AutoSize = true;
            lblOBDClear.Font = new Font("Segoe UI", 9F);
            lblOBDClear.ForeColor = Color.Black;
            lblOBDClear.Location = new Point(3, 105);
            lblOBDClear.Name = "lblOBDClear";
            lblOBDClear.Size = new Size(129, 15);
            lblOBDClear.TabIndex = 5;
            lblOBDClear.Text = "Odometro desde Clear:";
            // 
            // lblDTCClear
            // 
            lblDTCClear.AutoSize = true;
            lblDTCClear.Font = new Font("Segoe UI", 9F);
            lblDTCClear.ForeColor = Color.Black;
            lblDTCClear.Location = new Point(3, 90);
            lblDTCClear.Name = "lblDTCClear";
            lblDTCClear.Size = new Size(60, 15);
            lblDTCClear.TabIndex = 0;
            lblDTCClear.Text = "Clear DTC:";
            // 
            // lblCalId
            // 
            lblCalId.AutoSize = true;
            lblCalId.Font = new Font("Segoe UI", 9F);
            lblCalId.ForeColor = Color.Black;
            lblCalId.Location = new Point(3, 75);
            lblCalId.Name = "lblCalId";
            lblCalId.Size = new Size(46, 15);
            lblCalId.TabIndex = 0;
            lblCalId.Text = "CAL ID:";
            // 
            // lblRMP
            // 
            lblRMP.AutoSize = true;
            lblRMP.Font = new Font("Segoe UI", 9F);
            lblRMP.ForeColor = Color.Black;
            lblRMP.Location = new Point(3, 60);
            lblRMP.Name = "lblRMP";
            lblRMP.Size = new Size(35, 15);
            lblRMP.TabIndex = 0;
            lblRMP.Text = "RPM:";
            // 
            // lblBateria
            // 
            lblBateria.AutoSize = true;
            lblBateria.Font = new Font("Segoe UI", 9F);
            lblBateria.ForeColor = Color.Black;
            lblBateria.Location = new Point(3, 45);
            lblBateria.Name = "lblBateria";
            lblBateria.Size = new Size(45, 15);
            lblBateria.TabIndex = 4;
            lblBateria.Text = "Voltaje:";
            // 
            // lblProtocoloOBD
            // 
            lblProtocoloOBD.AutoSize = true;
            lblProtocoloOBD.Font = new Font("Segoe UI", 9F);
            lblProtocoloOBD.ForeColor = Color.Black;
            lblProtocoloOBD.Location = new Point(3, 30);
            lblProtocoloOBD.Name = "lblProtocoloOBD";
            lblProtocoloOBD.Size = new Size(62, 15);
            lblProtocoloOBD.TabIndex = 1;
            lblProtocoloOBD.Text = "Protocolo:";
            // 
            // lblVin
            // 
            lblVin.AutoSize = true;
            lblVin.Font = new Font("Segoe UI", 9F);
            lblVin.ForeColor = Color.Black;
            lblVin.Location = new Point(3, 15);
            lblVin.Name = "lblVin";
            lblVin.Size = new Size(29, 15);
            lblVin.TabIndex = 0;
            lblVin.Text = "VIN:";
            // 
            // lblrLuzMil
            // 
            lblrLuzMil.AutoSize = true;
            lblrLuzMil.Font = new Font("Segoe UI", 9F);
            lblrLuzMil.Location = new Point(253, 120);
            lblrLuzMil.Name = "lblrLuzMil";
            lblrLuzMil.Size = new Size(59, 15);
            lblrLuzMil.TabIndex = 18;
            lblrLuzMil.Text = "lblrLuzMil";
            // 
            // lblrOBDClear
            // 
            lblrOBDClear.AutoSize = true;
            lblrOBDClear.Font = new Font("Segoe UI", 9F);
            lblrOBDClear.Location = new Point(253, 105);
            lblrOBDClear.Name = "lblrOBDClear";
            lblrOBDClear.Size = new Size(75, 15);
            lblrOBDClear.TabIndex = 22;
            lblrOBDClear.Text = "lblrOBDClear";
            // 
            // lblrDTCClear
            // 
            lblrDTCClear.AutoSize = true;
            lblrDTCClear.Font = new Font("Segoe UI", 9F);
            lblrDTCClear.Location = new Point(253, 90);
            lblrDTCClear.Name = "lblrDTCClear";
            lblrDTCClear.Size = new Size(71, 15);
            lblrDTCClear.TabIndex = 27;
            lblrDTCClear.Text = "lblrDTCClear";
            // 
            // lblrCalId
            // 
            lblrCalId.AutoSize = true;
            lblrCalId.Font = new Font("Segoe UI", 9F);
            lblrCalId.Location = new Point(253, 75);
            lblrCalId.Name = "lblrCalId";
            lblrCalId.Size = new Size(51, 15);
            lblrCalId.TabIndex = 0;
            lblrCalId.Text = "lblrCalId";
            // 
            // lblrRPM
            // 
            lblrRPM.AutoSize = true;
            lblrRPM.Font = new Font("Segoe UI", 9F);
            lblrRPM.Location = new Point(253, 60);
            lblrRPM.Name = "lblrRPM";
            lblrRPM.Size = new Size(49, 15);
            lblrRPM.TabIndex = 0;
            lblrRPM.Text = "lblrRPM";
            // 
            // lblrBateria
            // 
            lblrBateria.AutoSize = true;
            lblrBateria.Font = new Font("Segoe UI", 9F);
            lblrBateria.Location = new Point(253, 45);
            lblrBateria.Name = "lblrBateria";
            lblrBateria.Size = new Size(60, 15);
            lblrBateria.TabIndex = 21;
            lblrBateria.Text = "lblrBateria";
            // 
            // lblrProtocoloOBD
            // 
            lblrProtocoloOBD.AutoSize = true;
            lblrProtocoloOBD.Font = new Font("Segoe UI", 9F);
            lblrProtocoloOBD.Location = new Point(253, 30);
            lblrProtocoloOBD.Name = "lblrProtocoloOBD";
            lblrProtocoloOBD.Size = new Size(100, 15);
            lblrProtocoloOBD.TabIndex = 19;
            lblrProtocoloOBD.Text = "lblrProtocoloOBD";
            // 
            // lblrVIN
            // 
            lblrVIN.AutoSize = true;
            lblrVIN.Font = new Font("Segoe UI", 9F);
            lblrVIN.Location = new Point(253, 15);
            lblrVIN.Name = "lblrVIN";
            lblrVIN.Size = new Size(43, 15);
            lblrVIN.TabIndex = 17;
            lblrVIN.Text = "lblrVIN";
            // 
            // lblTitulo2
            // 
            lblTitulo2.AutoSize = true;
            lblTitulo2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitulo2.Location = new Point(3, 0);
            lblTitulo2.Name = "lblTitulo2";
            lblTitulo2.Size = new Size(49, 15);
            lblTitulo2.TabIndex = 40;
            lblTitulo2.Text = "Lectura";
            // 
            // tlpMonitores
            // 
            tlpMonitores.ColumnCount = 3;
            tlpMonitores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMonitores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMonitores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMonitores.Controls.Add(lblCompletoMisfire, 2, 10);
            tlpMonitores.Controls.Add(lblDisponibleMisfire, 1, 10);
            tlpMonitores.Controls.Add(lblMisfire, 0, 10);
            tlpMonitores.Controls.Add(lblCompletoHeatedCatalyst, 2, 9);
            tlpMonitores.Controls.Add(lblDisponibleHeatedCatalyst, 1, 9);
            tlpMonitores.Controls.Add(lblHeatedCatalyst, 0, 9);
            tlpMonitores.Controls.Add(lblCompletoFuelSystem, 2, 8);
            tlpMonitores.Controls.Add(lblDisponibleFuelSystem, 1, 8);
            tlpMonitores.Controls.Add(lblFuelSystem, 0, 8);
            tlpMonitores.Controls.Add(lblCompletoAcRefrigerant, 2, 1);
            tlpMonitores.Controls.Add(lblDisponibleAcRefrigerant, 1, 1);
            tlpMonitores.Controls.Add(lblAcRefrigerant, 0, 1);
            tlpMonitores.Controls.Add(lblCompleto, 2, 0);
            tlpMonitores.Controls.Add(lblDisponiple, 1, 0);
            tlpMonitores.Controls.Add(lblMonitorTitulo, 0, 0);
            tlpMonitores.Controls.Add(lblDisponibleSecondaryAirSystem, 1, 7);
            tlpMonitores.Controls.Add(lblCompletoSecondaryAirSystem, 2, 7);
            tlpMonitores.Controls.Add(lblCatalyst, 0, 2);
            tlpMonitores.Controls.Add(lblComponent, 0, 3);
            tlpMonitores.Controls.Add(lblEvaporativeSystem, 0, 4);
            tlpMonitores.Controls.Add(lblDisponibleCatalyst, 1, 2);
            tlpMonitores.Controls.Add(lblCompletoCatalyst, 2, 2);
            tlpMonitores.Controls.Add(lblDisponibleComponent, 1, 3);
            tlpMonitores.Controls.Add(lblCompletoComponent, 2, 3);
            tlpMonitores.Controls.Add(lblDisponibleEvaporativeSystem, 1, 4);
            tlpMonitores.Controls.Add(lblCompletoEvaporativeSystem, 2, 4);
            tlpMonitores.Controls.Add(lblOxygenSensorHeater, 0, 5);
            tlpMonitores.Controls.Add(lblSecondaryAirSystem, 0, 7);
            tlpMonitores.Controls.Add(lblOxygenSensor, 0, 6);
            tlpMonitores.Controls.Add(lblDisponibleOxygenSensor, 1, 6);
            tlpMonitores.Controls.Add(lblCompletoOxygenSensor, 2, 6);
            tlpMonitores.Controls.Add(lblDisponibleOxygenSensorHeater, 1, 5);
            tlpMonitores.Controls.Add(lblCompletoOxygenSensorHeater, 2, 5);
            tlpMonitores.Controls.Add(lblEgerVvtSystem, 0, 11);
            tlpMonitores.Controls.Add(lblDisponibleEgerVvtSystem, 1, 11);
            tlpMonitores.Controls.Add(lblCompletoEgerVvtSystem, 2, 11);
            tlpMonitores.Controls.Add(lblBoostPressure, 0, 12);
            tlpMonitores.Controls.Add(lblDisponibleBoostPressure, 1, 12);
            tlpMonitores.Controls.Add(lblCompletoBoostPressure, 2, 12);
            tlpMonitores.Controls.Add(lblExhaustGasSensor, 0, 13);
            tlpMonitores.Controls.Add(lblDisponibleExhaustGasSensor, 1, 13);
            tlpMonitores.Controls.Add(lblCompletoExhaustGasSensor, 2, 13);
            tlpMonitores.Controls.Add(lblNmhcCatalyst, 0, 14);
            tlpMonitores.Controls.Add(lblDisponibleNmhcCatalyst, 1, 14);
            tlpMonitores.Controls.Add(lblCompletoNmhcCatalyst, 2, 14);
            tlpMonitores.Controls.Add(lblPmFilter, 0, 15);
            tlpMonitores.Controls.Add(lblDisponiblePmFilter, 1, 15);
            tlpMonitores.Controls.Add(lblCompletoPmFilter, 2, 15);
            tlpMonitores.Controls.Add(lblNoxScrAftertreatment, 0, 16);
            tlpMonitores.Controls.Add(lblDisponibleNoxScrAftertreatment, 1, 16);
            tlpMonitores.Controls.Add(lblCompletoNoxScrAftertreatment, 2, 16);
            tlpMonitores.Dock = DockStyle.Fill;
            tlpMonitores.Location = new Point(0, 0);
            tlpMonitores.Name = "tlpMonitores";
            tlpMonitores.RowCount = 18;
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.RowStyles.Add(new RowStyle());
            tlpMonitores.Size = new Size(733, 589);
            tlpMonitores.TabIndex = 0;
            // 
            // lblCompletoMisfire
            // 
            lblCompletoMisfire.AutoSize = true;
            lblCompletoMisfire.Location = new Point(552, 150);
            lblCompletoMisfire.Name = "lblCompletoMisfire";
            lblCompletoMisfire.Size = new Size(21, 15);
            lblCompletoMisfire.TabIndex = 0;
            lblCompletoMisfire.Text = "no";
            // 
            // lblDisponibleMisfire
            // 
            lblDisponibleMisfire.AutoSize = true;
            lblDisponibleMisfire.Location = new Point(369, 150);
            lblDisponibleMisfire.Name = "lblDisponibleMisfire";
            lblDisponibleMisfire.Size = new Size(21, 15);
            lblDisponibleMisfire.TabIndex = 0;
            lblDisponibleMisfire.Text = "no";
            // 
            // lblMisfire
            // 
            lblMisfire.AutoSize = true;
            lblMisfire.Location = new Point(3, 150);
            lblMisfire.Name = "lblMisfire";
            lblMisfire.Size = new Size(90, 15);
            lblMisfire.TabIndex = 0;
            lblMisfire.Text = "Misfire (SDCIIC)";
            // 
            // lblCompletoHeatedCatalyst
            // 
            lblCompletoHeatedCatalyst.AutoSize = true;
            lblCompletoHeatedCatalyst.Location = new Point(552, 135);
            lblCompletoHeatedCatalyst.Name = "lblCompletoHeatedCatalyst";
            lblCompletoHeatedCatalyst.Size = new Size(21, 15);
            lblCompletoHeatedCatalyst.TabIndex = 0;
            lblCompletoHeatedCatalyst.Text = "no";
            // 
            // lblDisponibleHeatedCatalyst
            // 
            lblDisponibleHeatedCatalyst.AutoSize = true;
            lblDisponibleHeatedCatalyst.Location = new Point(369, 135);
            lblDisponibleHeatedCatalyst.Name = "lblDisponibleHeatedCatalyst";
            lblDisponibleHeatedCatalyst.Size = new Size(21, 15);
            lblDisponibleHeatedCatalyst.TabIndex = 0;
            lblDisponibleHeatedCatalyst.Text = "no";
            // 
            // lblHeatedCatalyst
            // 
            lblHeatedCatalyst.AutoSize = true;
            lblHeatedCatalyst.Location = new Point(3, 135);
            lblHeatedCatalyst.Name = "lblHeatedCatalyst";
            lblHeatedCatalyst.Size = new Size(122, 15);
            lblHeatedCatalyst.TabIndex = 0;
            lblHeatedCatalyst.Text = "Heated Catalyst (SSO)";
            // 
            // lblCompletoFuelSystem
            // 
            lblCompletoFuelSystem.AutoSize = true;
            lblCompletoFuelSystem.Location = new Point(552, 120);
            lblCompletoFuelSystem.Name = "lblCompletoFuelSystem";
            lblCompletoFuelSystem.Size = new Size(21, 15);
            lblCompletoFuelSystem.TabIndex = 0;
            lblCompletoFuelSystem.Text = "no";
            // 
            // lblDisponibleFuelSystem
            // 
            lblDisponibleFuelSystem.AutoSize = true;
            lblDisponibleFuelSystem.Location = new Point(369, 120);
            lblDisponibleFuelSystem.Name = "lblDisponibleFuelSystem";
            lblDisponibleFuelSystem.Size = new Size(21, 15);
            lblDisponibleFuelSystem.TabIndex = 0;
            lblDisponibleFuelSystem.Text = "no";
            // 
            // lblFuelSystem
            // 
            lblFuelSystem.AutoSize = true;
            lblFuelSystem.Location = new Point(3, 120);
            lblFuelSystem.Name = "lblFuelSystem";
            lblFuelSystem.Size = new Size(109, 15);
            lblFuelSystem.TabIndex = 0;
            lblFuelSystem.Text = "Fuel System (SECC)";
            // 
            // lblCompletoAcRefrigerant
            // 
            lblCompletoAcRefrigerant.AutoSize = true;
            lblCompletoAcRefrigerant.Location = new Point(552, 15);
            lblCompletoAcRefrigerant.Name = "lblCompletoAcRefrigerant";
            lblCompletoAcRefrigerant.Size = new Size(21, 15);
            lblCompletoAcRefrigerant.TabIndex = 0;
            lblCompletoAcRefrigerant.Text = "no";
            // 
            // lblDisponibleAcRefrigerant
            // 
            lblDisponibleAcRefrigerant.AutoSize = true;
            lblDisponibleAcRefrigerant.Location = new Point(369, 15);
            lblDisponibleAcRefrigerant.Name = "lblDisponibleAcRefrigerant";
            lblDisponibleAcRefrigerant.Size = new Size(21, 15);
            lblDisponibleAcRefrigerant.TabIndex = 0;
            lblDisponibleAcRefrigerant.Text = "no";
            // 
            // lblAcRefrigerant
            // 
            lblAcRefrigerant.AutoSize = true;
            lblAcRefrigerant.Location = new Point(3, 15);
            lblAcRefrigerant.Name = "lblAcRefrigerant";
            lblAcRefrigerant.Size = new Size(113, 15);
            lblAcRefrigerant.TabIndex = 0;
            lblAcRefrigerant.Text = "Ac Refrigerant (SSA)";
            // 
            // lblCompleto
            // 
            lblCompleto.AutoSize = true;
            lblCompleto.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCompleto.Location = new Point(552, 0);
            lblCompleto.Name = "lblCompleto";
            lblCompleto.Size = new Size(74, 15);
            lblCompleto.TabIndex = 2;
            lblCompleto.Text = "Completado";
            // 
            // lblDisponiple
            // 
            lblDisponiple.AutoSize = true;
            lblDisponiple.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDisponiple.Location = new Point(369, 0);
            lblDisponiple.Name = "lblDisponiple";
            lblDisponiple.Size = new Size(65, 15);
            lblDisponiple.TabIndex = 1;
            lblDisponiple.Text = "Disponible";
            // 
            // lblMonitorTitulo
            // 
            lblMonitorTitulo.AutoSize = true;
            lblMonitorTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMonitorTitulo.Location = new Point(3, 0);
            lblMonitorTitulo.Name = "lblMonitorTitulo";
            lblMonitorTitulo.Size = new Size(52, 15);
            lblMonitorTitulo.TabIndex = 0;
            lblMonitorTitulo.Text = "Monitor";
            // 
            // lblDisponibleSecondaryAirSystem
            // 
            lblDisponibleSecondaryAirSystem.AutoSize = true;
            lblDisponibleSecondaryAirSystem.Location = new Point(369, 105);
            lblDisponibleSecondaryAirSystem.Name = "lblDisponibleSecondaryAirSystem";
            lblDisponibleSecondaryAirSystem.Size = new Size(21, 15);
            lblDisponibleSecondaryAirSystem.TabIndex = 0;
            lblDisponibleSecondaryAirSystem.Text = "no";
            // 
            // lblCompletoSecondaryAirSystem
            // 
            lblCompletoSecondaryAirSystem.AutoSize = true;
            lblCompletoSecondaryAirSystem.Location = new Point(552, 105);
            lblCompletoSecondaryAirSystem.Name = "lblCompletoSecondaryAirSystem";
            lblCompletoSecondaryAirSystem.Size = new Size(21, 15);
            lblCompletoSecondaryAirSystem.TabIndex = 0;
            lblCompletoSecondaryAirSystem.Text = "no";
            // 
            // lblCatalyst
            // 
            lblCatalyst.AutoSize = true;
            lblCatalyst.Location = new Point(3, 30);
            lblCatalyst.Name = "lblCatalyst";
            lblCatalyst.Size = new Size(81, 15);
            lblCatalyst.TabIndex = 0;
            lblCatalyst.Text = "Catalyst (SSO)";
            // 
            // lblComponent
            // 
            lblComponent.AutoSize = true;
            lblComponent.Location = new Point(3, 45);
            lblComponent.Name = "lblComponent";
            lblComponent.Size = new Size(96, 15);
            lblComponent.TabIndex = 0;
            lblComponent.Text = "Component (SC)";
            // 
            // lblEvaporativeSystem
            // 
            lblEvaporativeSystem.AutoSize = true;
            lblEvaporativeSystem.Location = new Point(3, 60);
            lblEvaporativeSystem.Name = "lblEvaporativeSystem";
            lblEvaporativeSystem.Size = new Size(142, 15);
            lblEvaporativeSystem.TabIndex = 0;
            lblEvaporativeSystem.Text = "Evaporative System (SCC)";
            // 
            // lblDisponibleCatalyst
            // 
            lblDisponibleCatalyst.AutoSize = true;
            lblDisponibleCatalyst.Location = new Point(369, 30);
            lblDisponibleCatalyst.Name = "lblDisponibleCatalyst";
            lblDisponibleCatalyst.Size = new Size(21, 15);
            lblDisponibleCatalyst.TabIndex = 0;
            lblDisponibleCatalyst.Text = "no";
            // 
            // lblCompletoCatalyst
            // 
            lblCompletoCatalyst.AutoSize = true;
            lblCompletoCatalyst.Location = new Point(552, 30);
            lblCompletoCatalyst.Name = "lblCompletoCatalyst";
            lblCompletoCatalyst.Size = new Size(21, 15);
            lblCompletoCatalyst.TabIndex = 0;
            lblCompletoCatalyst.Text = "no";
            // 
            // lblDisponibleComponent
            // 
            lblDisponibleComponent.AutoSize = true;
            lblDisponibleComponent.Location = new Point(369, 45);
            lblDisponibleComponent.Name = "lblDisponibleComponent";
            lblDisponibleComponent.Size = new Size(21, 15);
            lblDisponibleComponent.TabIndex = 0;
            lblDisponibleComponent.Text = "no";
            // 
            // lblCompletoComponent
            // 
            lblCompletoComponent.AutoSize = true;
            lblCompletoComponent.Location = new Point(552, 45);
            lblCompletoComponent.Name = "lblCompletoComponent";
            lblCompletoComponent.Size = new Size(21, 15);
            lblCompletoComponent.TabIndex = 0;
            lblCompletoComponent.Text = "no";
            // 
            // lblDisponibleEvaporativeSystem
            // 
            lblDisponibleEvaporativeSystem.AutoSize = true;
            lblDisponibleEvaporativeSystem.Location = new Point(369, 60);
            lblDisponibleEvaporativeSystem.Name = "lblDisponibleEvaporativeSystem";
            lblDisponibleEvaporativeSystem.Size = new Size(21, 15);
            lblDisponibleEvaporativeSystem.TabIndex = 0;
            lblDisponibleEvaporativeSystem.Text = "no";
            // 
            // lblCompletoEvaporativeSystem
            // 
            lblCompletoEvaporativeSystem.AutoSize = true;
            lblCompletoEvaporativeSystem.Location = new Point(552, 60);
            lblCompletoEvaporativeSystem.Name = "lblCompletoEvaporativeSystem";
            lblCompletoEvaporativeSystem.Size = new Size(21, 15);
            lblCompletoEvaporativeSystem.TabIndex = 0;
            lblCompletoEvaporativeSystem.Text = "no";
            // 
            // lblOxygenSensorHeater
            // 
            lblOxygenSensorHeater.AutoSize = true;
            lblOxygenSensorHeater.Location = new Point(3, 75);
            lblOxygenSensorHeater.Name = "lblOxygenSensorHeater";
            lblOxygenSensorHeater.Size = new Size(162, 15);
            lblOxygenSensorHeater.TabIndex = 0;
            lblOxygenSensorHeater.Text = "Oxygen Sensor Heater (SRGE)";
            // 
            // lblSecondaryAirSystem
            // 
            lblSecondaryAirSystem.AutoSize = true;
            lblSecondaryAirSystem.Location = new Point(3, 105);
            lblSecondaryAirSystem.Name = "lblSecondaryAirSystem";
            lblSecondaryAirSystem.Size = new Size(144, 15);
            lblSecondaryAirSystem.TabIndex = 0;
            lblSecondaryAirSystem.Text = "Secondary Air System (SE)";
            // 
            // lblOxygenSensor
            // 
            lblOxygenSensor.AutoSize = true;
            lblOxygenSensor.Location = new Point(3, 90);
            lblOxygenSensor.Name = "lblOxygenSensor";
            lblOxygenSensor.Size = new Size(126, 15);
            lblOxygenSensor.TabIndex = 0;
            lblOxygenSensor.Text = "Oxygen Sensor (SCSO)";
            // 
            // lblDisponibleOxygenSensor
            // 
            lblDisponibleOxygenSensor.AutoSize = true;
            lblDisponibleOxygenSensor.Location = new Point(369, 90);
            lblDisponibleOxygenSensor.Name = "lblDisponibleOxygenSensor";
            lblDisponibleOxygenSensor.Size = new Size(21, 15);
            lblDisponibleOxygenSensor.TabIndex = 0;
            lblDisponibleOxygenSensor.Text = "no";
            // 
            // lblCompletoOxygenSensor
            // 
            lblCompletoOxygenSensor.AutoSize = true;
            lblCompletoOxygenSensor.Location = new Point(552, 90);
            lblCompletoOxygenSensor.Name = "lblCompletoOxygenSensor";
            lblCompletoOxygenSensor.Size = new Size(21, 15);
            lblCompletoOxygenSensor.TabIndex = 0;
            lblCompletoOxygenSensor.Text = "no";
            // 
            // lblDisponibleOxygenSensorHeater
            // 
            lblDisponibleOxygenSensorHeater.AutoSize = true;
            lblDisponibleOxygenSensorHeater.Location = new Point(369, 75);
            lblDisponibleOxygenSensorHeater.Name = "lblDisponibleOxygenSensorHeater";
            lblDisponibleOxygenSensorHeater.Size = new Size(21, 15);
            lblDisponibleOxygenSensorHeater.TabIndex = 0;
            lblDisponibleOxygenSensorHeater.Text = "no";
            // 
            // lblCompletoOxygenSensorHeater
            // 
            lblCompletoOxygenSensorHeater.AutoSize = true;
            lblCompletoOxygenSensorHeater.Location = new Point(552, 75);
            lblCompletoOxygenSensorHeater.Name = "lblCompletoOxygenSensorHeater";
            lblCompletoOxygenSensorHeater.Size = new Size(21, 15);
            lblCompletoOxygenSensorHeater.TabIndex = 0;
            lblCompletoOxygenSensorHeater.Text = "no";
            // 
            // lblEgerVvtSystem
            // 
            lblEgerVvtSystem.AutoSize = true;
            lblEgerVvtSystem.ForeColor = SystemColors.HotTrack;
            lblEgerVvtSystem.Location = new Point(3, 165);
            lblEgerVvtSystem.Name = "lblEgerVvtSystem";
            lblEgerVvtSystem.Size = new Size(129, 15);
            lblEgerVvtSystem.TabIndex = 0;
            lblEgerVvtSystem.Text = "Eger Vvt System (SFAA)";
            // 
            // lblDisponibleEgerVvtSystem
            // 
            lblDisponibleEgerVvtSystem.AutoSize = true;
            lblDisponibleEgerVvtSystem.ForeColor = SystemColors.HotTrack;
            lblDisponibleEgerVvtSystem.Location = new Point(369, 165);
            lblDisponibleEgerVvtSystem.Name = "lblDisponibleEgerVvtSystem";
            lblDisponibleEgerVvtSystem.Size = new Size(21, 15);
            lblDisponibleEgerVvtSystem.TabIndex = 0;
            lblDisponibleEgerVvtSystem.Text = "no";
            // 
            // lblCompletoEgerVvtSystem
            // 
            lblCompletoEgerVvtSystem.AutoSize = true;
            lblCompletoEgerVvtSystem.ForeColor = SystemColors.HotTrack;
            lblCompletoEgerVvtSystem.Location = new Point(552, 165);
            lblCompletoEgerVvtSystem.Name = "lblCompletoEgerVvtSystem";
            lblCompletoEgerVvtSystem.Size = new Size(21, 15);
            lblCompletoEgerVvtSystem.TabIndex = 0;
            lblCompletoEgerVvtSystem.Text = "no";
            // 
            // lblBoostPressure
            // 
            lblBoostPressure.AutoSize = true;
            lblBoostPressure.ForeColor = Color.Red;
            lblBoostPressure.Location = new Point(3, 180);
            lblBoostPressure.Name = "lblBoostPressure";
            lblBoostPressure.Size = new Size(84, 15);
            lblBoostPressure.TabIndex = 0;
            lblBoostPressure.Text = "Boost Pressure";
            // 
            // lblDisponibleBoostPressure
            // 
            lblDisponibleBoostPressure.AutoSize = true;
            lblDisponibleBoostPressure.ForeColor = Color.Red;
            lblDisponibleBoostPressure.Location = new Point(369, 180);
            lblDisponibleBoostPressure.Name = "lblDisponibleBoostPressure";
            lblDisponibleBoostPressure.Size = new Size(21, 15);
            lblDisponibleBoostPressure.TabIndex = 0;
            lblDisponibleBoostPressure.Text = "no";
            // 
            // lblCompletoBoostPressure
            // 
            lblCompletoBoostPressure.AutoSize = true;
            lblCompletoBoostPressure.ForeColor = Color.Red;
            lblCompletoBoostPressure.Location = new Point(552, 180);
            lblCompletoBoostPressure.Name = "lblCompletoBoostPressure";
            lblCompletoBoostPressure.Size = new Size(21, 15);
            lblCompletoBoostPressure.TabIndex = 0;
            lblCompletoBoostPressure.Text = "no";
            // 
            // lblExhaustGasSensor
            // 
            lblExhaustGasSensor.AutoSize = true;
            lblExhaustGasSensor.ForeColor = Color.Red;
            lblExhaustGasSensor.Location = new Point(3, 195);
            lblExhaustGasSensor.Name = "lblExhaustGasSensor";
            lblExhaustGasSensor.Size = new Size(108, 15);
            lblExhaustGasSensor.TabIndex = 0;
            lblExhaustGasSensor.Text = "Exhaust Gas Sensor";
            // 
            // lblDisponibleExhaustGasSensor
            // 
            lblDisponibleExhaustGasSensor.AutoSize = true;
            lblDisponibleExhaustGasSensor.ForeColor = Color.Red;
            lblDisponibleExhaustGasSensor.Location = new Point(369, 195);
            lblDisponibleExhaustGasSensor.Name = "lblDisponibleExhaustGasSensor";
            lblDisponibleExhaustGasSensor.Size = new Size(21, 15);
            lblDisponibleExhaustGasSensor.TabIndex = 0;
            lblDisponibleExhaustGasSensor.Text = "no";
            // 
            // lblCompletoExhaustGasSensor
            // 
            lblCompletoExhaustGasSensor.AutoSize = true;
            lblCompletoExhaustGasSensor.ForeColor = Color.Red;
            lblCompletoExhaustGasSensor.Location = new Point(552, 195);
            lblCompletoExhaustGasSensor.Name = "lblCompletoExhaustGasSensor";
            lblCompletoExhaustGasSensor.Size = new Size(21, 15);
            lblCompletoExhaustGasSensor.TabIndex = 0;
            lblCompletoExhaustGasSensor.Text = "no";
            // 
            // lblNmhcCatalyst
            // 
            lblNmhcCatalyst.AutoSize = true;
            lblNmhcCatalyst.ForeColor = Color.Red;
            lblNmhcCatalyst.Location = new Point(3, 210);
            lblNmhcCatalyst.Name = "lblNmhcCatalyst";
            lblNmhcCatalyst.Size = new Size(85, 15);
            lblNmhcCatalyst.TabIndex = 0;
            lblNmhcCatalyst.Text = "Nmhc Catalyst";
            // 
            // lblDisponibleNmhcCatalyst
            // 
            lblDisponibleNmhcCatalyst.AutoSize = true;
            lblDisponibleNmhcCatalyst.ForeColor = Color.Red;
            lblDisponibleNmhcCatalyst.Location = new Point(369, 210);
            lblDisponibleNmhcCatalyst.Name = "lblDisponibleNmhcCatalyst";
            lblDisponibleNmhcCatalyst.Size = new Size(21, 15);
            lblDisponibleNmhcCatalyst.TabIndex = 0;
            lblDisponibleNmhcCatalyst.Text = "no";
            // 
            // lblCompletoNmhcCatalyst
            // 
            lblCompletoNmhcCatalyst.AutoSize = true;
            lblCompletoNmhcCatalyst.ForeColor = Color.Red;
            lblCompletoNmhcCatalyst.Location = new Point(552, 210);
            lblCompletoNmhcCatalyst.Name = "lblCompletoNmhcCatalyst";
            lblCompletoNmhcCatalyst.Size = new Size(21, 15);
            lblCompletoNmhcCatalyst.TabIndex = 0;
            lblCompletoNmhcCatalyst.Text = "no";
            // 
            // lblPmFilter
            // 
            lblPmFilter.AutoSize = true;
            lblPmFilter.ForeColor = Color.Red;
            lblPmFilter.Location = new Point(3, 225);
            lblPmFilter.Name = "lblPmFilter";
            lblPmFilter.Size = new Size(54, 15);
            lblPmFilter.TabIndex = 0;
            lblPmFilter.Text = "Pm Filter";
            // 
            // lblDisponiblePmFilter
            // 
            lblDisponiblePmFilter.AutoSize = true;
            lblDisponiblePmFilter.ForeColor = Color.Red;
            lblDisponiblePmFilter.Location = new Point(369, 225);
            lblDisponiblePmFilter.Name = "lblDisponiblePmFilter";
            lblDisponiblePmFilter.Size = new Size(21, 15);
            lblDisponiblePmFilter.TabIndex = 0;
            lblDisponiblePmFilter.Text = "no";
            // 
            // lblCompletoPmFilter
            // 
            lblCompletoPmFilter.AutoSize = true;
            lblCompletoPmFilter.ForeColor = Color.Red;
            lblCompletoPmFilter.Location = new Point(552, 225);
            lblCompletoPmFilter.Name = "lblCompletoPmFilter";
            lblCompletoPmFilter.Size = new Size(21, 15);
            lblCompletoPmFilter.TabIndex = 0;
            lblCompletoPmFilter.Text = "no";
            // 
            // lblNoxScrAftertreatment
            // 
            lblNoxScrAftertreatment.AutoSize = true;
            lblNoxScrAftertreatment.ForeColor = Color.Red;
            lblNoxScrAftertreatment.Location = new Point(3, 240);
            lblNoxScrAftertreatment.Name = "lblNoxScrAftertreatment";
            lblNoxScrAftertreatment.Size = new Size(129, 15);
            lblNoxScrAftertreatment.TabIndex = 0;
            lblNoxScrAftertreatment.Text = "Nox Scr Aftertreatment";
            // 
            // lblDisponibleNoxScrAftertreatment
            // 
            lblDisponibleNoxScrAftertreatment.AutoSize = true;
            lblDisponibleNoxScrAftertreatment.ForeColor = Color.Red;
            lblDisponibleNoxScrAftertreatment.Location = new Point(369, 240);
            lblDisponibleNoxScrAftertreatment.Name = "lblDisponibleNoxScrAftertreatment";
            lblDisponibleNoxScrAftertreatment.Size = new Size(21, 15);
            lblDisponibleNoxScrAftertreatment.TabIndex = 0;
            lblDisponibleNoxScrAftertreatment.Text = "no";
            // 
            // lblCompletoNoxScrAftertreatment
            // 
            lblCompletoNoxScrAftertreatment.AutoSize = true;
            lblCompletoNoxScrAftertreatment.ForeColor = Color.Red;
            lblCompletoNoxScrAftertreatment.Location = new Point(552, 240);
            lblCompletoNoxScrAftertreatment.Name = "lblCompletoNoxScrAftertreatment";
            lblCompletoNoxScrAftertreatment.Size = new Size(21, 15);
            lblCompletoNoxScrAftertreatment.TabIndex = 0;
            lblCompletoNoxScrAftertreatment.Text = "no";
            // 
            // pnlFooterPrincipal
            // 
            pnlFooterPrincipal.BackColor = Color.White;
            pnlFooterPrincipal.Controls.Add(btnFinalizarPruebaOBD);
            pnlFooterPrincipal.Dock = DockStyle.Bottom;
            pnlFooterPrincipal.Location = new Point(0, 715);
            pnlFooterPrincipal.Name = "pnlFooterPrincipal";
            pnlFooterPrincipal.Size = new Size(1572, 73);
            pnlFooterPrincipal.TabIndex = 1;
            // 
            // btnFinalizarPruebaOBD
            // 
            btnFinalizarPruebaOBD.Dock = DockStyle.Right;
            btnFinalizarPruebaOBD.FlatAppearance.BorderSize = 0;
            btnFinalizarPruebaOBD.FlatStyle = FlatStyle.Flat;
            btnFinalizarPruebaOBD.Font = new Font("Segoe UI", 24F);
            btnFinalizarPruebaOBD.ForeColor = Color.FromArgb(159, 34, 65);
            btnFinalizarPruebaOBD.Location = new Point(1294, 0);
            btnFinalizarPruebaOBD.Name = "btnFinalizarPruebaOBD";
            btnFinalizarPruebaOBD.Size = new Size(278, 73);
            btnFinalizarPruebaOBD.TabIndex = 0;
            btnFinalizarPruebaOBD.Text = "Finalizar";
            btnFinalizarPruebaOBD.UseVisualStyleBackColor = true;
            btnFinalizarPruebaOBD.Click += btnFinalizarPruebaOBD_Click;
            // 
            // pnlTopPrincipal
            // 
            pnlTopPrincipal.BackColor = Color.White;
            pnlTopPrincipal.Controls.Add(btnConectar);
            pnlTopPrincipal.Controls.Add(lblLecturaOBD);
            pnlTopPrincipal.Dock = DockStyle.Top;
            pnlTopPrincipal.Location = new Point(0, 0);
            pnlTopPrincipal.Name = "pnlTopPrincipal";
            pnlTopPrincipal.Size = new Size(1572, 126);
            pnlTopPrincipal.TabIndex = 0;
            // 
            // btnConectar
            // 
            btnConectar.Dock = DockStyle.Right;
            btnConectar.FlatAppearance.BorderSize = 0;
            btnConectar.FlatStyle = FlatStyle.Flat;
            btnConectar.Font = new Font("Segoe UI", 24F);
            btnConectar.ForeColor = Color.FromArgb(159, 34, 65);
            btnConectar.Location = new Point(1262, 0);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(310, 126);
            btnConectar.TabIndex = 1;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // lblLecturaOBD
            // 
            lblLecturaOBD.AutoSize = true;
            lblLecturaOBD.Dock = DockStyle.Fill;
            lblLecturaOBD.FlatStyle = FlatStyle.Flat;
            lblLecturaOBD.Font = new Font("Segoe UI Semibold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLecturaOBD.ForeColor = Color.Black;
            lblLecturaOBD.Location = new Point(0, 0);
            lblLecturaOBD.Name = "lblLecturaOBD";
            lblLecturaOBD.Size = new Size(401, 65);
            lblLecturaOBD.TabIndex = 0;
            lblLecturaOBD.Text = "Diagnóstico OBD";
            lblLecturaOBD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmOBD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1572, 788);
            Controls.Add(pnlPrincipal);
            Name = "frmOBD";
            Text = "frmOBD";
            Load += frmOBD_Load;
            pnlPrincipal.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tlpPrincipal.ResumeLayout(false);
            tlpPrincipal.PerformLayout();
            tlpMonitores.ResumeLayout(false);
            tlpMonitores.PerformLayout();
            pnlFooterPrincipal.ResumeLayout(false);
            pnlTopPrincipal.ResumeLayout(false);
            pnlTopPrincipal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlTopPrincipal;
        private Label lblLecturaOBD;
        private Panel pnlFooterPrincipal;
        private Button btnFinalizarPruebaOBD;
        private Button btnConectar;
        private TableLayoutPanel tlpPrincipal;
        private Label lblVin;
        private Label lblProtocoloOBD;
        private Label lblLuzMil;
        private Label lblDTCClear;
        private Label lblOdometroLuzMil;
        private Label lblRMP;
        private Label lblModo3Lista;
        private Label lblModo7Lista;
        private Label lblModoALista;
        private Label lblCalibrationVerificationNumber;
        private Label lblrVIN;
        private Label lblrRunTimeMillblrModoALista;
        private Label lblrModoALista;
        private Label lblrModo7Lista;
        private Label lblrCalibrationVerificationNumber;
        private Label lblrModo3Lista;
        private Label lblrDTCClear;
        private Label lblrRPM;
        private Label lblrOdometroLuzMil;
        private Label lblrProtocoloOBD;
        private Label lblrLuzMil;
        private TableLayoutPanel tlpMonitores;
        private Label lblCompletoSecondaryAirSystem;
        private Label lblDisponibleSecondaryAirSystem;
        private Label lblSecondaryAirSystem;
        private Label lblCompletoPmFilter;
        private Label lblDisponiblePmFilter;
        private Label lblPmFilter;
        private Label lblCompletoOxygenSensor;
        private Label lblDisponibleOxygenSensor;
        private Label lblOxygenSensor;
        private Label lblCompletoOxygenSensorHeater;
        private Label lblDisponibleOxygenSensorHeater;
        private Label lblOxygenSensorHeater;
        private Label lblCompletoNmhcCatalyst;
        private Label lblDisponibleNmhcCatalyst;
        private Label lblNmhcCatalyst;
        private Label lblCompletoMisfire;
        private Label lblDisponibleMisfire;
        private Label lblMisfire;
        private Label lblCompletoHeatedCatalyst;
        private Label lblDisponibleHeatedCatalyst;
        private Label lblHeatedCatalyst;
        private Label lblCompletoFuelSystem;
        private Label lblDisponibleFuelSystem;
        private Label lblFuelSystem;
        private Label lblCompletoExhaustGasSensor;
        private Label lblDisponibleExhaustGasSensor;
        private Label lblExhaustGasSensor;
        private Label lblCompletoEvaporativeSystem;
        private Label lblDisponibleEvaporativeSystem;
        private Label lblEvaporativeSystem;
        private Label lblCompletoEgerVvtSystem;
        private Label lblDisponibleEgerVvtSystem;
        private Label lblEgerVvtSystem;
        private Label lblCompletoComponent;
        private Label lblDisponibleComponent;
        private Label lblComponent;
        private Label lblCompletoCatalyst;
        private Label lblDisponibleCatalyst;
        private Label lblCatalyst;
        private Label lblCompletoBoostPressure;
        private Label lblDisponibleBoostPressure;
        private Label lblBoostPressure;
        private Label lblCompletoAcRefrigerant;
        private Label lblDisponibleAcRefrigerant;
        private Label lblAcRefrigerant;
        private Label lblCompleto;
        private Label lblDisponiple;
        private Label lblMonitorTitulo;
        private Label lblrCalId;
        private Label lblrBateria;
        private Label lblBateria;
        private Label lblCalId;
        private Label lblOBDClear;
        private Label lblrOBDClear;
        private Label lblRunTimeMil;
        private Label lblrRunTimeMil;
        private Label lblNoxScrAftertreatment;
        private Label lblDisponibleNoxScrAftertreatment;
        private Label lblCompletoNoxScrAftertreatment;
        private SplitContainer splitContainer1;
        private Label lblResultado;
        private Label lblTitulo2;
    }
}