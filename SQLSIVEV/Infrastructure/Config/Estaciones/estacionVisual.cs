using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Config.Estaciones {
    public class estacionVisual {

        // Acceso a BDD
        public string SqlServerName { get; set; }
        public string BaseDatos { get; set; }
        public string SQL_USER { get; set; }
        public string SQL_PASS { get; set; }

        public string APPNAME { get; set; }


        // Acceso a Roles Sivev
        public string APPROLE { get; set; }
        public string APPROLE_PASS { get; set; }

        // Acceso a Roles Aplicacion
        public string APPROLE_VISUAL { get; set; }
        public string APPROLE_PASS_VISUAL { get; set; }




        // Obtenido
        public string ClaveAccesoId { get; set; }

        public string EstacionId { get; set; }

        public short opcionMenu { get; set; }
    }
}
