using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class ModelsOBD {

    }

    public sealed class LecturasIniciales {
        public int? rpm { get; init; }
        public int? vel { get; init; }
        public string []cal { get; init; }
        public string protocolo { get; init; } = "";
        public string vin { get; init; } = "";
        public string Mensaje { get; init; } = "";
        public double? vOff { get; init; }
        public string dtcList03 { get; init; } = "";
        public string dtcList07 { get; init; } = "";
        public string dtcList0A { get; init; } = "";

        public int? distMilKm { get; init; }
        public int? distSinceClrKm { get; init; }
        public int? runTimeMilMin { get; init; }
        public int? timeSinceClr { get; init; }

    }

    public sealed class ObdMonitoresLuzMil {
        public string mensaje { get; set; }
        public byte? Intentos { get; set; }
        public byte? ConexionOb { get; set; }
        public bool? Mil { get; set; }
        public byte? Fallas { get; set; }
        public byte? Sdciic { get; set; }
        public byte? Secc { get; set; }
        public byte? Sc { get; set; }
        public byte? Sso { get; set; }
        public byte? Sci { get; set; }
        public byte? Sccc { get; set; }
        public byte? Se { get; set; }
        public byte? Ssa { get; set; }
        public byte? Sfaa { get; set; }
        public byte? Scso { get; set; }
        public byte? Srge { get; set; }

        public bool? LeeMonitores { get; set; }
        public bool? LeeDtc { get; set; }
        public bool? LeeDtcPend { get; set; }
        public bool? LeeVin { get; set; }
        public int? DtcCount { get; set; }

    }


    public sealed class ObdResultado {
        public string? VehiculoId { get; set; }
        public string? ProtocoloObd { get; set; }
        public string? CodError { get; set; }
        public string? CodErrorPend { get; set; }

        public byte? Intentos { get; set; }
        public byte? ConexionOb { get; set; }
        public byte? Mil { get; set; }
        public byte? Fallas { get; set; }
        public byte? Sdciic { get; set; }
        public byte? Secc { get; set; }
        public byte? Sc { get; set; }
        public byte? Sso { get; set; }
        public byte? Sci { get; set; }
        public byte? Sccc { get; set; }
        public byte? Se { get; set; }
        public byte? Ssa { get; set; }
        public byte? Sfaa { get; set; }
        public byte? Scso { get; set; }
        public byte? Srge { get; set; }

        public decimal? VoltsSwOff { get; set; }
        public decimal? VoltsSwOn { get; set; }

        public short? RpmOff { get; set; }
        public short? RpmOn { get; set; }
        public short? RpmCheck { get; set; }

        public bool? LeeMonitores { get; set; }
        public bool? LeeDtc { get; set; }
        public bool? LeeDtcPend { get; set; }
        public bool? LeeVin { get; set; }
        public string? mensaje { get; set; }
    }

}
