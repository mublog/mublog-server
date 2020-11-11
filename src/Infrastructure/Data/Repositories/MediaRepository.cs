using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    class MediaRepository : ARepository
    {
        public Media GetByGuid(Guid id) => this.context.Mediae.Where(m => m.PublicId == id).FirstOrDefault();
        public IQueryable<Media> GetByOwner(User owner) => this.context.Mediae.Where(m => m.Owner == owner);
        public IQueryable<Media> GetByPost(Post post) => this.context.Mediae.Where(m => m.Post == post);
    }
}
