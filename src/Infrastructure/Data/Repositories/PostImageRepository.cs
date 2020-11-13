using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostImageRepository
    {
        private readonly AppDbContext _db;

        public PostImageRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<PostImage> GetAll() => _db.PostImages;
        public async Task<PostImage> GetById(int id) => await _db.PostImages.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<PostImage> GetByGuid(Guid id) => await _db.PostImages.FirstOrDefaultAsync(m => m.PublicId == id);
        public async Task<IQueryable<PostImage>> GetByOwnerAsync(User owner) => _db.PostImages.Where(m => m.Owner == owner);
        // public async Task<Media> GetByPostAsync(Post post) => await _db.Mediae.Where(m => m.ParentPost == post);
        public async Task<PostImage> GetByPostIdAsync(int postId) => await _db.PostImages.FirstOrDefaultAsync(m => m.PostId == postId);
    }
}
