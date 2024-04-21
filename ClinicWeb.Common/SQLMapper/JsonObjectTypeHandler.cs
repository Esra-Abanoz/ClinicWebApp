using ClinicWeb.Core.Extentions;
using System.Data;

namespace ClinicWeb.Common.SqlMapper
{
    public class JsonObjectTypeHandler : Dapper.SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = (value == null)
                ? (object)DBNull.Value
                : SerializerHelper.Serialize(value);
            parameter.DbType = DbType.String;
        }
        public object Parse(Type destinationType, object value)
        {
            return SerializerHelper.DeserializeObject(value.ToString(),destinationType);
        }
    }
}
