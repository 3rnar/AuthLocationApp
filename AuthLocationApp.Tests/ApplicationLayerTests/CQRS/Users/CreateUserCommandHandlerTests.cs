using AuthLocationApp.Application.CQRS.Users.Commands;
using AuthLocationApp.Application.CQRS.Users.Commands.Handlers;
using AuthLocationApp.Application.DTOs.Users;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Application.Interfaces.Services;
using AuthLocationApp.Domain;
using AuthLocationApp.Domain.Exceptions;
using Moq;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Users
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _handler = new CreateUserCommandHandler(_mockUserRepository.Object, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task Handle_ThrowsException_When_UserWithEmailExists()
        {
            // Arrange
            var existingUser = new User(1, "test@example.com", "hashedPassword", 1, 1);
            _mockUserRepository.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", 1, 1));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal("User with this email already exists.", exception.Message);
        }

        [Fact]
        public async Task Handle_CreatesUser_When_ValidCommand()
        {
            // Arrange
            var existingUser = new User(1, "test@example.com", "hashedPassword", 1, 1);

            _mockUserRepository.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _mockPasswordHasher.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashedPassword");

            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", 1, 1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result > 0);
            _mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUserRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsZero_When_CreationFails()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null); // No user exists

            _mockPasswordHasher.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashedPassword");

            var command = new CreateUserCommand(new UserCreateDto("test@example.com", "Password123", 1, 1));

            // Мокаем ошибку при добавлении пользователя
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            });

            // Assert
            Assert.Equal("Database error", exception.Message);
        }
    }
}
