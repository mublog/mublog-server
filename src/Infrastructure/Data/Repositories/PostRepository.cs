using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository
    {
        private readonly AppDbContext _db;

        public PostRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Post> GetAll() => _db.Posts;
        public async Task<Post> GetById(int id) => await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);
        public async Task<Post> GetByPublicId(int id) => await _db.Posts.FirstOrDefaultAsync(p => p.PublicId == id);
        public async Task<Post> GetByUserId(int id) => await _db.Posts.FirstOrDefaultAsync(p => p.OwnerId == id);     
    }
}
