namespace th_poker_api.DTO.History
{ 
    public class TransferHistoryDto
    {  
        public string TransferID { get; set; }
        public float Amount { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

        public string dateTime { get; set; }
    }
}
