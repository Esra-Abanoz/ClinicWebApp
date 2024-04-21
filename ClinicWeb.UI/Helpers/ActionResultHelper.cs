using ClinicWeb.Common.SQLFactoring;
using Microsoft.AspNetCore.Mvc;
namespace ClinicWeb.UI.Helpers
{
    public class ActionResultHelper
    {
        public static JsonResult GridStoreLoad<T>(PagedList<T> list)
        {
            var total = list.TotalRecord;
            var records = list.Items.ToList();
            return new JsonResult(new { records, total });
        }
    }
    public static class HtmlHelperExtensions
    {
        public static string CurrentViewName(string html)
        {
            return System.IO.Path.GetFileNameWithoutExtension(html);
        }
    }
}
