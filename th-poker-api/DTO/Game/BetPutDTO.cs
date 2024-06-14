using System;
namespace th_poker_api.DTO.Game
{
	public class BetPutDTO
	{
        public string apiKey { get; set; }
        public string idTSRoom { get; set; }
		public string userID { get; set; }
		public int state { get; set; } //1 : call, 2: raise, 3: check, 4: fold,  5: all in
		public float amount { get; set; }
    }
}

