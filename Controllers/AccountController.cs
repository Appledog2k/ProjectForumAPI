using Articles.Models;
using Articles.Repository;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        // private readonly ISendMail _sendMail;
        private readonly ISendMailService _sendMailService;
        private readonly IConfiguration _configuration;


        public AccountController(IAccountRepository accountRepository, ISendMailService sendMailService, IConfiguration configuration)

        {
            _accountRepository = accountRepository;
            _sendMailService = sendMailService;
            _configuration = configuration;
        }

        //* /api/account/login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.LoginAsync(signInModel);
                if (result.IsSuccess)
                {
                    var mailContent = new MailContent();

                    mailContent.To = signInModel.Email;
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
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.SignUpAsync(signUpModel);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not invalid");
        }

        //* /api/account/confirmemail?userid&token
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if ((string.IsNullOrEmpty(userId)) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var result = await _accountRepository.ConfirmEmailAsync(userId, token);
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
            var result = await _accountRepository.ForgetPasswordAsync(email);
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
                var result = await _accountRepository.ResetPasswordAsync(resetPasswordModel);
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