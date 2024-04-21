namespace ClinicWeb.Common.GeneralResponse
{
    public class GeneralResponse<T> : BaseResponseModel
    {
        public T Object { get; set; }
    }
    public class GeneralResponse : BaseResponseModel
    {
    }
}