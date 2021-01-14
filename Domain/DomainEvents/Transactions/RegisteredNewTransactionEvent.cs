using Domain.DomainEvents.Base.Impl;
using Domain.Models.Entities;

namespace Domain.DomainEvents.Transactions
{
    public class RegisteredNewTransactionEvent : DomainEventBase
    {
        public Transaction Transaction { get; set; }
        public RegisteredNewTransactionEvent(Transaction transaction) : base(transaction.Id)
        {
            Transaction = transaction;
        }
    }
}
