using MediatR;

namespace Pic.Application;

public record TransferCommand (decimal Value, long Payer, long Payee) : IRequest<Response>;

