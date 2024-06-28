using MediatR;

namespace Pic.Application;

public record class AddBalanceCommand(decimal Balance, Guid UserId) : IRequest<Response>;

