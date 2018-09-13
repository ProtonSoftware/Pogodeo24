using System.Globalization;
using System.Text;

namespace Pogodeo.Services
{
    /// <summary>
    /// Helper extensions to deal with strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Normalizes the string to plain english alphabet letters
        /// </summary>
        /// <param name="obj">The string to normalize</param>
        /// <returns>Normalized string</returns>
        public static string ToNormalizedString(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
