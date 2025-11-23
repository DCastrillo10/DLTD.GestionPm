using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Comun
{
    public static class LetraCapital
    {
        // Convierte a Proper Case
        public static string ToProper(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            var words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            return string.Join(' ', words);
        }

        // Convierte a mayúsculas
        public static string ToUpperSafe(string s) => s?.ToUpper() ?? string.Empty;
    }
}
