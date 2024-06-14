using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using th_poker_api.DTO;
using th_poker_api.DTO.MasterData;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Error;
using th_poker_api.Model.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        #region Function
        private readonly IRoomService _roomService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _funcs = new Functions();

        public RoomController(DataContext dataContext, IRoomService roomService, IMapper mapper)
        {
            _context = dataContext;
            _roomService = roomService;
            _mapper = mapper;
        }
        #endregion

        #region Create Room
        [HttpPost("create-room")]
        public async Task<ActionResult<Handling>> createRoom(postRoom request)
        {
            try
            {
                if (_funcs.validateAPIKey(request.ApiKey) == false)
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
                    var listRoom = await _roomService.createRoom(request);

                    return !listRoom.Result ? BadRequest(listRoom) :  Ok(listRoom);
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

        #region List Room
        [HttpPost("list-room")]
        public async Task<IActionResult> listRoom(listRoom request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.APIKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                if (request.idMDGames == null || request.idMDGames == "")
                {
                    var roomall =  await _roomService.GetAllGames();

                    if(!ModelState.IsValid)
                        return BadRequest(ModelState);
                    else
                        roomall.Success = true;
                        roomall.Message = "Success";
                        return Ok(roomall);
                }
                else
                {
                    var listRoom = await _roomService.listRoom(request);

                    return !listRoom.Success ? BadRequest(listRoom) : Ok(listRoom);

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

        #region List Game Type
        [HttpPost("get-game-type")]
        public async Task<IActionResult> roomType(roomType request)
        {

            try
            {
                if (!_funcs.validateAPIKey(request.APIKey))
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
                    var gameType = await _roomService.gameTypeRoom(request);

                    if (gameType.data.Count != 0)
                    {
                        gameType.Success = true;
                        gameType.Message = "Success";
                        return Ok(gameType);
                    }
                    else
                    {
                        gameType.Success = false;
                        gameType.Message = "False";
                        return BadRequest(gameType);
                    }

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

        #region Update Room
        [HttpPost("update-room")]
        public async Task<IActionResult> updateRoom (updateRoom request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.APIKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                var roomUpdate = await _roomService.updateRoom(request);

                return !roomUpdate.Result ? BadRequest(roomUpdate) : Ok(roomUpdate);
            }
            catch(Exception err)
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
        #region choose room

        [HttpPost("choose-room")]
        public async Task<IActionResult> ChooseRoom(updateRoom request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.APIKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    });
                }
                var chooseRoom = await _roomService.updateRoom(request);

                return !chooseRoom.Result ? BadRequest(chooseRoom) : Ok(chooseRoom);

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

        #region Player Join Room
        [HttpPost("Player-Join-Room")]
        public async Task<IActionResult> PlayerJoinRoom(playerRoom request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.apiKey))
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
                    var response = await _roomService.PlayerJoinRoom(request);

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

        #region Delete Room

        [HttpPost("Delete-Room")]
        public async Task<IActionResult> deleteRoom(GetRoomName request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.apiKey))
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
                    var response = await _roomService.deleteRoom(request);

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

    }
}
