using Domain.DomainEvents.Base.Impl;
using Domain.Models.Entities;

namespace Domain.DomainEvents.Transactions
{
    public class RevertTransactionEvent : DomainEventBase
    {
        public Transaction Transaction { get; set; }
        public RevertTransactionEvent(Transaction transaction) : base(transaction.Id)
        {
            Transaction = transaction;
        }
    }
}
