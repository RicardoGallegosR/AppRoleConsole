using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Services {
    public sealed class CryptoHelper32 {
        public string Password { get; set; } = "DEFAULT_KEY";
        public string SaltText { get; set; } = "DEFAULT_SALT";
        public int Iterations { get; set; } = 100_000;
        public int KeySizeBits { get; set; } = 256;
    }

    public sealed class VisualRegistroWindows {
        public string Server { get; init; } = "";
        public string Database { get; init; } = "";
        public string User { get; init; } = "";
        public string Password { get; init; } = "";
        public string AppName { get; init; } = "";
        public string AppRole { get; init; } = "";
        public Guid AppRolePassword { get; init; } = Guid.Empty;
        public short OpcionMenuId { get; init; }
        public bool Relleno { get; init; } = false;
        public string UsuarioLinea { get; init; } = string.Empty;
        public string Ip { get; init; } = string.Empty;
        public short Centro { get; init; }
        public string ServidorVersionesControlador { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public Guid EstacionId { get; init; } = Guid.Empty;
        public Guid RollVisualAcceso { get; set; } = Guid.Empty;
        public string RollVisual { get; set; } = string.Empty;
        
        
        public string PlacaId { get; set; } = string.Empty;
        public Guid AccesoId { get; set; } = Guid.Empty;
        public Guid VerificacionId { get; set; } = Guid.Empty;

    }
}
