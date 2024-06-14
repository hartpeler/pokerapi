using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Org.BouncyCastle.Asn1.Ocsp;
using th_poker_api.DTO;
using th_poker_api.DTO.Friend;
using th_poker_api.DTO.Purchase;
using th_poker_api.DTO.Response;
using th_poker_api.Model.Purchase;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.PurchaseService
{
    public class PurchaseService : IPurchaseService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _func = new Functions();
        public PurchaseService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

   

        #endregion

        #region Push Top Up
        public async Task<Handling> topUp (topupDto request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();

            if (user == null) 
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "User Not Found"
                };
            }

            var _postPurchase = new TSPurchase
            {
                IdPurchase = Guid.NewGuid().ToString(),
                IdUser = user.UserId.ToString(),
                IdPymItem = request.paymentNum,
                IdStatus = Convert.ToInt32(request.platform),
                Amount = _func.getAmountPurchase(request.paymentNum),
                Description = "",
                Amount_a = request.prevVal,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
            };

            _context.TSPurchases.Add(_postPurchase);
            await _context.SaveChangesAsync();  

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Purchase Successfully"
            };
        }
        #endregion


    }
}
