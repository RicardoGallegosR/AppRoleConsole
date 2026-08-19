using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apps_Administrativa.Paneles.FisicoMecanica {
    public partial class CargarCertificados : UserControl {
        private readonly BindingList<FisicoMecanicaRegistro> _registros = new();
        private readonly BindingSource _bsRegistros = new();
        private readonly Color _eliminarNormal = Color.FromArgb(190, 24, 60);   // #BE183C
        private readonly Color _eliminarHover = Color.FromArgb(220, 20, 60);   // Crimson #DC143C

        private readonly Color _guardarNormal = Color.FromArgb(46, 125, 50);   // #2E7D32
        private readonly Color _guardarHover = Color.FromArgb(67, 160, 71);   // #43A047
        private readonly Color _guardarPresionado = Color.FromArgb(27, 94, 32);    // #1B5E20
        public CargarCertificados() {
            InitializeComponent();
            ConfigurarDgvRegistro();

            _bsRegistros.DataSource = _registros;
            dgvRegistro.DataSource = _bsRegistros;

        }

        #region Configurar DataGridView
        private void ConfigurarDgvRegistro() {
            dgvRegistro.AutoGenerateColumns = false;
            dgvRegistro.Columns.Clear();

            dgvRegistro.AllowUserToAddRows = false;
            dgvRegistro.AllowUserToDeleteRows = true;
            dgvRegistro.AllowUserToResizeRows = false;
            dgvRegistro.RowHeadersVisible = false;
            dgvRegistro.MultiSelect = false;
            dgvRegistro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRegistro.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvRegistro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvRegistro.BackgroundColor = Color.White;
            dgvRegistro.BorderStyle = BorderStyle.FixedSingle;
            dgvRegistro.EnableHeadersVisualStyles = false;
            dgvRegistro.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 54, 93);
            dgvRegistro.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRegistro.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvRegistro.ColumnHeadersHeight = 38;
            dgvRegistro.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgvRegistro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvRegistro.ScrollBars = ScrollBars.Both;

            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Placa), "Placa", 110);
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.VIN), "VIN", 180);
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Folio), "Folio", 85);
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Centro),"Centro",75);
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Semana),"Semana", 75, soloLectura: true);
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Fecha),"Fecha", 105, formato: "yyyy/MM/dd");
            AgregarColumnaTexto(nameof(FisicoMecanicaRegistro.Hora),"Hora", 90, formato: @"hh\:mm\:ss");
            AgregarColumnaCheckBox(nameof(FisicoMecanicaRegistro.Participa), "Participó", 75);
            AgregarColumnaCombo<TipoServicio>(nameof(FisicoMecanicaRegistro.Servicio), "Servicio", 110);
            AgregarColumnaCombo<TipoEstatus>(nameof(FisicoMecanicaRegistro.Estatus), "Estatus", 115);
            /*
            AgregarBloqueRevision("Dirección", nameof(FisicoMecanicaRegistro.DireccionN1),nameof(FisicoMecanicaRegistro.DireccionN2),nameof(FisicoMecanicaRegistro.DireccionN3));
            AgregarBloqueRevision("Suspensión",nameof(FisicoMecanicaRegistro.SuspensionN1), nameof(FisicoMecanicaRegistro.SuspensionN2), nameof(FisicoMecanicaRegistro.SuspensionN3));
            AgregarBloqueRevision("Frenos",nameof(FisicoMecanicaRegistro.FrenosN1),nameof(FisicoMecanicaRegistro.FrenosN2), nameof(FisicoMecanicaRegistro.FrenosN3));
            AgregarBloqueRevision("Equipo de seguridad",nameof(FisicoMecanicaRegistro.EquipoDeSeguridadN1), nameof(FisicoMecanicaRegistro.EquipoDeSeguridadN2), nameof(FisicoMecanicaRegistro.EquipoDeSeguridadN3));
            AgregarBloqueRevision("Parabrisas",nameof(FisicoMecanicaRegistro.ParabrisasYLimpiaparabrisasN1), nameof(FisicoMecanicaRegistro.ParabrisasYLimpiaparabrisasN2), nameof(FisicoMecanicaRegistro.ParabrisasYLimpiaparabrisasN3));
            AgregarBloqueRevision("Cristales", nameof(FisicoMecanicaRegistro.CristalesLateralesYTraseroN1), nameof(FisicoMecanicaRegistro.CristalesLateralesYTraseroN2), nameof(FisicoMecanicaRegistro.CristalesLateralesYTraseroN3));
            AgregarBloqueRevision("Luces delanteras",nameof(FisicoMecanicaRegistro.LucesDelanterasN1),nameof(FisicoMecanicaRegistro.LucesDelanterasN2), nameof(FisicoMecanicaRegistro.LucesDelanterasN3));
            AgregarBloqueRevision("Luces traseras", nameof(FisicoMecanicaRegistro.LucesTraserasN1),nameof(FisicoMecanicaRegistro.LucesTraserasN2),nameof(FisicoMecanicaRegistro.LucesTraserasN3));
            AgregarBloqueRevision("Carrocería", nameof(FisicoMecanicaRegistro.CarroceriaN1),nameof(FisicoMecanicaRegistro.CarroceriaN2),nameof(FisicoMecanicaRegistro.CarroceriaN3));
            AgregarBloqueRevision("Aire acondicionado", nameof(FisicoMecanicaRegistro.AireAcondicionadoN1), nameof(FisicoMecanicaRegistro.AireAcondicionadoN2), nameof(FisicoMecanicaRegistro.AireAcondicionadoN3));
            AgregarBloqueRevision("Llantas",nameof(FisicoMecanicaRegistro.LlantasN1), nameof(FisicoMecanicaRegistro.LlantasN2), nameof(FisicoMecanicaRegistro.LlantasN3));
            AgregarBloqueRevision("Puertas", nameof(FisicoMecanicaRegistro.PuertasN1), nameof(FisicoMecanicaRegistro.PuertasN2), nameof(FisicoMecanicaRegistro.PuertasN3));
            AgregarBloqueRevision("Taxímetro", nameof(FisicoMecanicaRegistro.TaximetroN1),nameof(FisicoMecanicaRegistro.TaximetroN2), nameof(FisicoMecanicaRegistro.TaximetroN3));
            // Mantener visibles algunas columnas importantes.
            */
            dgvRegistro.Columns[nameof(FisicoMecanicaRegistro.Placa)].Frozen = true;
            dgvRegistro.Columns[nameof(FisicoMecanicaRegistro.VIN)].Frozen = true;
            dgvRegistro.Columns[nameof(FisicoMecanicaRegistro.Folio)].Frozen = true;

            dgvRegistro.DataError += dgvRegistro_DataError;
            dgvRegistro.CellEndEdit += dgvRegistro_CellEndEdit;
        }
        private void AgregarBloqueRevision(string titulo,string propiedadN1, string propiedadN2, string propiedadN3) {
            AgregarColumnaCombo<TipoN>(propiedadN1, $"{titulo} N1", 105);
            AgregarColumnaCombo<TipoN>(propiedadN2, $"{titulo} N2", 105);
            AgregarColumnaCombo<TipoN>(propiedadN3, $"{titulo} N3", 105);
        }
        private void AgregarColumnaCheckBox(string propiedad,string encabezado,int ancho) {
            var columna = new DataGridViewCheckBoxColumn {
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                Width = ancho,
                ThreeState = false,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRegistro.Columns.Add(columna);
        }
        private void dgvRegistro_DataError(object? sender, DataGridViewDataErrorEventArgs e) {
            e.ThrowException = false;
            dgvRegistro.Rows[e.RowIndex].ErrorText = "El valor ingresado no es válido.";
        }
        private void AgregarColumnaTexto(string propiedad,string encabezado, int ancho, bool soloLectura = false,   string? formato = null) {
            var columna = new DataGridViewTextBoxColumn{
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                Width = ancho,
                ReadOnly = soloLectura,
                SortMode = DataGridViewColumnSortMode.Automatic
            };

            if (!string.IsNullOrWhiteSpace(formato)) {
                columna.DefaultCellStyle.Format = formato;
            }

            dgvRegistro.Columns.Add(columna);
        }
        private void AgregarColumnaCombo<TEnum>( string propiedad, string encabezado, int ancho)  where TEnum : struct, Enum {
            var columna = new DataGridViewComboBoxColumn {
                Name = propiedad,
                DataPropertyName = propiedad,
                HeaderText = encabezado,
                Width = ancho,
                DataSource = Enum.GetValues<TEnum>(),
                ValueType = typeof(TEnum),
                DisplayStyle =
            DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat
            };

            dgvRegistro.Columns.Add(columna);
        }
        private void dgvRegistro_CellEndEdit(object? sender,DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string propiedad = dgvRegistro.Columns[e.ColumnIndex].DataPropertyName;

            if (propiedad == nameof(FisicoMecanicaRegistro.Fecha)) {
                _bsRegistros.ResetCurrentItem();
            }
        }
        #endregion

        private void btnAgregar_Click(object sender, EventArgs e) {
            var nuevoRegistro = new FisicoMecanicaRegistro {
                Centro = 9903,
                Fecha = DateTime.Today,
                Hora = DateTime.Now.TimeOfDay,
                Participa = true,
                Servicio = TipoServicio.Taxi,
                Estatus = TipoEstatus.Aprobado
            };
            _registros.Add(nuevoRegistro);
            int ultimaFila = _registros.Count - 1;
            dgvRegistro.CurrentCell = dgvRegistro.Rows[ultimaFila].Cells[nameof(FisicoMecanicaRegistro.Placa)];
            dgvRegistro.BeginEdit(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            if (dgvRegistro.CurrentRow?.DataBoundItem is not FisicoMecanicaRegistro registro) {
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Eliminar registro",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (respuesta == DialogResult.Yes) {
                _registros.Remove(registro);
            }
        }
        private void btnGuardarRegistro_Click(object sender, EventArgs e) {

        }

        private void btnAgregar_MouseEnter(object sender, EventArgs e) {
            btnAgregar.BackColor = ColorTranslator.FromHtml("#2E75B6");
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e) {
            btnAgregar.BackColor = ColorTranslator.FromHtml("#1F4E79");
        }

        private void btnEliminar_MouseEnter(object sender, EventArgs e) {
            btnEliminar.BackColor = _eliminarHover;
        }

        private void btnEliminar_MouseLeave(object sender, EventArgs e) {
            btnEliminar.BackColor = _eliminarNormal;
        }
        private void btnGuardarRegistro_MouseEnter(object sender, EventArgs e) {
            btnGuardarRegistro.BackColor = _guardarHover;
        }

        private void btnGuardarRegistro_MouseLeave(object sender, EventArgs e) {
            btnGuardarRegistro.BackColor = _guardarNormal;
        }

        private void btnGuardarRegistro_MouseDown(object sender, MouseEventArgs e) {
            btnGuardarRegistro.BackColor = _guardarPresionado;
        }

        private void btnGuardarRegistro_MouseUp(object sender, MouseEventArgs e) {
            btnGuardarRegistro.BackColor = _guardarHover;
        }
    }
}
