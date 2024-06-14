namespace th_poker_api.DTO.Auth
{
    public class responseFbLogin
    {
        public bool Result { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string userId { get; set; }
        public string? UserName { get; set; }
        public string IdFacebookToken { get; set; }
        public string? Profile { get; set; } // tambah
        public string? Status { get; set; } // tambah
        public string? Role { get; set; } // tambah
        public bool? Gender { get; set; }
        public float? Currency { get; set; } // tambah
        public string? FreeSpin { get; set; }

        public string? AdsCount { get; set; }
        public string? TimeForAds { get; set; }
    }
}
