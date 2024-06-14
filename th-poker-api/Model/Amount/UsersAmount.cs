namespace th_poker_api.Model.Amount
{
    public class UsersAmount
    {
        [Key]
        public string AmountID { get; set; }
        public string IdUser { get; set; }
        public float amount { get; set; }
        public string? Desc { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? FreeSpin { get; set; }
        public int? AdsCount { get; set; } // To Count Player for seeing Ads, Limit Watch 8 Times / Day.
        public DateTime? TimeForAds { get; set; } //Sets When the ads Refreshed Time, For Example 1 Day
    }
}
