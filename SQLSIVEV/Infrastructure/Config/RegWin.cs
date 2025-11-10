using DotNetEnv;
using Microsoft.Win32;
using SQLSIVEV.Infrastructure.Config.Cifrados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQLSIVEV.Infrastructure.Config {
    public class RegWin : IDisposable {
        protected bool disposed = false;
        private const string SubKey = @"SOFTWARE\SIVEV";
        private RegistryKey? RegKey;

        private readonly EncryptConfig _enc; 



        #region Datos del Sistema
        internal static string mvarCentro = "";
        internal static string mvarEstacion = "";
        internal static string mvarCpuName = "";
        internal static string mvarDomainName = "";
        internal static string mvarIpAddress = "";
        internal static string mvarNumeroCaja = "";
        #endregion

        #region Configuración de SQL
        internal static string mvarSqlExpressName = "";
        internal static string mvarBaseSqlExpress = "";
        internal static string mvarSqlServerName = "";
        internal static string mvarBaseSql = "";
        internal static string mvarEstacionId = "";
        #endregion

        #region Configuración de Puertos
        internal static string mvarMsmPtoNum = "";
        internal static string mvarMcepPtoNum = "";
        internal static string mvarMcsPtoNum = "";
        internal static string mvarMemPtoNum = "";
        internal static string mvarMctPtoNum = "";
        internal static string mvarOpacimetroPtoNum = "";
        internal static string mvarBitsDatos = "";
        internal static string mvarBitsParidad = "";
        internal static string mvarBitsParada = "";
        internal static string mvarControlFlujo = "";
        internal static string mvarVelocidadPuerto = "";
        #endregion

        #region Configuración del Sistema
        internal static string mvarOpcionMenuId = "";
        internal static string mvarAccesoId = "";
        internal static string mvarModeloDinamometroId = "";
        #endregion

        #region Confirmaciones del Sistema
        internal static string mvarConfirma1 = "";
        internal static string mvarConfirma2 = "";
        internal static string mvarConfirma3 = "";
        internal static string mvarConfirma4 = "";
        internal static string mvarConfirma5 = "";
        internal static string mvarConfirma6 = "";
        internal static string mvarConfirma7 = "";
        internal static string mvarConfirmaRechazo = "";
        internal static string mvarConfirmaDos = "";
        internal static string mvarConfirmaCero = "";
        internal static string mvarConfirmaDobleCero = "";
        #endregion

        #region Configuración de Depuración
        internal static string mvarDebugActivo = "";
        internal static string mvarNivelDebug = "";
        internal static string mvarConectaSF = "";
        #endregion

        #region Seguridad (Cifrado)

        private readonly string saltValue;
        private readonly string hashAlgorithm;
        private readonly int passwordIterations;
        private readonly string initVector;
        private readonly int keySize;


        #endregion

        public delegate void ErrorProcesoEventHandler(ErrorProcesoArgs args);
        public event ErrorProcesoEventHandler? ErrorProceso;

        #region Desencriptar
        public string Desencriptacion(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize) {
            string result;
            try {
                byte[] iv = Encoding.ASCII.GetBytes(initVector);
                byte[] salt = Encoding.ASCII.GetBytes(saltValue);
                byte[] array = Convert.FromBase64String(cipherText);
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt, passwordIterations, HashAlgorithmName.SHA1);
                byte[] key = rfc2898DeriveBytes.GetBytes(keySize / 8);

                using (Aes aes = Aes.Create()) {
                    aes.Mode = CipherMode.CBC;
                    aes.Key = key;
                    aes.IV = iv;

                    using (ICryptoTransform transform = aes.CreateDecryptor())
                    using (MemoryStream memoryStream = new MemoryStream(array))
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read)) {
                        byte[] plainTextBytes = new byte[array.Length];
                        int count = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        result = Encoding.UTF8.GetString(plainTextBytes, 0, count);
                    }
                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                "SivevLib",                 //Libreria
                nameof(RegWin),             //Clase 
                nameof(Desencriptacion),    //Metodo
                ex.HResult,                 //ErrorNum
                ex.Message,                 //ErrorDesc
                0                           //SqlErr
            ));
                return default!;
            }
            return result;
        }

        #endregion

        #region Encriptar
        public string Encriptacion(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize) {
            string result;
            try {
                byte[] iv = Encoding.ASCII.GetBytes(initVector);
                byte[] salt = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using var rfc = new Rfc2898DeriveBytes(passPhrase, salt, passwordIterations, HashAlgorithmName.SHA1);
                byte[] key = rfc.GetBytes(keySize / 8);

                using (Aes aes = Aes.Create()) {
                    aes.Mode = CipherMode.CBC;
                    aes.Key = key;
                    aes.IV = iv;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (MemoryStream memoryStream = new MemoryStream())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        byte[] cipherTextBytes = memoryStream.ToArray();
                        result = Convert.ToBase64String(cipherTextBytes);
                    }
                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                "SivevLib",                 //Libreria
                nameof(RegWin),             //Clase 
                nameof(Encriptacion),       //Metodo
                ex.HResult,                 //ErrorNum
                ex.Message,                 //ErrorDesc
                0                           //SqlErr
            ));

                return default!;
            }

            return result;
        }
        #endregion

        #region LeerValor


        #endregion

        #region EscribirValor

        private void EscribirValor<T>(string nombrePropiedad, T valorOriginal) {
            try {
                string valorCifrado = this.Encriptacion(
                    valorOriginal?.ToString() ?? "",
                    nombrePropiedad,
                    this.saltValue,
                    this.hashAlgorithm,
                    this.passwordIterations,
                    this.initVector,
                    this.keySize
                );
                Console.WriteLine($"nombre de la propiedad {nombrePropiedad}, Valor {valorOriginal}");
                this.GuardaValor(SubKey, nombrePropiedad, valorCifrado);
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(EscribirValor),
                    ex.HResult,
                    ex.Message,
                    0
                ));
            }
        }



        public T LeerValor<T>(string nombrePropiedad) {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(SubKey);
                var rawObj = key?.GetValue(nombrePropiedad);
                string? raw = rawObj is byte[] b ? Encoding.UTF8.GetString(b) : rawObj?.ToString();

                if (string.IsNullOrWhiteSpace(raw)) return default!;

                // Intenta descifrar; si falla, usa raw (compatibilidad)
                string plano;
                try {
                    plano = this.Desencriptacion(raw, nombrePropiedad, this.saltValue, this.hashAlgorithm,
                                                 this.passwordIterations, this.initVector, this.keySize);
                    if (string.IsNullOrWhiteSpace(plano)) plano = raw;
                } catch {
                    plano = raw;
                }

                if (typeof(T) == typeof(Guid))
                    return (T)(object)(Guid.TryParse(plano, out var g) ? g : Guid.Empty);

                if (typeof(T) == typeof(string)) return (T)(object)plano;
                if (typeof(T) == typeof(int)) return (T)(object)int.Parse(plano);
                if (typeof(T) == typeof(short)) return (T)(object)short.Parse(plano);
                if (typeof(T) == typeof(bool)) return (T)(object)bool.Parse(plano);

                return (T)Convert.ChangeType(plano, typeof(T));
            } catch {
                return default!;
            }
        }




        private bool GuardaValor(string LLave, string Nombre, object Valor) {
            bool flag = false;
            try {
                this.RegKey = Registry.LocalMachine.OpenSubKey(LLave, true);
                if (this.RegKey == null) {
                    flag = false;
                } else {
                    this.RegKey.SetValue(Nombre, RuntimeHelpers.GetObjectValue(Valor));
                    this.RegKey.Close();
                    flag = true;
                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                "SivevLib",             //Libreria
                nameof(RegWin),         //Clase 
                nameof(GuardaValor),    //Metodo
                ex.HResult,             //ErrorNum
                ex.Message,             //ErrorDesc
                0                       //SqlErr
            ));
            }
            return flag;
        }
        #endregion

        #region SET an GET
        public short Centro {
            get => LeerValor<short>(nameof(Centro));
            set => EscribirValor(nameof(Centro), value);
        }


        public byte Estacion {
            get => LeerValor<byte>(nameof(Estacion));
            set => EscribirValor(nameof(Estacion), value);
        }

        public string CpuName {
            get => LeerValor<string>(nameof(CpuName));
            set => EscribirValor(nameof(CpuName), value);
        }

        public string DomainName {
            get => LeerValor<string>(nameof(DomainName));
            set => EscribirValor(nameof(DomainName), value);
        }

        public string IpAddress {
            get => LeerValor<string>(nameof(IpAddress));
            set => EscribirValor(nameof(IpAddress), value);
        }
        /*
        
        public string EstacionId {
            get => LeerValor<string>(nameof(EstacionId));
            set => EscribirValor(nameof(EstacionId), value);
        }*/


        public string EstacionId {
            get => LeerValor<string>(nameof(EstacionId));
            set => EscribirValor(nameof(EstacionId), value);
        }

        public string ClaveAccesoId {
            get {
                var v = LeerValor<string>(nameof(ClaveAccesoId));
                return v;
            }
            set => EscribirValor(nameof(ClaveAccesoId), value);
        }

        




        public short VelocidadPuerto {
            get => LeerValor<short>(nameof(VelocidadPuerto));
            set => EscribirValor(nameof(VelocidadPuerto), ((int)value).ToString());
        }

        public string APPNAME {
            get {
                var v = LeerValor<string>(nameof(APPNAME));
                return v;
            }
            set => EscribirValor(nameof(APPNAME), value);
        }



        public string AppRolePass {
            get {
                var v = LeerValor<string>(nameof(AppRolePass));
                Console.WriteLine($"AppRolePass leído: '{v}'");
                return v;
            }
            set => EscribirValor(nameof(AppRolePass), value);
        }

        public string AppRole {
            get => LeerValor<string>(nameof(AppRole));
            set => EscribirValor(nameof(AppRole), value);
        }

        public string SQL_USER {
            get => LeerValor<string>(nameof(SQL_USER));
            set => EscribirValor(nameof(SQL_USER), value);
        }

        public string SQL_PASS {
            get => LeerValor<string>(nameof(SQL_PASS));
            set => EscribirValor(nameof(SQL_PASS), value);
        }

        // Acceso a Roles Sivev

        public string APPROLE {
            get => LeerValor<string>(nameof(APPROLE));
            set => EscribirValor(nameof(APPROLE), value);
        }


        public string APPROLE_PASS {
            get => LeerValor<string>(nameof(APPROLE_PASS));
            set => EscribirValor(nameof(APPROLE_PASS), value);
        }




        // Acceso a Roles Aplicacion
        public string APPROLE_VISUAL { get; set; }

        public string APPROLE_PASS_VISUAL {
            get => LeerValor<string>(nameof(APPROLE_PASS_VISUAL));
            set => EscribirValor(nameof(APPROLE_PASS_VISUAL), value);
        }


        #endregion

        #region Configuración de Bases de Datos
        public string SqlExpressName {
            get => LeerValor<string>(nameof(SqlExpressName));
            set => EscribirValor(nameof(SqlExpressName), value);
        }

        public string BaseSqlExpress {
            get => LeerValor<string>(nameof(BaseSqlExpress));
            set => EscribirValor(nameof(BaseSqlExpress), value);
        }

        public string SqlServerName {
            get => LeerValor<string>(nameof(SqlServerName));
            set => EscribirValor(nameof(SqlServerName), value);
        }

        public string BaseSql {
            get => LeerValor<string>(nameof(BaseSql));
            set => EscribirValor(nameof(BaseSql), value);
        }
        #endregion

        #region Configuración de Puertos de Equipos
        public byte MsmPtoNum {
            get => LeerValor<byte>(nameof(MsmPtoNum));
            set => EscribirValor(nameof(MsmPtoNum), value);
        }

        public byte McepPtoNum {
            get => LeerValor<byte>(nameof(McepPtoNum));
            set => EscribirValor(nameof(McepPtoNum), value);
        }

        public byte McsPtoNum {
            get => LeerValor<byte>(nameof(McsPtoNum));
            set => EscribirValor(nameof(McsPtoNum), value);
        }

        public byte MemPtoNum {
            get => LeerValor<byte>(nameof(MemPtoNum));
            set => EscribirValor(nameof(MemPtoNum), value);
        }


        public byte MctPtoNum {
            get => LeerValor<byte>(nameof(MctPtoNum));
            set => EscribirValor(nameof(MctPtoNum), value);
        }

        public byte OpacimetroPtoNum {
            get => LeerValor<byte>(nameof(OpacimetroPtoNum));
            set => EscribirValor(nameof(OpacimetroPtoNum), value);
        }
        #endregion

        #region Configuración del Puerto Serie
        public byte BitsDatos {
            get => LeerValor<byte>(nameof(BitsDatos));
            set => EscribirValor(nameof(BitsDatos), value);
        }

        public int BitsParidad {
            get => LeerValor<int>(nameof(BitsParidad));
            set => EscribirValor(nameof(BitsParidad), value);
        }

        public int BitsParada {
            get => LeerValor<int>(nameof(BitsParada));
            set => EscribirValor(nameof(BitsParada), value);
        }

        public int ControlFlujo {
            get => LeerValor<int>(nameof(ControlFlujo));
            set => EscribirValor(nameof(ControlFlujo), value);
        }
        #endregion

        #region Configuración general del sistema
        public short OpcionMenuId {
            get => LeerValor<short>(nameof(OpcionMenuId));
            set => EscribirValor(nameof(OpcionMenuId), value);
        }

        public string AccesoId {
            get => LeerValor<string>(nameof(AccesoId));
            set => EscribirValor(nameof(AccesoId), value);
        }


        public int ModeloDinamometroId {
            get => LeerValor<int>(nameof(ModeloDinamometroId));
            set => EscribirValor(nameof(ModeloDinamometroId), value);
        }

        #endregion

        #region Bits de Confirmación

        public byte Confirma1 {
            get => LeerValor<byte>(nameof(Confirma1));
            set => EscribirValor(nameof(Confirma1), value);
        }

        public byte Confirma2 {
            get => LeerValor<byte>(nameof(Confirma2));
            set => EscribirValor(nameof(Confirma2), value);
        }

        public byte Confirma3 {
            get => LeerValor<byte>(nameof(Confirma3));
            set => EscribirValor(nameof(Confirma3), value);
        }

        public byte Confirma4 {
            get => LeerValor<byte>(nameof(Confirma4));
            set => EscribirValor(nameof(Confirma4), value);
        }

        public byte Confirma5 {
            get => LeerValor<byte>(nameof(Confirma5));
            set => EscribirValor(nameof(Confirma5), value);
        }

        public byte Confirma6 {
            get => LeerValor<byte>(nameof(Confirma6));
            set => EscribirValor(nameof(Confirma6), value);
        }
        public byte Confirma7 {
            get => LeerValor<byte>(nameof(Confirma7));
            set => EscribirValor(nameof(Confirma7), value);
        }
        #endregion

        #region Confirma Holograma
        public byte ConfirmaRechazo {
            get => LeerValor<byte>(nameof(ConfirmaRechazo));
            set => EscribirValor(nameof(ConfirmaRechazo), value);
        }

        public byte ConfirmaDos {
            get => LeerValor<byte>(nameof(ConfirmaDos));
            set => EscribirValor(nameof(ConfirmaDos), value);
        }

        public byte ConfirmaCero {
            get => LeerValor<byte>(nameof(ConfirmaCero));
            set => EscribirValor(nameof(ConfirmaCero), value);
        }

        public byte ConfirmaDobleCero {
            get => LeerValor<byte>(nameof(ConfirmaDobleCero));
            set => EscribirValor(nameof(ConfirmaDobleCero), value);
        }

        #endregion

        #region Configuración de depuración y conexión

        public byte DebugActivo {
            get => LeerValor<byte>(nameof(DebugActivo));
            set => EscribirValor(nameof(DebugActivo), value);
        }

        public byte NivelDebug {
            get => LeerValor<byte>(nameof(NivelDebug));
            set => EscribirValor(nameof(NivelDebug), value);
        }

        public byte ConectaSF {
            get => LeerValor<byte>(nameof(ConectaSF));
            set => EscribirValor(nameof(ConectaSF), value);
        }

        public byte NumeroCaja {
            get => LeerValor<byte>(nameof(NumeroCaja));
            set => EscribirValor(nameof(NumeroCaja), value);
        }

        #endregion

        #region Configuracion del Reg Edit
        public string[,] TraeValores(string subclave) {
            //public string[,] TraeValores(string subclave) {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(subclave, writable: false);

                if (key is null)
                    throw new Exception($"No se pudo abrir la clave del registro: {subclave}");

                string[] nombres = key.GetValueNames();
                string[,] resultados = new string[2, nombres.Length];

                for (int i = 0; i < nombres.Length; i++) {
                    string nombre = nombres[i];
                    string valor = key.GetValue(nombre)?.ToString() ?? "";
                    resultados[0, i] = nombre;
                    resultados[1, i] = valor;
                }

                return resultados;
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(TraeValores),
                    ex.HResult,
                    ex.Message,
                    0
                ));
                return new string[2, 0]; // ← Evita null
            }
        }

        private bool CreaSubllave(string subclave) {
            try {
                using var key = Registry.LocalMachine.CreateSubKey(subclave, RegistryKeyPermissionCheck.ReadWriteSubTree);
                return key is not null;
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(CreaSubllave),
                    ex.HResult,
                    ex.Message,
                    0
                ));
                return false;
            }
        }

        private bool ChecaSubllave(string Subllave) {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(Subllave, writable: true);
                return key is not null;
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(ChecaSubllave),
                    ex.HResult,
                    ex.Message,
                    0
                ));
                return false;
            }
        }

        private string Dec_Hex(double dValor, int iLen = 0) {
            try {
                if (dValor <= 0)
                    return string.Empty;

                // Convertir a entero (solo la parte entera)
                int valorEntero = (int)Math.Floor(dValor);

                // Convertir a hexadecimal
                string hex = Convert.ToString(valorEntero, 16).ToUpper();

                // Si se requiere cortar longitud
                if (iLen > 0 && hex.Length > iLen)
                    hex = hex.Substring(0, iLen);

                return hex;
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(Dec_Hex),
                    ex.HResult,
                    ex.Message,
                    0
                ));
                return string.Empty;
            }
        }

        #endregion

        #region ActualizaLLaves()

        public bool ActualizaLLaves() {
            bool flag = false;

            try {
                if (!ChecaSubllave(SubKey) && !CreaSubllave(SubKey))
                    throw new ApplicationException($"No se pudo crear la subclave '{SubKey}'");

                using var key = Registry.LocalMachine.OpenSubKey(SubKey);
                foreach (var kvp in ValoresPorDefecto) {
                    if (key?.GetValue(kvp.Key) is null) {
                        EscribirValor(kvp.Key, kvp.Value);
                        //Console.WriteLine($"Asignado valor por defecto a '{kvp.Key}': {kvp.Value}");
                        flag = true;
                    }
                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(ActualizaLLaves),
                    ex.HResult,
                    ex.Message,
                    0
                ));
            }

            return flag;
        }

        // Claves que SÍ se deben inicializar para estación Visual
        private static readonly HashSet<string> ClavesVisual = new(StringComparer.OrdinalIgnoreCase) {
            nameof(SqlServerName),
            nameof(BaseSql),
            nameof(SQL_USER),
            nameof(SQL_PASS),
            nameof(APPNAME),

            nameof(APPROLE),
            nameof(APPROLE_PASS),

            nameof(APPROLE_VISUAL),
            nameof(APPROLE_PASS_VISUAL),

            nameof(EstacionId),
            nameof(OpcionMenuId),
            nameof(ClaveAccesoId)
        };

        public bool ActualizaLlavesVisual() {
            bool flag = false;
            try {
                if (!ChecaSubllave(SubKey) && !CreaSubllave(SubKey))
                    throw new ApplicationException($"No se pudo crear la subclave '{SubKey}'");

                using var key = Registry.LocalMachine.OpenSubKey(SubKey);
                foreach (var kvp in ValoresPorDefecto) {
                    if (!ClavesVisual.Contains(kvp.Key)) continue;              // <- filtro Visual
                    if (key?.GetValue(kvp.Key) is not null) continue;           // ya existe, no tocar

                    EscribirValor(kvp.Key, kvp.Value);
                    flag = true;
                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs("SivevLib", nameof(RegWin), nameof(ActualizaLlavesVisual),
                    ex.HResult, ex.Message, 0));
            }
            return flag;
        }


        //creado por mi
        public bool GuardaTodosLosValores() {
            bool flag = false;

            try {
                if (!ChecaSubllave(SubKey) && !CreaSubllave(SubKey))
                    throw new ApplicationException($"No se pudo crear la subclave '{SubKey}'");

                foreach (var prop in this.GetType().GetProperties()) {
                    if (!prop.CanRead || prop.Name == nameof(RegKey)) continue;

                    object? valor = prop.GetValue(this);
                    if (valor != null) {
                        EscribirValor(prop.Name, valor); // ← Aquí se cifra antes de guardar
                        //Console.WriteLine($"Registrado (cifrado): {prop.Name} = {valor}");
                        flag = true;
                    }


                }
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(GuardaTodosLosValores),
                    ex.HResult,
                    ex.Message,
                    0
                ));
            }

            return flag;
        }

        private Dictionary<string, object> ValoresPorDefecto => new() {
            { "Centro", (short)0 },
            { "Estacion", (byte)0 },
            { "CpuName", "Desconocido" },
            { "DomainName", "Desconocido" },
            { "IpAddress", "000.000.000.000" },
            { "SqlExpressName", "Desconocido" },
            { "BaseSqlExpress", "Desconocida" },
            { "SqlServerName", "Desconocido" },
            { "BaseSql", "Desconocida" },
            { "EstacionId", "00000000-0000-0000-0000-000000000000" },
            { "MsmPtoNum", (byte)0 },
            { "McepPtoNum", (byte)0 },
            { "McsPtoNum", (byte)0 },
            { "MemPtoNum", (byte)0 },
            { "MctPtoNum", (byte)0 },
            { "OpacimetroPtoNum", (byte)0 },
            { "BitsDatos", (byte)8 },
            { "BitsParidad", 0 },
            { "BitsParada", 1 },
            { "ControlFlujo", 0 },
            { "VelocidadPuerto", (short)9600 },
            { "OpcionMenuId", (short)0 },
            { "AccesoId", "00000000-0000-0000-0000-000000000000" },
            { "ModeloDinamometroId", 0 },
            { "Confirma1", (byte)0 },
            { "Confirma2", (byte)0 },
            { "Confirma3", (byte)0 },
            { "Confirma4", (byte)0 },
            { "Confirma5", (byte)0 },
            { "Confirma6", (byte)0 },
            { "Confirma7", (byte)0 },
            { "ConfirmaRechazo", (byte)0 },
            { "ConfirmaDos", (byte)0 },
            { "ConfirmaCero", (byte)0 },
            { "ConfirmaDobleCero", (byte)0 },
            { "DebugActivo", (byte)0 },
            { "NivelDebug", (byte)0 },
            { "ConectaSF", (byte)1 },
            { "NumeroCaja", (byte)0 }
        };

        private bool ChecaValor(string clave, string nombre) {
            try {
                using var key = Registry.LocalMachine.OpenSubKey(clave);
                return key?.GetValue(nombre) != null;
            } catch (Exception ex) {
                ErrorProceso?.Invoke(new ErrorProcesoArgs(
                    "SivevLib",
                    nameof(RegWin),
                    nameof(ChecaValor),
                    ex.HResult,
                    ex.Message,
                    0
                ));
            }
            return false;
        }


        #endregion

        #region Constructores
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    //GC.Collect();
                }
                disposed = true;
            }
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RegWin() {
            Dispose(false);
        }


        public RegWin(EncryptConfig cfg) {
            saltValue = cfg.SaltValue;
            hashAlgorithm = cfg.HashAlgorithm;
            passwordIterations = cfg.PasswordIterations;
            initVector = cfg.InitVector;
            keySize = cfg.KeySize;
        }

        public RegWin() {
            // 2.1: intenta .env si está
            TryLoadFromEnv(out var fromEnv);

            if (fromEnv is not null) {
                saltValue = fromEnv.SaltValue;
                hashAlgorithm = fromEnv.HashAlgorithm;
                passwordIterations = fromEnv.PasswordIterations;
                initVector = fromEnv.InitVector;
                keySize = fromEnv.KeySize;
            } else {
                // 2.2: fallback al hardcode
                var cfg = Cifrados.Cifrados.Default;
                saltValue = cfg.SaltValue;
                hashAlgorithm = cfg.HashAlgorithm;
                passwordIterations = cfg.PasswordIterations;
                initVector = cfg.InitVector;
                keySize = cfg.KeySize;
            }
        }

        private static void TryLoadFromEnv(out EncryptConfig? cfg) {
            cfg = null;
            try {
                // Carga simple del .env en el mismo folder del exe
                var baseDir = AppContext.BaseDirectory;
                var envPath = Path.Combine(baseDir, ".env");
                if (File.Exists(envPath))
                    DotNetEnv.Env.Load(envPath);
                else
                    DotNetEnv.Env.Load(); // por si hay variables en proceso

                var salt = Environment.GetEnvironmentVariable("SALT_VALUE");
                var hash = Environment.GetEnvironmentVariable("HASH_ALGORITHM");
                var passIt = Environment.GetEnvironmentVariable("PASSWORD_ITERATIONS");
                var iv = Environment.GetEnvironmentVariable("INIT_VECTOR");
                var ks = Environment.GetEnvironmentVariable("KEY_SIZE");

                // si no hay nada, salimos
                if (string.IsNullOrWhiteSpace(salt) &&
                    string.IsNullOrWhiteSpace(hash) &&
                    string.IsNullOrWhiteSpace(passIt) &&
                    string.IsNullOrWhiteSpace(iv) &&
                    string.IsNullOrWhiteSpace(ks)) {
                    return;
                }

                cfg = new EncryptConfig {
                    SaltValue = string.IsNullOrWhiteSpace(salt) ? "s@1tValue" : salt,
                    HashAlgorithm = string.IsNullOrWhiteSpace(hash) ? "SHA1" : hash,
                    PasswordIterations = int.TryParse(passIt, out var it) ? it : 2,
                    InitVector = string.IsNullOrWhiteSpace(iv) ? "@1B2c3D4e5F6g7H8" : iv,
                    KeySize = int.TryParse(ks, out var ksz) ? ksz : 256
                };
            } catch {
                cfg = null;
            }
        }


        public static RegWin CrearDesdeEstacion(Estaciones.estacion datos) {
            Console.WriteLine($"EstacionId desde datos: '{datos.EstacionId}'");

            return new RegWin {
                Centro = (short)datos.Centro,
                Estacion = (byte)datos.Estacion,
                CpuName = datos.NombreCPU,
                DomainName = datos.Dominio,
                IpAddress = datos.DireccionIP,
                SqlServerName = datos.SqlServerName,
                BaseSql = datos.BaseDatos,
                McepPtoNum = (byte)datos.PuertoMCEP,
                McsPtoNum = (byte)datos.PuertoMCS,
                MctPtoNum = (byte)datos.PuertoMCT,
                MemPtoNum = (byte)datos.PuertoMEM,
                MsmPtoNum = (byte)datos.PuertoMSM,
                OpacimetroPtoNum = (byte)datos.PuertoOpacimetro,
                DebugActivo = (byte)datos.DebugActivo,
                NivelDebug = (byte)datos.NivelActivo,
                ConectaSF = (byte)datos.ConectaSF,
                EstacionId = datos.EstacionId

            };
        }


        public static RegWin CrearEstacionVisual(Estaciones.estacionVisual datos) {
            Console.WriteLine($"Crear estación visual");

            return new RegWin {


                SqlServerName = datos.SqlServerName,
                BaseSql = datos.BaseDatos,
                SQL_USER = datos.SQL_USER,
                SQL_PASS = datos.SQL_PASS,
                APPNAME =  datos.APPNAME,

                APPROLE = datos.APPROLE,
                APPROLE_PASS = datos.APPROLE_PASS,

                APPROLE_VISUAL = datos.APPROLE_VISUAL,
                APPROLE_PASS_VISUAL = datos.APPROLE_PASS_VISUAL,

                ClaveAccesoId = datos.ClaveAccesoId,

                EstacionId = datos.EstacionId,
                OpcionMenuId =  datos.opcionMenu

            };
        }









        public void ImprimirValoresDesdeRegistro() {
            Console.WriteLine("== Valores leídos del registro (descifrados) ==");

            foreach (var prop in this.GetType().GetProperties()) {
                if (!prop.CanWrite || !prop.CanRead || prop.Name == nameof(RegKey))
                    continue;

                try {
                    string? valorCifrado = Registry.LocalMachine.OpenSubKey(SubKey)?.GetValue(prop.Name)?.ToString();
                    Console.WriteLine($"Leyendo '{prop.Name}': cifrado = {valorCifrado}");

                    var metodo = typeof(RegWin).GetMethod(nameof(LeerValor))?.MakeGenericMethod(prop.PropertyType);
                    var valor = metodo?.Invoke(this, new object[] { prop.Name });

                    Console.WriteLine($"{prop.Name}: {valor}");
                } catch (Exception ex) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{prop.Name}: ERROR → {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        // ERRORES CONTROLADOS PERSONALIZADOS
        public record ErrorProcesoArgs(
            string Libreria,
            string Clase,
            string Metodo,
            int ErrNum,
            string ErrDesc,
            int SqlErr
        ) {
            public override string ToString() =>
                $"[{Libreria}.{Clase}.{Metodo}] Error {ErrNum}: {ErrDesc} (SQL:{SqlErr})";
        }

        #endregion
    }
}
