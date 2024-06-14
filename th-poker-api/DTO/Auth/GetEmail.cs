namespace th_poker_api.DTO.Auth
{
    public class GetEmail
    {
        public string APIKey { get;  set; }
        [Required, EmailAddress]
        public string email{ get; set; }
    }
}
