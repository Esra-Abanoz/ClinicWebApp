using ClinicWeb.Common.SQLFactoring;
using ClinicWeb.Core.UnitOfWork;
using ClinicWeb.Repository.Helpers;
using ClinicWeb.Repository.RepositoryContext;
using System.Data;

namespace ClinicWeb.Repository.BaseRepository
{
    public abstract class BaseRepository
    {
        private IDbTransaction Transaction { get; set; }

        public IDbContext Context { get; set; }

        protected BaseRepository(IUnitOfWork unitOfWork, IDbContext context)
        {
            Transaction = unitOfWork.Transaction;
            Context = context;
        }
        public IAltivaSession Session => new SqlQueryFactoring(Transaction, SessionHelper.DefaultSession==null?string.Empty:SessionHelper.GetIpPort(), SessionHelper.GetUserCookie);

        
    }
}
