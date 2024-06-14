namespace th_poker_api.DTO.Auth
{
    public class GetUser
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
