using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;
using Articles.Services.Resource;
using FluentValidation;

namespace Articles.Models.DTOs.Validation
{
    public class ArticleValidation : AbstractValidator<ArticleCreateRequest>
    {
        public ArticleValidation()
        {
            RuleFor(o => o.Title).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Tiêu đề"))
            .MinimumLength(3).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Tiêu đề", "3"))
            .MaximumLength(50).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Tiêu đề", "50"));

            RuleFor(o => o.Content).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Nội dung"))
            .MinimumLength(3).WithMessage(string.Format(Resource.VALIDATION_MIN_LENGTH, "Tiêu đề", "3"))
            .MaximumLength(500).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Tiêu đề", "500"));

        }

    }
}