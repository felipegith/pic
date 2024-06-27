
namespace Pic.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        var success = (await _context.SaveChangesAsync()) > 0;

        return success;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
