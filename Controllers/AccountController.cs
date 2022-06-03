using Articles.Models;
using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.DataHandling;
using Articles.Services.Mail;
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

        // todo : login
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _authManager.LoginAsync(loginUserDTO);
            return Ok(new Response(Resource.LOGIN_SUCCESS, null, new { Token = result }));
        }

        // todo : register
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var result = await _authManager.SignUpAsync(userDTO);
            return Ok(new Response(Resource.REGISTER_SUCCESS));

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