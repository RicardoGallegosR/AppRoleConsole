using Microsoft.VisualBasic.FileIO;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Utils;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.Intrinsics.X86;
using static SQLSIVEV.Infrastructure.Devices.Obd.Elm327;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class RBGR {
        public RBGR(IObdLogger? logger = null) {
            _logger = logger;
        }

        #region Declaración de Variables

        private int? _rpm, _rpmOff, _rpmOn,  _distMilKm, _distSinceClrKm, _runTimeMilMin, _timeSinceClr, _fallas03, _OperacionMotor, _WarmUpsDesdeBorrado,
            _EcuAddressInt, _ID_Calib, _ReadCvnMessageCount, _TiempoMotorEnMarchaSeg;
        private uint ? _odometro;
        private short? _vel, _BarometricPressure, _IatC, _IatCCoolantTempC;
        private int _baud = 38400, _readTimeoutMs = 6000, _writeTimeoutMs = 1200, _fallas07,_fallas0A, cnt03, cnt07, cnt0A;
        private static string SafeStr(object? x, string empty = "—") => x == null ? empty : x.ToString() ?? empty;
        private string _port = ""//"COM4"
            , _vin = string.Empty, _calJoined = string.Empty;
        private string? _FuelType = string.Empty, _EcuAddress = string.Empty, _NormativaObdVehiculo = string.Empty, _dtcList03 = string.Empty, _dtcList07 = string.Empty, _dtcList0A = string.Empty, 
            _protocolo = string.Empty, _cvn = string.Empty, _ReadCvnsRobusto = string.Empty, _Trama = string.Empty;
        private string[] _cal= Array.Empty<string>();
        private string[] expected = {
            "MISFIRE_MONITORING",
            "FUEL_SYSTEM_MONITORING",
            "COMPONENT_MONITORING",
            "CATALYST_MONITORING",
            "HEATED_CATALYST_MONITORING",
            "EVAPORATIVE_SYSTEM_MONITORING",
            "SECONDARY_AIR_SYSTEM_MONITORING",
            "OXYGEN_SENSOR_MONITORING",
            "OXYGEN_SENSOR_HEATER_MONITORING",
            "EGR_VVT_SYSTEM_MONITORING",
            "NMHC_CATALYST_MONITORING",
            "NOX_SCR_AFTERTREATMENT_MONITORING",
            "BOOST_PRESSURE_MONITORING",
            "EXHAUST_GAS_SENSOR_MONITORING",
            "PM_FILTER_MONITORING",
            "AC_REFRIGERANT_MONITORING"
        };
        private byte? _EmissionCode,_IntFuelType,_IntTipoCombustible0907, _fallas,_intNormativaObdVehiculo;
        private bool? _MilOn = false, _LeeDtcConfirmados = false, _LeeDtcPendientes = false, _LeeDtcPermanentes = false;
        private bool _vinFromObd = false, _leeMonitores;
        private double? _StftB1,_LtftB1,_MafGs,_MafKgH,_Tps,_TimingAdvance,_O2S1_V,_O2S2_V, _FuelLevel,_vOff, _CCM,_vOn;
        private IReadOnlyList<int> _Pids_01_20 = Array.Empty<int>(), _Pids_21_40 = Array.Empty<int>(), _Pids_41_60 = Array.Empty<int>();
        private readonly IObdLogger _logger;
        #endregion


        #region Detectar Puerto
        private string DetectarPuertoElm327(IProgress<string>? progreso = null) {
            var puertos = SerialPort.GetPortNames()
                                    .OrderBy(p => ExtraerNumeroPuerto(p))
                                    .ToArray();

            if (puertos.Length == 0) {
                SivevLogger.Warning("No se encontraron puertos COM disponibles.");
                throw new InvalidOperationException("No se encontraron puertos COM disponibles.");
            }

            var errores = new List<string>();

            foreach (var puerto in puertos) {
                progreso?.Report($"Probando puerto {puerto}...");
                //SivevLogger.Information($"Probando puerto {puerto}...");

                try {
                    if (EsPuertoElm327(puerto, progreso)) {
                        progreso?.Report($"ELM327 detectado en {puerto}.");
                        //SivevLogger.Information($"ELM327 detectado en {puerto}.");
                        return puerto;
                    }

                    errores.Add($"{puerto}: respondió, pero no como un ELM327 válido.");
                    SivevLogger.Warning($"{puerto}: respondió, pero no como un ELM327 válido.");

                } catch (Exception ex) {
                    errores.Add($"{puerto}: {ex.Message}");
                    SivevLogger.Error($"{puerto}: {ex.Message}");
                }
            }
            SivevLogger.Error("No se encontró un adaptador ELM327 en los puertos disponibles. Detalles: " + string.Join(" | ", errores));
            /*
            throw new InvalidOperationException(
                "No se encontró un adaptador ELM327 en los puertos disponibles. " +
                $"Detalle: {string.Join(" | ", errores)}");
            */
            return "COM4";
        }

        private bool EsPuertoElm327(string puerto, IProgress<string>? progreso = null) {
            using var serial = new SerialPort(puerto, _baud)  {
                ReadTimeout = _readTimeoutMs,
                WriteTimeout = _writeTimeoutMs,
                NewLine = "\r",
                DtrEnable = true,
                RtsEnable = true
            };

            serial.Open();

            // Limpiar buffers por si el adaptador trae basura previa
            serial.DiscardInBuffer();
            serial.DiscardOutBuffer();

            // A veces el ELM necesita un respiro al abrir el puerto
            Thread.Sleep(200);

            string respuesta1 = EnviarComando(serial, "ATZ", 1200);
            progreso?.Report($"{puerto} -> respuesta 1");
            //SivevLogger.Information($"{puerto} -> ATZ: {LimpiarTextoLog(respuesta1)}");

            string respuesta2 = EnviarComando(serial, "ATI", 800);
            progreso?.Report($"{puerto} -> respuesta 2");
            //SivevLogger.Information($"{puerto} -> ATI: {LimpiarTextoLog(respuesta2)}"); 

            string respuesta3 = EnviarComando(serial, "ATE0", 500);
            progreso?.Report($"{puerto} -> respuesta 3");
            //SivevLogger.Information($"{puerto} -> ATE0: {LimpiarTextoLog(respuesta3)}");

            string respuestaTotal = $"{respuesta1}\n{respuesta2}\n{respuesta3}".ToUpperInvariant();

            // Patrones típicos de ELM327
            if (respuestaTotal.Contains("ELM327")) return true;
            if (respuestaTotal.Contains("OBDII")) return true;
            if (respuestaTotal.Contains("ATI")) return true; // por si eco raro
            if (respuestaTotal.Contains("OK") && respuestaTotal.Contains(">")) return true;

            // Si responde prompt > y no dice ERROR, suele ser candidato muy fuerte
            if (respuestaTotal.Contains(">") && !respuestaTotal.Contains("ERROR"))
                return true;

            return false;
        }

        private string EnviarComando(SerialPort serial, string comando, int esperaMs = 500) {
            serial.DiscardInBuffer();
            serial.DiscardOutBuffer();

            serial.Write(comando + "\r");
            Thread.Sleep(esperaMs);

            return LeerRespuestaCompleta(serial);
        }

        private string LeerRespuestaCompleta(SerialPort serial) {
            var inicio = DateTime.UtcNow;
            var buffer = string.Empty;

            while ((DateTime.UtcNow - inicio).TotalMilliseconds < _readTimeoutMs) {
                try {
                    buffer += serial.ReadExisting();

                    // El prompt '>' suele indicar que terminó la respuesta
                    if (buffer.Contains(">"))
                        break;
                } catch {
                    // Ignorar lecturas intermedias
                }

                Thread.Sleep(50);
            }

            return buffer.Trim();
        }

        private static int ExtraerNumeroPuerto(string puerto) {
            var numeros = new string(puerto.Where(char.IsDigit).ToArray());
            return int.TryParse(numeros, out int n) ? n : int.MaxValue;
        }

        #endregion


        #region Clase produccion 
        public InspeccionObd2Set SpSetObd(IProgress<string>? progreso = null, IProgress<int>? porcentaje = null, IProgress<int>? rpmProgress = null, IProgress<double> batteryProgress = null) {
            InspeccionObd2Set p1 = new();
            var errores = new Dictionary<string, string>();
            int i = 0;
            try {
                if (string.IsNullOrWhiteSpace(_port)) {
                    SivevLogger.Information("Detectando puerto del adaptador OBD...");
                    _port = DetectarPuertoElm327(progreso);
                }

                try {
                    using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs, logger: _logger)) {
                        SivevLogger.Information($"Iniciando lectura OBD en puerto {_port} a {_baud} baudios.");
                        progreso?.Report($"Iniciando lectura OBD");

                        elm.Open();
                        progreso?.Report($"Abriendo puerto");
                        elm.Initialize(showHeaders: false);
                        SivevLogger.Information($"Puerto abierto e inicializado. Identificando protocolo de comunicación...");
                        progreso?.Report($"Identificando protocolo de comunicación");
                        //_protocolo = elm.WaitAndGetProtocolText(); //0100
                        p1.ProtocoloObd = elm.WaitAndGetProtocolText(); //0100
                        SivevLogger.Information($"Protocolo identificado: {_protocolo}. Iniciando consultas de datos...");
                        
                        progreso?.Report($"Pensando");
                        porcentaje?.Report(i++);
                        //_rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                        p1.RpmOn = TryQuery<short?>("RPM", () =>  (short?)elm.ReadRpm()?? 0, null, errores); 
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores)?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores)?? 0.0);
                        //SivevLogger.Information($"PID 011C.- RPM leída: {_rpm?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_vel = TryQuery<short?>("Velocidad", () => elm.ReadSpeedKmh(), null, errores); //010D
                        p1.VelVeh = TryQuery<short?>("Velocidad",() => elm.ReadSpeedKmh(), null,errores) ?? (short)-1;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 010D.- Velocidad leída: {_vel?.ToString() ?? "null"} km/h.");

                        porcentaje?.Report(i++);
                        //_vin = TryQuery<string>("VIN", () => elm.ReadVin() ?? "DESCONOCIDO", "DESCONOCIDO", errores);
                        p1.VehiculoId = TryQuery<string>("VIN", () => elm.ReadVin() ?? "DESCONOCIDO", "DESCONOCIDO", errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0902.- VIN leída: {_vin}.");

                        porcentaje?.Report(i++);
                        //_cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);//0904
                        p1.IDs_Adic = string.Join(" || ", TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores)) ?? "No se realizo lectura";
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0904.- CAL leída: {(_cal.Length > 0 ? string.Join(", ", _cal) : "null")}.");

                        porcentaje?.Report(i++);
                        //_vOn = TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores);
                        p1.VoltsSwOn = TryQuery<decimal?>("VOLTAGE", () => elm.ReadVoltageDecimal(), null, errores) ?? -1.0m;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"Voltage leída: {_vOn?.ToString() ?? "null"} V.");

                        porcentaje?.Report(i++);
                        //_dtcList03 = LeerYUnirDtcs("GET_DTC", () => elm.ReadStoredDtcs(), errores, out cnt03);
                        p1.CodigoError = LeerYUnirDtcs("GET_DTC", () => elm.ReadStoredDtcs(), errores, out cnt03) ?? "No se realizo lectura";
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        //_dtcList07 = LeerYUnirDtcs("GET_CURRENT_DTC", () => elm.ReadCurrentDtcs(), errores, out cnt07);
                        p1.CodigoErrorPendiente = LeerYUnirDtcs("GET_CURRENT_DTC", () => elm.ReadCurrentDtcs(), errores, out cnt07) ?? "No se realizo lectura";
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        //_dtcList0A = LeerYUnirDtcs("GET_PERMANENT_DTC", () => elm.ReadPermanentDtcs(), errores, out cnt0A);
                        p1.CodigoErrorPermanente = LeerYUnirDtcs("GET_PERMANENT_DTC", () => elm.ReadPermanentDtcs(), errores, out cnt0A) ?? "No se realizo lectura";
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);   

                        porcentaje?.Report(i++);
                        _LeeDtcConfirmados = cnt03 > 0;
                        
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        _LeeDtcPendientes = cnt07 > 0;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        _LeeDtcPermanentes = cnt0A > 0;

                        p1.LeeDtc = _LeeDtcConfirmados;
                        p1.LeeDtcPend = _LeeDtcPendientes;
                        p1.LeeDtcPerm = _LeeDtcPermanentes;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        //_vinFromObd = (!string.IsNullOrWhiteSpace(_vin) && !string.Equals(_vin, "DESCONOCIDO", StringComparison.OrdinalIgnoreCase)) ? true : false;
                        p1.LeeVin = (!string.IsNullOrWhiteSpace(p1.VehiculoId) && !string.Equals(p1.VehiculoId, "DESCONOCIDO", StringComparison.OrdinalIgnoreCase)) ? true : false;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        // NUEVOS VALORES 

                        porcentaje?.Report(i++);
                        //_distMilKm = TryQuery<int?>("DISTANCE_W_MIL", () => elm.ReadDistanceWithMilKm(), null, errores); //0121
                        p1.Dist_MIL_On = TryQuery<int?>("DISTANCE_W_MIL", () => elm.ReadDistanceWithMilKm(), null, errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0121.- Distancia recorrida con MIL encendido: {_distMilKm?.ToString() ?? "null"} km.");

                        porcentaje?.Report(i++);
                        //_distSinceClrKm = TryQuery<int?>("DISTANCE_SINCE_DTC_CLEAR", () => elm.ReadDistanceSinceClearKm(), null, errores);//0131
                        p1.Dist_Borrado_DTC = TryQuery<int?>("DISTANCE_SINCE_DTC_CLEAR", () => elm.ReadDistanceSinceClearKm(), null, errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0131.- Distancia recorrida desde el último borrado de DTC: {_distSinceClrKm?.ToString() ?? "null"} km.");

                        porcentaje?.Report(i++);
                        _runTimeMilMin = TryQuery<int?>("RUN_TIME_MIL", () => elm.ReadRunTimeMilMinutes(), null, errores);//014D
                        p1.Tpo_MIL_On = TryQuery<int?>("RUN_TIME_MIL", () => elm.ReadRunTimeMilMinutes(), null, errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 014D.- Tiempo de funcionamiento con MIL encendido: {_runTimeMilMin?.ToString() ?? "null"} minutos.");

                        porcentaje?.Report(i++);
                        //_timeSinceClr = TryQuery<int?>("TIME_SINCE_DTC_CLEARED", () => elm.ReadTimeSinceDtcClearedMinutes(), null, errores);//014E
                        p1.Tpo_Borrado_DTC = TryQuery<int?>("TIME_SINCE_DTC_CLEARED", () => elm.ReadTimeSinceDtcClearedMinutes(), null, errores);//014E
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 014E.- Tiempo transcurrido desde el último borrado de DTC: {_timeSinceClr?.ToString() ?? "null"} minutos.");

                        porcentaje?.Report(i++);
                        //_OperacionMotor = TryQuery<int?>("TiempoTotalSegundosOperacionMotor", () => elm.TiempoTotalSegundosOperacionMotor(), null, errores);//011F
                        p1.TiempoDeArranque = TryQuery<int?>("TiempoTotalSegundosOperacionMotor", () => elm.TiempoTotalSegundosOperacionMotor(), null, errores) ?? -1;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0); 
                        //SivevLogger.Information($"PID 011F.- Tiempo total de operación del motor: {_OperacionMotor?.ToString() ?? "null"} segundos.");

                        porcentaje?.Report(i++);
                        //_WarmUpsDesdeBorrado = TryQuery<int?>("WarmUpsDesdeBorrado", () => elm.WarmUpsSinceCodesCleared(), null, errores);//0130
                        p1.WarmUpsDesdeBorrado = TryQuery<int?>("WarmUpsDesdeBorrado", () => elm.WarmUpsSinceCodesCleared(), null, errores) ?? -1;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);   
                        //SivevLogger.Information($"PID 0130.- Número de ciclos de calentamiento desde el último borrado de DTC: {_WarmUpsDesdeBorrado?.ToString() ?? "null"}.");


                        porcentaje?.Report(i++);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        var status = TryQuery<Elm327.MonitorStatus?>("STATUS PID 0101", () => elm.ReadStatus(), null, errores);
                        
                        porcentaje?.Report(i++);
                        if (status is { } st) {
                            foreach (var name in expected) {
                                if (!st.Monitors.ContainsKey(name))
                                    st.Monitors[name] = (false, false);
                            }

                            var sb = new System.Text.StringBuilder(256);
                            sb.Append("PID_0101|");
                            sb.Append("MIL=").Append(st.MIL ? "1" : "0").Append("|");
                            sb.Append("DTC=").Append(st.DtcCount).Append("|");

                            // 2) Procesar monitores (si lo necesitas) --Agregar un Log en el futuro
                            if (st.Monitors.Count > 0) {
                                foreach (var kv in st.Monitors.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase)) {
                                    var (avail, comp) = kv.Value;
                                    string sAvail = avail ? "DISPONIBLE" : "NO DISPONIBLE";
                                    string sComp  = comp  ? "COMPLETO"   : "NO COMPLETO";
                                }
                            }
                            _Trama = sb.ToString();
                            _MilOn = st.MIL;
                            _fallas03 = st.DtcCount;
                            //porcentaje?.Report(90);
                        } else {
                            _MilOn = null;
                            _fallas03 = null;
                        }

                        var monitorCodes = new Dictionary<string, byte?>(StringComparer.OrdinalIgnoreCase);
                        foreach (var name in expected) {
                            if (status != null && status.Monitors.TryGetValue(name, out var tuple)) {
                                monitorCodes[name] = MapMonitorCode(tuple);
                            } else {
                                monitorCodes[name] = 0;
                            }
                        }


                        p1.Sdciic = monitorCodes["MISFIRE_MONITORING"] ?? 0;           // Misfire
                        p1.Sc = monitorCodes["FUEL_SYSTEM_MONITORING"] ?? 0;           // Fuel System
                        p1.Sci = monitorCodes["COMPONENT_MONITORING"] ?? 0;            // Components
                        p1.Secc = monitorCodes["CATALYST_MONITORING"] ?? 0;            // Catalyst
                        p1.Sccc = monitorCodes["HEATED_CATALYST_MONITORING"] ?? 0;     // Heated Catalyst
                        p1.Se = monitorCodes["EVAPORATIVE_SYSTEM_MONITORING"] ?? 0;    // Evaporative System
                        p1.Ssa = monitorCodes["SECONDARY_AIR_SYSTEM_MONITORING"] ?? 0; // Secondary Air System

                        // aquí es donde estaba el desajuste:
                        p1.Sfaa = monitorCodes["AC_REFRIGERANT_MONITORING"] ?? 0;       // A/C Refrigerant
                        p1.Srge = monitorCodes["EGR_VVT_SYSTEM_MONITORING"] ?? 0;       // EGR System
                        p1.Sso = monitorCodes["OXYGEN_SENSOR_MONITORING"] ?? 0;         // Oxygen Sensor
                        p1.Scso = monitorCodes["OXYGEN_SENSOR_HEATER_MONITORING"] ?? 0; // Oxygen Sensor Heater

                        porcentaje?.Report(i++);
                        //_leeMonitores = (status != null);
                        p1.LeeMonitores = (status != null);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        /////////////////////////////////////////////////////////////////////////////////////////////////
                        porcentaje?.Report(i++);
                        //_intNormativaObdVehiculo = TryQuery<byte?>("NORMATIVA_OBD_VEHICULO_int", () => elm.LeerNormativaObdVehiculo(), null, errores);//011C
                        p1.NEV = TryQuery<byte?>("NORMATIVA_OBD_VEHICULO_int", () => elm.LeerNormativaObdVehiculo(), null, errores);//011C
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 011C.- Normativa OBD del vehículo (int): {_intNormativaObdVehiculo?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_NormativaObdVehiculo = TryQuery<string?>("NORMATIVA_OBD_VEHICULO_string", () => elm.NormativaObdVehiculo(_intNormativaObdVehiculo), null, errores);//011C
                        //SivevLogger.Information($"PID 011C.- Normativa OBD del vehículo (string): {_NormativaObdVehiculo ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_IatCCoolantTempC = TryQuery<short?>("COOLANTTEMPC", () => elm.TemperaturaRefrigeranteC(), null, errores);//0105
                        p1.TR = TryQuery<short?>("COOLANTTEMPC", () => elm.TemperaturaRefrigeranteC(), null, errores);//0105
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0105.- Temperatura del refrigerante (°C): {_IatCCoolantTempC?.ToString() ?? "null"} °C.");

                        porcentaje?.Report(i++);
                        //_StftB1 = TryQuery<double?>("STFTB1", () => elm.StftBank1(), null, errores);//0106
                        p1.STFT_B1 = TryQuery<decimal?>("STFTB1", () => elm.StftBank1_decimal(), null, errores);//0106
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0106.- STFT Bank 1: {_StftB1?.ToString() ?? "null"} %.");

                        porcentaje?.Report(i++);
                        //_LtftB1 = TryQuery<double?>("LTFTB1", () => elm.LtftBank1(), null, errores);//0107
                        p1.LTFT_B1 = TryQuery<decimal?>("LTFTB1", () => elm.LtftBank1_decimal(), null, errores);//0107
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0107.- LTFT Bank 1: {_LtftB1?.ToString() ?? "null"} %.");

                        _rpmOn = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                        p1.RpmCheck = (short)(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);

                        porcentaje?.Report(i++);
                        //_IatC = TryQuery<short?>("IATC", () => elm.TemperaturaAireAdmisionC(), null, errores);//010F
                        p1.IAT = TryQuery<short?>("IATC", () => elm.TemperaturaAireAdmisionC(), null, errores);//010F
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 010F.- Temperatura del aire de admisión (°C): {_IatC?.ToString() ?? "null"} °C.");

                        porcentaje?.Report(i++);
                        //_MafGs = TryQuery<double?>("MAFGS", () => elm.FlujoAireMaf(), null, errores);//0110
                        p1.MAF = TryQuery<decimal?>("MAFGS", () => elm.FlujoAireMaf(), null, errores);//0110
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0110.- Flujo de aire MAF (g/s): {_MafGs?.ToString() ?? "null"} g/s.");

                        porcentaje?.Report(i++);
                        //_MafKgH = TryQuery<double?>("MAFKGH", () => elm.FlujoAireMafKgPorHora(), null, errores);
                        p1.MafKgH = TryQuery<decimal?>("MAFKGH", () => elm.FlujoAireMafKgPorHora(), null, errores);//0110
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0110.- Flujo de aire MAF (kg/h): {_MafKgH?.ToString() ?? "null"} kg/h.");

                        porcentaje?.Report(i++);
                        //_Tps = TryQuery<double?>("TPS", () => elm.PosicionAcelerador(), null, errores);//0111
                        p1.TPS = TryQuery<decimal?>("TPS", () => elm.PosicionAcelerador(), null, errores)??0m;//0111
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0111.- Posición del acelerador (TPS): {_Tps?.ToString() ?? "null"} %.");

                        porcentaje?.Report(i++);
                        //_TimingAdvance = TryQuery<double?>("TIMING_ADVANCE", () => elm.AvanceEncendido(), null, errores);//010E
                        p1.AvanceEnc = TryQuery<decimal?>("TIMING_ADVANCE", () => (decimal?)elm.AvanceEncendido(), null, errores)??0m;//010E
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 010E.- Avance de encendido: {_TimingAdvance?.ToString() ?? "null"} grados.");

                        porcentaje?.Report(i++);
                        //_O2S1_V = TryQuery<double?>("O2S1_V", () => elm.O2Sensor1Voltage(), null, errores);//0114
                        p1.Volt_O2 = TryQuery<decimal?>("O2S1_V", () => (decimal?)elm.O2Sensor1Voltage(), null, errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);   
                        //SivevLogger.Information($"PID 0114.- Voltaje del sensor de oxígeno 1: {_O2S1_V?.ToString() ?? "null"} V.");

                        porcentaje?.Report(i++);
                        //_O2S2_V = TryQuery<double?>("O2S2_V", () => elm.O2Sensor2Voltage(), null, errores);//0115
                        p1.Volt_O2_S2 = TryQuery<decimal?>("O2S2_V", () => (decimal?)elm.O2Sensor2Voltage(), null, errores);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0115.- Voltaje del sensor de oxígeno 2: {_O2S2_V?.ToString() ?? "null"} V.");

                        porcentaje?.Report(i++);
                        //_FuelLevel = TryQuery<double?>("FUEL_LEVEL", () => elm.NivelCombustible(), null, errores);//012F
                        p1.NivelComb = TryQuery<decimal?>("FUEL_LEVEL", () => (decimal?)elm.NivelCombustible(), null, errores) ?? 0m;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 012F.- Nivel de combustible: {_FuelLevel?.ToString() ?? "null"} %.");

                        porcentaje?.Report(i++);
                        //_BarometricPressure = TryQuery<short?>("BAROMETRIC_PRESSURE", () => elm.PresionBarometrica(), null, errores);//0133
                        p1.Pres_Baro = TryQuery<short?>("BAROMETRIC_PRESSURE", () => elm.PresionBarometrica(), null, errores) ?? 0;//0133
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0133.- Presión barométrica: {_BarometricPressure?.ToString() ?? "null"} kPa.");

                        porcentaje?.Report(i++);
                        //_FuelType = TryQuery<string?>("FUEL_TYPE", () => elm.TipoCombustible(), null, errores);//0151
                        p1.FuelType = TryQuery<string?>("FUEL_TYPE", () => elm.TipoCombustible(), null, errores)?? "DESCONOCIDO_";//0151
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0151.- Tipo de combustible: {_FuelType ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_IntFuelType = TryQuery<byte?>("INT_FUEL_TYPE", () => elm.byteTipoCombustible0151(), null, errores);//0151
                        p1.Combustible0151Id = TryQuery<byte?>("INT_FUEL_TYPE", () => elm.byteTipoCombustible0151(), null, errores) ?? 0;//0151
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0151.- Tipo de combustible (int): {_IntFuelType?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_IntTipoCombustible0907 = TryQuery<byte?>("INT_TIPO_COMBUSTIBLE_0907", () => elm.intTipoCombustible0907(), null, errores);
                        p1.Combustible0907Id = TryQuery<byte?>("INT_TIPO_COMBUSTIBLE_0907", () => elm.intTipoCombustible0907(), null, errores) ?? 0;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0907.- Tipo de combustible (int): {_IntTipoCombustible0907?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_EcuAddress = TryQuery<string?>("ECU_ADDRESS", () => elm.EcuAddress(), null, errores); //090A
                        p1.Dir_ECU = TryQuery<string?>("ECU_ADDRESS", () => elm.EcuAddress(), null, errores) ?? "DESCONOCIDO_";
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 090A.- Dirección ECU: {_EcuAddress ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_EcuAddressInt = TryQuery<int?>("ECU_ADDRESS_INT", () => elm.EcuAddressInt(), null, errores);//090A int
                        p1.EcuAddressInt = TryQuery<int?>("ECU_ADDRESS_INT", () => elm.EcuAddressInt(), null, errores) ?? 0;//090A int
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 090A.- Dirección ECU (int): {_EcuAddressInt?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_CCM = TryQuery<double?>("CCM", () => elm.LoadCalc(), null, errores); //0104
                        p1.CCM = TryQuery<decimal?>("CCM", () => (decimal?)elm.LoadCalc(), null, errores) ?? 0m; //0104
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0104.- Carga calculada (CCM): {_CCM?.ToString() ?? "null"} %.");

                        porcentaje?.Report(i++);
                        //_EmissionCode = TryQuery<byte?>("EMISSION_CODE", () => elm.RequisitosEmisionesVehiculo(), null, errores);//015F
                        p1.Req_Emisiones = TryQuery<byte?>("EMISSION_CODE", () => elm.RequisitosEmisionesVehiculo(), null, errores) ?? 0;//015F
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);   
                        //SivevLogger.Information($"PID 015F.- Requisitos de emisiones del vehículo: {_EmissionCode?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_Pids_01_20 = TryQuery<IReadOnlyList<int>>("PIDS_01_20", () => elm.PidsSoportadosBloque("0100", 0x01), Array.Empty<int>(), errores);
                        p1.PIDS_Sup_01_20 = string.Join(" || ", TryQuery<IReadOnlyList<int>>("PIDS_01_20", () => elm.PidsSoportadosBloque("0100", 0x01), Array.Empty<int>(), errores));
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        // SivevLogger.Information($"PID 0100.- PIDs soportados (01-20): {_Pids_01_20?.Count ?? 0}.");

                        porcentaje?.Report(i++);
                        //_Pids_21_40 = TryQuery<IReadOnlyList<int>>("PIDS_21_40", () => elm.PidsSoportadosBloque("0120", 0x21), Array.Empty<int>(), errores);
                        p1.PIDS_Sup_21_40 = string.Join(" || ", TryQuery<IReadOnlyList<int>>("PIDS_21_40", () => elm.PidsSoportadosBloque("0120", 0x21), Array.Empty<int>(), errores));
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0120.- PIDs soportados (21-40): {_Pids_21_40?.Count ?? 0}.");

                        porcentaje?.Report(i++);
                        p1.PIDS_Sup_41_60 = string.Join(" || ", TryQuery<IReadOnlyList<int>>("PIDS_41_60", () => elm.PidsSoportadosBloque("0140", 0x41), Array.Empty<int>(), errores));
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0140.- PIDs soportados (41-60): {_Pids_41_60?.Count ?? 0}.");

                        porcentaje?.Report(i++);
                        //_odometro = TryQuery<uint?>("Odometro", () => elm.ReadOdometer01A6(), null, errores);
                        p1.Odometro = TryQuery<uint?>("Odometro", () => elm.ReadOdometer01A6(), null, errores) ?? 0;
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 01A6.- Odómetro: {_odometro?.ToString() ?? "null"} km.");

                        porcentaje?.Report(i++);
                        //_fallas = ContarPxxxxUnicos(_dtcList03);
                        p1.Fallas = ContarPxxxxUnicos(_dtcList03);
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);

                        porcentaje?.Report(i++);
                        //_ID_Calib = TryQuery<int?>("_ID_Calib", () => elm.CalibIdMessageCount(), null, errores);// 0903
                        p1.ID_Calib = TryQuery<int?>("_ID_Calib", () => elm.CalibIdMessageCount(), null, errores) ?? 0;// 0903
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0903.- ID de calibración: {_ID_Calib?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_ReadCvnMessageCount = TryQuery<int?>("_ReadCvnMessageCount", () => elm.ReadCvnMessageCount(), null, errores);// 0905
                        p1.ReadCvnMessageCount = TryQuery<int?>("_ReadCvnMessageCount", () => elm.ReadCvnMessageCount(), null, errores) ?? 0;// 0905
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0905.- Número de mensajes CVN leídos: {_ReadCvnMessageCount?.ToString() ?? "null"}.");

                        porcentaje?.Report(i++);
                        //_ReadCvnsRobusto = TryQuery<string?>("ReadCvnsRobusto", () => elm.ReadCvnsRobusto(_ReadCvnMessageCount), null, errores); //0906
                        p1.ReadCvnsRobusto = TryQuery<string?>("ReadCvnsRobusto", () => elm.ReadCvnsRobusto(_ReadCvnMessageCount), null, errores) ?? "DESCONOCIDO_"; //0906
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 0906.- CVNs leídos (robusto): {(_ReadCvnsRobusto != null ? _ReadCvnsRobusto.Substring(0, Math.Min(100, _ReadCvnsRobusto.Length)) + ( _ReadCvnsRobusto.Length > 100 ? "..." : "") : "null")}.");

                        porcentaje?.Report(i++);
                        //_TiempoMotorEnMarchaSeg = TryQuery<int?>("TiempoMotorEnMarchaSeg", () => elm.TiempoMotorEnMarchaSeg(), null, errores); //017F
                        p1.TiempoMotorEnMarchaSeg = TryQuery<int?>("TiempoMotorEnMarchaSeg", () => elm.TiempoMotorEnMarchaSeg(), null, errores) ?? -1; //017F
                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);
                        //SivevLogger.Information($"PID 017F.- Tiempo total que el motor ha estado en marcha (segundos): {_TiempoMotorEnMarchaSeg?.ToString() ?? "null"} segundos.");

                        porcentaje?.Report(i++);
                        //_rpmOff = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                        p1.RpmOff = (short)(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);                        //rpmProgress?.Report(TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores) ?? 0);
                        //batteryProgress?.Report(TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores) ?? 0.0);


                        /*
                         NUEVOS PIDS 19/08/2026
                         */
                        porcentaje?.Report(i++);
                        p1._Lambda = TryQuery<decimal?>("LAMBDA", () => elm.Lambda(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._StftB2 = TryQuery<decimal?>("STFT_B2", () => elm.STFT_B2(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._LtftB2 = TryQuery<decimal?>("LTFT_B2", () => elm.LTFT_B2(), null, errores) ?? 0m; 
                        porcentaje?.Report(i++);
                        p1._RelativeAcceleratorPedalPosition = TryQuery<decimal?>("RelativeAcceleratorPedalPosition", () => elm.RelativeAcceleratorPedalPosition(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._AbsoluteLoadValue = TryQuery<decimal?>("AbsoluteLoadValue", () => elm.AbsoluteLoadValue(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._AmbientAirTemperature = TryQuery<short?>("AmbientAirTemperature", () => elm.AmbientAirTemperature(), null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._EngineOilTemperature = TryQuery<short?>("EngineOilTemperature", () => elm.EngineOilTemperature(), null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._EngineCoolantTemperature = TryQuery<short?>("EngineCoolantTemperature", () => elm.EngineCoolantTemperature(), null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._IntakeAirTemperature = TryQuery<short?>("IntakeAirTemperature", () => elm.IntakeAirTemperature(), null, errores) ?? 0;

                        porcentaje?.Report(i++);
                        p1._Map = TryQuery<short?>("MAP", () => elm.MAP(), null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._B1S1_V = TryQuery<decimal?>("B1S1_V  ", () => elm.B1S1_V(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._B1S2_V = TryQuery<decimal?>("B1S2_V", () => elm.B1S2_V(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._B2S1_V = TryQuery<decimal?>("B2S1_V", () => elm.B2S1_V(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._B2S2_V = TryQuery<decimal?>("B2S2_V", () => elm.B2S2_V(), null, errores) ?? 0m;
                        porcentaje?.Report(i++);
                        p1._EgtB1S1 = TryQuery<short?>("EGT_B1S1",() => elm.EGT_B1S1(), null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._EgtB1S2 = TryQuery<short?>("EGT_B1S2",() => elm.EGT_B1S2(),null,errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._EgtB2S1 = TryQuery<short?>("EGT_B2S1",() => elm.EGT_B2S1(),null, errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._EgtB2S2 = TryQuery<short?>("EGT_B2S2", () => elm.EGT_B2S2(),null,errores) ?? 0;
                        porcentaje?.Report(i++);
                        p1._B2S1 = TryQuery<decimal?>("B2S1", () => elm.B2S1(), null, errores) ?? 0m;//013D
                        porcentaje?.Report(i++);
                        p1._B2S2 = TryQuery<decimal?>("B2S2", () => elm.B2S2(), null, errores) ?? 0m;//013F
                        porcentaje?.Report(i++);
                        p1._B1S1 = TryQuery<decimal?>("B1S1", () => elm.B1S1(), null, errores) ?? 0m;//013D
                        porcentaje?.Report(i++);
                        p1._B1S2 = TryQuery<decimal?>("B1S2", () => elm.B1S2(), null, errores) ?? 0m;//013F




                        porcentaje?.Report(100);
                        SivevLogger.Information($"Lectura OBD finalizada exitosamente. VIN: {_vin}, Protocolo: {_protocolo}");
                        if (_logger is ObdTxtLogger txtLogger) {
                            txtLogger.EncabezadoLectura(
                                vin: p1.VehiculoId ?? "DESCONOCIDO",
                                protocolo: p1.ProtocoloObd ?? "DESCONOCIDO",
                                puerto: _port
                            );

                            txtLogger.ResumenDtc(
                                raw03: p1.CodigoError ?? "",
                                dec03: string.IsNullOrWhiteSpace(p1.CodigoError) ? "SIN DTC" : p1.CodigoError,

                                raw07: p1.CodigoErrorPendiente ?? "",
                                dec07: string.IsNullOrWhiteSpace(p1.CodigoErrorPendiente) ? "SIN DTC" : p1.CodigoErrorPendiente,

                                raw0A: p1.CodigoErrorPermanente ?? "",
                                dec0A: string.IsNullOrWhiteSpace(p1.CodigoErrorPermanente) ? "SIN DTC" : p1.CodigoErrorPermanente
                            );
                            try {
                                txtLogger.ComprimirZip();
                            } catch (Exception ex) {
                                SivevLogger.Error($"No se pudo comprimir el log OBD: {ex}");
                            }
                        }
                        p1.ConexionObd = true;
                        return p1;
                            /*new InspeccionObd2Set {
                            ConexionObd = true,
                            VehiculoId = _vin ?? "No se realizo lectura",
                            ProtocoloObd = _protocolo ?? "No se realizo lectura",

                            CodigoError = _dtcList03 ?? "No se realizo lectura",
                            CodigoErrorPendiente = _dtcList07 ?? "No se realizo lectura",
                            CodigoErrorPermanente = _dtcList0A ?? "No se realizo lectura",
                            
                            Mil = _MilOn.HasValue ? (_MilOn.Value ? (byte)1 : (byte)0) : (byte)0,
                            Fallas = _fallas,
                            Sdciic = monitorCodes["MISFIRE_MONITORING"] ?? 0,           // Misfire
                            Sc = monitorCodes["FUEL_SYSTEM_MONITORING"] ?? 0,           // Fuel System
                            Sci = monitorCodes["COMPONENT_MONITORING"] ?? 0,            // Components
                            Secc = monitorCodes["CATALYST_MONITORING"] ?? 0,            // Catalyst
                            Sccc = monitorCodes["HEATED_CATALYST_MONITORING"] ?? 0,     // Heated Catalyst
                            Se = monitorCodes["EVAPORATIVE_SYSTEM_MONITORING"] ?? 0,    // Evaporative System
                            Ssa = monitorCodes["SECONDARY_AIR_SYSTEM_MONITORING"] ?? 0, // Secondary Air System

                            // aquí es donde estaba el desajuste:
                            Sfaa = monitorCodes["AC_REFRIGERANT_MONITORING"] ?? 0,       // A/C Refrigerant
                            Srge = monitorCodes["EGR_VVT_SYSTEM_MONITORING"] ?? 0,       // EGR System
                            Sso = monitorCodes["OXYGEN_SENSOR_MONITORING"] ?? 0,         // Oxygen Sensor
                            Scso = monitorCodes["OXYGEN_SENSOR_HEATER_MONITORING"] ?? 0, // Oxygen Sensor Heater

                            
                            LeeMonitores = _leeMonitores,
                            LeeDtc = _LeeDtcConfirmados,
                            LeeDtcPend = _LeeDtcPendientes,
                            LeeDtcPerm = _LeeDtcPermanentes,
                            LeeVin = _vinFromObd,
                            VoltsSwOn = _vOn.HasValue ? (decimal?)_vOn.Value : (decimal?)(-1m),
                            RpmOn = _rpm.HasValue ? (short?)_rpm.Value : (short?)(-1),
                            

                            // NUEVOS VALORES :D
                            Dist_MIL_On = _distMilKm ?? -1,
                            Dist_Borrado_DTC = _distSinceClrKm ?? -1,
                            TiempoDeArranque = _OperacionMotor ?? -1,
                            Tpo_Borrado_DTC = _timeSinceClr ?? -1,

                            //NumVerifCalib = "",
                            IDs_Adic = string.Join(" || ", _cal) ?? "No se realizo lectura",

                            // Más valores instruidos por Toñin GALVAN 
                            CCM = _CCM.HasValue ? (decimal?)_CCM.Value : (decimal?)(0m),
                            WarmUpsDesdeBorrado = _WarmUpsDesdeBorrado.HasValue ? _WarmUpsDesdeBorrado.Value : (0),
                            NEV = _intNormativaObdVehiculo.HasValue ? (byte?)_intNormativaObdVehiculo.Value : (byte?)(0),
                            TR = _IatCCoolantTempC ?? 0,
                            STFT_B1 = _StftB1.HasValue ? (decimal?)_StftB1.Value : (decimal?)(0m),
                            LTFT_B1 = _LtftB1.HasValue ? (decimal?)_LtftB1.Value : (decimal?)(0m),
                            IAT = _IatC.HasValue ? (short?)_IatC.Value : (short?)(-1),
                            MAF = _MafGs.HasValue ? (decimal?)_MafGs.Value : (decimal?)(0m),
                            MafKgH = _MafKgH.HasValue ? (decimal?)_MafKgH.Value : (decimal?)(0m),
                            TPS = _Tps.HasValue ? (decimal?)_Tps.Value : (decimal?)(0m),
                            AvanceEnc = _TimingAdvance.HasValue ? (decimal?)_TimingAdvance.Value : (decimal?)(0m),
                            VelVeh = _vel ?? -1,
                            Volt_O2 = _O2S1_V.HasValue ? (decimal?)_O2S1_V.Value : (decimal?)(0m),
                            Volt_O2_S2 = _O2S2_V.HasValue ? (decimal?)_O2S2_V.Value : (decimal?)(0m),
                            NivelComb = _FuelLevel.HasValue ? (decimal?)_FuelLevel.Value : (decimal?)(0m),
                            Pres_Baro = _BarometricPressure ?? -1,
                            FuelType = _FuelType ?? "No se realizo lectura",
                            Combustible0151Id = _IntFuelType ?? 0,
                            Combustible0907Id = _IntTipoCombustible0907 ?? 0,
                            Dir_ECU = _EcuAddress ?? "No se realizo lectura",
                            EcuAddressInt = _EcuAddressInt,
                            Req_Emisiones = _EmissionCode ?? 0,

                            PIDS_Sup_01_20 = string.Join(" || ", _Pids_01_20),
                            PIDS_Sup_21_40 = string.Join(" || ", _Pids_21_40),
                            PIDS_Sup_41_60 = string.Join(" || ", _Pids_41_60),
                            Odometro = _odometro,
                            ID_Calib = _ID_Calib ?? 0,
                            ReadCvnMessageCount = _ReadCvnMessageCount ?? 0,
                            ReadCvnsRobusto = _ReadCvnsRobusto ?? "No se realizo lectura",
                            TramaPid0101 = _Trama ?? "No se realizo lectura",
                            TiempoMotorEnMarchaSeg = _TiempoMotorEnMarchaSeg ?? 0,
                            
                        };
                       //*/
                    }
                } catch (Exception exPuerto) {
                    SivevLogger.Warning($"Falló el puerto actual '{_port}'. Se intentará redetectar. Detalle: {exPuerto.Message}");
                    p1.ConexionObd = false;
                    p1.Mensaje = $"Error en lectura SBD: {exPuerto.Message}";
                    return p1;
                }
            } catch (Exception ex) {
                SivevLogger.Error($"Error en la lectura de los COM en SpSetObd: {ex.Message}");
                //throw;
                p1.ConexionObd = false;
                p1.Mensaje = $"Error en la lectura de los COM en SpSetObd: {ex.Message}";
                return p1;
            }
        }

        #endregion

        #region utils

        private string LeerYUnirDtcs(string nombreLectura, Func<List<string>?> lector, Dictionary<string, string> errores, out int cantidad) {
            var lista = TryQuery<List<string>?>(nombreLectura, lector, null, errores) ?? new List<string>();
            cantidad = lista.Count;

            return lista.Count > 0 ? string.Join(" || ", 
                                                            lista.Where(x => !string.IsNullOrWhiteSpace(x))
                                                                 .Select(x => x.Trim().ToUpperInvariant())
                                                                 .Distinct()) : "";
        }

        private static T TryQuery<T>(string key, Func<T> func, T fallback, Dictionary<string, string> errores) {
            try {
                var val = func();
                return val == null ? fallback : val;
            } catch (Exception e) {
                errores[key] = e.Message;
                try {
                    SivevLogger.Error($"ERROR DETECTADO en {key}.- {e.Message}");
                } catch { }
                return fallback;
            }
        }
        private byte MapMonitorCode((bool avail, bool comp) v) {
            if (v.avail && v.comp) return 1;
            if (!v.avail && v.comp) return 2;
            if (v.avail && !v.comp) return 3;
            return 0;
        }

        #endregion



        #region Contar codigos de Falla
        public static byte ContarPxxxxUnicos(string? texto) {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            ReadOnlySpan<char> s = texto.AsSpan();

            for (int i = 0; i <= s.Length - 5; i++) {
                if (char.ToUpperInvariant(s[i]) != 'P')
                    continue;

                char c1 = s[i + 1], c2 = s[i + 2], c3 = s[i + 3], c4 = s[i + 4];
                if (!IsHex(c1) || !IsHex(c2) || !IsHex(c3) || !IsHex(c4))
                    continue;

                // sin tuplas con Span, sin string.Create
                string code = new string(new[] {
                    'P',
                    char.ToUpperInvariant(c1),
                    char.ToUpperInvariant(c2),
                    char.ToUpperInvariant(c3),
                    char.ToUpperInvariant(c4)
                });

                set.Add(code);
                i += 4;
            }

            return (byte)set.Count;
        }

        private static bool IsHex(char c) {
            c = char.ToUpperInvariant(c);
            return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F');
        }
        #endregion

    }
}
