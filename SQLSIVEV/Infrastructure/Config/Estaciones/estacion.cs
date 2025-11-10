using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Config.Estaciones {
    public class estacion {
        public short PuertoMCEP { get; set; }
        public short PuertoMCS { get; set; }
        public short PuertoMCT { get; set; }
        public short PuertoMEM { get; set; }
        public short PuertoMSM { get; set; }
        public short PuertoOpacimetro { get; set; }
        public string SqlServerName { get; set; }
        public string BaseDatos { get; set; }
        public string DireccionIP { get; set; }
        public string Dominio { get; set; }
        public string NombreCPU { get; set; }
        public int Centro { get; set; }
        public short Estacion { get; set; }
        //public string EstacionId { get; set; }
        public string EstacionId { get; set; }
        public short DebugActivo { get; set; }
        public short NivelActivo { get; set; }
        public short ConectaSF { get; set; }
    }
}
