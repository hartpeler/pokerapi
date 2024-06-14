

namespace th_poker_api.Model.ZTesting
{
    public class UserDashboard
    {
        [Key]
        public string userID { get; set; }
        public string email { get; set; }
        public string pw { get; set; }
        public string roleID { get; set; }
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public string updatedBy { get; set; }
        public DateTime updatedOn { get; set; }

    }
}