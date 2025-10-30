using System;
using System.Diagnostics;
using System.Management; 
using System.Reflection;
using System.Runtime.InteropServices;    
using System.Text;

namespace SQLSIVEV.Infrastructure.SystemInfo {
    public sealed class DatosCpuGet : IDisposable {
        private bool _disposed;

        public string DescripcionError { get; private set; } = string.Empty;
        public string QueryConsulta { get; private set; } = string.Empty;

        // Buffers para GetVolumeInformation
        private readonly StringBuilder _volumeName = new(255);
        private readonly StringBuilder _fsName     = new(255);

        // Ojo: especificamos el tipo completo para evitar conflicto con System.IO.EnumerationOptions
        private readonly System.Management.EnumerationOptions _wmiOptions =
            new System.Management.EnumerationOptions
            {
                UseAmendedQualifiers = true,
                ReturnImmediately = true,
                Rewindable = false,
                Timeout = TimeSpan.FromSeconds(5)
            };

        public DatosCpuGet() {
            _disposed = false;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool GetVolumeInformation(
            string lpRootPathName,
            StringBuilder lpVolumeNameBuffer, int nVolumeNameSize,
            out uint lpVolumeSerialNumber,
            out uint lpMaximumComponentLength,
            out uint lpFileSystemFlags,
            StringBuilder lpFileSystemNameBuffer, int nFileSystemNameSize);

        /// <summary>
        /// Obtiene versión de la app, IdentifyingNumber (WMI) y el serial de volumen (C:).
        /// </summary>
        public bool DatosC(ref string versionApp, ref string idEquipo, ref string serieDisco) {
            try {
                // Versión de la app
                var asm = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                versionApp = fvi.FileVersion ?? asm.GetName().Version?.ToString() ?? "0.0.0.0";

                // UID del equipo via WMI
                idEquipo = InfoUID(); // usa _wmiOptions internamente

                // Serial de volumen C:
                if (!TryGetVolumeSerial(@"C:\", out var serial, out _, out _, out var lastError)) {
                    DescripcionError = $"GetVolumeInformation falló. Error={lastError}";
                    serieDisco = string.Empty;
                    return false;
                }

                serieDisco = serial.ToString();
                return true;
            } catch (Exception ex) {
                DescripcionError = ex.Message;
                return false;
            }
        }

        // Variante que devuelve DTO (opcional)
        public DatosCpuResult? GetDatosCpu() {
            string version = "", uid = "", serie = "";
            if (!DatosC(ref version, ref uid, ref serie))
                return null;

            return new DatosCpuResult(version, uid, serie);
        }

        private bool TryGetVolumeSerial(
            string rootPath,
            out uint serial,
            out string fileSystemName,
            out string volumeName,
            out int lastError) {
            _volumeName.Clear();
            _fsName.Clear();

            var ok = GetVolumeInformation(
                rootPath,
                _volumeName, _volumeName.Capacity,
                out serial,
                out var _,
                out var _,
                _fsName, _fsName.Capacity);

            if (!ok) {
                lastError = Marshal.GetLastWin32Error();
                volumeName = "";
                fileSystemName = "";
                return false;
            }

            lastError = 0;
            volumeName = _volumeName.ToString();
            fileSystemName = _fsName.ToString();
            return true;
        }

        private string InfoUID() {
            // Scope no es IDisposable
            var scope = new ManagementScope(@"\\.\root\cimv2");
            scope.Connect();

            const string query = "SELECT IdentifyingNumber FROM Win32_ComputerSystemProduct";
            using var searcher = new ManagementObjectSearcher(scope, new ObjectQuery(query), _wmiOptions);
            using ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject mo in results) {
                var val = mo["IdentifyingNumber"];
                if (val != null) return Convert.ToString(val)?.Trim() ?? string.Empty;
            }
            return string.Empty;
        }

        public void Dispose() {
            if (_disposed) return;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    public sealed record DatosCpuResult(string VersionApp, string IdEquipo, string SerieDisco);
}
