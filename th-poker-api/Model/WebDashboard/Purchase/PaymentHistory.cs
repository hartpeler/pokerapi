namespace th_poker_api.Model.WebDashboard.Purchase
{
    public class PaymentHistory
    {
        [Key]
        public string IDPayment { get; set; }
        public string IDPurchaseItem { get; set; }
        public string IDPaymentMethod { get; set; }
        public string IDStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
