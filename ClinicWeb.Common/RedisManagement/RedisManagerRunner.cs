using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Common.RedisManagement.Remote;

namespace ClinicWeb.Common.RedisManagement
{
    public static class RedisManagerRunner
    {
        public static T GetCachedValue<T>(string key, Func<T> action, CacheTimeEnum cacheTimeEnum = CacheTimeEnum.Minute, int expiresInTime = 5, RedisAddressEnum redisAddressEnum = RedisAddressEnum.LocalRedis)
        {
            if (redisAddressEnum == RedisAddressEnum.LocalRedis)
            {
                if (!LocalRedisManager.Exists(key))
                {
                    var result = action();
                    if (result != null)
                    {
                        LocalRedisManager.Add(key, result, cacheTimeEnum, expiresInTime);
                    }
                    return result;
                }
                return LocalRedisManager.Get<T>(key);
            }
            else
            {
                if (!RemoteRedisManager.Exists(key))
                {
                    var result = action();
                    if (result != null)
                    {
                        RemoteRedisManager.Add(key, result, cacheTimeEnum, expiresInTime);
                    }
                }
                return RemoteRedisManager.Get<T>(key);
            }

        }

        public static void Remove<T>(string key, RedisAddressEnum redisAddressEnum = RedisAddressEnum.LocalRedis)
        {
            if (redisAddressEnum == RedisAddressEnum.LocalRedis)
            {
                LocalRedisManager.Remove(key);
            }
            else
            {
                RemoteRedisManager.Remove(key);
            }
        }
    }

}
