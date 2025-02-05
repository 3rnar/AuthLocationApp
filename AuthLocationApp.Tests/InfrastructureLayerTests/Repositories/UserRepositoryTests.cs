using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using AuthLocationApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Repositories
{
    public class UserRepositoryTests : RepositoryTestsBase
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository(_context, _userMapper.Object);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            ResetDatabase();

            var dbModel = new UserDbModel { Id = 1, Email = "test@example.com", PasswordHash = "hashedPassword", CountryId = 1, ProvinceId = 1 };
            var user = new User(1, "test@example.com", "hashedPassword", 1, 1);

            _context.Users.Add(dbModel);
            _context.SaveChanges();

            _userMapper.Setup(m => m.ToDomain(It.IsAny<UserDbModel>())).Returns(user);

            // Act
            var result = await _userRepository.GetUserByEmailAsync("test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email.ToString());
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            ResetDatabase();

            // Act
            var result = await _userRepository.GetUserByEmailAsync("test@example.com");

            // Assert
            Assert.Null(result);
        }
    }
}
