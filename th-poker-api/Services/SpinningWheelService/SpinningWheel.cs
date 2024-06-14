using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.DTO.SpinningWheel;
using th_poker_api.Model.Amount;

namespace th_poker_api.Services.SpinningWheelService
{
    public class SpinningWheel : ISpinningWheel
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _func = new Functions();

        public SpinningWheel(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region Update Spinning Wheel
        public async Task<responseSWDto> updateSW(updateSWDto request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
            //var ads = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(u => u.CreatedOn).FirstOrDefaultAsync();
            var _postAmount = new UsersAmount
            {
                AmountID = Guid.NewGuid().ToString(),
                IdUser = request.UserId,
                amount = request.amount,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
                FreeSpin = DateTime.UtcNow.AddHours(2),
                Desc = "Amount From FreeSpin",
                AdsCount = ads.AdsCount,
                TimeForAds = ads.TimeForAds

                
                
            };

            _context.UserAmount.Add(_postAmount);
            await _context.SaveChangesAsync();
      //     var userAmount = await _context.UserAmount.OrderByDescending(u => u.CreatedOn).ToListAsync();
          //  var purchase = await _context.MDPaymentItem.OrderBy(v => v.Value).ToListAsync();
            return new responseSWDto
            {
                Result = true,
                code = _codes.accepted,
                Message = "Success",
                amount = _func.getAmount(user.UserId.ToString()) + request.amount,
                FreeSpin = _postAmount.FreeSpin.ToString(),
                AdsCount = ads.AdsCount.ToString(),
                TimeForAds = ads.TimeForAds.ToString()

            };
           
        }
        #endregion


        #region Reset Ads Spinning Wheel
        public async Task<responseResetSW> freeAds(updateResetWheel request)
        {

            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            //var freeSpin = await _context.UserAmount.Where(u => u.IdUser.Equals(request.UserId)).OrderByDescending(u =>u.CreatedOn).FirstOrDefaultAsync();
            var freeSpin = await _context.UserAmount.Where(u => u.IdUser.Equals(user.UserId.ToString())).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();
            if (user.UserId != request.UserId)
            {
                return new responseResetSW
                {
                    Result = false
                };
            }

            var _postAmount = new UsersAmount
            {
                AmountID = Guid.NewGuid().ToString(),
                IdUser = request.UserId,
                amount = request.amount,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
                AdsCount = request.AdsCount,
                FreeSpin = freeSpin.FreeSpin,
                Desc = "Reset From FreeSpin Ads",
                TimeForAds = DateTime.UtcNow.AddMinutes(5)
            };

            _context.UserAmount.Add(_postAmount);
            await _context.SaveChangesAsync();
          // var userAmount = await _context.UserAmount.OrderByDescending(u => u.CreatedOn).ToListAsync();
            return new responseResetSW
            {
                Result = true,
                code = _codes.accepted,
                Message = "Reset Spinning Wheel Succeeded",
                amount = _func.getAmount(user.UserId.ToString()) + request.amount,
                CreatedOn = _postAmount.CreatedOn.ToString(),
                AdsAmount = _postAmount.AdsCount,
                FreeSpin = _postAmount.FreeSpin.ToString(),
                TimeForAds = _postAmount.TimeForAds.ToString()
                 
                
            };

        }
        #endregion

    }

}
