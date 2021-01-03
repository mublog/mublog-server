using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<PagedList<T>> GetPaged(QueryParameters queryParameters);
        Task<T> FindByIdAsync(long id);
        Task<long> AddAsync(T entity);
        Task<bool> Remove(T entity);
        Task<bool> RemoveRange(IEnumerable<T> entities);
    }
}