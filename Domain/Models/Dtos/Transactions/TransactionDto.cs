using Domain.Models.Entities;
using System;

namespace Domain.Models.Dtos.Transactions
{
    public class TransactionDto
    {
        public Guid Id { get; }
        public DateTime Date { get; }
        public Guid From { get; }
        public Guid To { get; }
        public double Value { get; }

        public TransactionDto(Guid id, DateTime date, Guid @from, Guid to, double value)
        {
            Id = id;
            Date = date;
            From = @from;
            To = to;
            Value = value;
        }

        public static explicit operator TransactionDto(Transaction ent)
            => new TransactionDto(ent.Id, ent.CreationDate, ent.From, ent.To, ent.Value);
    }
}
