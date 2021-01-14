using System;
using Domain.Models.Entities;

namespace Domain.Models.Dtos.Transactions
{
    public class RegisterTransactionRequest
    {
        public Guid From { get; }
        public Guid To { get; }
        public double Value { get; }

        public RegisterTransactionRequest(Guid from, Guid to, double value)
        {
            From = from; 
            To = to;
            Value = value;
        }

        public static explicit operator Transaction(RegisterTransactionRequest request)
            => new Transaction(request.From, request.To, request.Value);
    }
}
