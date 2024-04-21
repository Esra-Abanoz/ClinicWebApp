using Newtonsoft.Json;

namespace ClinicWeb.Common.Utils
{
    public static class ClinicWebAppSettingsHelper
    {
        private const string _ClinicWebAppSettingsFileName = "ClinicWebConfiguration.json";
        public static ClinicWebAppSettings ReadClinicWebAppSettings()
        {
            bool debug = false;
#if DEBUG
            debug = true;
#endif
            string configFilePath = string.Empty;
            if (debug)
            {
                configFilePath = ClinicWebAppSettingsFilePath();
            }
            else
            {
                configFilePath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "ClinicWeb", _ClinicWebAppSettingsFileName);
            }
            if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
            {
                throw new FileNotFoundException(_ClinicWebAppSettingsFileName + " dosyası bulunamadı.");
            }
            using (StreamReader r = new StreamReader(configFilePath))
            {
                string json = r.ReadToEnd();
                try
                {
                    return JsonConvert.DeserializeObject<ClinicWebAppSettings>(json);
                }
                catch (Exception e)
                {
                    throw new Exception(_ClinicWebAppSettingsFileName + " dosyası okunamadı.");
                }
            }
        }
        private static string ClinicWebAppSettingsFilePath()
        {
            var basedir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (basedir != null)
            {
                var configFile = Path.Combine(basedir.FullName, _ClinicWebAppSettingsFileName);
                if (File.Exists(configFile))
                {
                    return configFile;
                }
                basedir = basedir.Parent;
            }
            return string.Empty;
        }
    }
    public class ClinicWebAppSettings
    {
        public int ReportingServicePort { get; set; }
        public ClinicWebAppSettingsRedisCache RedisCache { get; set; }
        public ClinicWebAppSettingsDatabase Database { get; set; }
    }
    public class ClinicWebAppSettingsRedisCache
    {
        public string Host { get; set; }
        public int Port { get; set; }
        private int _defaultDataBase = 1;
        public int? DefaultDataBase
        {
            get
            {
                if (_defaultDataBase <= 0)
                {
                    return 1;
                }

                return _defaultDataBase;
            }
            set
            {
                _defaultDataBase = value.GetValueOrDefault(1);
            }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ClinicWebAppSettingsDatabase
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string DefaultDatabase { get; set; }
        public string DefaultSearchPath { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
