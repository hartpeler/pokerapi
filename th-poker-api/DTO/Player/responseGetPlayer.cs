namespace th_poker_api.DTO.Player
{
    public class responseGetPlayer
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Profile { get; set; } // tambah
        public string? Status { get; set; } // tambah
        public string? Role { get; set; } // tambah
        public string? Referral { get; set; } // tambah
        public bool? Gender { get; set; }
        public float? Currency { get; set; } // tambah
        public string RefreshToken { get; set; } = string.Empty;
        public string FreeSpin { get; set; }
        public string DateTimeServer { get; set; }
        public string ReferalJoin { get; set; }
        public string adsCount { get; set; }
        public string timeForAds { get; set; }

    }
}
