namespace th_poker_api.Model.WebDashboard.Misc
{
    public class Statuses
    {
        [Key]
        public string IDStatus { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
