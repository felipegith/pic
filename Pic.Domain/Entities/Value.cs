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
    public void SetBalance(decimal balance)
    {
        Balance = balance;
    }
    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public bool VerifyBalanceToTransfer(decimal balance, decimal value)
    {
        if(balance <= 0)
        {
            return false;
        }

        return balance >= value ? true : false;
    }
}
