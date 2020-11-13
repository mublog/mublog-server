using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {

        public UserRepository(AppDbContext db) : base(db)
        {
        }

        public IQueryable<User> GetAll() => _db.Users;
        public async Task<User> GetByUsername(string name) => await _db.Users.FirstOrDefaultAsync(u => u.Username == name);

    }
}
