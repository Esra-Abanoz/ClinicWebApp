using StackExchange.Redis;

namespace ClinicWeb.Common.RedisManagement
{
    internal interface ICache : IDisposable
    {
        T Get<T>(string key);
        void Set<T>(string key, T obj, DateTime expireDate, When when = When.Always);
        void Set<T>(string key, T obj);
        void Remove(string key);
        void RemoveAll(IEnumerable<string> keys);
        bool Exists(string key);
        void FlushDatabase(int[] dbs);
    }
}
