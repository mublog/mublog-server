using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class ProfileImageRepository : Repository<ProfileImage>, IProfileImageRepository
    {
        public ProfileImageRepository(AppDbContext context) : base(context) { }
        

        public async Task<ProfileImage> GetByPublicId(Guid id)
        {
            return await Context.ProfileImages.FirstOrDefaultAsync(pi => pi.PublicId == id);
        }
    }
}