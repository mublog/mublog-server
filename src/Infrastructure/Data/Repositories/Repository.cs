using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext Context;

        protected Repository(AppDbContext context)
        {
            Context = context;
        }
        
        public IQueryable<T> Query()
        {
            return Context.Set<T>();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            await Context.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            Context.Update(entity);
            return SaveChanges();
        }

        public bool Remove(T entity)
        {
            Context.Remove(entity);
            return SaveChanges();
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            Context.RemoveRange(entities);
            return SaveChanges();
        }

        protected virtual bool SaveChanges() => Context.SaveChanges() >= 0;
        
        protected virtual async Task<bool> SaveChangesAsync() => await Context.SaveChangesAsync() >= 0;
        
    }
}