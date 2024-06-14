using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.DTO.Purchase;
using th_poker_api.DTO.Response;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.PurchaseService
{
    public interface IPurchaseService
    {
        Task<Handling> topUp(topupDto request);
    
    }
}
