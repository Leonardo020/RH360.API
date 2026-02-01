using MediatR;
using Microsoft.EntityFrameworkCore;
using RH360.Domain.DTO;
using RH360.Domain.Payloads;
using RH360.Infrastructure.Data.Context;

namespace RH360.Application.Users.GetUsers
{
    public class GetUsersQueryHandler(ApplicationDbContext DbContext) : IRequestHandler<GetUsersQuery, PaginationResponsePayload<GetUserDto>>
    {
        public async Task<PaginationResponsePayload<GetUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var pagination = request.Pagination;

            var query = DbContext.Users.AsQueryable();

            var totalItems = await query.CountAsync(cancellationToken);

            query = query.ApplyOrdering(pagination.OrderBy, pagination.OrderDirection);

            var users = await query
                .Skip(pagination.Skip)
                .Take(pagination.Take)
                .Select(user => (GetUserDto)user)
                .ToListAsync(cancellationToken);

            return new PaginationResponsePayload<GetUserDto>(
                users,
                totalItems,
                pagination.Page,
                pagination.PageSize
            );
        }
    }
}
