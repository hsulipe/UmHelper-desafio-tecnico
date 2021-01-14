using Domain.DomainEvents.Transactions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UmHelpFinanceiro.Services.Users;

namespace UmHelpFinanceiro.DomainEventHandlers.Transactions
{
    public class RevertTransactionEventHandler : INotificationHandler<RevertTransactionEvent>
    {
        private readonly IUserService _userService;

        public RevertTransactionEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(RevertTransactionEvent notification, CancellationToken cancellationToken)
        {
            await _userService.WithdrawAsync(notification.Transaction.To, notification.Transaction.Value);
            await _userService.DepositAsync(notification.Transaction.From, notification.Transaction.Value);
        }
    }
}
