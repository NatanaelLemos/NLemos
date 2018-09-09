using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NLemos.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateHash(this string text)
        {
            text = FormatString(text);
            text = text.Replace(" ", "").Replace(",", "").Replace("/", "").Replace("-", "");
            text = RemoveDiacritics(text);
            return text.Trim().ToLower();
        }

        static string FormatString(string text)
        {
            if (text.StartsWith("\""))
            {
                text = text.Substring(1);
            }

            if (text.EndsWith("\""))
            {
                text = text.Substring(0, text.Length - 1);
            }

            return text.Trim();
        }

        static string RemoveDiacritics(string text)
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
