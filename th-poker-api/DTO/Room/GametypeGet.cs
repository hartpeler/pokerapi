namespace th_poker_api.DTO.Room
{
    public class GametypeGet
    {
        public string? IdMDGames { get; set; }
       // public string? GameType { get; set; }
        //public string? IDMDGames { get; set; } 
        public string? GameTitle { get; set; }
        public string? GameDesc { get; set; }
        public float BuyInMin { get; set; }
        public float BuyInMax { get; set; }
        public float StakesMin { get; set; }
        public float StakesMax { get; set; }
      

    }
}
