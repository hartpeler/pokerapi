using th_poker_api.DTO;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.EmailService
{
    public interface IEmailService
    {
        Task<Handling> SendMail(UserRegisterDTO request);
        //Task<SuccessHandling> SendMailAsync(UserRegisterDTO request);
    }
}
