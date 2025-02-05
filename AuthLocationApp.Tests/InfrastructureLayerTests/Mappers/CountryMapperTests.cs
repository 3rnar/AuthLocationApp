using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Mappers
{
    public class CountryMapperTests
    {
        private readonly CountryMapper _mapper;

        public CountryMapperTests()
        {
            _mapper = new CountryMapper();
        }

        [Fact]
        public void ToDomain_ShouldReturnCountry_WhenDbModelIsValid()
        {
            // Arrange
            var dbModel = new CountryDbModel
            {
                Id = 1,
                Name = "Country1",
                Provinces = new List<ProvinceDbModel>
                {
                    new() { Id = 1, Name = "Province1", CountryId = 1 },
                    new() { Id = 2, Name = "Province2", CountryId = 1 }
                }
            };

            // Act
            var result = _mapper.ToDomain(dbModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbModel.Id, result.Id);
            Assert.Equal(dbModel.Name, result.Name);
            Assert.Equal(dbModel.Provinces.Count, result.Provinces.Count);
            Assert.Equal(dbModel.Provinces.First().Name, result.Provinces.First().Name);
        }

        [Fact]
        public void ToDbModel_ShouldReturnCountryDbModel_WhenDomainModelIsValid()
        {
            // Arrange
            var domain = new Country(1, "Country1");
            domain.AddProvince(new Province(1, "Province1", 1));
            domain.AddProvince(new Province(2, "Province2", 1));

            // Act
            var result = _mapper.ToDbModel(domain);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(domain.Id, result.Id);
            Assert.Equal(domain.Name, result.Name);
            Assert.Equal(domain.Provinces.Count, result.Provinces.Count);
            Assert.Equal(domain.Provinces.First().Name, result.Provinces.First().Name);
        }
    }
}
