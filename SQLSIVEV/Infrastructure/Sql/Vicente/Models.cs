using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Sql.Vicente {
    internal class Models {

    }
    public sealed record ProgresoConsulta {
        public int Actual { get; init; }
        public int Total { get; init; }
        public int VerificentroId { get; init; }
    }
    public sealed class EmaPorCVEV {
        public DateTime Fecha { get; init; } = DateTime.Today;
        public int VerificentroId { get; init; }
        public string Verificentro { get; set; } = string.Empty;

        public string Descripcion => VerificentroId == 0 ? Verificentro : $"{VerificentroId} - {Verificentro}";
    }
    public sealed class ResumenCertificadosCVEV {
        //public int VerificentroId { get; set; }
        public string NombreDominio { get; set; } = "DESCONOCIDO";
        public int Rechazo { get; set; }
        public int Dos { get; set; }
        public int Uno { get; set; }
        public int Cero { get; set; }
        public int DbCero { get; set; }
        public int Total { get; set; }
    }
    public sealed class ReporteEma {
        public string Verificentro { get; init; } = string.Empty;
        public int folio { get; init; }
        public string Holograma { get; init; } = string.Empty;
        public string Marca { get; init; } = string.Empty;
        public string Placa { get; init; } = string.Empty;
        public string Norma { get; init; } = string.Empty;
        public string fechaCaptura { get; init; } = string.Empty;
        public string horaCaptura { get; init; } = string.Empty;
        public string Tecnico { get; init; } = string.Empty;
    }
}
