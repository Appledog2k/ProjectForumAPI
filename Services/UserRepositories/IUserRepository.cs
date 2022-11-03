using Articles.Models.DTOs;

namespace Articles.Services.UserRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Xử lý đăng kí tài khoản
        /// </summary>
        Task<bool> RegisterAsync(UserDTO userDTO);
        /// <summary>
        /// Xử lý đăng nhập tài khoản
        /// </summary>
        Task<string> LoginAsync(LoginUserDTO loginUserDTO);
        /// <summary>
        /// Xử lý đăng suất tài khoản
        /// </summary>
        Task<string> LogoutAsync();
        Task<string> CreateTokenAsync();
        Task<string> ConfirmEmailAsync(Guid userId, string key);
        Task<string> ForgetPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }

}
