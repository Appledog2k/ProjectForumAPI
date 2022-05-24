using Articles.Models;
using Articles.Models.DTOs;

namespace Articles.Services.DataHandling
{
    public interface IAuthManager
    {
        Task<AccountManagerResponse> SignUpAsync(UserDTO userDTO);
        Task<AccountManagerResponse> LoginAsync(LoginUserDTO loginUserDTO);
        Task<AccountManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<AccountManagerResponse> ForgetPasswordAsync(string email);
        Task<AccountManagerResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);

    }
}