using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AuthLocationApp.Tests.InfrastructureLayerTests.Repositories
{
    public class RepositoryTestsBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<IMapper<CountryDbModel, Country>> _countryMapper;
        protected readonly Mock<IMapper<ProvinceDbModel, Province>> _provinceMapper;
        protected readonly Mock<IMapper<UserDbModel, User>> _userMapper;

        public RepositoryTestsBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _countryMapper = new Mock<IMapper<CountryDbModel, Country>>();
            _provinceMapper = new Mock<IMapper<ProvinceDbModel, Province>>();
            _userMapper = new Mock<IMapper<UserDbModel, User>>();
        }

        public void ResetDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
