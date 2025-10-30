namespace SQLSIVEV.Domain.OBD {
    public class ObdSnapshot {
        public string Protocolo { get; init; } = "desconocido";
        public string CalJoined { get; init; } = "desconocido";
        public int? Rpm { get; init; }
        public int? VelocidadKmh { get; init; }
        public string Vin { get; init; } = "desconocido";
        public int? DistMilKm { get; init; }
        public int? DistSinceClrKm { get; init; }
        public int? RunTimeMilMin { get; init; }
        public int? TimeSinceClrMin { get; init; }
        public double? Volts { get; init; }
        public bool? MilOn { get; init; }
        public int DtcCount { get; init; }
        public string? DtcList03 { get; init; }
        public string? DtcList07 { get; init; }
        public string? DtcList0A { get; init; }

        public int? RpmOff { get; set; }
        public int? RpmOn { get; set; }
        public double? VoltsOff { get; set; }
        public double? VoltsOn { get; set; }

        // Monitores (derecha)
        public Dictionary<string, (bool avail, bool comp)> Monitors { get; init; }
            = new(StringComparer.OrdinalIgnoreCase);
    }
}
