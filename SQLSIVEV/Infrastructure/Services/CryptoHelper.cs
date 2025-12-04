using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Services {
    public class CryptoHelper {
        public static string _Password { get; set; } = "DEFAULT_KEY";
        public static string _SaltText { get; set; } = "DEFAULT_SALT";
        public static int _Iterations { get; set; } = 100_000;
        public static int _KeySizeBits { get; set; } = 256;
        
        public static void Configurar(CryptoHelper32 conf) {
            _Password = conf.Password;
            _SaltText = conf.SaltText;
            _Iterations = conf.Iterations;
            _KeySizeBits = conf.KeySizeBits;
        }


        public static string Desencriptar(string textoCifrado) {
            if (string.IsNullOrEmpty(textoCifrado))
                return string.Empty;

            var allBytes = Convert.FromBase64String(textoCifrado);

            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] saltBytes = Encoding.UTF8.GetBytes(_SaltText);

            using var pdb = new Rfc2898DeriveBytes(
                _Password,
                saltBytes,
                _Iterations,
                HashAlgorithmName.SHA256
            );

            aes.Key = pdb.GetBytes(_KeySizeBits / 8);

            int ivLength = aes.BlockSize / 8;
            var iv = new byte[ivLength];
            Buffer.BlockCopy(allBytes, 0, iv, 0, ivLength);

            aes.IV = iv;

            using var ms = new MemoryStream(allBytes, ivLength, allBytes.Length - ivLength);
            using var crypto = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(crypto, Encoding.UTF8);

            return reader.ReadToEnd();
        }
    }
}
