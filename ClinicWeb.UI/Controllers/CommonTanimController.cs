using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Controllers
{
    public class CommonTanimController : BaseController
    {
        public CommonTanimController(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig) : base(hostingEnvironment, contextAccessor, iConfig)
        {
        }
       

       
    }
}
