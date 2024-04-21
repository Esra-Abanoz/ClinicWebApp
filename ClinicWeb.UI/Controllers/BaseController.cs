using ClinicWeb.Common.SQLFactoring;
using ClinicWeb.Common.Utils;
using ClinicWeb.Services.ServiceContext;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Controllers
{
    public class BaseController : Controller
    {
        public IConfiguration config { get; set; }
        public IHttpContextAccessor accessor;
        public IServiceContext ServiceContext;
        public IHostEnvironment hostingEnvironment;

        //private ReportRenderType? _reportRenderType = null;
        //public ReportRenderType GetReportRenderType()
        //{
        //    if (!_reportRenderType.HasValue)
        //    {
        //        try
        //        {
        //            if (Request.Query.Count(x => x.Key.Equals("renderType", StringComparison.InvariantCultureIgnoreCase)) > 0)
        //            {
        //                var q = Request.Query.First(x => x.Key.Equals("renderType", StringComparison.InvariantCultureIgnoreCase));
        //                _reportRenderType = (ReportRenderType)Enum.Parse(typeof(ReportRenderType), q.Value, true);
        //            }
        //            else
        //            {
        //                _reportRenderType = ReportRenderType.PDF;
        //            }
        //        }
        //        catch
        //        {
        //            _reportRenderType = ReportRenderType.PDF;
        //        }
        //    }
        //    return _reportRenderType.Value;
        //}
        public BaseController(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig)
        {
           // ServiceContext = ServiceContextFactory.New();
            config = iConfig;
            accessor = contextAccessor;
            this.hostingEnvironment = hostingEnvironment;

        }
        public IActionResult ExtGridStoreLoad<T>(PagedList<T> list)
        {
            return Json(new
            {
                data = list.Items.ToList(),
                total = list.TotalRecord,
                success = true,
                message = string.Empty
            });
        }
        public IActionResult ExtGridStoreLoad<T>(List<T> list)
        {
            return Json(new
            {
                data = list,
                total = list.Count,
                success = true,
                message = string.Empty
            });
        }
        //public IActionResult GetReportResult<TRequest>(TRequest reqeust)
        //{
        //    //return new ReportActionResult(ReportServiceConnect.Get(reqeust, this.GetReportRenderType()));
        //}
    }
}
