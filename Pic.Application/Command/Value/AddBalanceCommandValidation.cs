using FluentValidation;

namespace Pic.Application;

public class AddBalanceCommandValidation : AbstractValidator<AddBalanceCommand>
{
    public AddBalanceCommandValidation()
    {
        RuleFor(x=>x.Balance).GreaterThan(0);
        RuleFor(x=>x.UserId).NotEmpty();
    }
}
