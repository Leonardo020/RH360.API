using RH360.Infrastructure.Security;

namespace RH360.Tests.Users
{
    public class Argo2PasswordHasherTests
    {
        // Using FluentValidation.TestHelper

        [Fact]
        public void HashPassword_ShouldReturnDifferentHashes_ForSamePassword()
        {
            // Arrange
            string password = "Teste123!";

            // Act
            string hash1 = Argon2PasswordHasher.HashPassword(password);
            string hash2 = Argon2PasswordHasher.HashPassword(password);

            // Assert
            Assert.NotEqual(hash1, hash2); // Salt diferente = hash diferente
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
        {
            // Arrange
            string password = "SenhaSuperForte!";
            string hash = Argon2PasswordHasher.HashPassword(password);

            // Act
            bool result = Argon2PasswordHasher.VerifyPassword(password, hash);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
        {
            // Arrange
            string password = "SenhaOriginal123";
            string wrongPassword = "SenhaErrada123";
            string hash = Argon2PasswordHasher.HashPassword(password);

            // Act
            bool result = Argon2PasswordHasher.VerifyPassword(wrongPassword, hash);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HashPassword_ShouldReturnBase64String()
        {
            // Arrange
            string password = "Teste123$";

            // Act
            string hash = Argon2PasswordHasher.HashPassword(password);

            // Assert
            byte[] bytes = Convert.FromBase64String(hash);
            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);
        }
    }
}
