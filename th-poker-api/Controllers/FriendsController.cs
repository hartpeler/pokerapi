using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using th_poker_api.DTO.Friend;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Success;
using th_poker_api.Services.FriendsService;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class FriendsController : Controller
    {
        #region Function
        private readonly IFriendsService _friendsService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _funcs = new Functions();

        public FriendsController(DataContext dataContext, IFriendsService friendsService, IMapper mapper)
        {
            _context = dataContext;
            _friendsService = friendsService;
            _mapper = mapper;
        }
        #endregion

        #region Invitation
        [HttpPost("Friends-Invitation")]
        public async Task<IActionResult> invitationList(invitationGetDto request, CancellationToken ct)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var response = await _friendsService.invitationList(request, ct);

                    return !response.Success ? BadRequest(response) : Ok(response);
                }

            } catch(Exception err)
            {
                return BadRequest(new Handling() { 
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
        }

        #endregion

        #region Accepted
        [HttpPost("accept-friend")]
        public async Task<IActionResult> Accept(acceptDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });

                }
                var response = await _friendsService.acception(request);

                return !response.Result ? BadRequest(response) : Ok(response);

            }
            catch (Exception err)
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

        #region invitation Friend
        [HttpPost("invite-friend")]
        public async Task<IActionResult> invitationFriend (invitationFriendDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.Apikey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                var response = await _friendsService.invitationFriend(request);

                return !response.Result ? BadRequest(response) : Ok(response);

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

        #region FriendConnection
        [HttpPost("friend-Connection")]
        public async Task<IActionResult> friendConnection(friendsConnectionDTo request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var response = await _friendsService.friendConnection(request);
                    return !response.results ? BadRequest(response) : Ok(response);
                }
            }
            catch (Exception err)
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

        #region Search
        [HttpPost("seacrh-friend")]
        public async Task<IActionResult> searchFriend(searchDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var response = await _friendsService.searchFriend(request);

                    return !response.Result ? BadRequest(response) : Ok(response);
                }
            }catch(Exception err)
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

        #region Delete Friend
        [HttpPost("delete-friend")]
        public async Task<IActionResult> deleteFriend(delateFriendDto request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var response = await _friendsService.delateFriend(request);

                    return !response.Result ? BadRequest(response) : Ok(response);

                }
            }
            catch (Exception err)
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

        #region Decline Friend
        [HttpPost("decline-Friend")]
        public async Task<IActionResult> declineFriend(declineFriend request)
        {
            try
            {

                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var response = await _friendsService.declineFriend(request);

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

    }
}
