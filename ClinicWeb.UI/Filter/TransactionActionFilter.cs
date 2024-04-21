using ClinicWeb.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicWeb.UI.Filter
{
    public class TransactionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as BaseController;
            if (context.Exception == null)
            {
                controller?.ServiceContext?.Commit();
                GC.SuppressFinalize(this);
            }
            else
            {
                controller?.ServiceContext?.Rollback();
                GC.SuppressFinalize(this);
            }
        }
       

    }
}
