namespace th_poker_api.Model.Game
{
    public class GameplayHeader
    {
        [Key]
        public string GameplayHeaderID { get; set; }
        public string IdTSRoom { get; set; }
        public int Card1 { get; set; }
        public int Card2 { get; set; }
        public int Card3 { get; set; }
        public int Card4 { get; set; }
        public int Card5 { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ICollection<GameplayDetail> gameplayDetails { get; set; }

    }
}
 