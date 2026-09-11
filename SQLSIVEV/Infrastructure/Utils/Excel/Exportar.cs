using SQLSIVEV.Infrastructure.Sql.Vicente;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Utils.Excel {
    public static class Exportar {
        public static string ReporteEma(string ruta, EmaPorCVEV emaPorCVEV, DataTable dt) {
            try {
                string dir = Path.Combine( ruta, "EMA",  emaPorCVEV.Fecha.Year.ToString(), emaPorCVEV.Fecha.Month.ToString("00")   );
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string archivo = Path.Combine(dir, $"SERVICIOS REALIZADOS {emaPorCVEV.Verificentro} {emaPorCVEV.Fecha:yyyy-MM}.xlsx");
                using var osDocument = new SLDocument();
                string hoja = $"{ObtenerNombreMes(emaPorCVEV.Fecha.Month)} {emaPorCVEV.Fecha.Year}";
                osDocument.RenameWorksheet("Sheet1", hoja);
                osDocument.ImportDataTable(1, 1, dt, true);
                osDocument.SaveAs(archivo);
                return archivo;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al exportar a Excel: {ex.Message}", SivevOrigen.Configurador);
                throw;
            }
        }
        public static string ReporteRemanente(string ruta, EmaPorCVEV emaPorCVEV, DataTable dt) {
            try {
                string dir = Path.Combine( ruta, "Certificados",  emaPorCVEV.Fecha.Year.ToString(), emaPorCVEV.Fecha.Month.ToString("00")   );
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string archivo = Path.Combine(dir, $"REPORTE {emaPorCVEV.Verificentro} {emaPorCVEV.Fecha:yyyy-MM}.xlsx");
                using var osDocument = new SLDocument();
                string hoja = $"{ObtenerNombreMes(emaPorCVEV.Fecha.Month)} {emaPorCVEV.Fecha.Year}";
                osDocument.RenameWorksheet("Sheet1", hoja);
                osDocument.ImportDataTable(1, 1, dt, true);
                osDocument.SaveAs(archivo);
                return archivo;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al exportar a Excel: {ex.Message}", SivevOrigen.Configurador);
                throw;
            }
        }
        private static string ObtenerNombreMes(int numeroMes) {
            var cultura = new CultureInfo("es-MX");
            var fecha = new DateTime(2000, numeroMes, 1);
            return cultura.TextInfo
                .ToTitleCase(fecha.ToString("MMMM", cultura));
        }
    }
}
