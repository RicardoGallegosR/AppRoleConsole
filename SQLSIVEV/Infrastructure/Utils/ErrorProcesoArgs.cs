using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Utils {
    public record ErrorProcesoArgs(string Libreria, string Clase, string Metodo, int ErrNum, string ErrDesc, int SqlErr = 0) {
        public override string ToString() => $"[{Libreria}.{Clase}.{Metodo}] Error {ErrNum}: {ErrDesc} (SQL:{SqlErr})";
    }
}
