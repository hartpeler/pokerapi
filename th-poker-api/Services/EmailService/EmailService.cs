using MimeKit;
using th_poker_api.DTO;
using th_poker_api.DTO.Email;
using th_poker_api.Model.Success;
using MailKit.Net.Smtp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace th_poker_api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailConfiguration _emailConfig;
        private MessageCodes _codes = new MessageCodes();


        public EmailService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, EmailConfiguration emailConfig)
        {
            _context = dataContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailConfig = emailConfig;
        }
        #endregion

        #region Send Mail Method
        public async Task<Handling> SendMail (UserRegisterDTO request)
        {
            try
            {
                var mail = CreateEmailMessage(request);
                await SendAsync(mail);
                return new Handling
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Mail Sender"
                };
            }
            catch (Exception err)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                };
            }

            /*var emailRes = new SuccessHandling();
            var bodyTemplate = string.Empty;
            var body = string.Empty;
            var path = string.Empty;

            bodyTemplate = $"{Directory.GetCurrentDirectory()}";
            body = File.ReadAllText (bodyTemplate);
            body = body.Replace("FullName", request.Username);

            var mailSender = new MailSenderDTO()
            {
                EmailSender = "gamethehomestudios@gmail.com",
                EmailReciever = request.Email,
                EmailBody = body,
                EmailSubject = "test"
            };

            return*/
        }
        #endregion

        #region Private Method
        private MimeMessage CreateEmailMessage(UserRegisterDTO mail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("game the home studios", "gamethehomestudios@gmail.com")); // ganti email perusahaan nanti
            emailMessage.To.Add(new MailboxAddress(mail.Username, mail.Email));
            emailMessage.Subject = mail.ConfirmPassword;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", mail.Password) };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        #endregion

    }
}
