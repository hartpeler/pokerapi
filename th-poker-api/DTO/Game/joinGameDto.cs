namespace th_poker_api.DTO.Game
{
    public class joinGameDto
    {
        public string ApiKey { get; set; }
        public string IdUser { get; set; }
        public string GMHeaderId { get; set; }
        public float Balance { get; set; } //Balance Buy In Value

    }
}
