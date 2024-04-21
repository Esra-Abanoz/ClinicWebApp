using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Components
{
    [ViewComponent]
    public class KullaniciSifreDegistirme : ViewComponentBase
    {

        public KullaniciSifreDegistirme(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig) : base(hostingEnvironment, contextAccessor, iConfig)
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("KullaniciSifreDegistirme");
        }


    }
}
