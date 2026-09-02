using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Utils;
using SQLSIVEV.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Comun.Configuracion {
    public class ConfiguracionCaptura {
        private const string Origen = "CAPTURA";
        Regedit regedit = new Regedit(origen: Origen);

        public  CapturaRegistroWindows Cargar() {
            var lector = new GuardarWinRarConf();
            lector.CargarEnCryptoHelper();
            var conf = lector.GetConfig();
            CryptoHelper.Configurar(conf);

            CapturaRegistroWindows capturaCore = new() {
                dvar1 = regedit.Leer("Server"),
                dvar2 = regedit.Leer("Database"),
                dvar3 = regedit.Leer("User"),
                dvar4 = regedit.Leer("Password"),
                dvar5 = regedit.Leer("AppName"),
                dvar6 = regedit.Leer("AppRole"),
                dvar7 = regedit.LeerGuid("AppRolePassword"),
                dvar8 = regedit.LeerShort("OpcionMenuId", 0),
                dvar9 = regedit.LeerBool("Relleno", false),
                dvar10 = regedit.Leer("UsuarioLinea"),
                dvar11 = regedit.Leer("Ip"),
                dvar12 = regedit.LeerShort("Centro", 0),
                dvar13 = regedit.Leer("ServidorVersionesControlador"),
                dvar14 = regedit.Leer("url"),
                dvar15 = regedit.LeerGuid("EstacionId"),
                dvar26 = regedit.LeerBool("v26")
            };
            RegistrarConfiguracion(capturaCore);
            return capturaCore;
        }


        public static bool EsValida(VisualRegistroWindows config) {
            bool Vacio(string? valor) =>
                string.IsNullOrWhiteSpace(valor);
            return
                !Vacio(config.dvar1) &&
                !Vacio(config.dvar2) &&
                !Vacio(config.dvar3) &&
                !Vacio(config.dvar4) &&
                !Vacio(config.dvar5) &&
                !Vacio(config.dvar6) &&
                config.dvar7 != Guid.Empty &&
                config.dvar8 > 0 &&
                config.dvar15 != Guid.Empty;
        }


        private void RegistrarConfiguracion( CapturaRegistroWindows config) {
            SivevLogger.Information(
                $"|| Lectura REGEDIT " +
                $"|| SERVER: {config.dvar1}, " +
                $"|| DB: {config.dvar2}, " +
                $"|| SQL_USER: {config.dvar3}, " +
                $"|| APPNAME: {config.dvar5}, " +
                $"|| APPROLE: {config.dvar6}, " +
                $"|| OpcionMenu: {config.dvar8}, " +
                $"|| EstacionId: {config.dvar15}"
            , origen: Origen);
        }
    }
}
