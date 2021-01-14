using Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Exceptions.Users;
using Domain.Helpers;

namespace Domain.Models.Entities
{
    public class UserAccount : EntityBase
    {
        public Cpf Cpf { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public double InitialBalance { get; }
        public double CurrentBalance { get; private set; }

        // Navigation Properties
        public virtual ICollection<Transaction> Sent { get; private set; }
        public virtual ICollection<Transaction> Received { get; private set; }

        protected UserAccount() : base() { }

        public UserAccount(string cpf, string password, string firstName, string lastName,  double initialBalance) : base()
        {
            Cpf = new Cpf(cpf);
            var salt = PasswordSaltedHelper.GenerateSalt();
            Password = new Password(salt, PasswordSaltedHelper.GenerateSaltedHash(Encoding.ASCII.GetBytes(password), salt));
            Name = new Name(firstName, lastName);
            InitialBalance = (initialBalance > 0 ) ? initialBalance : throw new ArgumentException();
            CurrentBalance = initialBalance;
        }

        public double AddToBalance(double value)
        {
            if (value <= 0) throw new ArgumentException();
            CurrentBalance += value;
            return CurrentBalance;
        }

        public double SubtractFromBalance(double value)
        {
            if (value <= 0) throw new ArgumentException();
            if (value > CurrentBalance) throw new InsufficientBalanceException();
            CurrentBalance -= value;
            return CurrentBalance;
        }

    }
}
