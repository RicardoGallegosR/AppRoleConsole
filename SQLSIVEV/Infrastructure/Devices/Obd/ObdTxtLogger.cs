using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLSIVEV.Infrastructure.Devices.Obd.Elm327;
using Ionic.Zip;

namespace SQLSIVEV.Infrastructure.Devices.Obd {
    public class ObdTxtLogger : IObdLogger {
        private readonly string _ruta;

        private readonly string _passwordZip;

        public ObdTxtLogger(string ruta, string passwordZip) {
            _ruta = ruta;
            _passwordZip = passwordZip;
            var dir = Path.GetDirectoryName(_ruta);
            if (!string.IsNullOrWhiteSpace(dir))
                Directory.CreateDirectory(dir);
        }
        public void EncabezadoSesion(string verificacionId, string placa) {
            File.AppendAllText(_ruta,
        $@"=================================================
SESIÓN OBD-II
=================================================
Fecha.............: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
VerificacionId....: {verificacionId}
Placa.............: {placa}
=================================================

");
        }
        public void EncabezadoLectura(string vin, string protocolo, string puerto) {
            File.AppendAllText(_ruta,
        $@"
=================================================
=================================================
VIN...............: {vin}
Puerto............: {puerto}
Protocolo.........: {protocolo}
=================================================

");
        }
        public void ResumenDtc(string raw03, string dec03, string raw07, string dec07, string raw0A, string dec0A) {
            File.AppendAllText(_ruta, $@"

=================================================
RESUMEN DTC
=================================================

Modo 03 - Confirmados
RAW..........: {raw03}
DECODIFICADO.: {dec03}

Modo 07 - Pendientes
RAW..........: {raw07}
DECODIFICADO.: {dec07}

Modo 0A - Permanentes
RAW..........: {raw0A}
DECODIFICADO.: {dec0A}

=================================================

");
        }

        public void Info(string mensaje) {
            File.AppendAllText(_ruta,
                $"[INFO ] {mensaje}{Environment.NewLine}");
        }
        public void ComprimirZip() {
            if (!File.Exists(_ruta))
                return;

            string zipPath = Path.ChangeExtension(_ruta, ".zip");

            using (var zip = new Ionic.Zip.ZipFile()) {
                zip.Password = _passwordZip;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;

                zip.AddFile(_ruta, "");

                zip.Save(zipPath);
            }

            // Si todo salió bien
            File.Delete(_ruta);
        }

        public void RawTx(string comando) {
            File.AppendAllText(_ruta,
                $"[TX    ] {comando}{Environment.NewLine}");
        }

        public void RawRx(string respuesta) {
            File.AppendAllText(_ruta,
                $"[RX    ] {respuesta}{Environment.NewLine}");
        }

        public void Error(string mensaje, Exception? ex = null) {
            File.AppendAllText(_ruta,
                $"[ERROR ] {mensaje} {ex?.Message}{Environment.NewLine}");
        }

        public static void LimpiarLogsAntiguos(string rutaBase, int diasConservar = 7) {
            try {
                if (!Directory.Exists(rutaBase))
                    return;

                var limite = DateTime.Now.AddDays(-diasConservar);

                var archivos = Directory.GetFiles(rutaBase, "*.*", SearchOption.AllDirectories);

                foreach (var archivo in archivos) {
                    try {
                        var fecha = File.GetCreationTime(archivo);

                        if (fecha < limite)
                            File.Delete(archivo);
                    } catch {
                        // ignorar archivo bloqueado
                    }
                }

                // eliminar carpetas vacías
                foreach (var dir in Directory.GetDirectories(
                    rutaBase,
                    "*",
                    SearchOption.AllDirectories)) {
                    try {
                        if (!Directory.EnumerateFileSystemEntries(dir).Any())
                            Directory.Delete(dir);
                    } catch {
                    }
                }
            } catch {
            }
        }
    }
}
