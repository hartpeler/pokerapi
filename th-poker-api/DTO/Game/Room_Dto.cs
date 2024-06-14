namespace th_poker_api.DTO.Game
{
    public class Room_Dto
    {
        public bool Result { get; set; }
        public string IdTSRoom { get; set; }
        public string MdIdGames { get; set; }
        public string gmHeaderId { get; set; }
        public string gameDesc { get; set; }
        public float? BuyInMax { get; set; }
        public float? BuyInMin { get; set; }
        public float? StakesMin { get; set; }
        public float? StakesMax { get; set; }
        public string createdOn { get; set; }
        public float? totalJP { get; set; }
        public float? totalBJP { get; set; }
        



        /*1. Id game
2. Buy in Max
3. Buy in Min
4. StakesMin
5. Stakes Max

*/



    }

    public class ClaimDTO
    {
        public string ID { get; set; }
        public string WinType { get; set; }
        public double Amount { get; set; }
        public bool IsBJP { get; set; }
    }
}
