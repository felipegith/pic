using NSubstitute;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Test;


public class UserTest
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();


    [Fact]
    public void Should_Create_User_Type_Common_And_Return_Id()
    {
        
         var create = User.Create(Fixture.Name, Fixture.Email, Fixture.Document, Fixture.Password, Fixture.Type);

         _userRepository.Create(create);

         _userRepository.Received(1).Create(create);
         Assert.NotEqual(create.Id, Guid.Empty);

    }
}
