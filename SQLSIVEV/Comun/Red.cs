using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Comun {
    public static class Red {
        public static string ObtenerIP192ServerPrincipal() {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces()) {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback) {
                    var ipProps = ni.GetIPProperties();
                    foreach (var ip in ipProps.UnicastAddresses) {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) {
                            string direccion = ip.Address.ToString();

                            if (direccion.StartsWith("192.168.")) {
                                var partes = direccion.Split('.');

                                // Construir la IP terminando en .1
                                return $"{partes[0]}.{partes[1]}.{partes[2]}.1";
                            }
                        }
                    }
                }
            }
            return "No se encontró IP 192.168";
        }

        public static string ObtenerIP192PC() {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces()) {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback) {
                    var ipProps = ni.GetIPProperties();

                    foreach (var ip in ipProps.UnicastAddresses) {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) {
                            string direccion = ip.Address.ToString();

                            if (direccion.StartsWith("192.168.")) {
                                return direccion;
                            }
                        }
                    }
                }
            }

            return "No se encontró IP 192.168";
        }
    }
}
