using AuthLocationApp.Application.Interfaces.Services;
using AuthLocationApp.Infrastructure.Services;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Services
{
    public class BCryptPasswordHasherTests
    {
        private readonly IPasswordHasher _passwordHasher;

        public BCryptPasswordHasherTests()
        {
            _passwordHasher = new BCryptPasswordHasher();
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword_WhenGivenPlainPassword()
        {
            // Arrange
            string plainPassword = "Password123";

            // Act
            var hashedPassword = _passwordHasher.HashPassword(plainPassword);

            // Assert
            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrEmpty(hashedPassword));
            Assert.NotEqual(plainPassword, hashedPassword);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordsMatch()
        {
            // Arrange
            string plainPassword = "Password123";
            string hashedPassword = _passwordHasher.HashPassword(plainPassword);

            // Act
            var isVerified = _passwordHasher.VerifyPassword(plainPassword, hashedPassword);

            // Assert
            Assert.True(isVerified);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordsDoNotMatch()
        {
            // Arrange
            string plainPassword = "Password123";
            string wrongPassword = "WrongPassword";
            string hashedPassword = _passwordHasher.HashPassword(plainPassword);

            // Act
            var isVerified = _passwordHasher.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.False(isVerified);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenHashedPasswordIsIncorrect()
        {
            // Arrange
            string plainPassword = "Password123";
            string hashedPassword = "$2a$11$AoHqi5/edZYW6KgADAJFouT.A6iBsfLNUxNnF39b3xvvOS6vTA2Nz";

            // Act
            var isVerified = _passwordHasher.VerifyPassword(plainPassword, hashedPassword);

            // Assert
            Assert.False(isVerified);
        }
    }
}
