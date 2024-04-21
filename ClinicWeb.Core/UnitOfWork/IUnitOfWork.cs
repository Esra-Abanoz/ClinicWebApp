using System.Data;

namespace ClinicWeb.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();
        
    }
}
