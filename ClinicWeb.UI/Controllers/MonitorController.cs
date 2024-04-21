using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Common.Shared;
using ClinicWeb.Common.Utils;
using ClinicWeb.Repository.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.UI.Controllers
{
    public class MonitorController : BaseController
    {
        public MonitorController(IHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration iConfig) : base(hostingEnvironment, contextAccessor, iConfig)
        {
           
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SqlMonitorEnabled(bool isEnabled, string sifre, bool temizle = false, bool closeWindow = false)
        {
            if (!temizle)
            {
                if (isEnabled)
                {
                    if (accessor.HttpContext != null)
                        LocalRedisManager.Add(
                            string.Concat("sqlMonitorDataUid:", accessor.HttpContext.Request.Cookies["ss-id"]), "1",
                            CacheTimeEnum.Day, 1);
                }
                else
                {
                    DeleteSsIdMonitorData(true);
                }

                if (closeWindow)
                {
                    DeleteSsIdMonitorData(true, true);
                }
            }
            else
            {
                DeleteSsIdMonitorData(dataDelete: true);
            }

            if (SessionHelper.DefaultSession.Admin!=2)
            {
                if (isEnabled && string.IsNullOrEmpty(sifre))
                {
                    return Json(new { result = "1" });
                }
                if (isEnabled && !string.IsNullOrEmpty(sifre))
                {
                    var pass = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                    if (pass + "0" != sifre) return Json(new { result = "3" });
                    LocalRedisManager.Add(string.Concat("sqlMonitorDataUid:", Request.Cookies["ss-id"]), "1", CacheTimeEnum.Day, 1);
                    return Json(new { result = "2" });
                }
                return Json(new { result = "2" });
            }
            return Json(new { result = "2" });
        }

        private void DeleteSsIdMonitorData(bool uIdDelete = false, bool dataDelete = false)
        {
            if (Request.Cookies["ss-id"] != null)
            {
                if (uIdDelete)
                {
                    LocalRedisManager.Remove(string.Concat("sqlMonitorDataUid:", Request.Cookies["ss-id"]));
                }
                if (Request.Cookies["ss-id"] != null && dataDelete)
                {
                    LocalRedisManager.DeleteFindKey(string.Concat("sqlMonitorData:", Request.Cookies["ss-id"], "*"));
                }
            }
        }
        public ActionResult KisayolEkrani()
        {
            return new PartialViewResult
            {
                ViewName = "KisayolEkrani"
            };
        }
        public JsonResult RedistenSil(string redisKey)
        {
            LocalRedisManager.Remove(redisKey);
            return Json(new { result = "ok" });
        }

        [HttpPost]
        public JsonResult QueryRun(string querykey, bool explain)
        {
            try
            {
                var sql = LocalRedisManager.Get<SqlSharedModel>(string.Concat("sqlMonitorData:", Request.Cookies["ss-id"], ":", querykey));
                if (sql == null)
                    return Json(new { result = "Bağlantı Zaman Aşımına Uğradı, Lütfen İşlemi Tekrarlayınız" });

                var request = explain
                    ? string.Concat("EXPLAIN ", sql.SORGU)
                    : sql.SORGU;

                var result = ServiceContext.CommonService.SqlMonitorQueryRender(request);
                return Json(result.Columns.Count == 0 ? new { result = "Sorgu Çalıştırılamadı", count = 0 } : new { result = DynamicListToHtml.ToHtmlTable(result.Columns, result.Datas), count = result.Datas.Count });
            }
            catch (Exception e)
            {
                return Json(new { result = string.Concat("Sorgu Çalıştırılamadı :", e.Message), count = 0 });
            }
        }

        [HttpPost]
        public JsonResult GetSqlQuery(string querykey)
        {
            try
            {
                var sql = LocalRedisManager.Get<SqlSharedModel>(string.Concat("sqlMonitorData:", Request.Cookies["ss-id"], ":", querykey));
                return Json(sql != null ? new { result = sql.SORGU } : new { result = "Bağlantı Zaman Aşımına Uğradı, Lütfen İşlemi Tekrarlayınız." });
            }
            catch (Exception e)
            {
                return Json(new { result = string.Concat("Sorgu Çalıştırılamadı :", e.Message), count = 0 });
            }
        }
    }
}
