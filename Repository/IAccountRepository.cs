using Articles.Models;
using Microsoft.AspNetCore.Identity;

namespace Articles.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);

    }
}