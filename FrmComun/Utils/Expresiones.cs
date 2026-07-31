using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrmComun.Utils {
    public static class Expresiones {
        public static void SanitizeByRegex(TextBox tb, string invalidPattern) {
            string original = tb.Text;
            int sel = tb.SelectionStart;

            string cleaned = Regex.Replace(original, invalidPattern, "");

            if (original != cleaned) {
                int left = Math.Min(sel, original.Length);
                int removedLeft = Regex.Matches(original.Substring(0, left), invalidPattern).Count;

                tb.Text = cleaned;
                tb.SelectionStart = Math.Max(sel - removedLeft, 0);
            }
        }
    }
}
