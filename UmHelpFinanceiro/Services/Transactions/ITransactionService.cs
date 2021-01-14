using Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace UmHelpFinanceiro.Services.Transactions
{
    public interface ITransactionService
    {
        Task RegisterAsync(Transaction transaction);
        Task RevertAsync(Guid id);
    }
}
