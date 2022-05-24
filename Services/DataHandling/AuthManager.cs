using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Articles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.WebUtilities;
using Articles.Models.DTOs;
using Articles.Data;
using Articles.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Services.DataHandling
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ISendMailService _sendMailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManager(UserManager<ApiUser> userManager,
                           SignInManager<ApiUser> signInManager,
                           IConfiguration configuration,
                           ISendMailService sendMailService,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _sendMailService = sendMailService;
            _httpContextAccessor = httpContextAccessor;
        }

        //  TODO: Sign Up --- done
        public async Task<AccountManagerResponse> SignUpAsync(UserDTO userDTO)
        {
            if (userDTO == null) throw new NullReferenceException("SignUpModel is null");

            if (userDTO.Password != userDTO.ConfirmPassword)
            {
                return new AccountManagerResponse
                {
                    Message = "Password and Confirm Password do not match",
                    IsSuccess = false,
                    Errors = new List<string> { "Password and Confirm Password do not match" }
                };
            }

            var user = new ApiUser()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            // test succeeded
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

                return new AccountManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }
            else
            {
                return new AccountManagerResponse
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

        }

        // TODO: Login --- done

        public async Task<AccountManagerResponse> LoginAsync(LoginUserDTO loginUserDTO)
        {
            // Find out if there are any accounts with the same email as you just entered
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            if (user == null)
            {
                return new AccountManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
            if (!result)
            {
                return new AccountManagerResponse
                {
                    Message = "Ivalid password",
                    IsSuccess = false,
                };
            }
            // claim ownership
            var authClaims = new List<Claim>
            {
                new Claim("Email", loginUserDTO.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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
            return new AccountManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExprieDate = token.ValidTo
            };
        }


        // TODO: Confirm Email --- done
        public async Task<AccountManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AccountManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false,
                };

            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
            {
                return new AccountManagerResponse
                {
                    Message = "Email confirmed",
                    IsSuccess = true,
                };
            }
            else
            {
                return new AccountManagerResponse
                {
                    Message = "Email not confirmed",
                    IsSuccess = false,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }
        }

        // TODO: Forget Password --- done
        public async Task<AccountManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AccountManagerResponse
                {
                    Message = "No user associated with this email",
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
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
            return new AccountManagerResponse
            {
                Message = "Resset password URL has been sent to email successfully",
                IsSuccess = true
            };
        }
        // TODO: Reset Password --- done

        public async Task<AccountManagerResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user == null)
            {
                return new AccountManagerResponse
                {
                    Message = "No user associated with this email",
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            if (resetPasswordViewModel.NewPassword != resetPasswordViewModel.ConfirmPassword)
            {
                return new AccountManagerResponse
                {
                    Message = "Passwords do not match",
                    IsSuccess = false,
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordViewModel.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordViewModel.NewPassword);
            if (result.Succeeded)
            {
                return new AccountManagerResponse
                {
                    Message = "Password reset successfully",
                    IsSuccess = true,
                };
            }
            return new AccountManagerResponse
            {
                Message = "Password not reset",
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description)
            };

        }
    }
}
