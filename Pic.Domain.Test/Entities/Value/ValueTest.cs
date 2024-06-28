namespace Pic.Domain.Test;

public class ValueTest
{
    [Fact]
    public void Should_Create_Value_With_Balance_GreaterThan_Zero()
    {
        var create = Value.Create(Fixtures.Balance, Fixtures.Id);

        Assert.NotNull(create);
        Assert.NotEqual(create.Id, Guid.Empty);
        Assert.True(create.Balance >= 0);
        
    }
    [Fact]
    public void Should_Return_Null_For_Values_LesThan_Or_Equal_Zero()
    {
        // Act 
        var create = Value.Create(Fixtures.InvalidBalance, Fixtures.Id);
        //Assert
        Assert.Null(create);
    }
}


