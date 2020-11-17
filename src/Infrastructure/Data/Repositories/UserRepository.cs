using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected UserRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
