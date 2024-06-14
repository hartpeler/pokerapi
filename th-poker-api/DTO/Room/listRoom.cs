namespace th_poker_api.DTO.Room
{
    public class listRoom
    {
        [Required]
        public string APIKey { get; set; }
        public string? idMDGames { get; set; }

    }
}
