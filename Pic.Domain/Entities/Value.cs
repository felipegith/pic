namespace Pic.Domain;

public class Value
{
    public Guid Id {get; private set;}
    public decimal Balance {get; private set;}
    public Guid UserId {get; private set;}

    public static Value? Create(decimal balance, Guid userId)
    {
        if(balance <= 0)
        {
            return null;
        }

        return new Value()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Balance = balance
        };
    }
}
