using MediatR;
using Pic.Domain;

namespace Pic.Application;

public class TransferCommandHandler : IRequestHandler<TransferCommand, Response>
{
    public async Task<Response> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
      var userType = User.GetUserType(request.Type);

      if(userType == Domain.Type.Shopkeeper.ToString())
      {
        return new Response(false, "Invalid type to transfer", System.Net.HttpStatusCode.BadRequest);
      }
      
      return new Response(true, "Successfully transfer", System.Net.HttpStatusCode.OK);
    }
}
