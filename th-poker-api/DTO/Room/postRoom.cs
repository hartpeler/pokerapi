namespace th_poker_api.DTO.Room
{
    public class postRoom
    {
        public string ApiKey { get; set; }
        public string RoomCode { get; set; }
        public bool RoomType { get; set; }
        public int MaxPlayer { get; set; }
        public string betValue { get; set; }
        public string roomMaster { get; set; }
        public string IdMDGames { get; set; }
    }
}
