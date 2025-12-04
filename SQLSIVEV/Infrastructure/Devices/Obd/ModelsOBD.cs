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
        public string? VehiculoId { get; init; }
        public string? ProtocoloObd { get; init; }
        public string? CodError { get; init; }
        public string? CodErrorPend { get; init; }

        public byte? Intentos { get; init; }
        public byte? ConexionOb { get; init; }
        public byte? Mil { get; init; }
        public byte? Fallas { get; init; }
        public byte? Sdciic { get; init; }
        public byte? Secc { get; init; }
        public byte? Sc { get; init; }
        public byte? Sso { get; init; }
        public byte? Sci { get; init; }
        public byte? Sccc { get; init; }
        public byte? Se { get; init; }
        public byte? Ssa { get; init; }
        public byte? Sfaa { get; init; }
        public byte? Scso { get; init; }
        public byte? Srge { get; init; }

        public decimal? VoltsSwOff { get; init; }
        public decimal? VoltsSwOn { get; init; }

        public short? RpmOff { get; init; }
        public short? RpmOn { get; init; }
        public short? RpmCheck { get; init; }

        public bool? LeeMonitores { get; init; }
        public bool? LeeDtc { get; init; }
        public bool? LeeDtcPend { get; init; }
        public bool? LeeVin { get; init; }
        public string? mensaje { get; init; }

        // NUEVOS VALORES 
        public int? distMilKm { get; init; }
        public int? distSinceClrKm { get; init; }
        public int? runTimeMilMin { get; init; }
        public int? timeSinceClr { get; init; }

        public string? cvn { get; init; }
        public string[] cal { get; init; }

        // toñin cara de pan
        public int? TiempoTotalSegundosOperacionMotor { get; init;}
        public int? WarmUpsDesdeBorrado { get; init; }

    }

}
