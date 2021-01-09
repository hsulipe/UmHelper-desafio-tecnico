using System.Security.Cryptography;

namespace Domain.Models.ValueObjects
{
    public class Password
    {
        private const int SALT_SIZE = 8;

        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(byte[] password)
        {
            Salt = GenerateSalt();
            Hash = GenerateSaltedHash(password, Salt);
        }

        private static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SALT_SIZE];
            rng.GetBytes(buff);

            return buff;
        }

        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}
