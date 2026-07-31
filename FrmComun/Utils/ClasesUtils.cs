using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmComun.Utils {
    internal class ClasesUtils {
    }
    public sealed class ResultadoOBDII {
        public string Titulo { get; set; } = "";
        public string Marca { get; set; } = "";
        public string SubMarca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public string DTCConfirmado { get; set; } = "";
        public string DTCPendiente { get; set; } = "";
        public string Protocolo { get; set; } = "";
    }
}
