namespace th_poker_api.DTO
{
    public class UserDTO
    {
        [Required (ErrorMessage = "API KEY is Required")]
        public string APIKey { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Device { get; set; }
        public string? Ip { get; set; }
    }
}
