namespace ClinicWeb.Core.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
