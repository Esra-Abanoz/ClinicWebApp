using Microsoft.AspNetCore.Mvc.Razor;

namespace ClinicWeb.UI.Views
{
    public abstract class RazorPageViewBase : RazorPageViewBase<dynamic>
    {
    }
    public abstract class RazorPageViewBase<T> : RazorPage<T>
    {
        public string ActionName => ViewContext.RouteData.Values["action"].ToString();
        public string ControllerName => ViewContext.RouteData.Values["controller"].ToString();
        public string AreaName => ViewContext.RouteData.Values["area"] != null ? ViewContext.RouteData.Values["area"].ToString() : "";
        public string FullId
        {
            get
            {
                if (string.IsNullOrEmpty(AreaName))
                {
                    return string.Join("_",
                        new[] { ControllerName, ActionName }.Where(p => !string.IsNullOrWhiteSpace(p)));
                }
                else
                {
                    return string.Join("_",
                        new[] { AreaName, ControllerName, ActionName }.Where(p => !string.IsNullOrWhiteSpace(p)));
                }
            }
        }

        /// <summary>
        /// Proje debug modda mi derlenmiş
        /// </summary>
        public bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public string GetAppSetting(string settingKey)
        {
            //var confSection = _configuration.GetSection("settingKey");
            //if (confSection != null)
            //{
            //    return confSection.Value;
            //}
            return "";
        }
    }
}
