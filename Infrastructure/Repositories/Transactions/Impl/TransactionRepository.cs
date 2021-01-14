using Domain.Models.Entities;
using Infrastructure.Databases.Postgres;
using Infrastructure.Repositories.Base.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories.Transactions.Impl
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly FinanceDbContext _context;

        public TransactionRepository(FinanceDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> ListByUserAndDateRange(Guid userId, DateTime start, DateTime end)
        {
            return _context.Transactions
                .Where(x => (x.From == userId || x.To == userId) && 
                                     (start <= x.CreationDate && end >= x.CreationDate))
                .OrderByDescending(x => x.CreationDate);
        }

        public IEnumerable<Transaction> ListByDateRange(Guid userId, DateTime start, DateTime end)
        {
            return _context.Transactions
                .Where(x => start <= x.CreationDate && end >= x.CreationDate)
                .OrderByDescending(x => x.CreationDate);
        }
    }
}
