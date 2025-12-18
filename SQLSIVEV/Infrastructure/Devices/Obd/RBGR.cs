using Microsoft.VisualBasic.FileIO;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Utils;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class RBGR {

        #region Declaración de Variables

        private int? _rpm,  _distMilKm, _distSinceClrKm, _runTimeMilMin, _timeSinceClr, _fallas03, _OperacionMotor, _WarmUpsDesdeBorrado,
            _EcuAddressInt;
        private short? _vel, _BarometricPressure, _IatC, _IatCCoolantTempC;

        private int _baud = 38400, _readTimeoutMs = 6000, _writeTimeoutMs = 1200, _fallas07,_fallas0A, _rpmOff, _rpmOn, cnt03, cnt07, cnt0A;
        private static string SafeStr(object? x, string empty = "—") => x == null ? empty : x.ToString() ?? empty;
        private string _port = "COM4", _vin = string.Empty, _calJoined = string.Empty;
            
        private string? _FuelType = string.Empty, _EcuAddress = string.Empty, _NormativaObdVehiculo = string.Empty, _dtcList03 = string.Empty, _dtcList07 = string.Empty, _dtcList0A = string.Empty, _protocolo = string.Empty, _cvn = string.Empty;

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


        private const byte SIN_COMUNICACION = 4;
        private byte? _EmissionCode,_IntFuelType,_IntTipoCombustible0907;


        private bool? _MilOn = false, _LeeDtcConfirmados = false, _LeeDtcPendientes = false, _LeeDtcPermanentes = false;
        private bool _vinFromObd = false;

        private ObdResultado? _obd;
        private ObdMonitoresLuzMil? _monitores;


        private double? _StftB1,_LtftB1,_MafGs,_MafKgH,_Tps,_TimingAdvance,_O2S1_V,_O2S2_V, _FuelLevel,_vOff, _CCM,_vOn;

        private IReadOnlyList<int> _Pids_01_20 = Array.Empty<int>(), _Pids_21_40 = Array.Empty<int>(), _Pids_41_60 = Array.Empty<int>();



        #endregion

        #region PRIMER INTENTO
        // Lecturas de los valores del diagnóstico de OBD
        public LecturasIniciales LecturasPrincipales() {
            string mensaje = "";
            try {
                using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                    elm.Open();
                    elm.Initialize(showHeaders: false);

                    _protocolo = elm.WaitAndGetProtocolText();

                    var errores = new Dictionary<string, string>();

                    // Lecturas principales
                    _rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                    _vel = TryQuery<short?>("Velocidad", () => elm.ReadSpeedKmh(), null, errores);
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
                distSinceClrKm = _distSinceClrKm,
                runTimeMilMin = _runTimeMilMin,
                timeSinceClr = _timeSinceClr,
                dtcList03 = _dtcList03,
                dtcList07 = _dtcList07,
                dtcList0A = _dtcList0A,

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

                    if (status != null) {
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
                        } else {
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
                        Mensaje = "ok"

                    };
                }
            } catch (Exception ex) {
                mensaje = ex.Message;
            }


            return new ObdMonitoresLuzMil {
                Mensaje = mensaje,
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
        public InspeccionObd2Set SpSetObd() {
            string mensaje = "";
            var errores = new Dictionary<string, string>();
            try {
                using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                    elm.Open();
                    elm.Initialize(showHeaders: false);
                    _protocolo = elm.WaitAndGetProtocolText();

                    _rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                    _vel = TryQuery<short?>("Velocidad", () => elm.ReadSpeedKmh(), null, errores); //010D
                    _vin = TryQuery<string>("VIN", () => elm.ReadVin() ?? "DESCONOCIDO", "DESCONOCIDO", errores);
                    _cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);//0904
                    _vOn = TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores);


                    _dtcList03 = LeerYUnirDtcs("GET_DTC", () => elm.ReadStoredDtcs(), errores, out cnt03);
                    _dtcList07 = LeerYUnirDtcs("GET_CURRENT_DTC", () => elm.ReadCurrentDtcs(), errores, out cnt07);
                    _dtcList0A = LeerYUnirDtcs("GET_PERMANENT_DTC", () => elm.ReadPermanentDtcs(), errores, out cnt0A);

                    _LeeDtcConfirmados = cnt03 > 0;
                    _LeeDtcPendientes = cnt07 > 0;
                    _LeeDtcPermanentes = cnt0A > 0;

                    _vinFromObd = (!string.IsNullOrWhiteSpace(_vin) && !string.Equals(_vin, "DESCONOCIDO", StringComparison.OrdinalIgnoreCase)) ? true : false;

                    // NUEVOS VALORES 

                    _distMilKm = TryQuery<int?>("DISTANCE_W_MIL", () => elm.ReadDistanceWithMilKm(), null, errores); //0121
                    _distSinceClrKm = TryQuery<int?>("DISTANCE_SINCE_DTC_CLEAR", () => elm.ReadDistanceSinceClearKm(), null, errores);//0131
                    _runTimeMilMin = TryQuery<int?>("RUN_TIME_MIL", () => elm.ReadRunTimeMilMinutes(), null, errores);//014D
                    _timeSinceClr = TryQuery<int?>("TIME_SINCE_DTC_CLEARED", () => elm.ReadTimeSinceDtcClearedMinutes(), null, errores);//014E
                    _cvn = TryQuery<string>("CVN", () => {
                        var lista = elm.ReadCvns(); // List<string> o null
                        if (lista != null && lista.Count > 0)
                            return string.Join(" || ", lista);
                        return "DESCONOCIDO";
                    },
                        "DESCONOCIDO",
                        errores
                    );//0906

                    _cal = TryQuery<string[]>("CAL", () => elm.ReadCalibrationIds(), Array.Empty<string>(), errores);//0904

                    // Más valores instruidos por Toñin Cara de pan :D
                    _OperacionMotor = TryQuery<int?>("TiempoTotalSegundosOperacionMotor", () => elm.TiempoTotalSegundosOperacionMotor(), null, errores);//011F
                                                                                                                                                        //_WarmUpsDesdeBorrado = TryQuery<int?>("WarmUpsDesdeBorrado", () => elm.WarmUpsSinceCodesCleared(), null, errores);



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
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    _NormativaObdVehiculo = TryQuery<string?>("NORMATIVA_OBD_VEHICULO", () => elm.NormativaObdVehiculo(), null, errores);//011C
                    _IatCCoolantTempC = TryQuery<short?>("COOLANTTEMPC", () => elm.TemperaturaRefrigeranteC(), null, errores);//0105
                    _StftB1 = TryQuery<double?>("STFTB1", () => elm.StftBank1(), null, errores);
                    _LtftB1 = TryQuery<double?>("LTFTB1", () => elm.LtftBank1(), null, errores);
                    _IatC = TryQuery<short?>("IATC", () => elm.TemperaturaAireAdmisionC(), null, errores);
                    _MafGs = TryQuery<double?>("MAFGS", () => elm.FlujoAireMaf(), null, errores);//0110
                    _MafKgH = TryQuery<double?>("MAFKGH", () => elm.FlujoAireMafKgPorHora(), null, errores);
                    _Tps = TryQuery<double?>("TPS", () => elm.PosicionAcelerador(), null, errores);
                    _TimingAdvance = TryQuery<double?>("TIMING_ADVANCE", () => elm.AvanceEncendido(), null, errores);//010E
                    _O2S1_V = TryQuery<double?>("O2S1_V", () => elm.O2Sensor1Voltage(), null, errores);//0114
                    _O2S2_V = TryQuery<double?>("O2S2_V", () => elm.O2Sensor2Voltage(), null, errores);//0115
                    _FuelLevel = TryQuery<double?>("FUEL_LEVEL", () => elm.NivelCombustible(), null, errores);//012F
                    _BarometricPressure = TryQuery<short?>("BAROMETRIC_PRESSURE", () => elm.PresionBarometrica(), null, errores);//0133
                    _FuelType = TryQuery<string?>("FUEL_TYPE", () => elm.TipoCombustible(), null, errores);//0151
                    _IntFuelType = TryQuery<byte?>("INT_FUEL_TYPE", () => elm.byteTipoCombustible0151(), null, errores);//0151
                    _IntTipoCombustible0907 = TryQuery<byte?>("INT_TIPO_COMBUSTIBLE_0907", () => elm.intTipoCombustible0907(), null, errores);
                    _EcuAddress = TryQuery<string?>("ECU_ADDRESS", () => elm.EcuAddress(), null, errores);
                    //_EcuAddressInt = TryQuery<int?>("ECU_ADDRESS_INT", () => elm.EcuAddressInt(), null, errores);
                    _CCM = TryQuery<double?>("CCM", () => elm.LoadCalc(), null, errores); //0104
                    _EmissionCode = TryQuery<byte?>("EMISSION_CODE", () => elm.RequisitosEmisionesVehiculo(), null, errores);//015F
                    _Pids_01_20 = TryQuery<IReadOnlyList<int>>("PIDS_01_20", () => elm.PidsSoportadosBloque("0100", 0x01), Array.Empty<int>(), errores);
                    _Pids_21_40 = TryQuery<IReadOnlyList<int>>("PIDS_21_40", () => elm.PidsSoportadosBloque("0120", 0x21), Array.Empty<int>(), errores);
                    _Pids_41_60 = TryQuery<IReadOnlyList<int>>("PIDS_41_60", () => elm.PidsSoportadosBloque("0140", 0x41), Array.Empty<int>(), errores);



                    return new InspeccionObd2Set {
                        VehiculoId = _vin ?? "No se realizo lectura",
                        ProtocoloObd = _protocolo ?? "No se realizo lectura",
                        
                        CodigoError = _dtcList03 ?? "No se realizo lectura",
                        CodigoErrorPendiente = _dtcList07 ?? "No se realizo lectura",
                        CodigoErrorPermanente = _dtcList0A ?? "No se realizo lectura",
                        
                        Mil = _MilOn.HasValue ? (_MilOn.Value ? (byte)1 : (byte)0) : (byte)0,
                        Fallas = (byte)cnt03,
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
                        Sso = monitorCodes["OXYGEN_SENSOR_MONITORING"],         // Oxygen Sensor
                        Scso = monitorCodes["OXYGEN_SENSOR_HEATER_MONITORING"] ?? 0, // Oxygen Sensor Heater
                        
                        //*
                        LeeMonitores = false,
                        LeeDtc = _LeeDtcConfirmados,
                        LeeDtcPend = _LeeDtcPendientes,
                        LeeVin = _vinFromObd,
                        VoltsSwOn = _vOn.HasValue ? (decimal?)_vOn.Value : (decimal?)(-1m),          //
                        RpmOn = _rpm.HasValue ? (short?)_rpm.Value : (short?)(-1),              //
                        //*/

                        // NUEVOS VALORES :D
                        Dist_MIL_On = _distMilKm ?? -1,
                        Dist_Borrado_DTC = _distSinceClrKm ?? -1,
                        Tpo_Arranque = _OperacionMotor ?? -1,
                        Tpo_Borrado_DTC = _timeSinceClr ?? -1,
                        //NumVerifCalib = "",
                        IDs_Adic = string.Join(" || ", _cal) ?? "No se realizo lectura",
                        Lista_CVN = _cvn ?? "No se realizo lectura",


                        // Más valores instruidos por Toñin GALVAN 
                        CCM = _CCM.HasValue ? (decimal?)_CCM.Value : (decimal?)(-1m),
                        //WarmUpsDesdeBorrado = _WarmUpsDesdeBorrado,
                        //NEV = _NormativaObdVehiculo,
                        TR = _IatCCoolantTempC ?? 0,
                        STFT_B1 = _StftB1.HasValue ? (decimal?)_StftB1.Value : (decimal?)(-1m),
                        LTFT_B1 = _LtftB1.HasValue ? (decimal?)_LtftB1.Value : (decimal?)(-1m),
                        IAT = _IatC,
                        MAF = _MafGs.HasValue ? (decimal?)_MafGs.Value : (decimal?)(-1m),
                        //MafKgH = _MafKgH,
                        TPS = _Tps.HasValue ? (decimal?)_Tps.Value : (decimal?)(-1m),
                        AvanceEnc = _TimingAdvance.HasValue ? (decimal?)_TimingAdvance.Value : (decimal?)(-1m),
                        VelVeh = _vel ?? -1,
                        Volt_O2 = _O2S1_V.HasValue ? (decimal?)_O2S1_V.Value : (decimal?)(-1m),
                        //O2S2_V = _O2S2_V,
                        NivelComb = _FuelLevel.HasValue ? (decimal?)_FuelLevel.Value : (decimal?)(-1m),
                        Pres_Baro = _BarometricPressure ?? -1,
                        //FuelType = _FuelType,
                        Combustible0151Id = _IntFuelType ?? 0,
                        Combustible0907Id = _IntTipoCombustible0907 ?? 0,
                        Dir_ECU = _EcuAddress ?? "No se realizo lectura",
                        //EcuAddressInt = _EcuAddressInt,
                        Req_Emisiones = _EmissionCode ?? 0,

                        PIDS_Sup_01_20 = string.Join(" || ", _Pids_01_20),
                        PIDS_Sup_21_40 = string.Join(" || ", _Pids_21_40),
                        PIDS_Sup_41_60 = string.Join(" || ", _Pids_41_60)

                        //*/
                    };
                }
            } catch (Exception ex) {
                mensaje = ex.Message;
                SivevLogger.Error($"Falló en la lectura de OBD {ex.Message}");
            }
            return new InspeccionObd2Set();
        }



        public TryHandshakeGet TryHandshake(int maxTries = 3, int settleMs = 150) {
            string mensaje = "";
            var errores = new Dictionary<string, string>();

            for (int intento = 1; intento <= maxTries; intento++) {
                try {
                    using (var elm = new Elm327(portName: _port, baud: _baud, readTimeoutMs: _readTimeoutMs, writeTimeoutMs: _writeTimeoutMs)) {
                        elm.Open();
                        elm.Initialize(showHeaders: false);

                        var proto = elm.WaitAndGetProtocolText(maxTries: 3, settleMs: settleMs);

                        var rpm = TryQuery<int?>("RPM", () => elm.ReadRpm(), null, errores);//010C
                        var vOff = TryQuery<double?>("VOLTAGE", () => elm.ReadVoltage(), null, errores);

                        bool protoOk = !string.IsNullOrWhiteSpace(proto) &&
                               !proto.Contains("(A0", StringComparison.OrdinalIgnoreCase);

                        bool ecuOk = rpm.HasValue;

                        byte conexion = (byte)((protoOk && ecuOk) ? 1 : 0);

                        // Si falló, intenta de nuevo
                        if (conexion == 0) {
                            mensaje = $"Handshake fallido (intento {intento}/{maxTries}). Proto='{proto}'.";
                            continue;
                        }
                        return new TryHandshakeGet {
                            ProtocoloObd = proto,
                            Intentos = (byte)intento,
                            ConexionOb = conexion,
                            VoltsSwOff = vOff.HasValue ? (decimal)vOff.Value : 0m,
                            RpmOff = rpm.HasValue ? (short)rpm.Value : (short)0,
                            Mensaje = ""
                        };
                    }
                } catch (Exception ex) {
                    mensaje = $"Handshake excepción (intento {intento}/{maxTries}): {ex.Message}";
                }
            }
            return new TryHandshakeGet {
                ProtocoloObd = "DESCONOCIDO",
                Intentos = (byte)maxTries,
                ConexionOb = 0,
                VoltsSwOff = 0m,
                RpmOff = 0,
                Mensaje = mensaje
            };
        }

        #endregion

        #region utils

        private string LeerYUnirDtcs(string nombreLectura, Func<List<string>?> lector, Dictionary<string, string> errores, out int cantidad) {
            var lista = TryQuery<List<string>?>(nombreLectura, lector, null, errores) ?? new List<string>();
            cantidad = lista.Count;

            return lista.Count > 0 ? string.Join(" || ", lista) : "";
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
