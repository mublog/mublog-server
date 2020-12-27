using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Helpers;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<PagedList<Profile>> GetPaged(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Profile> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(Profile entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Profile entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Remove(Profile entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveRange(IEnumerable<Profile> entities)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Profile> FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}