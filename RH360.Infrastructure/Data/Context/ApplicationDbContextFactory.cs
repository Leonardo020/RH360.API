using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RH360.Infrastructure.Data.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "RH360.API");

            var config = (new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build()) as ConfigurationManager;

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql("Host=localhost;Port=5432;Database=app_db;Username=postgres;Password=postgres;Trust Server Certificate=true;");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
