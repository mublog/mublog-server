using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>
    {
        public PostRepository(AppDbContext db) : base(db)
        {
        }

        public IQueryable<Post> GetAll()
        {
            return _db.Posts;
        }
        
        public async Task<Post> GetById(int id)
        {
           return await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> GetByPublicId(int id)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.PublicId == id);
        }

        public async Task<Post> GetByUserId(int id)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.OwnerId == id);
        }
    }
}
