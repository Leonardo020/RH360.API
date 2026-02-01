using FluentAssertions;
using RH360.Application.Users.DeleteUser;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;

namespace RH360.Tests.Users
{
    public class DeleteUserCommandHandlerTests
    {
        // Using xUnit

        [Fact]
        public async Task Should_Soft_Delete_User()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("DeleteUserTestDb");

            var user = new User
            {
                Name = "User",
                Email = "email@test.com",
                PasswordHash = "hash123",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var handler = new HardDeleteUserCommandHandler(db);

            var command = new HardDeleteUserCommand(user.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            var deleted = await db.Users.FindAsync(user.Id);

            Assert.NotEqual(null!, deleted!.DeletedAt);
        }

        // Using fluentAssertions

        [Fact]
        public async Task Should_Return_False_When_User_Not_Found()
        {
            var db = DbContextHelper.CreateInMemoryDbContext("DeleteUserNotFoundTestDb");

            var handler = new HardDeleteUserCommandHandler(db);

            var command = new HardDeleteUserCommand(123);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}
