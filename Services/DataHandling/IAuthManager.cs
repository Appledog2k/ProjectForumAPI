using Articles.Models;
using Articles.Models.DTOs;

namespace Articles.Services.DataHandling
{
    public interface IAuthManager
    {
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<string> LoginAsync(LoginUserDTO loginUserDTO);
        Task<string> LogoutAsync();

        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<string> ForgetPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

    }
}