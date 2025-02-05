using AuthLocationApp.Application.CQRS.Users.Commands;
using AuthLocationApp.Application.CQRS.Users.Commands.Validations;
using AuthLocationApp.Application.DTOs.Users;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Users
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;

        public CreateUserCommandValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Fact]
        public void Should_HaveError_When_EmailIsEmpty()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("", "Password123", 1, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.Email" && e.ErrorMessage == "Email is required.");
        }

        [Fact]
        public void Should_HaveError_When_EmailIsInvalid()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("invalid-email", "Password123", 1, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.Email" && e.ErrorMessage == "Invalid email format.");
        }

        [Fact]
        public void Should_HaveError_When_PasswordIsEmpty()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "", 1, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.Password" && e.ErrorMessage == "Password is required.");
        }

        [Fact]
        public void Should_HaveError_When_PasswordIsWeak()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "weak", 1, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.Password" && e.ErrorMessage == "Password must contain at least one letter and one digit.");
        }

        [Fact]
        public void Should_HaveError_When_CountryIdIsNullOrLessThan1()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", null, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.CountryId" && e.ErrorMessage == "CountryId is required.");
        }

        [Fact]
        public void Should_HaveError_When_ProvinceIdIsNullOrLessThan1()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", 1, null));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "User.ProvinceId" && e.ErrorMessage == "ProvinceId is required.");
        }

        [Fact]
        public void Should_NotHaveError_When_CommandIsValid()
        {
            // Arrange
            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", 1, 1));

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
