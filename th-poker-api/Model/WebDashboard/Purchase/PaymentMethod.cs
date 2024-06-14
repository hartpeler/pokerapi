namespace th_poker_api.Model.WebDashboard.Purchase
{
    public class PaymentMethod
    {
        [Key]
        public string IDPaymentMethod { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
