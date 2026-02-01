using MediatR;
using RH360.API.Exceptions;
using RH360.Infrastructure.Data.Context;

namespace RH360.Application.Users.UpdateUser
{
    public class UpdateUserCommandHandler(ApplicationDbContext DbContext) : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = DbContext.Users.FirstOrDefault(user => user.Id == request.Id) 
                ?? throw new IdNotFoundException("User");
            
            user.Name = request.Name;
            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
