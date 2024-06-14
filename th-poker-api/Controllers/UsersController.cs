using Microsoft.AspNetCore.Mvc;
using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.Model.Error;
using th_poker_api.Model.Success;
using th_poker_api.Services.UserService;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        #region Function
        private readonly IUserService _userService;
        Functions _func = new Functions();
        private MessageCodes _codes = new MessageCodes();
        private readonly DataContext _context;
        public UsersController(DataContext dataContext, IUserService userService)
        {
            _context = dataContext;
            _userService = userService;
        }
        #endregion

        #region Get Player
        [HttpPost("Get-player")]
        public async Task<ActionResult<responseGetPlayer>> Getuser(getPlayerDto request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }

                var user = await _userService.GetPlayer(request);
                return !user.Success ? BadRequest(user) : Ok(user);

            } catch(Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
            
        }
        #endregion

        #region Update Profile
        [HttpPost("Update-Profile")]
        public async Task<ActionResult<Handling>> UpdatePofile(UpdateProfile request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"

                    });
                }
                
                var response = await _userService.UpdateProfile(request);
                return !response.Result ? BadRequest(response) : Ok(response);
                
            } catch(Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
           
        }
        #endregion

        #region Upload Profile
        [HttpPost("upload-profile")]
        public async Task<ActionResult<Handling>> uploadProfile( updatePictureDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.ApiKey))
                {
                    return NotFound(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                else
                {
                    var response = await _userService.AddPicture(request);
                    return !response.Result ? BadRequest(response) : Ok(response);
                }
            }
            catch(Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
            
        }

        #endregion

        #region User Referal 

        [HttpPost("user-referal")]
        public async Task<ActionResult<Handling>> referalJoin (referalCodeJoinDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.APIKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"

                    });
                }
                var response = await _userService.referalJoin(request);
                return !response.Result ? BadRequest(response) : Ok(response);

            } catch (Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
        }
        #endregion

    }
}
