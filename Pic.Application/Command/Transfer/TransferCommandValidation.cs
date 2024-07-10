using FluentValidation;

namespace Pic.Application;

public class TransferCommandValidation : AbstractValidator<TransferCommand>
{
    public TransferCommandValidation()
    {
        RuleFor(x=>x.Value).GreaterThan(0);
    }
}
