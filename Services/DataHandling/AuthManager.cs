using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.WebUtilities;
using Articles.Models.DTOs;
using Articles.Data;
using Articles.Services.Mail;
using AutoMapper;
using Articles.Models;

namespace Articles.Services.DataHandling
{
    public class AuthManager : IAuthManager
    {
        private ApiUser _user;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ISendMailService _sendMailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<ApiUser> userManager,
                           SignInManager<ApiUser> signInManager,
                           IConfiguration configuration,
                           ISendMailService sendMailService,
                           IHttpContextAccessor httpContextAccessor,
                           IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _sendMailService = sendMailService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        //  TODO: Sign Up --- done
        public async Task<bool> RegisterAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/account/confirmemail?userid={user.Id}&token={validEmailToken}";

                var mailContent = new MailContent();
                mailContent.To = userDTO.Email;
                mailContent.Subject = "Sign In Articles Page";
                mailContent.Body = $"<p>Please click the link to confirm your email: <a href='{url}'>Click here</a></p>";
                await _sendMailService.SendGMailAsync(mailContent);
                await _userManager.AddToRolesAsync(user, userDTO.Roles);

                return true;
            }
            return false;

        }

        // TODO: Login --- done

        public async Task<string> LoginAsync(LoginUserDTO loginUserDTO)
        {
            _user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, loginUserDTO.Password);
            // claim ownership
            var authClaims = new List<Claim>
            {
                new Claim("Email", loginUserDTO.Email),
                new Claim(ClaimTypes.NameIdentifier, _user.Id),
            };
            //coding for JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            // create token 
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1), // deadline for token
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            var mailContent = new MailContent();

            mailContent.To = loginUserDTO.Email;
            mailContent.Subject = "Sign In Articles Page";
            mailContent.Body = "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>";
            await _sendMailService.SendGMailAsync(mailContent);
            if (_user != null && validPassword)
            {
                return tokenAsString;
            }
            throw new BusinessException(Resource.Resource.LOGIN_FAIL);

        }

        // TODO:: LogoutAsync
        public async Task<string> LogoutAsync()
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            //Gets list of claims.
            IEnumerable<Claim> claims = identity.Claims;

            var usernameClaim = claims
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            var user = await _userManager.FindByNameAsync(usernameClaim.Value);
            var result = await _userManager.RemoveAuthenticationTokenAsync(user, "Web", "Access");
            if (result.Succeeded)
            {
                return Resource.Resource.LOGOUT_SUCCESS;
            }
            throw new BusinessException(Resource.Resource.LOGOUT_FAIL);
        }


        // TODO: Confirm Email --- done
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            List<string> error = new List<string>();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
            {
                return Resource.Resource.CONFIRMED_SUCCESS;
            }
            else
            {
                foreach (var e in result.Errors)
                {
                    error.Add(e.Description);
                }
                throw new BusinessException(error[0]);
            }

        }


        // TODO: Forget Password --- done
        public async Task<string> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return Resource.Resource.FORGET_PASSWORD_SUCCESS;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            var mailContent = new MailContent();
            mailContent.To = email;
            mailContent.Subject = "Sign In Articles Page";
            mailContent.Body = "<h1>Follow the instructions to reset your password</h1>" + $"<p>Please click the link to reset your password: <a href='{url}'>Click here</a></p>";
            await _sendMailService.SendGMailAsync(mailContent);
            return Resource.Resource.FORGET_PASSWORD_SUCCESS;
        }
        // TODO: Reset Password --- done

        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return Resource.Resource.RESET_PASSWORD_SUCCESS;
            }

            if (resetPasswordDTO.NewPassword != resetPasswordDTO.ConfirmPassword)
            {
                return Resource.Resource.RESET_PASSWORD_FAIL;
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDTO.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordDTO.NewPassword);
            if (result.Succeeded)
            {
                return Resource.Resource.RESET_PASSWORD_SUCCESS;
            }
            return Resource.Resource.RESET_PASSWORD_FAIL;
        }
    }
}
