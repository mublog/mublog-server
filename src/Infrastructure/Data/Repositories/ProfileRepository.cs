﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        protected ProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Profile> GetByUsername(string username)
        {
            return await Context.Profiles.FirstOrDefaultAsync(p => p.Username == username);
        }
    }
}