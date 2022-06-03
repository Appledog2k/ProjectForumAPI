using Articles.Models.DTOs;
using Articles.Services.Resource;
using FluentValidation;

namespace Articles.Models.Validation
{
    public class UserValidation : AbstractValidator<UserDTO>
    {
        public UserValidation()
        {
            RuleFor(o => o.Email).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Email"))
            .EmailAddress().WithMessage(string.Format(Resource.VALIDATION_DISPLAY, "Email"));
            RuleFor(o => o.Password).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Password"));
        }

    }
}