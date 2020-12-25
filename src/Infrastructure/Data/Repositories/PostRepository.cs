using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        public PagedList<Post> GetPaged(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Post> FindByIdAsync(int id)
        {
            var sql = $"SELECT * FROM posts WHERE id = {id};";

            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(Post entity)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddLike(Post post, Profile user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveLike(Post post, Profile user)
        {
            throw new System.NotImplementedException();
        }
    }
}