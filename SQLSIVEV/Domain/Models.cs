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