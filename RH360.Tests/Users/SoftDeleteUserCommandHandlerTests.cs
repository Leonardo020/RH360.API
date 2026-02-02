using RH360.Application.Users.DeleteUser;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;

namespace RH360.Tests.Users
{
    public class SoftDeleteUserCommandHandlerTests
    {
        // Using xUnit

        [Fact]
        public async Task Should_Soft_Delete_User()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("SoftDeleteUserTestDb");

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

            var handler = new SoftDeleteUserCommandHandler(db);

            var command = new SoftDeleteUserCommand(user.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            var deleted = await db.Users.FindAsync(user.Id);

            Assert.NotEqual(null!, deleted!.DeletedAt);
        }


        [Fact]
        public async Task Should_Throw_IdNotFoundException_When_User_Not_Found()
        {
            // Arrange
            var db = DbContextHelper.CreateInMemoryDbContext("HardDeleteUserTestDb");

            var handler = new SoftDeleteUserCommandHandler(db);

            // Id inexistente
            var command = new SoftDeleteUserCommand(999);

            // Act + Assert
            await Assert.ThrowsAsync<IdNotFoundException>(() =>
                handler.Handle(command, CancellationToken.None)
            );
        }
    }
}
