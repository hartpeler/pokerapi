using th_poker_api.DTO.Game;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.GameplayService
{
    public interface IGameplayService
    {
        //Task<responseGameHeader> GameplayHeader(postGameHeader request);
        Task<responseStandDto> standTable(standupDto request);
        Task<Handling> winlose(winLose request);
        Task<responseJoinGameDto> joinGame(joinGameDto request);
        Task<ResponseGameDTO> BetAndPut(BetPutDTO request);

        Task<Room_Dto> roomDetail(Room_Detail request);
        Task<ResponseGameDTO> Transfer(Transfer request);
        Task<Handling> UpdateClaimStatus(string claimID);
        Task<List<ClaimDTO>> GetClaimList(string UserID);
    }

}
