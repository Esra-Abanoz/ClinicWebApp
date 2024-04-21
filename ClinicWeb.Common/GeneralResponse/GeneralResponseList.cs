namespace ClinicWeb.Common.GeneralResponse
{
    public class GeneralResponseList<T> : ResponseBase
    {
        public List<T> Items { get; set; }
    }
}