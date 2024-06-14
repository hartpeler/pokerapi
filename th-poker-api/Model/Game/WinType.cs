namespace th_poker_api.Model.Game
{
    public class WinType
    {
        [Key]
        public int IdWinType { get; set; }
        public string Desc { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
