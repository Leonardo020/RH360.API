using MediatR;
using RH360.Domain.Entities;
using RH360.Infrastructure.Security;

namespace RH360.Application.Users.CreateUser
{
    public record CreateUserCommand(string Name, string Email, string Password) : IRequest<int>
    {
        public static explicit operator User(CreateUserCommand createUserCommand)
        {
            return new User
            {
                Name = createUserCommand.Name,
                Email = createUserCommand.Email,
                PasswordHash = Argon2PasswordHasher.HashPassword(createUserCommand.Password)
            };

        }
    }
}
