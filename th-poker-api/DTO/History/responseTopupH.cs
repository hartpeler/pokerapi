using Microsoft.VisualBasic;

namespace th_poker_api.DTO.History
{
    public class responseTopupH
    {
        public float Balance { get; set; }
        public float Amount_a { get; set; }
        public string status { get; set; }
        public DateTime Date { get; set; }
        public string DateView { get; set; }
    }
}
