namespace th_poker_api.DTO.SpinningWheel
{
    public class responseSWDto
    {
        public bool Result { get; set; }
        public int code { get; set; }
        public string Message { get; set; }
        public float amount { get; set; }
        public string FreeSpin { get; set; }
        public string AdsCount { get; set; }
        public string TimeForAds { get; set; }
    }
}
