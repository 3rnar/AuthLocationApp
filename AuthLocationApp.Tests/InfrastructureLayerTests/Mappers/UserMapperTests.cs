using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Mappers
{
    public class UserMapperTests
    {
        private readonly UserMapper _mapper;

        public UserMapperTests()
        {
            _mapper = new UserMapper();
        }

        [Fact]
        public void ToDomain_ShouldReturnUser_WhenDbModelIsValid()
        {
            // Arrange
            var dbModel = new UserDbModel
            {
                Id = 1,
                Email = "test@example.com",
                PasswordHash = "hashedPassword",
                CountryId = 1,
                ProvinceId = 1
            };

            // Act
            var result = _mapper.ToDomain(dbModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbModel.Id, result.Id);
            Assert.Equal(dbModel.Email, result.Email.ToString());
            Assert.Equal(dbModel.PasswordHash, result.PasswordHash);
            Assert.Equal(dbModel.CountryId, result.CountryId);
            Assert.Equal(dbModel.ProvinceId, result.ProvinceId);
        }

        [Fact]
        public void ToDbModel_ShouldReturnUserDbModel_WhenDomainModelIsValid()
        {
            // Arrange
            var domain = new User(1, "test@example.com", "hashedPassword", 1, 1);

            // Act
            var result = _mapper.ToDbModel(domain);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(domain.Id, result.Id);
            Assert.Equal(domain.Email.ToString(), result.Email);
            Assert.Equal(domain.PasswordHash, result.PasswordHash);
            Assert.Equal(domain.CountryId, result.CountryId);
            Assert.Equal(domain.ProvinceId, result.ProvinceId);
        }
    }
}
