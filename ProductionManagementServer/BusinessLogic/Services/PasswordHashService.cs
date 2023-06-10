using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal static class PasswordHashService
    {
        private static byte[] GenerateSalt()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];

            var rngRand = new RNGCryptoServiceProvider();
            rngRand.GetBytes(salt);

            return salt;
        }

        private static byte[] GenerateSha256Hash(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

            using var hash = new SHA256CryptoServiceProvider();
            
            return hash.ComputeHash(saltedPassword);
        }

        internal static string GeneratePasswordHash(string password)
        {
            var salt = GenerateSalt();

            return Convert.ToBase64String(GenerateSha256Hash(password, salt));
        }
    }
}
