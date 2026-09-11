using FrmComun.Utils;
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
    public partial class ucVisitante : UserControl {
        public event EventHandler? Acceso;
        public string Nombre => txtNombre.Text.Trim();
        public string ApellidoPaterno => txtApellidoP.Text.Trim();
        public string ApellidoMaterno => txtApellidoM.Text.Trim();
        public ucVisitante() {
            InitializeComponent();
            txtNombre.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtNombre, @"[^A-ZÁÉÍÓÚÜÑ ]"); 
            txtApellidoP.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtApellidoP, @"[^A-ZÁÉÍÓÚÜÑ ]"); 
            txtApellidoM.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtApellidoM , @"[^A-ZÁÉÍÓÚÜÑ ]");

            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtApellidoP.CharacterCasing = CharacterCasing.Upper;
            txtApellidoM.CharacterCasing = CharacterCasing.Upper;

            txtNombre.MaxLength = 50;
            txtApellidoP.MaxLength = 50;
            txtApellidoM.MaxLength = 50;

            btnAcceso.Click += btnAcceso_Click;
        }
        private void btnAcceso_Click(object sender, EventArgs e) =>
            Acceso?.Invoke(this, e);
    }
}
