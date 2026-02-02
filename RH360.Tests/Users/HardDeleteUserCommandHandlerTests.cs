using RH360.Application.Users.DeleteUser;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;

namespace RH360.Tests.Users
{
    public class HardDeleteUserCommandHandlerTests
    {
        // Using xUnit

        [Fact]
        public async Task Should_Hard_Delete_User()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("HardDeleteUserTestDb");

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

            Assert.Equal(null!, deleted);
        }

        [Fact]
        public async Task Should_Throw_IdNotFoundException_When_User_Not_Found()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("HardDeleteUserTestDb");

            var handler = new HardDeleteUserCommandHandler(db);

            // Id inexistente
            var command = new HardDeleteUserCommand(999);

            // Act + Assert
            await Assert.ThrowsAsync<IdNotFoundException>(() =>
                handler.Handle(command, CancellationToken.None)
            );
        }
    }
}
