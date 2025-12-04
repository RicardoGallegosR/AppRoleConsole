using Microsoft.Win32;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLSIVEV.Infrastructure.Utils;

namespace SQLSIVEV.Infrastructure.Services {
    public sealed class RegistroCrypto {
        private const string RegistryPath = @"SOFTWARE\VISUAL";
        
        
        public void EscribirValor<T>(string nombrePropiedad, T valorOriginal) {
            try {
                using var key = Registry.LocalMachine.CreateSubKey(RegistryPath, writable: true);

                if (key is null) {
                    SivevLogger.Error($"No se pudo crear/abrir la clave de registro {RegistryPath} al escribir {nombrePropiedad}");
                    return;
                }
                string valorTexto = valorOriginal switch {
                    Guid g        => g.ToString("D"),
                    null          => string.Empty,
                    _             => valorOriginal!.ToString() ?? string.Empty
                };
                key.SetValue(nombrePropiedad, valorTexto, RegistryValueKind.String);
                SivevLogger.Information($"[REG] Escrito: {nombrePropiedad} = {valorTexto}");
            } catch (Exception ex) {
                SivevLogger.Error($"Error al escribir '{nombrePropiedad}' en el registro: {ex.Message}");
            }
        }

        public T LeerValor<T>(string nombrePropiedad, T valorPorDefecto = default!) {
            try {
                using (var key = Registry.LocalMachine.OpenSubKey(RegistryPath, writable: false)) {
                    if (key is null) {
                        SivevLogger.Error($"La clave de registro '{RegistryPath}' no existe.");
                        return valorPorDefecto;
                    }

                    var raw = key.GetValue(nombrePropiedad);
                    if (raw is null) {
                        SivevLogger.Error($"El valor '{nombrePropiedad}' no existe en el registro.");
                        return valorPorDefecto;
                    }

                    string texto = raw.ToString() ?? string.Empty;

                    if (typeof(T) == typeof(string))
                        return (T)(object)texto;

                    if (typeof(T) == typeof(Guid)) {
                        if (Guid.TryParse(texto, out var g))
                            return (T)(object)g;

                        SivevLogger.Error($"No se pudo convertir '{texto}' a Guid para '{nombrePropiedad}'.");
                        return valorPorDefecto;
                    }

                    var convertido = (T)Convert.ChangeType(texto, typeof(T), CultureInfo.InvariantCulture);
                    return convertido;
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error al leer '{nombrePropiedad}' del registro: {ex.Message}");
                return valorPorDefecto;
            }
        }
    }
}
