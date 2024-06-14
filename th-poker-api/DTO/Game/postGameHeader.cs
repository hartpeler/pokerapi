namespace th_poker_api.DTO.Game
{
    public class postGameHeader
    {
        public string Apikey { get; set; }
        public string IdTSRoom { get; set; }
        public int Card1 { get; set; }
        public int Card2 { get; set; }  
        public int Card3 { get; set; }  
        public int Card4 { get; set; }  
        public int Card5 { get; set; }  
        public string RoomMaster { get; set; }  
    }
}
