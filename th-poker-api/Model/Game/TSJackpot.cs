namespace th_poker_api.Model.Game
{
    public class TSJackpot
    {
		[Key]
        public string JackpotID { get; set; }
        public string GameID { get; set; }

        public double Amount { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
