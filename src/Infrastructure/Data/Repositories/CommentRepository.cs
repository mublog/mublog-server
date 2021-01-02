using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<PagedList<Comment>> GetPaged(QueryParameters queryParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Comment> FindByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<long> AddAsync(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Remove(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveRange(IEnumerable<Comment> entities)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<Comment>> FindByPost(int postId)
        {
            throw new System.NotImplementedException();
        }
    }
}