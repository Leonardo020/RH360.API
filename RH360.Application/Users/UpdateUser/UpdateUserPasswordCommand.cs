using MediatR;

namespace RH360.Application.Users.UpdateUser
{
    public record UpdateUserPasswordCommand(int Id, string Password) : IRequest<bool>;
}
