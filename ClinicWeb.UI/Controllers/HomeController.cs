using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig) : base(hostingEnvironment, contextAccessor, iConfig)
        {
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Dashboard2()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult TestRaporu()
        //{
        //    return this.GetReportResult(new ReportingTestRequest
        //    {
        //        Adi = "Deneme Ad",
        //        Soyadi = "Denem Soyad",
        //        TestData = ServiceContext.BankaSubeTanimlariService.BankaListesi(new BankaSearchModel
        //        {
        //            Limit = int.MaxValue
        //        }).Items.Items.Select(x => new ReportingTestModel
        //        {
        //            Deneme = x.BankaAdi
        //        }).ToList()
        //    });
        //}
    }
}
