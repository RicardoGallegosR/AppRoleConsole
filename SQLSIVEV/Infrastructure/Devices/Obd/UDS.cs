using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    #region Referencia de bits en F401
    public sealed class UdsMonitorStatusF401 {
        public byte RawA { get; set; }
        public byte RawB { get; set; }
        public byte RawC { get; set; }
        public byte? RawD { get; set; }

        public bool MilOn { get; set; }
        public int ConfirmedDtcCount { get; set; }

        public bool CompressionIgnition { get; set; }

        public bool MisfireSupported { get; set; }
        public bool FuelSystemSupported { get; set; }
        public bool ComprehensiveComponentSupported { get; set; }

        public bool MisfireReady { get; set; }
        public bool FuelSystemReady { get; set; }
        public bool ComprehensiveComponentReady { get; set; }

        public bool CatalystSupported { get; set; }
        public bool HeatedCatalystSupported { get; set; }
        public bool EvaporativeSystemSupported { get; set; }
        public bool SecondaryAirSystemSupported { get; set; }
        public bool GasolineParticulateFilterSupported { get; set; }
        public bool OxygenSensorSupported { get; set; }
        public bool OxygenSensorHeaterSupported { get; set; }
        public bool EgrOrVvtSupported { get; set; }

        public bool NmhcCatalystSupported { get; set; }
        public bool NoxAftertreatmentSupported { get; set; }
        public bool BoostPressureSystemSupported { get; set; }
        public bool ExhaustGasSensorSupported { get; set; }
        public bool PmFilterSupported { get; set; }

        public override string ToString() {
            string tipoMotor = CompressionIgnition ? "Compresión / diésel" : "Chispa / gasolina";

            return
                $"MIL: {(MilOn ? "ENCENDIDA" : "APAGADA")}\n" +
                $"DTCs confirmados: {ConfirmedDtcCount}\n" +
                $"Tipo monitoreo: {tipoMotor}\n" +
                $"Misfire soportado/listo: {Bool(MisfireSupported)} / {Bool(MisfireReady)}\n" +
                $"Fuel system soportado/listo: {Bool(FuelSystemSupported)} / {Bool(FuelSystemReady)}\n" +
                $"Comprehensive component soportado/listo: {Bool(ComprehensiveComponentSupported)} / {Bool(ComprehensiveComponentReady)}\n" +
                $"Catalyst supported: {Bool(CatalystSupported)}\n" +
                $"Heated catalyst supported: {Bool(HeatedCatalystSupported)}\n" +
                $"EVAP supported: {Bool(EvaporativeSystemSupported)}\n" +
                $"Secondary air supported: {Bool(SecondaryAirSystemSupported)}\n" +
                $"GPF supported: {Bool(GasolineParticulateFilterSupported)}\n" +
                $"Oxygen sensor supported: {Bool(OxygenSensorSupported)}\n" +
                $"Oxygen sensor heater supported: {Bool(OxygenSensorHeaterSupported)}\n" +
                $"EGR/VVT supported: {Bool(EgrOrVvtSupported)}\n" +
                $"NOx aftertreatment supported: {Bool(NoxAftertreatmentSupported)}\n" +
                $"PM filter supported: {Bool(PmFilterSupported)}";
        }

        private static string Bool(bool value) {
            return value ? "Sí" : "No";
        }
    }
    #endregion

    public sealed class UDS {
        private readonly Func<string, string> _send;
        private readonly Action<string>? _logger;

        public string RequestHeader { get; private set; } = "7E0";
        public string ResponseHeader { get; private set; } = "7E8";

        public UDS(Func<string, string> sendCommand, Action<string>? logger = null) {
            _send = sendCommand ?? throw new ArgumentNullException(nameof(sendCommand));
            _logger = logger;
        }

        public void ConfigureCan11Bit(string requestHeader = "7E0", string responseHeader = "7E8") {
            RequestHeader = requestHeader;
            ResponseHeader = responseHeader;

            SendAt("AT H1");          // Mostrar headers
            SendAt("AT CAF1");        // Auto formatting CAN
            SendAt("AT CFC1");        // Flow control ON
            SendAt($"AT SH {RequestHeader}");
            SendAt($"AT CRA {ResponseHeader}");
        }

        public UdsResponse DiagnosticSessionControl(byte sessionType) {
            return SendService(0x10, sessionType);
        }

        public UdsResponse TesterPresent() {
            return SendService(0x3E, 0x00);
        }

        public UdsResponse ReadDataByIdentifier(ushort did) {
            byte high = (byte)(did >> 8);
            byte low = (byte)(did & 0xFF);

            return SendService(0x22, high, low);
        }

        public string? ReadAsciiDid(ushort did) {
            var response = ReadDataByIdentifier(did);

            if (!response.Success)
                return null;

            return BytesToAscii(response.DataBytes);
        }

        public string? ReadVin() {
            // DID estándar VIN en UDS
            return ReadAsciiDid(0xF190);
        }

        public string? ReadEcuName() {
            // DID común para nombre de ECU, depende del fabricante
            return ReadAsciiDid(0xF18A);
        }

        public string? ReadCalibrationId() {
            // DID común para Calibration ID, depende del fabricante
            return ReadAsciiDid(0xF187);
        }

        public UdsResponse ReadDtcInformation(byte subFunction) {
            // Servicio 19: ReadDTCInformation
            return SendService(0x19, subFunction);
        }

        public UdsResponse SendService(byte service, params byte[] payload) {
            var requestBytes = new List<byte> { service };
            requestBytes.AddRange(payload);

            string command = string.Join(" ", requestBytes.Select(b => b.ToString("X2")));

            _logger?.Invoke($"UDS TX [{RequestHeader}] {command}");

            string raw = _send(command);

            _logger?.Invoke($"UDS RX [{ResponseHeader}] {raw}");

            return ParseResponse(service, raw);
        }

        private string SendAt(string command) {
            _logger?.Invoke($"UDS AT TX {command}");

            string raw = _send(command);

            _logger?.Invoke($"UDS AT RX {raw}");

            return raw;
        }

        private UdsResponse ParseResponse(byte requestService, string raw) {
            if (string.IsNullOrWhiteSpace(raw)) {
                return UdsResponse.Fail(raw, "Respuesta vacía.");
            }

            string cleaned = CleanRaw(raw);

            if (cleaned.Contains("NO DATA", StringComparison.OrdinalIgnoreCase))
                return UdsResponse.Fail(raw, "NO DATA.");

            if (cleaned.Contains("STOPPED", StringComparison.OrdinalIgnoreCase))
                return UdsResponse.Fail(raw, "Lectura detenida por el ELM.");

            if (cleaned.Contains("?"))
                return UdsResponse.Fail(raw, "Comando no reconocido por el ELM.");

            var bytes = ExtractHexBytes(cleaned);

            if (bytes.Count == 0)
                return UdsResponse.Fail(raw, "No se encontraron bytes hexadecimales en la respuesta.");

            int negativeIndex = bytes.FindIndex(b => b == 0x7F);

            if (negativeIndex >= 0 && negativeIndex + 2 < bytes.Count) {
                byte originalService = bytes[negativeIndex + 1];
                byte nrc = bytes[negativeIndex + 2];

                return UdsResponse.Negative(
                    raw,
                    originalService,
                    nrc,
                    GetNegativeResponseDescription(nrc)
                );
            }

            byte expectedPositiveService = (byte)(requestService + 0x40);

            int positiveIndex = bytes.FindIndex(b => b == expectedPositiveService);

            if (positiveIndex < 0) {
                return UdsResponse.Fail(
                    raw,
                    $"No se encontró respuesta positiva esperada: {expectedPositiveService:X2}."
                );
            }

            var data = bytes.Skip(positiveIndex + 1).ToList();

            return UdsResponse.Positive(raw, expectedPositiveService, data);
        }
        public UdsMonitorStatusF401? ReadMonitorStatusF401() {
            var response = ReadDataByIdentifier(0xF401);

            if (!response.Success) {
                _logger?.Invoke($"UDS F401 ERROR: {response.ErrorMessage}");
                return null;
            }

            return DecodeMonitorStatusF401(response.DataBytes);
        }

        public static UdsMonitorStatusF401 DecodeMonitorStatusF401(List<byte> dataBytes) {
            if (dataBytes == null || dataBytes.Count < 5)
                throw new ArgumentException("La respuesta F401 debe contener al menos: F4 01 A B C.");

            int offset = 0;

            // Si viene con eco del DID: F4 01 A B C D
            if (dataBytes.Count >= 5 && dataBytes[0] == 0xF4 && dataBytes[1] == 0x01)
                offset = 2;

            if (dataBytes.Count < offset + 3)
                throw new ArgumentException("La respuesta F401 no contiene suficientes bytes A, B y C.");

            byte A = dataBytes[offset];
            byte B = dataBytes[offset + 1];
            byte C = dataBytes[offset + 2];

            byte? D = null;
            if (dataBytes.Count >= offset + 4)
                D = dataBytes[offset + 3];

            bool compressionIgnition = GetBit(B, 3);

            var result = new UdsMonitorStatusF401
    {
                RawA = A,
                RawB = B,
                RawC = C,
                RawD = D,

                MilOn = GetBit(A, 7),

                // En OBD Mode 01 PID 01, bits 0-6 de A suelen indicar número de DTCs confirmados.
                ConfirmedDtcCount = A & 0x7F,

                CompressionIgnition = compressionIgnition,

                MisfireSupported = GetBit(B, 0),
                FuelSystemSupported = GetBit(B, 1),
                ComprehensiveComponentSupported = GetBit(B, 2),

                MisfireReady = GetBit(B, 4),
                FuelSystemReady = GetBit(B, 5),
                ComprehensiveComponentReady = GetBit(B, 6),

                CatalystSupported = GetBit(C, 0),
                HeatedCatalystSupported = GetBit(C, 1),
                EvaporativeSystemSupported = !compressionIgnition && GetBit(C, 2),
                SecondaryAirSystemSupported = !compressionIgnition && GetBit(C, 3),
                GasolineParticulateFilterSupported = !compressionIgnition && GetBit(C, 4),
                OxygenSensorSupported = !compressionIgnition && GetBit(C, 5),
                OxygenSensorHeaterSupported = !compressionIgnition && GetBit(C, 6),
                EgrOrVvtSupported = GetBit(C, 7),

                NmhcCatalystSupported = compressionIgnition && GetBit(C, 0),
                NoxAftertreatmentSupported = compressionIgnition && GetBit(C, 1),
                BoostPressureSystemSupported = compressionIgnition && GetBit(C, 3),
                ExhaustGasSensorSupported = compressionIgnition && GetBit(C, 5),
                PmFilterSupported = compressionIgnition && GetBit(C, 6)
            };

            return result;
        }

        private static bool GetBit(byte value, int bit) {
            return (value & (1 << bit)) != 0;
        }
        private static string CleanRaw(string raw) {
            return raw
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace(">", " ")
                .Replace("SEARCHING...", " ", StringComparison.OrdinalIgnoreCase)
                .Trim();
        }

        private static List<byte> ExtractHexBytes(string text) {
            var matches = Regex.Matches(text, @"\b[0-9A-Fa-f]{2}\b");

            return matches
                .Select(m => Convert.ToByte(m.Value, 16))
                .ToList();
        }

        private static string BytesToAscii(IEnumerable<byte> bytes) {
            var cleanBytes = bytes
                .Where(b => b >= 0x20 && b <= 0x7E)
                .ToArray();

            return Encoding.ASCII.GetString(cleanBytes).Trim();
        }

        private static string GetNegativeResponseDescription(byte nrc) {
            return nrc switch {
                0x10 => "General Reject.",
                0x11 => "Service Not Supported.",
                0x12 => "SubFunction Not Supported.",
                0x13 => "Incorrect Message Length Or Invalid Format.",
                0x22 => "Conditions Not Correct.",
                0x24 => "Request Sequence Error.",
                0x31 => "Request Out Of Range.",
                0x33 => "Security Access Denied.",
                0x35 => "Invalid Key.",
                0x36 => "Exceeded Number Of Attempts.",
                0x37 => "Required Time Delay Not Expired.",
                0x78 => "Response Pending.",
                _ => $"NRC desconocido: 0x{nrc:X2}."
            };
        }
    }

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
