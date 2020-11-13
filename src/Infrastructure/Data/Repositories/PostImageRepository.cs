using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostImageRepository : RepositoryBase<PostImage>
    {
        public PostImageRepository(AppDbContext db) : base(db)
        {
        }

        public IQueryable<PostImage> GetAll()
        {
            return _db.PostImages;
        }

        public async Task<PostImage> GetById(int id)
        {
            return await _db.PostImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PostImage> GetByGuid(Guid id)
        {
            return await _db.PostImages.FirstOrDefaultAsync(m => m.PublicId == id);
        }

        public async Task<IQueryable<PostImage>> GetByOwner(User owner)
        {
            return _db.PostImages.Where(pi => pi.Owner.Id == owner.Id);
        }

        // public async Task<Media> GetByPostAsync(Post post) => await _db.Mediae.Where(m => m.ParentPost == post);
        public async Task<PostImage> GetByPostIdAsync(int postId)
        {
            return await _db.PostImages.FirstOrDefaultAsync(m => m.PostId == postId);
        }
    }
}
