using System.Text.Json.Serialization;

namespace th_poker_api.Model.Player
{
    public class UsersProfile
    {
        //[JsonIgnore]
        [Key]
        public string IDProfile { get; set; }
        public MDUsers IdUser { get; set; } // table MDUsers
        public byte[]? Profile { get; set; }
        public string? extension { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
