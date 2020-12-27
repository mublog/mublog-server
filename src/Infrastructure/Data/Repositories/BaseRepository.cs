using System;
using System.Data;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly IDbConnection Connection;
        
        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        public virtual BaseEntity ApplyChanges(BaseEntity entity)
        {
            if (entity.CreatedDate == null)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                entity.UpdatedDate = DateTime.UtcNow;
            }

            return entity;
        }
    }
}