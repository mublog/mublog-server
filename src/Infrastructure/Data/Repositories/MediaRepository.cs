using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class MediaRepository
    {
        private readonly AppDbContext _db;

        public MediaRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Media> GetAll() => _db.Mediae;
        public async Task<Media> GetById(int id) => await _db.Mediae.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<Media> GetByGuid(Guid id) => await _db.Mediae.FirstOrDefaultAsync(m => m.PublicId == id);
        public async Task<IQueryable<Media>> GetByOwnerAsync(User owner) => _db.Mediae.Where(m => m.Owner == owner);
        // public async Task<Media> GetByPostAsync(Post post) => await _db.Mediae.Where(m => m.ParentPost == post);
        public async Task<Media> GetByPostIdAsync(int postId) => await _db.Mediae.FirstOrDefaultAsync(m => m.PostId == postId);
    }
}
