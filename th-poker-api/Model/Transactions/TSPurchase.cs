using th_poker_api.Model.Purchase;

namespace th_poker_api.Model.Transactions
{
    public class TSPurchase
    {
        [Key]
        public string IdPurchase { get; set; }
        public string IdPymItem { get; set; }
        public string IdUser { get; set; }
        public string Description { get; set; }
        public int? IdStatus { get; set; }
        public float Amount { get; set; }
        public float Amount_a { get; set; } //amount_a = amount before transaction 
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
