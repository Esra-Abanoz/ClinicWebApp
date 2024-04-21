using StackExchange.Redis;

namespace ClinicWeb.Common.RedisManagement.Local
{
    public class LocalRedisConnectionFactory
    {
       
        static LocalRedisConnectionFactory()
        {
            var conn = string.Format("{0}:{1},defaultDatabase ={2},abortConnect=false,allowAdmin=true,ConnectTimeout=15000,ConfigCheckSeconds=60,ConnectRetry=30,SyncTimeout=15000", "127.0.0.1", 6379, 1);
            lazyConnection = ConnectionMultiplexer.Connect(conn);
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
