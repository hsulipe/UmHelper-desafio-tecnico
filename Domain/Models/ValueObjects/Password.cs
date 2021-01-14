using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Models.ValueObjects
{
    public class Password
    {

        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(byte[] salt, byte[] hash)
        {
            Salt = salt;
            Hash = hash;
        }

        public override bool Equals(object? obj)
        {
            return this == obj as Password;
        }

        protected bool Equals(Password other)
        {
            return Equals(Hash, other.Hash);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hash);
        }

        public static bool operator == (Password first, Password second)
        {
            return first?.GetHashCode() == second?.GetHashCode();
        }

        public static bool operator !=(Password first, Password second)
        {
            return !(first == second);
        }
    }
}
