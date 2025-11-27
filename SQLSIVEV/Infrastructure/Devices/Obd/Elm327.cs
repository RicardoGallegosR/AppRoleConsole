using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public sealed class Elm327 : IDisposable {
        private readonly SerialPort _port;

        public Elm327(string portName, int baud = 38400, int readTimeoutMs = 3000, int writeTimeoutMs = 1000) {
            // Acepta \\.\COMX o COMX
            var p = portName.StartsWith(@"\\.\", StringComparison.OrdinalIgnoreCase) ? portName.Substring(4) : portName;

            _port = new SerialPort(p, baud) {
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = readTimeoutMs,
                WriteTimeout = writeTimeoutMs,
                NewLine = "\r",
                Encoding = Encoding.ASCII,
                DtrEnable = true,
                RtsEnable = true
            };
        }

        public static string[] GetPorts() => SerialPort.GetPortNames()
            .OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToArray();

        public void Open() {
            if (!_port.IsOpen) _port.Open();
            SafeDrain();
        }

        public void Dispose() {
            try { if (_port.IsOpen) _port.Close(); } catch { }
        }

        // ====== Núcleo de lectura no bloqueante por línea ======
        private string ReadUntilPrompt(int timeoutMs) {
            var sw = Stopwatch.StartNew();
            var sb = new StringBuilder();

            while (sw.ElapsedMilliseconds < timeoutMs) {
                try {
                    var chunk = _port.ReadExisting();
                    if (!string.IsNullOrEmpty(chunk)) {
                        sb.Append(chunk);
                        if (sb.ToString().Contains(">"))
                            break;
                    } else {
                        Thread.Sleep(20);
                    }
                } catch (TimeoutException) {
                    // seguimos intentando hasta agotar timeoutMs
                }
            }
            return sb.ToString();
        }

        private static string Clean(string s) {
            return s.Replace("\r", "\n")
                    .Replace("\0", "")
                    .Trim();
        }

        private void SafeDrain() {
            try { _port.DiscardInBuffer(); _port.DiscardOutBuffer(); } catch { }
        }

        // Despierta el ELM: CR, AT, busca OK o '>'
        public bool Probe(int timeoutMs = 1500) {
            _port.Write("\r");                   // wake
            Thread.Sleep(100);
            SafeDrain();

            _port.Write("AT\r");
            var resp = ReadUntilPrompt(timeoutMs);
            resp = Clean(resp);
            return resp.IndexOf("OK", StringComparison.OrdinalIgnoreCase) >= 0
                   || resp.Contains(">");
        }

        public string ExecRaw(string cmd, int timeoutMs = 1500) {
            _port.Write(cmd + "\r");
            var resp = ReadUntilPrompt(timeoutMs);
            return Clean(resp);
        }

        public void Initialize(bool showHeaders = false) {
            if (!Probe(1500)) { _port.Write("\r\r"); ReadUntilPrompt(500); }

            _port.Write("ATZ\r");
            ReadUntilPrompt(3000);
            SafeDrain();

            ExecRaw("ATE0", 800);
            ExecRaw("ATL0", 800);
            ExecRaw("ATS0", 800);                 // espacios off por defecto
            ExecRaw(showHeaders ? "ATH1" : "ATH0", 800);
            ExecRaw("ATCAF1", 800);               // asegúrate de auto-formateo ON
            ExecRaw("ATAT1", 800);                // adaptive timing
            ExecRaw("ATSP0", 1500);               // auto protocolo
            ExecRaw("0100", 2000);                // “engancha” el protocolo negociado
            SafeDrain();
        }

        public string WaitAndGetProtocolText(int maxTries = 3, int settleMs = 150) {
            string proto = "";
            for (int i = 0; i < maxTries; i++) {
                var _ = ExecRaw("0100", 4000);       // provoca negociación
                Thread.Sleep(settleMs);               // deja “asentar”
                proto = ReadProtocolText();           // ATDP + (ATDPN)
                if (!proto.Contains("(A0", StringComparison.OrdinalIgnoreCase))
                    break;                            // ya negoció (A6, A7, etc.)
            }
            return proto;
        }



        public int? ReadRpm() {
            var resp = ExecRaw("010C", 2000); // puede tardar más según protocolo
            // Busca "41 0C A B" (con o sin espacios)
            var compact = resp.Replace(" ", "").Replace("\n", "");
            var idx = compact.IndexOf("410C", StringComparison.OrdinalIgnoreCase);
            if (idx < 0 || compact.Length < idx + 8) return null;

            try {
                int A = Convert.ToInt32(compact.Substring(idx + 4, 2), 16);
                int B = Convert.ToInt32(compact.Substring(idx + 6, 2), 16);
                return ((A << 8) | B) / 4;
            } catch { return null; }
        }


        public int? ReadSpeedKmh() {
            var resp = ExecRaw("010D", 4000);
            var compact = resp.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            var idx = compact.IndexOf("410D", StringComparison.OrdinalIgnoreCase);
            if (idx < 0 || compact.Length < idx + 6) return null;
            try {
                int v = Convert.ToInt32(compact.Substring(idx + 4, 2), 16);
                return v; // km/h
            } catch { return null; }
        }

        // Lee VIN (modo 09 PID 02), concatenando frames 49 02 01/02/03
        public string? ReadVin() {
            // Salida limpia para modo 09
            ExecRaw("ATCAF1", 600);  // auto-format
            ExecRaw("ATH0", 600);  // sin headers
            ExecRaw("ATS0", 600);  // sin espacios
            ExecRaw("ATAT1", 600);  // adaptive timing
            ExecRaw("ATST96", 600);  // un poco más de espera (ISO/KWP)

            var resp = ExecRaw("0902", 10000);

            // Tokeniza en bytes hex de 2 dígitos
            var hex = Regex.Matches(resp, "[0-9A-Fa-f]{2}")
                        .Select(m => m.Value.ToUpperInvariant())
                        .ToList();

            // parts["01"], "02", "03", "04"... se crean on-demand
            var parts = new Dictionary<string, List<byte>>();

            for (int i = 0; i + 2 < hex.Count; i++) {
                if (hex[i] == "49" && hex[i + 1] == "02" && Regex.IsMatch(hex[i + 2], "^0[0-9A-F]$")) {
                    string part = hex[i + 2];   // "01","02","03","04",...
                    if (!parts.TryGetValue(part, out var list)) {
                        list = new List<byte>(8);
                        parts[part] = list;
                    }

                    int j = i + 3;
                    bool maybeSkipLength = (part == "01"); // algunas ECUs ponen longitud aquí

                    while (j < hex.Count) {
                        // ¿nuevo bloque 49 02 xx? Entonces termina esta parte
                        if (j + 2 < hex.Count && hex[j] == "49" && hex[j + 1] == "02" &&
                            Regex.IsMatch(hex[j + 2], "^0[0-9A-F]$"))
                            break;

                        if (byte.TryParse(hex[j], NumberStyles.HexNumber, null, out var b)) {
                            // En parte 01, el primer byte puede ser la longitud total del VIN (0x11=17)
                            if (maybeSkipLength) {
                                maybeSkipLength = false;
                                // Si parece un largo razonable (17..25), lo saltamos.
                                if (b >= 0x11 && b <= 0x19) { j++; continue; }
                                // Si no, lo tratamos como carácter normal y seguimos.
                            }

                            if (b != 0x00) list.Add(b); // ignora padding
                        }
                        j++;
                    }
                    i = j - 1; // avanza el cursor
                }
            }

            // Ensambla partes en orden (01..0F) y corta a 17
            var vinBytes = new List<byte>(24);
            foreach (var p in parts.Keys.OrderBy(k => k)) { // "01","02","03","04",...            
                foreach (var b in parts[p]) {
                    if (b >= 0x20 && b <= 0x7E) { // ASCII visible
                        vinBytes.Add(b);
                        if (vinBytes.Count >= 17) break;
                    }
                }
                if (vinBytes.Count >= 17) break;
            }

            var vin = new string(vinBytes.Select(b => (char)b).ToArray()).Trim().ToUpperInvariant();
            if (vin.Length > 17) vin = vin[..17];

            // Reintento suave si quedó corto (<17) en ISO/KWP
            if (vin.Length < 17) {
                ExecRaw("ATSTA0", 400);       // aún más paciencia
                var resp2 = ExecRaw("0902", 12000);
                if (!string.Equals(resp2, resp, StringComparison.Ordinal)) {
                    // Opcional: podrías reejecutar el mismo parse con resp2 aquí.
                    // Para mantenerlo simple, solo devuelve si quedó válido.
                    // (Si quieres, duplico el parse con resp2 como hicimos arriba.)
                }
            }
            return string.IsNullOrWhiteSpace(vin) ? null : vin;
        }




        public double? ReadVoltage() {
            var resp = ExecRaw("ATRV", 2000).Replace(" ", "").Replace("\n", "").Replace("\r", "");
            var m = Regex.Match(resp, @"(\d+(?:\.\d+)?)V", RegexOptions.IgnoreCase);
            if (!m.Success) return null;
            if (double.TryParse(m.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var v))
                return v;
            return null;
        }

        // -- Helper genérico para PIDs 01xx que devuelven A y B (2 bytes) --
        private (int A, int B)? ReadPidAB(string cmd, int timeoutMs = 3000) {
            var resp = ExecRaw(cmd, timeoutMs);
            var compact = resp.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            // cmd "0131" -> buscamos "41 31"
            var pid = "41" + cmd.Substring(2);
            var idx = compact.IndexOf(pid, StringComparison.OrdinalIgnoreCase);
            if (idx < 0 || compact.Length < idx + 8) return null;
            try {
                int A = Convert.ToInt32(compact.Substring(idx + 4, 2), 16);
                int B = Convert.ToInt32(compact.Substring(idx + 6, 2), 16);
                return (A, B);
            } catch { return null; }
        }

        // Protocolo en texto (y código corto entre paréntesis si hay)
        public string ReadProtocolText() {
            var dp = ExecRaw("ATDP", 1500).Replace("\n", " ").Trim();
            var dpn = ExecRaw("ATDPN", 1500).Trim();
            return string.IsNullOrWhiteSpace(dpn) ? dp : $"{dp} ({dpn})";
        }

        // Distancia con MIL encendido (PID 01 21) en km
        public int? ReadDistanceWithMilKm() {
            var ab = ReadPidAB("0121", 3000);
            return ab.HasValue ? (int?)(ab.Value.A * 256 + ab.Value.B) : null;
        }

        // Distancia desde que se borraron DTC (PID 01 31) en km
        public int? ReadDistanceSinceClearKm() {
            var ab = ReadPidAB("0131", 3000);
            return ab.HasValue ? (int?)(ab.Value.A * 256 + ab.Value.B) : null;
        }

        // Tiempo con MIL encendido (PID 01 4D) en minutos
        public int? ReadRunTimeMilMinutes() {
            var ab = ReadPidAB("014D", 3000);
            return ab.HasValue ? (int?)(ab.Value.A * 256 + ab.Value.B) : null;
        }

        // Tiempo desde que se borraron DTC (PID 01 4E) en minutos
        public int? ReadTimeSinceDtcClearedMinutes() {
            var ab = ReadPidAB("014E", 3000);
            return ab.HasValue ? (int?)(ab.Value.A * 256 + ab.Value.B) : null;
        }

        // Calibration ID(s) (modo 09 PID 04) — puede devolver 1..N IDs
        public string[] ReadCalibrationIds() {
            // Preparación “limpia” para modo 09
            ExecRaw("ATCAF1", 600);   // formato auto
            ExecRaw("ATH0", 600);   // sin headers
            ExecRaw("ATS0", 600);   // sin espacios
            ExecRaw("ATAT1", 600);
            ExecRaw("ATST64", 600);

            var resp = ExecRaw("0904", 8000);

            var hex = Regex.Matches(resp, "[0-9A-Fa-f]{2}")
                        .Select(m => m.Value.ToUpperInvariant())
                        .ToList();

            var ids = new List<string>();
            var current = new List<byte>();

            void PushCurrent() {
                if (current.Count == 0) return;
                var s = new string(current.Where(b => b >= 0x20 && b <= 0x7E).Select(b => (char)b).ToArray()).Trim();
                if (!string.IsNullOrEmpty(s)) ids.Add(s);
                current.Clear();
            }

            // Lee todas las partes 49 04 01..nn
            for (int i = 0; i + 2 < hex.Count; i++) {
                if (hex[i] == "49" && hex[i + 1] == "04" && Regex.IsMatch(hex[i + 2], "^0[0-9A-F]$")) {
                    int j = i + 3;
                    while (j < hex.Count && !(j + 2 < hex.Count && hex[j] == "49" && hex[j + 1] == "04" && Regex.IsMatch(hex[j + 2], "^0[0-9A-F]$"))) {
                        if (byte.TryParse(hex[j], NumberStyles.HexNumber, null, out var b)) {
                            if (b == 0x00) PushCurrent();   // separador entre múltiples CALID
                            else current.Add(b);
                        }
                        j++;
                    }
                    // al cerrar esta parte, seguimos; la función empuja al final
                    i = j - 1;
                }
            }
            PushCurrent();
            return ids.Distinct().ToArray();
        }
        // --- Decode DTC: dos bytes A,B -> "P0XXX" / "C1XXX" / "B2XXX" / "U3XXX" ---
        private static string DecodeDtc(byte A, byte B) {
            char system = "PCBU"[A >> 6];
            int d1 = (A >> 4) & 0x3;
            int d2 = A & 0xF;
            int d3 = (B >> 4) & 0xF;
            int d4 = B & 0xF;
            return $"{system}{d1:X}{d2:X}{d3:X}{d4:X}";
        }

        // Parser robusto de DTCs para modo 03/07 (funciona en CAN 11/29 e ISO/KWP)
        // Parser robusto de DTCs para modo 03/07 (funciona en CAN 11/29 e ISO/KWP)
        private List<string> ReadDtcsInternal(string cmd, string respPrefix) {
            // Salida limpia para evitar headers y bytes de transporte
            ExecRaw("ATCAF1", 600);  // auto-format ON
            ExecRaw("ATH0", 600);  // sin headers
            ExecRaw("ATS0", 600);  // sin espacios
            ExecRaw("ATAT1", 600);  // adaptive timing

            var resp = ExecRaw(cmd, 9000); // 03 ó 07

            // Extrae solo pares hex (2 dígitos)
            var tokens = Regex.Matches(resp, "[0-9A-Fa-f]{2}")
                            .Select(m => m.Value.ToUpperInvariant())
                            .ToList();

            // Busca el prefijo de respuesta (43 para 03, 47 para 07)
            int k = tokens.FindIndex(t => t == respPrefix);
            if (k < 0) return new List<string>();

            // Bytes de datos después del prefijo
            var data = new List<byte>();
            for (int i = k + 1; i < tokens.Count; i++) {
                if (byte.TryParse(tokens[i], NumberStyles.HexNumber, null, out var b))
                    data.Add(b);
            }

            // Algunas ECUs ponen un "byte de longitud" justo después de 43/47.
            // Heurística segura:
            //  - si la cantidad de bytes es IMPAR, descarta el primero
            //  - o si el primer byte coincide con el resto (conteo), descártalo
            if (data.Count > 0 && (data.Count % 2 == 1 || data[0] == data.Count - 1))
                data.RemoveAt(0);

            // Forma pares A,B y decodifica, ignorando 00 00 (padding)
            var dtcs = new List<string>();
            for (int i = 0; i + 1 < data.Count; i += 2) {
                byte A = data[i], B = data[i + 1];
                if (A == 0x00 && B == 0x00) continue;

                // P/C/B/U según bits altos
                char system = "PCBU"[A >> 6];
                int d1 = (A >> 4) & 0x3;
                int d2 = A & 0xF;
                int d3 = (B >> 4) & 0xF;
                int d4 = B & 0xF;
                dtcs.Add($"{system}{d1:X}{d2:X}{d3:X}{d4:X}");
            }
            return dtcs;
        }
        public List<string> ReadStoredDtcs()
            => ReadDtcsInternal("03", "43");  // modo 03 → respuesta 43     

        public List<string> ReadCurrentDtcs()
            => ReadDtcsInternal("07", "47");  // modo 07 → respuesta 47

        public List<string> ReadPermanentDtcs()
            => ReadDtcsInternal("0A", "4A");  // modo 0A → respuesta 4A


        // ====== Monitor Status (PID 01 01) ======
        public sealed class MonitorStatus {
            public bool MIL { get; init; }
            public int DtcCount { get; init; }
            // NombreMonitor -> (available, complete)
            public Dictionary<string, (bool available, bool complete)> Monitors { get; init; } =
                new(StringComparer.OrdinalIgnoreCase);
        }

        public MonitorStatus? ReadStatus() {
            try {
                // Limpia formato y consulta PID 01 01
                ExecRaw("ATCAF1", 600);
                ExecRaw("ATH0", 600);
                ExecRaw("ATS0", 600);
                ExecRaw("ATAT1", 600);

                var resp = ExecRaw("0101", 4000);
                var compact = resp.Replace(" ", "").Replace("\r", "").Replace("\n", "");
                var idx = compact.IndexOf("4101", StringComparison.OrdinalIgnoreCase);
                if (idx < 0 || compact.Length < idx + 12) return null; // necesitamos al menos A..D (4 bytes)

                // Extrae A,B,C,D como bytes
                byte A = Convert.ToByte(compact.Substring(idx + 4, 2), 16);
                byte B = Convert.ToByte(compact.Substring(idx + 6, 2), 16);
                byte C = Convert.ToByte(compact.Substring(idx + 8, 2), 16);
                byte D = Convert.ToByte(compact.Substring(idx + 10, 2), 16);

                var status = new MonitorStatus {
                    MIL = (A & 0x80) != 0,
                    DtcCount = A & 0x7F
                };

                // Heurística estándar: bit 3 de B indica tipo de ignición:
                // 0 = Spark        (gasolina),
                // 1 = Compression  (diésel)
                bool compressionIgnition = (B & 0x08) != 0;

                // Monitores "continuos" (se reportan siempre en B y su "complete" en C)
                void AddCont(string name, int bit) {
                    bool avail = (B & (1 << bit)) != 0;           // B2..B0
                    bool comp  = (B & (1 << (bit + 4))) == 0;     // B6..B4  (0 = completo)
                    status.Monitors[name] = (avail, comp);
                }

                AddCont("MISFIRE_MONITORING", 0);
                AddCont("FUEL_SYSTEM_MONITORING", 1);
                AddCont("COMPONENT_MONITORING", 2);

                // Para los no continuos:
                // Spark(gasolina): disponibilidad en C, complete en D, bits 0..7:
                //          [0]CATALYST,
                //          [1]HEATED_CATALYST,
                //          [2]EVAPORATIVE_SYSTEM,
                //          [3]SECONDARY_AIR_SYSTEM,
                //          [4]A/C_REFRIGERANT,
                //          [5]OXYGEN_SENSOR,
                //          [6]OXYGEN_SENSOR_HEATER,
                //          [7]EGR_VVT_SYSTEM

                // Diesel:  disponibilidad en C, complete en D, bits típicos:
                //          usaremos un mapeo común en la práctica: 
                //          [0].-NMHC_CATALYST,
                //          [1].-NOX_SCR_AFTERTREATMENT,
                //          [2].-BOOST_PRESSURE,
                //          [3].-EXHAUST_GAS_SENSOR,
                //          [4].-PM_FILTER,
                //          [7].-EGR_VVT_SYSTEM
                if (!compressionIgnition) {
                    var sparkMap = new (string name, int bit)[] {
                        ("CATALYST_MONITORING", 0),
                        ("HEATED_CATALYST_MONITORING", 1),
                        ("EVAPORATIVE_SYSTEM_MONITORING", 2),
                        ("SECONDARY_AIR_SYSTEM_MONITORING", 3),
                        ("AC_REFRIGERANT_MONITORING", 4),
                        ("OXYGEN_SENSOR_MONITORING", 5),
                        ("OXYGEN_SENSOR_HEATER_MONITORING", 6),
                        ("EGR_VVT_SYSTEM_MONITORING", 7),
                    };
                    foreach (var (name, bit) in sparkMap) {
                        bool avail = (C & (1 << bit)) != 0;           // C
                        bool comp  = (D & (1 << bit)) == 0;           // D (0 = completo)
                        status.Monitors[name] = (avail, comp);
                    }
                } else {
                    var dieselMap = new (string name, int bit)[] {
                        ("NMHC_CATALYST_MONITORING", 0),
                        ("NOX_SCR_AFTERTREATMENT_MONITORING", 1),
                        ("BOOST_PRESSURE_MONITORING", 2),
                        ("EXHAUST_GAS_SENSOR_MONITORING", 3),
                        ("PM_FILTER_MONITORING", 4),
                        ("EGR_VVT_SYSTEM_MONITORING", 7),
                    };
                    foreach (var (name, bit) in dieselMap) {
                        bool avail = (C & (1 << bit)) != 0;           // C
                        bool comp  = (D & (1 << bit)) == 0;           // D (0 = completo)
                        status.Monitors[name] = (avail, comp);
                    }
                }

                return status;
            } catch {
                return null; // seguridad: nunca truenes la app si la ECU no soporta este PID
            }
        }
    }
}
