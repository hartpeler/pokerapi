using System.ComponentModel.DataAnnotations.Schema;

namespace th_poker_api.Model.Friend
{
    public class MDFriendship
    {
        //friendship id, id friend (fk user id from user), create by, status.
        [Key]
        public string RequesterId { get; set; }
        [Key]
        public string AddresseeId { get; set; }
        public Status Status { get; set; }
        public MDUsers Addressee { get; set; }
        public MDUsers Requester { get; set; }
    }

    public enum Status
    {
        Pending = 0,
        Accepted = 1
    }

}
