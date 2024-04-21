using ClinicWeb.UI.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace ClinicWeb.UI.Helpers
{
    public static class HtmlTemplateGenerator
    {
        public static string Adisoyadi { get; set; }
        public static string Yenisifre { get; set; }
        public static IHttpContextAccessor _context;
        public static string GetProcessHtml(string html, IHttpContextAccessor context)
        {
            Regex regex = new Regex("{(?<tagName>[^}]+)}");
            _context = context;
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
                case "tarih":
                    tagName = DateTime.Now.ToString();
                    break;
                case "ip":
                    tagName = GetClientIp();
                    break;
                case "sifre":
                    tagName = Yenisifre;
                    break;
            }
            return endTagPrefix + tagName.ToLower();
        }
        public static string GetClientIp()
        {
            return _context.HttpContext.GetClientIpAddress();
        }
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}