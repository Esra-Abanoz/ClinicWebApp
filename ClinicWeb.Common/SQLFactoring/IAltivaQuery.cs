using System.Collections;
using System.Data;

namespace ClinicWeb.Common.SQLFactoring
{
    public interface IAltivaQuery : IDisposable
    {
        IAltivaQuery SetParameter(string name, object val);
        IAltivaQuery SetParameter(string name, object val, DbType type);
     
        IAltivaQuery SetParameterList(string name, IEnumerable vals);
        long ToExecuteUpdate();
        long ToExecuteUpdateAndGetInsertId<T>(string idColumnName);
        List<T> ToTransformedList<T>() where T : class;
        List<T> ToList<T>() ;
        T ToUniqueResult<T>();
        T ToTransformedUniqueResult<T>() where T : class;
        PagedList<T> ToTransformedDbPagedList<T>(int pageIndex, int pageSize);
    }
}
