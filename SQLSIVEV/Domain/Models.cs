namespace SQLSIVEV.Domain.Models {

    public sealed class SpAppRollClaveGetResult {
        public int ReturnCode { get; init; }
        public int MensajeId { get; init; }
        public short Resultado { get; init; }
        public string FuncionAplicacion { get; init; } = "";
        public string ClaveAcceso { get; init; } = "";
    }


    public sealed class CredencialExisteHuellaResult {
        public int MensajeId { get; init; }
        public short Resultado { get; init; }
        public bool ExisteHuella { get; init; }
        public byte[] Huella { get; init; } = Array.Empty<byte>();
    }


    public sealed class SpAppChecaCpuResult {
        public int ReturnCode { get; init; }
        public int MensajeId { get; init; }
        public short Resultado { get; init; }
    }

    public sealed class SpBitacoraAplicacionesIniciaResult {
        public int ReturnCode { get; init; }
        public Guid BitacoraAplicacionId { get; init; }
    }

    public sealed class SpAppAccesoFin {
        public int iMensajeId { get; set; }
        public short siResultado { get; set; }
        public Guid uiEstacionId { get; set; }
        public Guid uiAccesoId { get; set; }
    }

    public sealed class SpAppProgramOnResult {
        public int ReturnCode { get; init; }
        public int MensajeId { get; init; }
        public short Resultado { get; init; }
    }


    public sealed class AccesoIniciaResult {
        public short ReturnCode { get; set; }
        public int MensajeId { get; set; }
        public Guid AccesoId { get; set; }
    }

    public sealed class VerificacionVisualIniResult {
        public int MensajeId { get; set; }
        public short Resultado { get; set; }
        public Guid VerificacionId { get; set; }
        public byte ProtocoloVerificacionId { get; set; }
        public string PlacaId { get; set; } = "DESCONOCIDO";
    }


    public sealed class AccesoFinResult {
        public short Resultado { get; set; }
        public int MensajeId { get; set; }
        public int ReturnCode { get; set; }
        public bool Ok => Resultado >= 0 && MensajeId == 0 && ReturnCode == 0;
    }

    public sealed class VerificacionPruebaObdFinaliza {
        public Guid VerificacionId { get; set; }
        public short ResultadoId { get; set; }
        public short CausaRechazoId { get; set; }
    }


    public sealed class SpAppBitacoraErroresSet {
        private const string ValorDesconocido = "DESCONOCIDO";
        
        // SALIDAS
        public int      MensajeId       { get; set; }
        public short    Resultado       { get; set; }

        // ENTRADAS
        public Guid     EstacionId      { get; set; } = Guid.Empty;
        public short    Centro          { get; set; }
        public string   NombreCpu       { get; set; } = ValorDesconocido;
        public short    OpcionMenuId    { get; set; } = 0;
        public DateTime FechaError      { get; set; } = new DateTime(1900, 1, 1);
        public string   Libreria        { get; set; } = ValorDesconocido;
        public string   Clase           { get; set; } = ValorDesconocido;
        public string   Metodo          { get; set; } = ValorDesconocido;
        public int      CodigoErrorSql  { get; set; } = 0;
        public int      CodigoError     { get; set; } = 0;
		public string   DescripcionError{ get; set; } = ValorDesconocido;
		public int      LineaCodigo     { get; set; } = 0;
        public int      LastDllError    { get; set; } = 0;
        public string   SourceError     { get; set; } = ValorDesconocido;
    }


    public sealed class CapturaVisualItem {
        public short CapturaVisualId { get; set; }
        public string Elemento { get; set; } = "";
        public bool Despliegue { get; set; } = false;
    }

    public sealed class CapturaVisualGetResult {
        public short Resultado { get; set; }
        public int MensajeId { get; set; }
        public int ReturnCode { get; set; }
        public List<CapturaVisualItem> Items { get; } = new();
        public bool Ok => Resultado >= 0 && MensajeId == 0 && ReturnCode == 0;
    }

    public sealed class AppTextoMensajeResult {
        public short Resultado { get; init; }
        public string Mensaje { get; init; } = "DESCONOCIDO";
        public int MensajeId { get; init; }
        public int ReturnCode { get; init; }
    }


    public sealed class CapturaInspeccionVisualNewSetResult {
        public int MensajeId { get; set; }
        public short Resultado { get; set; }
        public bool CheckObd { get; set; }
        public int ReturnCode { get; set; }
        public bool Ok => Resultado >= 0 && ReturnCode == 0 && MensajeId == 0;
    }


    public sealed class CapturaInspeccionObdSetResult {
        public int MensajeId { get; set; }        
        public short Resultado { get; set; }      
        public int ReturnCode { get; set; }       
        public bool Ok => Resultado >= 0 && ReturnCode == 0 && MensajeId == 0;
    }
    public sealed class ResultadoSql {
        public short Resultado { get; set; }
        public int MensajeId { get; set; }
    }

    public sealed class InspeccionObd2Set {
        public Guid EstacionId { get; set; } = Guid.Empty;
        public Guid AccesoId { get; set; } = Guid.Empty;
        public Guid VerificacionId { get; set; } = Guid.Empty;
        public string? VehiculoId { get; set; } = "DESCONOCIDO";
        public decimal? CCM { get; set; } = 0;
        public decimal? WarmUpsDesdeBorrado { get; set; } = 0;
        public int? NEV { get; set; } = 0;
        public string? NEV_string { get; set; } = "DESCONOCIDO";
        public short? TR { get; set; } = 0;
        public byte? ConexionObd { get; set; } = 0;
        public string? ProtocoloObd { get; set; } = "DESCONOCIDO";
        public int Intentos { get; set; } = 0;
        public byte? Mil { get; set; } = 0;
        public byte? Fallas { get; set; } = 0;
        public string? CodigoError { get; set; } = "DESCONOCIDO";
        public string? CodigoErrorPendiente { get; set; } = "DESCONOCIDO";
        public string? CodigoErrorPermanente { get; set; } = "DESCONOCIDO";
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
        public byte? Spsa { get; set; } 
        public byte? Sge { get; set; } 
        public byte? Schnm { get; set; }
        public byte? Sfp { get; set; } 
        public byte? Sscrron { get; set; } 
        public decimal? STFT_B1 { get; set; } = 0;
        public decimal? LTFT_B1 { get; set; } = 0;
        public short? IAT { get; set; } = 0;
        public decimal? MAF { get; set; } = 0;
        public decimal? MafKgH { get; set; } = 0;
        public decimal? TPS { get; set; } = 0;
        public decimal? VoltsSwOff { get; set; } = 0;
        public decimal? VoltsSwOn { get; set; } = 0;
        public short? RpmOff { get; set; } = 0;
        public short? RpmOn { get; set; } = 0;
        public short? RpmCheck { get; set; } = 0;
        public bool? LeeMonitores { get; set; } = false;
        public bool? LeeDtc { get; set; } = false;
        public bool? LeeDtcPend { get; set; } = false;
        public bool? LeeDtcPerm { get; set; } = false;
        public bool? LeeVin { get; set; } = false;
        public short? CodigoProtocolo { get; set; }
        public short? VelVeh { get; set; } = 0;
        public decimal? AvanceEnc { get; set; } = 0;
        public decimal? Volt_O2 { get; set; } = 0;
        public decimal? Volt_O2_S2 { get; set; } = 0;
        public int? TiemppoDeArranque { get; set; } = 0;
        public decimal? NivelComb { get; set; } = 0;
        public short? Pres_Baro { get; set; } = 0;
        public string? FuelType { get; set; } = "DESCONOCIDO";
        public byte? Combustible0151Id { get; set; } = 0;
        public int? Dist_MIL_On { get; set; } = 0;
        public int? Dist_Borrado_DTC { get; set; } = 0;
        public int? Tpo_MIL_On { get; set; } = 0;
        public int? Tpo_Borrado_DTC { get; set; } = 0;
        public byte? Combustible0907Id { get; set; } = 0;
        public string? Dir_ECU { get; set; } = "DESCONOCIDO";
        public int? EcuAddressInt { get; set; } = 0;
        public int? ID_Calib { get; set; } = 0;
        public string? IDs_Adic { get; set; } = "DESCONOCIDO";
        public string? NumVerifCalib { get; set; } = "DESCONOCIDO";
        public string? Lista_CVN { get; set; } = "DESCONOCIDO";
        public string? Est_Mon_DTC_Borrado { get; set; } = "DESCONOCIDO";
        public int? MotorTipoId { get; set; } = 0;
        public byte? Req_Emisiones { get; set; } = 0;
        public string? PIDS_Sup_01_20 { get; set; } = "DESCONOCIDO";
        public string? PIDS_Sup_21_40 { get; set; } = "DESCONOCIDO";
        public string? PIDS_Sup_41_60 { get; set; } = "DESCONOCIDO";

        public uint? Odometro { get; set; } = 0;
        public int? TiempoMotorEnMarchaSeg { get; set; } = 0;
        public int? ReadCvnMessageCount { get; set; } = 0;
        public string ReadCvnsRobusto { get; set; } = "DESCONOCIDO";
        public string TramaPid0101 { get; set; } = "DESCONOCIDO";

        public string? Mensaje { get; set; } = "DESCONOCIDO";
        public int? MensajeId { get; set; } = 0;
    }


}