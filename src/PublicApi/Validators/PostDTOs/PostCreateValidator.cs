using FluentValidation;
using Mublog.Server.PublicApi.Controllers.DTOs.V1.Posts;

#pragma warning disable 1591

namespace Mublog.Server.PublicApi.Validators.PostDTOs
{
    public class PostCreateValidator : AbstractValidator<PostCreateDto>
    {
        public PostCreateValidator()
        {
            RuleFor(x => x.Content).NotNull().WithMessage("Post cannot be empty");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Post cannot be empty");
            RuleFor(x => x.Content).Length(2, 1536).WithMessage("Post does not meet length requirements"); // TODO discuss max length
        }
    }
}