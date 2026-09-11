using Microsoft.Win32;
using SQLSIVEV.Infrastructure.Utils;

namespace SQLSIVEV.Infrastructure.Services {

    public sealed class GuardarWinRarConf {
        private readonly CryptoHelper32 _conf;

        public GuardarWinRarConf() {
            _conf = new CryptoHelper32();
        }

        public GuardarWinRarConf(CryptoHelper32 conf) {
            _conf = conf ?? throw new ArgumentNullException(nameof(conf));
        }

        /// <summary>
        /// Guarda los parámetros criptográficos en Registry.
        /// Requiere permisos de escritura sobre HKLM.
        /// </summary>
        public bool Guardar() {
            try {
                using var key = Registry.LocalMachine.CreateSubKey(_conf.WinRarConfRegistryPath, writable: true);
                
                if (key is null) {
                    SivevLogger.Error("No se pudo crear/abrir la configuración criptográfica.", SivevOrigen.Configurador);
                    return false;
                }
                key.SetValue("Uicc_apps", _conf.Password, RegistryValueKind.String);
                key.SetValue("installDir",_conf.SaltText, RegistryValueKind.String);
                key.SetValue("Version", _conf.Iterations, RegistryValueKind.DWord);
                key.SetValue("DLL",    _conf.KeySizeBits, RegistryValueKind.DWord);
                SivevLogger.Information("Parámetros criptográficos guardados correctamente.", SivevOrigen.Configurador);
                return true;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al guardar configuración criptográfica: {ex.Message}",SivevOrigen.Configurador);
                return false;
            }
        }

        /// <summary>
        /// Carga los parámetros criptográficos desde Registry.
        /// </summary>
        public bool Cargar() {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(_conf.WinRarConfRegistryPath, writable: false);
                if (key is null) {
                    SivevLogger.Warning("No existe la configuración criptográfica.\nSe conservarán los valores por defecto.",SivevOrigen.Configurador);
                    return false;
                }

                string? password = key.GetValue("Uicc_apps") as string;
                string? saltText = key.GetValue("installDir") as string;
                object? iterations = key.GetValue("Version");
                object? keySize =  key.GetValue("DLL");

                if (!string.IsNullOrWhiteSpace(password))
                    _conf.Password = password;

                if (!string.IsNullOrWhiteSpace(saltText))
                    _conf.SaltText = saltText;

                if (iterations is int i && i > 0)
                    _conf.Iterations = i;

                if (keySize is int k && k > 0)
                    _conf.KeySizeBits = k;

                SivevLogger.Information("Parámetros criptográficos cargados correctamente.", SivevOrigen.Configurador);
                return true;
            } catch (Exception ex) {
                SivevLogger.Error($"Error al cargar configuración criptográfica: {ex.Message}", SivevOrigen.Configurador);
                return false;
            }
        }

        public CryptoHelper32 GetConfig() => _conf;
    }
}