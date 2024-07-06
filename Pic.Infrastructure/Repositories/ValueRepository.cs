using Pic.Domain;

namespace Pic.Infrastructure;

public class ValueRepository : IValueRepository
{
    private readonly Context _context;

    public ValueRepository(Context context)
    {
        _context = context;
    }

    public void Create(Value value)
    {
        try
        {
            _context.Values.Add(value);
        }
        catch (Exception exception)
        {
            Console.Write(exception.Message);
            throw;
        }
    }

    public void Update(Value value)
    {
        var getValue = _context.Values.FirstOrDefault(x=>x.UserId == value.UserId);
        getValue?.SetBalance(value.Balance);
    }
}
