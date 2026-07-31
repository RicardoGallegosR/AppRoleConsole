using FrmComun.Utils;

namespace FrmComun.FrmUtils {
    public partial class frmMensajeResultadoOBD : Form {
        private ResultadoOBDII resultadoOBDII;
        public ResultadoOBDII OBDII {
            get => resultadoOBDII;
            set {
                resultadoOBDII = value ?? new ResultadoOBDII();
                if (resultadoOBDII.Titulo != null) lblTitulo.Text = resultadoOBDII.Titulo;
                if (resultadoOBDII.Marca != null) _lblMarca.Text = resultadoOBDII.Marca;
                if (resultadoOBDII.SubMarca != null) _lblSubMarca.Text = resultadoOBDII.SubMarca;
                if (resultadoOBDII.Modelo != null) _lblModelo.Text = resultadoOBDII.Modelo;
                if (resultadoOBDII.DTCConfirmado != null) _lblDTCConfirmado.Text = resultadoOBDII.DTCConfirmado;
                if (resultadoOBDII.DTCPendiente != null) _lblDTCPendientes.Text = resultadoOBDII.DTCPendiente;
                if (resultadoOBDII.Protocolo != null) _lblProtocolo.Text = resultadoOBDII.Protocolo;

            }
        }
        public frmMensajeResultadoOBD() {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();
    }
}
