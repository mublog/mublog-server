using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    abstract class ARepository<TEntity> where TEntity : BaseEntity
    {

        protected readonly AppDbContext context;

        public ARepository()
        {
            this.context = dbContext;
        }


    }
}
