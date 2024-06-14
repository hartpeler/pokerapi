namespace th_poker_api.DTO.Auth
{
    public class AuthLoginDevice
    {
        public string APIKey { get;  set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string? Device { get; set; }
        public string? Ip { get; set; }
    }
}
