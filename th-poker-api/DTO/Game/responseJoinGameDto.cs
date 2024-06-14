namespace th_poker_api.DTO.Game
{
    public class responseJoinGameDto
    {
        public bool Result { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string GmHeaderId { get; set; }
        public float Balance { get; set; }  //Balance Buy In Value

    }
}
