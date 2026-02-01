using MediatR;

namespace RH360.Application.Users.UpdateUser
{
    public record UpdateUserCommand(int Id, string Name, string Email) : IRequest<bool>;
}
