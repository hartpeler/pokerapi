using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography.X509Certificates;
using th_poker_api.DTO;
using th_poker_api.DTO.SpinningWheel;
using th_poker_api.Model.Success;
using th_poker_api.Services.SpinningWheelService;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SpinningWheelController : Controller
    {
        #region Function
        private readonly ISpinningWheel _spinningWheel;
        private readonly DataContext _context;
        Functions _funcs = new Functions();
        private MessageCodes _codes = new MessageCodes();

        public SpinningWheelController(ISpinningWheel spinningWheel, DataContext dataContext)
        {
            _spinningWheel = spinningWheel;
            _context = dataContext;
        }
        #endregion

        #region update Spinning Wheel
        [HttpPost("update-SW")]
        public async Task<IActionResult> updateSW(updateSWDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                else
                {
                    var roomall = await _spinningWheel.updateSW(request);

                    return !roomall.Result ? BadRequest(roomall) : Ok(roomall);
                }
            }
            catch (Exception err)
            {

                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message.ToString()
                });
            }    
        }
        #endregion
        #region Reset Spinning-Wheel
        [HttpPost("freeAds")]
        public async Task<IActionResult> freeAds(updateResetWheel request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                else
                {
                    var roomall = await _spinningWheel.freeAds(request);

                    return !roomall.Result ? BadRequest(roomall) : Ok(roomall);
                }
            }
            catch (Exception err) 
            {
                return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = err.Message.ToString()
                });
            }
        }
        #endregion 

    }
}
