namespace th_poker_api.Model.Purchase
{
    public class MDPaymentItem
    {
        [Key]
        public string IdPymItem { get; set; }
        public float Value { get; set; }
        public float Price { get; set; }
        public string? Desc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
