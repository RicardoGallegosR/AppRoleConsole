using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucEscaneoDocumentos : UserControl {
        public event EventHandler? EscanearClick;
        public event EventHandler? AceptarClick;
        public event EventHandler? CancelarClick;

        public ucEscaneoDocumentos() {
            InitializeComponent();

            ibEscanear.Click += (s, e) => EscanearClick?.Invoke(this, e);
            ibAceptar.Click += (s, e) => AceptarClick?.Invoke(this, e);
            ibCancelar.Click += (s, e) => CancelarClick?.Invoke(this, e);
        }
    }
}
