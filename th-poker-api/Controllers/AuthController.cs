using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;
using th_poker_api.DTO;
using th_poker_api.DTO.Auth;
using th_poker_api.Model.Success;
using th_poker_api.Services.EmailService;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq.Expressions;

namespace th_poker_api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        /// <summary>
        /// Functions
        /// </summary>
        private readonly IAuthService _authService;
        private readonly DataContext _context;
        private Functions _func = new Functions();
        private MessageCodes _codes = new MessageCodes();

        public AuthController(IAuthService authService, DataContext dataContext)
        {
            _authService = authService;
            _context = dataContext;
        }


        /// <summary>
        /// Creates Registration Auth
        /// </summary>
        /// <param name="Register"></param>
        /// <returns>Create Register Account for Login</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<Handling>> Register(AuthRegistercs request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }

                var response = await _authService.Register(request);

                return !response.Result ? BadRequest(response) : Ok(response);

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
       
        /// <summary>
        /// Login using Google Sign in
        /// </summary>
        /// <param name="Login Google"></param>
        /// <returns></returns>
        [HttpPost("login-google")]
        public async Task<IActionResult> LoginGoogle(loginGoogleDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.Apikey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }

                var response = await _authService.LoginGoogle(request);

                return !response.Result ? BadRequest(response) : Ok(response);

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

        /// <summary>
        /// Login Facebook using Facebook Account
        /// </summary>
        /// <param name="Login Facebook"></param>
        /// <returns></returns>
        [HttpPost("login-facebook")]
        public async Task<IActionResult> loginFb(loginFbDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.ApiKey))
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }

                var response = await _authService.LoginFacebook(request);

                return !response.Result ? BadRequest(response) : Ok(response);

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

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDto>> Login(UserDTO request)
        {

            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }

                var response = await _authService.Login(request);

                return !response.Success ? BadRequest(response) : Ok(response);
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

        /// <summary>
        /// Logout Function
        /// </summary>
        /// <param name="Logout"></param>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(LogoutDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.ApiKey))
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
                    var response = await _authService.Logout(request);

                    return !response.Result ? BadRequest(response) : Ok(response);
                }

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

        /// <summary>
        /// Send Email to Reset Password
        /// </summary>
        /// <param name="Reset-Password"></param>
        /// <returns></returns>
        [HttpPost("Send-Reset-Password")]
        public async Task<ActionResult<Handling>> SendResetPassword(GetEmail request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"
                    });
                }

                var mail = await _authService.SendResetPassword(request);

                return !mail.Result ? BadRequest(mail) : Ok(mail);

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

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="Change-Password"></param>
        /// <returns></returns>
        [HttpPost("Change-Password")]
        public async Task<ActionResult<Handling>> ChangePassword(ResetPasswordRequest request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }
                var changePassword = await _authService.ChangePassword(request);

                return !changePassword.Result ? BadRequest(changePassword) : Ok(changePassword);
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

        /// <summary>
        /// Check Session
        /// </summary>
        /// <param name="CheckSession"></param>
        /// <returns></returns>
        [HttpPost("CheckSession")]
        public async Task<ActionResult<responseGoogleLogin>> CheckLogin(CheckService request)
        {
            try
            {
                if (_func.validateAPIKey(request.APIKey) == false)
                {
                    return BadRequest(new Handling()
                    {
                        Result = false,
                        Code = 400,
                        Message = "Invalid API Key"

                    });
                }
                var response = await _authService.CheckLogin(request);

                return !response.Success ? BadRequest(response) : Ok(response);

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

       
        /// <summary>
        /// Disconnecting Player
        /// </summary>
        /// <param name="DisconnectPlayer"></param>
        /// <returns></returns>
        [HttpPost("DisconnectPlayer")]
        public async Task<IActionResult> disconnectPlayer(disconnectPlayerDto request)
        {
            try
            {
                if (!_func.validateAPIKey(request.APIKey))
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
                    var response = await _authService.disconnectPlayer(request);

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
       
    }
}

