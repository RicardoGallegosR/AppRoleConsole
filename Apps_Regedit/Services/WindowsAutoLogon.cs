using Microsoft.Win32;
using System.Net.NetworkInformation;

namespace Apps_Regedit.Services {
    internal class WindowsAutoLogon {
        private const string Ruta = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";

        public static void Configurar(string usuario, string password) {

            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("El usuario de línea no puede estar vacío.",  nameof(usuario));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña del usuario de línea no puede estar vacía.", nameof(password));

            string dominio = ObtenerDominioEquipo();
            string usuarioCompleto = usuario.Contains('\\') ? usuario : $@"{dominio}\{usuario}";

            using RegistryKey? key = Registry.LocalMachine.OpenSubKey(Ruta, writable: true);

            if (key is null)
                throw new InvalidOperationException("No fue posible abrir la configuración de inicio de sesión de Windows.");

            key.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);
            key.SetValue("DefaultUserName", usuarioCompleto, RegistryValueKind.String);
            key.SetValue("DefaultPassword", password, RegistryValueKind.String);
            key.SetValue("DefaultDomainName", dominio, RegistryValueKind.String);
            
        }
        private static string ObtenerDominioEquipo() {
            string dominio = IPGlobalProperties.GetIPGlobalProperties().DomainName;

            if (string.IsNullOrWhiteSpace(dominio))
                throw new InvalidOperationException("El equipo no tiene un dominio configurado.");

            return dominio;
        }
    }
}
