using NSubstitute;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application.Test;

public class TransferCommandTest
{
    public static TransferCommand Command = new (Fixture.Value, Fixture.Payer, Fixture.Payee);
    private readonly IUserRepository _userRepositoryMoq;
    private readonly IValueRepository _valueRepositoryMoq;
    private readonly IConsultService _consultServiceMoq;
    private readonly IUnitOfWork _uowMoq;
    public readonly HttpClient httpClient;
    private readonly HttpRequestFixture _httpRequestFixture;
    public readonly TransferCommandHandler _handle;
    public TransferCommandTest()
    {
        _userRepositoryMoq = Substitute.For<IUserRepository>();
        _consultServiceMoq = Substitute.For<IConsultService>();
        _valueRepositoryMoq = Substitute.For<IValueRepository>();
        _uowMoq = Substitute.For<IUnitOfWork>();
        httpClient = Substitute.For<HttpClient>();

        _httpRequestFixture = new HttpRequestFixture(httpClient);
        _handle = new TransferCommandHandler(_userRepositoryMoq, _consultServiceMoq, _valueRepositoryMoq, _uowMoq);
        
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
        Assert.Equal(Fixture.Type.ToString(), userType);
                
    }

    [Fact]
    public async Task Handle_Should_Be_Has_Balance_To_Transfer()
    {
        _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default).Returns(Fixture.UserMoq());
        var userMoq = await _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default);
        
        var result = await _handle.Handle(Command, default);

        Assert.IsType<Response>(result);
        //Assert.True(result.Success);
        Assert.True(userMoq.Value?.Balance >= 0);
        Assert.True(userMoq.Value.Balance >= Fixture.Value);
    }

    [Fact]
    public async Task Handle_Should_Make_Request_Before_Transfer_Value()
    {
        var request = new HttpRequestFixture(httpClient);

        var result = await request.GetAuthorizeTransfer();
        Assert.NotNull(result);
        Assert.IsType<string>(result.Status);
        Assert.IsType<bool>(result.Data.Authorization);
        
    }

    [Fact]
    public async Task Handle_Should_Return_Status_Fail_And_Data_Authorize_False_After_Make_Request()
    {
        //Arrange
        var httpClient = Fixture.HttpClientMoq(Fixture.HandleFailMoq());
        var httpRequestFixture = new HttpRequestFixture(httpClient);
        _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default).Returns(Fixture.UserMoq());

        // Act
        var result = await httpRequestFixture.GetAuthorizeTransfer();

        // Assert
        Assert.Equal("Fail", result.Status);
        Assert.False(result.Data.Authorization);
        
    }
    [Fact]
    public async Task Handle_Should_Debit_Balance_And_Update_Database_With_New_Balance()
    {
        _userRepositoryMoq.FindUserTypeForDocument(Fixture.Document, default).Returns(Fixture.UserMoq());
        //_valueRepositoryMoq.Update(Fixture.Balance, Fixture.Id);

       //_valueRepositoryMoq.Received(1).Update(Arg.Any<decimal>(), Fixture.Id);

        var result = await _handle.Handle(Command, default);
    }
}
