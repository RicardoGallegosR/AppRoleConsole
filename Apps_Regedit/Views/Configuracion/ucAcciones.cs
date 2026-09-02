using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Regedit.Views.Configuracion {
    public partial class ucAcciones : UserControl {
        #region Eventos
        public event EventHandler? GuardarClick;
        public event EventHandler? LeerClick;
        public event EventHandler? BuscarEstacionClick;
        public event EventHandler? BitacoraClick;
        #endregion

        #region Propiedades
        public string TextoBitacora {
            get => btnBitacora.Text;
            set => btnBitacora.Text = value;
        }

        public bool BitacoraHabilitada {
            get => btnBitacora.Enabled;
            set => btnBitacora.Enabled = value;
        }
        #endregion
        #region Constructor
        public ucAcciones() {
            InitializeComponent();

            btnGuardar.Click += btnGuardar_Click;
            btnLeer.Click += btnLeer_Click;
            btnBuscarEstacion.Click += btnBuscarEstacion_Click;
            btnBitacora.Click += btnBitacora_Click;
        }
        #endregion

        #region Métodos
        private void btnGuardar_Click(object sender, EventArgs e) => 
            GuardarClick?.Invoke(this, e);
        private void btnLeer_Click(object sender, EventArgs e) => 
            LeerClick?.Invoke(this, e);
        private void btnBuscarEstacion_Click(object sender, EventArgs e) => 
            BuscarEstacionClick?.Invoke(this, e);
        private void btnBitacora_Click(object sender, EventArgs e) => 
            BitacoraClick?.Invoke(this, e);
        #endregion
    }
}
