using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostImageRepository
    {
        private readonly AppDbContext _db;

        public PostImageRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PostImage> GetByGuid(Guid id) => await _db.PostImages.FirstOrDefaultAsync(m => m.PublicId == id);
        public IQueryable<PostImage> GetByOwner(User owner) => _db.PostImages.Where(m => m.Owner == owner);
        public async Task<IQueryable<PostImage>> GetByPostAsync(Post post) =>  _db.PostImages.Where(pi => pi.PostId == post.Id);
        public async Task<PostImage> GetByPostId(int postId) => await _db.PostImages.FirstOrDefaultAsync(m => m.PostId == postId);
    }
}