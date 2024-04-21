using ClinicWeb.Repository.RepositoryContext;
using ClinicWeb.Services.Services;

namespace ClinicWeb.Services.ServiceContext
{
    public class ServiceContext : IServiceContext
    {
        private readonly IDbContextService _dbContext;
        private CommonService _commonService;
       
        public CommonService CommonService => _commonService ??= new CommonService(_dbContext);
       
        public ServiceContext(IDbContextService dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.Commit();
            Reset();
        }
        public void Rollback()
        {
            _dbContext.Rollback();
            Reset();
        }
        void Reset()
        {
            _commonService = null;
        }
    }
}
