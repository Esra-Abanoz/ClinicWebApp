using ClinicWeb.Services.Services;

namespace ClinicWeb.Services.ServiceContext
{
    public interface IServiceContext
    {
        void Commit();
        void Rollback();
        CommonService CommonService { get; }

    }
}
