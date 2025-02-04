using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AuthLocationApp.Infrastructure.Services
{
    internal class DatabaseCleanupService
    {
        private readonly string _dbFilePath;

        public DatabaseCleanupService(IConfiguration configuration, IHostApplicationLifetime applicationLifetime)
        {
            _dbFilePath = configuration.GetConnectionString("DefaultConnection")!.Replace("Data Source=", "");

            if (_dbFilePath.StartsWith("./"))
            {
                _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), _dbFilePath.Substring(2));
            }

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            if (File.Exists(_dbFilePath))
            {
                File.Delete(_dbFilePath);
            }
        }
    }
}
