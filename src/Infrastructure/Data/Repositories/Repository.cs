using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _db;

        protected Repository(AppDbContext db)
        {
            _db = db;
        }
        
        public IQueryable<T> Query()
        {
            return _db.Set<T>();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            _db.Update(entity);
            return SaveChanges();
        }

        public bool Remove(T entity)
        {
            _db.Remove(entity);
            return SaveChanges();
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            return SaveChanges();
        }

        protected virtual bool SaveChanges() => _db.SaveChanges() >= 0;
        
        protected virtual async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;
        
    }
}