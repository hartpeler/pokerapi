namespace th_poker_api.Model.Game
{
    public class GameplayDetail
    {
        [Key]
        public string GameplayDetailID { get; set; }
        public GameplayHeader GMHeaderId { get; set; }
        public MDUsers IdUser { get; set; }
        public string IDTSRooms { get; set; }
        public int? CardUser1 { get; set; }
        public int? CardUser2 { get; set; }
        public int? Card1 { get; set; }
        public int? Card2 { get; set; }
        public int? Card3 { get; set; }
        public int? Card4 { get; set; }
        public int? Card5 { get; set; }
        public float? PrevBal { get; set; }
        public float? Balance { get; set; }
        public string? Status { get; set; } // Status 0 = Win, Status 1 = Lose, Status 2 = Win Without Phase 5
        public WinType? WinTypeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
