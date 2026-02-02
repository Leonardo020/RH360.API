using MediatR;
using Microsoft.EntityFrameworkCore;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Exceptions;

namespace RH360.Application.Users.DeleteUser
{
    public class HardDeleteUserCommandHandler(ApplicationDbContext DbContext) : IRequestHandler<HardDeleteUserCommand, bool>
    {
        public async Task<bool> Handle(HardDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(
                user => user.Id == request.Id, cancellationToken
            ) ?? throw new IdNotFoundException("User");

            DbContext.Remove(user);

            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
