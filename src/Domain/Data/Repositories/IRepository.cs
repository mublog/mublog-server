using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> FindById(long id);
        Task<long> Create(T entity);
        Task<bool> Remove(T entity);
    }
}