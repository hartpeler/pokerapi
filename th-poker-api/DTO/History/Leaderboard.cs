namespace th_poker_api.DTO.History
{
    public class leaderboard
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public float Chips { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class JackpotLeaderboard
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int JP { get; set; }
        public string ProfilePicture { get; set; }
    }
    public class BigJackpotLeaderboard
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int BJP { get; set; }
        public string ProfilePicture { get; set; }
    }

}
