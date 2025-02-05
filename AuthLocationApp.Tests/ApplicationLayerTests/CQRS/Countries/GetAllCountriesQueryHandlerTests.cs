using AuthLocationApp.Application.CQRS.Countries.Queries;
using AuthLocationApp.Application.CQRS.Countries.Queries.Handlers;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using Moq;

namespace AuthLocationApp.Tests.ApplicationLayerTests.CQRS.Countries
{
    public class GetAllCountriesQueryHandlerTests
    {
        private readonly Mock<ICountryRepository> _mockCountryRepository;
        private readonly GetAllCountriesQueryHandler _handler;

        public GetAllCountriesQueryHandlerTests()
        {
            _mockCountryRepository = new Mock<ICountryRepository>();
            _handler = new GetAllCountriesQueryHandler(_mockCountryRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsCountries_WhenRepositoryHasCountries()
        {
            // Arrange
            var countriesFromRepo = new List<Country>
            {
                new(1, "Country1"),
                new(2, "Country2")
            };

            _mockCountryRepository
                .Setup(repo => repo.GetAllAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(countriesFromRepo);

            var query = new GetAllCountriesQuery();

            var handler = new GetAllCountriesQueryHandler(_mockCountryRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Country1", result[0].Name);
            Assert.Equal("Country2", result[1].Name);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _mockCountryRepository
                .Setup(repo => repo.GetAllAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Country>());

            var query = new GetAllCountriesQuery();
            var handler = new GetAllCountriesQueryHandler(_mockCountryRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _mockCountryRepository
                .Setup(repo => repo.GetAllAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetAllCountriesQuery();
            var handler = new GetAllCountriesQueryHandler(_mockCountryRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
