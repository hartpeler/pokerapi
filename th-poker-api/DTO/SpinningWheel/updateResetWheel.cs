namespace th_poker_api.DTO.SpinningWheel
{
    public class updateResetWheel
    {
        public string ApiKey { get; set; }
        public string UserId { get; set; }

        public float amount { get; set; }

        //Adding Remaining Spin Ads
        public int AdsCount { get; set; }
    }
}
