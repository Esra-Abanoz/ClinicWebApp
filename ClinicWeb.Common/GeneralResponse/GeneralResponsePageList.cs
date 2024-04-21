using ClinicWeb.Common.SQLFactoring;

namespace ClinicWeb.Common.GeneralResponse
{
    public class GeneralResponsePageList<T> : ResponseBase
    {
        public PagedList<T> Items { get; set; }
    }
}
