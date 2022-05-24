using Articles.Models;
using Articles.Models.DTOs;
using Articles.Services.DataHandling;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        // private readonly ISendMail _sendMail;
        private readonly ISendMailService _sendMailService;
        private readonly IConfiguration _configuration;


        public AccountController(IAuthManager authManager, ISendMailService sendMailService, IConfiguration configuration)

        {
            _authManager = authManager;
            _sendMailService = sendMailService;
            _configuration = configuration;
        }

        //* /api/account/login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _authManager.LoginAsync(loginUserDTO);
                if (result.IsSuccess)
                {
                    var mailContent = new MailContent();

                    mailContent.To = loginUserDTO.Email;
                    mailContent.Subject = "Sign In Articles Page";
                    mailContent.Body = "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>";
                    await _sendMailService.SendGMailAsync(mailContent);
                    // await _sendMail.SendMailAsync("x2vosong@gmail.com", "x2vosong@gmail.com", "SignIn WebApi", "hello", "x2vosong@gmail.com", "=))))");
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        //* /api/account/signup

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            // if (ModelState.IsValid)
            // {
            //     var result = await _accountRepository.SignUpAsync(signUpModel);
            //     if (result.IsSuccess)
            //     {
            //         return Ok(result);
            //     }
            //     return BadRequest(result);
            // }
            // return BadRequest("Some properties are not invalid");
            var result = await _authManager.SignUpAsync(userDTO);
            return Ok(result);
        }

        //* /api/account/confirmemail?userid&token
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if ((string.IsNullOrEmpty(userId)) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var result = await _authManager.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/confirmemail.html");

            }
            return BadRequest(result);
        }

        //* /api/account/forgetpassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }
            var result = await _authManager.ForgetPasswordAsync(email);
            if (result.IsSuccess)
            {
                return Ok(result); // status code 200
            }
            return BadRequest(result); // status code 400
        }

        //* /api/account/resetpassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authManager.ResetPasswordAsync(resetPasswordModel);
                if (result.IsSuccess)
                {
                    return Ok(result); // status code 200
                }
                return BadRequest(result); // status

            }
            return BadRequest("Some properties are not valid");

        }
    }
}