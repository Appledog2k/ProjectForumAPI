using Articles.Models;
using Articles.Models.DTOs;

namespace Articles.Services.DataHandling
{
    public interface IAuthManager
    {
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<string> LoginAsync(LoginUserDTO loginUserDTO);
        Task<string> LogoutAsync();

        Task<string> CreateTokenAsync();
        Task<string> ConfirmEmailAsync(Guid userId, string key);
        Task<string> ForgetPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

    }
}