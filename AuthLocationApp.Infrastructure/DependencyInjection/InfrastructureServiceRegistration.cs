using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Application.Interfaces.Services;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.Repositories;
using AuthLocationApp.Infrastructure.Seed;
using AuthLocationApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AuthLocationApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddSingleton();
            services.AddRepositories();
            services.AddDatabase(configuration, environment);
            return services;
        }

        private static IServiceCollection AddSingleton(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IProvinceRepository, ProvinceRepository>();
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, 
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                options.UseSqlite(connectionString);

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging(true);
                }
            });
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = Log.ForContext("Category", "DatabaseInit");

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                logger.Information("Starting database initialization...");
                await DbInitializer.SeedAsync(context);
                logger.Information("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred during database initialization: {Message}", ex.Message);
            }
        }
    }
}
