using MediatR;
using RH360.Domain.DTO;

namespace RH360.Application.Users.GetUserByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<GetUserDto>;
}
