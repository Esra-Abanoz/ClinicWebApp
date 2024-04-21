using ClinicWeb.Repository.Repository;

namespace ClinicWeb.Repository.RepositoryContext
{
    public interface IDbContext
    {
        CommonRepository CommonRepository { get; }

    }
}
