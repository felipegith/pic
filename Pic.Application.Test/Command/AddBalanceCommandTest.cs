using System.Net;
using NSubstitute;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application.Test;

public class AddBalanceCommandTest
{
    public static AddBalanceCommand Command = new AddBalanceCommand(1000, Guid.NewGuid());
    private readonly IValueRepository _valueRepository;
    private readonly AddBalanceCommandHandler _handler;

    public AddBalanceCommandTest()
    {
        _valueRepository = Substitute.For<IValueRepository>();
        _handler = new AddBalanceCommandHandler(_valueRepository);
    }

    [Fact]
    public async Task Handle_Should_Add_New_Balance()
    {
        var result = await _handler.Handle(Command, default);
        Assert.IsType<Response>(result);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
    
}
