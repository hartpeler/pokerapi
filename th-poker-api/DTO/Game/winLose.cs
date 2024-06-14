namespace th_poker_api.DTO.Game
{
    public class winLose
    {
        public string ApiKey { get; set; }
        public string IdTSRoom { get; set; }
        public string UserId { get; set; }
        public int? CardUser1 { get; set; }
        public int? CardUser2 { get; set; }
        public int? Card1 { get; set; }
        public int? Card2 { get; set; }
        public int? Card3 { get; set; }
        public int? Card4 { get; set; }
        public int? Card5 { get; set; }
        public float Balance { get; set; }
        public string Status { get; set; }  // Status Win = 0, Lose = 1  *Edited By: William Tan
        public int WinTypeId { get; set; }
        public float Tax { get; set; } // 2% of the win
        public double JackpotPool { get; set; } //1% of the win
        public double BigJackpotPool { get; set; } //2% of the win
        public float JackpotWin { get; set; } //Royal flush: 100% of the pool, straight flush: 75% of the pool, four of the kind: 50% of the pool
        public float BigJackpotWin { get; set; }
    }

}
