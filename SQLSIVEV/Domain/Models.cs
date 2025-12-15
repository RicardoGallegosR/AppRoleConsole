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
    public sealed class InspeccionObdGet {
        public int MensajeId { get; set; }
        public short Resultado { get; set; }
    }

    public sealed class InspeccionObd2Set {
        public Guid EstacionId { get; set; }
        public Guid AccesoId { get; set; }
        public Guid VerificacionId { get; set; }
        public string? VehiculoId { get; set; }
        public decimal? CCM { get; set; }
        public byte? NEV { get; set; }
        public short? TR { get; set; }
        public byte? ConexionObd { get; set; }
        public string? ProtocoloObd { get; set; }
        public byte? Intentos { get; set; }
        public byte? Mil { get; set; }
        public byte? Fallas { get; set; }
        public string? CodigoError { get; set; }
        public string? CodigoErrorPendiente { get; set; }
        public string? CodigoErrorPermanente { get; set; }
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
        public decimal? STFT_B1 { get; set; }
        public decimal? LTFT_B1 { get; set; }
        public short? IAT { get; set; }
        public decimal? MAF { get; set; }
        public decimal? TPS { get; set; }
        public decimal? VoltsSwOff { get; set; }
        public decimal? VoltsSwOn { get; set; }
        public short? RpmOff { get; set; }
        public short? RpmOn { get; set; }
        public short? RpmCheck { get; set; }
        public bool? LeeMonitores { get; set; }
        public bool? LeeDtc { get; set; }
        public bool? LeeDtcPend { get; set; }
        public bool? LeeDtcPerm { get; set; }
        public bool? LeeVin { get; set; }
        public short? CodigoProtocolo { get; set; }
        public short? VelVeh { get; set; }
        public decimal? AvanceEnc { get; set; }
        public decimal? Volt_O2 { get; set; }
        public int? Tpo_Arranque { get; set; }
        public decimal? NivelComb { get; set; }
        public short? Pres_Baro { get; set; }
        public byte? Combustible0151Id { get; set; }
        public int? Dist_MIL_On { get; set; }
        public int? Dist_Borrado_DTC { get; set; }
        public int? Tpo_MIL_On { get; set; }
        public int? Tpo_Borrado_DTC { get; set; }
        public byte? Combustible0907Id { get; set; }
        public string? Dir_ECU { get; set; }
        public string? ID_Calib { get; set; }
        public string? IDs_Adic { get; set; }
        public string? NumVerifCalib { get; set; }
        public string? Lista_CVN { get; set; }
        public string? Est_Mon_DTC_Borrado { get; set; }
        public int? MotorTipoId { get; set; }
        public byte? Req_Emisiones { get; set; }
        public string? PIDS_Sup_01_20 { get; set; }
        public string? PIDS_Sup_21_40 { get; set; }
        public string? PIDS_Sup_41_60 { get; set; }

        public string? Mensaje { get; set; }
        public int? MensajeId { get; set; }
    }


}