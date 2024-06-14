namespace th_poker_api.Model.Card
{
    public class MDCards
    {
        [Key]
        public int Id { get; set; }
        public int IndexCard { get; set; }
        public string? Desc { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
