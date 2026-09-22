using DocumentFormat.OpenXml.Bibliography;
using FrmComun.CapturaCentralizada.Complementos;
using FrmComun.Utils;
using SQLSIVEV.Domain.Models;
using SQLSIVEV.Infrastructure.Sql;
using SQLSIVEV.Infrastructure.Sql.Vicente;
using SQLSIVEV.Infrastructure.Utils;

namespace FrmComun.CapturaCentralizada {
    public partial class ucRegistroVehicular : UserControl {

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
        private Guid _verificacionAnteriorId;


        private enum EtapaCaptura {
            Inicio = 0,
            Visitante = 1,
            Acceso = 2,
            Vehiculo = 3,
            TarjetaCirculacion = 4,
            Documentos = 5,
            Escaneo = 6,
            Finalizado = 7
        }
        public ucRegistroVehicular(SivevConnectionFactory sql, string roll, string passRoll, short opcionMenu, Guid estacionId, Guid accesoId, short centro) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = (passRoll ?? throw new ArgumentNullException(nameof(passRoll))).ToUpperInvariant();
            _opcionMenu = opcionMenu;
            _estacionId = estacionId;
            _accesoId = accesoId;
            _centro = centro;

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
            gbAcciones.Visible = false;

            tcPrincipal.TabPages.Remove(tpTC);
            tcPrincipal.TabPages.Remove(tpDocumentosAdicionales);


            ucAccesoConsulta1.CrearVerificacion += ucAccesoConsulta1_CrearVerificacion;
            Load += ucRegistroVehicular_Load;
            ucVinModelo1.ConsultarVin += ucVinModelo1_ConsultarVin;
            ucSeleccionVehiculo1.Seleccionar += ucSeleccionVehiculo1_Seleccionar;

            ucTarjetaCirculacion1.Editar += btnEditarTarjetaCirculacion_Click;
            ucTarjetaCirculacion1.Guardar += btnGuardarTarjetaCirculacion_Click;

        }

       
        
        private async void ucRegistroVehicular_Load(object? sender, EventArgs e) {
            try {
                await _catalogos.CargarAsync();
                ucTarjetaCirculacion1.CargarMarcas(_catalogos.Marcas);
                ucTarjetaCirculacion1.CargarCombustibles(_catalogos.Combustibles);
                ucTarjetaCirculacion1.FiltroSubmarcaChanged += Tarjeta_FiltroSubmarcaChanged;

                ucTarjetaCirculacion1.SubmarcaSeleccionada += ucTarjetaCirculacion1_SubmarcaSeleccionada;

                BeginInvoke(() => {
                    txtPlaca.Focus();
                    txtPlaca.SelectAll();
                });
            } catch (Exception ex) {
                Mostrar.Mensaje( "Error al cargar catálogos", ex.ToString());
                SivevLogger.Warning($"No fue posible cargar los catálogos: {ex}", SivevOrigen.Captura);
            }
        }

        private void txtPlaca_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                IniciarCaptura();
            }
        }
        private void ucTarjetaCirculacion1_SubmarcaSeleccionada(object? sender, EventArgs e) {
            int? submarcaId = ucTarjetaCirculacion1.SubmarcaId;

            if (submarcaId is null)
                return;

            // continuar flujo...
        }

        private void IniciarCaptura() {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text)) {
                Mostrar.Mensaje("Error", "Debe ingresar una placa válida.");
                txtPlaca.Clear();
                txtPlaca.Focus();
                txtPlaca.Text = string.Empty;
                return;
            }
            //FlujoGrama(EtapaCaptura.Visitante);
            ucVisitante1.EnfocarNombre();
                     
        }

        private void ucVisitante1_Acceso(object? sender, EventArgs e) {
            string nombre = ucVisitante1.Nombre;
            string apellidoP = ucVisitante1.ApellidoPaterno;
            string apellidoM = ucVisitante1.ApellidoMaterno;
            
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) || string.IsNullOrWhiteSpace(apellidoM)) {
                Mostrar.Mensaje("Error", "Debe ingresar el nombre completo del visitante.");
                ucVisitante1.EnfocarNombre();
                return;
            }
            //FlujoGrama(EtapaCaptura.Acceso);
            gbAcceso.Visible = true;
            ucAccesoConsulta1.Placa = txtPlaca.Text.Trim();

        }

        





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

        #region Crear verificacion 
        private async void ucAccesoConsulta1_CrearVerificacion(object? sender, CrearVerificacionEventArgs e) {
            var datos = e.Datos;
            try {
                await _sqlExecutor.EjecutarAsync(async connApp => {
                    var repo = new SivevRepository();

                    var r = await repo.SpAppCapturaIniciaWebSrvNewAsync(
                        cnn: connApp,
                        estacionId: _estacionId,
                        accesoId: _accesoId,
                        placa: txtPlaca.Text.Trim(),

                        pet: cbPET.Checked,
                        
                        // Son datos que vienen de la consulta de acceso, no del formulario de captura
                        consultasSemoviId: datos.ConsultasSemoviId,

                        vin: datos.Vin,
                        modelo: datos.Modelo,
                        tipoServicio: datos.TipoServicio,
                        folioAuto: datos.FolioAuto,
                        fechaTC: datos.FechaTC,

                        testFM: datos.TestFM,

                        conexionWs: datos.ConexionWs,
                        conexionWebSrv: datos.ConexionWebSrv,

                        adeudoFotoCivicas: datos.AdeudoFotoCivicas,
                        adeudoTenencia: datos.AdeudoTenencia,
                        adeudoInfraccion: datos.AdeudoInfraccion,

                        gdfNoRegistrado: datos.GdfNoRegistrado
                    );

                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp, $"Error en SpAppCapturaIniciaWebSrvNew {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al iniciar verificación", error.Mensaje);
                        return;
                    }
                    if (r.VerificacionId is null) {
                        Mostrar.Mensaje("Error","No se recibió un identificador de verificación.");
                        return;
                    }
                    _verificacionId = r.VerificacionId.Value;
                    //Mostrar.Mensaje("Verificación iniciada", $"Se ha iniciado la verificación con ID: {_verificacionId}");
                    //FlujoGrama(EtapaCaptura.Vehiculo);
                    ucAccesoConsulta1.HabilitarCrearVerificacion(false);
                    gbVinModelo.Visible = true;
                });
            } catch (Exception ex) {
                Mostrar.Mensaje("Error al iniciar verificación", ex.Message);
                SivevLogger.Error($"SpAppCapturaIniciaWebSrvNew: {ex}", SivevOrigen.Captura);
            }
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
                        Mostrar.Mensaje("Error" , $"Al obtener el listado de documentos adicionales\n{error.Mensaje}");
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
        private async void ucVinModelo1_ConsultarVin(object? sender,  EventArgs e) {
            try {
                if (_verificacionId == Guid.Empty) {
                    Mostrar.Mensaje("Error", "Primero debe iniciar la verificación.");
                    return;
                }

                await _sqlExecutor.EjecutarAsync( async connApp => {
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
                        Mostrar.Mensaje("Error al consultar VIN", error.Mensaje);
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
                Mostrar.Mensaje("Error al consultar VIN", ex.Message);
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
                        var error = await repo.PrintIfMsgAsync(connApp,  $"Error en SpAppCapturaVinModeloSet {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al consultar VIN", error.Mensaje);
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
                    Mostrar.Mensaje("Error","Debe seleccionar una submarca.");
                    SivevLogger.Warning("No se encontro vericacón anterior", SivevOrigen.Captura);
                    return;
                }
                if (ucTarjetaCirculacion1.CombustibleId is null) {
                    Mostrar.Mensaje("Error", "Debe seleccionar un combustible.");
                    SivevLogger.Warning("No seleccionaron el combustible.", SivevOrigen.Captura);
                    return;

                }
                if (string.IsNullOrWhiteSpace(ucTarjetaCirculacion1.ClaveVehicular)) {
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

                await _sqlExecutor.EjecutarAsync(async connApp =>  {
                    var r = await repo.SpAppCapturaDatosSetAsync(
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
                        imagenTarjetaCirculacion: new byte[] { 0x00, 0x00, 0x00 }
                    );
                    
                    if (r.MensajeId != 0) {
                        var error = await repo.PrintIfMsgAsync(connApp,  $"Error en SpAppCapturaDatosSet {r.MensajeId}", r.MensajeId);
                        Mostrar.Mensaje("Error al guardar tarjeta de circulación", error.Mensaje);
                        SivevLogger.Error($"Error al guardar tarjeta de circulación\n{error.Mensaje}", SivevOrigen.Captura);
                        ucTarjetaCirculacion1.EstablecerModoEdicion(true);
                        ucTarjetaCirculacion1.EsperarStore(true);
                        return;
                    }else {
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


        #endregion

    }
}
