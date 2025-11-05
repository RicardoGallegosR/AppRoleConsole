using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Config.Cifrados {
    public static class Cifrados {
        // Cifrado por defecto (lo que hoy tienes)
        public static EncryptConfig Default => new() {
            SaltValue = "s@1tValue",
            HashAlgorithm = "SHA1",
            PasswordIterations = 2,
            InitVector = "@1B2c3D4e5F6g7H8",
            KeySize = 256
        };



        // Para estación VISUAL
        public static EncryptConfig Visual => new() {
            SaltValue = "VISUAL_s@1t",
            HashAlgorithm = "SHA256",
            PasswordIterations = 3,
            InitVector = "V1suAlB2c3D4e5F6",
            KeySize = 256
        };



        // Para estación DE EMISIONES
        public static EncryptConfig Emisiones => new() {
            SaltValue = "EMI_s@1t",
            HashAlgorithm = "SHA256",
            PasswordIterations = 5,
            InitVector = "Em1sI0nesB2c3D4e5",
            KeySize = 256
        };


        // Para admin / escritorio / pruebas
        public static EncryptConfig Admin => new() {
            SaltValue = "ADM_s@1t",
            HashAlgorithm = "SHA1",
            PasswordIterations = 2,
            InitVector = "@1B2c3D4e5F6g7H8",
            KeySize = 256
        };
    }
}
