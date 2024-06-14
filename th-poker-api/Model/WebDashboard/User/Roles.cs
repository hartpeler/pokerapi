namespace th_poker_api.Model.WebDashboard.User
{
    public class RolesDashboard
    {
        [Key]
        public string RoleID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
