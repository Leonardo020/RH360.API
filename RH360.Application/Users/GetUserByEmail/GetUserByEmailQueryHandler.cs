using MediatR;
using Microsoft.EntityFrameworkCore;
using RH360.Domain.DTO;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;

namespace RH360.Application.Users.GetUserByEmail
{
    public class GetUserByEmailQueryHandler(ApplicationDbContext DbContext) : IRequestHandler<GetUserByEmailQuery, GetUserDto?>
    {
        public async Task<GetUserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            User? user = await DbContext.Users.FirstOrDefaultAsync(
                user => user.Email.ToLower() == request.Email.ToLower(), cancellationToken
            );

            return user is null ? null : (GetUserDto)user;
        }
    }
}
