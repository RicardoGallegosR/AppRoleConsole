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


    public sealed class ObdResultado {
        public string Mensaje { get; set; } = "";

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

        // NUEVOS VALORES 
        public int? DistMilKm { get; init; }
        public int? DistSinceClrKm { get; init; }
        public int? RunTimeMilMin { get; init; }
        public int? TimeSinceClr { get; init; }

        public string? Cvn { get; init; }
        public string[] Cal { get; init; } = Array.Empty<string>();

        //011F
        public int? TiempoTotalSegundosOperacionMotor { get; init;}
       
        //0104
        public int? WarmUpsDesdeBorrado { get; init; }

        //101C
        public string? NormativaObdVehiculo { get; init; }
        
        //0105
        public int? IatCCoolantTempC { get; init; }

        //0106
        public double? StftB1 { get; init; }

        //0107
        public double? LtftB1 { get; init; }

        //010F
        public int? IatC { get; init; }

        //0110 EN SEGUNDOS Y HORAS
        public double? MafGs { get; init; }
        public double? MafKgH { get; init; }

        //0111
        public double? Tps { get; init; }

        //010E
        public double? TimingAdvance { get; init; }

        
        //0114 y 0115
        public double? O2S1_V { get; init; }  // Banco 1, Sensor 1 (V)
        public double? O2S2_V { get; init; }  // Banco 1, Sensor 2 (V)

        //012F
        public double? FuelLevel { get; init; }

        //0133
        public int? BarometricPressure { get; init; }

        //0151
        public string? FuelType { get; init; }
        public int? IntFuelType { get; init; }

        //0907
        public int? IntTipoCombustible0907 { get; init; }

        //090A
        public string? EcuAddress { get; init; }
        public int? EcuAddressInt { get; init; }


        //015F
        public byte? EmissionCode { get; init; }

        //PID'S SOPORTADOS

        public IReadOnlyList<int> Pids_01_20 { get; init; } = Array.Empty<int>();
        public IReadOnlyList<int> Pids_21_40 { get; init; } = Array.Empty<int>();
        public IReadOnlyList<int> Pids_41_60 { get; init; } = Array.Empty<int>();

    }

}
