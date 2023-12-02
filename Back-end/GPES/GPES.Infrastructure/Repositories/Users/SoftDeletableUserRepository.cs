using GPES.Domain.Models.Database;
using GPES.Infrastructure.Database;
using GPES.Infrastructure.Repositories.IRepositories;

namespace GPES.Infrastructure.Repositories.Users
{
    public class SoftDeletableUserRepository : UserRepository, IUserRepository
    {
        public SoftDeletableUserRepository(GPESContext context)
            : base(context)
        {
        }

        public new async Task DeleteAsync(User entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
    }
}
