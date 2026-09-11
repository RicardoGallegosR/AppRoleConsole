using SQLSIVEV.Infrastructure.Services;

namespace SQLSIVEV.Comun {

    public class Regedit {

        private const string RegistryBasePath = @"SOFTWARE";

        public string Origen { get; }

        private readonly RegistroWindows _reg;


        // Constructor normal
        public Regedit(string origen) {

            if (string.IsNullOrWhiteSpace(origen))
                throw new ArgumentException("El origen no puede estar vacío.", nameof(origen));

            Origen = origen;
            string registryPath = $@"{RegistryBasePath}\{origen}";
            _reg = new RegistroWindows(registryPath, origen);
        }


        // Constructor con configuración de cifrado
        public Regedit(string origen, CryptoHelper32 conf): this(origen) {
            if (conf is null)
                throw new ArgumentNullException(nameof(conf), "La configuración de cifrado no puede ser nula.");
            CryptoHelper.Configurar(conf, Origen);
        }


        public string Leer(string nombrePropiedad) {
            string cifrado = _reg.LeerValor( nombrePropiedad, string.Empty);
            if (string.IsNullOrWhiteSpace(cifrado))
                return string.Empty;
            return CryptoHelper.Desencriptar(cifrado);
        }


        public string LeerString(string nombrePropiedad)
            => Leer(nombrePropiedad);


        public Guid LeerGuid(string nombrePropiedad) {
            string plano = Leer(nombrePropiedad);
            return Guid.TryParse(plano, out var valor) ? valor : Guid.Empty;
        }


        public short LeerShort(string nombrePropiedad, short defecto = 0) {
            string plano = Leer(nombrePropiedad);

            return short.TryParse(plano, out var valor)
                ? valor
                : defecto;
        }


        public bool LeerBool(string nombrePropiedad, bool defecto = false) {
            string plano = Leer(nombrePropiedad);

            return bool.TryParse(plano, out var valor)
                ? valor
                : defecto;
        }
    }
}