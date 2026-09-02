namespace Apps_Regedit.Views.Configuracion {
    partial class ucAcciones {
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
            flpBotones = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnLeer = new Button();
            btnBuscarEstacion = new Button();
            btnBitacora = new Button();
            flpBotones.SuspendLayout();
            SuspendLayout();
            // 
            // flpBotones
            // 
            flpBotones.Controls.Add(btnGuardar);
            flpBotones.Controls.Add(btnLeer);
            flpBotones.Controls.Add(btnBuscarEstacion);
            flpBotones.Controls.Add(btnBitacora);
            flpBotones.Dock = DockStyle.Fill;
            flpBotones.FlowDirection = FlowDirection.RightToLeft;
            flpBotones.Location = new Point(0, 0);
            flpBotones.Margin = new Padding(0);
            flpBotones.Name = "flpBotones";
            flpBotones.Padding = new Padding(4);
            flpBotones.Size = new Size(834, 59);
            flpBotones.TabIndex = 0;
            flpBotones.WrapContents = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Crimson;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(726, 8);
            btnGuardar.Margin = new Padding(4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(96, 34);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnLeer
            // 
            btnLeer.BackColor = Color.White;
            btnLeer.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210);
            btnLeer.FlatStyle = FlatStyle.Flat;
            btnLeer.ForeColor = Color.FromArgb(45, 55, 65);
            btnLeer.Location = new Point(622, 8);
            btnLeer.Margin = new Padding(4);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(96, 34);
            btnLeer.TabIndex = 0;
            btnLeer.TabStop = false;
            btnLeer.Text = "Leer";
            btnLeer.UseVisualStyleBackColor = false;
            // 
            // btnBuscarEstacion
            // 
            btnBuscarEstacion.BackColor = Color.White;
            btnBuscarEstacion.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210);
            btnBuscarEstacion.FlatStyle = FlatStyle.Flat;
            btnBuscarEstacion.ForeColor = Color.FromArgb(45, 55, 65);
            btnBuscarEstacion.Location = new Point(469, 8);
            btnBuscarEstacion.Margin = new Padding(4);
            btnBuscarEstacion.Name = "btnBuscarEstacion";
            btnBuscarEstacion.Size = new Size(145, 34);
            btnBuscarEstacion.TabIndex = 2;
            btnBuscarEstacion.Text = "Buscar estación";
            btnBuscarEstacion.UseVisualStyleBackColor = false;
            // 
            // btnBitacora
            // 
            btnBitacora.BackColor = Color.White;
            btnBitacora.FlatAppearance.BorderColor = Color.FromArgb(210, 210, 210);
            btnBitacora.FlatStyle = FlatStyle.Flat;
            btnBitacora.ForeColor = Color.FromArgb(45, 55, 65);
            btnBitacora.Location = new Point(292, 8);
            btnBitacora.Margin = new Padding(4);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(169, 34);
            btnBitacora.TabIndex = 3;
            btnBitacora.Text = "Bitácora ";
            btnBitacora.UseVisualStyleBackColor = false;
            // 
            // ucAcciones
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flpBotones);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(0);
            Name = "ucAcciones";
            Size = new Size(834, 59);
            flpBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpBotones;
        private Button btnGuardar;
        private Button btnLeer;
        private Button btnBuscarEstacion;
        private Button btnBitacora;
    }
}
