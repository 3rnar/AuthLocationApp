using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using AuthLocationApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AuthLocationApp.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly UserMapper _mapper;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _mapper = new UserMapper();
            _repository = new UserRepository(_context, _mapper);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            string email = "email@example.com";
            string passwordHash = "$2a$11$KeYfiYq7GfnPikU.EbHI/Ow53gZbSCVmvdOBJHkTteK3eswCyl1NG";
            int countryId = 1;
            int provinceId = 1;

            var user = new User(userId, email, passwordHash, countryId, provinceId);

            _context.Users.Add(_mapper.ToDbModel(user));
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(email, result.Email.ToString());
            Assert.Equal(passwordHash, result.PasswordHash);
            Assert.Equal(countryId, result.CountryId);
            Assert.Equal(provinceId, result.ProvinceId);
        }
    }
}
