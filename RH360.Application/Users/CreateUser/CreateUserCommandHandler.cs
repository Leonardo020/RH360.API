using MediatR;
using RH360.Domain.Entities;
using RH360.Infrastructure.Data.Context;

namespace RH360.Application.Users.CreateUser
{
    public class CreateUserCommandHandler(ApplicationDbContext DbContext) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = (User)request;

            DbContext.Add(user);

            await DbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
