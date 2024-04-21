using ClinicWeb.Common.Dynamic;

namespace ClinicWeb.Common.SQLFactoring
{
    public interface IAltivaSqlQuery : IAltivaQuery
    {
        //IAltivaSqlQuery AddScalar(string columnAlias, DbType type);
        //IAltivaSqlQuery AddScalars<TModel>(Expression<Func<TModel, object>> excludedProperties = null, Expression<Func<TModel, object>> onlyIncludeProperties = null);
        DynamicGridData DynamicGridData();
    }
}
