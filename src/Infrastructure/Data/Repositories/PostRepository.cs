using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Data.TransferEntities;


namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        private readonly ILogger<PostRepository> _logger;
        private readonly AutoMapper.IMapper _mapper;
        public PostRepository(IDbConnection connection, ILogger<PostRepository> logger, AutoMapper.IMapper mapper) :
            base(connection)
        {
            _logger = logger;
            _mapper = mapper; }

        public Task<PagedList<Post>> GetPaged(QueryParameters queryParameters)
            => GetPaged((PostQueryParameters)queryParameters, null);

        public async Task<PagedList<Post>> GetPaged(PostQueryParameters queryParameters, Profile profile)
        {
            var sql = "SELECT pst.id, pst.date_created, pst.public_id, pst.content, pst.date_post_edited, pst.date_updated, pst.owner_id, pfl.username, pfl.display_name, m.public_id AS profile_image_id, (SELECT COUNT(*) FROM posts_liked_by_profiles AS plp WHERE plp.liked_posts_id = pst.id) AS likes_amount ";
            if (profile != null && profile.Id != default) sql += ", exists(SELECT * FROM posts_liked_by_profiles AS plp LEFT JOIN posts p on p.id = plp.liked_posts_id WHERE plp.liking_profile_id = @ProfileId AND p.public_id = pst.public_id) AS liked ";
            sql += "FROM posts AS pst LEFT OUTER JOIN profiles AS pfl ON pst.owner_id = pfl.id LEFT OUTER JOIN mediae m on pfl.profile_image_id = m.id ";
            if (queryParameters.Username != default) sql += "WHERE pfl.username = @Username ";
            sql += "ORDER BY pst.public_id DESC LIMIT @PageSize OFFSET @PageOffset; ";
            sql += "SELECT count(*) FROM posts AS pst ";
            if (queryParameters.Username != default) sql += "LEFT OUTER JOIN profiles pfl on pst.owner_id = pfl.id WHERE pfl.username = @Username";
            sql += ";";

            var offset = (queryParameters.Page - 1) * queryParameters.Size;

             var results = await Connection.QueryMultipleAsync
                 (sql, new {queryParameters.Username, ProfileId = profile?.Id, PageSize = queryParameters.Size, PageOffset = offset});
             
            var pgPosts = await results.ReadAsync<TransferPost>();
            var totalRows = await results.ReadFirstOrDefaultAsync<long>();

            var posts = pgPosts.Select(p => p.ToPost(profile)).ToList();

            var pagedList = new PagedList<Post>(posts, (int) totalRows, queryParameters.Page, queryParameters.Size);

            return pagedList;
        }

        public async Task<Post> FindByIdAsync(long id)
        {
            var sql = "SELECT * FROM posts WHERE id = @Id LIMIT 1;";

            var post = await Connection.QueryFirstAsync<Post>(sql, new {Id = id});

            return post;
        }

        public async Task<long> AddAsync(Post post)
        {
            post.ApplyPostTimestamps();

            var sql = "INSERT INTO posts (date_created , date_updated, content, owner_id, date_post_edited) VALUES (@CreatedDate, @UpdatedDate, @Content, @OwnerId, @PostEditedDate) RETURNING id;";

            var id = await Connection.QuerySingleOrDefaultAsync<long>(sql, post);

            return id;
        }

        public async Task<bool> Remove(Post post)
        {
            var sql = "DELETE FROM posts WHERE id = @Id;";

            var rowsAffected = await Connection.ExecuteAsync(sql, post);

            return rowsAffected >= 1;
        }

        public async Task<Post> FindByPublicId(int publicId, Profile profile = null)
        {
            var sql = "SELECT pst.id, pst.date_created, pst.date_updated, pst.public_id, pst.content, pst.owner_id, pst.date_post_edited, pfl.username,pfl.display_name, m.public_id AS profile_image_id, (SELECT COUNT(*) FROM posts_liked_by_profiles AS plp WHERE plp.liked_posts_id = pst.id) AS likes_amount ";

            if (profile != null && profile.Id != default)
                sql += ", exists(SELECT * FROM posts_liked_by_profiles AS plp LEFT OUTER JOIN posts pst on pst.id = plp.liked_posts_id WHERE plp.liking_profile_id = @ProfileId AND pst.public_id = @PublicId) AS liked ";

            sql += "FROM posts AS pst LEFT JOIN profiles pfl on pfl.id = pst.owner_id LEFT OUTER JOIN mediae m on pfl.profile_image_id = m.id WHERE pst.public_id = @PublicId LIMIT 1;";


            var post = await Connection.QueryFirstOrDefaultAsync<TransferPost>(sql, new {PublicId = publicId, ProfileId = profile?.Id});

            if (post == null) return null;

            var postWithLike = post.ToPost(profile);

            return postWithLike;
        }

        public async Task<bool> AddLike(Post post, Profile profile)
        {
            var sql = "INSERT INTO posts_liked_by_profiles (liked_posts_id, liking_profile_id) VALUES (@PostId, @ProfileId);";

            var rowsAffected = await Connection.ExecuteAsync(sql, new {ProfileId = profile.Id, PostId = post.Id});

            return rowsAffected >= 1;
        }

        public async Task<bool> RemoveLike(Post post, Profile profile)
        {
            var sql = "DELETE FROM posts_liked_by_profiles WHERE liked_posts_id = @PostId AND liking_profile_id = @ProfileId;";

            var rowsAffected = await Connection.ExecuteAsync(sql, new {ProfileId = profile.Id, PostId = post.Id});

            return rowsAffected >= 1;
        }
        
        public async Task<bool> ChangeContent(Post post)
        {
            post.ApplyPostTimestamps();

            var sql = "UPDATE posts SET (date_updated, content, owner_id, date_post_edited) = (@UpdatedDate, @Content, @PostEditedDate) WHERE id = @Id;";

            var rowsAffected = await Connection.ExecuteAsync(sql, post);

            return rowsAffected >= 1;
        }
    }
}