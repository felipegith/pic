using Pic.Domain;

namespace Pic.Infrastructure;

public interface IValueRepository
{
    void Create(Value value);
    void Update(Value value);
}
