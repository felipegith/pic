using MediatR;

namespace Pic.Application;

public record class CreateUserCommand(string Name, string Email, string Document, string Password, Domain.Type Type) : IRequest<Response>;

