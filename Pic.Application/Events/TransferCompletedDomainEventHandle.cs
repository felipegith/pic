using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class TransferCompletedDomainEventHandle : INotificationHandler<TransferCompletedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IValueRepository _valueRepository;
    private readonly ISendEmailService _sendEmailService;
    private readonly IUnitOfWork _uow;

    public TransferCompletedDomainEventHandle(IUserRepository userRepository, ISendEmailService sendEmailService, IValueRepository valueRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _sendEmailService = sendEmailService;
        _valueRepository = valueRepository;
        _uow = uow;
    }

    public async Task Handle(TransferCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        
        var user = await _userRepository.FindUserTypeForDocument(notification.Document, cancellationToken);
        user.Value?.AddValueBalance(user.Value.Balance, notification.Value);
        _valueRepository.Update(user.Value);
        
        await _uow.Commit();
        _sendEmailService.SendEmail(user.Email);
    }
}
