using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Mappers
{
    public class ProvinceMapperTests
    {
        private readonly ProvinceMapper _mapper;

        public ProvinceMapperTests()
        {
            _mapper = new ProvinceMapper();
        }

        [Fact]
        public void ToDomain_ShouldReturnProvince_WhenDbModelIsValid()
        {
            // Arrange
            var dbModel = new ProvinceDbModel
            {
                Id = 1,
                Name = "Province1",
                CountryId = 1
            };

            // Act
            var result = _mapper.ToDomain(dbModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbModel.Id, result.Id);
            Assert.Equal(dbModel.Name, result.Name);
            Assert.Equal(dbModel.CountryId, result.CountryId);
        }

        [Fact]
        public void ToDbModel_ShouldReturnProvinceDbModel_WhenDomainModelIsValid()
        {
            // Arrange
            var domain = new Province(1, "Province1", 1);

            // Act
            var result = _mapper.ToDbModel(domain);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(domain.Id, result.Id);
            Assert.Equal(domain.Name, result.Name);
            Assert.Equal(domain.CountryId, result.CountryId);
        }
    }
}
