using MediatR;

namespace RH360.Application.Users.DeleteUser
{
    public record HardDeleteUserCommand(int Id) : IRequest<bool>;
}
