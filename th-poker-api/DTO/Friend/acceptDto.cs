namespace th_poker_api.DTO.Friend
{
    public class acceptDto
    {
        public string ApiKey { get; set; }
        public string InvitationID { get; set; }
        public int invitation { get; set; } 
        // Summary Invitation
        // 0 = Not Friends;
        // 1 = Friends;
        // 2 = Decline Friends;
    }
}
