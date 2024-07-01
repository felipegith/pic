using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class TransferCommandHandler : IRequestHandler<TransferCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IConsultService _consultService;

    public TransferCommandHandler(IUserRepository userRepository, IConsultService consultService)
    {
        _userRepository = userRepository;
        _consultService = consultService;
    }

    public async Task<Response> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
      var user = await _userRepository.FindUserTypeForDocument(request.Payer, cancellationToken);
      
      var userType = User.GetUserType(user.Type);

      if(userType == Domain.Type.Shopkeeper.ToString())
      {
        return new Response(false, "Invalid type to transfer", System.Net.HttpStatusCode.BadRequest);
      }
      
      var checkBalanceToTransfer = user.Value.VerifyBalanceToTransfer(user.Value.Balance, request.Value);

      if(!checkBalanceToTransfer)
      {
        return new Response(false, "Insufficient funds", System.Net.HttpStatusCode.BadRequest);
      }

      var verifyServiceToTransfer = await _consultService.GetAuthorizeTransfer() ?? new HttpRequestResponse(){ Status = "Fail", Data = new Authorize {Authorization = false}};
      
      if(!verifyServiceToTransfer.Data.Authorization)
      {
        return new Response(false, "Service unavailable", System.Net.HttpStatusCode.Forbidden);
      }
       
      return new Response(true, "Successfully transfer", System.Net.HttpStatusCode.OK);
    }
  
    
}
