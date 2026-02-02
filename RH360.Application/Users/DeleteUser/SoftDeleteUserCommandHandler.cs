using MediatR;
using Microsoft.EntityFrameworkCore;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;

namespace RH360.Application.Users.DeleteUser
{
    public class SoftDeleteUserCommandHandler(ApplicationDbContext DbContext) : IRequestHandler<SoftDeleteUserCommand, bool>
    {
        public async Task<bool> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(
                user => user.Id == request.Id, cancellationToken
            ) ?? throw new IdNotFoundException("User");

            if (user.DeletedAt is not null)
                return true;

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
