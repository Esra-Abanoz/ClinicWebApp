using System.Text.RegularExpressions;

namespace ClinicWeb.Common.Utils
{
    public static class HtmlTemplateGenerator
    {
        public static string Adisoyadi { get; set; }
        public static string GetProcessHtml(string html)
        {
            Regex regex = new Regex("@{(?<tagName>[^}]+)}");
            return regex.Replace(html, ProcessHtmlTag);
        }
        private static string ProcessHtmlTag(Match m)
        {
            var tagName = m.Groups["tagName"].Value;
            var endTagPrefix = "";
            if (tagName.StartsWith("/"))
            {
                endTagPrefix = "/";
                tagName = tagName.Substring(1);
            }
            switch (tagName)
            {
                case "adisoyadi":
                    tagName = Adisoyadi.ToUpper();
                    break;
            }
            return endTagPrefix + tagName.ToLower();
        }
    }
}