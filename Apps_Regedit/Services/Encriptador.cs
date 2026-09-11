using Microsoft.Win32;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Apps_Regedit.Services {
    public sealed class Encriptador {

        private readonly string _password;
        private readonly string _saltText;
        private readonly int _iterations;
        private readonly short _keySizeBits;

        private readonly string _registryPath;

        public Encriptador(string password, string saltText, string registryPath, int iterations = 100_000, short keySizeBits = 256) {
            _password = password;
            _saltText = saltText;
            _iterations = iterations;
            _keySizeBits = keySizeBits;
            _registryPath = registryPath;
        }

        public string Encriptar(string textoPlano) {

            if (string.IsNullOrEmpty(textoPlano))
                return string.Empty;

            using var aes = Aes.Create();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] saltBytes =
            Encoding.UTF8.GetBytes(_saltText);

            using var pdb = new Rfc2898DeriveBytes(_password,  saltBytes, _iterations, HashAlgorithmName.SHA256);

            aes.Key = pdb.GetBytes(_keySizeBits / 8);

            aes.GenerateIV();

            using var ms = new MemoryStream();

            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var crypto = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(crypto,Encoding.UTF8)) {
                writer.Write(textoPlano);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public void EscribirValor<T>(string nombrePropiedad, T valorOriginal) {
            try {
                using var key = Registry.LocalMachine.CreateSubKey(_registryPath, writable: true);

                if (key is null)
                    throw new InvalidOperationException($"No se pudo abrir {_registryPath}");

                string valorTexto = valorOriginal switch {
                    //Guid g => g.ToString("D"), null => string.Empty,
                    _ => valorOriginal!.ToString() ?? string.Empty
                };

                string valorCifrado = Encriptar(valorTexto);
                key.SetValue(nombrePropiedad, valorCifrado, RegistryValueKind.String);
                SivevLogger.Information($"[REG] Propiedad escrita: {nombrePropiedad}", SivevOrigen.Configurador);

            } catch (Exception ex) {
                SivevLogger.Error($"Error al escribir '{nombrePropiedad}': {ex.Message}", SivevOrigen.Configurador);
                throw;
            }
        }
    }
}
