using FluentValidation;
using Imagegram.Models.Commands;

namespace Imagegram.API.Validators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(post => post.Image)
                .NotNull()
                .WithMessage("Please attach photo to your post");

            RuleFor(post => post.Image.ContentType)
                .NotNull()
                .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/jpg") || contentType.Equals("image/png") || contentType.Equals("image/bmp"))
                .When(post => post.Image != null)
                .WithMessage("Provided image type is not valid");

        }
    }
}
