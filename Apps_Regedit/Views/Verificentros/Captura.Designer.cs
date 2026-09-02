namespace Apps_Regedit.Views.Verificentros {
    partial class Captura {
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
            groupBox1 = new GroupBox();
            pnlValoresUnicos = new Panel();
            tlpDatos = new TableLayoutPanel();
            txtRutaEscaneos = new TextBox();
            lblRuta = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            grpBaseDatos = new GroupBox();
            ucDataBase1 = new Apps_Regedit.Views.Configuracion.ucDataBase();
            grpEstacion = new GroupBox();
            pnlScrollEstacion = new Panel();
            ucEstacion1 = new Apps_Regedit.Views.Configuracion.ucEstacion();
            pnlHeader = new Panel();
            lblTituloConfiguracion = new Label();
            pnlFooter = new Panel();
            ucAcciones1 = new Apps_Regedit.Views.Configuracion.ucAcciones();
            pnlPrincipal.SuspendLayout();
            groupBox1.SuspendLayout();
            pnlValoresUnicos.SuspendLayout();
            tlpDatos.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpBaseDatos.SuspendLayout();
            grpEstacion.SuspendLayout();
            pnlScrollEstacion.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.AutoScroll = true;
            pnlPrincipal.BackColor = Color.WhiteSmoke;
            pnlPrincipal.Controls.Add(groupBox1);
            pnlPrincipal.Controls.Add(tableLayoutPanel1);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Controls.Add(pnlFooter);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(964, 726);
            pnlPrincipal.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pnlValoresUnicos);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 12F);
            groupBox1.Location = new Point(0, 424);
            groupBox1.Margin = new Padding(6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(10);
            groupBox1.Size = new Size(964, 227);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Propiedades unicas";
            // 
            // pnlValoresUnicos
            // 
            pnlValoresUnicos.BackColor = Color.White;
            pnlValoresUnicos.Controls.Add(tlpDatos);
            pnlValoresUnicos.Dock = DockStyle.Fill;
            pnlValoresUnicos.Location = new Point(10, 32);
            pnlValoresUnicos.Name = "pnlValoresUnicos";
            pnlValoresUnicos.Size = new Size(944, 185);
            pnlValoresUnicos.TabIndex = 0;
            // 
            // tlpDatos
            // 
            tlpDatos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDatos.ColumnCount = 2;
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpDatos.Controls.Add(txtRutaEscaneos, 1, 0);
            tlpDatos.Controls.Add(lblRuta, 0, 0);
            tlpDatos.Dock = DockStyle.Fill;
            tlpDatos.Font = new Font("Segoe UI", 12F);
            tlpDatos.ForeColor = Color.FromArgb(45, 55, 65);
            tlpDatos.Location = new Point(0, 0);
            tlpDatos.Name = "tlpDatos";
            tlpDatos.Padding = new Padding(14, 10, 14, 10);
            tlpDatos.RowCount = 1;
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDatos.Size = new Size(944, 185);
            tlpDatos.TabIndex = 1;
            // 
            // txtRutaEscaneos
            // 
            txtRutaEscaneos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtRutaEscaneos.Location = new Point(294, 78);
            txtRutaEscaneos.Margin = new Padding(6);
            txtRutaEscaneos.Name = "txtRutaEscaneos";
            txtRutaEscaneos.Size = new Size(630, 29);
            txtRutaEscaneos.TabIndex = 0;
            // 
            // lblRuta
            // 
            lblRuta.AutoSize = true;
            lblRuta.Dock = DockStyle.Fill;
            lblRuta.Location = new Point(18, 14);
            lblRuta.Margin = new Padding(4);
            lblRuta.Name = "lblRuta";
            lblRuta.Size = new Size(266, 157);
            lblRuta.TabIndex = 1;
            lblRuta.Text = "Ruta de escaneos";
            lblRuta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.4149361F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.5850639F));
            tableLayoutPanel1.Controls.Add(grpBaseDatos, 0, 0);
            tableLayoutPanel1.Controls.Add(grpEstacion, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 70);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(964, 354);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // grpBaseDatos
            // 
            grpBaseDatos.Controls.Add(ucDataBase1);
            grpBaseDatos.Dock = DockStyle.Fill;
            grpBaseDatos.Font = new Font("Segoe UI", 11F);
            grpBaseDatos.Location = new Point(8, 8);
            grpBaseDatos.Margin = new Padding(8);
            grpBaseDatos.Name = "grpBaseDatos";
            grpBaseDatos.Padding = new Padding(2);
            grpBaseDatos.Size = new Size(470, 338);
            grpBaseDatos.TabIndex = 0;
            grpBaseDatos.TabStop = false;
            grpBaseDatos.Text = "Conexión a Base de Datos";
            // 
            // ucDataBase1
            // 
            ucDataBase1.Dock = DockStyle.Fill;
            ucDataBase1.Location = new Point(2, 22);
            ucDataBase1.Margin = new Padding(4);
            ucDataBase1.Name = "ucDataBase1";
            ucDataBase1.Size = new Size(466, 314);
            ucDataBase1.SoloLectura = false;
            ucDataBase1.TabIndex = 0;
            // 
            // grpEstacion
            // 
            grpEstacion.Controls.Add(pnlScrollEstacion);
            grpEstacion.Dock = DockStyle.Fill;
            grpEstacion.Font = new Font("Segoe UI", 12F);
            grpEstacion.Location = new Point(494, 8);
            grpEstacion.Margin = new Padding(8);
            grpEstacion.Name = "grpEstacion";
            grpEstacion.Padding = new Padding(0);
            grpEstacion.Size = new Size(462, 338);
            grpEstacion.TabIndex = 1;
            grpEstacion.TabStop = false;
            grpEstacion.Text = "Configuración de Estación";
            // 
            // pnlScrollEstacion
            // 
            pnlScrollEstacion.AutoScroll = true;
            pnlScrollEstacion.Controls.Add(ucEstacion1);
            pnlScrollEstacion.Dock = DockStyle.Fill;
            pnlScrollEstacion.Location = new Point(0, 22);
            pnlScrollEstacion.Name = "pnlScrollEstacion";
            pnlScrollEstacion.Size = new Size(462, 316);
            pnlScrollEstacion.TabIndex = 0;
            // 
            // ucEstacion1
            // 
            ucEstacion1.Dock = DockStyle.Top;
            ucEstacion1.Location = new Point(0, 0);
            ucEstacion1.Name = "ucEstacion1";
            ucEstacion1.Size = new Size(445, 400);
            ucEstacion1.SoloLectura = false;
            ucEstacion1.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTituloConfiguracion);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(12);
            pnlHeader.Size = new Size(964, 70);
            pnlHeader.TabIndex = 1;
            // 
            // lblTituloConfiguracion
            // 
            lblTituloConfiguracion.Dock = DockStyle.Fill;
            lblTituloConfiguracion.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTituloConfiguracion.Location = new Point(12, 12);
            lblTituloConfiguracion.Name = "lblTituloConfiguracion";
            lblTituloConfiguracion.Size = new Size(940, 46);
            lblTituloConfiguracion.TabIndex = 0;
            lblTituloConfiguracion.Text = "CONFIGURACIÓN DE CAPTURA CENTRALIZADA";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(ucAcciones1);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 651);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(8);
            pnlFooter.Size = new Size(964, 75);
            pnlFooter.TabIndex = 0;
            // 
            // ucAcciones1
            // 
            ucAcciones1.AutoLogonHabilitado = true;
            ucAcciones1.BitacoraHabilitada = true;
            ucAcciones1.BuscarEstacionHabilitado = true;
            ucAcciones1.Dock = DockStyle.Fill;
            ucAcciones1.Font = new Font("Segoe UI", 12F);
            ucAcciones1.GuardarHabilitado = true;
            ucAcciones1.Location = new Point(8, 8);
            ucAcciones1.Margin = new Padding(0);
            ucAcciones1.Name = "ucAcciones1";
            ucAcciones1.Padding = new Padding(4);
            ucAcciones1.Size = new Size(948, 59);
            ucAcciones1.TabIndex = 0;
            ucAcciones1.TextoAutoLogon = "Auto Logon";
            ucAcciones1.TextoBitacora = "Bitácora ";
            ucAcciones1.TextoBuscarEstacion = "Buscar estación";
            // 
            // Captura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "Captura";
            Size = new Size(964, 726);
            Load += Captura_Load;
            pnlPrincipal.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            pnlValoresUnicos.ResumeLayout(false);
            tlpDatos.ResumeLayout(false);
            tlpDatos.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            grpBaseDatos.ResumeLayout(false);
            grpEstacion.ResumeLayout(false);
            pnlScrollEstacion.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlFooter;
        private Panel pnlHeader;
        private TableLayoutPanel tableLayoutPanel1;
        private Configuracion.ucAcciones ucAcciones1;
        private GroupBox grpBaseDatos;
        private Configuracion.ucDataBase ucDataBase1;
        private Label lblTituloConfiguracion;
        private GroupBox grpEstacion;
        private Panel pnlScrollEstacion;
        private Configuracion.ucEstacion ucEstacion1;
        private GroupBox groupBox1;
        private Panel pnlValoresUnicos;
        private TableLayoutPanel tlpDatos;
        private TextBox txtRutaEscaneos;
        private Label lblRuta;
    }
}
