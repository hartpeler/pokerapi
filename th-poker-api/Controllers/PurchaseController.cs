using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using th_poker_api.DTO;
using th_poker_api.DTO.Purchase;
using th_poker_api.Model.Success;
using th_poker_api.Services.PurchaseService;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        #region function
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPurchaseService _purchaseService;
        private MessageCodes _codes = new MessageCodes();
        private Functions _funcs = new Functions();

        public PurchaseController(DataContext dataContext, IMapper mapper, IPurchaseService purchaseService)
        {
            _context = dataContext;
            _mapper = mapper;
            _purchaseService = purchaseService;
        }
        #endregion

        #region TopUp 
        [HttpPost("topup")]
        public async Task<ActionResult<Handling>> topup(topupDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = true,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                var response = await _purchaseService.topUp(request);

                return !response.Result ? BadRequest(response) : Ok(response);

            }
            catch (Exception err)
            {
                var errs = err;
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message,
                });
            }
        }
        #endregion
    }
}
