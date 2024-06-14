using th_poker_api.Model.Player;

namespace th_poker_api.Model.Transactions
{
    public class TSTransactions
    {
        [Key]
        public Guid IdTSTransactions { get; set; }
        public string IdPlayers { get; set; } // primary key table Playes
        public string IdTSTrancactionType { get; set; } // primary key table TSTransactionTypes
        public string IdTSTrancactionStatus { get; set; } // primary key table TSTransactionStatuses
        public string IdTSRooms { get; set; } // primary key table TSRoooms
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
