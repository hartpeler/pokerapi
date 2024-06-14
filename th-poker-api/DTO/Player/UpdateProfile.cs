namespace th_poker_api.DTO.Player
{
    public class UpdateProfile
    {
        public string APIKey { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Username to short, Please enter at least 6 characters")]
        [MaxLength(12, ErrorMessage = "Username to long, Please enter a maximum of 12 characters")]
        public string? UserName { get; set; }
        public bool? Gender { get; set; }
    }
}
