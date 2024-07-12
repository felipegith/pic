using Mapster;
using Pic.Application;

namespace Pic.Apresentation;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserInput, CreateUserCommand>();
    }
}
