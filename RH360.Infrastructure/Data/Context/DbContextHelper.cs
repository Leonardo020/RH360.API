using Microsoft.EntityFrameworkCore;
using System;

namespace RH360.Infrastructure.Data.Context
{
    public static class DbContextHelper
    {
        public static ApplicationDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
