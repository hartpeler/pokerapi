

namespace th_poker_api.Model.Game
{
    public class MDGameTypes
    {
        [Key]
        public string IdMDGameType { get; set; }
        public string GameDesc { get; set; }
        public bool isActive { get; set; } // tambah
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public MDGames MDGames { get; set; }
    }
}
