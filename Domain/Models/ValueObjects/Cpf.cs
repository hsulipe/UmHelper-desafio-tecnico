using System;
using Domain.Exceptions.Users;

namespace Domain.Models.ValueObjects
{
    public class Cpf
    {
        public string Number { get; }

        public Cpf(string number)
        {
            Number = number;
            if(!IsCpf(Number)) throw new InvalidCpfException();
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public override string ToString()
        {
            return Number;
        }

        public override bool Equals(object? obj)
        {
            return this.Number == (obj as Cpf)?.Number;
        }

        protected bool Equals(Cpf other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (Number != null ? Number.GetHashCode() : 0);
        }

        public static bool operator == (Cpf first, Cpf second)
        {
            return first?.Number == second?.Number;
        }

        public static bool operator !=(Cpf first, Cpf second)
        {
            return !(first == second);
        }
    }
}
