namespace th_poker_api.DTO.Room
{
    public class ListRoomResponse
    {
        public string RoomCode { get; set; }
        public bool Roomtype { get; set; }
        public int MaxPlayer { get; set; }
        public int IdStatus { get; set; }
        public string IdGame { get; set; }
        public string roomMaster { get; set; }
        //public MDGames MDGames { get; set; }

        public float BuyInMin { get; set; }
        public float BuyInMax { get; set; }
        public float StakesMin { get; set; }
        public float StakesMax { get; set; }
        public int Player { get; set; }

    }

}
