using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmComun.FrmUtils {
    public partial class frmMensajes : Form {
        private string _mensaje = string.Empty;
        private string _titulo = string.Empty;
        public string Mensaje {
            get => _mensaje;
            set {
                _mensaje = value ?? string.Empty;
                if (lblMensajes != null) lblMensajes.Text = _mensaje;
            }
        }
        public string Titulo {
            get => _titulo;
            set {
                _titulo = value ?? string.Empty;
                if (lblTitulo != null) lblTitulo.Text = _titulo;
            }
        }
        public frmMensajes() {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();
    }
}
