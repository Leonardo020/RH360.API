using Microsoft.Extensions.Configuration;

namespace RH360.Infrastructure.Utils
{
    public static class ConnectionStringUtils
    {
        public static string GetPostgreSqlConnectionString(this ConfigurationManager configuration)
        {
            return  configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString not found in configuration");
        }
    }
}
