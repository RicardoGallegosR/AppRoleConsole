using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class ModelsOBD {

    }

    public sealed class LecturasIniciales {
        public int? rpm { get; init; }
        public int? vel { get; init; }
        public string[] cal { get; init; }
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
        public string Mensaje { get; set; }
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

    public sealed class TryHandshakeGet {
        public string? ProtocoloObd { get; set; }
        public byte? Intentos { get; init; }
        public byte? ConexionOb { get; init; }
        public decimal? VoltsSwOff { get; init; }
        public short? RpmOff { get; init; }
        public string? Mensaje { get; set; }
    }


    public sealed class ObdResultado {
        public string Mensaje { get; set; } = "";

        public string? VehiculoId { get; set; }
        public string? ProtocoloObd { get; set; }
        public string? CodError { get; set; }
        public string? CodErrorPend { get; set; }
        public string? CodErrorPerm { get; set; }

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

        // NUEVOS VALORES 
        public int? DistMilKm { get; set; }
        public int? DistSinceClrKm { get; set; }
        public int? RunTimeMilMin { get; set; }
        public int? TimeSinceClr { get; set; }

        public string? Cvn { get; set; }
        public string[] Cal { get; set; } = Array.Empty<string>();

        //011F
        public int? TiempoTotalSegundosOperacionMotor { get; set; }

        //0104
        public int? WarmUpsDesdeBorrado { get; set; }

        //101C
        public string? NormativaObdVehiculo { get; set; }

        //0105
        public int? IatCCoolantTempC { get; set; }

        //0106
        public double? StftB1 { get; set; }

        //0107
        public double? LtftB1 { get; set; }

        //010F
        public int? IatC { get; set; }

        //0110 EN SEGUNDOS Y HORAS
        public double? MafGs { get; set; }
        public double? MafKgH { get; set; }

        //0111
        public double? Tps { get; set; }

        //010E
        public double? TimingAdvance { get; set; }


        //0114 y 0115
        public double? O2S1_V { get; set; }  // Banco 1, Sensor 1 (V)
        public double? O2S2_V { get; set; }  // Banco 1, Sensor 2 (V)

        //012F
        public double? FuelLevel { get; set; }

        //0133
        public int? BarometricPressure { get; set; }

        //0151
        public string? FuelType { get; set; }
        public int? IntFuelType { get; set; }

        //0907
        public int? IntTipoCombustible0907 { get; set; }

        //090A
        public string? EcuAddress { get; set; }
        public int? EcuAddressInt { get; set; }


        //015F
        public byte? EmissionCode { get; set; }

        //PID'S SOPORTADOS

        public IReadOnlyList<int> Pids_01_20 { get; set; } = Array.Empty<int>();
        public IReadOnlyList<int> Pids_21_40 { get; set; } = Array.Empty<int>();
        public IReadOnlyList<int> Pids_41_60 { get; set; } = Array.Empty<int>();

    }

}
