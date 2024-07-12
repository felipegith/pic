using System.Net;
using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class AddBalanceCommandHandler : IRequestHandler<AddBalanceCommand, Response>
{
    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _uow;

    public AddBalanceCommandHandler(IValueRepository valueRepository, IUnitOfWork uow)
    {
        _valueRepository = valueRepository;
        _uow = uow;
    }

    public async Task<Response> Handle(AddBalanceCommand request, CancellationToken cancellationToken)
    {
        var create = Value.Create(request.Balance, request.UserId);

        if (create is null)
        {
            return new Response(false, "Balane should be greater than zero", HttpStatusCode.BadRequest);
        }
        _valueRepository.Create(create);
        await _uow.Commit();
        return new Response(true, "Sucess", System.Net.HttpStatusCode.Created, create);
    }
}
