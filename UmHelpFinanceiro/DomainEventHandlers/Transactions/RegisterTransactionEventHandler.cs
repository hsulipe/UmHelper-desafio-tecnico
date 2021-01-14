using Domain.DomainEvents.Transactions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UmHelpFinanceiro.Services.Users;

namespace UmHelpFinanceiro.DomainEventHandlers.Transactions
{
    public class RegisterTransactionEventHandler : INotificationHandler<RegisteredNewTransactionEvent>
    {
        private readonly IUserService _userService;

        public RegisterTransactionEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(RegisteredNewTransactionEvent notification, CancellationToken cancellationToken)
        {
            await _userService.WithdrawAsync(notification.Transaction.From, notification.Transaction.Value);
            await _userService.DepositAsync(notification.Transaction.To, notification.Transaction.Value);
        }
    }
}
