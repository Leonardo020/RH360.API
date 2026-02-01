using RH360.Domain.Entities;

namespace RH360.Application.Users
{
    public static class UserQueryExtensions
    {
        public static IQueryable<User> ApplyOrdering(
          this IQueryable<User> query,
          string? orderBy,
          string? direction)
        {
            return (orderBy?.ToLower(), direction?.ToLower()) switch
            {
                ("name", "desc") => query.OrderByDescending(x => x.Name),
                ("name", _) => query.OrderBy(x => x.Name),

                ("email", "desc") => query.OrderByDescending(x => x.Email),
                ("email", _) => query.OrderBy(x => x.Email),

                ("createdat", "desc") => query.OrderByDescending(x => x.CreatedAt),
                ("createdat", _) => query.OrderBy(x => x.CreatedAt),

                ("updatedat", "desc") => query.OrderByDescending(x => x.UpdatedAt),
                ("updatedat", _) => query.OrderBy(x => x.UpdatedAt),

                ("id", "desc") => query.OrderByDescending(x => x.Id),
                _ => query.OrderBy(x => x.Id) 
            };
        }
    }
}
