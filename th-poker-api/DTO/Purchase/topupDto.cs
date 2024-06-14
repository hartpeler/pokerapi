namespace th_poker_api.DTO.Purchase
{
    public class topupDto
    {
        public string ApiKey { get; set; }
        public string UserId { get; set; }
        public string paymentNum { get; set; }
        public string platform { get; set; }
        public float prevVal { get; set;  }
    }
}
