using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Repositories;
using Moq;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Repositories
{
    public class ProvinceRepositoryTests : RepositoryTestsBase
    {
        private readonly ProvinceRepository _provinceRepository;

        public ProvinceRepositoryTests()
        {
            _provinceRepository = new ProvinceRepository(_context, _provinceMapper.Object);
        }

        [Fact]
        public async Task GetAllByCountryIdAsync_ShouldReturnProvinces_WhenProvincesExist()
        {
            // Arrange
            ResetDatabase();

            var dbModel = new ProvinceDbModel { Id = 1, Name = "Province1", CountryId = 1 };
            var province = new Province(1, "Province1", 1);

            _context.Provinces.Add(dbModel);
            _context.SaveChanges();

            _provinceMapper.Setup(m => m.ToDomain(It.IsAny<ProvinceDbModel>())).Returns(province);

            // Act
            var result = await _provinceRepository.GetAllByCountryIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Province1", result.First().Name);
        }

        [Fact]
        public async Task GetAllByCountryIdAsync_ShouldReturnEmptyList_WhenNoProvincesExist()
        {
            // Arrange
            ResetDatabase();

            // Act
            var result = await _provinceRepository.GetAllByCountryIdAsync(1);

            // Assert
            Assert.Empty(result);
        }
    }
}
