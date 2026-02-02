using FluentAssertions;
using RH360.Application.Users.GetUserById;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RH360.Tests.Users
{
    public class GetUserByIdQueryHandlerTests
    {
        // Using xUnit

        [Fact]
        public async Task Should_Return_User_When_Found()
        {
            var db = DbContextHelper.CreateInMemoryDbContext("GetByIdTestDb");

            var user = new User
            {
                Name = "User",
                Email = "user@test.com",
                PasswordHash = "hash123",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var handler = new GetUserByIdQueryHandler(db);

            var query = new GetUserByIdQuery(user.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEqual(null!, result);
            Assert.Equal("User", result!.Name);
        }

        // Using FluentAsertions

        [Fact]
        public async Task Should_Throw_IdNotFoundException_When_User_Not_Found()
        {
            var db = DbContextHelper.CreateInMemoryDbContext("GetByIdNotFoundDb");

            var handler = new GetUserByIdQueryHandler(db);

            var query = new GetUserByIdQuery(999);

            await Assert.ThrowsAsync<IdNotFoundException>(() =>
                handler.Handle(query, CancellationToken.None)
            );
        }
    }
}
