namespace Apps_Regedit.Views.Verificentros {
    partial class Visual {
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
            tableLayoutPanel1 = new TableLayoutPanel();
            grpBaseDatos = new GroupBox();
            ucDataBase1 = new Apps_Regedit.Views.Configuracion.ucDataBase();
            grpEstacion = new GroupBox();
            pnlScrollEstacion = new Panel();
            ucEstacion1 = new Apps_Regedit.Views.Configuracion.ucEstacion();
            pnlFooter = new Panel();
            ucAcciones1 = new Apps_Regedit.Views.Configuracion.ucAcciones();
            pnlHeader = new Panel();
            lblTituloConfiguracion = new Label();
            pnlPrincipal.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpBaseDatos.SuspendLayout();
            grpEstacion.SuspendLayout();
            pnlScrollEstacion.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = SystemColors.Control;
            pnlPrincipal.Controls.Add(groupBox1);
            pnlPrincipal.Controls.Add(tableLayoutPanel1);
            pnlPrincipal.Controls.Add(pnlFooter);
            pnlPrincipal.Controls.Add(pnlHeader);
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(0, 0);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.Size = new Size(856, 621);
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
            groupBox1.Size = new Size(856, 122);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Propiedades unicas";
            // 
            // pnlValoresUnicos
            // 
            pnlValoresUnicos.BackColor = Color.White;
            pnlValoresUnicos.Dock = DockStyle.Fill;
            pnlValoresUnicos.Location = new Point(10, 32);
            pnlValoresUnicos.Name = "pnlValoresUnicos";
            pnlValoresUnicos.Size = new Size(836, 80);
            pnlValoresUnicos.TabIndex = 0;
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
            tableLayoutPanel1.Size = new Size(856, 354);
            tableLayoutPanel1.TabIndex = 4;
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
            grpBaseDatos.Size = new Size(415, 338);
            grpBaseDatos.TabIndex = 0;
            grpBaseDatos.TabStop = false;
            grpBaseDatos.Text = "Conexión a Base de Datos";
            // 
            // ucDataBase1
            // 
            ucDataBase1.Dock = DockStyle.Fill;
            ucDataBase1.Location = new Point(2, 22);
            ucDataBase1.Name = "ucDataBase1";
            ucDataBase1.Size = new Size(411, 314);
            ucDataBase1.SoloLectura = false;
            ucDataBase1.TabIndex = 0;
            // 
            // grpEstacion
            // 
            grpEstacion.Controls.Add(pnlScrollEstacion);
            grpEstacion.Dock = DockStyle.Fill;
            grpEstacion.Font = new Font("Segoe UI", 12F);
            grpEstacion.Location = new Point(439, 8);
            grpEstacion.Margin = new Padding(8);
            grpEstacion.Name = "grpEstacion";
            grpEstacion.Padding = new Padding(0);
            grpEstacion.Size = new Size(409, 338);
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
            pnlScrollEstacion.Size = new Size(409, 316);
            pnlScrollEstacion.TabIndex = 0;
            // 
            // ucEstacion1
            // 
            ucEstacion1.Dock = DockStyle.Top;
            ucEstacion1.Location = new Point(0, 0);
            ucEstacion1.Margin = new Padding(4, 4, 4, 4);
            ucEstacion1.Name = "ucEstacion1";
            ucEstacion1.Size = new Size(392, 442);
            ucEstacion1.SoloLectura = false;
            ucEstacion1.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(ucAcciones1);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 546);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(8);
            pnlFooter.Size = new Size(856, 75);
            pnlFooter.TabIndex = 3;
            // 
            // ucAcciones1
            // 
            ucAcciones1.BitacoraHabilitada = true;
            ucAcciones1.Dock = DockStyle.Fill;
            ucAcciones1.Font = new Font("Segoe UI", 12F);
            ucAcciones1.Location = new Point(8, 8);
            ucAcciones1.Margin = new Padding(0);
            ucAcciones1.Name = "ucAcciones1";
            ucAcciones1.Size = new Size(840, 59);
            ucAcciones1.TabIndex = 0;
            ucAcciones1.TextoBitacora = "Bitácora ";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTituloConfiguracion);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(12);
            pnlHeader.Size = new Size(856, 70);
            pnlHeader.TabIndex = 2;
            // 
            // lblTituloConfiguracion
            // 
            lblTituloConfiguracion.Dock = DockStyle.Fill;
            lblTituloConfiguracion.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTituloConfiguracion.Location = new Point(12, 12);
            lblTituloConfiguracion.Name = "lblTituloConfiguracion";
            lblTituloConfiguracion.Size = new Size(832, 46);
            lblTituloConfiguracion.TabIndex = 0;
            lblTituloConfiguracion.Text = "CONFIGURACIÓN DE VISUAL";
            // 
            // Visual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPrincipal);
            Name = "Visual";
            Size = new Size(856, 621);
            Load += Visual_Load;
            pnlPrincipal.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            grpBaseDatos.ResumeLayout(false);
            grpEstacion.ResumeLayout(false);
            pnlScrollEstacion.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrincipal;
        private Panel pnlHeader;
        private Label lblTituloConfiguracion;
        private Panel pnlFooter;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox grpBaseDatos;
        private GroupBox grpEstacion;
        private Panel pnlScrollEstacion;
        private GroupBox groupBox1;
        private Panel pnlValoresUnicos;
        private Configuracion.ucDataBase ucDataBase1;
        private Configuracion.ucEstacion ucEstacion1;
        private Configuracion.ucAcciones ucAcciones1;
    }
}
