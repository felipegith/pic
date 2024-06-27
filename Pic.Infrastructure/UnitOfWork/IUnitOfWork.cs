namespace Pic.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
