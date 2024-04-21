using ClinicWeb.Services.ServiceContext;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Components
{
    public abstract  class ViewComponentBase: ViewComponent
    {
        public string EndPoint { get; set; }
        public static IHttpContextAccessor accessor;
        public IServiceContext ServiceContext;
        public IHostEnvironment _hostingEnvironment;


        public ViewComponentBase(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig)
        {
           // ServiceContext = ServiceContextFactory.New();
            EndPoint = iConfig.GetValue<string>("ApiEndPoint:Url");
            accessor = contextAccessor;
            _hostingEnvironment = hostingEnvironment;

        }
       
    }
}
