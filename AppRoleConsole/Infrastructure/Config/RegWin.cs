using DotNetEnv;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppRoleConsole.Infrastructure.Config {
    public class RegWin : IDisposable {
        protected bool disposed = false;
        private const string HKEY_CLASSES_ROOT = "HKEY_CLASSES_ROOT";
        private const string HKEY_CURRENT_USER = "HKEY_CURRENT_USER";
        private const string HKEY_LOCAL_MACHINE = "HKEY_LOCAL_MACHINE";
        private const string HKEY_USERS = "HKEY_USERS";
        private const string HKEY_PERFORMANCE_DATA = "HKEY_PERFORMANCE_DATA";
        private const string HKEY_CURRENT_CONFIG = "HKEY_CURRENT_CONFIG";
        private const string HKEY_DYN_DATA = "HKEY_DYN_DATA";
        private const string SubKey = @"SOFTWARE\SIVEV";
        private const string LLaveCompleta = "HKEY_LOCAL_MACHINE\\SOFTWARE\\SIVEV";
        private RegistryKey? RegKey;
        private readonly string Valor;
        private readonly string[,] ControlReg;

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
        private readonly string sMasterPassWord;
        private readonly string sPropKey;
        private readonly string cipherText;
        private readonly string passPhrase;


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
                Console.WriteLine($"nombre de la propiedad{nombrePropiedad}, Valor {valorOriginal}");
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
                var raw = key?.GetValue(nombrePropiedad);
                string? valorEncriptado = raw is byte[] bytes
            ? Encoding.UTF8.GetString(bytes)
            : raw?.ToString();

                if (string.IsNullOrWhiteSpace(valorEncriptado))
                    return default!;

                string desencriptado = this.Desencriptacion(
            valorEncriptado,
            nombrePropiedad,
            this.saltValue,
            this.hashAlgorithm,
            this.passwordIterations,
            this.initVector,
            this.keySize
        );

                if (string.IsNullOrWhiteSpace(desencriptado))
                    return default!;

                // Si se espera un string y parece un GUID, devuelve el string del GUID completo
                if (typeof(T) == typeof(string) &&
                    nombrePropiedad.Contains("Id", StringComparison.OrdinalIgnoreCase) &&
                    Guid.TryParse(desencriptado, out var guid))
                    return (T)(object)guid.ToString();

                if (typeof(T) == typeof(Guid)) {
                    return (T)(object)Guid.Parse(desencriptado);
                }

                return (T)Convert.ChangeType(desencriptado, typeof(T));
            } catch (Exception ex) {
                Console.WriteLine($"Error al leer '{nombrePropiedad}': {ex.Message}");
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
            get {
                var v = LeerValor<string>(nameof(EstacionId));
                Console.WriteLine($"EstacionId leído: '{v}' (Length: {v?.Length})");
                return v;
            }
            set => EscribirValor(nameof(EstacionId), value);
        }

        public short VelocidadPuerto {
            get => LeerValor<short>(nameof(VelocidadPuerto));
            set => EscribirValor(nameof(VelocidadPuerto), ((int)value).ToString());
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


        public RegWin() {
            string baseDir = AppContext.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));
            string envPath = Path.Combine(projectRoot, ".env");
            Env.Load(envPath);

            this.saltValue = Environment.GetEnvironmentVariable("SALT_VALUE") ?? "s@1tValue";
            this.hashAlgorithm = Environment.GetEnvironmentVariable("HASH_ALGORITHM") ?? "SHA1";
            this.passwordIterations = int.Parse(Environment.GetEnvironmentVariable("PASSWORD_ITERATIONS") ?? "2");
            this.initVector = Environment.GetEnvironmentVariable("INIT_VECTOR") ?? "@1B2c3D4e5F6g7H8";
            this.keySize = int.Parse(Environment.GetEnvironmentVariable("KEY_SIZE") ?? "256");

            //Console.WriteLine($"Valores iniciales\n{saltValue}\n{hashAlgorithm}\n{passwordIterations}\n{initVector}\n{keySize}\n");
        }

        public static RegWin CrearDesdeEstacion(estacion datos) {
            Console.WriteLine($"EstacionId desde datos: '{datos.EstacionId}' (Length: {datos.EstacionId?.Length})");

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
    public class estacion {
        public short PuertoMCEP { get; set; }
        public short PuertoMCS { get; set; }
        public short PuertoMCT { get; set; }
        public short PuertoMEM { get; set; }
        public short PuertoMSM { get; set; }
        public short PuertoOpacimetro { get; set; }
        public string SqlServerName { get; set; }
        public string BaseDatos { get; set; }
        public string DireccionIP { get; set; }
        public string Dominio { get; set; }
        public string NombreCPU { get; set; }
        public int Centro { get; set; }
        public short Estacion { get; set; }
        public string EstacionId { get; set; }
        //public Guid EstacionId { get; set; }
        public short DebugActivo { get; set; }
        public short NivelActivo { get; set; }
        public short ConectaSF { get; set; }
    }
}
