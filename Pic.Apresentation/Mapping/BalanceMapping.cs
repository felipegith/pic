using Mapster;
using Pic.Application;

namespace Pic.Apresentation;

public class BalanceMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddBalanceInput, AddBalanceCommand>();
    }
}
