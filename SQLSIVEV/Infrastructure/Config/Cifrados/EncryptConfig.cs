using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Config.Cifrados {
    public sealed class EncryptConfig {
        public string SaltValue { get; init; } = "s@1tValue";
        public string HashAlgorithm { get; init; } = "SHA1";
        public int PasswordIterations { get; init; } = 2;
        public string InitVector { get; init; } = "@1B2c3D4e5F6g7H8";
        public int KeySize { get; init; } = 256;
    }
}
