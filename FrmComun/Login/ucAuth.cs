using FrmComun.Utils;
using Microsoft.Data.SqlClient;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrmComun.Login {
    public partial class ucAuth : UserControl {
        public event Action<Guid> AccesoObtenido;
        //public VisualRegistroWindows _Visual;
        public int Credencial { get; private set; } = 0;
        public event Action<int>? CredencialChanged;
        private Size _formSizeInicial;
        private float _fontSizeInicial;
        public bool ExisteHuella;
        public byte[] Huella;
        public event Action<string> _credencial;
        private readonly SivevConnectionFactory _sql;
        private readonly string _roll;
        private readonly string _passRoll;
        private readonly short _opcionMenu;
        private readonly Guid _estacionId;
        private readonly short _centro;

        public ucAuth(SivevConnectionFactory sql, string roll, string passRoll, short opcionMenu, Guid estacionId, short centro) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = passRoll.ToUpper() ?? throw new ArgumentNullException(nameof(passRoll));
            _opcionMenu = opcionMenu;
            _estacionId = estacionId;
            _centro = centro;

            InitializeComponent();
            ResetForm();
            //Mostrar.Mensaje($"Bienvenido al sistema de autenticación, por favor ingrese su credencial.",$"{_passRoll}");
        }
        private void ResetForm() {
            txbCredencial.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txbCredencial, @"[^0-9]");
            txbPassword.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txbPassword, @"[^a-zA-Z0-9]");
            txbCredencial.MaxLength = 6;
            txbPassword.MaxLength = 32;
            txbCredencial.Focus();

            
            txbCredencial.PreviewKeyDown += txbCredencial_PreviewKeyDown;
            //txbCredencial.TextChanged += txbCredencial_TextChanged;
            _fontSizeInicial = this.Font.Size;

            lblCredencial.Enabled = true;
            txbCredencial.Enabled = true;
            btnAcceder.Enabled = false;
            btnAcceder.Visible = false;
            txbPassword.Enabled = false;
            txbPassword.Visible = false;
            lblPassword.Visible = false;
            txbCredencial.Focus();
        }


        #region Buscar
        private void btnAcceder_Click(object sender, EventArgs e) {
            ActivacionBotonAcceder();
        }
        private void txbPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ActivacionBotonAcceder();
            }
        }

        private async void ActivacionBotonAcceder() {
            btnAcceder.Enabled = false;
            txbPassword.Enabled = false;
            lblPassword.Enabled = false;
            lblPassword.Visible = true;
            txbCredencial.Focus();

            ActualizarCredencial(validaCredencialNumerico(txbCredencial.Text));
            
            //Mostrar.Mensaje($"Verificando credencial {Credencial} y contraseña, por favor espere...");
            var r = await GetAccesoSQL(credencial:Credencial);
            
            Guid accesoNormalizado = Guid.Empty;
            if (r != null && r.MensajeId == 0 && r.AccesoId != Guid.Empty) {
                accesoNormalizado = r.AccesoId;
                await Task.Delay(200);
                AccesoObtenido?.Invoke(accesoNormalizado);
            }
            if (accesoNormalizado == Guid.Empty) {
                btnAcceder.Enabled = true;
                txbPassword.Text = "";
                txbPassword.Enabled = true;
                lblPassword.Enabled = true;
                lblPassword.Visible = true;
                txbPassword.Focus();
            }
        }

        #endregion


        private async void txbCredencial_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                lblPassword.Enabled = true;
                lblPassword.Visible = true;

                e.IsInputKey = true;
                Credencial = validaCredencialNumerico(txbCredencial.Text);

                bool IsSet(string s) => !string.IsNullOrWhiteSpace(s);
              
                var r = await CredencialExisteHuella(credencial:Credencial);

                if (r.MensajeId == 0) {
                    lblCredencial.Enabled = false;
                    txbCredencial.Enabled = false;
                    
                    

                    ActualizarCredencial(Credencial);
                    ExisteHuella = r.ExisteHuella;
                    Huella = r.Huella;

                    if (!ExisteHuella) {
                        MostrarPassword();
                    } else {
                        Mostrar.Mensaje($"ERROR",$"Se detectó que la credencial {Credencial} tiene huella registrada, se procederá a verificarla.");
                        /*
                        var visual_48 = new DPFP_SMA.Models.VisualRegistroWindows {
                            dvar18 = txbCredencial.Text,
                            dvar1 = _Visual.dvar1,
                            dvar2 = _Visual.dvar2,
                            dvar3 = _Visual.dvar3,
                            dvar4 = _Visual.dvar4,
                            dvar5 = _Visual.dvar5,
                            dvar15 = _Visual.dvar15,
                            dvar8 = _Visual.dvar8,
                            dvar17 = _Visual.dvar17,
                            dvar16 = _Visual.dvar16,
                            Huella = Huella
                        };

                        var analizaHuellaForm = new DPFP_SMA.Forms.Comun.VerificationForm(visual_48);
                        Guid accesoIdRecibido = Guid.Empty;
                        analizaHuellaForm.StartPosition = FormStartPosition.CenterParent;
                        analizaHuellaForm.AccesoObtenido += (accesoId) => {
                            accesoIdRecibido = accesoId;
                        };
                        analizaHuellaForm.ShowDialog();
                        if (!accesoIdRecibido.Equals(Guid.Empty)) {
                            analizaHuellaForm.Close();
                            _Visual.dvar20 = accesoIdRecibido;
                            AccesoObtenido?.Invoke(accesoIdRecibido);
                        }
                        */
                    }
                } else {
                    txbCredencial.Text = string.Empty;
                    txbCredencial.Focus();
                }
            } 
        }
        
        private void ActualizarCredencial(int valor) {
            Credencial = valor;
            CredencialChanged?.Invoke(valor);
        }
        private int validaCredencialNumerico(string strcredencial) {
            if (int.TryParse(strcredencial, out int credencial)) {
                return credencial;
            } else {
                Mostrar.Mensaje($"Solo números en la credencial");
            }
            return 0;
        }
        #region Focus 
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            BeginInvoke(new Action(() => {
                ActiveControl = txbCredencial;
                txbCredencial.Select();
                txbCredencial.Focus();
            }));
        }
        private void MostrarPassword(bool visible = true) {

            lblPassword.Visible = visible;
            lblPassword.Enabled = visible;

            txbPassword.Visible = visible;
            txbPassword.Enabled = visible;

            btnAcceder.Visible = visible;
            btnAcceder.Enabled = visible;

            txbPassword.BringToFront();

            BeginInvoke(new Action(() => {
                ActiveControl = txbPassword;
                txbPassword.Select();
                txbPassword.Focus();
            }));
        }
        #endregion
        #region Metodos SQL
        private async Task<CredencialExisteHuellaResult> CredencialExisteHuella(int credencial, CancellationToken ct = default) {
            int mensaje = 100;
            string msm = string.Empty;
            short resultado = 0;
            bool existeHuella = false;
            byte[] huella = Array.Empty<byte>();

            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var r = repo.SpAppCredencialExisteHuella(cnn: connApp, uiEstacionId: _estacionId, siOpcionMenuId: _opcionMenu, iCredencial: credencial);
                    resultado = r.Resultado;
                    mensaje = r.MensajeId;
                    existeHuella = r.ExisteHuella;
                    huella = r.Huella;

                    if (mensaje != 0) {
                        try {
                            var error = await repo.PrintIfMsgAsync(connApp, "Error en SpAppCredencialExisteHuella", mensaje);
                            msm = error.Mensaje;
                            var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu,  descripcion: $"Credencial: {credencial}, {error.Mensaje}", codigoSql: mensaje);
                            await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                            Mostrar.Mensaje($"Credencial: {credencial}",$"{error.Mensaje}");
                        } catch (Exception logEx) {
                            msm = logEx.Message;
                            SivevLogger.Error($"Falló la bitácora en CredencialExisteHuella: {logEx.Message}",SivevOrigen.LoginCredencial);
                        }
                    }
                }, ct);
            } catch (Exception e) {

                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu,descripcion: $"Credencial: {credencial}, {e.Message}",codigoSql: mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de CredencialExisteHuella: {logEx}", SivevOrigen.LoginCredencial);
                }
                Mostrar.Mensaje($"Error en SpAppCredencialExisteHuella",$"Global con la credencial {credencial}: {e.Message}");            }

            return new CredencialExisteHuellaResult {
                MensajeId = mensaje,
                Resultado = resultado,
                ExisteHuella = existeHuella,
                Huella = huella
            };
        }

        private async Task<AccesoIniciaResult> GetAccesoSQL(int credencial, CancellationToken ct = default) {
            int _mensaje = 100;
            short _resultado = 0;
            Guid _AccesoSql = Guid.Empty;
            var repo = new SivevRepository();

            try {
                await EjecutarSqlAsync(async connApp => {
                    var rinicial = await repo.SpAppAccesoIniciaAsync( conn:connApp, estacionId: _estacionId, opcionMenuId:_opcionMenu, credencial:credencial,password:txbPassword.Text, huella:Huella);
                    _resultado = rinicial.ReturnCode;
                    _mensaje = rinicial.MensajeId;
                    _AccesoSql = rinicial.AccesoId;

                    if (_mensaje != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"SpAppCredencialExisteHuella", _mensaje);
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu,  descripcion: $"Credencial: {credencial}, {error.Mensaje}", codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                        Mostrar.Mensaje($"Credencial: {credencial}", $"{error.Mensaje}");
                        ResetForm();
                    }
                });
            } catch (Exception e) {
                try {
                    await EjecutarSqlAsync(async connApp => {
                        var bitacora = Bitacora.ErroresSQL(estacionId: _estacionId, centro: _centro, opcionMenuId: _opcionMenu,descripcion: $"Credencial: {credencial}, {e.Message}",codigoSql: _mensaje);
                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);
                    }, ct);
                } catch (Exception logEx) {
                    SivevLogger.Error($"Falló la bitácora en catch de credencial {credencial}, GetAccesoSQL: {logEx.Message}");
                }
                Mostrar.Mensaje($"Error en GetAccesoSQL con la credencial {credencial}: {e.Message}");
            }
            return new AccesoIniciaResult {
                MensajeId = _mensaje,
                ReturnCode = _resultado,
                AccesoId = _AccesoSql
            };
        }
        #endregion

        #region Ejecutar SQL
        private async Task EjecutarSqlAsync(Func<SqlConnection, Task> accion,  CancellationToken ct = default) {

            await using var session = await _sql.OpenSessionAsync( new AppRoleConfig {
                    Nombre = _roll,
                    Password = _passRoll,
                    Habilitado = true
                }
            );

            await accion(session.Connection);
        }

        private async Task<T> EjecutarSqlAsync<T>(Func<SqlConnection, Task<T>> accion, CancellationToken ct = default) {
            await using var session = await _sql.OpenSessionAsync( new AppRoleConfig {
                    Nombre = _roll,
                    Password = _passRoll,
                    Habilitado = true
                }
            );
            return await accion(session.Connection);
        }
        #endregion
    }
}
