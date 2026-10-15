using System.Text.RegularExpressions;

namespace SQLSIVEV.Infrastructure.Utils {

    public static class WebServicesHelp {
        private static readonly Regex UnicodeRegex = new(
            @"\\?u([0-9a-fA-F]{4})", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase
            );
        public static string DecodificarUnicode(string texto) {

            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            return UnicodeRegex.Replace(texto, match => { 
                int codigo = Convert.ToInt32(match.Groups[1].Value, 16);
                return ((char)codigo).ToString();
            });
        }
    }
}