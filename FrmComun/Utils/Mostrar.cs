using FrmComun.FrmUtils;

namespace FrmComun.Utils {
    public static class Mostrar {
        public static void Mensaje(string titulo = "", string mensaje = "") {
            using (var dlg = new frmMensajes()) {
                dlg.Titulo = titulo;
                dlg.Mensaje = mensaje;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog();
            }
        }
        /*
        public static void MensajesResultadoOBDII(ResultadoOBDII resultado) {
            using (var dlg = new frmMensajeResultadoOBD()) {
                dlg.OBDII = resultado;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog();
            }
        }
        */
        public static void MensajesResultadoOBDII(IWin32Window owner, ResultadoOBDII resultado) {
            using var dlg = new frmMensajeResultadoOBD();
            dlg.OBDII = resultado;
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowInTaskbar = false;
            dlg.TopMost = true;

            dlg.ShowDialog(owner);
        }
    }
}
