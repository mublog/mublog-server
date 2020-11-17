using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected readonly AppDbContext _db;

        protected RepositoryBase(AppDbContext db)
        {
            _db = db;
        }

        public virtual async Task<bool> Add(T obj)
        {
            await _db.AddAsync(obj);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> AddRange(IEnumerable<T> objs)
        {
            await _db.AddRangeAsync(objs);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> Remove(T obj)
        {
            _db.Remove(obj);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> RemoveRange(IEnumerable<T> objs)
        {
            _db.RemoveRange(objs);
            return await SaveChangesAsync();
        }
        
        public virtual async Task<bool> Update(T obj)
        {
            _db.Update(obj);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> UpdateRange(IEnumerable<T> objs)
        {
            _db.UpdateRange(objs);
            return await SaveChangesAsync();
        }

        protected virtual async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() >= 0;
        }
    }
}