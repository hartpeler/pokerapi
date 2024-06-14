using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using th_poker_api.DTO;
using th_poker_api.DTO.Game;
using th_poker_api.Model.Success;
using th_poker_api.Services.GameplayService;


namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        #region function
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IGameplayService _gameplayService;
        private MessageCodes _codes = new MessageCodes();
        private Functions _funcs = new Functions();


        public GamesController(DataContext dataContext, IMapper mapper, IGameplayService gameplayService)
        {
            _context = dataContext;
            _mapper = mapper;
            _gameplayService = gameplayService;
        }

        #endregion

        #region post room table
       /* [HttpPost("post-table")]
        public async Task<ActionResult<responseGameHeader>> GameplayHeader(postGameHeader request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.Apikey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = true,
                        Code = _codes.error,
                        Message = "Invalid Api Key"
                    });
                }
                else
                {
                    var update = await _gameplayService.GameplayHeader(request);
                    if (update == null)
                    {
                        return BadRequest(new Handling() 
                        { 
                            Result = false,    
                            Code = _codes.error,
                            Message = "post error"
                        });
                    }
                    else
                    {
                        return Ok(update);
                    }
                }

            } catch (Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = true,
                    Code = _codes.error,
                    Message = err.Message
                });
            }
                
        }
       */
        #endregion

        #region Join Table
        [HttpPost("join-table")]
        public async Task<ActionResult<Handling>> joinTable (joinGameDto request)
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
                    var _post = await _gameplayService.joinGame(request);

                    return !_post.Result ?  BadRequest(_post) : Ok(_post);
                    
                }
            }
            catch(Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = true,
                    Code = _codes.error,
                    Message = err.Message
                }); 
            }
        }
        #endregion

        #region Stand Up Table
        [HttpPost("standup-fold")]
        public async Task<ActionResult<Handling>> standTable(standupDto request)
        {
            try
            {
                if(!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Invalid API Key"
                    }) ;
                }
                else
                {
                    var _stand = await _gameplayService.standTable(request);

                    return !_stand.Result ? BadRequest(_stand) : Ok(_stand);
                }
            }
            catch(Exception err)
            {
                return BadRequest(new Handling()
                { 
                    Result= false,
                    Code = _codes.error,
                    Message= err.Message
                });
            }

        }

        #endregion

        #region Win Lose

        [HttpPost("win-lose")]
        public async Task<ActionResult<Handling>> winLose (winLose request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = _codes.error,
                        Message = "Api Key Invalid",
                    });
                }
                var response = await _gameplayService.winlose(request);

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

        #region Get-Room-Detail
        [HttpPost("Get-Room-Detail")]


        public async Task<IActionResult> roomDetail(Room_Detail request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = true,
                        Code = _codes.error,
                        Message = "Invalid Api Key",
                    });
                }
                var response = await _gameplayService.roomDetail(request);

                return !response.Result ? BadRequest(response) : Ok(response);

            }
            catch (Exception err)
            {
                return BadRequest(new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = err.Message,
                });
            }
        }
        #endregion
        #region Bet Log
        [HttpPost("betlog")]
        public async Task<ActionResult<Handling>> betlog(BetPutDTO request)
        {
            try
            {
                if (!_funcs.validateAPIKey(request.apiKey))
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
                    var handler = await _gameplayService.BetAndPut(request);

                    return Ok(new Handling()
                    {
                        Result = true,
                        Code = _codes.ok,
                        Message = "Data Updated"
                    }) ;

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
        #region Transfer
        [HttpPost("transfer/{apiKey}")]
        public async Task<ActionResult<Handling>> Transfer([FromRoute] string apiKey, [FromBody] Transfer request)
        {
            try
            {
                if (!_funcs.validateAPIKey(apiKey))
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
                    var handler = await _gameplayService.Transfer(request);

                    return Ok(new Handling()
                    {
                        Result = true,
                        Code = _codes.ok,
                        Message = "Data Updated"
                    });

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

        #region claim
        [HttpPost("claim/{apiKey}")]
        public async Task<ActionResult<Handling>> Claim ([FromRoute] string apiKey, [FromBody] string ClaimID)
        {
            try
            {
                if (!_funcs.validateAPIKey(apiKey))
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
                    var handler = await _gameplayService.UpdateClaimStatus(ClaimID);
                    return handler;

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
        #region claim
        [HttpGet("claim/{apiKey}/{userID}")]
        public async Task<ActionResult<List<ClaimDTO>>> ClaimList([FromRoute] string apiKey, [FromRoute] string userID)
        {
            try
            {
                if (!_funcs.validateAPIKey(apiKey))
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
                    var handler = await _gameplayService.GetClaimList(userID);
                    return handler;

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
