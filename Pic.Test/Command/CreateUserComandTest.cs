using NSubstitute;
using Pic.Application;
using Pic.Infrastructure;

namespace Pic.Test;

public class CreateUserComandTest
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    [Fact]
    public async Task Handle_Should_Create_User_And_Return_Id()
    {
        var command = new CreateUserCommand(Fixture.Name, Fixture.Email, Fixture.Document, Fixture.Password, Fixture.Type);
        var handle = new CreateUserCommandHandler(_userRepository);

        var result = await handle.Handle(command, default);

        Assert.NotEqual(result, Guid.Empty);

    }
}
