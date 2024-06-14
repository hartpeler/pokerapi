using th_poker_api.DTO.History;
using th_poker_api.DTO.Response;

namespace th_poker_api.Services.HistoryService
{
    public interface IHistoryService
    {
        Task<ServiceResponse<List<responseTopupH>>> historyTopup(topupHistoryDto request);
        Task<ServiceResponse<List<responseGameH>>> historyGame(topupHistoryDto request, CancellationToken ct);
        Task<ServiceResponse<List<responseWinningH>>> historyWinning(topupHistoryDto request);
        Task<ServiceResponse<List<leaderboard>>> leaderboard();
        Task<ServiceResponse<List<TransferHistoryDto>>> GetTransferHistoryReceiver(string userId);
        Task<ServiceResponse<List<TransferHistoryDto>>> GetTransferHistorySender(string userId);
        Task<ServiceResponse<List<JackpotLeaderboard>>> leaderboardJackpot();
        Task<ServiceResponse<List<BigJackpotLeaderboard>>> LeaderboardBjp();
    }
}
