using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Sql.Configuracion {
    public sealed class cnx {
        public string Servidor { get; init; } = "";
        public string BDD { get; init; } = "";
        public string User { get; init; } = "";
        public string Pass { get; init; } = "";
        public string AppName { get; init; } = "";
        public string EstacionId { get; set; }
        public string Dominio { get; set; } = "";
        public string InstanciaSQL { get; set; } = "";
        public short CentroId { get; set; } = 0;
        public string Centro { get; set; } = "";
    }
}
