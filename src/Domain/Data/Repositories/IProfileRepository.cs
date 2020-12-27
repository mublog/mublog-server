using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile> FindByUsername(string username);
    }
}