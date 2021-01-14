using Domain.Models.Entities;
using Infrastructure.Repositories.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DomainEvents.Transactions;
using Infrastructure.Adapters.MediatR;

namespace UmHelpFinanceiro.Services.Transactions.Impl
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMediatRClient _mediatR;

        public TransactionService(ITransactionRepository transactionRepository, IMediatRClient mediatR)
        {
            _transactionRepository = transactionRepository;
            _mediatR = mediatR;
        }

        public async Task RegisterAsync(Transaction transaction)
        {
            _transactionRepository.Add(transaction);
            await _mediatR.PublishDomainEvent(new RegisteredNewTransactionEvent(transaction));
            await _transactionRepository.CommitAsync();
        }

        public async Task RevertAsync(Guid id)
        {
            var transaction = _transactionRepository.FindAsync(id).GetAwaiter().GetResult();
            
            if (transaction == null) throw new KeyNotFoundException();
            
            _transactionRepository.Delete(transaction);
            await _mediatR.PublishDomainEvent(new RevertTransactionEvent(transaction));
            await _transactionRepository.CommitAsync();
        }
    }
}
