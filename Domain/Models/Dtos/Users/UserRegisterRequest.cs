using System;
using System.Text;
using Domain.Models.Entities;

namespace Domain.Models.Dtos.Users
{
    public class UserRegisterRequest
    {
        public string Cpf { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public double InitialBalance { get; }


        public UserRegisterRequest(string cpf, string password, string firstName, string lastName, double initialBalance)
        {
            if (string.IsNullOrWhiteSpace(cpf) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(password) ||
                password.Length < 8)
                throw new ArgumentNullException();

            Cpf = cpf;
            Password = password; 
            FirstName = firstName;
            LastName = lastName;
            InitialBalance = initialBalance;
        }

        public static explicit operator UserAccount(UserRegisterRequest request)
            => new UserAccount(
                request.Cpf, 
                request.Password, 
                request.FirstName, 
                request.LastName,
                request.InitialBalance
                );
    }
}
