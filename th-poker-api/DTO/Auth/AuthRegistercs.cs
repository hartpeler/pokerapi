namespace th_poker_api.DTO.Auth
{
    public class AuthRegistercs
    {
        public string APIKey { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Username to short, Please enter at least 3 characters")]
        [MaxLength(12, ErrorMessage = "Username to long, Please enter a maximum of 12 characters")]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string? Password { get; set; }

        [Required, Compare("Password")]
        [MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string? ConfirmPassword { get; set; }
    }
}
