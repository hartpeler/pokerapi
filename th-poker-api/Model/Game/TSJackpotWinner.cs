namespace th_poker_api.Model.Game
{
    public class TSJackpotWinner
    {
        [Key]        
        public string JackpotWinnerID { get; set; }
        public string GameplayID { get; set; }
        public double Amount { get; set; }
        public bool Claim { get; set; }
        public bool IsBJP { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
