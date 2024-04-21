namespace ClinicWeb.Common.Dynamic
{
    public class DynamicGridData
    {
        public List<DynamicGridColumn> Columns { get; set; }

        public List<object[]> Datas { get; set; }
    }
    public class DynamicGridColumn
    {
        public string ColumnName { get; set; }

        public string Type { get; set; }
    }
}
