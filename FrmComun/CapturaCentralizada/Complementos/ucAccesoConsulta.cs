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
    public partial class ucAccesoConsulta : UserControl {
        public event EventHandler<CrearVerificacionEventArgs>? CrearVerificacion;
        public string Monetarias {
            get => lblStatusMonetarias.Text;
            set => lblStatusMonetarias.Text = value;
        }

        public string Ambientales {
            get => lblStatusAmbientales.Text;
            set => lblStatusAmbientales.Text = value;
        }

        public string Tenencias {
            get => lblStatusTenencias.Text;
            set => lblStatusTenencias.Text = value;
        }

        public string Fotocivicas {
            get => lblStatusFotocivicas.Text;
            set => lblStatusFotocivicas.Text = value;
        }

        public string Placa {
            get => txtPlaca.Text;
            set => txtPlaca.Text = value;
        }

        public void HabilitarCrearVerificacion(bool habilitar = true) {
            btnCrearVerificacion.Enabled = habilitar;
        }

        public ucAccesoConsulta() {
            InitializeComponent();
            btnCrearVerificacion.Click += btnCrearVerificacion_Click;
        }

        private void btnCrearVerificacion_Click(object? sender, EventArgs e) {
            var datos = new DatosInicioVerificacion();
            CrearVerificacion?.Invoke(this, new CrearVerificacionEventArgs(datos));
        }

        public void CargarMotivosAcceso(IEnumerable<MotivosAccesoDto> motivosAccesos) {
            cbMotivoAcceso.DataSource = null;
            cbMotivoAcceso.DisplayMember = nameof(MotivosAccesoDto.MotivoAcceso);
            cbMotivoAcceso.ValueMember = nameof(MotivosAccesoDto.MotivoAccesoId);
            cbMotivoAcceso.DataSource = motivosAccesos.ToList();
            //cbMarcas.SelectedIndex = -1;
        }
        

    }
}
