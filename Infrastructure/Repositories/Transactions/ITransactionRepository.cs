using System;
using System.Collections.Generic;
using Domain.Models.Entities;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Transactions
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        IEnumerable<Transaction> ListByUserAndDateRange(Guid userId, DateTime start, DateTime end);
        IEnumerable<Transaction> ListByDateRange(Guid userId, DateTime start, DateTime end);
    }
}
