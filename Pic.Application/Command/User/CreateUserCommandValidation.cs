using FluentValidation;

namespace Pic.Application;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(x=>x.Document).NotEmpty();
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
        RuleFor(x=>x.Password).NotEmpty();
        RuleFor(x=>x.Type).NotEmpty();
    }
}
