using MediatR;
using Microsoft.EntityFrameworkCore;
using RH360.Domain.DTO;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;

namespace RH360.Application.Users.GetUserById
{
    public class GetUserByIdQueryHandler(ApplicationDbContext DbContext) : IRequestHandler<GetUserByIdQuery, GetUserDto>
    {
        public async Task<GetUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await DbContext.Users.FirstOrDefaultAsync(
                user => user.Id == request.Id && user.DeletedAt == null, cancellationToken
            ) ?? throw new IdNotFoundException("User");

            return (GetUserDto)user;
        }
    }
}
