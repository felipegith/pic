using System.Diagnostics.CodeAnalysis;
using System.Net;
using NSubstitute;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application.Test;

public class TransferCommandTest
{
    public static TransferCommand Command = new (Fixture.Value, Fixture.Payer, Fixture.Payee);
    private readonly IUserRepository _userRepositoryMoq;
    public readonly TransferCommandHandler _handle;
    public TransferCommandTest()
    {
        _userRepositoryMoq = Substitute.For<IUserRepository>();
        _handle = new TransferCommandHandler(_userRepositoryMoq);
        
    }

    [Fact]
    public async Task Handle_Should_Verify_If_Type_User_To_Transfer_Is_Common_For_Document()
    {
        //Arrange
        
        //Act
        _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default).Returns(Fixture.UserMoq());
        var userMoq = await _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default);
        var result = await _handle.Handle(Command, default);
        var userType = User.GetUserType(userMoq.Type);

        //Assert
        Assert.IsType<Response>(result);
        Assert.True(result.Success);
        Assert.Equal(Fixture.Type.ToString(), userType);
        Assert.Equal("Successfully transfer", result.Message);
                
    }

    [Fact]
    public async Task Handle_Should_Be_Has_Balance_To_Transfer()
    {
        _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default).Returns(Fixture.UserMoq());
        var userMoq = await _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default);
        var result = await _handle.Handle(Command, default);

        Assert.IsType<Response>(result);
        Assert.True(result.Success);
        Assert.True(userMoq.Value?.Balance >= 0);
        Assert.True(userMoq.Value.Balance >= Fixture.Value);
    }
}
