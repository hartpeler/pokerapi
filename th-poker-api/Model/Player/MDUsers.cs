using th_poker_api.Model.Amount;
using th_poker_api.Model.Friend;
using th_poker_api.Model.Player;

namespace th_poker_api.Model.Player
{
    public class MDUsers
    {
        [Key]
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; } = new byte[32];
        public byte[]? PasswordSalt { get; set; } = new byte[32];
        public string? ReferalJoin { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public bool? Gender { get; set; }
        public bool LoggedIn { get; set; }
        public string profilePicture { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ICollection<MDFriendship> FriendshipAddressee { get; set; }
        public ICollection<MDFriendship> FriendshipRequester { get; set; }
        public ICollection<UsersProfile> usersProfiles { get; set; }
        public ICollection<UsersLogin> UsersLogins { get; set; }
        public ICollection<GameplayDetail> GameplayDetails { get; set; }
        public ICollection<UsersReferal> UsersReferals { get; set; }
        public ICollection<UsersToken> UserTokens { get; set; }

    }
}