using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Helpers;
using Profile = Mublog.Server.Domain.Data.Entities.Profile;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository, IDisposable
    {
        private readonly ILogger<PostRepository> _logger;
        private readonly AutoMapper.IMapper _mapper;

        public PostRepository(IDbConnection connection, ILogger<PostRepository> logger, AutoMapper.IMapper mapper) : base(connection)
        {
            _logger = logger;
            _mapper = mapper;
        }
        
        public PagedList<Post> GetPaged(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Post> FindByIdAsync(int id)
        {
            var sql = "SELECT * FROM posts WHERE id = @Id LIMIT 1;";

            var post = await Connection.QueryFirstAsync<Post>(sql, new {Id = id});

            return post;
        }

        public async Task<bool> AddAsync(Post entity)
        {
            entity.ApplyPostTimestamps();
            
            var sql = "INSERT INTO public.posts (date_created , date_updated, content, owner_id, date_post_edited) VALUES (@CreatedDate, @UpdatedDate, @Content, @OwnerId, @PostEditedDate);";
            
            var rowsAffected = await Connection.ExecuteAsync(sql, entity);

            return rowsAffected >= 1;
        }

        public async Task<bool> Update(Post entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Remove(Post entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveRange(IEnumerable<Post> entities)
        {
            throw new System.NotImplementedException();
        }

        public PagedList<PostWithLike> GetPagedWithLikes(PostQueryParameters queryParameters, Profile user = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PostWithLike> GetByPublicId(int id, string username = null)
        {
            var sql = "SELECT * FROM posts WHERE public_id = @PublicId LIMIT 1;";

            var post = await Connection.QueryFirstOrDefaultAsync<PGPostEntity>(sql, new {PublicId = id});

            if (post == null) return null;

            var test = "";
            
            var postWithLike = post.ToPostWithLike;

            return postWithLike;
        }

        public async Task<bool> AddLike(Post post, Profile user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveLike(Post post, Profile user)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}