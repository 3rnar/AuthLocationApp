using AuthLocationApp.Domain;
using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Tests.DomainLayerTests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsNullOrInvalid()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new User(1, "", "hashedPassword", 1, 1));
            Assert.Throws<DomainException>(() => new User(1, null!, "hashedPassword", 1, 1));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPasswordHashIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new User(1, "test@example.com", "", 1, 1));
            Assert.Throws<DomainException>(() => new User(1, "test@example.com", null!, 1, 1));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCountryIdOrProvinceIdIsInvalid()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new User(1, "test@example.com", "hashedPassword", 0, 1));
            Assert.Throws<DomainException>(() => new User(1, "test@example.com", "hashedPassword", 1, 0));
        }

        [Fact]
        public void Constructor_ShouldCreateUser_WhenValidDataProvided()
        {
            // Act
            var user = new User(1, "test@example.com", "hashedPassword", 1, 1);

            // Assert
            Assert.Equal(1, user.Id);
            Assert.Equal("test@example.com", user.Email.ToString());
            Assert.Equal("hashedPassword", user.PasswordHash);
            Assert.Equal(1, user.CountryId);
            Assert.Equal(1, user.ProvinceId);
        }
    }
}
