using System;
namespace th_poker_api.Model.Game
{
	public class TableBet
	{
		[Key]
		public string idTableBet { get; set; }
		public string idTSRoom { get; set; }
		public float amt { get; set; }
		public DateTime createdOn { get; set; }
		public string createdBy { get; set; }
    }
}

