using Dapper;

namespace ClinicWeb.Common.SQLFactoring
{
    public class SessionFactoringModels
    {
        public SessionFactoringModels()
        {
            Parameters = new DynamicParameters();
        }
        public string Query { get; set; }
        public int? TimeOut { get; set; }
        public DynamicParameters Parameters { get; set; }
        public string ProcessName { get; set; }
        public bool SqlWatch { get; set; }
    }
}