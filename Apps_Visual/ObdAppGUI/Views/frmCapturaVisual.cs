using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apps_Visual.UI.Theme;
//clbPrincipal

namespace Apps_Visual.ObdAppGUI.Views {
    public partial class frmCapturaVisual : Form {
        public frmCapturaVisual() {
            InitializeComponent();

            
        }

        private void frmCapturaVisual_Load(object sender, EventArgs e) {
        }

        private void clbPrincipal_keyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;

                var clb = (CheckedListBox)sender;
                int index = clb.SelectedIndex;

                if (index >= 0) {
                    bool isChecked = clb.GetItemChecked(index);
                    clb.SetItemChecked(index, !isChecked);
                }
            }
        }

        private void clbPrincipal_ItemCheck(object sender, ItemCheckEventArgs e) {
            var clb   = (CheckedListBox)sender;
            string txt = clb.Items[e.Index].ToString() ?? "";

            if (e.NewValue == CheckState.Checked) {
                if (txt.StartsWith("NO "))
                    txt = txt.Substring(3);
            } else if (e.NewValue == CheckState.Unchecked) {
                if (!txt.StartsWith("NO "))
                    txt = "NO " + txt;
            }

            clb.Items[e.Index] = txt;
            clb.Invalidate(); 
        }

        
    }
}
