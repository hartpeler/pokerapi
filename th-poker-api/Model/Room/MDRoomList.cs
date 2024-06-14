using th_poker_api.DTO.Room;

namespace th_poker_api.Model.Room
{
    public class MDRoomList
    {
        [Key]
        public string IdTSRoom { get; set; }
        public string RoomCode { get; set; }
        public bool RoomType { get; set; }
        public int MaxPlayer { get; set; }
        public int? IdStatus { get; set; }
        //public string? RoomMaster { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public MDGames MDGamesID { get; set; }
        public int Player { get; set; }
        //public string IdMdGames { get; set; }
    }
}
