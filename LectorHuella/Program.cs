using System;
using System.IO;
using System.Threading;
using DPFP;
using DPFP.Capture;
using DPFP.Processing;
using DPFP.Verification;

namespace ConsoleDPFP {
    class Program : DPFP.Capture.EventHandler {
        // --- Estado general ---
        private static Program _instance;
        private static AutoResetEvent _waitHandle = new AutoResetEvent(false);

        // --- DPFP objetos ---
        private Capture _capture;                   // capturador (lector)
        private Enrollment _enroller;               // enrolamiento (varias muestras -> Template)
        private Verification _verifier;             // verificación (features vs Template)
        private Template _template;                 // template cargado/enrolado

        // --- Modo actual ---
        private bool _modeEnroll = false;
        private bool _captureStarted = false;

        static void Main() {
            _instance = new Program();

            Console.WriteLine("=== DigitalPersona (Consola) - Enrolar / Verificar ===");
            Console.WriteLine("Requisitos: compilar x86, drivers instalados, DLLs de DPFP referenciados.\n");

            while (true) {
                Console.WriteLine("Menú:");
                Console.WriteLine("  1) Enrolar");
                Console.WriteLine("  2) Verificar (requiere template.bin o elegir ruta)");
                Console.WriteLine("  3) Cargar template de archivo");
                Console.WriteLine("  4) Guardar template a archivo");
                Console.WriteLine("  0) Salir");
                Console.Write("Opción: ");
                var key = Console.ReadLine();

                switch (key) {
                    case "1":
                        _instance.StartEnroll();
                        break;
                    case "2":
                        _instance.StartVerify();
                        break;
                    case "3":
                        _instance.LoadTemplateFromFile();
                        break;
                    case "4":
                        _instance.SaveTemplateToFile();
                        break;
                    case "0":
                        _instance.StopCapture();
                        return;
                    default:
                        Console.WriteLine("Opción no válida.\n");
                        break;
                }
            }
        }

        public Program() {
            _enroller = new Enrollment();
            _verifier = new Verification();
        }

        // =========================
        //   CAPTURA: START / STOP
        // =========================
        private void StartCapture() {
            if (_captureStarted) return;

            try {
                _capture = new Capture();
                _capture.EventHandler = this;
                _capture.StartCapture();
                _captureStarted = true;
                Log("Captura iniciada. Presente el dedo en el lector…");
            } catch (Exception ex) {
                Log("ERROR al iniciar captura: " + ex.Message);
            }
        }

        private void StopCapture() {
            try {
                if (_capture != null) {
                    _capture.StopCapture();
                    _capture.EventHandler = null;
                }
            } catch (Exception ex) {
                Log("ERROR al detener captura: " + ex.Message);
            } finally {
                _captureStarted = false;
                Log("Captura detenida.\n");
            }
        }

        // =========================
        //   ENROLAR / VERIFICAR
        // =========================
        private void StartEnroll() {
            _modeEnroll = true;
            _enroller = new Enrollment();
            Log($"[ENROLAR] Se requieren muestras: {_enroller.FeaturesNeeded}");
            StartCapture();

            // Esperar hasta que termine (Ready/Failed) o el usuario interrumpa con Ctrl+C
            Console.CancelKeyPress += (_, e) => { e.Cancel = true; _waitHandle.Set(); };
            _waitHandle.WaitOne();
        }

        private void StartVerify() {
            if (_template == null) {
                // intentar cargar un template por defecto
                var def = Path.Combine(AppContext.BaseDirectory, "template.bin");
                if (File.Exists(def)) {
                    _template = new Template();
                    using var fs = File.OpenRead(def);
                    _template.DeSerialize(fs);
                    Log("Template cargado por defecto: template.bin");
                } else {
                    Console.WriteLine("No hay template en memoria. Carga uno primero (opción 3).");
                    return;
                }
            }

            _modeEnroll = false;
            Log("[VERIFICAR] Presente un dedo en el lector…");
            StartCapture();

            Console.CancelKeyPress += (_, e) => { e.Cancel = true; _waitHandle.Set(); };
            _waitHandle.WaitOne();
        }

        // =========================
        //   EVENTOS DEL LECTOR
        // =========================
        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample) {
            try {
                var featuresEnroll = ExtractFeatures(Sample, DataPurpose.Enrollment);
                var featuresVerify = ExtractFeatures(Sample, DataPurpose.Verification);

                if (featuresEnroll == null || featuresVerify == null) {
                    Log("Muestra con calidad insuficiente. Reintente.");
                    return;
                }

                if (_modeEnroll) {
                    _enroller.AddFeatures(featuresEnroll);
                    Log($"[ENROLAR] Aún faltan: {_enroller.FeaturesNeeded}");

                    if (_enroller.TemplateStatus == Enrollment.Status.Ready) {
                        _template = _enroller.Template;
                        Log("[ENROLAR] COMPLETADO. Template en memoria.");
                        StopCapture();
                        _waitHandle.Set();
                    } else if (_enroller.TemplateStatus == Enrollment.Status.Failed) {
                        _enroller = new Enrollment();
                        Log("[ENROLAR] Falló. Reiniciado.");
                    }
                } else {
                    var result = new DPFP.Verification.Verification.Result();
                    _verifier.Verify(featuresVerify, _template, ref result);

                    if (result.Verified) {
                        Log($"[VERIFICAR] COINCIDE ✅  (FAR Achieved={result.FARAchieved})");
                    } else {
                        Log($"[VERIFICAR] NO coincide ❌ (FAR Achieved={result.FARAchieved})");
                    }
                    StopCapture();
                    _waitHandle.Set();
                }
            } catch (Exception ex) {
                Log("ERROR en OnComplete: " + ex.Message);
            }
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber) => Log("Dedo detectado.");
        public void OnFingerGone(object Capture, string ReaderSerialNumber) => Log("Dedo retirado.");
        public void OnReaderConnect(object Capture, string ReaderSerialNumber) => Log("Lector conectado.");
        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber) => Log("Lector desconectado.");
        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
            => Log("Calidad de muestra: " + CaptureFeedback);

        // =========================
        //   EXTRACCIÓN DE FEATURES
        // =========================
        private FeatureSet ExtractFeatures(Sample sample, DataPurpose purpose) {
            var extractor = new FeatureExtraction();
            var feedback = CaptureFeedback.None;
            var features = new FeatureSet();

            try {
                extractor.CreateFeatureSet(sample, purpose, ref feedback, ref features);
            } catch (Exception ex) {
                Log("ERROR extrayendo features: " + ex.Message);
                return null;
            }
            return (feedback == CaptureFeedback.Good) ? features : null;
        }

        // =========================
        //   GUARDAR / CARGAR TEMPLATE
        // =========================
        private void SaveTemplateToFile() {
            try {
                if (_template == null) {
                    Console.WriteLine("No hay template en memoria. Enrola primero.");
                    return;
                }
                Console.Write("Ruta archivo (ENTER = template.bin): ");
                var path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                    path = Path.Combine(AppContext.BaseDirectory, "template.bin");

                using var fs = File.Create(path);
                _template.Serialize(fs);
                Log("Template guardado en: " + path);
            } catch (Exception ex) {
                Log("ERROR guardando template: " + ex.Message);
            }
        }

        private void LoadTemplateFromFile() {
            try {
                Console.Write("Ruta archivo (ENTER = template.bin): ");
                var path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                    path = Path.Combine(AppContext.BaseDirectory, "template.bin");

                if (!File.Exists(path)) {
                    Console.WriteLine("No existe el archivo: " + path);
                    return;
                }

                var t = new Template();
                using var fs = File.OpenRead(path);
                t.DeSerialize(fs);

                _template = t;
                Log("Template cargado: " + path);
            } catch (Exception ex) {
                Log("ERROR cargando template: " + ex.Message);
            }
        }

        // =========================
        //   LOG CONSOLA
        // =========================
        private static void Log(string msg) {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} | {msg}");
        }
    }
}
