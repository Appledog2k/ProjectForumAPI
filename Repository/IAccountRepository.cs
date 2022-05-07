using Articles.Models;
using Microsoft.AspNetCore.Identity;

namespace Articles.Repository
{
    public interface IAccountRepository
    {
        Task<AccountManagerResponse> SignUpAsync(SignUpModel signUpModel);
        Task<AccountManagerResponse> LoginAsync(SignInModel signInModel);
        Task<AccountManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<AccountManagerResponse> ForgetPasswordAsync(string email);
        Task<AccountManagerResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);

    }
}