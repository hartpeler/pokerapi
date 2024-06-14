namespace th_poker_api.DTO.Player
{
    public class loginGoogleDto
    {
        public string Apikey { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string IdTokenGoogle { get; set; }
        public string? Device { get; set; }
        public string? Ip { get; set; }
    }
}
