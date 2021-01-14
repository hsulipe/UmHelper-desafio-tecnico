using System;
using Domain.Models.ValueObjects;
using System.Text;
using Domain.Helpers;

namespace Domain.Models.Dtos.Users
{
    public class UserAuthenticateRequestDto
    {
        public string Cpf { get; private set; }
        public string Password { get; private set; }

        public UserAuthenticateRequestDto(string cpf, string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8) throw new ArgumentException();

            Cpf = cpf;
            Password = password;
        }
    }
}
