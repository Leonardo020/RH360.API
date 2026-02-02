using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Utils;

namespace RH360.Infrastructure
{
    public static class InfrastructureConfigurationIOC
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetPostgreSqlConnectionString()));

            return services;
        }

    }
}
