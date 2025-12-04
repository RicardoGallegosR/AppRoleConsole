namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class RBGR {

        #region Declaración de Variables
        private static string SafeStr(object? x, string empty = "—") => x == null ? empty : x.ToString() ?? empty;

        private int? _rpm, _vel, _distMilKm, _distSinceClrKm, _runTimeMilMin, _timeSinceClr, _fallas03, _OperacionMotor, _WarmUpsDesdeBorrado;
        private int _baud = 38400, _readTimeoutMs = 6000, _writeTimeoutMs = 1200, _fallas07,_fallas0A;

        private string _port = "COM4", _vin = string.Empty, _calJoined = string.Empty, 
            _dtcList03 = string.Empty, _dtcList07 = string.Empty, _dtcList0A = string.Empty, _protocolo = string.Empty, _cvn = string.Empty;
        private string[] _cal;
        private const byte SIN_COMUNICACION = 4;

        private bool? _MilOn = false, _LeeDtcConfirmados = false, _LeeDtcPendientes = false, _LeeDtcPermanentes = false;
        private bool _vinFromObd;
        
        private ObdResultado _obd;
        private ObdMonitoresLuzMil _monitores;

        // TEMPORALES 
        // OFF/ON con *fallback* por si no corriste la lectura
        private int _rpmOff, _rpmOn, cnt03, cnt07, cnt0A;
        
        private double? _vOff;
        private double _vOn;

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
        #endregion

        #region PRIMER INTENTO
        // Lecturas de los valores del diagnóstico de OBD
        public LecturasIniciales LecturasPrincipales () {
            string mensaje = "";
            try {
                using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                    elm.Open();
                    elm.Initialize(showHeaders: false);

                    _protocolo = elm.WaitAndGetProtocolText();

                    var errores = new Dictionary<string, string>();

                    // Lecturas principales
                    _rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);
                    _vel = TryQuery<int?>("Velocidad", () => elm.ReadSpeedKmh(), null, errores);
                    _vin = TryQuery<string>("VIN", () => elm.ReadVin() ?? "DESCONOCIDO", "DESCONOCIDO", errores);
                    _cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);
                    _vOff = TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores);

                    // DTC Confirmados, Pendientes, Permanentes :D


                    // Distancias/tiempos relacionados a DTC/MIL (PID 01 xx)+
                    // Generico para 3, 7 y 0A
                    _distMilKm = TryQuery<int?>("DISTANCE_W_MIL", () => elm.ReadDistanceWithMilKm(), null, errores);
                    _distSinceClrKm = TryQuery<int?>("DISTANCE_SINCE_DTC_CLEAR", () => elm.ReadDistanceSinceClearKm(), null, errores);
                    _runTimeMilMin = TryQuery<int?>("RUN_TIME_MIL", () => elm.ReadRunTimeMilMinutes(), null, errores);
                    _timeSinceClr = TryQuery<int?>("TIME_SINCE_DTC_CLEARED", () => elm.ReadTimeSinceDtcClearedMinutes(), null, errores);

                    // DTC (modo 03 y 07) — sin condiciones por ahora (olvidamos J1939)
                    _dtcList03 = LeerYUnirDtcs("GET_DTC", () => elm.ReadStoredDtcs(), errores, out cnt03);
                    _dtcList07 = LeerYUnirDtcs("GET_CURRENT_DTC", () => elm.ReadCurrentDtcs(), errores, out cnt07);
                    _dtcList0A = LeerYUnirDtcs("GET_PERMANENT_DTC", () => elm.ReadPermanentDtcs(), errores, out cnt0A);

                }
            } catch (Exception ex) { 
                mensaje = ex.Message;
            }
            return new LecturasIniciales {
                rpm = _rpm,
                vel = _vel,
                cal = _cal,
                protocolo = _protocolo ?? "",
                vin = _vin ?? "",
                vOff = _vOff,
                distMilKm = _distMilKm,
                distSinceClrKm= _distSinceClrKm,
                runTimeMilMin= _runTimeMilMin,
                timeSinceClr= _timeSinceClr,
                dtcList03= _dtcList03,
                dtcList07= _dtcList07,
                dtcList0A= _dtcList0A,

                Mensaje = mensaje
            };

        }
        #endregion

        #region MONITORES
        public ObdMonitoresLuzMil Monitores() {
            string mensaje = "";
            var errores = new Dictionary<string, string>();

            try {
                using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                    elm.Open();
                    elm.Initialize(showHeaders: false);
                    _protocolo = elm.WaitAndGetProtocolText();

                    string vin = TryQuery<string>("VIN", () => elm.ReadVin() ?? "desconocido", "desconocido", errores);
                    bool vinFromObd = (!string.IsNullOrWhiteSpace(vin) && !string.Equals(vin, "desconocido", StringComparison.OrdinalIgnoreCase)) ? true : false;


                    var status = TryQuery<Elm327.MonitorStatus?>("STATUS", () => elm.ReadStatus(), null, errores);
                    if (status != null) {
                        foreach (var name in expected) {
                            if (!status.Monitors.ContainsKey(name))
                                status.Monitors[name] = (false, false);
                        }
                    }

                    if(status != null){                     
                        if (status.Monitors.Count > 0) {
                            Console.WriteLine("Monitores:");
                            foreach (var kv in status.Monitors.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase)) {
                                var (avail, comp) = kv.Value;
                                string sAvail = avail ? "disponible" : "no disponible";
                                string sComp = comp ? "completo" : "no completo";
                                Console.WriteLine($" - {kv.Key}: {sAvail}, {sComp}");
                            }
                        }
                    }

                    if (status != null) {
                        foreach (var name in expected)
                            if (!status.Monitors.ContainsKey(name))
                                status.Monitors[name] = (false, false);
                    }
                    _MilOn = status?.MIL;
                    _fallas03 = status?.DtcCount;

                    var monitorCodes = new Dictionary<string, byte?>(StringComparer.OrdinalIgnoreCase);
                    foreach (var name in expected) {
                        if (status != null && status.Monitors.TryGetValue(name, out var tuple)) {
                            monitorCodes[name] = MapMonitorCode(tuple);
                        }
                        else {
                            monitorCodes[name] = 0;
                        }
                    }
                    return new ObdMonitoresLuzMil {
                        Intentos = 0,
                        ConexionOb = 1,
                        Mil = _MilOn,
                        Fallas = 0,
                        Sdciic = monitorCodes["MISFIRE_MONITORING"],
                        Secc = monitorCodes["FUEL_SYSTEM_MONITORING"],
                        Sc = monitorCodes["COMPONENT_MONITORING"],
                        Sso = monitorCodes["CATALYST_MONITORING"],
                        Sci = monitorCodes["HEATED_CATALYST_MONITORING"],
                        Sccc = monitorCodes["EVAPORATIVE_SYSTEM_MONITORING"],
                        Se = monitorCodes["SECONDARY_AIR_SYSTEM_MONITORING"],
                        Ssa = monitorCodes["OXYGEN_SENSOR_MONITORING"],
                        Sfaa = monitorCodes["OXYGEN_SENSOR_HEATER_MONITORING"],
                        Scso = monitorCodes["EGR_VVT_SYSTEM_MONITORING"],
                        Srge = monitorCodes["BOOST_PRESSURE_MONITORING"],
                        LeeMonitores = false,
                        LeeDtc = false,
                        LeeDtcPend = false,
                        LeeVin = vinFromObd,
                        DtcCount = _fallas03,
                        mensaje = "ok"

                    };
                }
            } catch (Exception ex) {
                mensaje = ex.Message;
            }
            

            return new ObdMonitoresLuzMil {
                mensaje = mensaje,
                Intentos = 0,
                ConexionOb = 0,
                Mil = false,
                Fallas = 0,
                Sdciic = 0,
                Secc = 0,
                Sc = 0,
                Sso = 0,
                Sci = 0,
                Sccc = 0,
                Se = 0,
                Ssa = 0,
                Sfaa = 0,
                Scso = 0,
                Srge = 0,
                LeeMonitores = false,
                LeeDtc = false,
                LeeDtcPend = false,
                LeeVin = false,
                DtcCount = 0
            };
        }
        #endregion


        #region Clase produccion 
        public ObdResultado SpSetObd() {
            string mensaje = "";
            var errores = new Dictionary<string, string>();
            try {
                using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                    elm.Open();
                    elm.Initialize(showHeaders: false);
                    _protocolo = elm.WaitAndGetProtocolText();

                    _rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);
                    _vel = TryQuery<int?>("Velocidad", () => elm.ReadSpeedKmh(), null, errores);
                    _vin = TryQuery<string>("VIN", () => elm.ReadVin() ?? "DESCONOCIDO", "DESCONOCIDO", errores);
                    _cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);
                    _vOff = TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores);


                    _dtcList03 = LeerYUnirDtcs("GET_DTC", () => elm.ReadStoredDtcs(), errores, out cnt03);
                    _dtcList07 = LeerYUnirDtcs("GET_CURRENT_DTC", () => elm.ReadCurrentDtcs(), errores, out cnt07);
                    _dtcList0A = LeerYUnirDtcs("GET_PERMANENT_DTC", () => elm.ReadPermanentDtcs(), errores, out cnt0A);

                    _LeeDtcConfirmados = cnt03 > 0;
                    _LeeDtcPendientes = cnt07 > 0;
                    _LeeDtcPermanentes = cnt0A > 0;

                    _vinFromObd = (!string.IsNullOrWhiteSpace(_vin) && !string.Equals(_vin, "DESCONOCIDO", StringComparison.OrdinalIgnoreCase)) ? true : false;

                    // NUEVOS VALORES 
                    _distMilKm = TryQuery<int?>("DISTANCE_W_MIL", () => elm.ReadDistanceWithMilKm(), null, errores);
                    _distSinceClrKm = TryQuery<int?>("DISTANCE_SINCE_DTC_CLEAR", () => elm.ReadDistanceSinceClearKm(), null, errores);
                    _runTimeMilMin = TryQuery<int?>("RUN_TIME_MIL", () => elm.ReadRunTimeMilMinutes(), null, errores);
                    _timeSinceClr = TryQuery<int?>("TIME_SINCE_DTC_CLEARED", () => elm.ReadTimeSinceDtcClearedMinutes(), null, errores);
                    _cvn = TryQuery<string>(
                        "CVN",
                        () => {
                            var lista = elm.ReadCvns(); // List<string> o null

                            if (lista != null && lista.Count > 0)
                                return string.Join(" || ", lista);

                            return "DESCONOCIDO";
                        },
                        "DESCONOCIDO",
                        errores
                    );
                    _cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);

                    // Más valores instruidos por Toñin Cara de pan :D
                    _OperacionMotor = TryQuery<int?>("TiempoTotalSegundosOperacionMotor", () => elm.TiempoTotalSegundosOperacionMotor(), null, errores);
                    _WarmUpsDesdeBorrado = TryQuery<int?>("WarmUpsDesdeBorrado", () => elm.WarmUpsSinceCodesCleared(), null, errores);



                    var status = TryQuery<Elm327.MonitorStatus?>("STATUS", () => elm.ReadStatus(), null, errores);
                    if (status is { } st) { 
                        foreach (var name in expected) {
                            if (!st.Monitors.ContainsKey(name))
                                st.Monitors[name] = (false, false);
                        }

                        // 2) Procesar monitores (si lo necesitas) --Agregar un Log en el futuro
                        if (st.Monitors.Count > 0) {
                            foreach (var kv in st.Monitors.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase)) {
                                var (avail, comp) = kv.Value;
                                string sAvail = avail ? "DISPONIBLE" : "NO DISPONIBLE";
                                string sComp  = comp  ? "COMPLETO"   : "NO COMPLETO";
                            }
                        }
                        _MilOn = st.MIL;
                        _fallas03 = st.DtcCount;
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
                    return new ObdResultado {
                        VehiculoId = _vin,
                        ProtocoloObd = _protocolo,
                        CodError = _dtcList03,
                        CodErrorPend = _dtcList07,
                        Intentos = 1,           //
                        ConexionOb = 1,         //
                        Mil = _MilOn.HasValue ? (_MilOn.Value ? (byte)1 : (byte)0) : (byte)0,
                        Fallas = (byte)cnt03,
                        Sdciic = monitorCodes["MISFIRE_MONITORING"],              // Misfire
                        Secc = monitorCodes["FUEL_SYSTEM_MONITORING"],          // Fuel System
                        Sc = monitorCodes["COMPONENT_MONITORING"],            // Components
                        Sso = monitorCodes["CATALYST_MONITORING"],             // Catalyst
                        Sci = monitorCodes["HEATED_CATALYST_MONITORING"],      // Heated Catalyst
                        Sccc = monitorCodes["EVAPORATIVE_SYSTEM_MONITORING"],   // Evaporative System
                        Se = monitorCodes["SECONDARY_AIR_SYSTEM_MONITORING"], // Secondary Air System

                        // aquí es donde estaba el desajuste:
                        Ssa = monitorCodes["AC_REFRIGERANT_MONITORING"],       // A/C Refrigerant
                        Sfaa = monitorCodes["EGR_VVT_SYSTEM_MONITORING"],       // EGR System
                        Scso = monitorCodes["OXYGEN_SENSOR_MONITORING"],        // Oxygen Sensor
                        Srge = monitorCodes["OXYGEN_SENSOR_HEATER_MONITORING"], // Oxygen Sensor Heater

                        LeeMonitores = false,  
                        LeeDtc = _LeeDtcConfirmados,         
                        LeeDtcPend = _LeeDtcPendientes,
                        LeeVin = _vinFromObd,
                        VoltsSwOff = (decimal)_vOff,
                        VoltsSwOn = 0,          //
                        RpmOff = (short)_rpm,
                        RpmOn = 0,              //
                        RpmCheck = 0,           //

                        mensaje = "",

                        // NUEVOS VALORES :D
                        distMilKm = _distMilKm,
                        distSinceClrKm = _distSinceClrKm,
                        runTimeMilMin = _runTimeMilMin,
                        timeSinceClr = _timeSinceClr,
                        cvn = _cvn,
                        cal = _cal,

                        // Más valores instruidos por Toñin Cara de pan :D
                        TiempoTotalSegundosOperacionMotor = _OperacionMotor,
                        WarmUpsDesdeBorrado = _WarmUpsDesdeBorrado



                    };
                }
            } catch (Exception ex) {
                mensaje = ex.Message;
            }

            return new ObdResultado {
                VehiculoId = "DESCONOCIDO",
                ProtocoloObd = "DESCONOCIDO",
                CodError = "",
                CodErrorPend = "",
                Intentos = 0,
                ConexionOb = 0,
                Mil = 0,
                Fallas = 0,
                Sdciic = 0,
                Secc = 0,
                Sc = 0,
                Sso = 0,
                Sci = 0,
                Sccc = 0,
                Se = 0,
                Ssa = 0,
                Sfaa = 0,
                Scso = 0,
                Srge = 0,
                VoltsSwOff = 0,
                VoltsSwOn = 0,
                RpmOff = 0,
                RpmOn = 0,
                RpmCheck = 0,
                LeeMonitores = false,
                LeeDtc = false,
                LeeDtcPend = false,
                LeeVin = false,
                mensaje = mensaje,
                distMilKm = _distMilKm,
                distSinceClrKm = _distSinceClrKm,
                runTimeMilMin = _runTimeMilMin,
                timeSinceClr = _timeSinceClr

            };
        }
        #endregion

        #region utils

        private string LeerYUnirDtcs(string nombreLectura, Func<List<string>?> lector, Dictionary<string, string> errores, out int cantidad) {
            var lista = TryQuery<List<string>?>(nombreLectura, lector, null, errores) ?? new List<string>();
            cantidad = lista.Count;

            return lista.Count > 0 ? string.Join(" || ", lista) : "DESCONOCIDO";
        }




        private static T TryQuery<T>(string key, Func<T> func, T fallback, Dictionary<string, string> errores) {
            try {
                var val = func();
                return val == null ? fallback : val;
            } catch (Exception e) {
                errores[key] = e.Message;
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

    }
}
