using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Utils {
    public static class Bitacora {
        public static SpAppBitacoraErroresSet ErroresSQL(Guid estacionId, short centro, short opcionMenuId, string descripcion, int codigoSql = 0, int codigo = 0, [CallerMemberName] string callerMember = "", [CallerFilePath] string callerFile = "", [CallerLineNumber] int callerLine = 0) {
            return new SpAppBitacoraErroresSet {
                EstacionId = estacionId,
                Centro = centro,
                NombreCpu = Environment.MachineName,
                OpcionMenuId = opcionMenuId,
                FechaError = DateTime.Now,
                Libreria = Path.GetFileName(callerFile),
                Clase = Path.GetFileNameWithoutExtension(callerFile),
                Metodo = callerMember,
                CodigoErrorSql = codigoSql,
                CodigoError = codigo,
                DescripcionError = descripcion,
                LineaCodigo = callerLine,
                LastDllError = 0,
                SourceError = "DESCONOCIDO"
            };
        }
    }
}
