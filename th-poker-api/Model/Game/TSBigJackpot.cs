namespace th_poker_api.Model.Game
{
    public class TSBigJackpot
    {
		[Key]
        public string IDTSBigJackpot { get; set; }
        public string GameID { get; set; }
        public double Pool { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
