namespace ClinicWeb.Common.SQLFactoring
{
    public interface IAltivaSession : IAltivaSqlQuery
    {
        IAltivaSqlQuery CreateSQLQuery(string queryString);
    }
}
