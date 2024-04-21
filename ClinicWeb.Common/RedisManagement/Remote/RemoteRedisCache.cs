using ClinicWeb.Core.Extentions;
using StackExchange.Redis;

namespace ClinicWeb.Common.RedisManagement.Remote
{
    internal class RemoteRedisCache : ICache
    {
        private readonly IDatabase _redisDb;

        //Connection bilgisi initialize anında alınıyor
        public RemoteRedisCache()
        {
            _redisDb = RemoteRedisConnectionFactory.Connection.GetDatabase();
        }
        public ConnectionMultiplexer GetConnection()
        {
            return RemoteRedisConnectionFactory.Connection;
        }
        public void DeleteFindKey(string pattern)
        {
            if (!pattern.EndsWith("*"))
            {
                pattern += "*";
            }
            foreach (var ep in RemoteRedisConnectionFactory.Connection.GetEndPoints())
            {
                var server = RemoteRedisConnectionFactory.Connection.GetServer(ep);
                var keys = server.Keys(database: _redisDb.Database, pattern: pattern).ToArray();
                _redisDb.KeyDeleteAsync(keys);
            }
        }
        public void Set<T>(string key, T objectToCache, DateTime expireDate, When when = When.Always)
        {
            _redisDb.StringSet(key, SerializerHelper.Serialize(objectToCache), expireDate.Subtract(DateTime.Now), when,CommandFlags.None);
        }
        public void Set<T>(string key, T objectToCache)
        {
            _redisDb.StringSet(key, SerializerHelper.Serialize(objectToCache));
        }
        public void Remove(string key)
        {
            _redisDb.KeyDelete(key);
        }
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (!keys.Any()) return;
            foreach (var key in keys)
            {
                _redisDb.KeyDelete(key);
            }
        }
        public void FlushDatabase(int[] dbs)
        {
            foreach (var item in dbs)
            {
                var newcon = string.Format("{0}:{1},defaultDatabase ={2},abortConnect = false,allowAdmin=1", "127.0.0.1", "6379", item);
                var lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(newcon));
                lazyConnection.Value.GetServer("127.0.0.1:6379").FlushDatabase(item);
            }
        }
        public bool SetReturn(string key, string value, TimeSpan expireDate)
        {
            return _redisDb.StringSet(key, value, expireDate, When.NotExists,CommandFlags.None);
        }
        //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
        public T Get<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);

            return redisObject.HasValue ? SerializerHelper.Deserialize<T>(redisObject) : default(T);
        }
        //Redis te var olan key'e karşılık gelen value'yu alıp liste olarak deserialize ettikten sonra return eden metot
        public List<T> GetList<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);

            return redisObject.HasValue ? SerializerHelper.Deserialize<List<T>>(redisObject) : default(List<T>);
        }
        //Redis te var olan key-value değerlerini silen metot
        public void Delete(string key)
        {
            _redisDb.KeyDelete(key);
        }
        //Gönderilen key parametresine göre redis'te bu key var mı yok mu bilgisini return eden metot
        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }
        //Redis bağlantısını Dispose eden metot
        public void Dispose()
        {
            RemoteRedisConnectionFactory.Connection.Dispose();
        }
    }
}
