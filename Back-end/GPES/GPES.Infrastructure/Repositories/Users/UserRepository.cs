using GPES.Domain.Models.Database;
using GPES.Infrastructure.Database;
using GPES.Infrastructure.Repositories.IRepositories;

namespace GPES.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly GPESContext _context;

        public UserRepository(GPESContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
