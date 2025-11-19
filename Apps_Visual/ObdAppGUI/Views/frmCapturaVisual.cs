using Apps_Visual.UI.Theme;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Config.Estaciones;
using SQLSIVEV.Infrastructure.Security;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//clbPrincipal

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmCapturaVisual : Form {
        /*
        public Guid _estacionId = Guid.Empty, _accesoId = Guid.Empty;
        
        public string SERVER = string.Empty, DB = string.Empty, SQL_USER = string.Empty, SQL_PASS = string.Empty,
            appName = string.Empty, APPROLE = string.Empty, APPROLE_PASS = string.Empty, _placa = string.Empty;
        //*/

        public event Action<bool> HabilitarPruebas;
        public Guid _verificacionId = Guid.Empty;
        public byte _protocoloVerificacíon;

        //*
        private Guid _estacionId = Guid.Parse("BFFF8EA5-76A4-F011-811C-D09466400DBA");
        private Guid _accesoId = Guid.Parse("94A18C29-ABC1-F011-811C-D09466400DBA");

        private string 
            SERVER = "192.168.16.8", 
            DB = "SIVEV", 
            SQL_USER = "SivevCentros", 
            SQL_PASS = "CentrosSivev",
            appName = "SivAppVfcVisual", 
            APPROLE = "RollVfcVisual", 
            APPROLE_PASS = "95801B7A-4577-A5D0-952E-BD3D89757EA5", 
            _placa = string.Empty;
        //*/
        public int panelX = 0, panelY = 0;

        private byte tiTaponCombustible = 0, tiTaponAceite = 0, tiBayonetaAceite = 0, tiPortafiltroAire = 0,
             tiTuboEscape = 0, tiFugasMotorTrans = 0, tiNeumaticos = 0, tiComponentesEmisiones = 0,
             tiMotorGobernado = 0;

        int odometro = 0;


        public frmCapturaVisual() {
            InitializeComponent();
            ConfigurarCheckBox(cbTaponCombustible, "Contiene Tapon de Combustible");
            ConfigurarCheckBox(cbTaponAceite, "Contiene Tapon de Aceite");
            ConfigurarCheckBox(cbBayonetaAceite, "Contiene Bayoneta de Aceite");
            ConfigurarCheckBox(cbPortaFiltroAire, "Contiene Porta filtro de Aire");
            ConfigurarCheckBox(cbTuboEscape, "Contiene Tubo de Escape");
            ConfigurarCheckBox(cbFugasMotorTrans, "Contiene Fugas de Motor o Transmisión");
            ConfigurarCheckBox(cbNeumaticos, "Contiene Neumáticos");
            ConfigurarCheckBox(cbComponentesEmisiones, "Contiene Componentes de Emisiones");
            ConfigurarCheckBox(cbMotorGobernado, "Contiene Motor de Gobernado");
            ResetForm();
            //this.Load += frmCapturaVisual_Load;

           

        }
        private async void frmCapturaVisual_Load(object sender, EventArgs e) {
            bool IsSet(string s) => !string.IsNullOrWhiteSpace(s);

            if (IsSet(SERVER) && IsSet(DB) && IsSet(SQL_USER) && IsSet(SQL_PASS)
                       && IsSet(appName) && IsSet(APPROLE) && IsSet(APPROLE_PASS) && _estacionId != Guid.Empty
                       && _accesoId != Guid.Empty) {

                var r = await GetAccesoSQLVerificaciones(
                    SERVER: SERVER,
                    DB: DB,
                    SQL_USER: SQL_USER,
                    SQL_PASS: SQL_PASS,
                    appName: appName,
                    APPROLE: APPROLE,
                    APPROLE_PASS: APPROLE_PASS,
                    estacionId: _estacionId,
                    AccesoId: _accesoId
                );
                if (r.MensajeId == 0) {
                    lblPlaca.Text = r.PlacaId;
                    _placa = r.PlacaId;
                    _verificacionId = r.VerificacionId;
                    _protocoloVerificacíon = r.ProtocoloVerificacionId;
                    /*######################################*/

                } else {
                    foreach (Control c in pnlPrincipal.Controls)
                        c.Dispose();
                    pnlPrincipal.Controls.Clear();
                    HabilitarPruebas?.Invoke(true);
                }
            } else {
                MostrarMensaje($"Se rompio la conexion SQL, Revisar la configuracion SERVER: {SERVER}\nDB: {DB}\nSQL_USER: {SQL_USER}\nSQL_PASS: {SQL_PASS}\nappName: {appName}\nAPPROLE_PASS: {APPROLE_PASS}\nestacionId: {_estacionId}");
                foreach (Control c in pnlPrincipal.Controls)
                    c.Dispose();
                pnlPrincipal.Controls.Clear();
                HabilitarPruebas?.Invoke(true);
            }
        }




        #region Configuración Inicial :D
        private void CheckBox_CheckedChanged(object sender, EventArgs e) {
            var chk = (CheckBox)sender;
            var textoBase = chk.Tag.ToString();

            if (chk.Checked) {
                chk.Text = textoBase;
                chk.ForeColor = AppColors.AprobadoInspeccionVisual;   // azul
            } else {
                chk.Text = "NO " + textoBase;
                chk.ForeColor = AppColors.InstitucionalPrimario;       // vino
            }
        }



        private void CheckBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                var chk = (CheckBox)sender;
                chk.Checked = !chk.Checked;
            }
        }


        private void ConfigurarCheckBox(CheckBox chk, string textoBase) {
            // Guardamos el texto original sin "NO"
            chk.Tag = textoBase;

            // Estado inicial
            chk.Text = "NO " + textoBase;
            chk.ForeColor = AppColors.InstitucionalPrimario;

            // Eventos genéricos
            chk.CheckedChanged += CheckBox_CheckedChanged;
            chk.KeyDown += CheckBox_KeyDown;
        }


        #region formatear
        public Panel GetPanel() {
            ResetForm();
            return pnlPrincipal;
        }

        private void ResetForm() {
            cbBayonetaAceite.Enabled = true;
            cbBayonetaAceite.Visible = true;
            cbBayonetaAceite.Checked = false;


            cbComponentesEmisiones.Enabled = true;
            cbComponentesEmisiones.Visible = true;
            cbComponentesEmisiones.Checked = false;

            cbFugasMotorTrans.Visible = true;
            cbFugasMotorTrans.Enabled = true;
            cbFugasMotorTrans.Checked = false;

            cbMotorGobernado.Enabled = true;
            cbMotorGobernado.Visible = true;
            cbMotorGobernado.Checked = false;

            cbNeumaticos.Enabled = true;
            cbNeumaticos.Visible = true;
            cbNeumaticos.Checked = false;

            cbPortaFiltroAire.Enabled = true;
            cbPortaFiltroAire.Visible = true;
            cbPortaFiltroAire.Checked = false;

            cbTaponAceite.Enabled = true;
            cbTaponAceite.Visible = true;   
            cbTaponAceite.Checked = false;

            cbTaponCombustible.Enabled = true;
            cbTaponCombustible.Visible = true;
            cbTaponCombustible.Checked = false;

            cbTuboEscape.Enabled = true;
            cbTuboEscape.Visible = true;    
            cbTuboEscape.Checked = false;

            btnSiguente.Enabled = false;


            if (panelX == 0 && panelY == 0) {
                pnlPrincipal.Size = new Size(Width, Height);
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * Width), 0);
            } else {
                pnlPrincipal.Size = new Size((int)Math.Ceiling(.98 * panelX), (int)Math.Ceiling(.95 * panelY));
                pnlPrincipal.Location = new Point((int)Math.Ceiling(.004 * panelX), 0);
            }
        }
        #endregion


        #endregion


        private void Combistible (int combustible) {
            // GASOLINA
            if (combustible == 1) {
                cbBayonetaAceite.Enabled = true;
                cbBayonetaAceite.Visible = true;

                cbComponentesEmisiones.Enabled = true;
                cbComponentesEmisiones.Visible = true;

                cbFugasMotorTrans.Visible = true;
                cbFugasMotorTrans.Enabled = true;

                cbMotorGobernado.Enabled = false;
                cbMotorGobernado.Visible = false;

                cbNeumaticos.Enabled = true;
                cbNeumaticos.Visible = true;

                cbPortaFiltroAire.Enabled = true;
                cbPortaFiltroAire.Visible = true;

                cbTaponAceite.Enabled = true;
                cbTaponAceite.Visible = true;

                cbTaponCombustible.Enabled = true;
                cbTaponCombustible.Visible = true;

                cbTuboEscape.Enabled = true;
                cbTuboEscape.Visible = true;


                // DIESEL
            } else if (combustible == 2) {
                cbBayonetaAceite.Enabled = false;
                cbBayonetaAceite.Visible = false;

                cbComponentesEmisiones.Enabled = false;
                cbComponentesEmisiones.Visible = false;

                cbFugasMotorTrans.Visible = false;
                cbFugasMotorTrans.Enabled = false;

                cbMotorGobernado.Enabled = true;
                cbMotorGobernado.Visible = true;

                cbNeumaticos.Enabled = false;
                cbNeumaticos.Visible = false;

                cbPortaFiltroAire.Enabled = false;
                cbPortaFiltroAire.Visible = false;

                cbTaponAceite.Enabled = false;
                cbTaponAceite.Visible = false;

                cbTaponCombustible.Enabled = false;
                cbTaponCombustible.Visible = false;

                cbTuboEscape.Enabled = true;
                cbTuboEscape.Visible = true;

                //GAS LP
            } else if (combustible == 3) {
                cbBayonetaAceite.Enabled = true;
                cbBayonetaAceite.Visible = true;

                cbComponentesEmisiones.Enabled = false;
                cbComponentesEmisiones.Visible = false;

                cbFugasMotorTrans.Visible = false;
                cbFugasMotorTrans.Enabled = false;

                cbMotorGobernado.Enabled = false;
                cbMotorGobernado.Visible = false;

                cbNeumaticos.Enabled = false;
                cbNeumaticos.Visible = false;

                cbPortaFiltroAire.Enabled = true;
                cbPortaFiltroAire.Visible = true;

                cbTaponAceite.Enabled = true;
                cbTaponAceite.Visible = true;

                cbTaponCombustible.Enabled = false;
                cbTaponCombustible.Visible = false;

                cbTuboEscape.Enabled = true;
                cbTuboEscape.Visible = true;

                //GAS NC
            } else if (combustible == 4) {
                cbBayonetaAceite.Enabled = true;
                cbBayonetaAceite.Visible = true;

                cbComponentesEmisiones.Enabled = false;
                cbComponentesEmisiones.Visible = false;

                cbFugasMotorTrans.Visible = false;
                cbFugasMotorTrans.Enabled = false;

                cbMotorGobernado.Enabled = false;
                cbMotorGobernado.Visible = false;

                cbNeumaticos.Enabled = false;
                cbNeumaticos.Visible = false;

                cbPortaFiltroAire.Enabled = true;
                cbPortaFiltroAire.Visible = true;

                cbTaponAceite.Enabled = true;
                cbTaponAceite.Visible = true;

                cbTaponCombustible.Enabled = false;
                cbTaponCombustible.Visible = false;

                cbTuboEscape.Enabled = true;
                cbTuboEscape.Visible = true;


                // HIBRIDO(GASOLINA)
            } else if (combustible == 5) {
                cbBayonetaAceite.Enabled = true;
                cbBayonetaAceite.Visible = true;

                cbComponentesEmisiones.Enabled = true;
                cbComponentesEmisiones.Visible = true;

                cbFugasMotorTrans.Visible = true;
                cbFugasMotorTrans.Enabled = true;

                cbMotorGobernado.Enabled = false;
                cbMotorGobernado.Visible = false;

                cbNeumaticos.Enabled = true;
                cbNeumaticos.Visible = true;

                cbPortaFiltroAire.Enabled = true;
                cbPortaFiltroAire.Visible = true;

                cbTaponAceite.Enabled = true;
                cbTaponAceite.Visible = true;

                cbTaponCombustible.Enabled = true;
                cbTaponCombustible.Visible = true;

                cbTuboEscape.Enabled = true;
                cbTuboEscape.Visible = true;
            }
        }


        private void ConversionCheckBox(int CapturaVisual, string elemento, bool despliegue) {
            if (CapturaVisual == 2 || CapturaVisual == 11 || CapturaVisual == 18 || CapturaVisual == 26) {
                cbBayonetaAceite.Text = "NO CONTIENE " + elemento;
                cbBayonetaAceite.Visible = despliegue;
                cbBayonetaAceite.Enabled = despliegue;
            } else if (CapturaVisual == 8 || CapturaVisual == 17 || CapturaVisual == 24 || CapturaVisual == 32) {
                cbComponentesEmisiones.Text = "NO CONTIENE " + elemento;
                cbComponentesEmisiones.Visible = despliegue;
                cbComponentesEmisiones.Enabled = despliegue;
            } else if (CapturaVisual == 4 || CapturaVisual == 15 || CapturaVisual == 22 || CapturaVisual == 30) {
                cbFugasMotorTrans.Text = "NO CONTIENE " + elemento;
                cbFugasMotorTrans.Visible = despliegue;
                cbFugasMotorTrans.Enabled = despliegue;
            } else if (CapturaVisual == 7 || CapturaVisual == 16 || CapturaVisual == 23 || CapturaVisual == 31) {
                cbNeumaticos.Text = "NO CONTIENE " + elemento;
                cbNeumaticos.Visible = despliegue;
                cbNeumaticos.Enabled = despliegue;
            } else if (CapturaVisual == 4 || CapturaVisual == 13 || CapturaVisual == 20 || CapturaVisual == 28) { 
                cbPortaFiltroAire.Text = "NO CONTIENE " + elemento;
                cbPortaFiltroAire.Visible = despliegue;
                cbPortaFiltroAire.Enabled = despliegue;
            } else if (CapturaVisual == 5 || CapturaVisual == 9 || CapturaVisual == 14 || CapturaVisual == 21 || CapturaVisual == 29) {
                cbTuboEscape.Text = "NO CONTIENE " + elemento;
                cbTuboEscape.Visible = despliegue;
                cbTuboEscape.Enabled = despliegue;
            } else if (CapturaVisual == 1 || CapturaVisual == 25) {
                cbTaponCombustible.Text = "NO CONTIENE " + elemento;
                cbTaponCombustible.Visible = despliegue;
                cbTaponCombustible.Enabled = despliegue;
            } else if (CapturaVisual == 3 || CapturaVisual == 12 || CapturaVisual == 9 || CapturaVisual == 27) {
                cbTaponAceite.Text = "NO CONTIENE " + elemento;
                cbTaponAceite.Visible = despliegue;
                cbTaponAceite.Enabled = despliegue;
            } else if (CapturaVisual == 10) {
                cbMotorGobernado.Text = "NO CONTIENE " + elemento;
                cbMotorGobernado.Visible = despliegue;
                cbMotorGobernado.Enabled = despliegue;
            }
        }


        private void btnSiguente_Click(object sender, EventArgs e) {

        }


        #region SQL
        #region primer store 
        private async Task<VerificacionVisualIniResult> GetAccesoSQLVerificaciones(string SERVER, string DB, string SQL_USER, string SQL_PASS,
            string appName, string APPROLE, string APPROLE_PASS, Guid estacionId, Guid AccesoId) {
            
            int _mensaje = 0;
            short _resultado = 0;
            Guid _verificacion = Guid.Empty;
            byte _protocoloVerificacionId = (byte)0;
            string _placa = "DESCONOCIDO";

            var repo = new SivevRepository();

            try {
                using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                await connApp.OpenAsync();
                using (var scope = new AppRoleScope(connApp, APPROLE, APPROLE_PASS)) {
                    try {

                        var rinicial = await repo.SpAppVerificacionVisualIniAsync( 
                            conn:connApp,
                            estacionId:estacionId, 
                            accesoId:AccesoId
                            );

                        _resultado = rinicial.Resultado;
                        _mensaje = rinicial.MensajeId;
                        _verificacion = rinicial.VerificacionId;
                        _protocoloVerificacionId = rinicial.ProtocoloVerificacionId;
                        _placa = rinicial.PlacaId;


                        if (_mensaje != 0) {
                            var error = await repo.PrintIfMsgAsync(connApp, $"MensajeId {_mensaje}", _mensaje);
                            MostrarMensaje($"Error en SpAppVerificacion_Visual_Ini_Async MensajeId = {_mensaje}: {error.Mensaje}");
                            return new VerificacionVisualIniResult {
                                MensajeId = _mensaje,
                                Resultado = _resultado,
                                VerificacionId = Guid.Empty,
                                ProtocoloVerificacionId = 0,
                                PlacaId = "DESCONOCIDO"
                            };
                        }
                    } catch (Exception ex) {
                        MostrarMensaje($"Error en Get_Acceso_SQL_Verificaciones {ex.Message}");
                    }
                }
            } catch (Exception e) {
                MostrarMensaje($"Error en Get_Acceso_SQL_Verificaciones {e.Message}");
            }

            return new VerificacionVisualIniResult {
                MensajeId = 0,
                Resultado = 0,
                VerificacionId = _verificacion,
                ProtocoloVerificacionId = _protocoloVerificacionId,
                PlacaId = _placa
            };
        }

        #endregion

        private async Task<CapturaVisualGetResult> BanderasAEvaluar(
                                                                        string SERVER,
                                                                        string DB,
                                                                        string SQL_USER,
                                                                        string SQL_PASS,
                                                                        string appName,
                                                                        string APPROLE,
                                                                        string APPROLE_PASS,
                                                                        Guid estacionId,
                                                                        Guid AccesoId,
                                                                        Guid verificacionId,
                                                                        string? elemento,
                                                                        byte combustible
                                                                    ) {
            var repo = new SivevRepository();
            var result = new CapturaVisualGetResult();   

            try {
                using var connApp = SqlConnectionFactory.Create(SERVER, DB, SQL_USER, SQL_PASS, appName);
                await connApp.OpenAsync();

                using (var scope = new AppRoleScope(connApp, APPROLE, APPROLE_PASS)) {
                    try {
                        var rbanderas = await repo.SpAppCapturaVisualGetAsync(
                                                                                conn: connApp,
                                                                                estacionId: estacionId,
                                                                                accesoId: AccesoId,
                                                                                verificacionId: verificacionId,
                                                                                elemento: elemento,
                                                                                tiCombustible: combustible
                                                                            );

                        // Copiamos valores al objeto result
                        result.Resultado = rbanderas.Resultado;
                        result.MensajeId = rbanderas.MensajeId;
                        result.ReturnCode = rbanderas.ReturnCode;
                        result.Items.AddRange(rbanderas.Items);

                        if (result.MensajeId != 0) {
                            var error = await repo.PrintIfMsgAsync( connApp, $"Error en SpAppCapturaVisualGetAsync MensajeId {result.MensajeId}",result.MensajeId);
                            var msg = error?.Mensaje ?? "Mensaje no disponible";
                            MostrarMensaje($"Error en SpAppCapturaVisualGetAsync MensajeId = {result.MensajeId}: {msg}");
                        }
                    } catch (Exception ex) {
                        MostrarMensaje($"Error en Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.SpAppCapturaVisualGetAsync: {ex.Message}");
                        result.MensajeId = -1;
                        result.Resultado = -1;
                        result.ReturnCode = -1;
                    }
                }
            } catch (Exception e) {
                MostrarMensaje($"Error en Apps_Visual.ObdAppGUI.Views.frmCapturaVisual.BanderasAEvaluar: {e.Message}");
                result.MensajeId = -2;
                result.Resultado = -2;
                result.ReturnCode = -2;
            }

            return result;
        }


        #region utils
        private void MostrarMensaje(string mensaje) {
            using (var dlg = new frmMensajes(mensaje)) {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.TopMost = true;
                dlg.ShowDialog(this);
            }
        }
        #endregion
        #endregion

    }
}
