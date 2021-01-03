using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        // TODO implement change display name
        
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
            profile.ApplyTimestamps();
            
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
            // TODO
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

        public async Task<ICollection<Profile>> GetFollowers(Profile profile)
        {
            var sql = "SELECT pfl.id, pfl.date_created, pfl.date_updated, pfl.username, pfl.display_name, pfl.profile_image_id, pfl.user_state, m.public_id AS profile_image_public_id FROM profiles_following_profile LEFT OUTER JOIN profiles pfl on pfl.id = profiles_following_profile.follower_id LEFT OUTER JOIN mediae m on m.id = pfl.profile_image_id WHERE following_id = @ProfileId;";

            var transferProfiles = await Connection.QueryAsync<TransferProfile>(sql, new {ProfileId = profile.Id});

            var profiles = transferProfiles.Select(tp => tp.ToProfile()).ToList();

            return profiles;
        }

        public async Task<ICollection<Profile>> GetFollowing(Profile profile)
        {
            var sql = "SELECT pfl.id, pfl.date_created, pfl.date_updated, pfl.username, pfl.display_name, pfl.profile_image_id, pfl.user_state, m.public_id AS profile_image_public_id FROM profiles_following_profile LEFT OUTER JOIN profiles pfl on pfl.id = profiles_following_profile.follower_id LEFT OUTER JOIN mediae m on m.id = pfl.profile_image_id WHERE follower_id = @ProfileId;";

            var transferProfiles = await Connection.QueryAsync<TransferProfile>(sql, new {ProfileId = profile.Id});

            var profiles = transferProfiles.Select(tp => tp.ToProfile()).ToList();

            return profiles;
        }

        public async Task<bool> AddFollowing(Profile followingProfile, Profile followerProfile)
        {
            var sql = "INSERT INTO profiles_following_profile (following_id, follower_id) VALUES (@FollowingId, @FollowerId);";

            var rowsAffected = await Connection.ExecuteAsync(sql, new {FollowingId = followingProfile.Id, FollowerId = followerProfile.Id});

            return rowsAffected >= 1;
        }

        public async Task<bool> RemoveFollowing(Profile followingProfile, Profile followerProfile)
        {
            var sql = "DELETE FROM profiles_following_profile WHERE follower_id = @FollowerId AND following_id = @FollowingId;";

            var rowsAffected = await Connection.ExecuteAsync(sql, new {FollowingId = followingProfile.Id, FollowerId = followerProfile.Id});

            return rowsAffected >= 1;
        }

        public async Task<bool> ChangeDisplayName(Profile profile)
        {
            var sql = "UPDATE profiles SET date_updated = @UpdatedDate, display_name = @DisplayName WHERE id = @Id;";

            profile.ApplyTimestamps();
            
            var rowsAffected = await Connection.ExecuteAsync(sql, profile);

            return rowsAffected >= 1;
        }
    }
}