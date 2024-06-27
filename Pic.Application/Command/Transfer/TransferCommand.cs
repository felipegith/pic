using MediatR;

namespace Pic.Application;

public record TransferCommand (Domain.Type Type, decimal Value, decimal Payer, decimal Payee) : IRequest<Response>;

