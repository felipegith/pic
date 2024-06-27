using Pic.Domain;

namespace Pic.Infrastructure;

public interface IUserRepository
{
    void Create(User user);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
    Task<bool> IsUniqueDocument(string document, CancellationToken cancellationToken);
}
