using RH360.Domain.Entities;
using RH360.Domain.Interfaces.Repositories;

namespace RH360.Infrastructure.Data.Repositories
{
    public class UserRepository() : IUserRepository
    {
        public Task<int> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
