using ClinicWeb.Core.UnitOfWork;
using ClinicWeb.Repository.Repository;

namespace ClinicWeb.Repository.RepositoryContext
{
    public class DbContext : IDbContextService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;
        protected IUnitOfWork UnitOfWork => _unitOfWork ??= _unitOfWorkFactory.Create();
        private CommonRepository _commonRepository;

       
        public CommonRepository CommonRepository => _commonRepository ??= new CommonRepository(UnitOfWork, this);
       

        public DbContext(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
            }
            finally
            {
                Reset();
            }
        }
        public void Rollback()
        {
            try
            {
                UnitOfWork.Rollback();
            }
            finally
            {
                Reset();
            }
        }
        void Reset()
        {
            _unitOfWork = null;
            _commonRepository = null;
        }
    }
}
