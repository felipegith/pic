using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application.Test;

public class CreateUserCommandTest
{
    
    private static readonly CreateUserCommand Command = new (Fixture.Name, Fixture.Email, Fixture.Document, Fixture.Password, Fixture.Type);
    private readonly IUserRepository _userRepositoryMoq;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandTest()
    {
        _userRepositoryMoq = Substitute.For<IUserRepository>();
        _handler = new CreateUserCommandHandler(_userRepositoryMoq);
    }
    
    [Fact]
    public async Task Handle_Should_Create_User_And_Return_Data()
    {
        _userRepositoryMoq.Create(Fixture.UserMoq());
        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        Assert.True(result.Success);
        Assert.IsType<Response>(result);
        Assert.IsType<User>(result.Content);
        Assert.Equal(Fixture.Success, result.Message);
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
        
    }
     [Fact]
     public async Task Handle_Verify_If_Email_Already_Exists_Database()
     {
        // Arrange
        _userRepositoryMoq.IsUniqueEmail(Fixture.Email, default).Returns(true);

        // Act
        var result = await _handler.Handle(Command, default);
        
        //Assert
        await _userRepositoryMoq.Received(1).IsUniqueEmail(Fixture.Email, default);
        Assert.Equal(result.Message, Fixture.EmailExists);
        Assert.False(result.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);

     }
    
    [Fact]
     public async Task Handle_Verify_If_Email_Dont_Exists_Database_To_Create_New_User()
     {
        //Arrange
        _userRepositoryMoq.IsUniqueEmail(Fixture.Email, default).Returns(false);

        //Act
        var result = await _handler.Handle(Command, default);
        await _userRepositoryMoq.Received(1).IsUniqueEmail(Fixture.Email, default);
        Assert.Equal(result.Message, Fixture.Success);
        Assert.True(result.Success);
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
                 
     }
     [Fact]
     public async Task Handle_Verify_If_Document_Already_Exists_Database()
     {
        //Arrange
        _userRepositoryMoq.IsUniqueDocument(Fixture.Document, default).Returns(true);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        await _userRepositoryMoq.Received(1).IsUniqueDocument(Fixture.Document, default);
        Assert.Equal(Fixture.DocumentExists, result.Message);
        Assert.False(result.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        
     }

      [Fact]
      public async Task Handle_Verify_If_Document_Dont_Exists_Database_To_Create_New_User()
      {
         //Arrange
        _userRepositoryMoq.IsUniqueDocument(Fixture.Document, default).Returns(false);

        //Act
        var result = await _handler.Handle(Command, default);

        //Assert
        await _userRepositoryMoq.Received(1).IsUniqueDocument(Fixture.Document, default);
        Assert.Equal(result.Message, Fixture.Success);
        Assert.True(result.Success);
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
     }
}
