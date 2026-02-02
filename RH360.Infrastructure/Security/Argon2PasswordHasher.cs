using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace RH360.Infrastructure.Security
{
    public class Argon2PasswordHasher
    {
        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, 
                Iterations = 4,          
                MemorySize = 1024 * 64  
            };

            byte[] hash = argon2.GetBytes(32);

            byte[] result = new byte[48];
            Buffer.BlockCopy(salt, 0, result, 0, 16);
            Buffer.BlockCopy(hash, 0, result, 16, 32);

            return Convert.ToBase64String(result);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] decoded = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(decoded, 0, salt, 0, 16);

            byte[] originalHash = new byte[32];
            Buffer.BlockCopy(decoded, 16, originalHash, 0, 32);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 64
            };

            byte[] computedHash = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(originalHash, computedHash);
        }
    }
}
