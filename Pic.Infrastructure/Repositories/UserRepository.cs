using Microsoft.EntityFrameworkCore;
using Pic.Domain;

namespace Pic.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly Context _context;

    public UserRepository(Context context)
    {
        _context = context;
    }

    public void Create(User user)
    {
        try
        {
            _context.Users.Add(user);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    public async Task<User> FindUserTypeForDocument(long document, CancellationToken cancellationToken)
        => await _context.Users.Include(x=>x.Value).FirstOrDefaultAsync(x=>x.Document == document);

    public async Task<bool> IsUniqueDocument(long document, CancellationToken cancellationToken)
    {
        var uniqueDocument = await _context.Users.FirstOrDefaultAsync(x=>x.Document == document);
        
        return uniqueDocument is null ? false : true;
    }
     
    public async Task<bool> IsUniqueEmail (string email, CancellationToken cancellationToken)
    {
       var uniqueEmail =  await _context.Users.FirstOrDefaultAsync(x=>x.Email == email, cancellationToken);

       return uniqueEmail is null ? false : true;
      
    }
     
}
