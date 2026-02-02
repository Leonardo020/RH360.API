using MediatR;
using RH360.Domain.DTO;
using RH360.Domain.Payloads;

namespace RH360.Application.Users.GetUsers
{
    public record GetUsersQuery(PaginationRequestPayload Pagination) : IRequest<PaginationResponsePayload<GetUserDto>>
    {
        public PaginationRequestPayload Pagination { get; set; } = Pagination;
    }
}
