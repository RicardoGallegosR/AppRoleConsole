using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmComun.CapturaCentralizada.Complementos {
    public sealed class DatosInicioVerificacion {
        public int ConsultasSemoviId { get; init; } = 0;
        public string Vin { get; init; } = string.Empty;
        public short Modelo { get; init; } = 0;
        public string TipoServicio { get; init; } = string.Empty;
        public string FolioAuto { get; init; } = string.Empty;
        public DateTime FechaTC { get; init; } = new DateTime(1900, 1, 1);
        public bool TestFM { get; init; } = false;
        public bool ConexionWs { get; init; } = false;
        public byte ConexionWebSrv { get; init; } = 1;
        public bool AdeudoFotoCivicas { get; init; } = false;
        public bool AdeudoTenencia { get; init; } = false;
        public bool AdeudoInfraccion { get; init; } = false;
        public bool GdfNoRegistrado { get; init; } = false;
    }
    public sealed class CrearVerificacionEventArgs : EventArgs {
        public DatosInicioVerificacion Datos { get; }
        public CrearVerificacionEventArgs(DatosInicioVerificacion datos) {
            Datos = datos;
        }
    }
}
