using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Devices.UDS {
    public sealed class UdsResponse {
        public bool Success { get; private set; }
        public bool IsNegativeResponse { get; private set; }

        public string RawResponse { get; private set; } = string.Empty;
        public string? ErrorMessage { get; private set; }

        public byte? PositiveService { get; private set; }
        public byte? NegativeOriginalService { get; private set; }
        public byte? NegativeResponseCode { get; private set; }

        public List<byte> DataBytes { get; private set; } = new();

        public static UdsResponse Positive(string raw, byte positiveService, List<byte> data) {
            return new UdsResponse {
                Success = true,
                IsNegativeResponse = false,
                RawResponse = raw,
                PositiveService = positiveService,
                DataBytes = data ?? new List<byte>()
            };
        }

        public static UdsResponse Negative(string raw, byte originalService, byte nrc, string description) {
            return new UdsResponse {
                Success = false,
                IsNegativeResponse = true,
                RawResponse = raw,
                NegativeOriginalService = originalService,
                NegativeResponseCode = nrc,
                ErrorMessage = description
            };
        }

        public static UdsResponse Fail(string raw, string error) {
            return new UdsResponse {
                Success = false,
                IsNegativeResponse = false,
                RawResponse = raw,
                ErrorMessage = error
            };
        }

        public override string ToString() {
            if (Success) {
                string data = string.Join(" ", DataBytes.Select(b => b.ToString("X2")));
                return $"OK | Servicio positivo: {PositiveService:X2} | Data: {data}";
            }

            if (IsNegativeResponse) {
                return $"NEGATIVA | Servicio: {NegativeOriginalService:X2} | NRC: {NegativeResponseCode:X2} | {ErrorMessage}";
            }

            return $"ERROR | {ErrorMessage}";
        }
    }
}
