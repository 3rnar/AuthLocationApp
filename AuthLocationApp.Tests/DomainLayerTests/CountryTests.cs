using AuthLocationApp.Domain;
using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Tests.DomainLayerTests
{
    public class CountryTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new Country(1, ""));
            Assert.Throws<DomainException>(() => new Country(1, null!));
        }

        [Fact]
        public void AddProvince_ShouldThrowException_WhenProvinceIsNull()
        {
            // Arrange
            var country = new Country(1, "Country1");

            // Act & Assert
            Assert.Throws<DomainException>(() => country.AddProvince(null!));
        }

        [Fact]
        public void RemoveProvince_ShouldThrowException_WhenProvinceDoesNotExist()
        {
            // Arrange
            var country = new Country(1, "Country1");
            var province = new Province(1, "Province1", 1);
            country.AddProvince(province);

            // Act & Assert
            Assert.Throws<DomainException>(() => country.RemoveProvince(2));
        }

        [Fact]
        public void RemoveProvince_ShouldRemoveProvince_WhenProvinceExists()
        {
            // Arrange
            var country = new Country(1, "Country1");
            var province = new Province(1, "Province1", 1);
            country.AddProvince(province);

            // Act
            country.RemoveProvince(1);

            // Assert
            Assert.Empty(country.Provinces);
        }
    }

}
