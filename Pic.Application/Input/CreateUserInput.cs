namespace Pic.Application;

public record CreateUserInput(string Name, string Email, long Document, string Password, Domain.Type Type);

