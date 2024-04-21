using Dapper;
using ClinicWeb.Common.Dynamic;
using ClinicWeb.Common.RedisManagement;
using ClinicWeb.Common.RedisManagement.Local;
using ClinicWeb.Common.Shared;
using ClinicWeb.Common.SqlMapper;
using ClinicWeb.Common.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Data;

namespace ClinicWeb.Common.SQLFactoring
{

    public class SqlQueryFactoring : IAltivaSession
    {
        private readonly IDbTransaction _transaction;
        private readonly SessionFactoringModels _sqlQuery;
        private readonly string _getUserCookie;
        private readonly string _hubUrl;
        public SqlQueryFactoring(IDbTransaction transaction, string hubUrl, string getUserCookie)
        {
            _getUserCookie = getUserCookie;
            _hubUrl = hubUrl;
            _transaction = transaction;
            _sqlQuery = new SessionFactoringModels();
            Dapper.SqlMapper.AddTypeHandler(typeof(string[]), new StringArrayTypeHandler());
            // Dapper.SqlMapper.AddTypeHandler(typeof(Dictionary<string, string>), new JsonObjectTypeHandler());
            Dapper.SqlMapper.AddTypeHandler(typeof(DateTime), new DateTimeTypeHandler());
            Dapper.SqlMapper.AddTypeHandler(typeof(TimeSpan), new TimeSpanToTicksHandler());
            Dapper.SqlMapper.AddTypeHandler(typeof(Enum), new StringEnumTypeHandler());
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        public DynamicGridData DynamicGridData()
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
            var session = _transaction.Connection;
            var returnValue = new DynamicGridData();
            var cmd = session.CreateCommand();
            cmd.CommandText = _sqlQuery.Query;
            using (var reader = cmd.ExecuteReader())
            {
                returnValue.Columns = new List<DynamicGridColumn>();
                var schemaTable = reader.GetSchemaTable();
                foreach (DataRow field in schemaTable.Rows)
                {
                    returnValue.Columns.Add(new DynamicGridColumn
                    {
                        ColumnName = field["ColumnName"].ToString(),
                        Type = SqlQueryExtensions.GetShortName(field["DataType"].ToString())
                    });
                }
            }
            returnValue.Datas = session.Query<dynamic>(_sqlQuery.Query)
                    .ToList()
                    .Select(x => ((IDictionary<string, object>)x).Values.ToArray())
                    .ToList();
            return returnValue;
        }
        public IAltivaSqlQuery CreateSQLQuery(string queryString)
        {
            _sqlQuery.Query = queryString;
            return this;
        }
        public IAltivaQuery SetParameter(string name, object val)
        {
            _sqlQuery.Parameters.Add(name, val);
            return this;
        }
        public IAltivaQuery SetParameter(string name, object val, DbType type)
        {
            _sqlQuery.Parameters.Add(name, val, type);
            return this;
        }

        public IAltivaQuery SetParameterList(string name, IEnumerable vals)
        {
            _sqlQuery.Parameters.Add(name, vals);
            return this;
        }


        public long ToExecuteUpdate()
        {
            ShowSqlOutput(_sqlQuery.Query,_sqlQuery.Parameters);
            return GetExecuteUpdateAndGetInsertId(query: _sqlQuery.Query, _sqlQuery.Parameters, _sqlQuery.TimeOut, _transaction, false, _sqlQuery.ProcessName);
        }
        public long ToExecuteUpdateAndGetInsertId<T>(string idColumnName)
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
           return GetExecuteUpdateAndGetInsertId(
                query: _sqlQuery.Query,
                commandTimeout: _sqlQuery.TimeOut,
                param: _sqlQuery.Parameters,
                transaction: _transaction,
                lastid: true,
                processname: _sqlQuery.ProcessName,
                idcolumnname: idColumnName);
        }
        public List<T> ToTransformedList<T>() where T : class
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
            var result = GetResultTransformer<T>(
                query: _sqlQuery.Query,
                param: _sqlQuery.Parameters,
                transaction: _transaction,
                commandTimeout: _sqlQuery.TimeOut
            );
            return result?.ToList();
        }
        public List<T> ToList<T>() 
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
            var result = GetResultTransformer<T>(
                query: _sqlQuery.Query,
                param: _sqlQuery.Parameters,
                transaction: _transaction,
                commandTimeout: _sqlQuery.TimeOut
            );
            return result?.ToList();
        }
        public T ToUniqueResult<T>()
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
            return GetToUniqResult<T>(
                sql: _sqlQuery.Query,
                param: _sqlQuery.Parameters,
                transaction: _transaction,
                commandTimeout: _sqlQuery.TimeOut);
        }
        public T ToTransformedUniqueResult<T>() where T : class
        {
            ShowSqlOutput(_sqlQuery.Query, _sqlQuery.Parameters);
            var result = GetResultTransformer<T>(
                query: _sqlQuery.Query,
                commandTimeout: _sqlQuery.TimeOut,
                param: _sqlQuery.Parameters,
                transaction: _transaction
            )?.SingleOrDefault();
            if (result == null)
            {
                return Activator.CreateInstance(typeof(T)) as T;
            }
            return result;
        }
        public PagedList<T> ToTransformedDbPagedList<T>(int pageIndex, int pageSize)
        {
            return GetTransformedDbPagedList<T>(query: _sqlQuery.Query,
                param: _sqlQuery.Parameters,
                transaction: _transaction,
                pageIndex: pageIndex,
                pageSize: pageSize,
                commandTimeout: _sqlQuery.TimeOut);
        }
        private void ShowSqlOutput(string query, DynamicParameters param)
        {
            try
            {
                if (!string.IsNullOrEmpty(_getUserCookie) && LocalRedisManager.Exists(string.Concat("sqlMonitorDataUid:", _getUserCookie)))
                {
                    var connection = new HubConnectionBuilder()
                        .WithUrl(string.Concat(_hubUrl, "/monitorHub"))
                        .Build();
                    //var ssId = HttpContext.Current.Request["ss-id"]?.ToString();
                    connection.StartAsync().ContinueWith(async task =>
                    {
                        if (!task.IsFaulted)
                        {
                            var key = string.Concat(Guid.NewGuid().ToString(), DateTime.Now.Ticks, Guid.NewGuid().ToString())
                                .GetHashCode();
                            var sqlmodel = new SqlSharedModel
                            {
                                SURE = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss fff"),
                                SORGU = OtherFunctions.GetModelStringBuilderMonitor(query, param).Replace('\u00A0', ' '),
                                VERIKAYNAGI = "Database",
                                userId = _getUserCookie,
                                REDISKEY = "",
                                QueryKey = key.ToString()
                            };
                            LocalRedisManager.Add(string.Concat("sqlMonitorData:", _getUserCookie, ":", key), sqlmodel, CacheTimeEnum.Minute, 30);
                            await connection.InvokeAsync("ShowSql", key.ToString(), _getUserCookie);
                            await connection.DisposeAsync();
                        }
                    });
                }
            }
            catch
            {
                // ignored
            }
        }
        private long GetExecuteUpdateAndGetInsertId(string query, DynamicParameters param, int? commandTimeout = null, IDbTransaction transaction = null,
            bool lastid = false, string processname = null, string idcolumnname = null)
        {
            if (lastid)
            {
                if (string.IsNullOrEmpty(idcolumnname))
                {
                    query = query + (query.TrimEnd().EndsWith(";") ? "" : ";") + "SELECT LAST_INSERT_ID();";
                }
                else
                {
                    query = query.TrimEnd().TrimEnd(';').TrimEnd() + " RETURNING " + idcolumnname + ";";
                }
                var sonuc = transaction?.Connection.Query<long>(query, param, transaction).SingleOrDefault();
                if (sonuc != null) return (long)sonuc;
            }
            var result = transaction?.Connection.Execute(query, param, transaction, commandTimeout: commandTimeout, commandType: CommandType.Text);
            return (long)result;

        }
        private IEnumerable<T> GetResultTransformer<T>(string query, DynamicParameters param = null, IDbTransaction transaction = null, int? commandTimeout = 60, CommandType? commandType = CommandType.Text)
        {
          return transaction?.Connection.Query<T>(string.Format(query), param, transaction, false, commandTimeout,commandType);
        }
        private PagedList<T> GetTransformedDbPagedList<T>(string query, DynamicParameters param = null, int pageIndex = 1, int pageSize = 25, IDbTransaction transaction = null, int? commandTimeout = 60)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (query.TrimEnd().EndsWith(";"))
            {
                query = query.TrimEnd().TrimEnd(';');
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new Exception("T-Sql Sorgusu bulunamadı");
            }
            var rowCountSql = $"SELECT COUNT(*) AS TotalCount FROM ({query}) AS originalQueryRowCount;";
            string monitorsql;
            if (pageSize == int.MaxValue)
            {
                monitorsql = query + ";";
            }
            else
            {
                var startRow = (pageIndex - 1) * pageSize;
                monitorsql = string.Format(query + " LIMIT " + "{0} OFFSET {1};", pageSize, startRow);
            }
            string sql = rowCountSql + monitorsql;
            var multiresult = transaction?.Connection.QueryMultiple(sql, param, transaction);
            var resulttotal = multiresult.ReadSingle<int>();
            var resultset = multiresult.Read<T>(false).ToList();
            var totalPage = Math.Ceiling(Convert.ToDouble(resulttotal.ToString()) / Convert.ToDouble(pageSize.ToString()));
            ShowSqlOutput(monitorsql, param);
            return new PagedList<T>(resultset, pageSize, pageIndex, resulttotal,
                Convert.ToInt32(totalPage));

        }
        private T GetToUniqResult<T>(string sql, DynamicParameters param = null, IDbTransaction transaction = null, int? commandTimeout = 60, CommandType? commandType = CommandType.Text)
        {
            return  transaction.Connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }
        public void Dispose()
        {
            _sqlQuery.Parameters = null;
            GC.SuppressFinalize(this);
        }

       
    }
}
