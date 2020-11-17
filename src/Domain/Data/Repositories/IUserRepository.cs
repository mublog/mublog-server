using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}