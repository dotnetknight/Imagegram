using FluentValidation;
using Imagegram.Models.Commands;

namespace Imagegram.API.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(account => account.Name).NotEmpty();
        }
    }
}
