namespace th_poker_api.DTO.Response
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T data { get; set; }
    }
}
