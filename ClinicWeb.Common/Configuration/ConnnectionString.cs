using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.Utils;
using System.Reflection;

namespace ClinicWeb.Common.Configuration
{
    public class ConnnectionString
    {
        private const string _ClinicWebAppSettingsFileName = "ClinicWebConfiguration.json";
        private const string GetPostgresqlConString = "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;PersistSecurityInfo=true;Search Path={5};";

        public static string Get()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var cacheName = (assembly.EscapedCodeBase + "configuration").ToMd5();
                if (MemoryCacheManager.Contains(cacheName))
                {
                    var a = MemoryCacheManager.Get<string>(cacheName);
                    var connectionstring = string.Empty;
                    try
                    {
                        connectionstring = PluginRunner.Instance.LoadAssemblyDecryptString(a);
                    }
                    catch (Exception e)
                    {
                        connectionstring = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(connectionstring))
                    {
                        return connectionstring;
                    }
                }

                var conf = ClinicWebAppSettingsHelper.ReadClinicWebAppSettings();
                string dbIp = "localhost";
                int dbPort = 3306;
                string dbName = "altiva";
                string searchPath = "merkezisatinalma";
                string dbUser = string.Empty;
                string dbPassword = string.Empty;
                if (!string.IsNullOrWhiteSpace(conf.Database.Host))
                {
                    dbIp = conf.Database.Host;
                }
                if (conf.Database.Port > 0)
                {
                    dbPort = conf.Database.Port;
                }
                if (!string.IsNullOrWhiteSpace(conf.Database.DefaultDatabase))
                {
                    dbName = conf.Database.DefaultDatabase;
                }
                if (!string.IsNullOrWhiteSpace(conf.Database.DefaultSearchPath))
                {
                    searchPath = conf.Database.DefaultSearchPath;
                }
                if (!string.IsNullOrWhiteSpace(conf.Database.UserName))
                {
                    dbUser = conf.Database.UserName;
                }
                if (!string.IsNullOrWhiteSpace(conf.Database.Password))
                {
                    dbPassword = conf.Database.Password;
                }
                var model = PluginRunner.Instance.LoadAssembly(dbPassword);
                if (model.success)
                {
                    var connectionstring = string.Format(GetPostgresqlConString, dbIp, dbName, dbUser, model.connectionstring, dbPort, searchPath);
                    MemoryCacheManager.Add(cacheName, PluginRunner.Instance.LoadAssemblyEncryptString(connectionstring), CacheTimeEnum.Day, 10);
                    return connectionstring;
                }
                throw new Exception(model.message);
            }
            catch (Exception e)
            {
                throw new Exception("Veritabanına bağlanamadı.");
            }
        }
    }
}

