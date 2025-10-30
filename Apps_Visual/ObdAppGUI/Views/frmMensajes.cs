using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmMensajes : Form {
        private string _mensaje = string.Empty;
        private bool _Cerrar = true;
        public string Mensaje {
            get => _mensaje;
            set {
                _mensaje = value ?? string.Empty;
                if (txbMensaje != null) txbMensaje.Text = _mensaje;
            }
        }

        public bool Cerrar {
            get => _Cerrar;
            set {
                _Cerrar = value;
                btnCerrar.Enabled = _Cerrar;
            }
        }

        public frmMensajes() {
            InitializeComponent();
        }

        
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();

    }
}
