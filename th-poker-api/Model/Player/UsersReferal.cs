namespace th_poker_api.Model.Player
{
    public class UsersReferal
    {
        [Key]
        public string ReferalId { get; set; }
        public MDUsers UsersId { get; set; }
        public string ReferalCode { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
