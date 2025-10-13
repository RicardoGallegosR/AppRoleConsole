namespace AppRoleConsole.Domain.Models {

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
        public int ReturnCode { get; init; }    // @iError (return value)
        public int MensajeId { get; init; }     // @iMensajeId OUTPUT
        public short Resultado { get; init; }   // @siResultado OUTPUT
    }



    /// <summary>
    /// Falla porque el Roll no tiene permisos de ejecucion 
    /// </summary>
    public sealed class SpBitacoraAplicacionesIniciaResult {
        public int ReturnCode { get; init; }            // valor de RETURN(@iError)
        public Guid BitacoraAplicacionId { get; init; } // @uiBitacoraAplicacionId OUTPUT
    }

    public sealed class SpAppProgramOnResult {
        public int ReturnCode { get; init; } // @@ERROR que retorna el SP
        public int MensajeId { get; init; } // @iMensajeId OUTPUT
        public short Resultado { get; init; } // @siResultado OUTPUT
    }


    public sealed class AccesoIniciaResult {
        public short ReturnCode { get; set; }          // @siResultado
        public int MensajeId { get; set; }          // @iMensajeId
        public Guid AccesoId { get; set; }          // @uiAccesoId
    }

    public sealed class VerificacionVisualIniResult {
        public int MensajeId { get; set; }                 // @iMensajeId
        public short Resultado { get; set; }                 // @siResultado  (-? fuera / 0 continuar / 1 no hay pruebas)
        public Guid VerificacionId { get; set; }            // @uiVerificacionId
        public byte ProtocoloVerificacionId { get; set; }   // @tiProtocoloVerificacionId
        public string PlacaId { get; set; } = "DESCONOCIDO";  // @vcPlacaId (varchar(11))
    }
}