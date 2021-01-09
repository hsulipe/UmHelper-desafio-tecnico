using Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class UserAccount : EntityBase
    {
        public Cpf Cpf { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public double Balance { get; private set; }

        // Navigation Properties
        public virtual ICollection<Transaction> Sent { get; private set; }
        public virtual ICollection<Transaction> Received { get; private set; }

        protected UserAccount() : base() { }

        public UserAccount(string cpf, byte[] password, string firstName, string lastName,  double initialBalance) : base()
        {
            Cpf = new Cpf(cpf);
            Password = new Password(password);
            Name = new Name(firstName, lastName);
            Balance = (initialBalance > 0 ) ? initialBalance : throw new ArgumentException();
        }
    }
}
