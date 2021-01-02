using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Data.TransferEntities;

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

        public async Task<Profile> FindByIdAsync(long id)
        {
            var sql = "SELECT pfl.id, pfl.date_created, pfl.date_updated, pfl.username, pfl.display_name, pfl.profile_image_id, pfl.user_state, m.public_id AS profile_image_public_id FROM profiles AS pfl LEFT OUTER JOIN mediae m on m.id = pfl.profile_image_id WHERE pfl.id = @Id LIMIT 1;";

            var transferProfile = await Connection.QueryFirstOrDefaultAsync<TransferProfile>(sql, new {Id = id});

            return transferProfile?.ToProfile();
        }

        public async Task<long> AddAsync(Profile profile)
        {
            var sql = "INSERT INTO profiles (date_created, date_updated, username, display_name) VALUES (@CreatedDate, @UpdatedDate, @Username, @DisplayName) RETURNING id;";

            var id = await Connection.QueryFirstOrDefaultAsync<long>(sql, profile);

            return id;
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
            var sql = "SELECT pfl.id, pfl.date_created, pfl.date_updated, pfl.username, pfl.display_name, pfl.profile_image_id, pfl.user_state, m.public_id AS profile_image_public_id FROM profiles AS pfl LEFT OUTER JOIN mediae m on m.id = pfl.profile_image_id WHERE username = @Username LIMIT 1;";

            var transferProfile = await Connection.QueryFirstOrDefaultAsync<TransferProfile>(sql, new {Username = username.ToLower()});

            return transferProfile?.ToProfile();
        }
    }
}