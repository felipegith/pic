

namespace Pic.Domain;

public record class TransferCompletedDomainEvent(string Email, decimal Value, long Document) : IDomainEvent;

