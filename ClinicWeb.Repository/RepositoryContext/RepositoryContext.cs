using ClinicWeb.Common.Configuration;
using ClinicWeb.Core.UnitOfWork;
using Npgsql;

namespace ClinicWeb.Repository.RepositoryContext
{
    public sealed class RepositoryContext
    {
        public static DbContext New()
        {
            return new DbContext(new UnitOfWorkFactory<NpgsqlConnection>(ConnnectionString.Get()));
        }
    }
}
