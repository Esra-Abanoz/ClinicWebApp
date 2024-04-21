
namespace ClinicWeb.Repository.RepositoryContext
{
    public interface IDbContextService : IDbContext
    {
        void Commit();
        void Rollback();
    }
}
