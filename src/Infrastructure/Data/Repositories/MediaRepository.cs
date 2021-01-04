using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class MediaRepository : BaseRepository, IMediaRepository
    {
        public MediaRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<Media> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> Create(Media media)
        {
            media.ApplyTimestamps();
            //todo: whats difference between id and public id?
            var sql = "INSERT INTO mediae (data_created, date_updated, public_id, media_type, owner_id) VALUES (@CreatedDate, @UpdatedDate, @PublicId, @MediaType, @OwnerId) RETURNING id;";

            var id = await Connection.QueryFirstOrDefaultAsync<long>(sql, media);

            return id;
        }
        
        public async Task<bool> Remove(Media entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Media> FindByPublicId(Guid id)
        {
            throw new NotImplementedException();

            var sql = "SELECT pfl.id, pfl.date_created, pfl.date_updated, pfl.username, pfl.display_name, pfl.description, pfl.profile_image_id, pfl.user_state, m.public_id AS profile_image_public_id, (SELECT count(*) FROM profiles_following_profile AS pfp WHERE pfp.following_id = @Id) AS follower_count, (SELECT count(*) FROM profiles_following_profile AS pfp WHERE pfp.follower_id = @Id) AS following_count FROM profiles AS pfl LEFT OUTER JOIN mediae m ON m.id = pfl.profile_image_id WHERE pfl.id = @Id LIMIT 1;";

            var media = await Connection.QueryFirstOrDefaultAsync<Media>(sql, new { Id = id });

            return media;
        }
    }
}