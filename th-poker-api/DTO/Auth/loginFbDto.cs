namespace th_poker_api.DTO.Auth
{
    public class loginFbDto
    {
        public string ApiKey { get; set; }
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Device { get; set; }
        public string? Ip { get; set; }
    }
}
