using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Core.Extentions;
using ClinicWeb.Models.SessionModel;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.UI.Extensions;
using ClinicWeb.UI.Filter;
using ClinicWeb.UI.Helpers;
using ClinicWeb.UI.Models.Account;
using ClinicWeb.UI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Controllers
{
    public class AccountController : BaseController
    {
      
        public AccountController(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig) : base(hostingEnvironment, contextAccessor, iConfig)
        {
         
        }
        [SkipAuthorize]
        public IActionResult Index(IndexViewModel model )
        {

            if (SessionHelper.DefaultSession != null && SessionHelper.DefaultSession.Id != 0)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
            //    model.HastaneListesi = ServiceContext.CommonService.HastaneListesiGetir().Items;
            //    model.ButceTuruListesi = ServiceContext.CommonService.ButceTuruGetir().Items;
            //    model.YilListesi = ServiceContext.CommonService.YillariGetirLoginEkrani().Items;
            }
            return View(model);
        }
        [HttpPost]
        [SkipAuthorize]
        //public ActionResult Login(LoginRequestModel model)
        //{
        //    try
        //    {
        //        var result = new LoginValidator(model).Validate();
        //        if (!result.IsValid)
        //        {
        //            foreach (var item in result.ErrorMessage)
        //            {
        //                return Json(new { result = false, message = OtherCommon.NotifMessage(item.Message, NotifyType.confirmedNotif) });
        //            }
        //        }
        //        var kullaniciBilgileri = ServiceContext.KullaniciService.GirisKullaniciGetir(model).Object;
        //        if (kullaniciBilgileri == null ||kullaniciBilgileri.Id==0)
        //            return Json(new
        //            {
        //                result = false,
        //                message = "Kullanıcı Adı veya Şifre Yanlış. Lütfen bilgilerinizi kontrol ediniz."
        //            });
        //        string ipAdres = HttpContext.GetClientIpAddress();
        //        if (IpKontrol(kullaniciBilgileri, ipAdres) ==false)
        //        {
        //            return Json(new
        //            {
        //                result = false,
        //                message = "Giriş Yaptığınız Ip sistemede tanımlı değildir."
        //            });
        //        }
        //        var encUserId = Guid.NewGuid().ToString().Replace("-","");
        //        CookieOptions cookie = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
        //        Response.Cookies.Append("ss-id", encUserId, cookie);
      
        //        if (!LocalRedisManager.Exists(RedisKeyGenerator.CreateWithParts("ss-id", encUserId)))
        //        {
        //            LocalRedisManager.Add(RedisKeyGenerator.CreateWithParts("ss-id", encUserId), kullaniciBilgileri, CacheTimeEnum.Minute,180);
        //        }


        //        return Json(new { result = true, message = "Başarılı." });
        //    }
        //    catch 
        //    {
        //        return Json(new { result = false, message = "İşlem Sırasında bir hata oluştu. Tekrar Deneyiniz." });
        //    }
        //}
        [HttpPost]
        [SkipAuthorize]
        public ActionResult CalismaBolgesiGuncelle(CalisMaBolgesiResultModel model)
        {
            if (SessionHelper.DefaultSession != null && SessionHelper.DefaultSession.Id != 0)
            {
                var session = SessionHelper.DefaultSession;
                session.HastaneId = model.Hastane;
                session.ButceTuru = model.ButceTuru;
                session.Yil = model.Yil;
                session.HastaneAdi = model.HastaneAdi;
                // var bagliHastane=  ServiceContext.KullaniciService.BagliHastaneGetir(model.Hastane).Object;
                //session.BagliHastaneId = bagliHastane.BagliHastaneId;
                //session.BagliHastane = bagliHastane.BagliHastane;


                LocalRedisManager.Add(RedisKeyGenerator.CreateWithParts("ss-id", SessionHelper.GetUserCookie), session, CacheTimeEnum.Minute, 60); 
                return Json(new { result = true, message = "Başarılı." });
            }
            else
            {
                
                    return Json(new { result = false, message = "İşlem Sırasında bir hata oluştu. Tekrar Deneyiniz." });
            }
        }
        private bool IpKontrol(KullaniciViewModel model, string ipAdres)
        {
            if (string.IsNullOrEmpty(model.BaslangicIp) && string.IsNullOrEmpty(model.BitisIp))
            {
                return true;
            }
            if (string.IsNullOrEmpty(model.BitisIp))
            {
                if (model.BaslangicIp == ipAdres)
                {
                    return true;
                }
                return false;
            }
            var baslangicIp = OtherCommon.IP2Long(model.BaslangicIp);
            var bitisIp = OtherCommon.IP2Long(model.BitisIp);
            var ipAddress = OtherCommon.IP2Long(ipAdres);
            var inRange = (ipAddress >= baslangicIp && ipAddress <= bitisIp);

            return inRange;
        }
    
        [HttpPost]
        [SkipAuthorize]
        //public IActionResult ForgotPassword(KullaniciSifremiUnuttumRequest model)
        //{
        //    var result = new ForgotPasswordValidator(model).Validate();
        //    if (!result.IsValid)
        //    {
        //        foreach (var item in result.ErrorMessage)
        //        {
        //            return Json(new { result = false, message = OtherCommon.NotifMessage(item.Message, NotifyType.confirmedNotif) });
        //        }
        //    }
        //    var pathToHtmlFile = Path.Combine(hostingEnvironment.ContentRootPath, @"Template\passwordtemplate.html");
        //    var htmlString = System.IO.File.ReadAllText(pathToHtmlFile);
        //    //var kullanicibilgileri =ServiceContext.KullaniciService.SifremiUnuttumKullaniciBilgileriGetir(model).Object;
        //    //var mailBilgileri = ServiceContext.KullaniciService.MailBilgileriGetir().Object;
        //    if (kullanicibilgileri == null || string.IsNullOrEmpty(kullanicibilgileri.AdSoyad))
        //        return Json(new
        //        {
        //            result = false,
        //            message = OtherCommon.NotifMessage("Email Adresi Bulunamadı !", NotifyType.confirmedNotif)
        //        });
        //    var kullaniciYeniSifre = HtmlTemplateGenerator.CreatePassword(10);
        //    HtmlTemplateGenerator.Adisoyadi = kullanicibilgileri.AdSoyad;
        //    HtmlTemplateGenerator.Yenisifre = kullaniciYeniSifre;
        //    var mailtemp = HtmlTemplateGenerator.GetProcessHtml(htmlString, accessor);
        //    MailHelper mail = new MailHelper(mailBilgileri.Host, mailBilgileri.Port, mailBilgileri.Sifre, mailBilgileri.EMail, mailBilgileri.MailImza, mailBilgileri.Ssl);
        //    var gonderilecekadres = new List<string> { model.EMail };
        //    mail.Gonder(gonderilecekadres, "Merkezi Satın Alma -Kullanıcı Şifre Sıfırlama", mailtemp);
        //    //KullaniciSifreGuncelleRequest requesmodel = new KullaniciSifreGuncelleRequest()
        //    //{
        //    //    Id = kullanicibilgileri.Id,
        //    //    Sifre = kullaniciYeniSifre
        //    //};
        //    //ServiceContext.KullaniciService.KullaniciSifreGuncelle(requesmodel);
        //    return Json(new { result = true, message = OtherCommon.NotifMessage("Şifre Bilgileriniz mail adresinize gönderilmiştir.", NotifyType.successNotif) });
        //}
        [SkipAuthorize]
        public IActionResult LogOff()
        {
            var ssId = RedisKeyGenerator.CreateWithParts("ss-id", accessor.HttpContext?.Request.Cookies["ss-id"]);
            if (LocalRedisManager.Exists(ssId))
            {
                LocalRedisManager.Remove(ssId);
            }
            Response.Cookies.Delete("ss-id");
            return RedirectToAction("Index", "Account");
        }
        //[SkipAuthorize]
        //[HttpGet]
        //public IActionResult KullaniciListesi([FromQuery] string term)
        //{
        //    var list = ServiceContext.KullaniciService.KullanicilariGetir(new KullaniciAraRequest() { KullaniciAdi = term }).Items
        //        .Select(x => new ClinicWebListItem(x.KullaniciAdi, x.KullaniciAdi)).ToList();
        //    return Json(list);
        //}
    }
}
