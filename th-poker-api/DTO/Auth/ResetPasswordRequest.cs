using System.ComponentModel.DataAnnotations;

namespace th_poker_api.DTO.Auth
{
    public class ResetPasswordRequest
    {
        public string APIKey { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? oldPassword { get; set; }
        [MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string? newPassword { get; set; }
        [Required]
        [Compare("newPassword")]
        [MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string? ConfirmNewPassword { get; set; }
    }
}
