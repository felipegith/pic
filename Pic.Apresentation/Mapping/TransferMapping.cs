using Mapster;
using Pic.Application;

namespace Pic.Apresentation;

public class TransferMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransferInput, TransferCommand>();
    }
}

