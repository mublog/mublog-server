using System;
using System.Data;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class BaseRepository : IDisposable

    {
        protected readonly IDbConnection Connection;

        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}