namespace FrmComun.Utils {
    public class ActividadAplicacionFilter : IMessageFilter {
        public event Action? ActividadDetectada;
        public bool PreFilterMessage(ref Message m) {
            // Teclado
            bool teclado = m.Msg >= 0x0100 && m.Msg <= 0x0109;

            // Mouse
            bool mouse = m.Msg >= 0x0200 && m.Msg <= 0x020E;
            if (teclado || mouse) {
                ActividadDetectada?.Invoke();
            }
            return false;
        }
    }
}
