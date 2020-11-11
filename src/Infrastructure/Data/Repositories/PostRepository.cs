using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    class PostRepository : ARepository
    {
        public Post GetByPublicId(int id) => this.context.Posts.Where(p => p.PublicId == id).FirstOrDefault();
        public IQueryable<Post> GetByUser(User user) => this.context.Posts.Where(p => p.PublicId == id);     
    }
}
