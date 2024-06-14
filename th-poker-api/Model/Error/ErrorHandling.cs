namespace th_poker_api.Model.Error
{
    public class ErrorHandling
    {
        public bool Result { get; set; }
        public int Code { get; set; }
        public List<string> Message { get; set; }
    }
}
