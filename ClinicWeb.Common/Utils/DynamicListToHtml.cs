using ClinicWeb.Common.Dynamic;
using System.Text;

namespace ClinicWeb.Common.Utils
{
    public static class DynamicListToHtml
    {
        private static List<int> blobIndis = new List<int>();
        public static string ToHtmlTable(List<DynamicGridColumn> columns, List<object[]> rows)
        {
            var ret = new StringBuilder();
            ret.Append("<table id='dynamicTable' class='table'> ");
            ret.Append("<thead>");
            ret.Append(ToColumnHeaders(columns));
            ret.Append("</thead><tbody>");
            ret.Append(ToHtmlTableRow(rows));
            ret.Append("</tbody></table>");
            blobIndis.Clear();
            return ret.ToString();
        }
        private static string ToColumnHeaders(List<DynamicGridColumn> colums)
        {
            var builder = new StringBuilder("<tr>");
            var i = 0;
            foreach (var item in colums)
            {
                if (item.Type== "byte" || item.Type=="byte[]")
                {
                    blobIndis.Add(i);
                }
                builder.Append("<th>" + item.ColumnName + "</th>");
                i++;
            }
            builder.Append("</tr>");
            return builder.ToString();
        }
        private static string ToHtmlTableRow(IList<object[]> rows)
        {
            var html = new StringBuilder();
            var i = 0;
            if (!rows.Any()) return html.ToString();
            foreach (var row in rows)
            {
                html.Append("<tr>");

                foreach (var item in row)
                {
                    if (!blobIndis.Contains(i))
                    {
                        html.Append("<td>&nbsp;" + item + " </td>");
                    }
                    else
                    {
                        html.Append("<td>Blob değer önizleme yapılamıyor.</td>");
                    }
                    i++;
                }
                html.Append("</tr>");
            }
            return html.ToString();
        }
    }
}

