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

        public async Task<long> AddAsync(Profile profile)
        {
            var sql = "INSERT INTO profiles (date_created, date_updated, username, display_name) VALUES (@CreatedDate, @UpdatedDate, @Username, @DisplayName) RETURNING id;";

            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Profile profile)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Remove(Profile profile)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveRange(IEnumerable<Profile> profiles)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Profile> FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}