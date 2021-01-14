using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public static class PasswordSaltedHelper
    {
        private const int SALT_SIZE = 8;

        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SALT_SIZE];
            rng.GetBytes(buff);

            return buff;
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
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

        public static bool CompareHash(byte[] attemptedPassword, byte[] hash, byte[] salt)
        {
            string base64Hash = Convert.ToBase64String(hash);
            string base64AttemptedHash = Convert.ToBase64String(GenerateSaltedHash(attemptedPassword, salt));

            return base64Hash == base64AttemptedHash;
        }
    }
}
