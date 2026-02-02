using MediatR;
using RH360.Infrastructure.Data.Context;
using RH360.Infrastructure.Security;

namespace RH360.Application.Users.UpdateUser
{
    public class UpdateUserPasswordCommandHandler(ApplicationDbContext DbContext) : IRequestHandler<UpdateUserPasswordCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = DbContext.Users.FirstOrDefault(user => user.Id == request.Id);
            
            if (user is null)
                return false;

            user.PasswordHash = Argon2PasswordHasher.HashPassword(request.Password);
            user.UpdatedAt = DateTime.UtcNow;

            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
