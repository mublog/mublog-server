using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
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

        public virtual IQueryable<T> Query()
        {
            return Context.Set<T>();
        }
        
        public virtual PagedList<T> GetPaged(QueryParameters queryParameters)
        {
            return PagedList<T>.ToPagedList(Query().AsNoTracking(), queryParameters.Page, queryParameters.Size);
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await Context.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public virtual Task<bool> Update(T entity)
        {
            Context.Update(entity);
            return SaveChangesAsync();
        }

        public virtual Task<bool> Remove(T entity)
        {
            Context.Remove(entity);
            return SaveChangesAsync();
        }

        public virtual Task<bool> RemoveRange(IEnumerable<T> entities)
        {
            Context.RemoveRange(entities);
            return SaveChangesAsync();
        }

        protected virtual bool SaveChanges()
        {
            return Context.SaveChanges() >= 0;
        }

        protected virtual async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() >= 0;
        }
    }
}