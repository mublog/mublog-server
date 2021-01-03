using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class MediaRepository : BaseRepository, IMediaRepository
    {
        public MediaRepository(IDbConnection connection) : base(connection)
        {
        }
        
        public Task<PagedList<Media>> GetPaged(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<Media> FindByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> AddAsync(Media entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Media entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(Media entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveRange(IEnumerable<Media> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<Media> FindByPublicId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}