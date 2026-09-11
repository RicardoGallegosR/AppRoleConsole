using SQLSIVEV.Infrastructure.Sql.Vicente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmComun.CapturaCentralizada {
    public partial class ucRegistroVehicular : UserControl {

        private readonly SivevConnectionFactory _sql;
        private readonly string _roll;
        private readonly string _passRoll;
        private readonly short _opcionMenu;
        private readonly Guid _estacionId;
        private readonly short _centro;

        public ucRegistroVehicular(SivevConnectionFactory sql, string roll, string passRoll, short opcionMenu, Guid estacionId, short centro) {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _roll = roll ?? throw new ArgumentNullException(nameof(roll));
            _passRoll = passRoll.ToUpper() ?? throw new ArgumentNullException(nameof(passRoll));
            _opcionMenu = opcionMenu;
            _estacionId = estacionId;
            _centro = centro;

            InitializeComponent();
        }
    }
}
