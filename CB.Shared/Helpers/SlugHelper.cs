using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CB.Shared.Helpers
{
    public static class SlugHelper
    {
        private static readonly Dictionary<char, string> CustomReplacements = new()
        {
            ['ə'] = "e",
            ['Ə'] = "E",
            ['ö'] = "o",
            ['Ö'] = "O",
            ['ü'] = "u",
            ['Ü'] = "U",
            ['ş'] = "s",
            ['Ş'] = "S",
            ['ç'] = "c",
            ['Ç'] = "C",
            ['ğ'] = "g",
            ['Ğ'] = "G",
            ['ı'] = "i",
            ['I'] = "I"
        };

        private static string GenerateSlugInternal(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var ch in phrase)
            {
                if (CustomReplacements.ContainsKey(ch))
                    sb.Append(CustomReplacements[ch]);
                else
                    sb.Append(ch);
            }

            string normalized = sb.ToString().Normalize(NormalizationForm.FormD);
            var chars = normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            string cleaned = new string(chars).Normalize(NormalizationForm.FormC);

            string lower = cleaned.ToLowerInvariant();

            string slug = Regex.Replace(lower, @"[^a-z0-9\s-]", "");

            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

            slug = Regex.Replace(slug, @"-+", "-");

            return slug;
        }

        public static Dictionary<string, string> GenerateSlugs(Dictionary<string, string> titles)
        {
            var result = new Dictionary<string, string>();
            foreach (var kv in titles)
            {
                result[kv.Key] = GenerateSlugInternal(kv.Value);
            }
            return result;
        }
    }

}
