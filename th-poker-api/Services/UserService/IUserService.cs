using Microsoft.AspNetCore.Mvc;
using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.UserService
{

    public interface IUserService
    {
        Task<Handling> UpdateProfile(UpdateProfile request);
        Task<Handling> AddPicture([FromForm] updatePictureDto request);
        Task<responseGetPlayer> GetPlayer(getPlayerDto request);
        Task<Handling> referalJoin (referalCodeJoinDto request);
      

    }
}
