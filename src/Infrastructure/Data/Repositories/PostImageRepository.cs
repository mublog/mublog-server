using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostImageRepository : Repository<PostImage>, IPostImageRepository
    {
        protected PostImageRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<PostImage> GetByPublic(Guid id) => await _db.PostImages.FirstOrDefaultAsync(m => m.PublicId == id);
        public IQueryable<PostImage> GetByOwner(User owner) => _db.PostImages.Where(m => m.Owner == owner);
        public async Task<IQueryable<PostImage>> GetByPostAsync(Post post) =>  _db.PostImages.Where(pi => pi.PostId == post.Id);
        public async Task<PostImage> GetByPostId(int postId) => await _db.PostImages.FirstOrDefaultAsync(m => m.PostId == postId);
    }
}