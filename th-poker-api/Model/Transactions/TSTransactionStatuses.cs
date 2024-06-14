namespace th_poker_api.Model.Transactions
{
    public class TSTransactionStatuses
    {
        [Key]
        public string IdTSTransactionStatus { get; set; }
        public string TransactionStatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
