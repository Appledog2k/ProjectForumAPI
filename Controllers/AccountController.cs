using Articles.Models;
using Articles.Models.DTOs;
using Articles.Services.DataHandling;
using Articles.Services.Mail;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ISendMailService _sendMailService;
        private readonly IConfiguration _configuration;


        public AccountController(IAuthManager authManager, ISendMailService sendMailService, IConfiguration configuration)

        {
            _authManager = authManager;
            _sendMailService = sendMailService;
            _configuration = configuration;
        }


        // todo : login
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {

            var result = await _authManager.LoginAsync(loginUserDTO);
            return Ok(result);

        }

        // todo : register

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            var result = await _authManager.SignUpAsync(userDTO);
            return Ok(result);
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