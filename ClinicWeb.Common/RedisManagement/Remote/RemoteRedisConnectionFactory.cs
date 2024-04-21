using ClinicWeb.Common.Utils;
using StackExchange.Redis;

namespace ClinicWeb.Common.RedisManagement.Remote
{
    public class RemoteRedisConnectionFactory
    {
        internal class RedisConJson
        {
            public string Server { get; set; }
            public string Port { get; set; }
            public string DefaultDatabase { get; set; }
            public string ConnectionName { get; set; }
        }
        static RemoteRedisConnectionFactory()
        {
            lazyConnection = ConnectionMultiplexer.Connect(ConString());
        }
        private static string ConString()
        {
            var rediscacheName = "RemoteRedisconnection";
            if (MemoryCacheManager.Contains(rediscacheName))
            {
                return GetObjectFromCache(rediscacheName);
            }

            var connectionstr = getConnection();
            SetObjectFromCache(rediscacheName, connectionstr);
            return connectionstr;
        }
        internal static string getConnection()
        {
            var conf = ClinicWebAppSettingsHelper.ReadClinicWebAppSettings();
            if (string.IsNullOrEmpty(conf.RedisCache.Password))
            {
                return string.Format("{0}:{1},defaultDatabase={2},abortConnect=false,allowAdmin=true,ConnectTimeout=15000,ConfigCheckSeconds=60,ConnectRetry=30,SyncTimeout=15000", conf.RedisCache.Host, conf.RedisCache.Port, conf.RedisCache.DefaultDataBase.GetValueOrDefault(1));
            }
            return string.Format("{0}:{1},password={2},defaultDatabase={3},abortConnect=false,allowAdmin=true,ConnectTimeout=15000,ConfigCheckSeconds=60,ConnectRetry=30,SyncTimeout=15000", conf.RedisCache.Host, conf.RedisCache.Port, (string)PluginRunner.Instance.LoadAssemblyDecryptString(conf.RedisCache.Password), conf.RedisCache.DefaultDataBase.GetValueOrDefault(1));
        }
        internal static string GetObjectFromCache(string cacheItemName)
        {
            return MemoryCacheManager.Get<string>(cacheItemName);

        }
        internal static void SetObjectFromCache(string cacheItemName, string constring)
        {
            MemoryCacheManager.Add(cacheItemName, constring, CacheTimeEnum.Day, 1);
        }
        private static ConnectionMultiplexer lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get { return lazyConnection; }
        }
        public static void DisposeConnection()
        {
            if (lazyConnection.IsConnected)
                lazyConnection.Dispose();
        }
    }
}
