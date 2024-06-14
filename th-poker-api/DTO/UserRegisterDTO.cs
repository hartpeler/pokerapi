namespace th_poker_api.DTO
{
    public class UserRegisterDTO
    {
        public string APIKey { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Username to short, Please enter at least 5 characters")]
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
