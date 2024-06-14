namespace th_poker_api.DTO.History
{
    public class responseWinningH
    {
        public string GamePlayid { get; set; }
        public string Date { get; set; }
        public int CardUser1 { get; set; }
        public int CardUser2 { get; set; }
        public int Card1 { get; set; }
        public int Card2 { get; set; }
        public int Card3 { get; set; }
        public int Card4 { get; set; }
        public int Card5 { get; set; }
        public string Decs { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public float Balance { get; set; }

    }
}
