using System.Text.RegularExpressions;

namespace GestaoDeConcessionaria.Application.Extensions
{
    public static class StringExtensions
    {
        public static string SomenteDigitos(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return Regex.Replace(input, @"[^\d]", "");
        }
    }
}
