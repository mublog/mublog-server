using System;
using System.Data;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class BaseRepository : IDisposable

    {
        protected readonly IDbConnection Connection;

        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        public virtual void Dispose()
        {
            Connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}