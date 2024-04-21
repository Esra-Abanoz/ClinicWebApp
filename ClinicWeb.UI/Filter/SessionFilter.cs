using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Models.Enums;
using ClinicWeb.Models.SessionModel;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.UI.Controllers;
using ClinicWeb.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ClinicWeb.UI.Filter
{
    public class SessionFilter : IActionFilter
    {
        private IHttpContextAccessor _accessor;

        public SessionFilter(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_accessor.HttpContext?.Request.Cookies["ss-id"] == null) return;
            var sessionKey = RedisKeyGenerator.CreateWithParts("ss-id", context.HttpContext?.Request.Cookies["ss-id"]);
            var session = LocalRedisManager.Get<KullaniciViewModel>(sessionKey);
            var controller = context.Controller as BaseController;

            var sure = SessionHelper.SistemParemtreleriGetir(SistemParemetreleriEnum.SessionSure).Deger;


            if (string.IsNullOrEmpty(sure)) sure = "20";
            if (session != null)
            {
                LocalRedisManager.Add(sessionKey, session, CacheTimeEnum.Minute, Convert.ToInt32(sure));
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (SkipAuthorizeCheck(context))
            {
                return;
            }
            if (SessionHelper.DefaultSession != null && SessionHelper.DefaultSession.Id == 0)
            {
                if (context.HttpContext?.Request.Cookies["ss-id"] != null)
                {
                    var sessionKey = RedisKeyGenerator.CreateWithParts("ss-id", context.HttpContext?.Request.Cookies["ss-id"]);

                    var session = LocalRedisManager.Get<KullaniciViewModel>(sessionKey);
                    if (session == null)
                    {
                        context.HttpContext.Response.Cookies.Delete("ss-id");
                    }
                }
                if (context.HttpContext != null && context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new StatusCodeResult((int)HttpStatusCode.Redirect);
                }
                else
                {
                    var returnUrl = context.HttpContext.Request.Path + '?';
                    if (context.HttpContext.Request.QueryString.HasValue)
                        returnUrl += context.HttpContext.Request.QueryString + "&";
                    try
                    {
                        if (context.HttpContext.Request.Form != null && context.HttpContext.Request.Form?.Count > 0)
                        {
                            returnUrl = "";
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    returnUrl = returnUrl.TrimEnd().TrimEnd('?').TrimEnd().TrimEnd('&');
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "Index", redirectUrl = returnUrl }));
                }
            }
        }
        private bool SkipAuthorizeCheck(ActionExecutingContext context)
        {
            var filter = context.ActionDescriptor.EndpointMetadata.OfType<SkipAuthorize>();
            if (filter.Any() && !filter.Any(x => x._skipAuthorize == false))
            {
                return true;
            }
            return false;
        }
    }
    public class SkipAuthorize : ActionFilterAttribute
    {
        public readonly bool _skipAuthorize;
        public SkipAuthorize()
        {
            _skipAuthorize = true;
        }
        public SkipAuthorize(bool skipAuthorize)
        {
            _skipAuthorize = skipAuthorize;
        }
    }

}
