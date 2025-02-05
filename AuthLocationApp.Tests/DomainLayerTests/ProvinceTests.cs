using AuthLocationApp.Domain;
using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Tests.DomainLayerTests
{
    public class ProvinceTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => new Province(1, "", 1));
            Assert.Throws<DomainException>(() => new Province(1, null!, 1));
        }

        [Fact]
        public void ChangeName_ShouldThrowException_WhenNewNameIsNullOrEmpty()
        {
            // Arrange
            var province = new Province(1, "Province1", 1);

            // Act & Assert
            Assert.Throws<DomainException>(() => province.ChangeName(""));
            Assert.Throws<DomainException>(() => province.ChangeName(null!));
        }

        [Fact]
        public void ChangeName_ShouldUpdateName_WhenValidNameIsProvided()
        {
            // Arrange
            var province = new Province(1, "Province1", 1);

            // Act
            province.ChangeName("NewProvinceName");

            // Assert
            Assert.Equal("NewProvinceName", province.Name);
        }
    }
}
