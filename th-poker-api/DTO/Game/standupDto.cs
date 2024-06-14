namespace th_poker_api.DTO.Game
{
    public class standupDto
    {
        public string ApiKey { get; set; }
        public string roomCode { get; set; }
        public string UserId { get; set; }
        // public string GMHeaderId { get; set; }
        public int? CardUser1 { get; set; }
        public int? CardUser2 { get; set; }
       /* public int? Card1 { get; set; }
        public int? Card2 { get; set; }
        public int? Card3 { get; set; }
        public int? Card4 { get; set; }
        public int? Card5 { get; set; }*/
        public float Balance { get; set; }
    }
}
