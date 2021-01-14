using Domain.Models.Entities;
using Infrastructure.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Exceptions.Users;

namespace UmHelpFinanceiro.Services.Users.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserAccountRepository _accountRepository;

        public UserService(IUserAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<bool> RegisterAsync(UserAccount user)
        {
            if (_accountRepository.Any(x => x.Cpf.Number == user.Cpf.Number)) throw new RegisteredCpfException();
            _accountRepository.Add(user);
            return _accountRepository.CommitAsync();
        }

        public async Task<double> DepositAsync(Guid id, double value)
        {
            var user = await _accountRepository.FindAsync(id);
            if (user == null) throw new KeyNotFoundException();

            user.AddToBalance(value);
            _accountRepository.Update(user);
            await _accountRepository.CommitAsync();
            return user.CurrentBalance;
        }

        public async Task<double> WithdrawAsync(Guid id, double value)
        {
            var user = await _accountRepository.FindAsync(id);
            if (user == null) throw new KeyNotFoundException();

            user.SubtractFromBalance(value);
            _accountRepository.Update(user);
            await _accountRepository.CommitAsync();
            return user.CurrentBalance;
        }
    }
}
