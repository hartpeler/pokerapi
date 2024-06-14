namespace th_poker_api.DTO.Friend
{
    public class invitationResponseDto
    {
        public string InvitationID { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public float amount { get; set; }
        public string? Status { get; set; }
        public string? LastLogin { get; set; }
    }
}
