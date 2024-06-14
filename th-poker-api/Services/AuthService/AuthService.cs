using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using th_poker_api.DTO;
using th_poker_api.DTO.Email;
using th_poker_api.Model.Success;
using th_poker_api.DTO.Auth;
using System.Text;
using th_poker_api.Model.Amount;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.DTO.Purchase;
using th_poker_api.Model.Player;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics.Arm;
using th_poker_api.Model.Friend;

namespace th_poker_api.Services.AuthService
{
    public class AuthService : IAuthService
    {

        #region Function 
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailConfiguration _emailConfig;
        private Functions _func = new Functions();
        private MessageCodes _codes = new MessageCodes();

        private static System.Random random = new System.Random();


        public AuthService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, EmailConfiguration emailConfig)
        {
            _context = dataContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailConfig = emailConfig;
        }
        #endregion

        #region Login Google
        public async Task<responseGoogleLogin> LoginGoogle(loginGoogleDto request)
        {
            var tokenGoogle = await _context.UsersToken.Where(u => u.IdTokenGoogle.Equals(request.IdTokenGoogle))
                .Include(v => v.IdUser)
                .FirstOrDefaultAsync();

            if (tokenGoogle == null)
            {
                var passwords =  Guid.NewGuid().ToString();
                CreatePasswordHash( passwords, out byte[] passwordHash, out byte[] passwordSalt);
                var nameUser = ReferalCode(request.UserName.ToLower(), 5);
                var trimName = nameUser.Replace(' ', '_');
                var numberRdm = RandomStringv(3);
                var referals = trimName + numberRdm;

                var _postData = new MDUsers
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = referals, 
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = 1,
                    Status = 1,
                    Gender = true,
                    CreatedOn = DateTime.Now,
                    profilePicture = _func.urlApi + "File/ProfilePicture/Male1.png"
                };
                _context.MDUsers.Add(_postData);
                await _context.SaveChangesAsync();

                var user = await _context.MDUsers.Where(u => u.UserId.Equals(_postData.UserId.ToString()))
                    .FirstOrDefaultAsync();

                //var name = ReferalCode(user.UserName.ToLower(), 5);
                //var number = RandomStringv(3);
                //var referal = name + number;
                var referalExist = await _context.UsersReferal.Where(u => u.ReferalCode.Equals(referals)).FirstOrDefaultAsync();

                var userReferal = new UsersReferal
                {
                    ReferalId = Guid.NewGuid().ToString(),
                    UsersId = user,
                    //ReferalCode = referalExist == null ? referal : name + RandomStringv(3),
                    ReferalCode = referalExist == null ? referals : _postData.UserName,
                    CreatedOn = DateTime.Now
                };

                var _userAmount = new UsersAmount
                {
                    AmountID = Guid.NewGuid().ToString(),
                    IdUser = user.UserId,
                    amount = 200000000,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    FreeSpin = DateTime.UtcNow,
                    AdsCount = 0,
                    TimeForAds = DateTime.UtcNow
                };

                var _postToken = new UsersToken
                {
                    UserTokenID = Guid.NewGuid().ToString(),
                    IdUser = user,
                    RefreshToken = request.IdTokenGoogle,
                    IdTokenGoogle = request.IdTokenGoogle,
                    TokenCreated = DateTime.Now
                };

                var _postProfile = new UsersProfile
                {
                    IDProfile = Guid.NewGuid().ToString(),
                    IdUser = user,
                    Profile = Encoding.ASCII.GetBytes(request.Image),
                    CreatedOn = DateTime.Now
                };

                var userLogin = new UsersLogin
                {
                    IdUsersLogin = Guid.NewGuid().ToString(),
                    IdUser = user,
                    Ip = request.Ip,
                    Device = request.Device,
                    Status = "Online",
                    LastLogin = DateTime.Now,
                };

                _context.UsersToken.Add(_postToken);
                _context.UserAmount.Add(_userAmount);
                _context.UsersReferal.Add(userReferal);
                _context.UsersProfile.Add(_postProfile);
                _context.UsersLogin.Add(userLogin);
                await _context.SaveChangesAsync();

                var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
                var user_role = await _context.MDRoles.Where(u => u.Id.Equals(user.Role)).FirstOrDefaultAsync();
                var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();

                return new responseGoogleLogin
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success",
                    userId = _postData.UserId,
                    IdTokenGoogle = _postToken.IdTokenGoogle,
                    UserName = _postData.UserName,
                    Profile = user.profilePicture,
                    Status = user_stts.Desc,
                    Role = user_role.Desc,
                    Gender = _postData.Gender,
                    Currency = _userAmount.amount,
                    FreeSpin = _userAmount.FreeSpin.ToString(),
                    AdsCount = ads.AdsCount.ToString(),
                    TimeForAds = ads.TimeForAds.ToString()
                };
            }
            
            else
            {
                var user = await _context.MDUsers.Where(u => u.UserId.Equals(tokenGoogle.IdUser.UserId))
                    .Include(v => v.usersProfiles)
                    .Include(v => v.UsersReferals)
                    .Include(v => v.UsersLogins)
                    .Include(v => v.UserTokens)
                    .FirstOrDefaultAsync();

                var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(tokenGoogle.IdUser.Status)).FirstOrDefaultAsync();

              
                if (user.Status != 1)
                {
                    return new responseGoogleLogin
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Your Account Has Been " + user_stts.Desc
                    };

                }

                var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
                var freespin = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.FreeSpin).FirstOrDefaultAsync();

                var user_role = await _context.MDRoles.Where(u => u.Id.Equals(tokenGoogle.IdUser.Role)).FirstOrDefaultAsync();

                user.UsersLogins.FirstOrDefault().Ip = request.Ip ; 
                user.UsersLogins.FirstOrDefault().Device = request.Device;
                user.UsersLogins.FirstOrDefault().Status = "Online";
                user.UsersLogins.FirstOrDefault().LastLogin = DateTime.Now;


                _context.SaveChangesAsync();
                return new responseGoogleLogin
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success",
                    userId = user.UserId,
                    IdTokenGoogle = user.UserTokens.FirstOrDefault().IdTokenGoogle,
                    UserName = user.UserName,
                    Profile = user.profilePicture,
                    Role = user_role.Desc,
                    Status = user_stts.Desc,
                    Gender = user.Gender,
                    Currency = _func.getAmount(user.UserId.ToString()),
                    FreeSpin = freespin.FreeSpin.ToString(),
                    AdsCount = ads.AdsCount.ToString(),
                    TimeForAds = ads.TimeForAds.ToString()
                };
            }
        }
        #endregion

        #region login Facebook
        public async Task<responseFbLogin> LoginFacebook(loginFbDto request)
        {
            var tokenFb = await _context.UsersToken.Where(u => u.IdTokenFacebook.Equals(request.IdToken))
                .Include(v => v.IdUser)
                .FirstOrDefaultAsync();

            string Trimmed = String.Concat(request.UserName.Where(c => !Char.IsWhiteSpace(c)));


            if (tokenFb == null)
            {
                var _postData = new MDUsers
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = Trimmed.Length > 12 ? ReferalCode(Trimmed, 12) : Trimmed,
                    Email = request.Email,
                    Role = 1,
                    Status = 1,
                    Gender = true,
                    CreatedOn = DateTime.Now,
                    profilePicture = _func.urlApi + "File/ProfilePicture/Male1.png"
                };

                _context.MDUsers.Add(_postData);
                await _context.SaveChangesAsync();

                var user = await _context.MDUsers.Where(u => u.UserId.Equals(_postData.UserId.ToString()))
                    .FirstOrDefaultAsync();

                var name = ReferalCode(user.UserName.ToLower(), 5);
                var number = RandomStringv(3);
                var referal = name + number;
                var referalExist = await _context.UsersReferal.Where(u => u.ReferalCode.Equals(referal)).FirstOrDefaultAsync();

                var userReferal = new UsersReferal
                {
                    ReferalId = Guid.NewGuid().ToString(),
                    UsersId = user,
                    ReferalCode = referalExist == null ? referal : name + RandomStringv(3),
                    CreatedOn = DateTime.Now
                };

                var _userAmount = new UsersAmount
                {
                    AmountID = Guid.NewGuid().ToString(),
                    IdUser = user.UserId,
                    amount = 200000000,
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    FreeSpin = DateTime.UtcNow,
                    AdsCount = 0,
                    TimeForAds = DateTime.UtcNow
                };

                var _postToken = new UsersToken
                {
                    UserTokenID = Guid.NewGuid().ToString(),
                    IdUser = user,
                    RefreshToken = request.IdToken,
                    IdTokenFacebook = request.IdToken,
                    TokenCreated = DateTime.Now
                };

                var userLogin = new UsersLogin
                {
                    IdUsersLogin = Guid.NewGuid().ToString(),
                    IdUser = user,
                    Ip = request.Ip,
                    Device = request.Device,
                    Status = "Online",
                    LastLogin = DateTime.Now,
                };

                _context.UsersToken.Add(_postToken);
                _context.UserAmount.Add(_userAmount);
                _context.UsersReferal.Add(userReferal);
                _context.UsersLogin.Add(userLogin);

                await _context.SaveChangesAsync();

                var user_role = await _context.MDRoles.Where(u => u.Id.Equals(user.Role)).FirstOrDefaultAsync();
                var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();

                var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
                return new responseFbLogin
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success",
                    userId = _postData.UserId,
                    IdFacebookToken = _postToken.IdTokenFacebook,
                    UserName = _postData.UserName,
                    Profile = _postData.profilePicture,
                    Status = user_stts.Desc,
                    Role = user_role.Desc,
                    Gender = _postData.Gender,
                    Currency = _userAmount.amount,
                    FreeSpin = _userAmount.FreeSpin.ToString(),

                    AdsCount = ads.AdsCount.ToString(),
                    TimeForAds = ads.TimeForAds.ToString()
                };
            }
            else
            {
                var user1 = await _context.MDUsers.Where(u => u.UserId.Equals(tokenFb.IdUser.UserId.ToString()))
                   .Include(v => v.usersProfiles)
                   .Include(v => v.UsersReferals)
                   .Include(v => v.UsersLogins)
                   .Include(v => v.UserTokens)
                   .FirstOrDefaultAsync();

                var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(tokenFb.IdUser.Status)).FirstOrDefaultAsync();

                if (user1.Status != 1)
                {
                    return new responseFbLogin
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Your Account Has Been " + user_stts.Desc
                    };

                }
                var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user1.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
                user1.UsersLogins.FirstOrDefault().Ip = request.Ip;
                user1.UsersLogins.FirstOrDefault().Device = request.Device;
                user1.UsersLogins.FirstOrDefault().Status = "Online";
                user1.UsersLogins.FirstOrDefault().LastLogin = DateTime.Now;

              

                var freespin = await _context.UserAmount.Where(u => u.IdUser.Equals(user1.UserId.ToString())).OrderByDescending(v => v.FreeSpin).FirstOrDefaultAsync();

                var user_role = await _context.MDRoles.Where(u => u.Id.Equals(tokenFb.IdUser.Role)).FirstOrDefaultAsync();

                _context.SaveChangesAsync();
                return new responseFbLogin
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success",
                    userId = user1.UserId,
                    IdFacebookToken = user1.UserTokens.FirstOrDefault().IdTokenFacebook,
                    UserName = user1.UserName,
                    Profile = user1.profilePicture,
                    Role = user_role.Desc,
                    Status = user_stts.Desc,
                    Gender = user1.Gender,
                    Currency = _func.getAmount(user1.UserId.ToString()),
                    FreeSpin = freespin.FreeSpin.ToString(),

                    AdsCount = ads.AdsCount.ToString(),
                    TimeForAds = ads.TimeForAds.ToString()
                };
            }
        }
        #endregion

        #region Login Method
        public async Task<AuthResponseDto> Login(UserDTO request)
        {
            if (!isValidEmail(request.Email))
            {
                return new AuthResponseDto()
                {
                    Success = false,
                    Message = "Email Is Not Valid",
                };
            }

            var user = await _context.MDUsers.Where(u => u.Email.Equals(request.Email))
                .Include(v => v.usersProfiles)
                .Include(v => v.UsersReferals)
                .Include(v => v.UsersLogins)
                .FirstOrDefaultAsync();

            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResponseDto
                {
                    Message = user == null ? "Your Email is not Registered." : "Wrong Password."
                };
            }
            var user_Token = await _context.UsersToken.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();
            var user_role = await _context.MDRoles.Where(u => u.Id.Equals(user.Role)).FirstOrDefaultAsync();
            var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();
            var name = ReferalCode(user.UserName.ToLower(), 5);
            var number = RandomStringv(3);
            var referal = name + number;
            var referalExist = await _context.UsersReferal.Where(u => u.ReferalCode.Equals(referal)).FirstOrDefaultAsync();
            

            if (user.Status != 1)
            {
                return new AuthResponseDto { Message = "Your Account Has Been " + user_stts.Desc };

            }
            if (user_Token == null && user.UsersLogins.Count == 0)
            {
                var generateToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                var userLogin = new UsersLogin
                {
                    IdUsersLogin = Guid.NewGuid().ToString(),
                    IdUser = user,
                    Ip = request.Ip,
                    Device = request.Device,
                    Status = "Online",
                    LastLogin = DateTime.Now,
                };

                var usertoken = new UsersToken
                {
                    UserTokenID = Guid.NewGuid().ToString(),
                    IdUser = user,
                    RefreshToken = generateToken,
                    TokenCreated = DateTime.Now
                };

                var userAmount = new UsersAmount
                {
                    AmountID = Guid.NewGuid().ToString(),
                    IdUser = user.UserId.ToString(),
                    amount = 200000000,
                    Desc = "Free Amount From Register Registering",
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    FreeSpin = DateTime.UtcNow,
                    //UserAmount Stands for Ads, Edited by: William Tan
                    AdsCount = 0,
                    TimeForAds = DateTime.UtcNow
                };

                var userReferal = new UsersReferal
                {
                    ReferalId = Guid.NewGuid().ToString(),
                    UsersId = user,
                    ReferalCode = referalExist == null ? referal : name + RandomStringv(3),
                    CreatedOn = DateTime.Now
                };

                _context.UsersReferal.Add(userReferal);
                _context.UsersLogin.Add(userLogin);
                _context.UsersToken.Add(usertoken);
                _context.UserAmount.Add(userAmount);
                _context.UsersLogin.Add(userLogin);

                
                await _context.SaveChangesAsync();
                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();
               // var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.TimeForAds).FirstOrDefaultAsync();

                var test = new AuthResponseDto
                {
                    Success = true,
                    Message = "Login",
                    UserId = user.UserId.ToString(),
                    Email = user.Email,
                    UserName = user.UserName,
                    Profile = user.profilePicture,
                    Role = user_role.Desc,
                    Status = user_stts.Desc,
                    Referral = userReferal.ReferalCode,
                    Gender = user.Gender,
                    Currency = userAmount.amount,
                    FreeSpin = userAmount.FreeSpin.ToString(),
                    RefreshToken = generateToken,
                    ReferalJoin = user.ReferalJoin == null ? "" : user.ReferalJoin,
                    AdsCount = userAmount.AdsCount.ToString(),
                    TimeForAds = userAmount.TimeForAds.ToString()

                };
                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login",
                    UserId = user.UserId.ToString(),
                    Email = user.Email,
                    UserName = user.UserName,
                    Profile = user.profilePicture,
                    Role = user_role.Desc,
                    Status = user_stts.Desc,
                    Referral = userReferal.ReferalCode,
                    Gender = user.Gender,
                    Currency = userAmount.amount,
                    FreeSpin = userAmount.FreeSpin.ToString(),
                    RefreshToken = generateToken,
                    ReferalJoin = user.ReferalJoin == null ? "" : user.ReferalJoin,
                    AdsCount = userAmount.AdsCount.ToString(),
                    TimeForAds = userAmount.TimeForAds.ToString()

                };

            }
            else
            {
                var generateToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

                var amount = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).SumAsync(v => v.amount);
                var freespin = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.FreeSpin).FirstOrDefaultAsync();
                var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.TimeForAds).FirstOrDefaultAsync();
                //var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
                user.UsersLogins.FirstOrDefault().Ip = request.Ip;
                user.UsersLogins.FirstOrDefault().Device = request.Device;
                user.UsersLogins.FirstOrDefault().Status = "Online";
                user.UsersLogins.FirstOrDefault().LastLogin = DateTime.Now;

                user_Token.RefreshToken = generateToken;
                user_Token.TokenCreated = DateTime.Now;

                _context.SaveChangesAsync();
                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login",
                    UserId = user.UserId.ToString(),
                    Email = user.Email,
                    UserName = user.UserName,
                    Profile = user.profilePicture,
                    Role = user_role.Desc,
                    Status = user_stts.Desc,
                    Referral = user.UsersReferals.FirstOrDefault().ReferalCode,
                    Gender = user.Gender,
                    Currency = _func.getAmount(user.UserId.ToString()),
                    RefreshToken = generateToken,
                    FreeSpin = freespin.FreeSpin.ToString(),
                    ReferalJoin = user.ReferalJoin,

                    //Shows Ads Response Time
                    AdsCount = ads.AdsCount.ToString(),
                    TimeForAds = ads.TimeForAds.ToString()
                };
            }

        }
        #endregion

        #region Logout Method

        public async Task<Handling> Logout(LogoutDto request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            var user_login = await _context.UsersLogin.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();
            user_login.Status = "Offline";
            user_login.Device = Guid.NewGuid().ToString();
            user_login.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return new Handling
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Logout"
            };
        }
        #endregion

        #region Register Method 
        public async Task<Handling> Register(AuthRegistercs request)
        {
            if(request.Username.ToLower().Replace(" ","").Length < 6 || request.Username == null || request.Username == "")
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Minimum username character is 6"

                };
            }
            var checkEmail = _context.MDUsers.Where(u => u.Email.Equals(request.Email)).FirstOrDefault();
            var checkUser = _context.MDUsers.Where(u => u.UserName.Equals(request.Username.ToLower())).FirstOrDefault();

            /*        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                      Regex validate = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$");
                      string Trimmed = String.Concat(request.Username.Where(c => !Char.IsWhiteSpace(c)));*/

            if (isValidUserName(request.Username) || !isValidEmail(request.Email))
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = isValidUserName(request.Username) ? "Username can't be spaces" : "Email isn't Valid"

                };
            }
            if (checkUser != null || checkEmail != null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = checkEmail != null ? "Email Already Registered" : "Username Already Registered"

                };

            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new MDUsers
            {
                UserId = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.Username.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = 1,
                Status = 1,
                Gender = true,
                CreatedOn = DateTime.Now,
                profilePicture = _func.urlApi + "File/ProfilePicture/Male1.png"

            };
            _context.MDUsers.Add(user);
            await _context.SaveChangesAsync();
            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Registration is Succesfully"
            };
        }
        #endregion

        #region SendResetPassword
        public async Task<Handling> SendResetPassword(GetEmail request)
        {
            var checkEmail = await _context.MDUsers.Where(u => u.Email.Equals(request.email)).FirstOrDefaultAsync();
            if (checkEmail == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = "Email Not Found."
                };
            }
            var mail = CreateEmailMessage(request.email);
            await SendAsync(mail);
            return new Handling
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Check your email for reset password"
            };
        }
        #endregion

        #region Change Password
        public async Task<Handling> ChangePassword(ResetPasswordRequest request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            var userToken = await _context.UsersToken.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();

            if (userToken.IdTokenGoogle != null || userToken.IdTokenFacebook != null)
            {
                CreatePasswordHash(request.newPassword, out byte[] passwordHash1, out byte[] passwordSalt1);
                user.PasswordSalt = passwordSalt1;
                user.PasswordHash = passwordHash1;
                await _context.SaveChangesAsync();

                return new Handling
                {
                    Result = true,
                    Code = 200,
                    Message = "Change Password successfully"
                };
            }
            if (user == null || !VerifyPasswordHash(request.oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = user == null ? "User Not Found" : "Wrong Password"
                };
            }

            CreatePasswordHash(request.newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();

            return new Handling
            {
                Result = true,
                Code = 200,
                Message = "Change Password successfully"
            };

        }
        #endregion

        #region Private Method

        private static bool isValidEmail(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //regex.IsMatch(email);
                //var addr = new System.Net.Mail.MailAddress(email);
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private static bool isValidUserName(string userName)
        {
            return userName.Contains(" ");
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computerHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        private string CreateToken(MDUsers user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                // Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now // for client datetime.UtcNow
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken refreshToken, UsersToken user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
            };

            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;

            _context.SaveChanges();
        }

        public static string ReferalCode(string name, int length)
        {
            string referal = name;
            string firstThree = referal.Substring(0, length);
            return firstThree;
        }


        private static string RandomStringv(int length)
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000).ToString("D" + length);

            return r.ToString();
        }
        private static string RandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?@#$&/abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private MimeMessage CreateEmailMessage(string mail)
        {
            var user = _context.MDUsers.Where(u => u.Email.Equals(mail)).FirstOrDefault();
            var randomPassword = RandomPassword(8);

            CreatePasswordHash(randomPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;

            user.PasswordSalt = passwordSalt;
            _context.SaveChanges();

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("the home studios", "noreply@gamethehomestudios.com"));
            emailMessage.To.Add(new MailboxAddress(mail, mail));
            emailMessage.Subject = "Grand Poker Reset Password";

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<!doctype html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"utf-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <title>Grand Poker Reset Password </title>\r\n</head>\r\n  <body>\r\n    <div class=\"container\">\r\n        <div style=\"  margin: auto;\r\n        width: 50%;\r\n        border: 2px solid gray;\r\n        padding: 10px;\">\r\n            <div style=\"  text-align: center;\">\r\n                <h1>Grand Poker</h1>\r\n                <h4>Reset password your account</h4>    \r\n            </div>\r\n            <hr>\r\n            <div class=\"card-body\">\r\n                <p>Grand Poker received a request for password recovery from {1} </p>\r\n                <p >Use this code for a temporary password:</p>\r\n                <h2 style=\" text-align: center;\r\n                color: black; font-weight: bold;\">{0}</h2>\r\n                <footer>If you do not recognize {1} you can safely ignore this email.</footer>\r\n            </div>\r\n          </div>\r\n    </div>\r\n  </body>\r\n</html>", randomPassword, mail) };

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

        #region check auth
        public async Task<checkLogin> CheckLogin(CheckService request)
        {
            checkLogin rsp = new checkLogin();
            try
            {
                
                     var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.userID)).FirstOrDefaultAsync();
            
                 //    var sessionList = _context.UsersLogin.Where(p => p.IdUser.Equals(user)  && p.Device.Equals(request.deviceID)).FirstOrDefault();
                     var sessionList = _context.UsersLogin.Where(p => p.IdUser.Equals(user) && p.Device.Equals(request.deviceID) && p.Status.Equals("Online")).FirstOrDefault();

                     var freespin = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId)).OrderByDescending(v => v.FreeSpin).FirstOrDefaultAsync();

                    var user_Token = await _context.UsersToken.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();
                   // var user_status = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();

                    var user_Referal = await _context.UsersReferal.Where(u => u.UsersId.Equals(user)).FirstOrDefaultAsync();

                    var user_role = await _context.MDRoles.Where(u => u.Id.Equals(user.Role)).FirstOrDefaultAsync();

                    var token =  await _context.UsersToken.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();
                    var user_stts = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();

                    var sessionLists = _context.UsersLogin.Where(p => p.IdUser.Equals(user)).ToList();

/*
                    foreach (var list in sessionLists)
                    {
                        rsp.Success = true;
                        list.Status = sessionList != null ?  "Online" : "Offline";
                    }
                    _context.SaveChanges();
*/


                    rsp.Success = true;
                    rsp.Message = sessionList != null? "Device used in the same place" : "Please Try to Login";
                    rsp.Code = sessionList != null ? _codes.ok : _codes.forbidden;
                    rsp.UserId = sessionList != null ? request.userID : null;
                    rsp.UserName = sessionList != null ? user.UserName : null;
                    rsp.Email = sessionList != null ? user.Email : null;
                    rsp.Profile = sessionList != null ? user.profilePicture : null;
                   
                    rsp.Gender = sessionList != null ? user.Gender : null;
                    rsp.Referral = sessionList != null ? user_Referal.ReferalCode.ToString() : null;
                    rsp.RefreshToken = sessionList != null ? user_Token.RefreshToken.ToString() : null;
                    rsp.Role = sessionList != null ? user_role.Desc.ToString() : null;
                    rsp.ReferalJoin = sessionList != null ? user.ReferalJoin : null;
                    
                    rsp.Currency = sessionList != null ? _func.getAmount(user.UserId.ToString()) : null;
                    rsp.IdTokenGoogle = sessionList != null ? user_Token.IdTokenGoogle : null;
                    rsp.IdTokenFacebook = sessionList != null ? user_Token.IdTokenFacebook : null;

                    rsp.Status = sessionList != null ? user_stts.Desc : null;
                    rsp.FreeSpin = sessionList != null ? freespin.FreeSpin.ToString() : null;
                    
                    

                if (sessionList == null)
                {
                    //var sessionLists = _context.UsersLogin.Where(p => p.IdUser.Equals(user)).ToList();
                    foreach (var list in sessionLists)
                    {
                        rsp.Success = true;
                        rsp.Message = "You Need To Log In";
                        //list.Status = "Offline";
                    }
                    _context.SaveChanges();
                }


                /* rsp.Success = true; 
                 rsp.Message = sessionList != null ? "Device used in the same place" : "Device used to login is different, logging out now";
                 rsp.Code = sessionList != null ? _codes.ok : _codes.forbidden;
                 rsp.UserId = sessionList != null ? request.userID : null;
                 rsp.UserName = sessionList != null ? user.UserName : null;
                 rsp.Email = sessionList != null ? user.Email : null;
                 rsp.Profile = sessionList != null ? user.profilePicture : null;
                 rsp.Status = sessionList != null ? user_stts.Desc : null;
                 rsp.Gender = sessionList != null ? user.Gender : null;
                 rsp.Referral = sessionList != null ? user_Referal.ReferalCode.ToString() : null;
                 rsp.RefreshToken = sessionList != null ? user_Token.RefreshToken.ToString() : null;
                 rsp.Role = sessionList != null ? user_role.Desc.ToString() : null;
                 rsp.ReferalJoin = sessionList != null ? user.ReferalJoin : null;
                 rsp.FreeSpin = sessionList != null ? freespin.FreeSpin.ToString() : null;
                 rsp.Currency = sessionList != null ? _func.getAmount(user.UserId.ToString()) : null;
                 rsp.IdTokenGoogle = sessionList != null ? user_Token.IdTokenGoogle : null;
                 rsp.IdTokenFacebook = sessionList != null ? user_Token.IdTokenFacebook : null;*/

                /* if (sessionList == null)
                 {
                     var users = _context.UsersLogin.Where(p => p.IdUser.Equals(user)).ToList();
                     foreach (var list in users)
                     {
                         rsp.Success = true;
                         user.Status = 0;
                     }
                     _context.SaveChanges();
                 }*/




            }

            catch (Exception err)
            {
                rsp.Code = _codes.error;
                rsp.Message = err.Message.ToString();
            }

            return rsp;
        }
        #endregion

        #region DisconnectPlayer
        public async Task<Handling> disconnectPlayer(disconnectPlayerDto request)
        {
             var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId))
             .Include(u => u.UsersLogins)
             .FirstOrDefaultAsync();

         
            var sessionList = await _context.UsersLogin.Where(v => v.IdUser.Equals(user))
                
                .FirstOrDefaultAsync();
           
            if (user == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Player Id False"
                };
            }


            sessionList.Status = "Offline";

            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Player Disconnected",
            };
        }
  
    #endregion
    }
}
