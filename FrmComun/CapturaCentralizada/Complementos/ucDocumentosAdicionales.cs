using SQLSIVEV.Domain.Models;
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
        public event EventHandler? AgregarClick;




        public void CargarDocumentos(IEnumerable<DocumentoAdicionalDto> documentos) {
            dgvDocumentos.DataSource = null;
            dgvDocumentos.DataSource = documentos.ToList();

            dgvDocumentos.ClearSelection();
        }

        public ucDocumentosAdicionales() {
            InitializeComponent();
            ConfiguracionDGV();

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

            dgvDocumentos.Columns.Clear();

            dgvDocumentos.Columns.Add(
                new DataGridViewTextBoxColumn {
                    Name = "TipoDocumento",
                    HeaderText = "Tipo de Documento",
                    DataPropertyName = nameof(DocumentoAdicionalDto.TituloFolioDocumento),
                    FillWeight = 130,
                    ReadOnly = true
                });

            dgvDocumentos.Columns.Add(
                new DataGridViewTextBoxColumn {
                    Name = "Referencia",
                    HeaderText = "Referencia",
                    DataPropertyName = nameof(DocumentoAdicionalDto.Referencia),
                    FillWeight = 100,
                    ReadOnly = true
                });

            dgvDocumentos.Columns.Add(
                new DataGridViewTextBoxColumn {
                    Name = "ValorReferencia",
                    HeaderText = "Valor de Referencia",
                    DataPropertyName = nameof(DocumentoAdicionalDto.ValorReferencia),
                    FillWeight = 120
                });

            dgvDocumentos.Columns.Add(
                new DataGridViewTextBoxColumn {
                    Name = "Fecha",
                    HeaderText = "Fecha",
                    DataPropertyName = nameof(DocumentoAdicionalDto.Fecha),
                    FillWeight = 80,
                    DefaultCellStyle = new DataGridViewCellStyle {
                        Format = "yyyy/MM/dd",
                        Alignment = DataGridViewContentAlignment.MiddleCenter

                    }
                });

            dgvDocumentos.Columns.Add(
                new DataGridViewCheckBoxColumn {
                    Name = "NoPresenta",
                    HeaderText = "No Presenta",
                    DataPropertyName = nameof(DocumentoAdicionalDto.NoPresenta),
                    FillWeight = 65
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
