namespace Pic.Domain.Test;

public class UserTest
{

    [Fact]
    public void Should_Create_Object_User()
    {
        var create = User.Create(Fixtures.Name, Fixtures.Email, Fixtures.Document, Fixtures.Password, Fixtures.Type);

        Assert.NotNull(create);
        Assert.NotEqual(create.Id, Guid.Empty);
        Assert.Equal(create.Name, Fixtures.Name);
        Assert.Equal(create.Email, Fixtures.Email);
        Assert.Equal(create.Document, Fixtures.Document);
        Assert.Equal(create.Password, Fixtures.Password);
    }
}
