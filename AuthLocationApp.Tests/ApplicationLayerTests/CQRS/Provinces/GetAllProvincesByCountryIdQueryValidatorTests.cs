using AuthLocationApp.Application.CQRS.Provinces.Queries;
using AuthLocationApp.Application.CQRS.Provinces.Queries.Validations;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Provinces
{
    public class GetAllProvincesByCountryIdQueryValidatorTests
    {
        private readonly GetAllProvincesByCountryIdQueryValidator _validator;

        public GetAllProvincesByCountryIdQueryValidatorTests()
        {
            _validator = new GetAllProvincesByCountryIdQueryValidator();
        }

        [Fact]
        public void Should_HaveError_When_CountryIdIsZero()
        {
            // Arrange
            var query = new GetAllProvincesByCountryIdQuery(0);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CountryId" && e.ErrorMessage == "CountryId must be greater than 0.");
        }

        [Fact]
        public void Should_HaveError_When_CountryIdIsNegative()
        {
            // Arrange
            var query = new GetAllProvincesByCountryIdQuery(-1);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CountryId" && e.ErrorMessage == "CountryId must be greater than 0.");
        }

        [Fact]
        public void Should_NotHaveError_When_CountryIdIsGreaterThanZero()
        {
            // Arrange
            var query = new GetAllProvincesByCountryIdQuery(1);

            // Act
            var result = _validator.Validate(query);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
