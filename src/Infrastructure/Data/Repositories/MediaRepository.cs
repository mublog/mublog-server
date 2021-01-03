using System;
using System.Data;
using System.Threading.Tasks;
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

        public async Task<long> Create(Media entity)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> Remove(Media entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Media> FindByPublicId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}