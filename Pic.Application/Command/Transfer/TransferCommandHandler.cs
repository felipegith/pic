using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class TransferCommandHandler : IRequestHandler<TransferCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _uow;
    private readonly IConsultService _consultService;
    public TransferCommandHandler(IUserRepository userRepository, IConsultService consultService, IValueRepository valueRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _consultService = consultService;
        _valueRepository = valueRepository;
        _uow = uow;
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

      var verifyServiceToTransfer = await _consultService.GetAuthorizeTransfer() ?? new HttpRequestResponse(){ Status = "Success", Data = new Authorize {Authorization = true}}; 
      
        if(!verifyServiceToTransfer.Data.Authorization)
        {
          return new Response(false, "Service unavailable", System.Net.HttpStatusCode.Forbidden);
        }
      var debit = Value.Debit(user.Value.Balance, request.Value);
      
      var value = Value.Update(debit, user, request.Value, request.Payee);
      _valueRepository.Update(value);
            
      await _uow.Commit();
      return new Response(true, "Successfully transfer", System.Net.HttpStatusCode.OK);
    }
  
    
}
