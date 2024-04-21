using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Models.Enums;
using ClinicWeb.Models.SessionModel;
using ClinicWeb.Models.SistemParametre;
using Microsoft.AspNetCore.Http;


namespace ClinicWeb.Repository.Helpers
{
    public static class SessionHelper 
    {
        private static IHttpContextAccessor current;

        public static void Configure(IHttpContextAccessor accessor)
        {
            current = accessor;
        }
        public static string GetIpPort()
        {
            if (current != null && current.HttpContext != null)
            {
                return string.Concat(current.HttpContext.Request.Scheme, "://", current.HttpContext.Request.Host);

            }

            return string.Empty;
        }

        public static SistemParemetreleriModel SistemParemtreleriGetir(SistemParemetreleriEnum paremetreEnum)
        {
            var key = RedisKeyGenerator.CreateWithParts("Parametreler", paremetreEnum.ToString());
            if (LocalRedisManager.Exists(key))
            {
                return LocalRedisManager.Get<SistemParemetreleriModel>(key);
            }
            var context = RepositoryContext.RepositoryContext.New();
            var result = new SistemParemetreleriModel();//context.SistemParametreRepository.ParemetreGetir(paremetreEnum);
            //Düzelmesi Gerek
            context.Commit();
            LocalRedisManager.Add(key, result, CacheTimeEnum.Day,30);
            return result;
        }

        public static string GetUserCookie {
            get
            {
                if (current != null && current.HttpContext != null)
                {
                    if (current.HttpContext.Request.Cookies["ss-id"] != null)
                    {
                        return current.HttpContext.Request.Cookies["ss-id"];
                    }
                }

                return string.Empty;
            }

        }

        public static KullaniciViewModel DefaultSession
        {
            get
            {
                if (current != null && current.HttpContext!=null)
                {
                    if (current.HttpContext.Request.Cookies["ss-id"] != null)
                    {
                        var session = LocalRedisManager.Get<KullaniciViewModel>(RedisKeyGenerator.CreateWithParts("ss-id", current.HttpContext.Request.Cookies["ss-id"]));
                        if (session == null)
                        {
                            current.HttpContext.Response.Cookies.Delete("ss-id");
                        }

                        return session ?? new KullaniciViewModel();
                    }

                }
                return new KullaniciViewModel();
            }
        }

        public static bool YetkiKontrol(YetkiEnum myenum)
        {
            if (DefaultSession.Admin==2)
            {
                return true;
            }
            var key = string.Concat("KullaniciRolYetkiler:YetkiListesi", DefaultSession.Id);
            List<UserYetkiGetirModel> yetkilist;
            if (LocalRedisManager.Exists(key))
            {
                yetkilist = LocalRedisManager.Get<List<UserYetkiGetirModel>>(key);
            }
            else
            {
                var context = RepositoryContext.RepositoryContext.New();
                yetkilist = new List<UserYetkiGetirModel>();//context.KullaniciIslemleriRepository.KullaniciYetkiListesi();
                context.Commit();
                LocalRedisManager.Add(key, yetkilist, CacheTimeEnum.Year,1);
            }
            var yetkidurum = yetkilist.Count(x => x.Key == (int)myenum);
            return yetkidurum > 0;

        }

       
    }
}
