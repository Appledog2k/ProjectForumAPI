using Articles.Models.DTOs;
using Articles.Models.Response;
using Articles.Services.Resource;
using Articles.Services.UserRepositories;
using Microsoft.AspNetCore.Mvc;
namespace Articles.Controllers
{
    [Route("forum/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AccountController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var result = await _userRepository.RegisterAsync(userDTO);
            return Ok(new Response(Resource.REGISTER_SUCCESS));
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _userRepository.LoginAsync(loginUserDTO);
            return Ok(new Response(Resource.LOGIN_SUCCESS, null, new { Token = result }));
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _userRepository.LogoutAsync();
            return Ok(new Response(result));
        }

        [HttpGet("/confirmemail")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var result = await _userRepository.ConfirmEmailAsync(userId, token);
            return Redirect($"{_configuration["AppUrl"]}/confirmemail.html");
        }

        [HttpPost("/ForgetPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _userRepository.ForgetPasswordAsync(email);
            return Ok(new Response(result));
        }

        [HttpPost("/ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _userRepository.ResetPasswordAsync(resetPasswordDTO);
            return Ok(new Response(result));
        }
    }
}