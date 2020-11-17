using System;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IProfileImageRepository : IRepository<ProfileImage>
    {
        Task<ProfileImage> GetByPublicId(Guid id);
    }
}