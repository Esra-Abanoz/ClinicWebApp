using ClinicWeb.Core.Extentions;
using ClinicWeb.Models.Enums;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.UI.Controllers;
using ClinicWeb.UI.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicWeb.UI.Filter
{
    public class YetkiKontrolAttribute : ActionFilterAttribute
    {
        private YetkiEnum _yetkiEnum;

        public YetkiKontrolAttribute(YetkiEnum yetkiEnum)
        {
            _yetkiEnum = yetkiEnum;
        }
        public  override void OnActionExecuted(ActionExecutedContext context)
        {
           return;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (SessionHelper.DefaultSession.Admin==2)
            {
                return;
            }
            var controller = context.Controller as BaseController;
           // var yetkilist = controller?.ServiceContext.KullaniciService.KullaniciYetkiListesi().Items;
            controller?.ServiceContext?.Commit();
            //var yetkidurum = yetkilist.Count(x => x.Key == (int)_yetkiEnum);
            //if (yetkidurum > 0)
            //{
            //    return;
            //}
            context.Result = new JavaScriptResult("alert('Yetki Talebinde Bulununuz; Gerekli Yetki : " + _yetkiEnum.GetDescription() + "')");
        }
    }
}
