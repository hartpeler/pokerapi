namespace th_poker_api.Model.Game
{
    public class TSHouse
    {
        [Key]
        public string HouseID { get; set; }
        public string GameplayID { get; set; }
        public float Amount { get; set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
