using ClinicWeb.UI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ClinicWeb.UI.Controllers
{
    public class ResourcesController : Controller
    {
        private const string CssIconpath = "EXT/extneticons";
        private const string CssCustomIconpath = "EXT/extnetcustomicons";
        private const string CssIconprefix = "customicon";
        private const string CssCustomIconprefix = "custom";
        private static string[] _iconExtensions = new string[] { ".png" };
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ResourcesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult IconCssResource()
        {
            var assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var str = new StringBuilder();
            var pngFilesFolder = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssIconpath));
            if (pngFilesFolder.Exists)
            {
                var pngFiles = pngFilesFolder.GetFiles(string.Join("|", _iconExtensions.Select(x => string.Concat("*", x)).ToList()));
                if (pngFiles.Length > 0)
                {
                    foreach (var pngFile in pngFiles)
                    {
                        str.Append($".{CssIconprefix}-");
                        str.Append(Path.GetFileNameWithoutExtension(pngFile.Name));
                        str.Append("{background-image:url('");
                        str.Append(Url.Action("Icon", "Resources", new { iconFile = pngFile.Name, version = assemblyVersion }));
                        str.Append("')}");
                    }
                }
            }

            pngFilesFolder = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssCustomIconpath));
            if (pngFilesFolder.Exists)
            {
                var pngFiles = pngFilesFolder.GetFiles(string.Join("|", _iconExtensions.Select(x => string.Concat("*", x)).ToList()));
                if (pngFiles.Length > 0)
                {
                    foreach (var pngFile in pngFiles)
                    {
                        str.Append($".{CssIconprefix}-{CssCustomIconprefix}-");
                        str.Append(Path.GetFileNameWithoutExtension(pngFile.Name));
                        str.Append("{background-image:url('");
                        str.Append(Url.Action("CustomIcon", "Resources", new { iconFile = pngFile.Name, version = assemblyVersion }));
                        str.Append("')}");
                    }
                }
            }
            return new ContentResult
            {
                Content = str.ToString(),
                ContentType = "text/css",
                StatusCode = 200
            };
        }
        [HttpGet]
        public IActionResult IconCssResourceHtml()
        {
            var assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var str = new StringBuilder();
            str.Append("<!DOCTYPE html><html><head>");
            str.Append("<link rel='stylesheet' type='text/css' href='");
            str.Append(Url.Action("IconCssResource", "Resources"));
            str.Append("' />");
            str.Append("</head><body><table style='vertical-align: top;'><tr><td style='vertical-align: top;'><table border='1'>");
            var pngFilesFolder = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssIconpath));
            if (pngFilesFolder.Exists)
            {
                var pngFiles = pngFilesFolder.GetFiles(string.Join("|", _iconExtensions.Select(x => string.Concat("*", x)).ToList()));
                if (pngFiles.Length > 0)
                {
                    str.Append("<tr><td>Icon</td><td>Css</td></tr>");
                    foreach (var pngFile in pngFiles)
                    {
                        str.Append("<tr><td><img style='padding: 5px;' src='");
                        str.Append(Url.Action("Icon", "Resources", new { iconFile = pngFile.Name, version = assemblyVersion }));
                        str.Append("' /></td><td>");
                        str.Append($"{CssIconprefix}-");
                        str.Append(Path.GetFileNameWithoutExtension(pngFile.Name));
                        str.Append("</td></tr>");
                    }
                }
            }
            str.Append("</table></td><td style='vertical-align: top;'>");

            str.Append("<table border='1'>");
            pngFilesFolder = new DirectoryInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssCustomIconpath));
            if (pngFilesFolder.Exists)
            {
                var pngFiles = pngFilesFolder.GetFiles(string.Join("|", _iconExtensions.Select(x => string.Concat("*", x)).ToList()));
                if (pngFiles.Length > 0)
                {
                    str.Append("<tr><td>Custom Icon</td><td>Css</td></tr>");
                    foreach (var pngFile in pngFiles)
                    {
                        str.Append("<tr><td><img style='padding: 5px;' src='");
                        str.Append(Url.Action("CustomIcon", "Resources", new { iconFile = pngFile.Name, version = assemblyVersion }));
                        str.Append("' /></td><td>");
                        str.Append($"{CssIconprefix}-{CssCustomIconprefix}-");
                        str.Append(Path.GetFileNameWithoutExtension(pngFile.Name));
                        str.Append("</td></tr>");
                    }
                }
            }
            str.Append("</table></td></tr></table></body></html>");
            return new ContentResult
            {
                Content = str.ToString(),
                ContentType = MimeTypeHelper.GetMimeType(".html"),
                StatusCode = 200
            };
        }
        [HttpGet]
        public IActionResult Icon(string iconFile)
        {
            if (string.IsNullOrWhiteSpace(iconFile))
            {
                return NoContent();
            }
            var file = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssIconpath, iconFile));
            if (!file.Exists)
            {
                return NoContent();
            }
            return new PhysicalFileResult(file.FullName, MimeTypeHelper.GetMimeType(file.Extension));
        }
        [HttpGet]
        public IActionResult CustomIcon(string iconFile)
        {
            if (string.IsNullOrWhiteSpace(iconFile))
            {
                return NoContent();
            }
            var file = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, CssCustomIconpath, iconFile));
            if (!file.Exists)
            {
                return NoContent();
            }
            return new PhysicalFileResult(file.FullName, MimeTypeHelper.GetMimeType(file.Extension));
        }
    }
}
