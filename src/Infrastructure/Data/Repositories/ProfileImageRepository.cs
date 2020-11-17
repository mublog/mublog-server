using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class ProfileImageRepository : Repository<ProfileImage>, IProfileImageRepository
    {
        public ProfileImageRepository(AppDbContext db) : base(db) { }
        

        public async Task<ProfileImage> GetByPublicId(Guid id)
        {
            return await base._db.ProfileImages.FirstOrDefaultAsync(pi => pi.PublicId == id);
        }
    }
}