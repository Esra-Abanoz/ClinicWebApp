﻿using System.Data;

namespace ClinicWeb.Common.SqlMapper
{
    public class StringEnumTypeHandler : Dapper.SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = value.ToString();
            parameter.DbType = DbType.AnsiString;
        }

        public object Parse(Type destinationType, object value)
        {
            return Enum.Parse(destinationType, Convert.ToString(value));
        }
    }
}
