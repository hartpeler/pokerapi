namespace th_poker_api.Model.Friend
{
    public class MDFriends
    {
        [Key]
        public string Id { get; set; }
        public string FriendId { get; set; } // untuk teman
        public MDUsers userId { get; set; } // untuk user
        public string userCreateID { get; set; }
        public string Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
