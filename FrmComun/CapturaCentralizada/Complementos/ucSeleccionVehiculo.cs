using FrmComun.Utils;
using SQLSIVEV.Domain.Models;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucSeleccionVehiculo : UserControl {
        public event EventHandler? NuevoVehiculo;
        public event EventHandler? Seleccionar;

        public bool PET;
        //public VerificacionAnteriorDto? VehiculoSeleccionado;

        public record ColumnaGrid(string Name, string Header, string DataProperty, int Width );
        public ucSeleccionVehiculo() {
            InitializeComponent();
            ConfigurarDgvVehiculos();
            CrearColumnas();

            btnSeleccionar.Click += btnSeleccionar_Click;
            dgvVehiculos.CellDoubleClick += dgvVehiculos_CellDoubleClick;


        }
        /*
        private void btnSeleccionar_Click(object sender, EventArgs e) =>
            Seleccionar?.Invoke(this, e);
        */

        public void CargarVehiculos(IEnumerable<VerificacionAnteriorDto> vehiculos) {
            dgvVehiculos.DataSource = null;
            dgvVehiculos.DataSource = vehiculos.ToList();

            dgvVehiculos.ClearSelection();
        }

        public VerificacionAnteriorDto? VehiculoSeleccionado {
            get {
                if (dgvVehiculos.CurrentRow?.DataBoundItem
                    is VerificacionAnteriorDto vehiculo) {
                    return vehiculo;
                }

                return null;
            }
        }

        private void btnSeleccionar_Click(object? sender, EventArgs e) {
            ConfirmarSeleccion();
        }

        private void dgvVehiculos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0)
                return;
            ConfirmarSeleccion();
        }

        private void ConfirmarSeleccion() {
            if (VehiculoSeleccionado is null) {
                Mostrar.Mensaje("Vehículo","Debe seleccionar un vehículo.");
                return;
            }
            Seleccionar?.Invoke(this,EventArgs.Empty);
        }


        #region Configuracion dgv
        private void CrearColumnas() {

            dgvVehiculos.Columns.Clear();

            dgvVehiculos.Columns.Add(
                new DataGridViewCheckBoxColumn {
                    Name = "Seleccion",
                    HeaderText = "",
                    Width = 35,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                }
            );

            foreach (var columna in ColumnasVehiculo) {

                dgvVehiculos.Columns.Add(
                    new DataGridViewTextBoxColumn {
                        Name = columna.Name,
                        HeaderText = columna.Header,
                        DataPropertyName = columna.DataProperty,
                        Width = columna.Width,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    }
                );
            }
        }
        private void ConfigurarDgvVehiculos() {
            //dgvVehiculos.ReadOnly = true;
            dgvVehiculos.AutoGenerateColumns = false;

            dgvVehiculos.AllowUserToAddRows = false;
            dgvVehiculos.AllowUserToDeleteRows = false;
            dgvVehiculos.AllowUserToResizeRows = false;

            dgvVehiculos.MultiSelect = false;

            dgvVehiculos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvVehiculos.RowHeadersVisible = false;

            dgvVehiculos.BackgroundColor = Color.White;
            dgvVehiculos.BorderStyle = BorderStyle.None;

            dgvVehiculos.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvVehiculos.GridColor = Color.FromArgb(225, 230, 235);

            //dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //dgvVehiculos.ScrollBars = ScrollBars.Both;

            dgvVehiculos.ColumnHeadersHeight = 38;
            dgvVehiculos.RowTemplate.Height = 34;

            dgvVehiculos.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle {
                    BackColor = Color.FromArgb(245, 247, 249),
                    ForeColor = Color.FromArgb(45, 55, 65),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    SelectionBackColor = Color.FromArgb(245, 247, 249),
                    SelectionForeColor = Color.FromArgb(45, 55, 65)
                };

            dgvVehiculos.DefaultCellStyle =
                new DataGridViewCellStyle {
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(45, 55, 65),
                    Font = new Font("Segoe UI", 9F),
                    SelectionBackColor = Color.FromArgb(218, 245, 229),
                    SelectionForeColor = Color.FromArgb(1, 118, 71),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };

            dgvVehiculos.EnableHeadersVisualStyles = false;
        }

        private static readonly ColumnaGrid[] ColumnasVehiculo = {
            new("Fecha",       "Fecha",       "Fecha",       130),
            new("Vencimiento", "Vencimiento", "Vencimiento", 130),
            new("Placa",       "Placa",       "Placa",        80),
            new("Vin",         "VIN",         "Vin",         170),
            new("Marca",       "Marca",       "Marca",        90),
            new("Submarca",    "Submarca",    "Submarca",    150),
            new("Modelo",      "Modelo",      "Modelo",        75),
            new("Combustible", "Combustible", "Combustible", 110),
            new("Propietario", "Propietario", "Propietario", 220)
        };
        #endregion
    }
}
