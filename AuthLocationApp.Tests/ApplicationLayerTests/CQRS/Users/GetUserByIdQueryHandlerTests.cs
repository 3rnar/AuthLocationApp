using AuthLocationApp.Application.CQRS.Users.Queries;
using AuthLocationApp.Application.CQRS.Users.Queries.Handlers;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using Moq;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Users
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly GetUserByIdQueryHandler _handler;

        public GetUserByIdQueryHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new GetUserByIdQueryHandler(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsUser_When_UserExists()
        {
            // Arrange
            var userFromRepo = new User(1, "test@example.com", "hashedPassword", 1, 1);
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userFromRepo);

            var query = new GetUserByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
            Assert.Equal("test@example.com", result?.Email);
            Assert.Equal(1, result?.CountryId);
            Assert.Equal(1, result?.ProvinceId);
        }

        [Fact]
        public async Task Handle_ReturnsNull_When_UserDoesNotExist()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var query = new GetUserByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ThrowsException_When_RepositoryFails()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetUserByIdQuery(1);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
