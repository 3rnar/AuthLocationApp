using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using AuthLocationApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Repositories
{
    public class CountryRepositoryTests : RepositoryTestsBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryRepositoryTests()
        {
            _countryRepository = new CountryRepository(_context, _countryMapper.Object);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnCountry_WhenCountryExists()
        {
            // Arrange
            ResetDatabase();

            var dbModel = new CountryDbModel { Id = 1, Name = "Country1" };
            var country = new Country(1, "Country1");

            _context.Countries.Add(dbModel);
            _context.SaveChanges();

            _countryMapper.Setup(m => m.ToDomain(It.IsAny<CountryDbModel>())).Returns(country);

            // Act
            var result = await _countryRepository.GetByNameAsync("Country1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Country1", result.Name);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenCountryDoesNotExist()
        {
            // Arrange
            ResetDatabase();

            // Act
            var result = await _countryRepository.GetByNameAsync("Country1");

            // Assert
            Assert.Null(result);
        }
    }
}
