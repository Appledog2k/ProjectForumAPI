using Articles.Models;
using Microsoft.AspNetCore.Identity;

namespace Articles.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new AppUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email
            };
            return await _userManager.CreateAsync(user, signUpModel.Password);

        }


    }
}