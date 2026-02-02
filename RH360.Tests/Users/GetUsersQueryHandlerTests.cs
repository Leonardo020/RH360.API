using FluentAssertions;
using RH360.Application.Users.GetUsers;
using RH360.Domain.Entities;
using RH360.Domain.Payloads;
using RH360.Infrastructure.Data.Context;

namespace RH360.Tests.Users
{
    public class GetUsersQueryHandlerTests
    {
        // Using xUnit
        [Fact]
        public async Task Should_Return_Paginated_Users()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("GetUsersPaginationDb");

            db.Users.AddRange(
                new User { Name = "User 1", Email = "u1@test.com", PasswordHash = "hash123", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "User 2", Email = "u2@test.com", PasswordHash = "hash456", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "User 3", Email = "u3@test.com", PasswordHash = "hash678", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "User 4", Email = "u4@test.com", PasswordHash = "hash891", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "User 5", Email = "u5@test.com", PasswordHash = "hash234", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            await db.SaveChangesAsync();

            var handler = new GetUsersQueryHandler(db);

            var paginationRequest = new PaginationRequestPayload
            {
                Page = 2,
                PageSize = 2
            };

            var query = new GetUsersQuery(paginationRequest);

            // Act
            var result = await handler.Handle(
                query,
                CancellationToken.None
            );

            // Assert
            Assert.Equal(2, result.Items.Count());  
            Assert.Equal(2, result.Page);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(5, result.TotalItems);

            // Itens esperados na página 2 (ordenando por Id)

            Assert.Equal("User 3", result.Items.First().Name);
            Assert.Equal("User 4", result.Items.Last().Name);
        }

        // Using FluentAssertions 

        [Fact]
        public async Task Should_Return_Users_Sorted_By_Name_Descending()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("SortingDescDb");

            db.Users.AddRange(
                new User { Name = "Peter Parker", Email = "z@test.com", PasswordHash = "hash123", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "Mary Jane", Email = "m@test.com", PasswordHash = "hash124", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new User { Name = "J. Jonah Jameson", Email = "l@test.com", PasswordHash = "hash126", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            await db.SaveChangesAsync();

            var handler = new GetUsersQueryHandler(db);

            var paginationRequest = new PaginationRequestPayload
            {
                Page = 1,
                PageSize = 10
            };

            var query = new GetUsersQuery(
                paginationRequest
            );

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Items.Should().HaveCount(3); 
            result.Page.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalItems.Should().Be(3);

            result.Items.First().Name.Should().Be("Peter Parker");
            result.Items.Last().Name.Should().Be("J. Jonah Jameson");
        }

    }
}
