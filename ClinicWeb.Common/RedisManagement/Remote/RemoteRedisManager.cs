namespace ClinicWeb.Common.RedisManagement.Remote
{
    public static class RemoteRedisManager
    {
        static readonly RemoteRedisCache Cache = new RemoteRedisCache();
        public static void Add<T>(string key, T source, CacheTimeEnum cacheType, int cacheTime)
        {
            switch (cacheType)
            {
                case CacheTimeEnum.Second:
                    Cache.Set(key, source, DateTime.Now.AddSeconds(cacheTime));
                    break;
                case CacheTimeEnum.Minute:
                    Cache.Set(key, source, DateTime.Now.AddMinutes(cacheTime));
                    break;
                case CacheTimeEnum.Hour:
                    Cache.Set(key, source, DateTime.Now.AddHours(cacheTime));
                    break;
                case CacheTimeEnum.Day:
                    Cache.Set(key, source, DateTime.Now.AddDays(cacheTime));
                    break;
                case CacheTimeEnum.Month:
                    Cache.Set(key, source, DateTime.Now.AddMonths(cacheTime));
                    break;
                case CacheTimeEnum.Year:
                    Cache.Set(key, source, DateTime.Now.AddYears(cacheTime));
                    break;
            }
        }
        public static void DeleteFindKey(string pattern)
        {
            Cache.DeleteFindKey(pattern);
        }
        public static List<T> GetList<T>(string key)
        {
            return Cache.GetList<T>(key);

        }
        public static bool Exists(string key)
        {
            return Cache.Exists(key);
        }
        public static void FlushDatabase(int[] dbs)
        {
            Cache.FlushDatabase(dbs);
        }
        public static T Get<T>(string key)
        {
           return Cache.Get<T>(key);
        }
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
        public static void RemoveAll(IEnumerable<string> keys)
        {
            Cache.RemoveAll(keys);
        }
    }
}