namespace th_poker_api.DTO
{
    public class RefreshToken
    {
        [Required]
        public string? Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
