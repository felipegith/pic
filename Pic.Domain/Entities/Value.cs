namespace Pic.Domain;

public class Value
{
    public Guid Id {get; private set;}
    public decimal Balance {get; private set;}
    public Guid UserId {get; private set;}
    public virtual User User {get; private set;} 

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

    public static Value Update(decimal balance, User user, decimal valueTransfer, long paye)
    {
         var value = new Value()
         {
             UserId = user.Id,
             Balance = balance
        };
        
        user.AddDomainEvent(new TransferCompletedDomainEvent(user.Email, valueTransfer, paye));
        
        return value;
    }
    
    public static decimal Debit(decimal balance, decimal value)
    {
       if(balance <= 0) throw new ArgumentOutOfRangeException();
       return balance - value;
    }
    public void SetBalance(decimal balance)
    {
        Balance = balance;
    }
    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }
    public void AddValueBalance(decimal value, decimal balance)
    {
        var sum = balance + value;
        SetBalance(sum);
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
