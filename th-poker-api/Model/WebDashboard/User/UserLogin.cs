namespace th_poker_api.Model.WebDashboard.User
{
    public class UserLoginDashboard
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
