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



}