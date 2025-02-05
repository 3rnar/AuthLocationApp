using AuthLocationApp.Application.CQRS.Users.Queries;
using AuthLocationApp.Application.CQRS.Users.Queries.Validations;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Users
{
    public class GetUserByIdQueryValidatorTests
    {
        private readonly GetUserByIdQueryValidator _validator;

        public GetUserByIdQueryValidatorTests()
        {
            _validator = new GetUserByIdQueryValidator();
        }

        [Fact]
        public void Should_HaveError_When_IdIsZero()
        {
            // Arrange
            var query = new GetUserByIdQuery(0);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "UserId must be greater than 0.");
        }

        [Fact]
        public void Should_HaveError_When_IdIsNegative()
        {
            // Arrange
            var query = new GetUserByIdQuery(-1);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "UserId must be greater than 0.");
        }

        [Fact]
        public void Should_NotHaveError_When_IdIsGreaterThanZero()
        {
            // Arrange
            var query = new GetUserByIdQuery(1);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
