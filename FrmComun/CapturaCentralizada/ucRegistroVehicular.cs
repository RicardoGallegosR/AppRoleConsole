using DocumentFormat.OpenXml.Bibliography;
using FrmComun.CapturaCentralizada.Complementos;
using FrmComun.Utils;
using SQLSIVEV.Comun.Configuracion;
using SQLSIVEV.Comun.Servicios;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Services;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;
using System.Text.RegularExpressions;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrmComun.CapturaCentralizada {
    public partial class ucRegistroVehicular : UserControl {
        #region Declaraciones de variables
        private bool _capturaCancelada;
        private readonly SivevConnectionFactory _sql;
        private readonly string _roll;
        private readonly string _passRoll;
        private readonly short _opcionMenu;
        private readonly Guid _estacionId;
        private readonly Guid _accesoId;
        private readonly short _centro;
        private readonly CatalogosCaptura _catalogos;
        private readonly AppRoleSqlExecutor _sqlExecutor;
        private Guid _verificacionId;
        private Guid _empresaPersonaId;
        private Guid _BiatacoraVisitaId;
        private Guid _verificacionAnteriorId;
        private string _rutaDocumentos;
        private readonly VerificentroApiConfig _verificentroApiConfig;
        private string _CentroDomain =string.Empty;
        public event Action? CancelacionSolicitada;
        public event Action? CapturaFinalizada;
        private ConsultaFotocivicasDto? _datosFotocivicas;
        public event Action<bool>? CapturaCancelada;
        public bool Captura { get; private set; } = false;
        private FisicoMecanicaDto? _datosFisicoMecanica;
        private CitasDto? _datosCitas;


        private void UcAccesoConsulta_FotocivicasConsultadas(object? sender, ConsultaFotocivicasDto datos) {
            _datosFotocivicas = datos;
        }

        #endregion
        #region Constructor
        public ucRegistroVehicular(SivevConnectionFactory sql, string roll, string passRoll, short opcionMenu, Guid estacionId, Guid accesoId, short centro, string ruta, VerificentroApiConfig verificentroApiConfig, string centroDomain) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = (passRoll ?? throw new ArgumentNullException(nameof(passRoll))).ToUpperInvariant();
            _opcionMenu = opcionMenu;
            _estacionId = estacionId;
            _accesoId = accesoId;
            _centro = centro;
            if (!centroDomain.Equals("AO-91")) {
                _CentroDomain = centroDomain.Replace("-", "");
            } else {
                _CentroDomain = centroDomain;
            }
            _rutaDocumentos = ruta;
            _verificentroApiConfig = verificentroApiConfig;



            InitializeComponent();

            _sqlExecutor = new AppRoleSqlExecutor(_sql, _roll, _passRoll);
            txtPlaca.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtPlaca, @"[^A-HJ-NPR-Z0-9]");
            txtPlaca.MaxLength = 11;
            txtLinea.TextChanged += (s, ev) => Expresiones.SanitizeByRegex(txtLinea, @"[^1-7]");
            txtPlaca.Focus();
            //FlujoGrama(EtapaCaptura.Inicio);
            _catalogos = new CatalogosCaptura(sql: _sql, roll: _roll, passRoll: _passRoll, estacion: _estacionId, accesoId: _accesoId, opcionMenu: _opcionMenu, centro: _centro);

            ucVisitante1.Acceso += ucVisitante1_Acceso;

            gbVinModelo.Visible = false;
            gbAcceso.Visible = false;
            gbVisitante.Visible = false;

            tcPrincipal.TabPages.Remove(tpTC);
            tcPrincipal.TabPages.Remove(tpDocumentosAdicionales);


            Load += ucRegistroVehicular_Load;
            ucVinModelo1.ConsultarVin += ucVinModelo1_ConsultarVin;
            ucSeleccionVehiculo1.Seleccionar += ucSeleccionVehiculo1_Seleccionar;

            ucTarjetaCirculacion1.Editar += btnEditarTarjetaCirculacion_Click;
            ucTarjetaCirculacion1.Guardar += btnGuardarTarjetaCirculacion_Click;
            ucEscaneoDocumentos1.GuardarClick += ucEscaneoDocumentos1_GuardarClick;



            ucAccesoConsulta1.Configurar(_verificentroApiConfig, _CentroDomain);
            ucAccesoConsulta1.CapturaCancelada += ucAccesoConsulta1_CapturaCancelada;
            ucAccesoConsulta1.CrearVerificacion += ucAccesoConsulta1_CrearVerificacion;
            ucAccesoConsulta1.FotocivicasConsultadas += UcAccesoConsulta_FotocivicasConsultadas;
            ucAccesoConsulta1.FisicoMecanicaConsultada += datosFM => {
                RecibirFisicoMecanica(datosFM);
            };
            ucAccesoConsulta1.CitasConsultada += datosCitas => {
                RecibirCitas(datosCitas);
            };




        }
        private void RecibirFisicoMecanica(FisicoMecanicaDto datosFM) {
            _datosFisicoMecanica = datosFM;
        }
        private void RecibirCitas(CitasDto datosCitas) {
            _datosCitas = datosCitas;
        }
        #endregion

        #region Load y eventos de captura
        private async void ucRegistroVehicular_Load(object? sender, EventArgs e) {
            try {
                await _catalogos.CargarAsync();
                ucTarjetaCirculacion1.CargarMarcas(_catalogos.Marcas);
                ucTarjetaCirculacion1.CargarCombustibles(_catalogos.Combustibles);
                ucTarjetaCirculacion1.FiltroSubmarcaChanged += Tarjeta_FiltroSubmarcaChanged;
                ucTarjetaCirculacion1.SubmarcaSeleccionada += ucTarjetaCirculacion1_SubmarcaSeleccionada;
                ucAccesoConsulta1.CargarMotivosAcceso(_catalogos.MotivosAcceso);


                BeginInvoke(() => {
                    txtPlaca.Focus();
                    txtPlaca.SelectAll();
                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al cargar catálogos", ex.ToString());
                SivevLogger.Warning($"No fue posible cargar los catálogos: {ex}", SivevOrigen.Captura);
            }
        }
        #endregion


        private void txtPlaca_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                txtPlaca.Enabled = false;
                IniciarCaptura();
            }
        }
        private void ucTarjetaCirculacion1_SubmarcaSeleccionada(object? sender, EventArgs e) {
            int? submarcaId = ucTarjetaCirculacion1.SubmarcaId;

            if (submarcaId is null)
                return;

            // continuar flujo...
        }
        #region INICIAR CAPTURA, REVISAMOS SI LA PLACA TIENE EL FORMATO Y ESTE EN PERIODO
        private void IniciarCaptura() {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text)) {
                Mostrar.Mensaje("Error", "Debe ingresar una placa válida.");
                txtPlaca.Clear();
                txtPlaca.Focus();
                txtPlaca.Text = string.Empty;
                return;
            }
            gbVisitante.Visible = true;
            ucVisitante1.EnfocarNombre();
        }
        #endregion

        #region METER AQUI EL STORE PARA GUARDAR EL VISITANTE Y OBTENER EL ID DE ACCESO
        private async void ucVisitante1_Acceso(object? sender, EventArgs e) {
            string nombre = ucVisitante1.Nombre;
            string apellidoP = ucVisitante1.ApellidoPaterno;
            string apellidoM = ucVisitante1.ApellidoMaterno;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) || string.IsNullOrWhiteSpace(apellidoM)) {
                Mostrar.Mensaje("Error", "Debe ingresar el nombre completo del visitante.");
                ucVisitante1.EnfocarNombre();
                return;

            }
            ucVisitante1.RegistoActivado();
            // CONFIGURACION DE AUDEUDOS
            gbAcceso.Visible = true;

            ucAccesoConsulta1.Placa = txtPlaca.Text.Trim();
            ucAccesoConsulta1._bitacoraVisitaId = _BiatacoraVisitaId;
            ucAccesoConsulta1._estacionId = _estacionId;
            ucAccesoConsulta1._accesoId = _accesoId;
            ucAccesoConsulta1._sqlExecutor = _sqlExecutor;
            ucAccesoConsulta1.HabilitarCrearVerificacion();
            ucAccesoConsulta1.MotivosAccesoDefecto();
            ucAccesoConsulta1.HabilitarBotonBusqueda();
            await ucAccesoConsulta1.ConsultarCitasYAdesdos();
            ucAccesoConsulta1.HabilitarCrearVerificacion(true);
        }
        #endregion





        #region Tarjeta de Circulacion cambio de submarca
        private async void Tarjeta_FiltroSubmarcaChanged(object? sender, EventArgs e) {
            int? marcaId = ucTarjetaCirculacion1.MarcaId;
            int modelo = ucTarjetaCirculacion1.Modelo;

            if (marcaId is null) {
                ucTarjetaCirculacion1.CargarSubmarcas(Array.Empty<SubmarcaDto>());
                return;
            }

            await _catalogos.CargarSubmarcasAsync(marcaId.Value, modelo);
            ucTarjetaCirculacion1.CargarSubmarcas(_catalogos.Submarcas);
        }
        #endregion
        /*
            REVISAR PARA CREAR LA VERIFICACION, 
            YA QUE AHORA SE CREA DESDE EL MODULO DE ACCESO, 
            Y NO DESDE EL FORMULARIO DE CAPTURA
         */






        #region Crear verificacion 
        private async void ucAccesoConsulta1_CrearVerificacion(object? sender, CrearVerificacionEventArgs e) {
            var datos = e.Datos;
            ucAccesoConsulta1.RegistoActivado();
            _datosFotocivicas.MensajeTenencia = WebServicesHelp.DecodificarUnicode(_datosFotocivicas.MensajeTenencia ?? string.Empty);
            
            string nombre = ucVisitante1.Nombre;
            string apellidoP = ucVisitante1.ApellidoPaterno;
            string apellidoM = ucVisitante1.ApellidoMaterno;
            
            try {
                Cursor = Cursors.WaitCursor;
                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var repo = new SivevRepository();

                    var r = await repo.SpAppCapturaIniciaWebSrv2026Async(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        placa: txtPlaca.Text.Trim(),
                        pet: ucAccesoConsulta1.PET,
                        respuesta: _datosFotocivicas
                    );


                   
                    if (r.MensajeId != 0 && r.MensajeId != 50789) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"Error en SpAppCapturaIniciaWebSrv2026 {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al iniciar verificación", error.Mensaje);
                        if (r.MensajeId == 50777) {
                            ucAccesoConsulta1_CapturaCancelada(true);
                        }
                        return;
                    }
                    if (r.MensajeId != 0 && r.MensajeId == 50789) {
                        string detalleAdeudos = ConstruirMensajeAdeudos(_datosFotocivicas);

                        Mostrar.Mensaje("Acceso rechazado", detalleAdeudos);
                        ucAccesoConsulta1_CapturaCancelada(true);
                        return;
                    }
                    
                    if (r.VerificacionId is null) {
                        Mostrar.Mensaje("Error", "No se recibió un identificador de verificación.");
                        return;
                    }
                    _verificacionId = r.VerificacionId.Value;

                    //-- Guardar en bitácora de visitas


                    var rBitacora = await repo.SpAppCapturaBitacoraVisitasSet(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        Nombre: nombre,
                        Paterno: apellidoP,
                        Materno: apellidoM,
                        MotivoAccesoId: ucAccesoConsulta1.byMotivoAccesoId ?? (byte)0,
                        VerificacionId: _verificacionId
                    );


                    if (rBitacora.MensajeId != 0) {
                        var errorBitacora = await repo.MensajeIdSQL(connApp, $"Error en SpAppCapturaBitacoraVisitasSet {rBitacora.MensajeId}", rBitacora.MensajeId);
                        Mostrar.Mensaje("Error al registrar al ciudadano", $"MensajeId: {errorBitacora.MensajeId}, Mensaje: {errorBitacora.Mensaje}");
                        CancelacionSolicitada?.Invoke();
                        return;
                    }
                    if (rBitacora.Data is BitacoraVisitasDto bitacoraVisita && bitacoraVisita.BitacoraVisitaId != Guid.Empty) {
                        _empresaPersonaId = bitacoraVisita.EmpresaPersonaId;
                    }
                    if (rBitacora.Data is BitacoraVisitasDto bitacoraVisita2 && bitacoraVisita2.EmpresaPersonaId != Guid.Empty) {
                        _BiatacoraVisitaId = bitacoraVisita2.BitacoraVisitaId;
                    }

                    // Guardar citas en bitácora de Citas
                    var rcitas = await repo.SpAppBitacoraCitasSWSet(cnn: connApp, estacionId: _estacionId, VerificacionId: _verificacionId,accesoId: _accesoId, bitacoraVisitaId: _BiatacoraVisitaId, citas:_datosCitas);
                    if (rcitas.MensajeId != 0) {
                        var error = await repo.MensajeIdSQL(connApp, $"Error en SpAppBitacoraCitasSWSet {rcitas.MensajeId}", rcitas.MensajeId);
                        Mostrar.Mensaje("Error al consultar el servicio de citas", error.Mensaje);
                        return;
                    }

                    ucAccesoConsulta1.HabilitarCrearVerificacion(false);
                    gbVinModelo.Visible = true;
                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al iniciar verificación", ex.Message);
                SivevLogger.Error($"SpAppCapturaIniciaWebSrvNew: {ex}", SivevOrigen.Captura);
            } finally {
                Cursor = Cursors.Default;
            }

            switch (_datosCitas.codigoRespuesta) {
                // CASOS QUE NO PUEDEN CONTINUAR
                case "V03":
                case "V05":
                    Mostrar.Mensaje("Acceso rechazado", $"Centro: {_datosCitas.centro}\nFecha: {_datosCitas.fechaCita} {_datosCitas.horaCita}\n\n{_datosCitas.mensaje}");
                    ucAccesoConsulta1_CapturaCancelada(true);
                    return;


                // CASOS QUE SÍ PUEDEN CONTINUAR
                case "V06":
                case "S01":
                    break;


                // RESPUESTA NO CONTEMPLADA
                default:
                    SivevLogger.Warning($"Respuesta de citas no reconocida:\n{_datosCitas.codigoRespuesta}\n\n{_datosCitas.mensaje}", SivevOrigen.Captura);
                    Mostrar.Mensaje("Advertencia", $"Respuesta de citas no reconocida:\n{_datosCitas.codigoRespuesta}\n\n{_datosCitas.mensaje}");
                    return;
            }




        }
        private static string ConstruirMensajeAdeudos(ConsultaFotocivicasDto datos) {
            var mensajes = new List<string>();

            if (datos.AdeudoTenencia) {
                string tenencias = string.Join(", ", datos.Tenencias);
                string mensaje = WebServicesHelp.DecodificarUnicode(datos.MensajeTenencia ?? string.Empty).Replace("|", Environment.NewLine);
                mensajes.Add($"ADEUDO DE TENENCIA\nEjercicio(s): {(string.IsNullOrWhiteSpace(tenencias) ? "No especificados" : tenencias)}\n{mensaje}");
            }
            if (datos.AdeudoSancion) {
                int sancionesPendientes = datos.Sanciones?.Count ?? 0;
                mensajes.Add($"SANCIONES AMBIENTALES\nRegistros reportados: {sancionesPendientes}");
            }
            
            int economicasPendientes = datos.Economicas?.Count(x => string.Equals(x.Status, "NO PAGADA",StringComparison.OrdinalIgnoreCase)) ?? 0;
            if (datos.AdeudoInfraccion || economicasPendientes > 0) {
                mensajes.Add($"INFRACCIONES ECONÓMICAS\nPendientes: {economicasPendientes}");
            }

            if (datos.Fotocivicas?.TieneAdeudo == true) {
                var foto = datos.Fotocivicas;
                mensajes.Add(
                    $"FOTOCÍVICAS\n" +
                    $"Puntos: {foto.Puntos}\n" +
                    $"Curso en línea: {(foto.DebeEnLinea ? "Sí" : "No")}\n" +
                    $"Curso presencial: {(foto.DebePresencial ? "Sí" : "No")}\n" +
                    $"Amonestaciones: {(foto.DebeAmonestaciones ? "Sí" : "No")}"
                );
            }
            return mensajes.Count > 0 ? string.Join($"{Environment.NewLine}{Environment.NewLine}",mensajes) : "Finanzas reportó una condición de adeudo, pero no proporcionó detalles adicionales.";
        }
        #endregion

        #region Buscar Documentos Adicionales  
        private async void BuscarDocumentos() {
            try {
                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var repo = new SivevRepository();

                    var r = repo.SpAppCapturaDocumentosAdicionalesGet(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                       verificacionId:_verificacionId
                    );

                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"Error en SpAppCapturaDocumentosAdicionalesGet {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error", $"Al obtener el listado de documentos adicionales\n{error.Mensaje}");
                        return;
                    }
                    foreach (var documento in r.Data) {
                        if (documento.TipoDocumentoId == 14) {
                            documento.Fecha = ucTarjetaCirculacion1.FechaTC;
                            documento.ValorReferencia = ucTarjetaCirculacion1.FolioTarjetaCirculacion;
                        } else {
                            documento.Fecha = DateTime.Today;
                            documento.ValorReferencia = string.Empty;
                        }
                    }

                    ucDocumentosAdicionales1.CargarDocumentos(r.Data);
                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error obtener el listado de documentos adicionales", ex.Message);
                SivevLogger.Error($"SpAppCapturaDocumentosAdicionalesGet: {ex}", SivevOrigen.Captura);
            }
        }
        #endregion

        #region VIN Modelo
        private async void ucVinModelo1_ConsultarVin(object? sender, EventArgs e) {
            try {
                if (_verificacionId == Guid.Empty) {
                    Mostrar.Mensaje("Error", "Primero debe iniciar la verificación.");
                    return;
                }
                ucVinModelo1.HabilitarModulo(false);
                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var repo = new SivevRepository();

                    var r = await repo.SpAppCapturaVinModeloSetAsync(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        verificacionId: _verificacionId,
                        vin: ucVinModelo1.Vin,
                        odometro: 0
                    );
                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp,  $"Error en SpAppCapturaVinModeloSet {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al capturar VIN", error.Mensaje);
                        SivevLogger.Warning($"Error al capturar VIN, {error.Mensaje}", SivevOrigen.Captura);
                        return;
                    }

                    if (r.Modelo <= 0) {
                        Mostrar.Mensaje("VIN", "No se obtuvo un modelo válido.");
                        return;
                    }
                    ucVinModelo1.EstablecerModelo(r.Modelo);
                    ucSeleccionVehiculo_ConsultarVin();
                    MostrarTabTC();

                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al capturar VIN", ex.Message);
                SivevLogger.Error($"SpAppCapturaVinModeloSet: {ex}", SivevOrigen.Captura);
            }
        }
        private void MostrarTabTC() {
            if (!tcPrincipal.TabPages.Contains(tpTC)) {
                tcPrincipal.TabPages.Add(tpTC);
            }
            tcPrincipal.SelectedTab = tpTC;
        }
        #endregion

        #region Verificacion anterior
        private async void ucSeleccionVehiculo_ConsultarVin() {
            try {
                if (_verificacionId == Guid.Empty) {
                    Mostrar.Mensaje("Error", "Primero debe iniciar la verificación.");
                    return;
                }
                var repo = new SivevRepository();

                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var r = await repo.SpAppCapturaVerificacionesAnterioresGetAsync(cnn: connApp, estacionId: _estacionId, accesoId: _accesoId, verificacionId: _verificacionId);

                    if (r.MensajeId != 0) {
                        var error = await repo.MensajeIdSQL(connApp,  $"Error al consultar verificaciones anteriores {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al consultar verificaciones anteriores", error.Mensaje);
                        return;
                    }
                    ucSeleccionVehiculo1.CargarVehiculos(r.Data);
                });

            } catch (Exception ex) {
                Mostrar.Mensaje("Error al consultar la verificación anterior", ex.Message);
                SivevLogger.Error($"SpAppCapturaVerificacionesAnterioresGetAsync: {ex}", SivevOrigen.Captura);
            }
        }


        #region Seleccionar vehiculo
        private void ucSeleccionVehiculo1_Seleccionar(object? sender, EventArgs e) {
            var vehiculo = ucSeleccionVehiculo1.VehiculoSeleccionado;

            if (vehiculo is null)
                return;

            _verificacionAnteriorId = vehiculo.VerificacionAntId;

            string vin = vehiculo.Vin;
            int marcaId = vehiculo.MarcaId;
            int submarcaId =  vehiculo.SubMarcaId;
            int modelo = vehiculo.Modelo;
            byte combustibleId = vehiculo.CombustibleId;


            //Mostrar.Mensaje("DataSet", $"vin: {vin}\nMarcaId: {marcaId}\nSubmarca: {submarcaId}\nModelo: {modelo}\nCombustible: {combustibleId}");


            ucTarjetaCirculacion1.SeleccionarMarca(marcaId);
            ucTarjetaCirculacion1.SeleccionarSubMarca(submarcaId);
            ucTarjetaCirculacion1.EstablecerModelo(modelo);
            ucTarjetaCirculacion1.SeleccionarCombustible(combustibleId);
            if (vehiculo.ApelPaterno.Equals("DESCONOCIDO") && vehiculo.ApelMaterno.Equals("DESCONOCIDO")) {
                ucTarjetaCirculacion1.EstablecerPersonaMoral(vehiculo.Nombre);
            } else {
                ucTarjetaCirculacion1.EstablecerPersonaFisica(vehiculo.Nombre, vehiculo.ApelPaterno, vehiculo.ApelMaterno);
            }
            ucTarjetaCirculacion1.CargarTarjetaForlio(vehiculo.TarjetaFolio);
            ucTarjetaCirculacion1.CargarFechaTC(vehiculo.TarjetaFecha.Value);
        }
        #endregion
        #region Editar Tarjeta de Circulacion 
        private void btnEditarTarjetaCirculacion_Click(object? sender, EventArgs e) {
            ucTarjetaCirculacion1.EstablecerModoEdicion(true);
            ucTarjetaCirculacion1.EnfocarNombre();
        }
        #endregion
        #region Guardar Tarjeta de Circulacion 
        private async void btnGuardarTarjetaCirculacion_Click(object? sender, EventArgs e) {
            try {
                if (_verificacionId == Guid.Empty) {
                    Mostrar.Mensaje("Error", "Primero debe iniciar la verificación.");
                    SivevLogger.Warning("Se saltaron la creación de verificación.", SivevOrigen.Captura);
                    return;
                }
                if (_verificacionAnteriorId == Guid.Empty) {
                    Mostrar.Mensaje("Error", "Debe seleccionar una verificación anterior.");
                    SivevLogger.Warning("No hay verificación anterior.", SivevOrigen.Captura);
                    return;
                }
                if (ucTarjetaCirculacion1.SubmarcaId is null) {
                    Mostrar.Mensaje("Error", "Debe seleccionar una submarca.");
                    SivevLogger.Warning("No se encontro vericacón anterior", SivevOrigen.Captura);
                    return;
                }
                if (ucTarjetaCirculacion1.CombustibleId is null) {
                    Mostrar.Mensaje("Error", "Debe seleccionar un combustible.");
                    SivevLogger.Warning("No seleccionaron el combustible.", SivevOrigen.Captura);
                    return;

                }
                if (ucTarjetaCirculacion1.ClaveVehicular is null) {
                    Mostrar.Mensaje("Error", "Debe capturar la clave vehicular");
                    SivevLogger.Warning("No registraron la clave vehicular", SivevOrigen.Captura);
                    return;
                }


                bool esEmpresa = !ucTarjetaCirculacion1.EsPersonaFisica;
                string apellidoPaterno = esEmpresa ? "DESCONOCIDO" : ucTarjetaCirculacion1.ApellidoPaterno;
                string apellidoMaterno = esEmpresa ? "DESCONOCIDO" : ucTarjetaCirculacion1.ApellidoMaterno;

                // Bloquear mientras se ejecuta el store
                ucTarjetaCirculacion1.EsperarStore();

                var repo = new SivevRepository();

                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var r = await repo.SpAppCapturaDatosSet2026Async(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        verificacionId: _verificacionId,
                        verificacionAntId: _verificacionAnteriorId,

                        submarcaId:  ucTarjetaCirculacion1.SubmarcaId.Value,
                        combustibleId: ucTarjetaCirculacion1.CombustibleId.Value,
                        esEmpresa: esEmpresa,
                        nombre: ucTarjetaCirculacion1.Nombre,
                        apelPaterno: apellidoPaterno,

                        apelMaterno: apellidoMaterno,
                        tarjetaFolio: ucTarjetaCirculacion1.FolioTarjetaCirculacion,
                        tarjetaFecha: ucTarjetaCirculacion1.FechaTC,
                        tubosEscape: checked((short) ucTarjetaCirculacion1.TubosEscape),
                        imagenFactura: new byte[] { 0x00, 0x00, 0x00 },
                        imagenTarjetaCirculacion: new byte[] { 0x00, 0x00, 0x00 },
                        ClaveVehicular: ucTarjetaCirculacion1.ClaveVehicular ?? 0
                    );

                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp,  $"Error en SpAppCapturaDatosSet {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al guardar tarjeta de circulación", error.Mensaje);
                        SivevLogger.Error($"Error al guardar tarjeta de circulación\n{error.Mensaje}", SivevOrigen.Captura);
                        ucTarjetaCirculacion1.EstablecerModoEdicion(true);
                        ucTarjetaCirculacion1.EsperarStore(true);
                        return;
                    } else {
                        // Si llegamos aquí, se guardó correctamente.
                        MostrartpDocumentosAdicionales();
                        ucTarjetaCirculacion1.EsperarStore();

                        BuscarDocumentos();
                        //Mostrar.Mensaje("Tarjeta de circulación", "Los datos se guardaron correctamente.");
                        SivevLogger.Information("Tarjeta de circulación\nLos datos se guardaron correctamente.", SivevOrigen.Captura);
                    }
                });
            } catch (Exception ex) {
                // Si hubo excepción, permitir corregir/reintentar.
                ucTarjetaCirculacion1.EstablecerModoEdicion(true);
                Mostrar.Mensaje("Error al guardar tarjeta de circulación", ex.Message);
                SivevLogger.Error($"SpAppCapturaDatosSetAsync: {ex}", SivevOrigen.Captura);
            }
        }

        private void MostrartpDocumentosAdicionales() {
            if (!tcPrincipal.TabPages.Contains(tpDocumentosAdicionales)) {
                tcPrincipal.TabPages.Add(tpDocumentosAdicionales);
            }
            tcPrincipal.SelectedTab = tpDocumentosAdicionales;
        }
        #endregion
        #region Ruta PDF
        private async void ucEscaneoDocumentos1_GuardarClick(object? sender, EventArgs e) {
            try {
                ucEscaneoDocumentos1.VerificacionId = _verificacionId;
                ucEscaneoDocumentos1.RutaBase = _rutaDocumentos;
                string ruta = ucEscaneoDocumentos1.GuardarPdf();

                var repo = new SivevRepository();

                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var r = await repo.SpAppCapturaObtenerLineaNew(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        verificacionId: _verificacionId
                    );

                    if (r.MensajeId is not (0 or 53323)) {
                        var error = await repo.MensajeIdSQL(connApp,  $"Error en SpAppCapturaObtenerLineaNew {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al Obtener Linea", error.Mensaje);
                        return;
                    }
                    txtLinea.Text = r.Data.Linea.ToString();
                    Mostrar.Mensaje("Documento guardado", $"El documento se guardó correctamente\nNúmero de línea: {txtLinea.Text}");
                    await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId: _estacionId, _AccesoId: _accesoId);
                    CapturaFinalizada?.Invoke();
                });
        } catch (Exception ex) {
                Mostrar.Mensaje("Error", $"No se pudo guardar el documento.\n\n{ex.Message}");
                SivevLogger.Error($"Error al guardar documento PDF: {ex}", SivevOrigen.Captura);
            }
        }
        #endregion

        #endregion





        #region Cancelar prueba 

        private void ucAccesoConsulta1_CapturaCancelada(bool cancelar) {
            if (!cancelar)
                return;
            CancelacionSolicitada?.Invoke();
        }

        public async Task<bool> CancelarAccesoAsync(CancellationToken ct = default) {
            var repo = new SivevRepository();
            try {
                return await _sqlExecutor.EjecutarAsync(async connApp => {
                    var r = await repo.SpAppCapturaAbandona2026Async(
                        connApp,
                        uiAccesoId: _accesoId,
                        uiEstacionId: _estacionId,
                        uiVerificacionId: _verificacionId,
                        ct: ct
                    );

                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp,"SpAppCapturaAbandona2026Async", r.MensajeId);
                        var bitacora = Bitacora.ErroresSQL(
                            estacionId: _estacionId,
                            centro: _centro,
                            opcionMenuId: _opcionMenu,
                            descripcion: $"Error al cancelar captura: {error.Mensaje}",
                            codigoSql: r.MensajeId
                        );

                        await repo.SpSpAppBitacoraErroresSetAsync2026(connApp, bitacora, ct);

                        Mostrar.Mensaje("Cancelar captura", error.Mensaje);

                        //return false;
                    }
                    await repo.SpAppAccesoFinAsync(conn: connApp, _EstacionId: _estacionId, _AccesoId: _accesoId);
                    return true;
                }, ct);

            } catch (Exception ex) {
                SivevLogger.Error($"Error en CancelarAccesoAsync: {ex}", SivevOrigen.Captura);
                Mostrar.Mensaje("Error al cancelar captura", ex.Message);
                return false;
            }
        }

        #endregion


    }
}
