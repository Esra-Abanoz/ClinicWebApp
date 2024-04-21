namespace ClinicWeb.Common.GeneralResponse
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> MessagesList { get; set; }
        public string ResponseCode { get; set; }
    }
}
