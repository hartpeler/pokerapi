using System.Data;
using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.DTO.Player;
using th_poker_api.Model.Success;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using AutoMapper;
using th_poker_api.Model.Amount;

namespace th_poker_api.Services.UserService
{
    public class UserService : IUserService
    {

        #region Function 
        private readonly DataContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private MessageCodes _codes = new MessageCodes();
        private Functions _func = new Functions();

        public UserService(DataContext dataContext, IHostingEnvironment hostingEnvironment)
        {
            _context = dataContext;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region GetPlayer
        public async Task<responseGetPlayer> GetPlayer(getPlayerDto request)
        {

            responseGetPlayer dto = new responseGetPlayer();
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId))
                .Include(v => v.usersProfiles)
                .Include(v => v.UsersReferals)
                .FirstOrDefaultAsync();

            var freespin = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.FreeSpin).FirstOrDefaultAsync();
            var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();

            if (user != null)
            {
                var role = await _context.MDRoles.Where(u => u.Id.Equals(user.Role)).FirstOrDefaultAsync();
                var status = await _context.MDStatuses.Where(u => u.IdStatus.Equals(user.Status)).FirstOrDefaultAsync();
                var user_Token = await _context.UsersToken.Where(u => u.IdUser.Equals(user)).FirstOrDefaultAsync();


                dto.Success = true;
                dto.Message = "Success";
                dto.UserId = user.UserId.ToString();
                dto.Email = user.Email;
                dto.UserName = user.UserName;
                dto.Profile = user.profilePicture;
                dto.Referral = user.UsersReferals.FirstOrDefault().ReferalCode;
                dto.Gender = user.Gender;
                dto.Role = role.Desc;
                dto.Status = status.Desc;
                dto.Currency = _func.getAmount(user.UserId.ToString());
                dto.FreeSpin = freespin.FreeSpin.ToString();
                dto.DateTimeServer = DateTime.Now.ToString();
                dto.RefreshToken = user_Token.RefreshToken;
                dto.ReferalJoin = user.ReferalJoin;
                dto.adsCount = ads.AdsCount.ToString();
                dto.timeForAds = ads.TimeForAds.ToString();
            }
            else
            {
                dto.Success = false;
                dto.Message = "User is not exists";
            }
            return dto;
        }
        #endregion

        #region Update Profile Method
        public async Task<Handling> UpdateProfile( UpdateProfile request)
        {
            var user_exist = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();

            if (user_exist == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = "User Not Found"

                };
            }
            if (request.UserName.ToLower().Replace(" ", "").Length < 6 || request.UserName == null || request.UserName == "")
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Minimum username character is 6"

                };
            }
            user_exist.UserName = request.UserName;
            user_exist.Gender = request.Gender;

            await _context.SaveChangesAsync();
            return new Handling
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Profile Update"
            };
        }

        #endregion

        #region update Picture Profile 
        public async Task<Handling> AddPicture([FromForm] updatePictureDto request)
        {
            try
            {
                var profilePicture = _context.MDUsers.Where(p => p.UserId.Equals(request.userId)).FirstOrDefault();
                if (profilePicture == null)
                {
                    return new Handling()
                    {
                        Result = false,
                        Code = _codes.notfound,
                        Message = "User Not Found"

                    };
                }
                profilePicture.profilePicture = request.picture;
                _context.SaveChanges();
                return new Handling()
                {
                    Result = true,
                    Code = _codes.ok,
                    Message = "Profile picture updated"

                };
            }
            catch(Exception err)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.error,
                    Message =err.Message
                };
            }
        }
        #endregion

        #region Referal
        public async Task<Handling> referalJoin (referalCodeJoinDto request)
        {
            float amount = 200000000; // belum diimplementasikan
           

            var userJoin = await _context.MDUsers.Where(u => u.UserId.Equals(request.userId)).FirstOrDefaultAsync();

            var userReferal = await _context.UsersReferal.Where(u => u.ReferalCode.Equals(request.referalCode)).Include(v => v.UsersId).FirstOrDefaultAsync();
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(userReferal.UsersId.UserId.ToString())).FirstOrDefaultAsync();



            
            var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(request.userId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
            var adsReferal = await _context.UserAmount.Where(u => u.IdUser.Equals(request.userId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();

            if (userReferal == null || userJoin == null)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = userReferal == null ? "Referal Not Found" : "User Not Found"
                };
            }
            if (userJoin.UserId == userReferal.UsersId.UserId)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = "You Cannot Add Yourself"
                };
            }
            if (userJoin.ReferalJoin != null)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = "Can't Add Referral Code"
                };
            }

            var _postAmount = new UsersAmount
            {
                AmountID = Guid.NewGuid().ToString(),
                IdUser = user.UserId,
                amount = amount,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
                FreeSpin = ads.FreeSpin,
                AdsCount = ads.AdsCount,
                TimeForAds = ads.TimeForAds,
                Desc = "Amount From user use Referal code"

            };

            var _postAmountJoin = new UsersAmount
            {
                AmountID = Guid.NewGuid().ToString(),
                IdUser = userJoin.UserId,
                amount = amount,
                CreatedBy = userJoin.UserName,
                CreatedOn = DateTime.Now,
                Desc = "Amount From use referal Code",
                FreeSpin = adsReferal.FreeSpin,
                AdsCount = adsReferal.AdsCount,
                TimeForAds = adsReferal.TimeForAds

            };

            userJoin.ReferalJoin = request.referalCode;
            _context.UserAmount.Add(_postAmount);
            _context.UserAmount.Add(_postAmountJoin);
            await _context.SaveChangesAsync();

            return new Handling
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Success"
            };

        }
        #endregion


        #region Private Method

        private string getFileName(string filename)
        {
            string extension = Path.GetExtension(filename);
            int i = 0;
            while (File.Exists(filename))
            {
                if (i == 0)
                    filename = filename.Replace(extension, "(" + ++i + ")" + extension);
                else
                    filename = filename.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
            }
            return filename;
        }

        private static void resize(string fileParh, int width, int height)
        {
            var file = fileParh;

            using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var image = new Bitmap(imageStream))
            {
                var resizedImage = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                }
            }
        } 
        #endregion

    }

}

