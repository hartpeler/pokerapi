namespace th_poker_api.Model.Purchase
{
    public class MDPrice
    {
        [Key]
        public string IdPrice { get; set; }
       /* public MDPaymentMethod IdPymMethod { get; set; }
        public MDPaymentItem IdPymItem { get; set; }*/
        public string IdPymMethod { get; set; }
        public string IdPymItem { get; set; }
        public string Currency { get; set; }
        public string Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
