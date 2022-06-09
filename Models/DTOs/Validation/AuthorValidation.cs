using Articles.Models.DTOs;
using Articles.Services.Resource;
using FluentValidation;

namespace Articles.Models.DTOs.Validation
{
    public class AuthorValidation : AbstractValidator<Create_AuthorDTO>
    {
        public AuthorValidation()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Tên"))
            .MinimumLength(3).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Tên", "3"))
            .MaximumLength(50).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Tên", "50"));
        }

    }
}