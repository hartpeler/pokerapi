using System.Text.Json.Serialization;

namespace th_poker_api.Model.Player
{
    public class UsersLogin
    {
        [Key]
        public string IdUsersLogin { get; set; }
        public MDUsers IdUser { get; set; } // table MDUsers
        public string? Device { get; set; }
        public string? Ip { get; set; }
        public string? Status { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
