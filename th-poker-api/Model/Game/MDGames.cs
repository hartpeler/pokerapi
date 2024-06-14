using System.ComponentModel.DataAnnotations;
using th_poker_api.Model.Room;

namespace th_poker_api.Model.Game
{
    public class MDGames
    {
        [Key]
        public string IdMDGames { get; set; }
        public ICollection<MDGameTypes> MDGameTypes { get; set; }
        public ICollection<MDRoomList> MDRoomList { get; set; }
       // public string IdMDGametype { get; set; } // primary key table MDGameTypes
        //public string MDGameTypes { get; set; }
        public string GameTitle { get; set; }
        public string GameDesc { get; set; }
        public float BuyInMin { get; set; }
        public float BuyInMax { get; set; }
        public float StakesMin { get; set; }
        public float StakesMax { get; set; }
        public string CreatedBy { get; set; }
        public bool isActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
