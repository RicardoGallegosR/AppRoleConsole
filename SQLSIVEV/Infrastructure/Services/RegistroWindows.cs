using Microsoft.Win32;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Services {
    public sealed class RegistroWindows {
        private readonly string _registryPath;
        private readonly string _origen;


        public RegistroWindows(string registryPath, string origen) {
            if (string.IsNullOrWhiteSpace(registryPath))
                throw new ArgumentException("La ruta del registro no puede estar vacía.", nameof(registryPath));

            _registryPath = registryPath;
            _origen = origen;
        }

        public T LeerValor<T>(string nombrePropiedad, T valorPorDefecto = default!) {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(_registryPath, writable: false);
                if (key is null) {
                    SivevLogger.Error($"La clave '{_registryPath}' no existe.", origen: _origen);
                    return valorPorDefecto;
                }
                object? raw = key.GetValue(nombrePropiedad);
                if (raw is null) {
                    SivevLogger.Warning($"El valor '{nombrePropiedad}' no existe.", origen: _origen);
                    return valorPorDefecto;
                }
                string texto = raw.ToString() ?? string.Empty;
                
                if (typeof(T) == typeof(string))
                    return (T)(object)texto;

                if (typeof(T) == typeof(Guid)) {
                    if (Guid.TryParse(texto, out Guid guid))
                        return (T)(object)guid;
                    
                    return valorPorDefecto;
                }

                return (T)Convert.ChangeType(texto, typeof(T), CultureInfo.InvariantCulture);
            } catch (Exception ex) {
                SivevLogger.Error($"Error al leer '{nombrePropiedad}': {ex.Message}", origen: _origen);
                return valorPorDefecto;
            }
        }
    }
}
