using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Common.Shared;
using ClinicWeb.Core.Extentions;
using Microsoft.AspNetCore.SignalR;

namespace ClinicWeb.UI.Hubs
{
    public class SqlMonitorHub : Hub
    {
        public async Task ShowSql(string sqlUniqId, string sqlClientId)
        {
            await Clients.All.SendAsync("ShowSql", SerializerHelper.Serialize(LocalRedisManager.Get<SqlSharedModel>(string.Concat("sqlMonitorData:", sqlClientId, ":", sqlUniqId))));
        }
    }
}
