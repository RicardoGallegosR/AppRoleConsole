using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmComun.CapturaCentralizada.Complementos {
    public partial class ucDocumentosAdicionales : UserControl {
        private DataGridViewTextBoxColumn colTipoDocumento;
        private DataGridViewTextBoxColumn colReferencia;
        private DataGridViewTextBoxColumn colValorReferencia;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewCheckBoxColumn colNoPresenta;
        private DataGridViewImageColumn colEstado;
        private DataGridViewButtonColumn colEscanear;



        public event EventHandler? EscanearClick;
        public event EventHandler? AgregarClick;


        public ucDocumentosAdicionales() {
            InitializeComponent();
            ConfiguracionDGV();

            btnEscanear.Click += (s, e) => EscanearClick?.Invoke(this, e);
            btnAgregar.Click += (s, e) => AgregarClick?.Invoke(this, e);
        }

        private void ConfiguracionDGV() {
            dgvDocumentos.AutoGenerateColumns = false;
            dgvDocumentos.AllowUserToAddRows = false;
            dgvDocumentos.AllowUserToDeleteRows = false;
            dgvDocumentos.AllowUserToResizeRows = false;

            dgvDocumentos.RowHeadersVisible = false;
            dgvDocumentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocumentos.MultiSelect = false;

            dgvDocumentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDocumentos.BackgroundColor = Color.White;
            dgvDocumentos.BorderStyle = BorderStyle.None;


            dgvDocumentos.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "TipoDocumento",
                HeaderText = "Tipo de Documento",
                FillWeight = 130
            });

            dgvDocumentos.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Referencia",
                HeaderText = "Referencia",
                FillWeight = 100
            });

            dgvDocumentos.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "ValorReferencia",
                HeaderText = "Valor de Referencia",
                FillWeight = 120
            });

            dgvDocumentos.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Fecha",
                HeaderText = "Fecha",
                FillWeight = 80
            });

            dgvDocumentos.Columns.Add(new DataGridViewCheckBoxColumn {
                Name = "NoPresenta",
                HeaderText = "No Presenta",
                FillWeight = 65
            });

            dgvDocumentos.Columns.Add(new DataGridViewButtonColumn {
                Name = "Escanear",
                HeaderText = "",
                Text = "Escanear",
                UseColumnTextForButtonValue = true,
                FillWeight = 75
            });

        }

        private void dgvDocumentos_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0)
                return;

            if (dgvDocumentos.Columns[e.ColumnIndex].Name == "Escanear") {
                var row = dgvDocumentos.Rows[e.RowIndex];

                string tipoDocumento =
            row.Cells["TipoDocumento"].Value?.ToString() ?? "";
            }
            // Activar tu panel de escaneo
            // ucEscaneoDocumento...
        }
    }
}
