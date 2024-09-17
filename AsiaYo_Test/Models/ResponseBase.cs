namespace AsiaYo_Test.Models
{
    public class ResponseBase<T>
    {
        public T Entries { get; set; }
        public string Message { get; set; } = string.Empty;
        public EnumStatusCode StatusCode { get; set; } = EnumStatusCode.Success;
    }

    public enum EnumStatusCode
    {
        Success = 200,
        Fail = 400
    }
}
