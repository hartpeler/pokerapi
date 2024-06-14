using th_poker_api.DTO.Game;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.SpinningWheel;

namespace th_poker_api.Services.SpinningWheelService
{
    public interface ISpinningWheel
    {
        Task<responseSWDto> updateSW(updateSWDto request);
        Task<responseResetSW> freeAds(updateResetWheel request);
    }
}
    