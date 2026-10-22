using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace loader_utils
{
    public static partial class DocumentHelper
    {
        public static string GetNextRegNum(int len = 6)
        {
            StringBuilder builder = new();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(len)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString().ToUpper();
        }

        public static List<string> SubjectCodeCheck(string subjectCode)
        {
            var warnings = new List<string>();

            if (subjectCode != null)
            {
                if (!SubjectCodeRegex().IsMatch(subjectCode))
                {
                    warnings.Add($"Код субъекта {subjectCode} содержит одновременно буквы русского и латинского алфавита");
                }

                if (subjectCode.Length < 4)
                {
                    warnings.Add($"Длина кода субъекта {subjectCode} меньше 4 символов");
                }

                if (subjectCode.Length > 15)
                {
                    warnings.Add($"Длина кода субъекта {subjectCode} больше 15 символов");
                }
            }
            return warnings;
        }

        public static string GetSchemaVersion(XmlReader reader)
        {
            try
            {
                while (reader.SafeRead())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        return reader.GetAttribute("schemaVersion");
                    }
                }
            }
            catch
            {
                return "";
            }
            return "";
        }

        [GeneratedRegex(@"^([0-9а-яё\s]+|[0-9a-z\s]+)$", RegexOptions.IgnoreCase, "ru-RU")]
        private static partial Regex SubjectCodeRegex();
    }
}
