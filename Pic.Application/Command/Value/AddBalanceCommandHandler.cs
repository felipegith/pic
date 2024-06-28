using System.Net;
using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class AddBalanceCommandHandler : IRequestHandler<AddBalanceCommand, Response>
{
    private readonly IValueRepository _valueRepository;

    public AddBalanceCommandHandler(IValueRepository valueRepository)
    {
        _valueRepository = valueRepository;
    }

    public async Task<Response> Handle(AddBalanceCommand request, CancellationToken cancellationToken)
    {
        var create = Value.Create(request.Balance, request.UserId);

        if (create is null)
        {
            return new Response(false, "Balane should be greater than zero", HttpStatusCode.BadRequest);
        }
        
        return new Response(true, "Sucess", System.Net.HttpStatusCode.Created, create);
    }
}
