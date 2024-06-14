using System.Text.Json.Serialization;

namespace th_poker_api.Model.Player
{
    public class UsersToken
    {
        [Key]
        public string UserTokenID { get; set; }
        public MDUsers IdUser { get; set; } // table MDUsers
        public string? RefreshToken { get; set; }
        public string? IdTokenGoogle { get; set; }
        public string? IdTokenFacebook { get; set; }
        public DateTime? TokenCreated { get; set; }
    }
}
