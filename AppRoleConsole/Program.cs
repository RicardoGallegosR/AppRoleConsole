using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;


class Program {

    static string SERVER = AppConfig.Sql.SqlServerName();
    static string DB = AppConfig.Sql.BaseSql();
    static string SQL_USER = AppConfig.Sql.SQL_USER();
    static string SQL_PASS = AppConfig.Sql.SQL_PASS();

    static string APPROLE = AppConfig.RollInicial.APPROLE();
    static string APPROLE_PASS = AppConfig.RollInicial.APPROLE_PASS();

    public static async Task<int> Main() {

        //string RollAcceso = "RollVfcVisual";
        //string ClaveAcceso = "95801B7A-4577-A5D0-952E-BD3D89757EA5";
        
        string RollAcceso = string.Empty;
        string ClaveAcceso = string.Empty;
        var conf = new RegWin();
        var appName = string.IsNullOrWhiteSpace(conf.DomainName) ? "" : conf.DomainName.Trim();
        int MensajeId = 0;

        Guid accesoId = Guid.Empty;
        Guid VerificacionId = Guid.Empty;
        byte ProtocoloVerificacionId = 0;
        string PlacaId = string.Empty;
        byte combustible = 1;
        Guid estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");

        short opcionMenu = 151;
        int credencial = 16499;
        string passCredencial = "PASS1234";


        byte tiTaponCombustible = 0;
        byte tiTaponAceite = 0;
        byte tiBayonetaAceite = 0;
        byte tiPortafiltroAire = 0;
        byte tiTuboEscape = 0;
        byte tiFugasMotorTrans = 0;
        byte tiNeumaticos = 0;
        byte tiComponentesEmisiones = 0;
        byte tiMotorGobernado = 0;
        int odometro = 0;


        var repo = new SivevRepository();

        using var conn = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
        conn.Open();

        var repo2 = new SivevRepository();

        using (var scope2 = new AppRoleScope(conn, APPROLE, APPROLE_PASS.ToString())) {
            var r = repo2.SpAppRollClaveGet(conn);
            await repo.PrintIfMsgAsync(conn, "Fallo en SpAppRollClaveGet", r.MensajeId);
            if (r.MensajeId != 0) {
                return 0;
            }
            var invertida = new string((r.ClaveAcceso ?? "").Reverse().ToArray());
            ClaveAcceso = invertida;
            RollAcceso = r.FuncionAplicacion;
        }
        conn.Close();
           


        /*---------------------------VALIDA BITACORA----------------------------------------------------------------
         */
        using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
        await connApp.OpenAsync();

        using var scope = new AppRoleScope(connApp, RollAcceso, ClaveAcceso); 

        try {
            // Abrir acceso
            var rinicial = repo.SpAppCredencialExisteHuella(cnn:connApp,uiEstacionId: estacionId, siOpcionMenuId:opcionMenu,iCredencial:credencial);
            await repo.PrintIfMsgAsync(connApp, "Fallo en SpAppCredencialExisteHuella", rinicial.MensajeId);

            if (rinicial.Resultado != 0) {
                return 0;
            }


            var r = await repo.SpAppAccesoIniciaAsync( conn:connApp, estacionId:estacionId, opcionMenuId:opcionMenu, credencial: credencial, password: passCredencial, huella: rinicial.Huella);
            await repo.PrintIfMsgAsync(connApp, "Fallo en SpAppAccesoIniciaAsync", r.MensajeId);


            accesoId = r.AccesoId;

            // busca verificaciones en procesoid = 4
            /*---------------------------VALIDA SpAppVerificacionVisualIni ----------------------------------------------------------------
         */
            var r2 = await repo.SpAppVerificacionVisualIniAsync(conn:connApp, estacionId: estacionId, accesoId:accesoId);
  
            VerificacionId = r2.VerificacionId;
            ProtocoloVerificacionId = r2.ProtocoloVerificacionId;
            PlacaId = r2.PlacaId;   

            if (r2.Resultado != 0) {
                return 0;
            }




            // proceso prueba Id = 5
            /*---------------------------VALIDA SpAppCapturaVisualGetAsync ----------------------------------------------------------------
         */
            var r3 = await repo.SpAppCapturaVisualGetAsync(conn:connApp,estacionId: estacionId,accesoId:accesoId,verificacionId:VerificacionId,elemento:"DESCONOCIDO", tiCombustible:combustible);
            await repo.PrintIfMsgAsync(connApp, $"Fallo en SpAppCapturaVisualGetAsync resultado {r3.Resultado}", r3.MensajeId);

            /* IMPRIME LAS COSAS DE TOÑO
            Console.WriteLine($"\n--- CapturaVisualGet: {r3.Items.Count} item(s) ---");
            foreach (var it in r3.Items) {
                Console.WriteLine($"Id={it.CapturaVisualId} | Elemento='{it.Elemento}' | Despliegue={(it.Despliegue ? "Sí" : "No")}");
            }
            */

            var r4 = await repo.SpAppCapturaInspeccionVisualNewSetAsync(conn:connApp, verificacionId: VerificacionId, estacionId:estacionId, accesoId:accesoId,
               tiTaponCombustible:tiTaponCombustible, tiTaponAceite: tiTaponAceite, tiBayonetaAceite: tiBayonetaAceite, tiPortafiltroAire: tiPortafiltroAire,
               tiTuboEscape: tiTuboEscape, tiFugasMotorTrans: tiFugasMotorTrans, tiNeumaticos: tiNeumaticos, tiComponentesEmisiones: tiComponentesEmisiones, tiMotorGobernado:tiMotorGobernado, 
               odometro:odometro);








            await repo.PrintIfMsgAsync(connApp, $"Fallo en SpAppCapturaInspeccionVisualNewSetAsync resultado {r4.Resultado}", r4.MensajeId);

            Console.WriteLine($"pCheckObd: {r4.CheckObd}");


            var rObd = await repo.SpAppCapturaInspeccionObdSetAsync(
                        conn: connApp, estacionId: estacionId, accesoId: accesoId, verificacionId: VerificacionId,
                        vehiculoId: "DESCONOCIDO", tiConexionObd: 1, protocoloObd: "ISO 15765-4 CAN 11/500",  tiIntentos: 1, tiMil: 0, 
                        siFallas: 0, codError: "", codErrorPend: "", tiSdciic: 1, tiSecc: 1, tiSc: 1, tiSso: 1, tiSci: 1, tiSccc: 0,
                        tiSe: 0, tiSsa: 0, tiSfaa: 0, tiScso: 0, tiSrge: 0, voltsSwOff: 12.6m, voltsSwOn: 12.2m, rpmOff: 0, rpmOn: 820, rpmCheck: 800,
                        leeMonitores: true, leeDtc: true, leeDtcPend: true, leeVin: true,  codigoProtocolo: 15765);

            await repo.PrintIfMsgAsync(connApp, "SpAppCapturaInspeccionObdSetAsync", rObd.MensajeId);

            if (!rObd.Ok) {
                if (rObd.MensajeId == 0)
                    Console.WriteLine($"No OK: Resultado={rObd.Resultado}, Return={rObd.ReturnCode}");
                return 0;
            }
















        } catch (Exception ex) {
            Console.WriteLine($"Excepción: {ex.Message}");
        } finally {
            if (accesoId != Guid.Empty) {
                var fin = await repo.SpAppAccesoFinAsync(connApp, estacionId, accesoId);
                //Console.WriteLine($"[Fin Acceso] Resultado={fin.Resultado} MensajeId={fin.MensajeId} Return={fin.ReturnCode}");
            }
        }
        return 0;
    }
}
