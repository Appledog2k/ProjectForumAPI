using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.DataHandling;
using Articles.Services.Resource;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // todo : ctor
        private readonly IAuthManager _authManager;
        private readonly IConfiguration _configuration;
        public AccountController(IAuthManager authManager, IConfiguration configuration)
        {
            _authManager = authManager;
            _configuration = configuration;
        }



        // TODO: register
        [HttpPost]
        [Route("/register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var result = await _authManager.RegisterAsync(userDTO);
            return Ok(new Response(Resource.REGISTER_SUCCESS));

        }

        // TODO: login
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _authManager.LoginAsync(loginUserDTO);
            return Ok(new Response(Resource.LOGIN_SUCCESS, null, new { Token = result }));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authManager.LogoutAsync();
            return Ok(new Response(result));
        }



        // todo : confirmEmail
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authManager.ConfirmEmailAsync(userId, token);
            return Redirect($"{_configuration["AppUrl"]}/confirmemail.html");
        }

        // todo : forgetPassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authManager.ForgetPasswordAsync(email);
            return Ok(result);
        }

        // todo : reset password
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _authManager.ResetPasswordAsync(resetPasswordDTO);
            return Ok(result);
        }

    }
}