using MediatR;

namespace Pic.Application;

public record class CreateUserCommand(string Name, string Email, long Document, string Password, Domain.Type Type) : IRequest<Response>;

