using Articles.Models;
using FluentValidation;

namespace Articles.Views.FluentValidation
{
    class SignInValidation : AbstractValidator<SignInModel>
    {
        public SignInValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}