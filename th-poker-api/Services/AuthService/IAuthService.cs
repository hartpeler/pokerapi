using Microsoft.AspNetCore.Mvc;
using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.AuthService
{
    public interface IAuthService
    {
        Task<Handling> Logout(LogoutDto request);
        Task<AuthResponseDto> Login(UserDTO request);
        Task<responseGoogleLogin> LoginGoogle(loginGoogleDto request);
        Task<responseFbLogin> LoginFacebook(loginFbDto request);
        Task<Handling> Register (AuthRegistercs request);
        Task<Handling> SendResetPassword(GetEmail request);
        Task<Handling> ChangePassword(ResetPasswordRequest request);
        Task<checkLogin> CheckLogin(CheckService request);   ///Wait Until Google has been Implemented
        Task<Handling> disconnectPlayer(disconnectPlayerDto request);


    }
}
