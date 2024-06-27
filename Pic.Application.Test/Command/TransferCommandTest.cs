using System.Net;
using Pic.Domain;

namespace Pic.Application.Test;

public class TransferCommandTest
{
    public static TransferCommand Command = new (Fixture.Type, Fixture.Value, Fixture.Payer, Fixture.Payee);
    
    public readonly TransferCommandHandler _handle;
    public TransferCommandTest()
    {
        _handle = new TransferCommandHandler();
    }

    [Fact]
    public async Task Handle_Should_Verify_If_Type_User_To_Transfer_Is_Common()
    {
        //Arrange
        var type = User.GetUserType(Fixture.Type);

        //Act
        var result = await _handle.Handle(Command, default);

        //Assert
        Assert.IsType<Response>(result);
        Assert.True(result.Success);
        Assert.Equal("Successfully transfer", result.Message);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(Fixture.Type.ToString(), type);
    }
}
