namespace Apps_Administrativa.Formularios {
    partial class Home {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            pnlPrincipal = new Panel();
            splitContainer1 = new SplitContainer();
            pnlVersion = new Panel();
            lblVersion = new Label();
            menuStrip1 = new MenuStrip();
            msReportes = new ToolStripMenuItem();
            msCertificados = new ToolStripMenuItem();
            msCancelacionDeFolios = new ToolStripMenuItem();
            msCancelaciónDeFoliosSinUtilizar = new ToolStripMenuItem();
            msVerificacionesAnteriores = new ToolStripMenuItem();
            msExcento = new ToolStripMenuItem();
            msFisicomecanica = new ToolStripMenuItem();
            msCapturaDePruebas = new ToolStripMenuItem();
            msPassword = new ToolStripMenuItem();
            msPersonal = new ToolStripMenuItem();
            msEstatus = new ToolStripMenuItem();
            msBajas = new ToolStripMenuItem();
            msRestriccionesDeOpcionesMenú = new ToolStripMenuItem();
            msDesbloquearClave = new ToolStripMenuItem();
            msTesoreria = new ToolStripMenuItem();
            msRevisiónYLiberaciónDeAdeudos = new ToolStripMenuItem();
            msConsultaDeLineaDeCaptura = new ToolStripMenuItem();
            msDesbloquearPlaca = new ToolStripMenuItem();
            msMantenimiento = new ToolStripMenuItem();
            msConfiguraciónDeEstación = new ToolStripMenuItem();
            msListaActivos = new ToolStripMenuItem();
            msMantenimientoMEM = new ToolStripMenuItem();
            msRegistroOrdenesServicioMEM = new ToolStripMenuItem();
            msEstatusLinea = new ToolStripMenuItem();
            msAsignarLinea = new ToolStripMenuItem();
            msSalir = new ToolStripMenuItem();
            pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlVersion.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.White;
            pnlPrincipal.Controls.Add(splitContainer1);
            pnlPrincipal.Controls.Add(menuStrip1);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(1621, 476);
            pnlPrincipal.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlVersion);
            splitContainer1.Size = new Size(1621, 443);
            splitContainer1.SplitterDistance = 129;
            splitContainer1.TabIndex = 2;
            // 
            // pnlVersion
            // 
            pnlVersion.BackColor = Color.Crimson;
            pnlVersion.Controls.Add(lblVersion);
            pnlVersion.Dock = DockStyle.Fill;
            pnlVersion.Location = new Point(0, 0);
            pnlVersion.Name = "pnlVersion";
            pnlVersion.Size = new Size(129, 443);
            pnlVersion.TabIndex = 2;
            // 
            // lblVersion
            // 
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(0, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(129, 443);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Version";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 14F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { msReportes, msCertificados, msFisicomecanica, msPassword, msPersonal, msTesoreria, msDesbloquearPlaca, msMantenimiento, msEstatusLinea, msAsignarLinea, msSalir });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1621, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // msReportes
            // 
            msReportes.Name = "msReportes";
            msReportes.Size = new Size(97, 29);
            msReportes.Text = "Reportes";
            // 
            // msCertificados
            // 
            msCertificados.DropDownItems.AddRange(new ToolStripItem[] { msCancelacionDeFolios, msCancelaciónDeFoliosSinUtilizar, msVerificacionesAnteriores, msExcento });
            msCertificados.Enabled = false;
            msCertificados.Name = "msCertificados";
            msCertificados.Size = new Size(124, 29);
            msCertificados.Text = "Certificados";
            msCertificados.Visible = false;
            // 
            // msCancelacionDeFolios
            // 
            msCancelacionDeFolios.Name = "msCancelacionDeFolios";
            msCancelacionDeFolios.Size = new Size(356, 30);
            msCancelacionDeFolios.Text = "Cancelación de folios usados";
            // 
            // msCancelaciónDeFoliosSinUtilizar
            // 
            msCancelaciónDeFoliosSinUtilizar.Name = "msCancelaciónDeFoliosSinUtilizar";
            msCancelaciónDeFoliosSinUtilizar.Size = new Size(356, 30);
            msCancelaciónDeFoliosSinUtilizar.Text = "Cancelación de folios sin utilizar";
            // 
            // msVerificacionesAnteriores
            // 
            msVerificacionesAnteriores.Name = "msVerificacionesAnteriores";
            msVerificacionesAnteriores.Size = new Size(356, 30);
            msVerificacionesAnteriores.Text = "Verificaciones anteriores";
            // 
            // msExcento
            // 
            msExcento.Name = "msExcento";
            msExcento.Size = new Size(356, 30);
            msExcento.Text = "Excento";
            // 
            // msFisicomecanica
            // 
            msFisicomecanica.DropDownItems.AddRange(new ToolStripItem[] { msCapturaDePruebas });
            msFisicomecanica.Name = "msFisicomecanica";
            msFisicomecanica.Size = new Size(152, 29);
            msFisicomecanica.Text = "FisicoMecanica";
            // 
            // msCapturaDePruebas
            // 
            msCapturaDePruebas.Name = "msCapturaDePruebas";
            msCapturaDePruebas.Size = new Size(250, 30);
            msCapturaDePruebas.Text = "Captura de pruebas";
            msCapturaDePruebas.Click += msCapturaDePruebas_Click;
            // 
            // msPassword
            // 
            msPassword.Enabled = false;
            msPassword.Name = "msPassword";
            msPassword.Size = new Size(120, 29);
            msPassword.Text = "Contraseña";
            msPassword.Visible = false;
            // 
            // msPersonal
            // 
            msPersonal.DropDownItems.AddRange(new ToolStripItem[] { msEstatus, msBajas, msRestriccionesDeOpcionesMenú, msDesbloquearClave });
            msPersonal.Enabled = false;
            msPersonal.Name = "msPersonal";
            msPersonal.Size = new Size(96, 29);
            msPersonal.Text = "Personal";
            msPersonal.Visible = false;
            // 
            // msEstatus
            // 
            msEstatus.Name = "msEstatus";
            msEstatus.Size = new Size(353, 30);
            msEstatus.Text = "Estatus";
            // 
            // msBajas
            // 
            msBajas.Name = "msBajas";
            msBajas.Size = new Size(353, 30);
            msBajas.Text = "Bajas";
            // 
            // msRestriccionesDeOpcionesMenú
            // 
            msRestriccionesDeOpcionesMenú.Name = "msRestriccionesDeOpcionesMenú";
            msRestriccionesDeOpcionesMenú.Size = new Size(353, 30);
            msRestriccionesDeOpcionesMenú.Text = "Restricciones de opciones menú";
            // 
            // msDesbloquearClave
            // 
            msDesbloquearClave.Name = "msDesbloquearClave";
            msDesbloquearClave.Size = new Size(353, 30);
            msDesbloquearClave.Text = "Desbloquear clave";
            // 
            // msTesoreria
            // 
            msTesoreria.DropDownItems.AddRange(new ToolStripItem[] { msRevisiónYLiberaciónDeAdeudos, msConsultaDeLineaDeCaptura });
            msTesoreria.Enabled = false;
            msTesoreria.Name = "msTesoreria";
            msTesoreria.Size = new Size(100, 29);
            msTesoreria.Text = "Tesorería";
            msTesoreria.Visible = false;
            // 
            // msRevisiónYLiberaciónDeAdeudos
            // 
            msRevisiónYLiberaciónDeAdeudos.Name = "msRevisiónYLiberaciónDeAdeudos";
            msRevisiónYLiberaciónDeAdeudos.Size = new Size(364, 30);
            msRevisiónYLiberaciónDeAdeudos.Text = "Revisión y liberación  de adeudos";
            // 
            // msConsultaDeLineaDeCaptura
            // 
            msConsultaDeLineaDeCaptura.Name = "msConsultaDeLineaDeCaptura";
            msConsultaDeLineaDeCaptura.Size = new Size(364, 30);
            msConsultaDeLineaDeCaptura.Text = "Consulta de línea de captura";
            // 
            // msDesbloquearPlaca
            // 
            msDesbloquearPlaca.Enabled = false;
            msDesbloquearPlaca.Name = "msDesbloquearPlaca";
            msDesbloquearPlaca.Size = new Size(181, 29);
            msDesbloquearPlaca.Text = "Desbloquear Placa";
            msDesbloquearPlaca.Visible = false;
            // 
            // msMantenimiento
            // 
            msMantenimiento.DropDownItems.AddRange(new ToolStripItem[] { msConfiguraciónDeEstación, msListaActivos, msMantenimientoMEM, msRegistroOrdenesServicioMEM });
            msMantenimiento.Name = "msMantenimiento";
            msMantenimiento.Size = new Size(153, 29);
            msMantenimiento.Text = "Mantenimiento";
            // 
            // msConfiguraciónDeEstación
            // 
            msConfiguraciónDeEstación.Name = "msConfiguraciónDeEstación";
            msConfiguraciónDeEstación.Size = new Size(343, 30);
            msConfiguraciónDeEstación.Text = "Configuración de estación";
            msConfiguraciónDeEstación.Click += msConfiguraciónDeEstación_Click;
            // 
            // msListaActivos
            // 
            msListaActivos.Enabled = false;
            msListaActivos.Name = "msListaActivos";
            msListaActivos.Size = new Size(343, 30);
            msListaActivos.Text = "Lista activos";
            msListaActivos.Visible = false;
            // 
            // msMantenimientoMEM
            // 
            msMantenimientoMEM.Enabled = false;
            msMantenimientoMEM.Name = "msMantenimientoMEM";
            msMantenimientoMEM.Size = new Size(343, 30);
            msMantenimientoMEM.Text = "Mantenimiento MEM";
            msMantenimientoMEM.Visible = false;
            // 
            // msRegistroOrdenesServicioMEM
            // 
            msRegistroOrdenesServicioMEM.Enabled = false;
            msRegistroOrdenesServicioMEM.Name = "msRegistroOrdenesServicioMEM";
            msRegistroOrdenesServicioMEM.Size = new Size(343, 30);
            msRegistroOrdenesServicioMEM.Text = "Registro ordenes servicio MEM";
            msRegistroOrdenesServicioMEM.Visible = false;
            // 
            // msEstatusLinea
            // 
            msEstatusLinea.Enabled = false;
            msEstatusLinea.Name = "msEstatusLinea";
            msEstatusLinea.Size = new Size(133, 29);
            msEstatusLinea.Text = "Estatus Línea";
            msEstatusLinea.Visible = false;
            // 
            // msAsignarLinea
            // 
            msAsignarLinea.Enabled = false;
            msAsignarLinea.Name = "msAsignarLinea";
            msAsignarLinea.Size = new Size(138, 29);
            msAsignarLinea.Text = "Asignar Línea";
            msAsignarLinea.Visible = false;
            // 
            // msSalir
            // 
            msSalir.Name = "msSalir";
            msSalir.Size = new Size(61, 29);
            msSalir.Text = "Salir";
            msSalir.Click += msSalir_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1621, 476);
            ControlBox = false;
            Controls.Add(pnlPrincipal);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Load += Home_Load;
            pnlPrincipal.ResumeLayout(false);
            pnlPrincipal.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlVersion.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private MenuStrip menuStrip1;
        private Label lblVersion;
        private SplitContainer splitContainer1;
        private Panel pnlVersion;
        private ToolStripMenuItem msReportes;
        private ToolStripMenuItem msFisicomecanica;
        private ToolStripMenuItem msPassword;
        private ToolStripMenuItem msPersonal;
        private ToolStripMenuItem msTesoreria;
        private ToolStripMenuItem msDesbloquearPlaca;
        private ToolStripMenuItem msMantenimiento;
        private ToolStripMenuItem msEstatusLinea;
        private ToolStripMenuItem msAsignarLinea;
        private ToolStripMenuItem msCertificados;
        private ToolStripMenuItem msCancelacionDeFolios;
        private ToolStripMenuItem msCancelaciónDeFoliosSinUtilizar;
        private ToolStripMenuItem msVerificacionesAnteriores;
        private ToolStripMenuItem msExcento;
        private ToolStripMenuItem msSalir;
        private ToolStripMenuItem msEstatus;
        private ToolStripMenuItem msBajas;
        private ToolStripMenuItem msRestriccionesDeOpcionesMenú;
        private ToolStripMenuItem msDesbloquearClave;
        private ToolStripMenuItem msRevisiónYLiberaciónDeAdeudos;
        private ToolStripMenuItem msConsultaDeLineaDeCaptura;
        private ToolStripMenuItem msConfiguraciónDeEstación;
        private ToolStripMenuItem msListaActivos;
        private ToolStripMenuItem msMantenimientoMEM;
        private ToolStripMenuItem msRegistroOrdenesServicioMEM;
        private ToolStripMenuItem msCapturaDePruebas;
    }
}