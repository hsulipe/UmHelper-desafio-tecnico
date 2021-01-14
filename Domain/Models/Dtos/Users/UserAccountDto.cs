using System;
using Domain.Models.Entities;
using Domain.Models.ValueObjects;

namespace Domain.Models.Dtos.Users
{
    public class UserAccountDto
    {
        public Guid Id { get; }
        public Cpf Cpf { get; }
        public Name Name { get; }
        public double Balance { get; }
        public DateTime CreationDate { get; }

        public UserAccountDto(Guid id, Cpf cpf, Name name, double balance, DateTime creationDate)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            Balance = balance;
            CreationDate = creationDate;
        }

        public static explicit operator UserAccountDto(UserAccount user)
            => new UserAccountDto(user.Id, user.Cpf, user.Name, user.CurrentBalance, user.CreationDate);
    }
}
