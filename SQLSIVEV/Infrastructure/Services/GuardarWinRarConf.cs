using Microsoft.Win32;
using SQLSIVEV.Infrastructure.Utils;

namespace SQLSIVEV.Infrastructure.Services {
    public class GuardarWinRarConf {
        private const string WinRarConfRegistryPath = @"SOFTWARE\WinRARConf";
        private const string RutaCompleta           = @"HKLM\SOFTWARE\WinRARConf";

        private readonly CryptoHelper32 _conf;

        public GuardarWinRarConf() {
            _conf = new CryptoHelper32();
        }

        /// <summary>
        /// Guarda los parámetros actuales de _conf en el registro.
        /// (Esta operación requiere permisos de admin sobre HKLM).
        /// </summary>
        public void Leer() {
            try {
                using var key = Registry.LocalMachine.CreateSubKey(WinRarConfRegistryPath, writable: true);
                if (key is null) {
                    SivevLogger.Error($"No se pudo crear/abrir la clave {WinRarConfRegistryPath}");
                    return;
                }

                key.SetValue("Uicc_apps", _conf.Password, RegistryValueKind.String);
                key.SetValue("installDir", _conf.SaltText, RegistryValueKind.String);
                key.SetValue("Version", _conf.Iterations, RegistryValueKind.DWord);
                key.SetValue("DLL", _conf.KeySizeBits, RegistryValueKind.DWord);

                SivevLogger.Information("Llaves guardadas correctamente en ***OCULTO***.");
            } catch (Exception ex) {
                SivevLogger.Error($"Ocurrió un error al guardar en el registro:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Lee del registro y carga los valores en la instancia _conf.
        /// Devuelve true si se leyó algo válido, false si no existe la clave.
        /// </summary>
        public bool CargarEnCryptoHelper() {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(WinRarConfRegistryPath, writable: false);

                if (key is null) {
                    SivevLogger.Warning(
                        $"La clave {RutaCompleta} no existe. Se usarán los valores por defecto de CryptoHelper.");
                    return false;
                }

                var uicc    = key.GetValue("Uicc_apps")  as string;
                var install = key.GetValue("installDir") as string;
                var version = key.GetValue("Version");
                var dll     = key.GetValue("DLL");

                if (!string.IsNullOrWhiteSpace(uicc)) {
                    //_conf.Password = uicc;
                    _conf.Password = "DEFAULT_KEY";

                }

                if (!string.IsNullOrWhiteSpace(install)) {
                    //_conf.SaltText = install;
                    _conf.SaltText = "DEFAULT_SALT";
                }


                if (version is int v && v > 0)
                    _conf.Iterations = v;

                if (dll is int d && d > 0)
                    _conf.KeySizeBits = d;

                SivevLogger.Information("Parámetros de cifrado cargados desde ***OCULTO***.");
                SivevLogger.Information($"Password: {_conf.Password}, SaltText {_conf.SaltText}");
                return true;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al leer parámetros de cifrado desde {RutaCompleta}.");
                return false;
            }
        }

        /// <summary>
        /// Expone la configuración actual, por si la necesitas en otros componentes.
        /// </summary>
        public CryptoHelper32 GetConfig() => _conf;
    }
}
