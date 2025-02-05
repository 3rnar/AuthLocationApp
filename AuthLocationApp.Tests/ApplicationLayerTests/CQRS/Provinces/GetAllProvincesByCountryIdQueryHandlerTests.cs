using AuthLocationApp.Application.CQRS.Provinces.Queries;
using AuthLocationApp.Application.CQRS.Provinces.Queries.Handlers;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using Moq;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Provinces
{
    public class GetAllProvincesByCountryIdQueryHandlerTests
    {
        private readonly Mock<IProvinceRepository> _mockProvinceRepository;
        private readonly GetAllProvincesByCountryIdQueryHandler _handler;

        public GetAllProvincesByCountryIdQueryHandlerTests()
        {
            _mockProvinceRepository = new Mock<IProvinceRepository>();
            _handler = new GetAllProvincesByCountryIdQueryHandler(_mockProvinceRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsProvinces_WhenRepositoryHasProvinces()
        {
            // Arrange
            var provincesFromRepo = new List<Province>
            {
                new(1, "Province1", 1),
                new(2, "Province2", 1)
            };

            _mockProvinceRepository
                .Setup(repo => repo.GetAllByCountryIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(provincesFromRepo);

            var query = new GetAllProvincesByCountryIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Province1", result[0].Name);
            Assert.Equal("Province2", result[1].Name);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _mockProvinceRepository
                .Setup(repo => repo.GetAllByCountryIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Province>());

            var query = new GetAllProvincesByCountryIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _mockProvinceRepository
                .Setup(repo => repo.GetAllByCountryIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetAllProvincesByCountryIdQuery(1);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
