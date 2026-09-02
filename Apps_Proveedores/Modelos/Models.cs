using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps_Proveedores.Modelos {
    internal class Models {
    }
    public sealed class EstadoSincronizacion {
        public bool ServicioActivo { get; set; }
        public string Origen { get; set; } = "";
        public string UltimaSincronizacion { get; set; } = "";
        public bool Sincronizado { get; set; }
        public string Diferencia { get; set; } = "No disponible";
    }
}
