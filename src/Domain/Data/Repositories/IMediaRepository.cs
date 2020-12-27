using System;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IMediaRepository : IRepository<Media>
    {
        Task<Media> FindByPublicId(Guid id);
    }
}