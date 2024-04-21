using ClinicWeb.Common.Dynamic;
using ClinicWeb.Common.GeneralResponse;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.Repository.RepositoryContext;

namespace ClinicWeb.Services.Services
{
    public class CommonService : BaseService.BaseService
    {
        public CommonService(IDbContextService repositoryContext) : base(repositoryContext)
        {
        }
        public DynamicGridData SqlMonitorQueryRender(string sql)
        {
            return RepositoryContext.CommonRepository.SqlMonitorQueryRender(sql);
        }

    }
}
