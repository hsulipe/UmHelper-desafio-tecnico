using Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace UmHelpFinanceiro.Services.Users
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserAccount user);
        Task<double> DepositAsync(Guid id, double value);
        Task<double> WithdrawAsync(Guid id, double value);
    }
}
