using MediatR;
using RH360.Domain.DTO;

namespace RH360.Application.Users.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<GetUserDto>;
}
