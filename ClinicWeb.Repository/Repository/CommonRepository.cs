using ClinicWeb.Common.Dynamic;
using ClinicWeb.Common.GeneralResponse;
using ClinicWeb.Common.SQLFactoring;
using ClinicWeb.Core.UnitOfWork;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.Repository.RepositoryContext;
using System.Text;

namespace ClinicWeb.Repository.Repository
{
    public class CommonRepository : BaseRepository.BaseRepository
    {
        public CommonRepository(IUnitOfWork unitOfWork, IDbContext context) :
            base(unitOfWork, context)
        {
        }
        public DynamicGridData SqlMonitorQueryRender(string sql)
        {
            var sonuc = Session.CreateSQLQuery(sql).DynamicGridData();
            return sonuc;
        }
    }
}
