using MediatR;

namespace RH360.Application.Users.DeleteUser
{
    public record SoftDeleteUserCommand(int Id) : IRequest<bool>;
}
