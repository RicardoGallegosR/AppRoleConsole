using SQLSIVEV.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Comun {
    public class Regedit {
        private const string RegistryBasePath = @"SOFTWARE";
        public string Origen { get; }
        private readonly RegistroWindows _reg;

        public Regedit(string origen) {

            if (string.IsNullOrWhiteSpace(origen))
                throw new ArgumentException("El origen no puede estar vacío.", nameof(origen));

            Origen = origen;

            string registryPath = $@"{RegistryBasePath}\{origen}";

            _reg = new RegistroWindows(registryPath, origen);
        }

        public string Leer(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;
            return CryptoHelper.Desencriptar(cifrado);
        }
        public Guid LeerGuid(string nombrePropiedad) {
            string plano = Leer(nombrePropiedad); // usa el método que desencripta
            if (string.IsNullOrWhiteSpace(plano))
                return Guid.Empty;

            return Guid.TryParse(plano, out var g) ? g : Guid.Empty;
        }
        public string LeerString(string nombrePropiedad) {
            string cifrado = _reg.LeerValor(nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;

            return CryptoHelper.Desencriptar(cifrado);
        }
        public short LeerShort(string nombrePropiedad, short defecto = 0) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return short.TryParse(plano, out var valor) ? valor : defecto;
        }

        public bool LeerBool(string nombrePropiedad, bool defecto = false) {
            string plano = LeerString(nombrePropiedad);
            if (string.IsNullOrWhiteSpace(plano))
                return defecto;

            return bool.TryParse(plano, out var valor) ? valor : defecto;
        }
    }
}
