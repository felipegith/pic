using MediatR;

namespace Pic.Application;

public record TransferCommand (decimal Value, long Payer, decimal Payee) : IRequest<Response>;

