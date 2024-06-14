namespace th_poker_api.DTO.Player
{
    public class checkLogin
    {
        public bool Success { get; set; } = false;
        public int Code { get; set; }
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
        public string ReferalJoin { get; set; }
        public string? IdTokenGoogle { get; set; }
        public string? IdTokenFacebook { get; set; }

    }
}
